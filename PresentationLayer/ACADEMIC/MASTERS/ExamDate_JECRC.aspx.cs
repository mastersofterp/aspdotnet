
using System;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using ClosedXML.Excel;
using System.IO;



public partial class ACADEMIC_MASTERS_ExamDate : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    AcademinDashboardController objADEController = new AcademinDashboardController();
    static int count = 0;
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.PopulateDropDown();
                divbatch.Visible = false;
                divMsg.InnerHtml = string.Empty;
            }
        }
    }

    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();

        string sessionno = string.Empty;

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
        ViewState["Session"] = sessionno;
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamDate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamDate.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "COLLEGE_IDS,DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["College_ids"] = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString();
                ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ViewState["Branchno"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
                ViewState["Semesterno"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
            }
            if (Session["usertype"].ToString().Equals("1"))
            {
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            }
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));

            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            DataSet ds1 = objADEController.Get_College_Session(4, Session["college_nos"].ToString());
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddlSession1.DataSource = ds1;
                ddlSession1.DataValueField = ds1.Tables[0].Columns[0].ToString();
                ddlSession1.DataTextField = ds1.Tables[0].Columns[1].ToString();
                ddlSession1.DataBind();

                ddlSession2.DataSource = ds1;
                ddlSession2.DataValueField = ds1.Tables[0].Columns[0].ToString();
                ddlSession2.DataTextField = ds1.Tables[0].Columns[1].ToString();
                ddlSession2.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Schemewise Table Table Code

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            Exam objExam = new Exam();
            objExam.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

            objExam.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objExam.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objExam.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objExam.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            //int sessionid = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONID,0)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
            int sessionid = Convert.ToInt32(ddlSession.SelectedValue); //JECRC NEW REQUIREMENT
            int subexamno = Convert.ToInt32(ddlSubExamName.SelectedValue);
            objExam.CollegeCode = Convert.ToString(Session["colcode"]);
            objExam.Exdtno = Convert.ToInt32(ViewState["exdtno"]);
            objExam.Exam_TT_Type = Convert.ToInt32(ddlExamName.SelectedValue);
            objExam.collegeid = Convert.ToInt32(ViewState["college_id"]);
            int OrgID = Convert.ToInt32(Session["OrgId"]);
            if (checkBlack() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Please Select Date And Slot", this.Page);
                checkbox();
                return;
            }
            if (checkDate() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Invalid Date Format/Selected Date is previous Date than Today", this.Page);
                return;
            }
            //if (Validate() == false)
            //{
            //    objCommon.DisplayMessage(updExamdate, "Same Date And Slot Already Exists", this.Page);
            //    return;
            //}

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {
                    objExam.Status = 1;
                    objExam.Courseno = Convert.ToInt32((dataitem.FindControl("chkAccept") as CheckBox).ToolTip);
                    objExam.Slot = Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue);
                    int Modeexam = Convert.ToInt32((dataitem.FindControl("ddlmodeexam") as DropDownList).SelectedValue);
                    //string ccode = (dataitem.FindControl("lblCourseno") as Label).ToolTip;
                    string ccode = (dataitem.FindControl("lblCourseno") as Label).Text;  //JECRC NEW REQUIREMENT
                    objExam.Examdate = Convert.ToDateTime((dataitem.FindControl("txtExamDate") as TextBox).Text);

                    CustomStatus cs = (CustomStatus)objExamController.AddExamDay1(objExam, OrgID, Modeexam, ccode, sessionid, subexamno);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(updExamdate, "Exam Day(s) Update Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updExamdate, "Error Insert/Update", this.Page);
                    }
                    DropDownList ddlSlot = dataitem.FindControl("ddlSlot") as DropDownList;
                    DropDownList ddlmodeexam = dataitem.FindControl("ddlmodeexam") as DropDownList;
                    TextBox txtDate = dataitem.FindControl("txtExamDate") as TextBox;
                    ddlSlot.Enabled = true;
                    txtDate.Enabled = true;
                    GetCourses();
                    btnViewLogin.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        ddlCollege.SelectedValue = "0";
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype.Items.Clear();
        ddlSubjecttype.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSubExamName.Items.Clear();
        ddlSubExamName.Items.Add(new ListItem("Please Select", "0"));
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
    }

    private void Clear()
    {
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlScheme.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
        ddlExamName.SelectedIndex = -1;
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype.Items.Clear();
        ddlSubjecttype.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSubExamName.Items.Clear();
        ddlSubExamName.Items.Add(new ListItem("Please Select", "0"));

        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    if (ddlCollege.SelectedIndex > 0)
                    {
                        ddlDegree.Items.Clear();
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENAME");
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "IS_ACTIVE=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                        ddlSession.Focus();
                    }
                    else
                    {
                        ddlDegree.Items.Clear();
                        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                        objCommon.DisplayMessage("Please select College/School Name.", this.Page);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ViewState["branchno"]) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]), "S.SCHEMENAME DESC");
            ddlScheme.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.ddlBranch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"], "A.LONGNAME");
        ddlBranch.Focus();

    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        ddlSubExamName.Items.Clear();
        ddlSubExamName.Items.Add(new ListItem("Please Select", "0"));

        objCommon.FillDropDownList(ddlSubjecttype, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "R.SUBID");

        //objCommon.FillDropDownList(ddlExamName, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "EXAMNAME");
        ////objCommon.FillDropDownList(ddlSection, "ACD_SECTION", " DISTINCT SECTIONNO", "SECTIONNAME", " SECTIONNO>0", "SECTIONNAME");
        //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SEC ON (SR.SECTIONNO=SEC.SECTIONNO)", " DISTINCT SR.SECTIONNO", "SEC.SECTIONNAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + ddlSemester.SelectedValue + "AND SR.SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(SR.CANCEL,0)=0", "SEC.SECTIONNAME");
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ddlSubExamName.Items.Clear();
        ddlSubExamName.Items.Add(new ListItem("Please Select", "0"));
        objCommon.FillDropDownList(ddlExamName, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "EXAMNAME");
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
        objCommon.FillDropDownList(ddlSubjecttype, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "R.SUBID");

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        GetCourses();
    }

    private void GetCourses()
    {
        //if (Convert.ToInt32(Session["OrgId"]) != 9)
        //{

        //    ScriptManager.RegisterStartupScript(this.updExamdate, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(6)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(6)').hide();});", true);

        //}

        int schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int collegeid = Convert.ToInt32(ViewState["college_id"]);
        int examno = Convert.ToInt32(ddlExamName.SelectedValue);
        int subexamno = Convert.ToInt32(ddlSubExamName.SelectedValue);
        string proc_name1 = "PKG_GET_COURSE_ALL_JECRC";
        string para_name1 = "@P_SCHEMENO,@P_SEMESTERNO,@P_SECTIONNO,@P_SESSIONNO,@P_COLLEGE_ID,@P_SUBID,@P_EXAMNO,@P_SUBEXAMNO";
        string call_values1 = "" + schemeno + "," + semesterno + "," + sectionno + "," + sessionno + "," + collegeid + "," + Convert.ToInt32(ddlSubjecttype.SelectedValue) + "," + examno + "," + subexamno + "";
        DataSet ds = objCommon.DynamicSPCall_Select(proc_name1, para_name1, call_values1);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvCourse.DataSource = ds;
            lvCourse.DataBind();
            lvCourse.Visible = true;
            btnSubmit.Visible = true;
            foreach (ListViewDataItem lvHead in lvCourse.Items)
            {
                DropDownList ddlSlot = lvHead.FindControl("ddlSlot") as DropDownList;
                DropDownList ddlmodeexam = lvHead.FindControl("ddlmodeexam") as DropDownList;
                HiddenField hdn_fld = lvHead.FindControl("hdf_slotno") as HiddenField;
                HiddenField hdf_modeexam = lvHead.FindControl("hdf_modeexam") as HiddenField;
                CheckBox chkHead = lvHead.FindControl("chkAccept") as CheckBox;
                TextBox txtDate = lvHead.FindControl("txtExamDate") as TextBox;

                objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT WITH (NOLOCK)", "SLOTNO", "SLOTNAME", "SLOTNO>0", "SLOTNO");
                objCommon.FillDropDownList(ddlmodeexam, "ACD_EXAMINATION_MODE WITH (NOLOCK)", "ModeEXAMno", "ModeEXAMNAME", "ModeEXAMNO>0", "ModeEXAMNO");
                ddlSlot.SelectedValue = hdn_fld.Value;
                ddlmodeexam.SelectedValue = hdf_modeexam.Value;

                if (chkHead.Checked == true)
                {
                    ddlSlot.Enabled = true;
                    txtDate.Enabled = true;
                    btnViewLogin.Visible = true;
                    count++;
                }
                else
                {
                    ddlSlot.Enabled = false;
                    txtDate.Enabled = false;
                    count--;
                }

                if (count == 0)
                {
                    btnViewLogin.Visible = false;
                }
            }
        }
        else
        {
            btnViewLogin.Visible = false;
            btnSubmit.Visible = false;
            lvCourse.DataSource = ds;
            lvCourse.DataBind();
            objCommon.DisplayMessage("Courses Not Found", this.Page);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID=" + ViewState["college_id"] + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "AND BRANCHNO=" + Convert.ToInt32(ViewState["branchno"])) == "0")
        //{
        //    objCommon.DisplayMessage(updExamdate, "Record not found", this);
        //    return;
        //}
        //else
        //{
        this.Show("TimeTable_Report", "rptTimeTable_JECRC.rpt");
        //}
    }

    private void Show(string reportTitle, string rptFileName)
    {
        try
        {
            string procedure = "PKG_ACAD_EXAM_DATE_GET_REPORT_JECRC";
            string parameter = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_SECTIONNO,@P_SCHEMENO,@P_EXAM_NO,@P_COLLEGE_ID,@P_SUBEXAMNO";
            string values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ViewState["degreeno"]) + "," + Convert.ToInt32(ViewState["branchno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlSection.SelectedValue) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlExamName.SelectedValue) + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32(ddlSubExamName.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(procedure, parameter, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
                        ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                     ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
                      ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) +
                       ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) +
                     ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) +
                     ",@P_EXAM_NO=" + Convert.ToInt32(ddlExamName.SelectedValue) +
                     ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) +
                     ",@P_SUBEXAMNO=" + Convert.ToInt32(ddlSubExamName.SelectedValue) +
                     ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");
                ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "No Time Table Created For the selection", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.Show() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ddlSubjecttype.Items.Clear();
        ddlSubjecttype.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSubExamName.Items.Clear();
        ddlSubExamName.Items.Add(new ListItem("Please Select", "0"));

        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        //objCommon.FillDropDownList(ddlSubjecttype, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "R.SUBID");
    }

    protected void ddlSubjecttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSubExamName.Items.Clear();
        ddlSubExamName.Items.Add(new ListItem("Please Select", "0"));

        //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        objCommon.FillDropDownList(ddlExamName, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND ED.ACTIVESTATUS=1 AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "EXAMNAME");

        objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SEC ON (SR.SECTIONNO=SEC.SECTIONNO)", " DISTINCT SR.SECTIONNO", "SEC.SECTIONNAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + ddlSemester.SelectedValue + "AND SR.SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(SR.CANCEL,0)=0", "SEC.SECTIONNAME");
    }

    protected void btndelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {
                    if (Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue) > 0 && Convert.ToString((dataitem.FindControl("txtExamDate") as TextBox).Text) != "")
                    {
                        (dataitem.FindControl("txtExamDate") as TextBox).Text = string.Empty;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btndelete_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnViewLogin_Click(object sender, EventArgs e)
    {
        try
        {
            ExamController objExamController = new ExamController();
            // CustomStatus cs = (CustomStatus)objExamController.GetViewOnStudentLock(Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSubjecttype.SelectedValue));
            CustomStatus cs = (CustomStatus)objExamController.GetViewOnStudentLock(Convert.ToInt32(ddlSubExamName.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSubjecttype.SelectedValue)); //SUBEXAMWISE LOCK ADDED BY INJAMAM ON 08-06-2023
            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(updExamdate, "Record Update Successfully!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnViewLogin_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ibtnEvalDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string ccode = null;
            int EXAM_TT_TYPE = 0, SUBEXAMNO = 0, SUBID = 0;
            DataSet ds = null;
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {

                    ImageButton ibtnEvalDelete = sender as ImageButton;
                    int IDNO = int.Parse(ibtnEvalDelete.CommandArgument);
                    //ds = objCommon.FillDropDown("ACD_EXAM_DATE", "CCODE,EXAM_TT_TYPE", "SUBEXAMNO,SUBID", "EXDTNO=" + IDNO + "", "");
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //    {
                    //        ccode = ds.Tables[0].Rows[i]["CCODE"].ToString();
                    //        EXAM_TT_TYPE = Convert.ToInt32(ds.Tables[0].Rows[i]["EXAM_TT_TYPE"]);
                    //        SUBEXAMNO = Convert.ToInt32(ds.Tables[0].Rows[i]["SUBEXAMNO"]);
                    //        SUBID = Convert.ToInt32(ds.Tables[0].Rows[i]["SUBID"]);
                    //    }
                    //}
                    //ds = objCommon.FillDropDown("ACD_EXAM_DATE", "EXDTNO", "", "CCODE='" + ccode + "' AND EXAM_TT_TYPE=" + EXAM_TT_TYPE + "AND SUBEXAMNO=" + SUBEXAMNO + "AND SUBID=" + SUBID + "", "");

                    ccode = objCommon.LookUp("ACD_COURSE", "DISTINCT CCODE", "COURSENO=" + IDNO);
                    EXAM_TT_TYPE = Convert.ToInt32(ddlExamName.SelectedValue);
                    SUBEXAMNO = Convert.ToInt32(ddlSubExamName.SelectedValue);
                    SUBID = Convert.ToInt32(ddlSubjecttype.SelectedValue);
                    ds = objCommon.FillDropDown("ACD_EXAM_DATE", "EXDTNO", "", "CCODE='" + ccode + "' AND EXAM_TT_TYPE=" + EXAM_TT_TYPE + "AND SUBEXAMNO=" + SUBEXAMNO + "AND SUBID=" + SUBID + "", "");

                    string allexdtno = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        allexdtno += ds.Tables[0].Rows[i]["EXDTNO"].ToString() + ",";
                    }
                    allexdtno = allexdtno.TrimEnd(',');

                    int retStatus = objExamController.DeleteTimeTable_JECRC(allexdtno);

                    if ((retStatus == 1) && (chkBox.Checked))
                    {
                        objCommon.DisplayMessage(this.updExamdate, "Time Table Cancelled Successfully", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updExamdate, "Error in Cancelling Time Table", this.Page);
                    }
                    GetCourses();
                    btnViewLogin.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updExamdate, "Select CheckBox to delete Time Table", this.Page);
                    btnViewLogin.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.ibtnEvalDelete_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void TimeTableExcel()
    {
        #region old
        //int sessionno = Convert.ToInt32(ddlSession.SelectedValue);


        //string proc_name = "PKG_ACAD_EXAM_DATE_GET_REPORT_EXCEL";
        //string para_name = "@P_SESSIONNO";
        //string call_values = "" + sessionno + "";
        //DataSet dsExcel = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);

        //GridView gv = new GridView();
        //if (dsExcel != null && dsExcel.Tables.Count > 0 && dsExcel.Tables[0].Rows.Count > 0)
        //{
        //    gv.DataSource = dsExcel;
        //    gv.DataBind();
        //    string attachment = "attachment ; filename=TimeTableExcelReport.xls";
        //    Response.ClearContent();
        //    Response.AddHeader("content-disposition", attachment);
        //    Response.ContentType = "application/ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    gv.RenderControl(htw);
        //    Response.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();
        //}
        //else
        //{
        //    gv.DataSource = null;
        //    gv.DataBind();
        //    GetCourses();
        //    objCommon.DisplayMessage("Time Table Record Found For Excel", this.Page);
        //}
        #endregion
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        string proc_name = "PKG_ACAD_EXAM_DATE_GET_REPORT_EXCEL";
        string para_name = "@P_SESSIONNO";
        string call_values = "" + sessionno + "";
        DataSet ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
        ds.Tables[0].TableName = "SectionWise Exam Time Table";
        ds.Tables[1].TableName = "DateWise Exam Time Table";

        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (DataTable dt in ds.Tables)
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        wb.Worksheets.Add(dt);
                    }
                }
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Exam_Time_Table.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        this.TimeTableExcel();
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        objCommon.FillDropDownList(ddlSubExamName, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.SUBEXAMNO", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = " + Convert.ToInt32(ddlExamName.SelectedValue) + "  AND ISNULL(EP.ACTIVESTATUS,0) = 1 AND SE.SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue) + "", "SE.SUBEXAMNO");

    }

    protected void ddlSubExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
    }

    private bool checkDate()
    {
        Boolean stat = true;
        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {

            CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

            if (chkBox.Checked)
            {
                try
                {
                    DateTime date1 = Convert.ToDateTime((dataitem.FindControl("txtExamDate") as TextBox).Text);
                    if (date1 < DateTime.Now)
                    {
                        stat = false;
                        checkbox();
                        return stat;
                    }
                }
                catch (Exception)
                {
                    stat = false;
                    return stat;
                }
            }
        }
        checkbox();
        return stat;
    }

    private bool checkBlack()
    {
        Boolean stat = true;
        foreach (ListViewDataItem dataitem in lvCourse.Items)
        {

            CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

            if (chkBox.Checked)
            {
                try
                {
                    string date = (dataitem.FindControl("txtExamDate") as TextBox).Text;
                    int value = Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue);
                    if (string.IsNullOrEmpty(date))
                    {
                        stat = false;
                        return stat;
                    }
                    if (value == 0)
                    {
                        stat = false;
                        return stat;
                    }
                }
                catch (Exception)
                {
                    stat = false;
                    return stat;
                }
            }
        }
        checkbox();
        return stat;
    }

    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList currentDropdown = (DropDownList)sender;
        ListViewItem currentItem = (ListViewItem)currentDropdown.NamingContainer;
        int currentIndex = currentItem.DisplayIndex;
        string selectedValue = currentDropdown.SelectedValue;
        string textdate = null;
        currentDropdown.Enabled = true;
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                TextBox txtDate = item.FindControl("txtExamDate") as TextBox;
                textdate = txtDate.Text;
                txtDate.Enabled = true;
            }

        }
        foreach (ListViewDataItem ROW in lvCourse.Items)
        {
            CheckBox chkBox = ROW.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                if (ROW.DisplayIndex != currentIndex)
                {
                    TextBox txtDate1 = ROW.FindControl("txtExamDate") as TextBox;
                    DropDownList dropslot1 = ROW.FindControl("ddlSlot") as DropDownList;
                    if (currentDropdown.Text == dropslot1.Text && textdate == txtDate1.Text)
                    {
                        objCommon.DisplayMessage(this.updExamdate, "Same Date & Same Slot Already Exists", this.Page);
                        currentDropdown.SelectedIndex = 0;
                        checkbox();
                        return;
                    }
                    txtDate1.Enabled = true;
                    dropslot1.Enabled = true;
                }
            }
        }
        checkbox();
    }

    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        TextBox currentdate = (TextBox)sender;
        ListViewItem currentItem = (ListViewItem)currentdate.NamingContainer;
        int currentIndex = currentItem.DisplayIndex;
        string selecteddate = currentdate.Text;
        string drpslot = null;
        currentdate.Enabled = true;
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                DropDownList dropslot = item.FindControl("ddlSlot") as DropDownList;
                drpslot = dropslot.Text;
                dropslot.Enabled = true;
            }

        }
        foreach (ListViewDataItem ROW in lvCourse.Items)
        {
            CheckBox chkBox = ROW.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                if (ROW.DisplayIndex != currentIndex)
                {
                    TextBox txtDate1 = ROW.FindControl("txtExamDate") as TextBox;
                    DropDownList dropslot1 = ROW.FindControl("ddlSlot") as DropDownList;
                    if (drpslot == dropslot1.Text && selecteddate == txtDate1.Text)
                    {
                        objCommon.DisplayMessage(this.updExamdate, "Same Date & Same Slot Already Exists", this.Page);
                        currentdate.Text = string.Empty;
                        checkbox();
                        return;
                    }
                    txtDate1.Enabled = true;
                    dropslot1.Enabled = true;
                }
            }
        }
        checkbox();
    }

    protected void checkbox()
    {
        foreach (ListViewDataItem ROW in lvCourse.Items)
        {
            CheckBox chkBox = ROW.FindControl("chkAccept") as CheckBox;
            TextBox txtDate1 = ROW.FindControl("txtExamDate") as TextBox;
            DropDownList dropslot1 = ROW.FindControl("ddlSlot") as DropDownList;
            if (chkBox.Checked)
            {
                txtDate1.Enabled = true;
                dropslot1.Enabled = true;
            }
            else
            {
                txtDate1.Enabled = false;
                dropslot1.Enabled = false;
            }
        }
    }

    private bool Validate()
    {
        bool stat = true;
        foreach (ListViewDataItem lvHead in lvCourse.Items)
        {
            CheckBox chkBox = lvHead.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                TextBox txtDate = lvHead.FindControl("txtExamDate") as TextBox;
                DropDownList dropslot = lvHead.FindControl("ddlSlot") as DropDownList;
                string ccode = (lvHead.FindControl("lblCourseno") as Label).ToolTip;
                foreach (ListViewDataItem row in lvCourse.Items)
                {
                    CheckBox chkBox1 = row.FindControl("chkAccept") as CheckBox;
                    if (chkBox1.Checked)
                    {
                        TextBox txtDate1 = row.FindControl("txtExamDate") as TextBox;
                        DropDownList dropslot1 = row.FindControl("ddlSlot") as DropDownList;
                        string ccode1 = (row.FindControl("lblCourseno") as Label).ToolTip;
                        if (txtDate.Text == txtDate1.Text && dropslot.SelectedValue == dropslot1.SelectedValue && ccode1 != ccode)
                        {
                            stat = false;
                            checkbox();
                            return stat;
                        }
                    }

                }

            }

        }
        return stat;
    }

    private bool ValidateConfirmation()
    {
        bool stat = true;
        foreach (ListViewDataItem lvHead in lvCourse.Items)
        {
            CheckBox chkBox = lvHead.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                TextBox txtDate = lvHead.FindControl("txtExamDate") as TextBox;
                DropDownList dropslot = lvHead.FindControl("ddlSlot") as DropDownList;
                string ccode = (lvHead.FindControl("lblCourseno") as Label).ToolTip;
                foreach (ListViewDataItem row in lvCourse.Items)
                {
                    CheckBox chkBox1 = row.FindControl("chkAccept") as CheckBox;
                    if (chkBox1.Checked)
                    {
                        TextBox txtDate1 = row.FindControl("txtExamDate") as TextBox;
                        DropDownList dropslot1 = row.FindControl("ddlSlot") as DropDownList;
                        string ccode1 = (row.FindControl("lblCourseno") as Label).ToolTip;
                        if (txtDate.Text == txtDate1.Text && dropslot.SelectedValue == dropslot1.SelectedValue && ccode1 != ccode)
                        {
                            stat = false;
                            checkbox();
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Reg", "submitConfirm();", true);
                            return stat;
                        }
                    }

                }

            }

        }
        return stat;
    }
    #endregion scheme base Time table code

    #region Global Course Time Table Tab
    protected void ddlSession1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpattern.Items.Clear();
        ddlpattern.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype1.Items.Clear();
        ddlSubjecttype1.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        ddlSubexamname1.Items.Clear();
        ddlSubexamname1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
        btnViewLogin1.Visible = false;
        if (ddlSession1.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSubjecttype1, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)  INNER JOIN ACD_SESSION_MASTER SM  ON (SM.SESSIONNO= R.SESSIONNO)", " DISTINCT R.SUBID", "S.SUBNAME", "sm.SESSIONID=" + Convert.ToInt32(ddlSession1.SelectedValue), "R.SUBID");
            objCommon.FillDropDownList(ddlpattern, "ACD_EXAM_PATTERN EP INNER JOIN ACD_SCHEME S ON (EP.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_EXAM_NAME EN ON (EP.PATTERNNO=EN.PATTERNNO AND ISNULL(EN.ACTIVESTATUS,0) = 1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT EP.PATTERNNO", "PATTERN_NAME", "EP.PATTERNNO > 0 AND ISNULL(EP.ACTIVESTATUS,0)=1 AND SESSIONID=" + Convert.ToInt32(ddlSession1.SelectedValue), "EP.PATTERNNO");
        }
        else
        {
            ddlSubjecttype1.Items.Clear();
            ddlSubjecttype1.Items.Add(new ListItem("Please Select", "0"));
            lvCourse1.Visible = false;
        }

    }

    protected void ddlSubjecttype1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        ddlSubexamname1.Items.Clear();
        ddlSubexamname1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
        btnViewLogin1.Visible = false;
        if (ddlSubjecttype1.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlExamName1, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND ISNULL(ACTIVESTATUS,0)=1 AND SUBID=" + Convert.ToInt32(ddlSubjecttype1.SelectedValue) + "", "EXAMNAME");

            objCommon.FillDropDownList(ddlExamName1, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND ISNULL(ACTIVESTATUS,0)=1 AND SUBID=" + Convert.ToInt32(ddlSubjecttype1.SelectedValue) + "AND S.PATTERNNO=" + Convert.ToInt32(ddlpattern.SelectedValue), "EXAMNAME");
        }

    }

    protected void ddlExamName1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubexamname1.Items.Clear();
        ddlSubexamname1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
        btnViewLogin1.Visible = false;
        objCommon.FillDropDownList(ddlSubexamname1, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.SUBEXAMNO", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = " + Convert.ToInt32(ddlExamName1.SelectedValue) + "  AND ISNULL(EP.ACTIVESTATUS,0) = 1 AND SE.SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjecttype1.SelectedValue) + "", "SE.SUBEXAMNO");
    }

    protected void btnShow1_Click(object sender, EventArgs e)
    {
        GetCourses1();
    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            Exam objExam = new Exam();
            objExam.CollegeCode = Convert.ToString(Session["colcode"]);
            objExam.Exdtno = Convert.ToInt32(ViewState["exdtno"]);
            objExam.Exam_TT_Type = Convert.ToInt32(ddlExamName1.SelectedValue);
            int OrgID = Convert.ToInt32(Session["OrgId"]);
            int subexamno = Convert.ToInt32(ddlSubexamname1.SelectedValue);
            if (checkBlack1() == false)
            {
                objCommon.DisplayMessage(this.updglobal, "Please Select Date And Slot", this.Page);
                checkbox1();
                return;
            }
            if (checkDate1() == false)
            {
                objCommon.DisplayMessage(this.updglobal, "Invalid Date Format/Selected Date is previous Date than Today", this.Page);
                return;
            }
            //if (Validate1() == false)
            //{
            //    objCommon.DisplayMessage(this.updglobal, "Same Date And Slot Already Exists", this.Page);
            //    return;
            //}
            foreach (ListViewDataItem dataitem in lvCourse1.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {
                    if (Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue) > 0 && Convert.ToString((dataitem.FindControl("txtExamDate") as TextBox).Text) != "")
                    {
                        objExam.Status = 1;
                        string ccode = (dataitem.FindControl("lblCourseno") as Label).ToolTip;
                        objExam.Slot = Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue);
                        int Modeexam = 0;// Convert.ToInt32((dataitem.FindControl("ddlmodeexam") as DropDownList).SelectedValue);
                        int sessionid = Convert.ToInt32(ddlSession1.SelectedValue);
                        objExam.Examdate = Convert.ToDateTime((dataitem.FindControl("txtExamDate") as TextBox).Text);
                        //return;   //Injamam Break point for testing
                        CustomStatus cs = (CustomStatus)objExamController.AddExamDayElect(objExam, OrgID, Modeexam, ccode, sessionid, subexamno);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(updglobal, "Exam Day(s) Saved Successfully!", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(updglobal, "Exam Day(s) Updated Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(updglobal, "Error While Insert/Update", this.Page);
                        }
                    }
                    DropDownList ddlSlot = dataitem.FindControl("ddlSlot") as DropDownList;
                    TextBox txtDate = dataitem.FindControl("txtExamDate") as TextBox;
                    ddlSlot.Enabled = true;
                    txtDate.Enabled = true;
                    GetCourses1();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnSubmit1_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ddlSession1.SelectedIndex = 0;
        ddlpattern.Items.Clear();
        ddlpattern.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype1.Items.Clear();
        ddlSubjecttype1.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        ddlSubexamname1.Items.Clear();
        ddlSubexamname1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
        btnViewLogin1.Visible = false;
    }

    private void GetCourses1()
    {
        //if (Convert.ToInt32(Session["OrgId"]) != 9)
        //{

        //    ScriptManager.RegisterStartupScript(this.updglobal, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(6)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(6)').hide();});", true);

        //}
        DataSet ds = objExamController.GetCoursesGlobleElectiv(Convert.ToInt16(ddlSession1.SelectedValue), Convert.ToInt16(ddlSubjecttype1.SelectedValue), Convert.ToInt16(ddlSubexamname1.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvCourse1.DataSource = ds;
            lvCourse1.DataBind();
            lvCourse1.Visible = true;
            foreach (ListViewDataItem lvHead in lvCourse1.Items)
            {
                DropDownList ddlSlot = lvHead.FindControl("ddlSlot") as DropDownList;
                DropDownList ddlmodeexam = lvHead.FindControl("ddlmodeexam") as DropDownList;
                HiddenField hdn_fld = lvHead.FindControl("hdf_slotno") as HiddenField;
                HiddenField hdf_modeexam = lvHead.FindControl("hdf_modeexam") as HiddenField;
                CheckBox chkHead = lvHead.FindControl("chkAccept") as CheckBox;
                TextBox txtDate = lvHead.FindControl("txtExamDate") as TextBox;
                objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT WITH (NOLOCK)", "SLOTNO", "SLOTNAME", "SLOTNO>0", "SLOTNO");
                objCommon.FillDropDownList(ddlmodeexam, "ACD_EXAMINATION_MODE WITH (NOLOCK)", "ModeEXAMno", "ModeEXAMNAME", "ModeEXAMNO>0", "ModeEXAMNO");
                ddlSlot.SelectedValue = hdn_fld.Value;
                ddlmodeexam.SelectedValue = hdf_modeexam.Value;

                if (chkHead.Checked == true)
                {
                    ddlSlot.Enabled = true;
                    txtDate.Enabled = true;
                    btnViewLogin1.Visible = true;
                    count++;
                }
                else
                {
                    ddlSlot.Enabled = false;
                    txtDate.Enabled = false;
                    count--;
                }
                btnSubmit1.Enabled = true;
            }
        }
        else
        {
            lvCourse1.DataSource = ds;
            lvCourse1.DataBind();
            btnSubmit1.Enabled = false;
            objCommon.DisplayMessage(this.updglobal, "Global Elective Course Not Found", this.Page);
        }
    }

    protected void checkbox1()
    {
        foreach (ListViewDataItem ROW in lvCourse1.Items)
        {
            CheckBox chkBox = ROW.FindControl("chkAccept") as CheckBox;
            TextBox txtDate1 = ROW.FindControl("txtExamDate") as TextBox;
            DropDownList dropslot1 = ROW.FindControl("ddlSlot") as DropDownList;
            if (chkBox.Checked)
            {
                txtDate1.Enabled = true;
                dropslot1.Enabled = true;
            }
            else
            {
                txtDate1.Enabled = false;
                dropslot1.Enabled = false;
            }
        }
    }

    private bool Validate1()
    {
        bool stat = true;
        foreach (ListViewDataItem lvHead in lvCourse1.Items)
        {
            CheckBox chkBox = lvHead.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                TextBox txtDate = lvHead.FindControl("txtExamDate") as TextBox;
                DropDownList dropslot = lvHead.FindControl("ddlSlot") as DropDownList;
                string ccode = (lvHead.FindControl("lblCourseno") as Label).ToolTip;
                foreach (ListViewDataItem row in lvCourse1.Items)
                {
                    CheckBox chkBox1 = row.FindControl("chkAccept") as CheckBox;
                    if (chkBox1.Checked)
                    {
                        TextBox txtDate1 = row.FindControl("txtExamDate") as TextBox;
                        DropDownList dropslot1 = row.FindControl("ddlSlot") as DropDownList;
                        string ccode1 = (row.FindControl("lblCourseno") as Label).ToolTip;
                        if (txtDate.Text == txtDate1.Text && dropslot.SelectedValue == dropslot1.SelectedValue && ccode1 != ccode)
                        {
                            stat = false;
                            checkbox1();
                            return stat;
                        }
                    }

                }

            }

        }
        return stat;
    }

    private bool checkDate1()
    {
        Boolean stat = true;
        foreach (ListViewDataItem dataitem in lvCourse1.Items)
        {

            CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

            if (chkBox.Checked)
            {
                try
                {
                    DateTime date1 = Convert.ToDateTime((dataitem.FindControl("txtExamDate") as TextBox).Text);
                    if (date1 < DateTime.Now)
                    {
                        stat = false;
                        checkbox1();
                        return stat;
                    }
                }
                catch (Exception)
                {
                    stat = false;
                    return stat;
                }
            }
        }
        checkbox1();
        return stat;
    }

    private bool checkBlack1()
    {
        Boolean stat = true;
        foreach (ListViewDataItem dataitem in lvCourse1.Items)
        {

            CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

            if (chkBox.Checked)
            {
                try
                {
                    string date = (dataitem.FindControl("txtExamDate") as TextBox).Text;
                    int value = Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue);
                    if (string.IsNullOrEmpty(date))
                    {
                        stat = false;
                        return stat;
                    }
                    if (value == 0)
                    {
                        stat = false;
                        return stat;
                    }
                }
                catch (Exception)
                {
                    stat = false;
                    return stat;
                }
            }
        }
        checkbox1();
        return stat;
    }

    protected void ddlSlot1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList currentDropdown = (DropDownList)sender;
        ListViewItem currentItem = (ListViewItem)currentDropdown.NamingContainer;
        int currentIndex = currentItem.DisplayIndex;
        string selectedValue = currentDropdown.SelectedValue;
        string textdate = null;
        currentDropdown.Enabled = true;
        foreach (ListViewDataItem item in lvCourse1.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                TextBox txtDate = item.FindControl("txtExamDate") as TextBox;
                textdate = txtDate.Text;
                txtDate.Enabled = true;
            }

        }
        foreach (ListViewDataItem ROW in lvCourse1.Items)
        {
            CheckBox chkBox = ROW.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                if (ROW.DisplayIndex != currentIndex)
                {
                    TextBox txtDate1 = ROW.FindControl("txtExamDate") as TextBox;
                    DropDownList dropslot1 = ROW.FindControl("ddlSlot") as DropDownList;
                    if (currentDropdown.Text == dropslot1.Text && textdate == txtDate1.Text)
                    {
                        objCommon.DisplayMessage(this.updglobal, "Same Date & Same Slot Already Exists", this.Page);
                        currentDropdown.SelectedIndex = 0;
                        checkbox1();
                        return;
                    }
                    txtDate1.Enabled = true;
                    dropslot1.Enabled = true;
                }
            }
        }
        checkbox1();
    }

    protected void txtExamDate1_TextChanged(object sender, EventArgs e)
    {
        TextBox currentdate = (TextBox)sender;
        ListViewItem currentItem = (ListViewItem)currentdate.NamingContainer;
        int currentIndex = currentItem.DisplayIndex;
        string selecteddate = currentdate.Text;
        string drpslot = null;
        currentdate.Enabled = true;
        foreach (ListViewDataItem item in lvCourse1.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                DropDownList dropslot = item.FindControl("ddlSlot") as DropDownList;
                drpslot = dropslot.Text;
                dropslot.Enabled = true;
            }

        }
        foreach (ListViewDataItem ROW in lvCourse1.Items)
        {
            CheckBox chkBox = ROW.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                if (ROW.DisplayIndex != currentIndex)
                {
                    TextBox txtDate1 = ROW.FindControl("txtExamDate") as TextBox;
                    DropDownList dropslot1 = ROW.FindControl("ddlSlot") as DropDownList;
                    if (drpslot == dropslot1.Text && selecteddate == txtDate1.Text)
                    {
                        objCommon.DisplayMessage(this.updglobal, "Same Date & Same Slot Already Exists", this.Page);
                        currentdate.Text = string.Empty;
                        checkbox1();
                        return;
                    }
                    txtDate1.Enabled = true;
                    dropslot1.Enabled = true;
                }
            }
        }
        checkbox1();
    }

    protected void ddlSubexamname1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
        btnViewLogin1.Visible = false;
    }

    protected void ibtnEvalDelete1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int sessionid = Convert.ToInt32(ddlSession1.SelectedValue);
            int subexamno = Convert.ToInt32(ddlSubexamname1.SelectedValue);
            foreach (ListViewDataItem dataitem in lvCourse1.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {

                    ImageButton ibtnEvalDelete1 = sender as ImageButton;
                    string ccode = (ibtnEvalDelete1.CommandArgument).ToString();
                    int retStatus = Convert.ToInt32(objExamController.DeleteTimeTableElectiv(ccode, sessionid, subexamno));
                    if ((retStatus == 1) && (chkBox.Checked))
                    {
                        objCommon.DisplayMessage(this.updglobal, "Time Table Cancelled Successfully", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updglobal, "Error in Cancelling Time Table", this.Page);
                    }
                    GetCourses1();
                    btnViewLogin1.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updglobal, "Select CheckBox to delete Time Table", this.Page);
                    btnViewLogin1.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.ibtnEvalDelete1_Click() --> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnViewLogin1_Click(object sender, EventArgs e)
    {
        try
        {
            ExamController objExamController = new ExamController();
            int sessionid = Convert.ToInt32(ddlSession1.SelectedValue);
            int subexamno = Convert.ToInt32(ddlSubexamname1.SelectedValue);
            CustomStatus cs = (CustomStatus)objExamController.GetViewOnStudentLock(sessionid, subexamno);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updglobal, "Record Update Successfully!", this.Page);
            }
            else if (cs.Equals(CustomStatus.Error))
            {
                objCommon.DisplayMessage(updglobal, "Request Fail!", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnViewLogin1_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlpattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        btnViewLogin1.Visible = false;
        btnSubmit1.Enabled = false;
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        objCommon.FillDropDownList(ddlSubjecttype1, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)  INNER JOIN ACD_SESSION_MASTER SM  ON (SM.SESSIONNO= R.SESSIONNO) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO=R.SCHEMENO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=SC.PATTERNNO)", " DISTINCT R.SUBID", "S.SUBNAME", "sm.SESSIONID=" + Convert.ToInt32(ddlSession1.SelectedValue) + "AND EP.PATTERNNO=" + Convert.ToInt32(ddlpattern.SelectedValue), "R.SUBID");
    }

    #endregion global Tab End

    #region Common Course Tab
    protected void btncancel2_Click(object sender, EventArgs e)
    {
        ddlSession2.SelectedIndex = 0;
        ddlpattern1.Items.Clear();
        ddlpattern1.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype2.Items.Clear();
        ddlSubjecttype2.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName2.Items.Clear();
        ddlExamName2.Items.Add(new ListItem("Please Select", "0"));
        ddlSubexamname2.Items.Clear();
        ddlSubexamname2.Items.Add(new ListItem("Please Select", "0"));
        ddlcoursecat.Items.Clear();
        ddlcoursecat.Items.Add(new ListItem("Please Select", "0"));
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        lvtimetable.DataSource = null;
        lvtimetable.DataBind();
        btnsubmit2.Enabled = false;
        btnviewstudlog2.Visible = false;
    }

    protected void btncourse2_Click(object sender, EventArgs e)
    {
        btnviewstudlog2.Visible = true;
        btnsubmit2.Enabled = true;
        GetCommonCourses();
    }

    private void GetCommonCourses()
    {
        //if (Convert.ToInt32(Session["OrgId"]) != 9)
        //{

        //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(6)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(6)').hide();});", true);

        //}
        DataSet ds = objExamController.GetCommonCourseTimeTable(Convert.ToInt16(ddlSession2.SelectedValue), Convert.ToInt16(ddlSubjecttype2.SelectedValue), Convert.ToInt16(ddlSubexamname2.SelectedValue), Convert.ToInt16(ddlcoursecat.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvtimetable.DataSource = ds;
            lvtimetable.DataBind();
            foreach (ListViewDataItem dataitem in lvtimetable.Items)
            {
                ListBox ddlschemelist = dataitem.FindControl("ddlschemelist") as ListBox;
                HiddenField hdn_schemeno = dataitem.FindControl("hdn_schemeno") as HiddenField;

                DataSet ds1 = objCommon.FillDropDown("ACD_SCHEME  WITH (NOLOCK)", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO IN (" + hdn_schemeno.Value.ToString() + ")", "");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlschemelist.DataSource = ds1;
                    ddlschemelist.DataValueField = ds1.Tables[0].Columns[0].ToString();
                    ddlschemelist.DataTextField = ds1.Tables[0].Columns[1].ToString();
                    ddlschemelist.DataBind();
                }
                string schemenos = hdn_schemeno.Value.ToString();
                for (int j = 0; j < schemenos.ToString().Length; j++)
                {
                    for (int k = 0; k < ddlschemelist.Items.Count; k++)
                    {
                        if (schemenos.Contains(ddlschemelist.Items[k].Value))
                        {
                            ddlschemelist.Items[k].Selected = true;
                        }
                    }
                }
            }
            btnviewstudlog2.Visible = true;
        }
        else
        {

            lvtimetable.DataSource = ds;
            lvtimetable.DataBind();
            btnsubmit2.Enabled = false;
            btnviewstudlog2.Visible = false;
        }
        SetInitialRow();
    }

    protected void btnsubmit2_Click(object sender, EventArgs e)
    {
        try
        {
            if (CheckMultipleScheme() == false)
            {
                return;
            }
            Exam objExam = new Exam();
            CustomStatus cs = 0;
            objExam.Status = 1;
            int sessionid = Convert.ToInt32(ddlSession2.SelectedValue);
            int orgid = Convert.ToInt32(Session["OrgId"]);
            int subexamno = Convert.ToInt32(ddlSubexamname2.SelectedValue);
            int subid = Convert.ToInt32(ddlSubjecttype2.SelectedValue);
            objExam.Exam_TT_Type = Convert.ToInt32(ddlExamName2.SelectedValue);
            foreach (ListViewDataItem items in lvcommoncourse.Items)
            {
                DropDownList ddlcommoncourse = items.FindControl("ddlcommoncourse") as DropDownList;
                ListBox ddlschemelist = items.FindControl("ddlschemelist") as ListBox;
                DropDownList ddlSlot1 = items.FindControl("ddlSlot1") as DropDownList;
                TextBox txtExamDate1 = items.FindControl("txtExamDate1") as TextBox;
                string schemes = selectedMultipeScheme(ddlschemelist);
                string ccode = ddlcommoncourse.SelectedValue.ToString();
                objExam.Examdate = Convert.ToDateTime(txtExamDate1.Text.ToString());
                objExam.Slot = Convert.ToInt32(ddlSlot1.SelectedValue);
                cs = (CustomStatus)objExamController.AddCommonCourseTimeTable(objExam, orgid, ccode, sessionid, subexamno, subid, schemes);
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updcommon, "Exam Day(s) Saved Successfully", this.Page);
                ClearCommon();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updcommon, "Exam Day(s) Updated Successfully", this.Page);
                ClearCommon();
            }
            else
            {
                objCommon.DisplayMessage(updcommon, "Error While Insert/Update", this.Page);
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnsubmit2_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ClearCommon()
    {
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        SetInitialRow();
        GetCommonCourses();
    }

    protected void btnviewstudlog2_Click(object sender, EventArgs e)
    {
        int stat = 0;
        foreach (ListViewDataItem item in lvtimetable.Items)
        {
            HiddenField ccode = item.FindControl("hdf_course") as HiddenField;
            HiddenField scheme = item.FindControl("hdn_schemeno") as HiddenField;
            Label Date = item.FindControl("txtExamDate1") as Label;
            HiddenField slot = item.FindControl("hdf_slotno1") as HiddenField;
            stat = objExamController.GetViewOnStudentLock_CommonCourses(Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(ddlSubexamname2.SelectedValue), Convert.ToInt32(ddlSubjecttype2.SelectedValue), Convert.ToInt32(slot.Value), Convert.ToDateTime(Date.Text), ccode.Value, scheme.Value);

        }
        if (stat == 1)
        {
            objCommon.DisplayMessage(updcommon, "Exam Time Table Updated Successfully On Student Login", this.Page);
            GetCommonCourses();
        }
        else
        {
            objCommon.DisplayMessage(updcommon, "Failed to Updated Time Table", this.Page);
        }
    }

    protected void ddlSession2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpattern1.Items.Clear();
        ddlpattern1.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype2.Items.Clear();
        ddlSubjecttype2.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName2.Items.Clear();
        ddlExamName2.Items.Add(new ListItem("Please Select", "0"));
        ddlSubexamname2.Items.Clear();
        ddlSubexamname2.Items.Add(new ListItem("Please Select", "0"));
        ddlcoursecat.Items.Clear();
        ddlcoursecat.Items.Add(new ListItem("Please Select", "0"));
        btnsubmit2.Enabled = false;
        btnviewstudlog2.Visible = false;
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        lvtimetable.DataSource = null;
        lvtimetable.DataBind();

        if (ddlSession2.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlpattern1, "ACD_EXAM_PATTERN EP INNER JOIN ACD_SCHEME S ON (EP.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_EXAM_NAME EN ON (EP.PATTERNNO=EN.PATTERNNO AND ISNULL(EN.ACTIVESTATUS,0) = 1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT EP.PATTERNNO", "PATTERN_NAME", "EP.PATTERNNO > 0 AND ISNULL(EP.ACTIVESTATUS,0)=1 AND SESSIONID=" + Convert.ToInt32(ddlSession2.SelectedValue), "EP.PATTERNNO");
        }
    }

    protected void ddlpattern1_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlSubjecttype2.Items.Clear();
        ddlSubjecttype2.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName2.Items.Clear();
        ddlExamName2.Items.Add(new ListItem("Please Select", "0"));
        ddlSubexamname2.Items.Clear();
        ddlSubexamname2.Items.Add(new ListItem("Please Select", "0"));
        ddlcoursecat.Items.Clear();
        ddlcoursecat.Items.Add(new ListItem("Please Select", "0"));
        btnsubmit2.Enabled = false;
        btnviewstudlog2.Visible = false;
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        lvtimetable.DataSource = null;
        lvtimetable.DataBind();
        if (ddlpattern1.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubjecttype2, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)  INNER JOIN ACD_SESSION_MASTER SM  ON (SM.SESSIONNO= R.SESSIONNO) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO=R.SCHEMENO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=SC.PATTERNNO)", " DISTINCT R.SUBID", "S.SUBNAME", "sm.SESSIONID=" + Convert.ToInt32(ddlSession2.SelectedValue) + "AND EP.PATTERNNO=" + Convert.ToInt32(ddlpattern1.SelectedValue), "R.SUBID");
        }
    }

    protected void ddlSubjecttype2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamName2.Items.Clear();
        ddlExamName2.Items.Add(new ListItem("Please Select", "0"));
        ddlSubexamname2.Items.Clear();
        ddlSubexamname2.Items.Add(new ListItem("Please Select", "0"));
        ddlcoursecat.Items.Clear();
        ddlcoursecat.Items.Add(new ListItem("Please Select", "0"));
        btnsubmit2.Enabled = false;
        btnviewstudlog2.Visible = false;
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        lvtimetable.DataSource = null;
        lvtimetable.DataBind();
        if (ddlSubjecttype2.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlExamName2, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND ISNULL(ACTIVESTATUS,0)=1 AND SUBID=" + Convert.ToInt32(ddlSubjecttype2.SelectedValue) + "AND S.PATTERNNO=" + Convert.ToInt32(ddlpattern1.SelectedValue), "EXAMNAME");
        }
    }

    protected void ddlExamName2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubexamname2.Items.Clear();
        ddlSubexamname2.Items.Add(new ListItem("Please Select", "0"));
        ddlcoursecat.Items.Clear();
        ddlcoursecat.Items.Add(new ListItem("Please Select", "0"));
        btnsubmit2.Enabled = false;
        btnviewstudlog2.Visible = false;
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        lvtimetable.DataSource = null;
        lvtimetable.DataBind();
        if (ddlExamName2.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubexamname2, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.SUBEXAMNO", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = " + Convert.ToInt32(ddlExamName2.SelectedValue) + "  AND ISNULL(EP.ACTIVESTATUS,0) = 1 AND SE.SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjecttype2.SelectedValue) + "", "SE.SUBEXAMNO");
        }
    }

    protected void ddlSubexamname2_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsubmit2.Enabled = false;
        btnviewstudlog2.Visible = false;
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        lvtimetable.DataSource = null;
        lvtimetable.DataBind();
        ddlcoursecat.Items.Clear();
        ddlcoursecat.Items.Add(new ListItem("Please Select", "0"));
        if (ddlSubexamname2.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlcoursecat, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON  (SR.CCODE=C.CCODE) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (ST.IDNO=SR.IDNO) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONNO=SR.SESSIONNO) INNER JOIN ACD_COURSE_CATEGORY CC ON (CC.CATEGORYNO=C.CATEGORYNO)", " DISTINCT C.CATEGORYNO", "CATEGORYNAME", "ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(SR.REGISTERED,0)=1 AND ISNULL(SR.EXAM_REGISTERED,0)=1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession2.SelectedValue) + " AND SR.SUBID=" + Convert.ToInt32(ddlSubjecttype2.SelectedValue), "C.CATEGORYNO");
        }
    }

    protected void ddlcoursecat_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnsubmit2.Enabled = false;
        btnviewstudlog2.Visible = false;
        lvcommoncourse.DataSource = null;
        lvcommoncourse.DataBind();
        lvtimetable.DataSource = null;
        lvtimetable.DataBind();
    }

    protected void imgaddcourse_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (lvcommoncourse.Items.Count > 0)
                {
                    DataTable dtNewTable = new DataTable();
                    dtNewTable.Columns.Add(new DataColumn("CCODE", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("COURSENAME", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("SCHEME", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("SCHEMENAME", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("EXAMDATE", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("SLOTNO", typeof(int)));
                    dtNewTable.Columns.Add(new DataColumn("SLOT_NAME", typeof(string)));

                    drCurrentRow = dtNewTable.NewRow();
                    drCurrentRow["CCODE"] = string.Empty;
                    drCurrentRow["COURSENAME"] = string.Empty;
                    drCurrentRow["SCHEME"] = string.Empty;
                    drCurrentRow["SCHEMENAME"] = string.Empty;
                    drCurrentRow["EXAMDATE"] = string.Empty;
                    drCurrentRow["SLOTNO"] = 0;
                    drCurrentRow["SLOT_NAME"] = string.Empty;

                    for (int i = 0; i < lvcommoncourse.Items.Count; i++)
                    {
                        HiddenField hdfcourse = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdf_course");
                        HiddenField hdfslotno1 = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdf_slotno1");
                        HiddenField hdn_schemeno = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdn_schemeno");
                        DropDownList ddlcommoncourse = (DropDownList)lvcommoncourse.Items[rowIndex].FindControl("ddlcommoncourse");
                        ListBox ddlschemelist = (ListBox)lvcommoncourse.Items[rowIndex].FindControl("ddlschemelist");
                        DropDownList ddlSlot1 = (DropDownList)lvcommoncourse.Items[rowIndex].FindControl("ddlSlot1");
                        TextBox txtExamDate1 = (TextBox)lvcommoncourse.Items[rowIndex].FindControl("txtExamDate1");

                        if (ddlcommoncourse.SelectedIndex == 0)
                        {
                            objCommon.DisplayMessage(updcommon, "Please Select Course", this.Page);
                            return;
                        }
                        else if (selectedScheme(ddlschemelist) == false)
                        {
                            objCommon.DisplayMessage(updcommon, "Please Select Scheme", this.Page);
                            return;
                        }
                        else if (txtExamDate1.Text.Trim() == string.Empty)
                        {
                            objCommon.DisplayMessage(updcommon, "Please Enter Date", this.Page);
                            return;
                        }
                        else if (ddlSlot1.SelectedIndex == 0)
                        {
                            objCommon.DisplayMessage(updcommon, "Please Select Slot", this.Page);
                            return;
                        }
                        else
                        {
                            hdfcourse.Value = ddlcommoncourse.Text;
                            hdn_schemeno.Value = selectedMultipeScheme(ddlschemelist);
                            hdfslotno1.Value = ddlSlot1.Text;
                            drCurrentRow = dtNewTable.NewRow();
                            drCurrentRow["CCODE"] = hdfcourse.Value;
                            drCurrentRow["COURSENAME"] = ddlcommoncourse.Text;
                            drCurrentRow["SCHEME"] = hdn_schemeno.Value;
                            drCurrentRow["SCHEMENAME"] = ddlschemelist.Text;
                            drCurrentRow["EXAMDATE"] = txtExamDate1.Text;
                            drCurrentRow["SLOTNO"] = hdfslotno1.Value;
                            drCurrentRow["SLOT_NAME"] = ddlSlot1.Text;
                            rowIndex++;
                            dtNewTable.Rows.Add(drCurrentRow);
                        }
                    }

                    drCurrentRow = dtNewTable.NewRow();
                    drCurrentRow["CCODE"] = string.Empty;
                    drCurrentRow["COURSENAME"] = string.Empty;
                    drCurrentRow["SCHEME"] = string.Empty;
                    drCurrentRow["SCHEMENAME"] = string.Empty;
                    drCurrentRow["EXAMDATE"] = string.Empty;
                    drCurrentRow["SLOTNO"] = 0;
                    drCurrentRow["SLOT_NAME"] = string.Empty;
                    dtNewTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtNewTable;
                    lvcommoncourse.DataSource = dtNewTable;
                    lvcommoncourse.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updcommon, "Maximum Options Limit Reached", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.imgaddcourse_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        SetPreviousData();
    }

    protected void ddlcommoncourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList currentdropdown = (DropDownList)sender;
        ListViewItem currentItem = (ListViewItem)currentdropdown.NamingContainer;
        int currentIndex = currentItem.DisplayIndex;
        string selectedCcode = currentdropdown.SelectedValue;
        foreach (ListViewDataItem item in lvcommoncourse.Items)
        {

            ListBox ddlschemelist = item.FindControl("ddlschemelist") as ListBox;
            if (item.DisplayIndex == currentIndex)
            {
                ddlschemelist.Items.Clear();
                DataSet ds1 = objCommon.FillDropDown("ACD_SCHEME C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON  (SR.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (ST.IDNO=SR.IDNO) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT SR.SCHEMENO", "C.SCHEMENAME", "ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(SR.REGISTERED,0)=1 AND ISNULL(SR.EXAM_REGISTERED,0)=1 AND SM.SESSIONID= " + Convert.ToInt32(ddlSession2.SelectedValue) + " AND SR.SUBID= " + Convert.ToInt32(ddlSubjecttype2.SelectedValue) + " AND CCODE='" + selectedCcode + "'", "SR.SCHEMENO");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlschemelist.DataSource = ds1;
                    ddlschemelist.DataValueField = ds1.Tables[0].Columns[0].ToString();
                    ddlschemelist.DataTextField = ds1.Tables[0].Columns[1].ToString();
                    ddlschemelist.DataBind();
                }
            }

        }

    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("CCODE", typeof(string)));
        dt.Columns.Add(new DataColumn("COURSENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("SCHEME", typeof(string)));
        dt.Columns.Add(new DataColumn("SCHEMENAME", typeof(string)));
        dt.Columns.Add(new DataColumn("EXAMDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("SLOTNO", typeof(int)));
        dt.Columns.Add(new DataColumn("SLOT_NAME", typeof(string)));

        dr = dt.NewRow();
        dr["CCODE"] = string.Empty;
        dr["COURSENAME"] = string.Empty;
        dr["SCHEME"] = string.Empty;
        dr["SCHEMENAME"] = string.Empty;
        dr["EXAMDATE"] = string.Empty;
        dr["SLOTNO"] = 0;
        dr["SLOT_NAME"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvcommoncourse.DataSource = dt;
        lvcommoncourse.DataBind();
        //bind data to list view if initially no records found
        bindlistview();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        foreach (ListViewDataItem dataitem in lvcommoncourse.Items)
        {
            DropDownList ddlcommoncourse = dataitem.FindControl("ddlcommoncourse") as DropDownList;
            DropDownList ddlSlot1 = dataitem.FindControl("ddlSlot1") as DropDownList;
            ListBox ddlschemelist = dataitem.FindControl("ddlschemelist") as ListBox;

            objCommon.FillDropDownList(ddlcommoncourse, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON  (SR.CCODE=C.CCODE) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (ST.IDNO=SR.IDNO) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT C.CCODE ", "(C.CCODE +' - '+ COURSE_NAME) as COURSE_NAME", "ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(SR.REGISTERED,0)=1 AND ISNULL(SR.EXAM_REGISTERED,0)=1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession2.SelectedValue) + " AND SR.SUBID=" + Convert.ToInt32(ddlSubjecttype2.SelectedValue) + "AND (C.CATEGORYNO=" + Convert.ToInt32(ddlcoursecat.SelectedValue) + "OR 0=" + Convert.ToInt32(ddlcoursecat.SelectedValue) + ")", "C.CCODE");


            objCommon.FillDropDownList(ddlSlot1, "ACD_EXAM_TT_SLOT WITH (NOLOCK)", "SLOTNO", "SLOTNAME", "SLOTNO>0", "SLOTNO");

        }

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hdf_course = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdf_course");
                    HiddenField hdn_schemeno = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdn_schemeno");
                    HiddenField hdf_slotno1 = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdf_slotno1");
                    DropDownList ddlcommoncourse = (DropDownList)lvcommoncourse.Items[rowIndex].FindControl("ddlcommoncourse");
                    DropDownList ddlSlot1 = (DropDownList)lvcommoncourse.Items[rowIndex].FindControl("ddlSlot1");
                    ListBox ddlschemelist = (ListBox)lvcommoncourse.Items[rowIndex].FindControl("ddlschemelist");
                    TextBox txtExamDate1 = (TextBox)lvcommoncourse.Items[rowIndex].FindControl("txtExamDate1");
                    ImageButton imgbtnrowremove = (ImageButton)lvcommoncourse.Items[rowIndex].FindControl("imgbtnrowremove");
                    ImageButton imgaddcourse = (ImageButton)lvcommoncourse.Items[rowIndex].FindControl("imgaddcourse");

                    hdf_course.Value = dt.Rows[i]["CCODE"].ToString();
                    hdn_schemeno.Value = dt.Rows[i]["SCHEME"].ToString();
                    hdf_slotno1.Value = dt.Rows[i]["SLOTNO"].ToString();
                    ddlcommoncourse.SelectedValue = dt.Rows[i]["CCODE"].ToString();
                    ddlcommoncourse_SelectedIndexChanged(ddlcommoncourse, EventArgs.Empty);
                    ddlSlot1.SelectedValue = dt.Rows[i]["SLOTNO"].ToString();
                    string schemenos = dt.Rows[i]["SCHEME"].ToString();
                    for (int j = 0; j < schemenos.ToString().Length; j++)
                    {
                        for (int k = 0; k < ddlschemelist.Items.Count; k++)
                        {
                            if (schemenos.Contains(ddlschemelist.Items[k].Value))
                            {
                                ddlschemelist.Items[k].Selected = true;
                            }
                        }
                    }
                    txtExamDate1.Text = dt.Rows[i]["EXAMDATE"].ToString();
                    if (i == 0)
                    {
                        imgaddcourse.Visible = true;
                    }
                    else
                    {
                        imgbtnrowremove.Visible = true;
                    }
                    rowIndex++;
                }

                rowIndex = 0;
            }
        }
        else
        {
            SetInitialRow();
        }
    }

    protected bool selectedScheme(ListBox schemelist)
    {
        bool stat = false;
        foreach (ListItem item in schemelist.Items)
        {
            if (item.Selected == true)
            {
                stat = true;
            }
        }
        return stat;
    }

    protected string selectedMultipeScheme(ListBox schemelist)
    {
        string stat = "";
        foreach (ListItem item in schemelist.Items)
        {
            if (item.Selected == true)
            {
                stat += item.Value + ",";
            }
        }
        return stat;
    }

    protected void bindlistview()
    {
        if (lvcommoncourse.DataSource != null)
        {
            foreach (ListViewDataItem items in lvcommoncourse.Items)
            {
                DropDownList ddlcommoncourse = items.FindControl("ddlcommoncourse") as DropDownList;
                DropDownList ddlSlot1 = items.FindControl("ddlSlot1") as DropDownList;
                ListBox ddlschemelist = items.FindControl("ddlschemelist") as ListBox;
                ImageButton imgbtnrowremove = items.FindControl("imgbtnrowremove") as ImageButton;
                ImageButton imgaddcourse = items.FindControl("imgaddcourse") as ImageButton;
                objCommon.FillDropDownList(ddlcommoncourse, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON  (SR.CCODE=C.CCODE) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (ST.IDNO=SR.IDNO) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT C.CCODE ", "(C.CCODE +' - '+ COURSE_NAME) as COURSE_NAME", "ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(SR.REGISTERED,0)=1 AND ISNULL(SR.EXAM_REGISTERED,0)=1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession2.SelectedValue) + " AND SR.SUBID=" + Convert.ToInt32(ddlSubjecttype2.SelectedValue) + "AND (C.CATEGORYNO=" + Convert.ToInt32(ddlcoursecat.SelectedValue) + "OR 0=" + Convert.ToInt32(ddlcoursecat.SelectedValue) + ")", "C.CCODE");

                objCommon.FillDropDownList(ddlSlot1, "ACD_EXAM_TT_SLOT WITH (NOLOCK)", "SLOTNO", "SLOTNAME", "SLOTNO>0", "SLOTNO");

                imgbtnrowremove.Visible = false;
                imgaddcourse.Visible = true;
                btnsubmit2.Enabled = true;

            }
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = sender as ImageButton;
        int rowIndex = (Convert.ToInt32(imgbtn.CommandArgument) - 1);
        HiddenField hdf_course = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdf_course");
        HiddenField hdn_schemeno = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdn_schemeno");
        HiddenField hdf_slotno1 = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdf_slotno1");
        Label txtExamDate1 = (Label)lvtimetable.Items[rowIndex].FindControl("txtExamDate1");
        int stat = objExamController.DeleteTimeTableCommonCourses(hdf_course.Value, Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(ddlSubexamname2.SelectedValue), Convert.ToInt32(ddlSubjecttype2.SelectedValue), hdn_schemeno.Value, Convert.ToDateTime(txtExamDate1.Text), Convert.ToInt32(hdf_slotno1.Value));
        if (stat == 1)
        {
            objCommon.DisplayMessage(updcommon, "Exam Time Table Cancel Successfully", this.Page);
            GetCommonCourses();
        }
        else
        {
            objCommon.DisplayMessage(updcommon, "Failed to Cancel Time Table", this.Page);
        }
    }

    protected void imgbtnedit_Click(object sender, ImageClickEventArgs e)
    {
        int lvrowcount = 1;
        ImageButton imgbtn = sender as ImageButton;
        int rowIndex = (Convert.ToInt32(imgbtn.CommandArgument) - 1);
        HiddenField hdf_course = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdf_course");
        HiddenField hdn_schemeno = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdn_schemeno");
        HiddenField hdf_slotno1 = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdf_slotno1");
        Label txtExamDate1 = (Label)lvtimetable.Items[rowIndex].FindControl("txtExamDate1");
        ImageButton imgbtnrowremove = (ImageButton)lvtimetable.Items[rowIndex].FindControl("imgbtnrowremove");
        ImageButton imgaddcourse = (ImageButton)lvtimetable.Items[rowIndex].FindControl("imgaddcourse");
        foreach (ListViewDataItem item in lvcommoncourse.Items)
        {
            DropDownList ddlcommoncourse = item.FindControl("ddlcommoncourse") as DropDownList;
            DropDownList ddlSlot1 = item.FindControl("ddlSlot1") as DropDownList;
            ListBox ddlschemelist = item.FindControl("ddlschemelist") as ListBox;
            TextBox txtExamDate = item.FindControl("txtExamDate1") as TextBox;
            HiddenField hdf_course1 = item.FindControl("hdf_course") as HiddenField;
            HiddenField hdn_schemeno1 = item.FindControl("hdn_schemeno") as HiddenField;
            HiddenField hdf_slotno2 = item.FindControl("hdf_slotno1") as HiddenField;
            if (ddlSlot1.SelectedIndex == 0 && ddlcommoncourse.SelectedIndex == 0 && txtExamDate.Text == string.Empty && selectedScheme(ddlschemelist) == false)
            {
                hdf_course1.Value = hdf_course.Value;
                hdn_schemeno1.Value = hdn_schemeno.Value;
                hdf_slotno2.Value = hdf_slotno1.Value;
                txtExamDate.Text = (Convert.ToDateTime(txtExamDate1.Text)).ToString();
                ddlcommoncourse.SelectedValue = hdf_course1.Value.ToString();
                ddlcommoncourse_SelectedIndexChanged(ddlcommoncourse, EventArgs.Empty);
                ddlSlot1.SelectedValue = hdf_slotno2.Value.ToString();
                string schemenos = hdn_schemeno1.Value.ToString();

                for (int j = 0; j < schemenos.ToString().Length; j++)
                {
                    for (int k = 0; k < ddlschemelist.Items.Count; k++)
                    {
                        if (schemenos.Contains(ddlschemelist.Items[k].Value))
                        {
                            ddlschemelist.Items[k].Selected = true;
                        }
                    }
                }
            }
            else if (lvrowcount == lvcommoncourse.Items.Count)
            {
                imgaddcourse_Click(sender, e);
                imgbtnedit_Click(sender, e);
            }

            lvrowcount++;
        }
    }

    protected void imgbtnrowremove_Click(object sender, ImageClickEventArgs e)
    {
        setdatatoViewstate();
        ImageButton imgbtn = sender as ImageButton;
        ListViewDataItem item = imgbtn.NamingContainer as ListViewDataItem;
        ImageButton lb = (ImageButton)sender;
        ListViewDataItem gvRow = (ListViewDataItem)lb.NamingContainer;
        int rowID = gvRow.DataItemIndex;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count == 1)
            {
                if (gvRow.DataItemIndex <= dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            if (dt.Rows.Count > 1)
            {
                rowID = gvRow.DataItemIndex;
                if (gvRow.DataItemIndex <= dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);
                }
            }
            ViewState["CurrentTable"] = dt;
            lvcommoncourse.DataSource = dt;
            lvcommoncourse.DataBind();
            if (rowID == 0)
            {
                SetInitialRow();
            }
            SetPreviousData();
        }
    }

    protected bool CheckMultipleScheme()
    {
        bool stat = true;
        int row = 0, row1 = 0;
        string schemes = "", schemes1 = "";
        foreach (ListViewDataItem items in lvcommoncourse.Items)
        {
            DropDownList ddlcommoncourse = items.FindControl("ddlcommoncourse") as DropDownList;
            ListBox ddlschemelist = items.FindControl("ddlschemelist") as ListBox;
            schemes = selectedMultipeScheme(ddlschemelist);
            row1 = 0;
            foreach (ListViewDataItem item in lvcommoncourse.Items)
            {
                DropDownList ddlcommoncourse1 = item.FindControl("ddlcommoncourse") as DropDownList;
                ListBox ddlschemelist1 = item.FindControl("ddlschemelist") as ListBox;
                schemes1 = selectedMultipeScheme(ddlschemelist1);
                if (row != row1 && ddlcommoncourse.SelectedValue == ddlcommoncourse1.SelectedValue && schemes.Contains(schemes1))
                {
                    objCommon.DisplayMessage(updcommon, "Same Scheme selected for Course " + ddlcommoncourse.SelectedItem, this.Page);
                    stat = false;
                    return stat;
                }
                row1++;
            }
            row++;
        }
        return stat;
    }

    protected void setdatatoViewstate()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DataRow drCurrentRow = null;
        DataTable dtNewTable = new DataTable();
        drCurrentRow = dtNewTable.NewRow();
        dtNewTable.Columns.Add(new DataColumn("CCODE", typeof(string)));
        dtNewTable.Columns.Add(new DataColumn("COURSENAME", typeof(string)));
        dtNewTable.Columns.Add(new DataColumn("SCHEME", typeof(string)));
        dtNewTable.Columns.Add(new DataColumn("SCHEMENAME", typeof(string)));
        dtNewTable.Columns.Add(new DataColumn("EXAMDATE", typeof(string)));
        dtNewTable.Columns.Add(new DataColumn("SLOTNO", typeof(int)));
        dtNewTable.Columns.Add(new DataColumn("SLOT_NAME", typeof(string)));

        for (int i = 0; i < lvcommoncourse.Items.Count; i++)
        {

            HiddenField hdfcourse = (HiddenField)lvcommoncourse.Items[i].FindControl("hdf_course");
            HiddenField hdfslotno1 = (HiddenField)lvcommoncourse.Items[i].FindControl("hdf_slotno1");
            HiddenField hdn_schemeno = (HiddenField)lvcommoncourse.Items[i].FindControl("hdn_schemeno");
            DropDownList ddlcommoncourse = (DropDownList)lvcommoncourse.Items[i].FindControl("ddlcommoncourse");
            ListBox ddlschemelist = (ListBox)lvcommoncourse.Items[i].FindControl("ddlschemelist");
            DropDownList ddlSlot1 = (DropDownList)lvcommoncourse.Items[i].FindControl("ddlSlot1");
            TextBox txtExamDate1 = (TextBox)lvcommoncourse.Items[i].FindControl("txtExamDate1");
            hdfcourse.Value = ddlcommoncourse.Text;
            hdn_schemeno.Value = selectedMultipeScheme(ddlschemelist);
            hdfslotno1.Value = ddlSlot1.Text;
            drCurrentRow = dtNewTable.NewRow();
            drCurrentRow["CCODE"] = hdfcourse.Value;
            drCurrentRow["COURSENAME"] = ddlcommoncourse.Text;
            drCurrentRow["SCHEME"] = hdn_schemeno.Value;
            drCurrentRow["SCHEMENAME"] = ddlschemelist.Text;
            drCurrentRow["EXAMDATE"] = txtExamDate1.Text;
            drCurrentRow["SLOTNO"] = hdfslotno1.Value;
            drCurrentRow["SLOT_NAME"] = ddlSlot1.Text;
            dtNewTable.Rows.Add(drCurrentRow);
        }
        ViewState["CurrentTable"] = dtNewTable;
    }

    #endregion common Time Table End

}