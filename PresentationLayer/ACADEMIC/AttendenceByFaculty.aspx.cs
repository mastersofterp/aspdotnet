using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_AttendenceByFaculty : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    DataSet ds, ds1, attDone;
    StudentAttendanceController objAttController = new StudentAttendanceController();
    StudentAttendance objAttendance = new StudentAttendance();
    int fromDashboardStatus;
    static string prevPage = String.Empty;
    string courseno = String.Empty;
    string coursename = String.Empty;
    string schemename = String.Empty;
    string sectionname = String.Empty;
    string SUBJECTTYPE = String.Empty;
    string Batchname = String.Empty;
    string sectionno = String.Empty;
    string batchno = String.Empty;
    string subid = String.Empty;
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
            if (Session["arr"] != null)
            {
                string[] data = Session["arr"] as string[];
                if (data != null)
                {
                    coursename = data[0].ToString();
                    schemename = data[1].ToString();
                    sectionname = data[2].ToString();
                    SUBJECTTYPE = data[3].ToString();
                    Batchname = data[4].ToString();
                    courseno = data[5].ToString();
                    sectionno = data[6].ToString();
                    batchno = data[7].ToString();
                    subid = data[8].ToString();
                }
            }

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                for (int i = 0; i < chkPeriod.Items.Count; i++)
                {
                    chkPeriod.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
                }

                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                //lblCurSession.ToolTip = Session["currentsession"].ToString();
                //lblCurSession.Text = Session["sessionname"].ToString();
                ViewState["batch"] = null;
                if (Session["usertype"].ToString() == "3")
                {
                    trTeacher.Visible = false;
                    if (coursename != "" || schemename != "" || sectionname != "" || SUBJECTTYPE != "" || Batchname != "")
                    {
                        dvSelection.Visible = false;
                        dvNext.Visible = true;
                        dvDates.Visible = false;
                        dvRegister.Visible = false;

                        lblCourse.Text += coursename.ToString() + " [<span style='color:Green'>" + schemename.ToString() + "</span>]" + " (<b><span style='color:Red'>Section : " + sectionname.ToString() + "</span>) " + " (<b><span style='color:Red'>" + SUBJECTTYPE.ToString() + "</span></b>) " + " (<b><span style='color:Blue'> Batch : " + Batchname.ToString() + "</span></b>)";
                        lblCourse.ToolTip = courseno;
                        fromDashboardStatus = 1;// 1 means from dashboard redirecting 
                        BindCourseName(courseno, sectionno, batchno, fromDashboardStatus);
                    }
                    else
                    {

                        fromDashboardStatus = 0;// 1 means from dashboard redirecting 
                    }
                }
                else if (Session["usertype"].ToString() == "1")
                {
                    trTeacher.Visible = true;
                    dvSelection.Visible = true;
                    dvNext.Visible = false;
                    dvDates.Visible = false;
                    dvRegister.Visible = false;
                    fromDashboardStatus = 0;// 1 means from dashboard redirecting 
                }
                Session["faculty"] = null;
                //Fill Dropdown Session
                objCommon.FillDropDownList(ddlSession, "ACD_TEACHINGPLAN TP INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=TP.SESSIONNO", "DISTINCT TP.SESSIONNO", "SM.SESSION_PNAME", "SM.SESSIONNO>0", "TP.SESSIONNO DESC");
                if (fromDashboardStatus == 1)
                {
                    ddlSession.SelectedValue = Session["currentsession"].ToString();
                }
                objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
                //objCommon.FillDropDownList(ddlAttby, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
                if (Session["usertype"].ToString() == "3")
                {
                    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO) CROSS APPLY DBO.Split(ADTEACHER,',') A CROSS APPLY DBO.Split(UA_NO,',') B", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "SR.SESSIONNO=" + ddlSession.SelectedValue + " AND (A.VALUE =" + Session["userno"].ToString() + " OR B.VALUE=" + Session["userno"].ToString()+")", "SC.SECTIONNO");
                }
                else if (Session["usertype"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "SR.SESSIONNO=" + ddlSession.SelectedValue + "", "SC.SECTIONNO");
                }
                // objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID>0", "SUBID");
                if (fromDashboardStatus == 1)
                {
                    ddlSection.SelectedValue = sectionno.ToString();
                    ddlSection_SelectedIndexChanged(sender, e);
                }
            }

        }
        ViewState.Add("DDLIndex", ddlTopics.SelectedIndex.ToString());
        // Clear message div
        divScript.InnerHtml = string.Empty;
        Session["arr"] = null;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=AttendenceByFaculty.aspx");
        }
    }
    #endregion

    #region Private Methods

    private void Clear()
    {
        ddlSubjectType.SelectedIndex = 0;
        hdfTot.Value = "0";
        txtTotStud.Text = "0";
        txtPresent.Text = "0";
        txtAbsent.Text = "0";
        txtAttendanceDate.Text = string.Empty;
        ddlClassType.SelectedIndex = 1;
        //chkPeriod.SelectedIndex = 1;
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (fromDashboardStatus == 1)
            {
                ddlSubjectType.SelectedValue = subid.ToString();
            }
            if (ddlSubjectType.SelectedIndex > 0)
            {
                ddlAttFor.Items.Clear();
                ddlAttFor.Items.Add(new ListItem("Please Select", "0"));
                ddlAttFor.Items.Add(new ListItem("Theory", "1"));
                ddlAttFor.Items.Add(new ListItem("Practical", "2"));
                ddlAttFor.Items.Add(new ListItem("Workshop", "3"));
                ddlAttFor.Items.Add(new ListItem("Project", "4"));
                ddlAttFor.Items.Add(new ListItem("Non Credit", "5"));

                if (ddlSubjectType.SelectedValue == "1")
                {
                    ddlAttFor.SelectedValue = "1";
                }
                else if (ddlSubjectType.SelectedValue == "2")
                {
                    ddlAttFor.SelectedValue = "2";
                }
                else if (ddlSubjectType.SelectedValue == "3")
                {
                    ddlAttFor.SelectedValue = "3";
                }
                else if (ddlSubjectType.SelectedValue == "4")
                {
                    ddlAttFor.SelectedValue = "4";
                }
                else if (ddlSubjectType.SelectedValue == "5")
                {
                    ddlAttFor.SelectedValue = "5";
                }

                AttendanceFor();

            }
            else
            {
                ddlAttFor.Items.Clear();
                ddlAttFor.Items.Add(new ListItem("Please Select", "0"));
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.ddlSubjectType_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void AttendanceFor()
    {
        try
        {
            if (ddlAttFor.SelectedIndex > 0)
            {
                if (ddlSubjectType.SelectedIndex > 0)
                {
                    lblMsg.Text = string.Empty;
                    if (ddlSubjectType.SelectedIndex == 0)
                    {
                        lvCourse.DataSource = null;
                        lvCourse.DataBind();
                        return;
                    }

                    MarksEntryController objMarksEntry = new MarksEntryController();

                    if (Session["usertype"].ToString() == "3")//teacher
                    {
                        ds = objMarksEntry.GetCourseForTeacherForAttendance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
                    }
                    else if (Session["usertype"].ToString() == "1")//admin
                    {
                        ds = objMarksEntry.GetCourseForTeacherForAttendance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
                    }
                    //else if (Session["usertype"].ToString() == "1" && ddlSubjectType.SelectedValue == "3")//admin
                    //{
                    //    ds = objMarksEntry.GetCourseForTeacherForAttendance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
                    //}
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        lvCourse.DataSource = ds;
                        lvCourse.DataBind();
                    }
                    else
                    {
                        lvCourse.DataSource = null;
                        lvCourse.DataBind();
                        objCommon.DisplayMessage(this.updpnl, "No Course is allotted.", this.Page);
                    }
                }
                else
                {
                    ddlAttFor.SelectedIndex = 0;
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
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
                objUCommon.ShowError(Page, "Academic_MarkEntry.btnShowCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView(int batch)
    {
        try
        {
            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            txtPresent.Text = "0";
            txtAbsent.Text = "0";

            string[] ccode1 = lblCourse.Text.Split(':');
            string ccode = ccode1[0].Trim();

            if (Session["usertype"].ToString() == "3")//teacher
            {
                // ds = objAttController.GetStudentAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), batch);
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                    ds = objAttController.GetStudentAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), batch, ccode.ToString(), txtAttendanceDate.Text.ToString());
                else
                    ds = objAttController.GetStudentAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), batch, ccode.ToString(), txtAttendanceDate.Text.ToString());
            }
            else if (Session["usertype"].ToString() == "1")
            {
                //  ds = objAttController.GetStudentAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), batch);
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                    ds = objAttController.GetStudentAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), batch, ccode.ToString(), txtAttendanceDate.Text.ToString());
                else
                    ds = objAttController.GetStudentAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), batch, ccode.ToString(), txtAttendanceDate.Text.ToString());

            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                txtTotStud.Text = ds.Tables[0].Rows.Count.ToString();
                //  txtPresent.Text = ds.Tables[0].Rows.Count.ToString();
                // txtAbsent.Text = ds.Tables[0].Rows.Count.ToString();
                hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                btnSubmit.Enabled = true;
                divScript.InnerHtml = "<script type='text/javascript' language='javascript'>CountSelection();</script>";
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceByFaculty.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewByAttendanceDate(int batch)
    {
        try
        {
            string[] ccode1 = lblCourse.Text.Split(':');
            string ccode = ccode1[0].Trim();

            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            txtPresent.Text = "0";
            txtAbsent.Text = "0";
            DataSet ds = null;
            //DataSet ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue));
            //changed by reena
            if (Session["usertype"].ToString() == "3" && (ddlClassType.SelectedValue == "1" || ddlClassType.SelectedValue == "3"))//teacher
            {
                // ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), lblCourse.ToolTip, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch,Convert.ToInt32(ddlSection.SelectedValue));
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), lblCourse.ToolTip, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(lblSection.Text), ccode.ToString());
                else
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), "0", Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(lblSection.Text), ccode.ToString());
            }
            else if (Session["usertype"].ToString() == "3" && ddlClassType.SelectedValue == "2")
            {
                // ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), (lblCourse.ToolTip), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue));
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), (lblCourse.ToolTip), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue), ccode.ToString());
                else
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), ("0"), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue), ccode.ToString());

            }
            else if (Session["usertype"].ToString() == "1" && ddlClassType.SelectedValue == "2")
            {
                //   ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), (lblCourse.ToolTip), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue));
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                {
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), (lblCourse.ToolTip), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue), ccode.ToString());
                }
                else
                {
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), ("0"), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue), ccode.ToString());
                }
            }
            else if (Session["usertype"].ToString() == "1" && (ddlClassType.SelectedValue == "1" || ddlClassType.SelectedValue == "3"))
            {
                // ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), lblCourse.ToolTip, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue));
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                {
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), lblCourse.ToolTip, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue), ccode.ToString());
                }
                else
                {
                    ds = objAttController.GetAttendenceByDate(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), ("0"), Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), batch, Convert.ToInt32(ddlSection.SelectedValue), ccode.ToString());
                }
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Enabled = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lvStudents.Visible = true;
                txtTotStud.Text = ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceByFaculty.BindListViewByAttendanceDate-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetCourseName(object coursename, object schemename, object sectionname, object SUBJECTTYPE, object Batchname)
    {
        return coursename.ToString() + " [<span style='color:Green'>" + schemename.ToString() + "</span>]" + " (<b><span style='color:Red'>Section : " + sectionname.ToString() + "</span>) " + " (<b><span style='color:Red'>" + SUBJECTTYPE.ToString() + "</span></b>) " + " (<b><span style='color:Blue'> Batch : " + Batchname.ToString() + "</span></b>)";
        // return coursename.ToString() + " [<span style='color:Green'>" + schemename.ToString() + "</span>]" + " (<b><span style='color:Red'>Section : " + sectionname.ToString() + "</span>) " + " (<b><span style='color:Red'>" + SUBJECTTYPE.ToString() + "</span></b>) " + " (<b><span style='color:Blue'> Batch : " + Batchname.ToString() + "</span></b>)";
    }

    #endregion

    #region Form Events

    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        this.ShowStudent();
        this.CheckPresenty();
    }

    private void CheckPresenty()
    {
        int PresentStatus = 0;
        int StudID = 0;
        int total = 0;
        int present = 0;
        int absent = 0;

        foreach (ListViewDataItem item in lvStudents.Items)
        {
            CheckBox chk = item.FindControl("chkRow") as CheckBox;
            if (chk.Checked == true)
            {
                present++;
            }
            total++;
        }
        foreach (ListViewDataItem item in lvStudents.Items)
        {
            CheckBox chkExtra = item.FindControl("chkExtraCrclr") as CheckBox;
            if (chkExtra.Checked == true)
            {
                present++;
            }
            //total++;
        }
        absent = total - present;
        txtAbsent.Text = absent.ToString();
        txtPresent.Text = present.ToString();
        txtTotStud.Text = total.ToString();

        string[] ccode1 = lblCourse.Text.Split(':');
        string ccode = ccode1[0].Trim();

        //GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());

        int sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        int uaNo = Convert.ToInt32(Session["userno"].ToString());
        string attDate = txtAttendanceDate.Text;
        int subId = Convert.ToInt32(ddlSubjectType.SelectedValue); //Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "SUBID", "CCODE='" + ccode + "'"));
        int thpr = Convert.ToInt32(ddlAttFor.SelectedValue); //Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "TH_PR", "CCODE='" + ccode + "'"));
        string courseNo = lblCourse.ToolTip; //objCommon.LookUp("ACD_ATTENDANCE", "COURSENO", "CCODE='" + ccode + "'");
        int classType = Convert.ToInt32(ddlClassType.SelectedValue); // Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "CLASS_TYPE", "CCODE='" + ccode + "'"));
        //int period = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "PERIOD", "CCODE='" + ccode + "'"));
        int period = Convert.ToInt32(chkPeriod.SelectedValue);
        int batchNo = ViewState["batch"] == string.Empty ? 0 : Convert.ToInt32(ViewState["batch"]);
        int sectionno = Convert.ToInt32(ddlSection.SelectedValue); //Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "SECTIONNO", "CCODE='" + ccode + "'"));

        DataSet ds2 = null;
        ds2 = objAttController.GetAttendenceByDate(sessionNo, uaNo, attDate, subId, thpr, courseNo, classType, period, batchNo, sectionno, ccode);

        foreach (ListViewDataItem lvitem1 in lvStudents.Items)
        {
            CheckBox ckh = lvitem1.FindControl("chkRow") as CheckBox;
            ckh.Checked = false;
            CheckBox ckhExtraCurP = lvitem1.FindControl("chkExtraCrclr") as CheckBox;
            int ExtraCurr_PresentStatus = 0;
            HiddenField hf = lvitem1.FindControl("hidStudentId") as HiddenField;
            int idno = Convert.ToInt32(hf.Value);

            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                StudID = Convert.ToInt32(ds2.Tables[0].Rows[i]["IDNO"].ToString());
                PresentStatus = Convert.ToInt32(ds2.Tables[0].Rows[i]["ATT_STATUS"].ToString());
                // ExtraCurr_PresentStatus = Convert.ToInt32(ds2.Tables[0].Rows[i]["Extra_Curr_ATT_STATUS"].ToString());
                ExtraCurr_PresentStatus = Convert.ToInt32(ds2.Tables[0].Rows[i]["EXTRA_CURR_STATUS"].ToString());
                if (StudID == idno)
                {
                    if (PresentStatus == 1)
                    {
                        ckh.Checked = true;
                        //ckh.Enabled = false;
                        //ckhExtraCurP.Enabled = false;
                        ckhExtraCurP.Checked = false;
                        ckhExtraCurP.Enabled = false;
                    }
                    else
                    {
                        if (ExtraCurr_PresentStatus == 1)
                        {
                            ckh.Checked = false;
                            ckh.Enabled = true;
                            ckhExtraCurP.Enabled = true;
                            ckhExtraCurP.Checked = true;
                        }
                    }
                }
            }
            //Added new on 2020 Jun 04 by Amit K

            HiddenField hidDiscplnrInfo = lvitem1.FindControl("hidDiscplnrInfo") as HiddenField;
            HiddenField hidDiscFromDt = lvitem1.FindControl("hidDiscFromDt") as HiddenField;
            HiddenField hidDiscToDt = lvitem1.FindControl("hidDiscToDt") as HiddenField;
            DateTime attdate = Convert.ToDateTime(txtAttendanceDate.Text);
            if (hidDiscplnrInfo.Value != "")
            {
                if ((attdate.CompareTo(Convert.ToDateTime(hidDiscFromDt.Value)) == 0 || attdate.CompareTo(Convert.ToDateTime(hidDiscFromDt.Value)) > 0) &&
                    (attdate.CompareTo(Convert.ToDateTime(hidDiscToDt.Value)) == 0 || attdate.CompareTo(Convert.ToDateTime(hidDiscToDt.Value)) < 0))
                {
                    ckh.Enabled = false;
                }
                //else
                //{
                //    chk.Enabled = true;
                //}
            }

            //Added new on 2020 Jun 04 by Amit K end
        }

    }

    private void ShowStudent()
    {
        try
        {
            string[] ccode1 = lblCourse.Text.Split(':');
            string ccode = ccode1[0].Trim();

            DataSet GetDuplicateds = null;
            if (Convert.ToInt32(ViewState["batch"]) > 0)
            {
                //i++;
                if (Session["usertype"].ToString() == "3" && ddlClassType.SelectedValue == "3")//teacher 2 in 3
                {
                    //GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                }
                else if (ddlClassType.SelectedValue == "3" && Session["usertype"].ToString() == "1")  // 2 in 3
                {
                    // GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                }
                else if (Session["usertype"].ToString() == "1" && (ddlClassType.SelectedValue == "1" || ddlClassType.SelectedValue == "3"))
                {
                    // GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                }
                else if (Session["usertype"].ToString() == "1" && (ddlClassType.SelectedValue == "2" || ddlClassType.SelectedValue == "2"))
                {
                    // GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                }
                else if (Session["usertype"].ToString() == "3" && (ddlClassType.SelectedValue == "1" || ddlClassType.SelectedValue == "2" || ddlClassType.SelectedValue == "3"))
                {
                    //GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                    }

                }

                if (GetDuplicateds.Tables.Count > 0 && GetDuplicateds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = GetDuplicateds.Tables[0].Rows[0];

                    string batch = objCommon.LookUp("ACD_BATCH", "BATCHNAME", "BATCHNO=" + Convert.ToInt32(ViewState["batch"]));

                    objCommon.DisplayMessage(this.updpnl, "Selected Date and Slot Attendance Already Entered For Batch " + batch.ToString() + "!!", this.Page);

                    if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "3")//admin/teacher
                    {

                        if (ddlAttFor.SelectedValue == "2")
                        {
                            txtRemark.Text = dr["REMARK"].ToString();
                            string addSlot = dr["ADDITIONAL_SLOT"].ToString();
                            if (!string.IsNullOrEmpty(addSlot))
                            {
                                string[] arr = addSlot.Split(',');
                                for (int i = 0; i <= arr.Length - 1; i++)
                                {
                                    for (int j = 0; j <= chkPeriod.Items.Count - 1; j++)
                                    {
                                        if (chkPeriod.Items[j].Value == arr[i])
                                        {
                                            chkPeriod.Items[j].Selected = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            chkPeriod.SelectedValue = dr["PERIOD"].ToString();
                        }

                        // txtTopic.Text = dr["TOPIC_COVERED"].ToString().Equals(DBNull.Value) ? string.Empty : dr["TOPIC_COVERED"].ToString();

                        BindListViewByAttendanceDate(Convert.ToInt32(ViewState["batch"]));

                        ViewState["action"] = "edit";
                        int retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "  AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ViewState["batch"]) + ""));  //changed by reena on 4_10_16
                        ViewState["retAttNo"] = retAttNo;
                    }
                }
                else
                {
                    BindListView(Convert.ToInt32(ViewState["batch"]));
                    int retAttNo;
                    if (lblCourse.ToolTip != "0")
                    {
                        //retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "ISNULL(ATT_NO,0) ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "  AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ViewState["batch"])));  //changed by reena on 4_10_16
                        retAttNo = objCommon.LookUp("ACD_ATTENDANCE", "ISNULL(ATT_NO,0) ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "  AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ViewState["batch"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "ISNULL(ATT_NO,0) ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "  AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ViewState["batch"])));
                    }
                    else
                    {
                        //retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "ISNULL(ATT_NO,0) ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "  AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "AND CCODE='" + ccode.ToString() + "' AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ViewState["batch"])));
                        retAttNo = objCommon.LookUp("ACD_ATTENDANCE", "ISNULL(ATT_NO,0) ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "  AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "AND CCODE='" + ccode.ToString() + "' AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ViewState["batch"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "ISNULL(ATT_NO,0) ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "  AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "AND CCODE='" + ccode.ToString() + "' AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ViewState["batch"])));
                    }
                    ViewState["retAttNo"] = retAttNo;
                    ViewState["action"] = "add";
                }
            }
            else
            {
                if (Session["usertype"].ToString() == "3" && (ddlClassType.SelectedValue == "1" || ddlClassType.SelectedValue == "3"))//teacher
                {
                    //Added by Deepali on 11/01/2020 to check attendance against date and slot
                    int checkduplicate = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(ATT_NO)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)" + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + "AND SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)));
                    
                        if (checkduplicate > 0)
                        {
                            objCommon.DisplayMessage(this.updpnl, "Selected Date and slot Attendance Already Entered for Alternate/Extra Class !", this.Page);
                            return;
                        }
                    //end
                    
                    //GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                }

                else if (Session["usertype"].ToString() == "1" && ddlClassType.SelectedValue == "2")//admin
                {
                    int checkduplicate = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(ATT_NO)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)" + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + "AND SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)));

                    if (checkduplicate > 0)
                    {
                        objCommon.DisplayMessage(this.updpnl, "Selected Date and slot Attendance Already Entered for Alternate/Extra Class !", this.Page);
                        return;
                    }

                    // GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                }
                else if (Session["usertype"].ToString() == "1" && (ddlClassType.SelectedValue == "1" || ddlClassType.SelectedValue == "3"))//admin
                {
                    int checkduplicate = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(ATT_NO)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)" + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + "AND SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)));

                    if (checkduplicate > 0)
                    {
                        objCommon.DisplayMessage(this.updpnl, "Selected Date and slot Attendance Already Entered for Alternate/Extra Class !", this.Page);
                        return;
                    }
                    //GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                }
                else if (ddlClassType.SelectedValue == "2" && Session["usertype"].ToString() == "3")
                {

                    int checkduplicate = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(ATT_NO)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)" + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + "AND SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)));

                    if (checkduplicate > 0)
                    {
                        objCommon.DisplayMessage(this.updpnl, "Selected Date and slot Attendance Already Entered for Alternate/Extra Class !", this.Page);
                        return;
                    }
                    // GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0));
                    if (lblCourse.ToolTip != "0")
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                    else
                    {
                        GetDuplicateds = objAttController.GetDuplicateAttendence(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    }
                }

                if (GetDuplicateds.Tables.Count > 0 && GetDuplicateds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = GetDuplicateds.Tables[0].Rows[0];

                    DataSet dsDuplicatecheck = objAttController.GetAttendenceByDate_Edit(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());
                    if (dsDuplicatecheck.Tables[0].Rows.Count > 0)
                    {
                        //{
                        objCommon.DisplayMessage(this.updpnl, "Selected Date and slot Attendance Already Entered for Regular/Extra Class !!", this.Page);

                        //}

                        if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "3")//admin
                        {
                            chkPeriod.SelectedValue = dr["PERIOD"].ToString();
                            // txtTopic.Text = dr["TOPIC_COVERED"].ToString().Equals(DBNull.Value) ? string.Empty : dr["TOPIC_COVERED"].ToString();
                            BindListViewByAttendanceDate(Convert.ToInt32(ViewState["batch"]));

                            ViewState["action"] = "edit";
                            //// int retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "ATT_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103) AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue));

                            //if (ddlAttFor.SelectedValue == "1")
                            //{
                            //    int retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "UA_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "and th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue));
                            //    //ViewState["retAttNo"] = retAttNo;
                            //    string Faculty = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + retAttNo);

                            //    if (Session["usertype"].ToString() == "1")
                            //    {
                            //        if (retAttNo == Convert.ToInt32(ddlTeacher.SelectedValue))
                            //            btnSubmit.Enabled = true;
                            //        else
                            //        {
                            //            btnSubmit.Enabled = false;
                            //            lblMsgw.Text = "Attendance Already taken by the " + Faculty.ToString();
                            //            pmsg.Visible = true;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        if (retAttNo == Convert.ToInt32(Session["userno"].ToString()))
                            //            btnSubmit.Enabled = true;
                            //        else
                            //        {
                            //            btnSubmit.Enabled = false;
                            //            lblMsgw.Text = "Attendance Already taken by the " + Faculty.ToString();
                            //            pmsg.Visible = true;
                            //        }
                            //    }
                            //}

                            if (ddlAttFor.SelectedValue == "1")
                            {
                                //int retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "UA_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "and th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND SECTIONNO=" + lblSection.Text));
                                int retAttNo;
                                if (lblCourse.ToolTip != "0")
                                    retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "UA_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "and th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND SECTIONNO=" + lblSection.Text));
                                else
                                    retAttNo = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "UA_NO", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "and th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + " AND ccode='" + ccode.ToString() + "' AND SESSIONNO=" + ddlSession.SelectedValue + " AND PERIOD=" + chkPeriod.SelectedValue + " AND SECTIONNO=" + lblSection.Text));

                                //ViewState["retAttNo"] = retAttNo;
                                string Faculty = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + retAttNo);

                                if (Session["usertype"].ToString() == "1")
                                {
                                    if (retAttNo == Convert.ToInt32(ddlTeacher.SelectedValue))
                                        btnSubmit.Enabled = true;
                                    else
                                    {
                                        btnSubmit.Enabled = false;
                                        lblMsgw.Text = "Attendance Already taken by the " + Faculty.ToString();
                                        pmsg.Visible = true;
                                    }
                                }
                                else
                                {
                                    if (retAttNo == Convert.ToInt32(Session["userno"].ToString()))
                                        btnSubmit.Enabled = true;
                                    else
                                    {
                                        btnSubmit.Enabled = false;
                                        lblMsgw.Text = "Attendance Already taken by the " + Faculty.ToString();
                                        pmsg.Visible = true;
                                    }
                                }
                            }

                            if (ddlAttFor.SelectedValue == "2")
                            {
                                string addSlot = dr["ADDITIONAL_SLOT"].ToString();
                                if (!string.IsNullOrEmpty(addSlot))
                                {
                                    string[] arr = addSlot.Split(',');
                                    for (int i = 0; i <= arr.Length - 1; i++)
                                    {
                                        for (int j = 0; j <= chkPeriod.Items.Count - 1; j++)
                                        {
                                            if (chkPeriod.Items[j].Value == arr[i])
                                            {
                                                chkPeriod.Items[j].Selected = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                chkPeriod.SelectedValue = dr["PERIOD"].ToString();
                            }
                            // txtTopic.Text = dr["TOPIC_COVERED"].ToString().Equals(DBNull.Value) ? string.Empty : dr["TOPIC_COVERED"].ToString();
                            BindListViewByAttendanceDate(Convert.ToInt32(ViewState["batch"]));

                            ViewState["action"] = "edit";
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnl, "Selected Date and slot Attendance Already Entered for Regular/Extra Class/Alternate Class !!", this.Page);
                        return;
                    }
                }
                else
                {
                    BindListView(0);
                    ViewState["retAttNo"] = null;
                    ViewState["action"] = "add";
                }
            }
            //BindListView(0);line number 862
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceByFaculty.ShowStudent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            //added by sumit on 25-jan-2020
            //if (ddlTopics.SelectedIndex == 0)
            //{
            //    objCommon.DisplayMessage(this.updpnl, "Please Select Topic Covered", this.Page);
            //    return;
            //}
            if (ddlClassType.SelectedValue == "1" && ddlTopics.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updpnl, "Please Select Topic Covered its mandetory for " + ddlClassType.SelectedItem.Text + " !!", this.Page);
                return;
            }
            else if (ddlClassType.SelectedValue == "3" && ddlTopics.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updpnl, "Please Select Topic Covered its mandetory for " + ddlClassType.SelectedItem.Text + " !!", this.Page);
                return;
            }


            string[] ccode1 = lblCourse.Text.Split(':');
            string ccode = ccode1[0].Trim();

            // btnSubmit.Enabled = false;
            int select = 0;
            int count = 0;
            string Pr_Slots = string.Empty;
            for (int i = 0; i < chkPeriod.Items.Count; i++)
            {
                if (chkPeriod.Items[i].Selected == true)
                {
                    Pr_Slots = Pr_Slots + chkPeriod.Items[i].Value + ",";
                    select++;
                    count++;
                }

            }
            if (Pr_Slots.Length > 0)
            {
                Pr_Slots = Pr_Slots.Substring(0, Pr_Slots.Length - 1);
            }
            if (select > 0)
            {
                if (count == 1)
                {
                    int row_count = 0;
                    for (int i = 0; i < chkPeriod.Items.Count; i++)
                    {
                        if (chkPeriod.Items[i].Selected == true)
                        {
                            row_count++;
                            if (ddlAttFor.SelectedValue == "2")
                            {
                                if (row_count > 1)
                                {
                                    break;
                                }
                            }
                            char sp = ',';
                            string[] course = ddlCourse.SelectedValue.Split(sp);

                            string studentId = string.Empty;

                            objAttendance.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                            if (ddlClassType.SelectedValue == "3" & Convert.ToInt32(chkPeriod.Items[i].Value) > -1)//EXTRA  2 AGAINST 3
                            // if (ddlClassType.SelectedValue == "3" & Convert.ToInt32(chkPeriod.Items[i].Value) > 0)//EXTRA  2 AGAINST 3
                            {
                                //objAttendance.UaNo = Convert.ToInt32(Session["userno"]);
                                if (ddlAttby.SelectedIndex > 0 & ddlCourse.SelectedIndex > 0)
                                {
                                    if (chkSwap.Checked == true)
                                    {
                                        objAttendance.AguaNo = Convert.ToInt32(ddlAttby.SelectedValue);
                                        objAttendance.AgsectionNo = Convert.ToInt32(lblSection.Text);
                                        objAttendance.AgCourseNo = Convert.ToInt32(lblCourse.ToolTip);
                                    }
                                    else
                                    {
                                        objAttendance.AguaNo = Convert.ToInt32(ddlAttby.SelectedValue);
                                        // objAttendance.AgsectionNo = Convert.ToInt32(course[1]);
                                        objAttendance.AgsectionNo = Convert.ToInt32(lblSection.Text);
                                        objAttendance.AgCourseNo = Convert.ToInt32(course[0]);
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updpnl, "Please Select Teacher name and course.", this.Page);
                                    return;
                                }
                            }

                            if (Session["usertype"].ToString() == "3")//teacher
                            {
                                objAttendance.UaNo = Convert.ToInt32(Session["userno"]);
                            }
                            else if (Session["usertype"].ToString() == "1")//admin
                            {
                                objAttendance.UaNo = Convert.ToInt32(ddlTeacher.SelectedValue);
                            }

                            if (chkSwap.Checked == true)
                            {
                                int SchemeNo = 0;
                                if (ViewState["ccode"].ToString() == string.Empty)
                                {
                                    SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(course[0])));
                                }
                                objAttendance.SchemeNo = Convert.ToInt32(SchemeNo);

                                int semesterNo = 0;
                                if (ViewState["ccode"].ToString() == string.Empty)
                                {
                                    semesterNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SEMESTERNO", "COURSENO=" + Convert.ToInt32(course[0])));
                                }

                                objAttendance.SemesterNo = Convert.ToInt32(semesterNo);

                                objAttendance.CourseNo = Convert.ToInt32(course[0]);

                                string cCode = "";
                                if (ViewState["ccode"].ToString() == string.Empty)
                                {
                                    cCode = (objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Convert.ToInt32(course[0])));
                                }
                                else
                                {
                                    cCode = ccode.ToString();
                                }

                                objAttendance.CCode = cCode;
                                objAttendance.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
                                objAttendance.Swap_status = 1;
                            }
                            else
                            {
                                int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip)));
                                objAttendance.SchemeNo = Convert.ToInt32(SchemeNo);

                                int semesterNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SEMESTERNO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip)));
                                objAttendance.SemesterNo = Convert.ToInt32(semesterNo);

                                objAttendance.CourseNo = Convert.ToInt32(lblCourse.ToolTip);

                                string cCode = (objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip)));
                                objAttendance.CCode = cCode;

                                objAttendance.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);

                                objAttendance.Swap_status = 0;
                            }

                            objAttendance.Th_Pr = Convert.ToInt32(ddlAttFor.SelectedValue);
                            objAttendance.SubId = Convert.ToInt32(ddlSubjectType.SelectedValue);
                            objAttendance.AttDate = Convert.ToDateTime(txtAttendanceDate.Text);

                            objAttendance.Period = Convert.ToInt32(chkPeriod.Items[i].Value);
                            objAttendance.Hours = Convert.ToInt32(chkPeriod.Items[i].Value);
                            objAttendance.ClassType = Convert.ToInt32(ddlClassType.SelectedValue);
                            objAttendance.CurDate = DateTime.Now;
                            //objAttendance.Topic = Convert.ToString(txtTopic.Text);
                            //int tp_no = Convert.ToInt32(ddlTopics.SelectedValue);
                            //string topics = objCommon.LookUp("ACD_TEACHINGPLAN", "TOPIC_COVERED", "TP_NO=" + tp_no);
                            //objAttendance.Topic = ddlTopics.SelectedItem.Text;
                            objAttendance.Topic = objCommon.LookUp("ACD_TEACHINGPLAN", "TOPIC_COVERED", "TP_NO=" + ddlTopics.SelectedValue);
                            objAttendance.CollegeCode = Session["colcode"].ToString();
                            objAttendance.StudIds = string.Empty;
                            objAttendance.AttStatus = string.Empty;
                            objAttendance.Extra_Curr_Status = string.Empty;
                            int Batch = 0;
                            Batch = Convert.ToInt32(ViewState["batch"]);

                            //ADDED BY SUMIT ON 06022020
                            string sStudent = string.Empty;
                            if (lvStudents != null)
                            {
                                foreach (ListViewDataItem dti in lvStudents.Items)
                                {
                                    CheckBox chkSel = dti.FindControl("chkRow") as CheckBox;
                                    CheckBox chkSell = dti.FindControl("chkExtraCrclr") as CheckBox;
                                    if (chkSel.Checked)
                                    {
                                        if (sStudent.Equals(string.Empty))
                                            sStudent = chkSel.ToolTip;
                                        else
                                            sStudent = sStudent + "," + chkSel.ToolTip;
                                    }
                                    if (chkSell.Checked)
                                    {
                                        if (sStudent.Equals(string.Empty))
                                            sStudent = chkSel.ToolTip;
                                        else
                                            sStudent = sStudent + "," + chkSel.ToolTip;
                                    }
                                }
                                if (sStudent.Equals(string.Empty))
                                {
                                    //objCommon.DisplayMessage(this.updpnl, "Please select at least one student", this);
                                    //return;
                                }
                            }



                            foreach (ListViewDataItem item in lvStudents.Items)
                            {
                                string idno = (item.FindControl("hidStudentId") as HiddenField).Value;
                                string batch = (item.FindControl("hidBatchId") as HiddenField).Value;

                                if (objAttendance.StudIds.Length > 0) objAttendance.StudIds += ",";
                                objAttendance.StudIds += idno;

                                if (objAttendance.AttStatus.Length > 0) objAttendance.AttStatus += ",";
                                if ((item.FindControl("chkRow") as CheckBox).Checked)
                                {
                                    objAttendance.AttStatus += "1";
                                }
                                else
                                {
                                    objAttendance.AttStatus += "0";
                                }

                                CheckBox ckhExtraCurP = item.FindControl("chkExtraCrclr") as CheckBox;
                                if (objAttendance.Extra_Curr_Status.Length > 0) objAttendance.Extra_Curr_Status += ",";
                                //Added By Pritish on 14/05/2019
                                if (ckhExtraCurP.Checked && (item.FindControl("chkRow") as CheckBox).Checked == false)
                                {
                                    objAttendance.Extra_Curr_Status += "1";
                                }
                                else
                                {
                                    objAttendance.Extra_Curr_Status += "0";
                                }

                            }

                            int dateCount = 0;
                            if (ddlClassType.SelectedValue == "1" || ddlClassType.SelectedValue == "3")
                            {
                                //  dateCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "count(*)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "and ua_no =" + (ddlTeacher.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PERIOD=" + chkPeriod.Items[i].Value)); 
                                if (ViewState["ccode"].ToString() == string.Empty)
                                {
                                    dateCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "count(*)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "and ua_no =" + (ddlTeacher.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PERIOD=" + chkPeriod.Items[i].Value));
                                }
                                else
                                {
                                    dateCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "count(*)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND th_pr=" + Convert.ToInt32(ddlAttFor.SelectedValue) + "and ua_no =" + (ddlTeacher.SelectedValue) + " AND CCODE='" + ccode.ToString() + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PERIOD=" + chkPeriod.Items[i].Value));
                                }
                            }
                            else if (ddlClassType.SelectedValue == "2")
                            {
                                // dateCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "count(*)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "and TH_PR=" + Convert.ToInt32(ddlAttFor.SelectedValue) + " and ua_no =" + (ddlTeacher.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PERIOD=" + chkPeriod.Items[i].Value));
                                if (ViewState["ccode"].ToString() == string.Empty)
                                {
                                    dateCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "count(*)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "and TH_PR=" + Convert.ToInt32(ddlAttFor.SelectedValue) + " and ua_no =" + (ddlTeacher.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PERIOD=" + chkPeriod.Items[i].Value));
                                }
                                else
                                {
                                    dateCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "count(*)", "ATT_DATE=convert(datetime,'" + txtAttendanceDate.Text + "',103)  AND CLASS_TYPE=" + Convert.ToInt32(ddlClassType.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "and TH_PR=" + Convert.ToInt32(ddlAttFor.SelectedValue) + " and ua_no =" + (ddlTeacher.SelectedValue) + " AND CCODE='" + ccode.ToString() + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PERIOD=" + chkPeriod.Items[i].Value));
                                }
                            }

                            objAttendance.Additional_Slot = Pr_Slots;
                            objAttendance.Attendance_Remark = txtRemark.Text;
                            //DataSet dsDuplicatecheck = objAttController.GetAttendenceByDate_Edit(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), txtAttendanceDate.Text, Convert.ToInt32(ddlClassType.SelectedValue), Convert.ToInt32(chkPeriod.SelectedValue), Convert.ToInt16(0), ccode.ToString());

                            //if (dsDuplicatecheck.Tables[0].Rows.Count > 0)
                            //{
                            //PASS PARAMETER TOPIC COVERED NO. ON DATED 17-02-2020
                            //CustomStatus cs = (CustomStatus)objAttController.AddAttendance(objAttendance, Batch)
                            CustomStatus cs = (CustomStatus)objAttController.AddAttendance(objAttendance, Batch, Convert.ToInt32(ddlTopics.SelectedValue));
                            CheckPresenty();
                            if (cs.Equals(CustomStatus.DuplicateRecord))
                            {
                                objCommon.DisplayMessage(this.updpnl, "Attendance Updated Successfully!!", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updpnl, "Attendance Saved Successfully!!", this.Page);
                            }
                            //}
                            //else
                            //{
                            //    objCommon.DisplayMessage(this.updpnl, "Selected Date and slot Attendance Already Entered for Regular/Extra Class !!", this.Page);
                            //}

                        }
                    }
                    ddlTopics.SelectedIndex = 0;
                    ddlClassType.SelectedIndex = 0;
                    ddlAttby.SelectedIndex = 0;
                    txtAttendanceDate.Text = string.Empty;
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                    chkSwap.Checked = false;
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    lvStudents.Visible = false;
                    txtAbsent.Text = string.Empty;
                    txtPresent.Text = string.Empty;
                    txtTotStud.Text = string.Empty;
                    trAttby.Visible = false;
                    trCourse.Visible = false;
                    trSwap.Visible = false;
                    txtRemark.Text = string.Empty;
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnl, "Please Select Only One Period.", this.Page);
                }
            }

            else
            {
                objCommon.DisplayMessage(this.updpnl, "Please Select Period.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceByFaculty.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void ddlClassType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] ccode1 = lblCourse.Text.Split(':');
        string ccode = ccode1[0].Trim();

        //txtTopic.Text = objCommon.LookUp("ACD_ATTENDANCE", "TOPIC_COVERED", "CLASS_TYPE=" + ddlClassType.SelectedValue + " and UA_NO=" + Session["userno"].ToString() + " and CCODE='" + ccode.ToString() + "'");

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = "0";
        txtPresent.Text = "0";
        txtAbsent.Text = "0";
        btnSubmit.Enabled = false;
        ddlTopics.SelectedIndex = 0;
        int Scheme = 0;
        if (lblCourse.ToolTip != "0")
        {
            Scheme = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + lblCourse.ToolTip));
        }

        if (ddlClassType.SelectedIndex > 0)
        {
            DataSet dsHoliday = objCommon.FillDropDown("PAYROLL_HOLIDAYS", "DT", "HOLIDAYNAME", "HNO>0 AND ISNULL(RESTRICT_STATUS,'N') = 'N'", "DT");
            if (string.IsNullOrEmpty(txtAttendanceDate.Text.Trim()))
            {
                objCommon.DisplayMessage(this.updpnl, "Please Select Attendance Date!", this.Page);
                ddlClassType.SelectedIndex = 0;
                return;
            }
            DateTime attdate = Convert.ToDateTime(txtAttendanceDate.Text.Trim());
            if (attdate > DateTime.Now)
            {
                objCommon.DisplayMessage(this.updpnl, "Selected Date is Greater than Todays date!", this.Page);
                ddlClassType.SelectedIndex = 0;
                return;
            }

            if (ddlClassType.SelectedValue == "1")
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                trAttby.Visible = false;
                trCourse.Visible = false;
                trSwap.Visible = false;
                ddlAttby.SelectedIndex = 0;
                ddlCourse.SelectedIndex = 0;
                chkSwap.Checked = false;
                ddlTopics.SelectedIndex = 0;
                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    DateTime dt;
                    for (int cnt = 0; cnt < dsHoliday.Tables[0].Rows.Count; cnt++)
                    {
                        dt = Convert.ToDateTime(dsHoliday.Tables[0].Rows[cnt]["DT"]);
                        if (attdate.ToShortDateString() == dt.ToShortDateString())
                        {
                            objCommon.DisplayMessage(this.updpnl, "Selected Date is a Holiday! (" + dsHoliday.Tables[0].Rows[cnt]["HOLIDAYNAME"] + ")", this.Page);
                            ddlClassType.SelectedIndex = 0;
                            txtAttendanceDate.Text = string.Empty;
                            return;
                        }
                    }
                }
                int slot = 0;
                int day = 0;

                if (attdate.DayOfWeek.ToString() == "Monday")
                {
                    if (Session["usertype"].ToString() == "1")
                    {
                        //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY1,0) AS DAY1", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + ddlTeacher.SelectedValue + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY1,0) AS DAY1", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY1,0) AS DAY1", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }

                    }
                    else if (Session["usertype"].ToString() == "3")
                    {
                        // day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY1,0) AS DAY1", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + Session["userno"] + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY1,0) AS DAY1", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY1,0) AS DAY1", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }

                    }
                    if (day == 1)
                        slot = 1;
                    else
                    {
                        slot = 0;
                    }
                }
                else if (attdate.DayOfWeek.ToString() == "Tuesday")
                {
                    if (Session["usertype"].ToString() == "1")
                    {
                        //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY2,0) AS DAY2", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + ddlTeacher.SelectedValue + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY2,0) AS DAY2", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY2,0) AS DAY2", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                    }
                    else if (Session["usertype"].ToString() == "3")
                    {
                        //  day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY2,0) AS DAY2", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + Session["userno"] + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY2,0) AS DAY2", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY2,0) AS DAY2", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }

                    }

                    if (day == 1)
                        slot = 2;
                    else
                        slot = 0;
                }
                else if (attdate.DayOfWeek.ToString() == "Wednesday")
                {
                    if (Session["usertype"].ToString() == "1")
                    {
                        // day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY3,0) AS DAY3", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + ddlTeacher.SelectedValue + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY3,0) AS DAY3", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY3,0) AS DAY3", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                    }
                    else if (Session["usertype"].ToString() == "3")
                    {
                        // day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY3,0) AS DAY3", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + Session["userno"] + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY3,0) AS DAY3", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY3,0) AS DAY3", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }

                    }
                    if (day == 1)
                        slot = 3;
                    else
                        slot = 0;
                }
                else if (attdate.DayOfWeek.ToString() == "Thursday")
                {
                    if (Session["usertype"].ToString() == "1")
                    {
                        // day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + ddlTeacher.SelectedValue + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + ddlTeacher.SelectedValue + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }

                    }
                    else if (Session["usertype"].ToString() == "3")
                    {
                        //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + Session["userno"] + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + Session["userno"] + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY4,0) AS DAY4", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                    }
                    if (day == 1)
                        slot = 4;
                    else
                        slot = 0;
                }
                else if (attdate.DayOfWeek.ToString() == "Friday")
                {
                    if (Session["usertype"].ToString() == "1")
                    {
                        //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY5,0) AS DAY5", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + ddlTeacher.SelectedValue + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY5,0) AS DAY5", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY5,0) AS DAY5", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                    }
                    else if (Session["usertype"].ToString() == "3")
                    {
                        //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY5,0) AS DAY5", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + Session["userno"] + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY5,0) AS DAY5", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY5,0) AS DAY5", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                    }

                    if (day == 1)
                        slot = 5;
                    else
                        slot = 0;
                }
                else if (attdate.DayOfWeek.ToString() == "Saturday")
                {
                    if (Session["usertype"].ToString() == "1")
                    {
                        // day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY6,0) AS DAY6", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND A.VALUE =" + ddlTeacher.SelectedValue + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY6,0) AS DAY6", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY6,0) AS DAY6", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + ddlTeacher.SelectedValue + " OR ADTEACHER=" + ddlTeacher.SelectedValue + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                    }
                    else if (Session["usertype"].ToString() == "3")
                    {
                        //day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER CROSS APPLY DBO.Split(ADTEACHER,',') A", "ISNULL(DAY6,0) AS DAY6", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND A.VALUE =" + Session["userno"] + " AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        if (Scheme == 0)
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY6,0) AS DAY6", "SESSIONNO=" + ddlSession.SelectedValue + " AND CCODE='" + ccode.ToString() + "' AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                        else
                        {
                            day = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "ISNULL(DAY6,0) AS DAY6", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND (UA_NO =" + Session["userno"] + " OR ADTEACHER=" + Session["userno"] + ") AND TH_PR =" + Convert.ToInt32(ddlAttFor.SelectedValue)));
                        }
                    }
                    if (day == 1)
                        slot = 6;
                    else
                        slot = 0;
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnl, "Selected date is Sunday. You cannot enter attendance for Regular class.", this.Page);
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    return;
                }
                if (slot == 0)
                {
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                    objCommon.DisplayMessage(this.updpnl, "You Have No Period allotted on " + attdate.DayOfWeek.ToString(), this.Page);
                    ddlClassType.SelectedIndex = 0;
                    txtAttendanceDate.Text = string.Empty;
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    return;
                }
                if (Session["usertype"].ToString() == "1")//ADMIN
                {
                    //ds1 = objAttController.GetPeriod(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), slot, Convert.ToInt32(ddlSection.SelectedValue), Scheme);
                    if (Scheme == 0)
                    {
                        ds1 = objAttController.GetPeriod(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), slot, Convert.ToInt32(ddlSection.SelectedValue), Scheme, ccode.ToString());
                    }
                    else
                    {
                        ds1 = objAttController.GetPeriod(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), slot, Convert.ToInt32(ddlSection.SelectedValue), Scheme, ccode.ToString());
                    }
                }
                else if (Session["usertype"].ToString() == "3")//TEACHER
                {
                    // ds1 = objAttController.GetPeriod(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), slot, Convert.ToInt32(ddlSection.SelectedValue), Scheme);
                    if (Scheme == 0)
                    {
                        ds1 = objAttController.GetPeriod(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), slot, Convert.ToInt32(ddlSection.SelectedValue), Scheme, ccode.ToString());
                    }
                    else
                    {
                        ds1 = objAttController.GetPeriod(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), slot, Convert.ToInt32(ddlSection.SelectedValue), Scheme, ccode.ToString());
                    }
                }
                ///////// START ADDED BY SARANG 28/02/2020 /////////////////////
                //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                //{
                //    if (ds1.Tables[0].Rows.ToString() != string.Empty)
                //    {
                //        DateTime Timeto = Convert.ToDateTime(ds1.Tables[0].Rows[i]["TIMETO"].ToString());
                //        string SlotName = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                //        int Back_Dates_Allow = Convert.ToInt32(objCommon.LookUp("REFF", "NOS_BK_ATTENDS_ALLOW", ""));
                //        //string Back_Dates_Allow = objCommon.LookUp("REFF", "NOS_BK_ATTENDS_ALLOW", "0=0");
                //        TimeSpan sp = DateTime.Now.TimeOfDay;
                //        int hour = sp.Hours;
                //        int minute = sp.Minutes;

                //        int s = Timeto.Hour;
                //        int d = Timeto.Minute;
                //        DateTime dt1 = Convert.ToDateTime(+s + ":" + d);
                //        DateTime dt = Convert.ToDateTime(+hour + ":" + minute);
                //        string Time = dt.ToString("hh:mm:ss tt");
                //        string sr = dt.ToString("dd/MM/yyyy");
                //        string Time_s = dt1.ToString("hh:mm:ss tt");
                //        string Date = txtAttendanceDate.Text;
                //        string Date_Time = (sr + " " + Time);
                //        string t = (Date + " " + Time_s);
                //        DateTime r = Convert.ToDateTime(t);
                //        DateTime Final_Time = Convert.ToDateTime(Date_Time);
                //        TimeSpan difference = new TimeSpan(Math.Abs(Final_Time.Ticks - r.Ticks));
                //        //TimeSpan diff = TimeSpan.FromHours(Convert.ToDouble(hour + minute));
                //        if (Back_Dates_Allow < difference.TotalHours)
                //        {
                //            if (Back_Dates_Allow.ToString() != "0" || Back_Dates_Allow.ToString() == string.Empty)
                //            {
                //                objCommon.DisplayMessage(this.updpnl, "Attendance More Than " + Back_Dates_Allow + " Hour(s) Is Not Allowed. !", this.Page);
                //                //ddlClassType.SelectedIndex = 0;
                //                //txtAttendanceDate.Text = string.Empty;
                //                ds1.Tables[0].Rows[i].Delete();
                //                ds1.Tables[0].Rows[i].AcceptChanges();
                //                i--;
                //            }
                //        }
                //    }
                //}
                ///////// END ADDED BY SARANG 28/02/2020 /////////////////////
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string value = ds1.Tables[0].Rows[i]["SLOTNO"].ToString().Trim();
                        string item = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString().Trim();
                        //chkPeriod.Items.Add(item);
                        //chkPeriod.DataTextField = item;
                        //chkPeriod.DataValueField = value;
                        chkPeriod.Items.Add(new ListItem(item, value));
                        dvPeriod.Visible = true;
                    }
                    //chkPeriod.SelectedIndex = 0;
                }
                else
                {
                    //chkPeriod.Items.Clear();
                    //chkPeriod.Items.Add(new ListItem("Please Select", "0"));
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                }
            }
            else if (ddlClassType.SelectedValue == "2")
            {
                int SchemeNo = 0;
                if (lblCourse.ToolTip != "0")
                {
                    SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + lblCourse.ToolTip));
                }

                if (Session["usertype"].ToString() == "1")//ADMIN
                {
                    // ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    if (Scheme == 0)
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }
                    else
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }
                }
                else if (Session["usertype"].ToString() == "3")//TEACHER
                {
                    // ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    if (Scheme == 0)
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }
                    else
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }

                }
                ///////// START ADDED BY SARANG 28/02/2020 /////////////////////
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows.ToString() != string.Empty)
                    {
                        DateTime Timeto = Convert.ToDateTime(ds1.Tables[0].Rows[i]["TIMETO"].ToString());
                        string SlotName = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                        int Back_Dates_Allow = Convert.ToInt32(objCommon.LookUp("REFF", "NOS_BK_ATTENDS_ALLOW", ""));
                        //string Back_Dates_Allow = objCommon.LookUp("REFF", "NOS_BK_ATTENDS_ALLOW", "0=0");
                        TimeSpan sp = DateTime.Now.TimeOfDay;
                        int hour = sp.Hours;
                        int minute = sp.Minutes;

                        int s = Timeto.Hour;
                        int d = Timeto.Minute;
                        DateTime dt1 = Convert.ToDateTime(+s + ":" + d);
                        DateTime dt = Convert.ToDateTime(+hour + ":" + minute);
                        string Time = dt.ToString("hh:mm:ss tt");
                        string sr = dt.ToString("dd/MM/yyyy");
                        string Time_s = dt1.ToString("hh:mm:ss tt");
                        string Date = txtAttendanceDate.Text;
                        string Date_Time = (sr + " " + Time);
                        string t = (Date + " " + Time_s);
                        DateTime r = Convert.ToDateTime(t);
                        DateTime Final_Time = Convert.ToDateTime(Date_Time);
                        TimeSpan difference = new TimeSpan(Math.Abs(Final_Time.Ticks - r.Ticks));
                        //TimeSpan diff = TimeSpan.FromHours(Convert.ToDouble(hour + minute));
                        if (Back_Dates_Allow < difference.TotalHours)
                        {
                            if (Back_Dates_Allow.ToString() != "0" || Back_Dates_Allow.ToString() == string.Empty)
                            {
                                objCommon.DisplayMessage(this.updpnl, "Attendance More Than " + Back_Dates_Allow + " Hour(s) Is Not Allowed. !", this.Page);
                                //ddlClassType.SelectedIndex = 0;
                                //txtAttendanceDate.Text = string.Empty;
                                ds1.Tables[0].Rows[i].Delete();
                                ds1.Tables[0].Rows[i].AcceptChanges();
                                i--;
                            }
                        }
                    }
                }
                ///////// END ADDED BY SARANG 28/02/2020 /////////////////////
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string value = ds1.Tables[0].Rows[i]["SLOTNO"].ToString().Trim();
                        string item = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString().Trim();
                        //chkPeriod.Items.Add(item);
                        //chkPeriod.DataTextField = item;
                        //chkPeriod.DataValueField = value;
                        chkPeriod.Items.Add(new ListItem(item, value));
                        dvPeriod.Visible = true;
                    }
                    //chkPeriod.SelectedIndex = 0;
                }
                else
                {
                    //chkPeriod.Items.Clear();
                    //chkPeriod.Items.Add(new ListItem("Please Select", "0"));
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                }
                //chkPeriod.Items.Clear();
                //for (int i = 1; i <= 10; i++)
                //{

                //    chkPeriod.Items.Add(new ListItem(i.ToString(), i.ToString()));
                //}
                trAttby.Visible = false;
                trCourse.Visible = false;
                trSwap.Visible = false;
                ddlAttby.SelectedIndex = 0;
                ddlCourse.SelectedIndex = 0;
                chkSwap.Checked = false;
            }
            else if (ddlClassType.SelectedValue == "3")
            {
                int SchemeNo = 0;
                if (Scheme != 0)
                {
                    SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + lblCourse.ToolTip));
                }

                if (Session["usertype"].ToString() == "1")//ADMIN
                {
                    //ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    if (Scheme == 0)
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }
                    else
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }
                }
                else if (Session["usertype"].ToString() == "3")//TEACHER
                {
                    // ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    if (Scheme == 0)
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }
                    else
                    {
                        ds1 = objAttController.GetPeriodExtra(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), SchemeNo);
                    }
                }
                ///////// START ADDED BY SARANG 28/02/2020 /////////////////////
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows.ToString() != string.Empty)
                    {
                        DateTime Timeto = Convert.ToDateTime(ds1.Tables[0].Rows[i]["TIMETO"].ToString());
                        string SlotName = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString();
                        int Back_Dates_Allow = Convert.ToInt32(objCommon.LookUp("REFF", "NOS_BK_ATTENDS_ALLOW", ""));
                        //string Back_Dates_Allow = objCommon.LookUp("REFF", "NOS_BK_ATTENDS_ALLOW", "0=0");
                        TimeSpan sp = DateTime.Now.TimeOfDay;
                        int hour = sp.Hours;
                        int minute = sp.Minutes;

                        int s = Timeto.Hour;
                        int d = Timeto.Minute;
                        DateTime dt1 = Convert.ToDateTime(+s + ":" + d);
                        DateTime dt = Convert.ToDateTime(+hour + ":" + minute);
                        string Time = dt.ToString("hh:mm:ss tt");
                        string sr = dt.ToString("dd/MM/yyyy");
                        string Time_s = dt1.ToString("hh:mm:ss tt");
                        string Date = txtAttendanceDate.Text;
                        string Date_Time = (sr + " " + Time);
                        string t = (Date + " " + Time_s);
                        DateTime r = Convert.ToDateTime(t);
                        DateTime Final_Time = Convert.ToDateTime(Date_Time);
                        TimeSpan difference = new TimeSpan(Math.Abs(Final_Time.Ticks - r.Ticks));
                        //TimeSpan diff = TimeSpan.FromHours(Convert.ToDouble(hour + minute));
                        if (Back_Dates_Allow < difference.TotalHours)
                        {
                            if (Back_Dates_Allow.ToString() != "0" || Back_Dates_Allow.ToString() == string.Empty)
                            {
                                objCommon.DisplayMessage(this.updpnl, "Attendance More Than " + Back_Dates_Allow + " Hour(s) Is Not Allowed. !", this.Page);
                                //ddlClassType.SelectedIndex = 0;
                                //txtAttendanceDate.Text = string.Empty;
                                ds1.Tables[0].Rows[i].Delete();
                                ds1.Tables[0].Rows[i].AcceptChanges();
                                i--;
                            }
                        }
                    }
                }
                ///////// END ADDED BY SARANG 28/02/2020 /////////////////////
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string value = ds1.Tables[0].Rows[i]["SLOTNO"].ToString().Trim();
                        string item = ds1.Tables[0].Rows[i]["SLOTNAME"].ToString().Trim();
                        chkPeriod.Items.Add(new ListItem(item, value));
                        dvPeriod.Visible = true;
                    }
                }
                else
                {
                    chkPeriod.Items.Clear();
                    dvPeriod.Visible = false;
                }
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                txtTotStud.Text = "0";
                txtPresent.Text = "0";
                txtAbsent.Text = "0";
                chkPeriod.Items.Clear();
                dvPeriod.Visible = false;
                // ddl1.selectedIndex = selVal;

                btnSubmit.Enabled = false;
                trAttby.Visible = false;
                trCourse.Visible = false;
                trSwap.Visible = false;
                lvStudents.Visible = false;
            }
        }
        else
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            chkPeriod.Items.Clear();
            dvPeriod.Visible = false;
            txtTotStud.Text = "0";
            txtPresent.Text = "0";
            txtAbsent.Text = "0";
            btnSubmit.Enabled = false;
            trAttby.Visible = false;
            trCourse.Visible = false;
            trSwap.Visible = false;
            lvStudents.Visible = false;
        }
    }

    private void doselection()
    {
        ddlClassType.SelectedValue = "0";
    }
    protected void BindCourseName(object sender, string secNo, string batch, int fromDashboardStatus) // fromDashboardStatus 1 means --> from dashboard page , 0 means --> from attendance by faculty link
    {
        try
        {
            string assTimetable = string.Empty;

            //Show Next Panel
            txtAttendanceDate.Text = string.Empty;
            txtAbsent.Text = string.Empty;
            txtPresent.Text = string.Empty;
            //txtTopic.Text = string.Empty;
            txtTotStud.Text = string.Empty;
            //chkBatch.Items.Clear();
            ddlClassType.SelectedIndex = 0;
            //ddlPeriod.SelectedIndex = 0;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            //pnlSelection.Visible = false;
            dvSelection.Visible = false;
            //pnlNext.Visible = true;
            dvNext.Visible = true;
            ViewState["ccode"] = "";
            if (fromDashboardStatus == 0)
            {
                LinkButton lnk = sender as LinkButton;
                if (!lnk.CommandArgument.Equals(string.Empty))
                {
                    lblCourse.Text = lnk.Text;
                    lblCourse.ToolTip = lnk.CommandArgument;

                    if (Convert.ToInt32(lblCourse.ToolTip) == 0)
                    {
                        string[] ccode = lblCourse.Text.Split(':');
                        ViewState["ccode"] = ccode[0];
                    }

                    lblSection.Text = lnk.ToolTip;
                    ViewState["batch"] = lnk.CommandName;
                    #region commented part
                    //objCommon.FillDropDownList(ddlTopics, "ACD_TEACHINGPLAN", "DISTINCT TP_NO", "(CONVERT(NVARCHAR(MAX),TOPIC_COVERED)+' --> (UNIT NO.-'+CONVERT(NVARCHAR(50),UNIT_NO)+' AND TOPIC NO.- '+CONVERT(NVARCHAR(50),LECTURE_NO)+')') AS TOPIC_COVERED", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + lnk.ToolTip + " AND (BATCHNO=" + lnk.CommandName + " OR " + lnk.CommandName + "=0)", "TOPIC_COVERED");
                    //objCommon.FillDropDownList(ddlTopics, "ACD_TEACHINGPLAN", "DISTINCT UNIT_NO", "TOPIC_COVERED", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + lnk.ToolTip + " AND (BATCHNO=" + lnk.CommandName + " OR " + lnk.CommandName + "=0)", "TOPIC_COVERED");
                    //ddlTopics.SelectedIndex = 0;

                    //Comment by Mahesh on Dated 17-02-2020

                    //////DataSet ds = objCommon.FillDropDown("ACD_TEACHINGPLAN", "DISTINCT TOPIC_COVERED", "", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + lnk.ToolTip + " AND (BATCHNO=" + lnk.CommandName + " OR " + lnk.CommandName + "=0)", "TOPIC_COVERED");

                    //////ddlTopics.Items.Clear();
                    //////ddlTopics.Items.Add("Please Select");
                    ////////ddlTopics.SelectedItem.Value = "0";

                    //////if (ds.Tables[0].Rows.Count > 0)
                    //////{
                    //////    ddlTopics.DataSource = ds;
                    //////    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //////    {
                    //////        ddlTopics.Items.Add(new ListItem(ds.Tables[0].Rows[i][0].ToString(), (i + 1).ToString()));
                    //////        //ddlTopics.DataBind();
                    //////        ddlTopics.SelectedIndex = 0;
                    //////    }
                    //////}
                    ////////ddlTopics.Items.Clear();
                    ////////ddlTopics.Items.Clear();
                    ////////ddlTopics.Items.Insert(0, new ListItem("Please Select", "0"));
                    ////////for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
                    ////////{
                    ////////    ddlTopics.Items.Add(new ListItem(ds.Tables[0].Rows[i][i].ToString(), i.ToString()));
                    ////////}
                    #endregion
                    objCommon.FillDropDownList(ddlTopics, "ACD_TEACHINGPLAN", "TP_NO", "TOPIC_COVERED +' ; Proposed Date-'+  CONVERT(NVARCHAR,[DATE],107) +' ; Lecture No- '+ Cast(LECTURE_NO as nvarchar(50)) as TOPIC_COVERED", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + lnk.ToolTip + " AND (BATCHNO=" + lnk.CommandName + " OR " + lnk.CommandName + "=0)", "TP_NO");
                }
            }
            else
            {
                string coursenoArgument = sender.ToString();
                if (!coursenoArgument.Equals(string.Empty))
                {
                    //lblCourse.Text = lnk.Text;
                    lblCourse.ToolTip = coursenoArgument.ToString();

                    if (Convert.ToInt32(lblCourse.ToolTip) == 0)
                    {
                        string[] ccode = lblCourse.Text.Split(':');
                        ViewState["ccode"] = ccode[0];
                    }

                    lblSection.Text = secNo;
                    ViewState["batch"] = batch;
                    #region commented part
                    //objCommon.FillDropDownList(ddlTopics, "ACD_TEACHINGPLAN", "DISTINCT TP_NO", "(CONVERT(NVARCHAR(MAX),TOPIC_COVERED)+' --> (UNIT NO.-'+CONVERT(NVARCHAR(50),UNIT_NO)+' AND TOPIC NO.- '+CONVERT(NVARCHAR(50),LECTURE_NO)+')') AS TOPIC_COVERED", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + lnk.ToolTip + " AND (BATCHNO=" + lnk.CommandName + " OR " + lnk.CommandName + "=0)", "TOPIC_COVERED");
                    //objCommon.FillDropDownList(ddlTopics, "ACD_TEACHINGPLAN", "DISTINCT UNIT_NO", "TOPIC_COVERED", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + lnk.ToolTip + " AND (BATCHNO=" + lnk.CommandName + " OR " + lnk.CommandName + "=0)", "TOPIC_COVERED");
                    //ddlTopics.SelectedIndex = 0;

                    //Comment by Mahesh on Dated 17-02-2020

                    //////DataSet ds = objCommon.FillDropDown("ACD_TEACHINGPLAN", "DISTINCT TOPIC_COVERED", "", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + lnk.ToolTip + " AND (BATCHNO=" + lnk.CommandName + " OR " + lnk.CommandName + "=0)", "TOPIC_COVERED");

                    //////ddlTopics.Items.Clear();
                    //////ddlTopics.Items.Add("Please Select");
                    ////////ddlTopics.SelectedItem.Value = "0";

                    //////if (ds.Tables[0].Rows.Count > 0)
                    //////{
                    //////    ddlTopics.DataSource = ds;
                    //////    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //////    {
                    //////        ddlTopics.Items.Add(new ListItem(ds.Tables[0].Rows[i][0].ToString(), (i + 1).ToString()));
                    //////        //ddlTopics.DataBind();
                    //////        ddlTopics.SelectedIndex = 0;
                    //////    }
                    //////}
                    ////////ddlTopics.Items.Clear();
                    ////////ddlTopics.Items.Clear();
                    ////////ddlTopics.Items.Insert(0, new ListItem("Please Select", "0"));
                    ////////for (int i = 1; i <= ds.Tables[0].Rows.Count; i++)
                    ////////{
                    ////////    ddlTopics.Items.Add(new ListItem(ds.Tables[0].Rows[i][i].ToString(), i.ToString()));
                    ////////}
                    #endregion
                    objCommon.FillDropDownList(ddlTopics, "ACD_TEACHINGPLAN", "TP_NO", "TOPIC_COVERED +' ; Proposed Date-'+  CONVERT(NVARCHAR,[DATE],107) +' ;Lecture No- '+ Cast(LECTURE_NO as nvarchar(50)) as TOPIC_COVERED", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND COURSENO=" + lblCourse.ToolTip + " AND SECTIONNO=" + secNo + " AND (BATCHNO=" + batch + " OR " + batch + "=0)", "TP_NO");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceByFaculty.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        BindCourseName(sender, "0", "0", 0);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        txtAttendanceDate.Text = string.Empty;
        txtAbsent.Text = string.Empty;
        txtPresent.Text = string.Empty;
        //txtTopic.Text = string.Empty;
        txtTotStud.Text = string.Empty;
        lblCourse.Text = string.Empty;
        lblCourse.ToolTip = string.Empty;
        chkPeriod.Items.Clear();
        dvPeriod.Visible = false;
        trAttby.Visible = false;
        trCourse.Visible = false;
        trSwap.Visible = false;
        //chkBatch.Items.Clear();
        ddlClassType.SelectedIndex = 0;
        ddlAttby.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        chkSwap.Checked = false;
        //ddlPeriod.SelectedIndex = 0;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
        //pnlNext.Visible = false;
        dvNext.Visible = false;
        ddlAttby.SelectedIndex = 0;
        //pnlSelection.Visible = true;
        dvSelection.Visible = true;
        pmsg.Visible = false;
        btnSubmit.Enabled = true;
        lblMsgw.Text = string.Empty;

        //Response.Redirect(Request.Url.ToString());

    }

    private void filterFaculty(int slot)
    {
        string date = txtAttendanceDate.Text;
        DateTime dt = new DateTime();
        dt = Convert.ToDateTime(date);
        DataSet ds = null;

        if (Session["usertype"].ToString() == "1")
            ds = objAttController.GetFacultiesBySlot(Convert.ToInt32(ddlSession.SelectedValue), dt.ToString("yyyy/MM/dd"), slot, Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue));
        else if (Session["usertype"].ToString() == "3")
            ds = objAttController.GetFacultiesBySlot(Convert.ToInt32(ddlSession.SelectedValue), dt.ToString("yyyy/MM/dd"), slot, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue));

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlAttby.Items.Clear();
            ddlAttby.Items.Add(new ListItem("Please Select", "0"));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string value = ds.Tables[0].Rows[i]["UA_NO"].ToString();
                string text = ds.Tables[0].Rows[i]["UA_FULLNAME"].ToString();
                ddlAttby.Items.Add(new ListItem(text, value));
            }
        }
        else
        {
            ddlAttby.Items.Clear();
            ddlAttby.Items.Add(new ListItem("Please Select", "0"));
            if (slot != 0)
                objCommon.DisplayMessage(this.updpnl, "No Faculty found for the selected slot and selected date.", this.Page);
            return;
        }
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chk = e.Item.FindControl("chkRow") as CheckBox;
        CheckBox extracurrchk = e.Item.FindControl("chkExtraCrclr") as CheckBox;

        //if (chk.ToolTip == "True")
        //{
        //    chk.Checked = true;
        //}
        //else
        //{
        //    chk.Checked = false;
        //}

        //if (extracurrchk.ToolTip == "True")
        //{
        //    extracurrchk.Checked = true;
        //}
        //else
        //{
        //    extracurrchk.Checked = false;
        //}
        //Added new on 2020 Jun 04 by Amit K

        HiddenField hidDiscplnrInfo = e.Item.FindControl("hidDiscplnrInfo") as HiddenField;  
        HiddenField hidDiscFromDt = e.Item.FindControl("hidDiscFromDt") as HiddenField;  
        HiddenField hidDiscToDt = e.Item.FindControl("hidDiscToDt") as HiddenField;              
        DateTime attdate = Convert.ToDateTime(txtAttendanceDate.Text);
        if (hidDiscplnrInfo.Value != "")
        {
            if ((attdate.CompareTo(Convert.ToDateTime(hidDiscFromDt.Value)) == 0 || attdate.CompareTo(Convert.ToDateTime(hidDiscFromDt.Value)) > 0) &&
                (attdate.CompareTo(Convert.ToDateTime(hidDiscToDt.Value)) == 0 || attdate.CompareTo(Convert.ToDateTime(hidDiscToDt.Value)) < 0))
            {
                chk.Enabled = false;
                extracurrchk.Enabled = false;
            }
            else
            {
                if (chk.ToolTip == "True")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }

                if (extracurrchk.ToolTip == "True")
                {
                    extracurrchk.Checked = true;
                }
                else
                {
                    extracurrchk.Checked = false;
                }
            }
        }
        else
        {

            //Added new on 2020 Jun 04 by Amit K end
	 
            if (chk.ToolTip == "True")
            {
                chk.Checked = true;
            }
            else
            {
                chk.Checked = false;
            }

            if (extracurrchk.ToolTip == "True")
            {
                extracurrchk.Checked = true;
            }
            else
            {
                extracurrchk.Checked = false;
            }
        } // added by amit on 2020 jun 15

        if ((e.Item.FindControl("chkRow") as CheckBox).Checked == true)
            txtPresent.Text = (Convert.ToInt32(txtPresent.Text) + 1).ToString();

    }

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.ShowStudent();
        if (ddlClassType.SelectedValue == "2" & Convert.ToInt32(chkPeriod.SelectedValue) > 0)
        //if (ddlClassType.SelectedValue == "2" & Convert.ToInt32(chkPeriod.SelectedValue) > 0)
        {
            trAttby.Visible = true;
            trCourse.Visible = true;
            trSwap.Visible = true;
        }
        else
        {
            trAttby.Visible = false;
            trCourse.Visible = false;
            trSwap.Visible = false;
        }
    }

    private void ShowAttendance()
    {
        try
        {
            string[] ccode1 = lblCourse.Text.Split(':');
            string ccode = ccode1[0].Trim();

            DataSet ds = null;
            int ua_no = 0;
            objAttendance.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            if (Session["usertype"].ToString() == "1")//ADMIN
            {
                attDone = objAttController.GetCourseWiseAttendanceDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ViewState["batch"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue));
            }
            else if (Session["usertype"].ToString() == "3")//TEACHER
            {
                attDone = objAttController.GetCourseWiseAttendanceDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["batch"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue));
            }


            if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                ds = objAttController.GetAttendenceByDate_Report(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ua_no), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), (lblCourse.ToolTip), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
            else
                ds = objAttController.GetAttendenceByDate_Report(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ua_no), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), ("0"), Convert.ToInt32(ViewState["batch"]), ccode.ToString());


            int i = 0;
            if (attDone.Tables[0].Rows.Count > 0)
            {
                //pnlNext.Visible = false;
                dvNext.Visible = false;
                //pnlDates.Visible = true;
                dvDates.Visible = true;
                lvAttDone.DataSource = attDone;
                lvAttDone.DataBind();

                foreach (ListViewDataItem item in lvAttDone.Items)
                {
                    Label lblAttDate = item.FindControl("lblAttDate") as Label;
                    Label lblClassType = item.FindControl("lblClassType") as Label;

                    DateTime day = Convert.ToDateTime(attDone.Tables[0].Rows[i]["ATT_DATE"]);

                    lblAttDate.ToolTip = day.DayOfWeek.ToString();

                    int classType = Convert.ToInt32(attDone.Tables[0].Rows[i]["CLASS_TYPE"]);
                    if (classType == 1)
                        lblClassType.Text = "Regular Class";
                    else if (classType == 3)
                        lblClassType.Text = "Alternate Class";
                    else if (classType == 2)
                        lblClassType.Text = "Extra Class";
                    i++;
                }
            }
            else
            {
                dvDates.Visible = false;
                dvNext.Visible = true;
                objCommon.DisplayMessage(this.updpnl, "No Details found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceByFaculty.ShowAttendance --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        this.ShowAttendance();
    }

    protected void btnBackToAtt_Click(object sender, EventArgs e)
    {
        lvAttDone.DataSource = null;
        lvAttDone.DataBind();
        //pnlDates.Visible = false;
        dvDates.Visible = false;
        //pnlRegister.Visible = false;
        dvRegister.Visible = false;
        //pnlNext.Visible = true;
        dvNext.Visible = true;
        txtFromDate.Text = string.Empty;
        txtTodate.Text = string.Empty;
    }

    protected void ddlAttFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAttFor.SelectedIndex > 0)
            {
                if (ddlSubjectType.SelectedIndex > 0)
                {
                    lblMsg.Text = string.Empty;
                    if (ddlSubjectType.SelectedIndex == 0)
                    {
                        lvCourse.DataSource = null;
                        lvCourse.DataBind();
                        return;
                    }

                    MarksEntryController objMarksEntry = new MarksEntryController();

                    if (Session["usertype"].ToString() == "3")//teacher
                    {
                        ds = objMarksEntry.GetCourseForTeacherForAttendance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
                    }
                    else if (Session["usertype"].ToString() == "1")//admin
                    {
                        ds = objMarksEntry.GetCourseForTeacherForAttendance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
                    }
                    //else if (Session["usertype"].ToString() == "1" && ddlSubjectType.SelectedValue == "3")//admin
                    //{
                    //    ds = objMarksEntry.GetCourseForTeacherForAttendance(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
                    //}
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        lvCourse.DataSource = ds;
                        lvCourse.DataBind();
                    }
                    else
                    {
                        lvCourse.DataSource = null;
                        lvCourse.DataBind();
                        objCommon.DisplayMessage(this.updpnl, "No Course is allotted.", this.Page);
                    }
                }
                else
                {
                    ddlAttFor.SelectedIndex = 0;
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
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
                objUCommon.ShowError(Page, "Academic_MarkEntry.btnShowCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlAttby_SelectedIndexChanged(object sender, EventArgs e)
    {
        MarksEntryController objMarksEntry = new MarksEntryController();

        ds = objMarksEntry.GetCourseForTeacherForAttendancealt(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlAttby.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string value = ds.Tables[0].Rows[i]["COURSENO"].ToString() + "," + ds.Tables[0].Rows[i]["SECTIONNO"].ToString();
                string text = ds.Tables[0].Rows[i]["COURSENAME"].ToString() + " (" + ds.Tables[0].Rows[i]["SECTION"].ToString() + ")";
                //ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.Items.Add(new ListItem(text, value));
                //string value = ds.Tables[0].Rows[i]["COURSENO"].ToString();
                //string text = ds.Tables[0].Rows[i]["COURSENAME"].ToString() + " (" + ds.Tables[0].Rows[i]["SECTION"].ToString() + ")";

                //ddlCourse.Items.Add(new ListItem(text, value));
            }
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            objCommon.DisplayMessage(this.updpnl, "No Course Found for Selected Faculty.", this.Page);
            return;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlClassType.SelectedValue == "1" && ddlTopics.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updpnl, "Please Select Topic Covered its mandetory for " + ddlClassType.SelectedItem.Text + " !!", this.Page);
            return;
        }
        else if (ddlClassType.SelectedValue == "3" && ddlTopics.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updpnl, "Please Select Topic Covered its mandetory for " + ddlClassType.SelectedItem.Text + " !!", this.Page);
            return;
        }
        else
        {
            pnlTimeTable.Visible = true;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;
            //ddlTopics.SelectedIndex = 0;
            int select = 0;
            for (int i = 0; i < chkPeriod.Items.Count; i++)
            {
                if (chkPeriod.Items[i].Selected == true)
                {
                    select++;
                }
            }
            if (select > 0)
            {
                StudentAttendanceController objStudAttCon = new StudentAttendanceController();
                StudentAttendance objAtt = new StudentAttendance();

                string[] ccode1 = lblCourse.Text.Split(':');
                string ccode = ccode1[0].Trim();

                objAtt.SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME='" + ddlSession.SelectedItem + "'"));
                objAtt.FacultyNo = Convert.ToInt32(Session["userno"].ToString());
                objAtt.AttDate = Convert.ToDateTime(txtAttendanceDate.Text);

                objAtt.CourseNo = Convert.ToInt32(lblCourse.ToolTip); //Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "COURSENO", "CCODE='" + ccode + "'"));
                //objAtt.BatchNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "BATCHNO", "CCODE='" + ccode + "'"));
                objAtt.BatchNo = ViewState["batch"] == string.Empty ? 0 : Convert.ToInt32(ViewState["batch"]);//0;
                //objAtt.Period = (objCommon.LookUp("ACD_ATTENDANCE", "PERIOD", "CCODE='" + ccode + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "PERIOD", "CCODE='" + ccode + "'")));
                objAtt.Period = Convert.ToInt32(chkPeriod.SelectedValue);
                objAtt.ClassType = Convert.ToInt32(ddlClassType.SelectedValue);//(objCommon.LookUp("ACD_ATTENDANCE", "CLASS_TYPE", "CCODE='" + ccode + "'") == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "CLASS_TYPE", "CCODE='" + ccode + "'")));
                objAtt.Sectionno = Convert.ToInt32(ddlSection.SelectedValue); //Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "SECTIONNO", "CCODE='" + ccode + "'"));

                ////string d = (txtAttendanceDate.Text).ToString("");
                ////objAtt.AttDate = 

                //int slot = Convert.ToInt32(chkPeriod.SelectedValue);

                //txtTopic.Text = objCommon.LookUp("ACD_TEACHINGPLAN", "TOPIC_COVERED", "UA_NO=" + Session["userno"].ToString() +" and SLOT="+slot+" and CCODE='" + ccode.ToString() + "'");

                int count = objStudAttCon.GetAttendanceRepeat(objAtt);

                if (count != 0)
                {
                    this.ShowStudent();
                    this.CheckPresenty();
                }
                else
                {
                    this.ShowStudent();
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updpnl, "Please Select Period.", this.Page);
            }
        }
    }

    protected void chkPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {

        string[] ccode1 = lblCourse.Text.Split(':');
        string ccode = ccode1[0].Trim();
        string date = txtAttendanceDate.Text;
        DateTime dt = new DateTime();
        dt = Convert.ToDateTime(date);
        DataSet ds = null;
        //int slot;

        int count = 0;
        int check = 0;
        for (int i = 0; i < chkPeriod.Items.Count; i++)
        {
            if (chkPeriod.Items[i].Selected == true)
            {
                count++;
                check = Convert.ToInt32(chkPeriod.Items[i].Value);
            }
        }

        if (ddlClassType.SelectedValue == "3") filterFaculty(Convert.ToInt32(check));

        ds = objAttController.GetFacultiesBySlot(Convert.ToInt32(ddlSession.SelectedValue), dt.ToString("yyyy/MM/dd"), check, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlAttFor.SelectedValue));

        //if (ddlClassType.SelectedValue == "3" & count > 0) // AGAINST 2
        if (ddlClassType.SelectedValue == "3" && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) // AGAINST 2      Convert.ToInt32(chkPeriod.Items[i].Value)
        {
            trAttby.Visible = true;
            trCourse.Visible = true;
            trSwap.Visible = true;
        }
        else
        {
            trAttby.Visible = false;
            trCourse.Visible = false;
            trSwap.Visible = false;
        }

        ////string[] ccode1 = lblCourse.Text.Split(':');
        ////string ccode = ccode1[0].Trim();

        ////int count = 0;
        ////int check = 0;
        ////for (int i = 0; i < chkPeriod.Items.Count; i++)
        ////{
        ////    if (chkPeriod.Items[i].Selected == true)
        ////    {
        ////        count++;
        ////        check = Convert.ToInt32(chkPeriod.Items[i].Value);
        ////    }
        ////}

        ////if (ddlClassType.SelectedValue == "3")
        ////    filterFaculty(Convert.ToInt32(check));

        ////if (ddlClassType.SelectedValue == "3" & count > 0) // AGAINST 2
        ////{
        ////    trAttby.Visible = true;
        ////    trCourse.Visible = true;
        ////    trSwap.Visible = true;
        ////}
        ////else
        ////{
        ////    trAttby.Visible = false;
        ////    trCourse.Visible = false;
        ////    trSwap.Visible = false;
        ////}

        ////int slot = chkPeriod.SelectedValue == string.Empty ? 0 : Convert.ToInt32(chkPeriod.SelectedValue);
        //txtTopic.Text = objCommon.LookUp("ACD_TEACHINGPLAN", "TOPIC_COVERED", "UA_NO=" + Session["userno"].ToString() + " and SLOT=" + slot + " and CCODE='" + ccode.ToString() + "'");
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Session["usertype"].ToString() == "1")//ADMIN
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SUBJECTTYPE S ON (CT.SUBID = S.SUBID)", "DISTINCT CT.SUBID", "S.SUBNAME", "CT.UA_NO=" + Convert.ToInt32(ddlTeacher.SelectedValue) + " AND CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SECTIONNO = " + ddlSection.SelectedValue, "CT.SUBID");
        }
        else if (Session["usertype"].ToString() == "3")//TEACHER
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SUBJECTTYPE S ON (CT.SUBID = S.SUBID)", "DISTINCT CT.SUBID", "S.SUBNAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SECTIONNO = " + ddlSection.SelectedValue, "CT.SUBID");
        }
        ddlSubjectType.SelectedIndex = 0;
        ddlAttFor.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        if (fromDashboardStatus == 1)
        {
            ddlSubjectType.SelectedValue = subid.ToString();
            ddlSubjectType_SelectedIndexChanged(sender, e);
        }
    }

    protected void btnDayWise_Click(object sender, EventArgs e)
    {
        //string[] fromDate = txtTodate.Text.Split('/');
        string[] fromDate = txtFromDate.Text.Split('/');
        string[] toDate = txtTodate.Text.Split('/');
        DateTime fromdate = Convert.ToDateTime(fromDate[0] + "/" + fromDate[1] + "/" + fromDate[2]);
        DateTime todate = Convert.ToDateTime(toDate[0] + "/" + toDate[1] + "/" + toDate[2]);

        //DateTime fromdate = Convert.ToDateTime(txtFromDate.Text);
        //DateTime todate = Convert.ToDateTime(txtTodate.Text);

        if (fromdate > todate)
        {
            objCommon.DisplayMessage(this.updpnl, "From Date always be less than To date. Please Enter proper Date range.", this.Page);
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
        }
        else
        {
            ShowDayWiseReport();
        }
    }

    private void ShowDayWiseReport()
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ccode = string.Empty;

            if (Convert.ToInt32(lblCourse.ToolTip) != 0)
            {
                ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + lblCourse.ToolTip);
            }
            else
            {
                ccode = ccode.ToString();
            }

            int SchemeNo = 0;
            if (Convert.ToInt32(lblCourse.ToolTip) != 0)
            {
                SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + lblCourse.ToolTip));
            }

            string degree = string.Empty;
            string branch = string.Empty;
            if (Convert.ToInt32(lblCourse.ToolTip) != 0)
            {
                degree = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "SCHEMENO=" + SchemeNo);
                branch = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO)BRANCH", "SCHEMENO=" + SchemeNo);
            }

            string ContentType = string.Empty;
            DataSet ds = null;
            StudentAttendanceController objAC = new StudentAttendanceController();
            if (Session["usertype"].ToString() == "3")//teacher
            {
                //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                {
                    ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                }
                else
                {
                    ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(0), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                }
            }
            else if (Session["usertype"].ToString() == "1")//admin
            {
                //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]));
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                {
                    ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                }
                else
                {
                    ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(0), Convert.ToInt32(ddlTeacher.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), ccode.ToString());
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.RemoveAt(7);

                //ds.Tables[0].Columns.Remove("ROLLNO");
                //ds.Tables[0].Columns.Remove("IDNO");

                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + branch.Replace(" ", "_") + "_" + ccode + "_" + txtFromDate.Text.Trim() + "_" + txtTodate.Text.Trim() + ".xls";
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
                objCommon.DisplayMessage(this.updpnl, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.ShowAllRooms() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //pnlNext.Visible = false;
        dvNext.Visible = false;
        //pnlRegister.Visible = true;
        dvRegister.Visible = true;
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "3")
        {
            objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "SR.SESSIONNO=" + ddlSession.SelectedValue + " AND UA_NO=" + Session["userno"], "SC.SECTIONNO");
        }
        else if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "SR.SESSIONNO=" + ddlSession.SelectedValue + "", "SC.SECTIONNO");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReportinFormate("AttendanceDetails", "rptAtttendanceDetails.rpt");
    }

    private void ShowReportinFormate(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Session["usertype"].ToString() == "3")
            {
                string[] ccode1 = lblCourse.Text.Split(':');
                string ccode = ccode1[0].Trim();
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@UserName=" + Session["username"].ToString() + ",@P_TH_PR=" + ddlAttFor.SelectedValue + ",@P_BATCHNO=" + Convert.ToInt32(ViewState["batch"]) + ",@P_CCODE=" + ccode.ToString() + "";
            }
            else
            {
                string[] ccode1 = lblCourse.Text.Split(':');
                string ccode = ccode1[0].Trim();
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UANO=" + Convert.ToInt32(ddlTeacher.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@UserName=" + Session["username"].ToString() + ",@P_TH_PR=" + ddlAttFor.SelectedValue + ",@P_BATCHNO=" + Convert.ToInt32(ViewState["batch"]) + ",@P_CCODE=" + ccode.ToString() + "";
            }

            divScript.InnerHtml += " <script type='text/javascript' language='javascript'> try{";
            divScript.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
            //divScript.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } ";
            //divScript.InnerHtml += " window.close();";
            //divScript.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlTeacher.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlAttFor.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }

    private void ShowReportinFormate_new(string reportTitle, string rptFileName)
    {
        try
        {
            int TH_PR = 0;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            GridView GVDayWiseAtt = new GridView();
            string ccode = string.Empty;
            if (Convert.ToInt32(lblCourse.ToolTip) != 0)
            {
                ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + lblCourse.ToolTip);
            }
            else
            {
                ccode = ccode.ToString();
            }
            int SchemeNo = 0;
            if (Convert.ToInt32(lblCourse.ToolTip) != 0)
            {
                SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + lblCourse.ToolTip));
            }
            string degree = string.Empty;
            string branch = string.Empty;
            if (Convert.ToInt32(lblCourse.ToolTip) != 0)
            {
                degree = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('DEGREENAME',DEGREENO)DEGREE", "SCHEMENO=" + SchemeNo);
                branch = objCommon.LookUp("ACD_SCHEME", "DBO.FN_DESC('BRANCHLNAME',BRANCHNO)BRANCH", "SCHEMENO=" + SchemeNo);
            }
            if (Session["usertype"].ToString() == "3")//teacher
            {
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                {
                    //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), (ViewState["ccode"].ToString()).Trim());
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + SchemeNo + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_TH_PR=" + Convert.ToInt32(ddlAttFor.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@P_FROMDATE=" + Convert.ToString(txtFromDate.Text) + ",@P_TODATE=" + Convert.ToString(txtTodate.Text) + ",@P_BATCHNO=" + Convert.ToInt32(ViewState["batch"]) + ",@P_CCODE='" + ccode.ToString() + "'";
                }
                else
                {
                    //ds = objAC.GetDayWiseData(Convert.ToInt32(ddlSession.SelectedValue), SchemeNo, Convert.ToInt32(0), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ddlAttFor.SelectedValue), Convert.ToInt32(ViewState["batch"]), (ViewState["ccode"].ToString()).Trim());
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + SchemeNo + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_TH_PR=" + Convert.ToInt32(ddlAttFor.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(0) + ",@P_FROMDATE=" + Convert.ToString(txtFromDate.Text) + ",@P_TODATE=" + Convert.ToString(txtTodate.Text) + ",@P_BATCHNO=" + Convert.ToInt32(ViewState["batch"]) + ",@P_CCODE='" + ccode.ToString() + "'";
                }
            }
            else if (Session["usertype"].ToString() == "1")//admin
            {
                if (Convert.ToInt32(lblCourse.ToolTip) != 0)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + SchemeNo + ",@P_UA_NO=" + Convert.ToInt32(ddlTeacher.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_TH_PR=" + Convert.ToInt32(ddlAttFor.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@P_FROMDATE=" + Convert.ToString(txtFromDate.Text) + ",@P_TODATE=" + Convert.ToString(txtTodate.Text) + ",@P_BATCHNO=" + Convert.ToInt32(ViewState["batch"]) + ",@P_CCODE='" + ccode.ToString() + "'";
                }
                else
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + SchemeNo + ",@P_UA_NO=" + Convert.ToInt32(ddlTeacher.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_TH_PR=" + Convert.ToInt32(ddlAttFor.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(0) + ",@P_FROMDATE=" + Convert.ToString(txtFromDate.Text) + ",@P_TODATE=" + Convert.ToString(txtTodate.Text) + ",@P_BATCHNO=" + Convert.ToInt32(ViewState["batch"]) + ",@P_CCODE=" + ccode.ToString() + "";
                }
            }

            divScript.InnerHtml += " <script type='text/javascript' language='javascript'> try{";
            divScript.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
            //divScript.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } ";
            //divScript.InnerHtml += " window.close();";
            //divScript.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e) //Added By Hemanth G
    {
        StudentAttendanceController objAC = new StudentAttendanceController();
        Button btnDel = sender as Button;

        int attno = int.Parse(btnDel.CommandArgument);

        int output = objAC.deleteAttendanceRecord(attno, Convert.ToInt32(Session["userno"]));
        if (output == 1)
        {
            objCommon.DisplayMessage(this.updpnl, "Attendance Entry Deleted Successfully!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(this.updpnl, "Error Occured !!", this.Page);
        }
        this.ShowAttendance();

    }
    //ADDED BY DILEEP 20032020
    private void CheckHolidayDate()
    {
        DateTime holidaydate = Convert.ToDateTime(txtAttendanceDate.Text);
        txtAttendanceDate.Text = holidaydate.ToString("yyyy/MM/dd");
        int Holiday_Event = Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(1)", "( '" + (txtAttendanceDate.Text) + "' BETWEEN (ACADEMIC_HOLIDAY_STDATE) AND (ACADEMIC_HOLIDAY_ENDDATE) OR ACADEMIC_HOLIDAY_STDATE='" + (txtAttendanceDate.Text) + "') AND IS_HOLIDAY_EVENT  IN(1,3) AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
        if (Holiday_Event > 0)
        {
            objCommon.DisplayMessage(updpnl, "Selected Date is found as Holiday or Suspend Class Date", this.Page);
            txtAttendanceDate.Text = string.Empty;
        }
        else
        {
            txtAttendanceDate.Text = holidaydate.ToString("dd/MM/yyyy");
        }
    }

    // this is for checking holidays date written by Dileep on 20-03-2020
    protected void txtAttendanceDate_TextChanged(object sender, EventArgs e)
    {
        CheckHolidayDate();
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        chkPeriod.Items.Clear();
        dvPeriod.Visible = false;
        txtTotStud.Text = "0";
        txtPresent.Text = "0";
        txtAbsent.Text = "0";
        btnSubmit.Enabled = false;
        trAttby.Visible = false;
        trCourse.Visible = false;
        trSwap.Visible = false;
        lvStudents.Visible = false;
        ddlClassType.SelectedIndex = 0;
    }
}
