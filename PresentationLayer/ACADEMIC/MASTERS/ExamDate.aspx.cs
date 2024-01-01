//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Exam Days Management                                                         
// CREATION DATE : 10-SEPT-2012                                                         
// CREATED BY    : ASHISH MOTGHARE       
// MODIFIED BY   : INJAMAM ANSARI                                              
// MODIFIED DATE :                                                                      
// MODIFIED DESC : Globale and Elective Course Time Table And All validation                                                  
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

                // Set form mode equals to -1(New Mode).

                //if (CheckActivity())
                //{
                this.PopulateDropDown();
                //this.BindDates();

                //}
                //else
                //{
                //    divbody.Visible = false;
                //    divbuttons.Visible = false;
                //}
                //lvDate.Enabled = true;
                int section = 0;
                string sec = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ISNULL(SEC_TIMETABLE,0)", "");
                if (string.IsNullOrEmpty(sec)) { } else { section = Convert.ToInt32(sec); }
                divbatch.Visible = false;
                if (section == 0)
                {
                    divsection.Visible = false;
                }
                else
                {
                    divsection.Visible = true;
                }
                btnSubmit.Visible = false;
                divMsg.InnerHtml = string.Empty;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }



        }
        #region commented to make common for all client
        //if ((Convert.ToInt32(Session["OrgId"]) == 7))// For Rajagiri Client
        //{
        //    btnExamAttendence.Visible = true;
        //}
        //else if ((Convert.ToInt32(Session["OrgId"]) == 2)) //For Crescent Client 
        //{
        //    btnExamAttendence.Visible = true;
        //    btnClashExcel.Visible = true;
        //}
        //else if ((Convert.ToInt32(Session["OrgId"]) == 3)) //For CPU KOTA Client 
        //{
        //    btnExamAttendence.Visible = true;
        //}
        //else if ((Convert.ToInt32(Session["OrgId"]) == 6))
        //{
        //    btnBranchWiserpt.Visible = true;
        //}
        //else
        //{
        //    btnBranchWiserpt.Visible = false;
        //    btnExamAttendence.Visible = false;
        //}
        #endregion

        if ((Convert.ToInt32(Session["OrgId"]) == 2)) //For Crescent Client 
        {
            btnClashExcel.Visible = true;
        }
        else
        {
            btnClashExcel.Visible = false;
        }

    }

    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();

        string sessionno = string.Empty;

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
        //sessionno = Session["currentsession"].ToString();
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

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
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

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "COLLEGE_IDS,DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["College_ids"] = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString();
                ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ViewState["Branchno"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
                ViewState["Semesterno"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
            }
            //Term
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
            //ddlSession.SelectedIndex = 1;
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //College Name
            if (Session["usertype"].ToString().Equals("1"))
            {
                //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + ViewState["College_ids"].ToString() + ")", "COLLEGE_ID DESC");
                // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            }
            else
            {
                //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND COLLEGE_ID IN (" + ViewState["College_ids"].ToString() + ")", "COLLEGE_ID");
                // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            }

            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");


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
                //Global Tab Session Drop Down
                ddlSession1.DataSource = ds1;
                ddlSession1.DataValueField = ds1.Tables[0].Columns[0].ToString();
                ddlSession1.DataTextField = ds1.Tables[0].Columns[1].ToString();
                ddlSession1.DataBind();

                //Common Tab Session Drop Down
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

    private void Clear()
    {

        ddlDay.SelectedIndex = 0;

        ddlSession.SelectedIndex = 0;

        ddlDegree.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlScheme.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;

        ddlDay.SelectedIndex = -1;
        ddlExamName.SelectedIndex = -1;
    }

    private void EnableControls()
    {
        ddlDegree.Enabled = true;
        ddlBranch.Enabled = true;
        ddlScheme.Enabled = true;
        ddlSemester.Enabled = true;
        ddlExamName.Enabled = true;
    }

    #region regular time table section start here
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
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
        //ddlsubexamname.Items.Clear();
        //ddlsubexamname.Items.Add(new ListItem("Please Select", "0"));
        ddlbatch.Items.Clear();
        ddlbatch.Items.Add(new ListItem("Please Select", "0"));
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        divbatch.Visible = false;
        ViewState["count"] = 0;
    }

    //Added By Hemanth G
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
            // int batchno = Convert.ToInt32(ddlbatch.SelectedValue);
            int batchno = ddlbatch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlbatch.SelectedValue);

            objExam.CollegeCode = Convert.ToString(Session["colcode"]);
            objExam.Exdtno = Convert.ToInt32(ViewState["exdtno"]);
            objExam.Exam_TT_Type = Convert.ToInt32(ddlExamName.SelectedValue);
            //objExam.Exam_TT_Type = Convert.ToInt32(ddlsubexamname.SelectedValue);       // added on 15-05-2023 by Injamam Ansari for Subexamwise //commented on 15-06-2023 by Injamam
            int th_pr = Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "TH_PR", "SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue)));
            objExam.collegeid = Convert.ToInt32(ViewState["college_id"]);
            int OrgID = Convert.ToInt32(Session["OrgId"]);
            if (checkcourse() == false)
            {
                objCommon.DisplayMessage(updExamdate, "No Course Selected!", this.Page);
                return;
            }
            if (checkblack() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Please Select Date And Slot", this.Page);
                return;
            }
            if (checkdate() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Invalid Date Format/Selected Date is previous Date than Today", this.Page);
                return;
            }
            if (Validate() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Same Date And Slot Already Exists", this.Page);
                return;
            }
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {
                    if (Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue) > 0 && Convert.ToString((dataitem.FindControl("txtExamDate") as TextBox).Text) != "")
                    {
                        objExam.Status = 1;
                        //objExam.Courseno = Convert.ToInt32((dataitem.FindControl("chkAccept") as CheckBox).ToolTip);// comment by gaurav 
                        //   objExam.Courseno = Convert.ToInt32((dataitem.FindControl("chkAccept") as CheckBox).ToolTip[0]);
                        objExam.Slot = Convert.ToInt32((dataitem.FindControl("ddlSlot") as DropDownList).SelectedValue);
                        // int Modeexam = 0;
                        int Modeexam = Convert.ToInt32((dataitem.FindControl("ddlmodeexam") as DropDownList).SelectedValue);
                        objExam.Examdate = Convert.ToDateTime((dataitem.FindControl("txtExamDate") as TextBox).Text);
                        int lblcourseno = Convert.ToInt32((dataitem.FindControl("lblCourseno") as Label).ToolTip);

                        objExam.Courseno = lblcourseno;
                        string ccode = Convert.ToString((dataitem.FindControl("chkAccept") as CheckBox).ToolTip);

                        #region commented
                        //if (Convert.ToInt32(Session["OrgId"]) == 9)  //Atlas
                        //{
                        //    CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno);
                        //    if (cs.Equals(CustomStatus.RecordSaved))
                        //    {


                        //        objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                        //    }
                        //}
                        //else if (Convert.ToInt32(Session["OrgId"]) == 6 && ddlSubjecttype.SelectedValue == "2")// Added by GAurav  For RCPIPER MAKE TIME TABLE BATCH WISE FOR PRACTICAL.
                        //{
                        //    CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, batchno);

                        //    if (cs.Equals(CustomStatus.RecordSaved))
                        //    {
                        //        objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                        //    }

                        //}
                        //else if (Convert.ToInt32(Session["OrgId"]) == 2)// for crescent
                        //{

                        //    CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, ccode);

                        //    if (cs.Equals(CustomStatus.RecordSaved))
                        //    {
                        //        objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                        //    }


                        //}
                        //else if (Convert.ToInt32(Session["OrgId"]) == 16 && th_pr > 1)// Added by Injamam  For Maher MAKE TIME TABLE BATCH WISE FOR PRACTICAL.
                        //{
                        //    CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, batchno);

                        //    if (cs.Equals(CustomStatus.RecordSaved))
                        //    {
                        //        objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                        //    }

                        //}
                        //else
                        //{

                        //    CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, ccode);

                        //    //CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno);
                        //    if (cs.Equals(CustomStatus.RecordSaved))
                        //    {

                        //        objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                        //    }
                        //}
                        #endregion

                        //Added by Injamam for Theory and Practical for common code

                        if (th_pr > 1) //For subject Type Other than Theory
                        {
                            //CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, batchno;
                            CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, batchno, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                            }
                        }
                        else //For Theory
                        {
                            //CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, ccode);
                            CustomStatus cs = (CustomStatus)objExamController.AddExamDay(objExam, OrgID, Modeexam, sectionno, ccode, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(updExamdate, "Exam Day(s) Saved Successfully!", this.Page);
                            }
                        }
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

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //********************************NEW******************************
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
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
        //ddlsubexamname.Items.Clear();
        //ddlsubexamname.Items.Add(new ListItem("Please Select", "0"));
        ddlbatch.Items.Clear();
        ddlbatch.Items.Add(new ListItem("Please Select", "0"));
        divbatch.Visible = false;
        ViewState["count"] = 0;

        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                //DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS", "@P_COLSCHEMENO", "" + Convert.ToInt32(ddlClgname.SelectedValue) + "");
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
                    if (ddlCollege.SelectedIndex > 0)
                    {
                        ddlDegree.Items.Clear();
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENAME");
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                        //ddlDegree.Focus();
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
                objUCommon.ShowError(Page, "ExamDate.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        #region commented old
        //*****************************END*******************************
        //try
        //{

        //    if (ddlCollege.SelectedIndex > 0)
        //    {
        //        ddlDegree.Items.Clear();

        //        if (Convert.ToInt32(Session["usertype"])==1 )
        //            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENAME");
        //            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND A.DEGREENO IN (" + ViewState["Degreeno"].ToString()+")", "A.DEGREENAME");

        //        else
        //            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.DEPTNO=" + Session["userdeptno"].ToString() + " AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND A.DEGREENO IN (" + ViewState["Degreeno"].ToString() + ")", "A.DEGREENAME");

        //        ddlDegree.Focus();
        //    }
        //    else
        //    {
        //        ddlDegree.Items.Clear();
        //        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        //        objCommon.DisplayMessage("Please select College/School Name.", this.Page);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ExamDate.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
        #endregion
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["count"] = 0;
            objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ViewState["branchno"]) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]), "S.SCHEMENAME DESC");

            ddlScheme.Focus();

            #region commented old
            //if (ddlDegree.SelectedValue == "1" && ddlBranch.SelectedValue == "99")
            //{
            //    ddlScheme.Items.Clear();
            //    ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            //    ddlScheme.Focus();
            //}
            //else
            //{
            //    // Scheme Name

            //    //DEPTNO
            //    if (Convert.ToInt32(Session["usertype"]) == 1)
            //        objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "S.SCHEMENAME DESC");
            //    else
            //        objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "S.DEPTNO=" + Session["userdeptno"].ToString() + " AND B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "S.SCHEMENAME DESC");

            //    ddlScheme.Focus();
            //}
            #endregion commented old
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //*****************************NEW**************

        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"], "A.LONGNAME");
        ddlBranch.Focus();
        // ddlScheme.SelectedIndex = 0;

        //************************END************************
        #region commented old
        //// Branch Name
        ////
        //if (Convert.ToInt32(Session["usertype"]) == 1)
        //    //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND A.BRANCHNO IN (" + ViewState["Branchno"].ToString() + ")", "A.LONGNAME");
        //else
        //   // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEPTNO=" + Session["userdeptno"].ToString() + " AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "A.LONGNAME");
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEPTNO=" + Session["userdeptno"].ToString() + " AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND A.BRANCHNO IN (" + ViewState["Branchno"].ToString() + ")", "A.LONGNAME");

        //ddlBranch.Focus();
        #endregion
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.Visible = false;
        lvCourse.DataBind();
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        //ddlsubexamname.Items.Clear();
        //ddlsubexamname.Items.Add(new ListItem("Please Select", "0"));
        ddlbatch.Items.Clear();
        ddlbatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype.Items.Clear();
        ddlSubjecttype.Items.Add(new ListItem("Please Select", "0"));
        divbatch.Visible = false;
        ViewState["count"] = 0;
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubjecttype, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND R.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "R.SUBID");
            //    //objCommon.FillDropDownList(ddlExamName, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "EXAMNAME");
            //    objCommon.FillDropDownList(ddlExamName, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue), "EXAMNAME");
        }
        #region commented old
        ////objCommon.FillDropDownList(ddlSection, "ACD_SECTION", " DISTINCT SECTIONNO", "SECTIONNAME", " SECTIONNO>0", "SECTIONNAME");

        //if (Convert.ToInt32(Session["OrgId"]) == 9)
        //{
        //    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SEC ON (SR.SECTIONNO=SEC.SECTIONNO)", " DISTINCT SR.SECTIONNO", "SEC.SECTIONNAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + ddlSemester.SelectedValue + "AND SR.SESSIONNO=" + ddlSession.SelectedValue + "AND SR.CANCEL=0", "SEC.SECTIONNAME");
        //}
        //else
        //{
        //    objCommon.FillDropDownList(ddlSection, "ACD_SECTION", " DISTINCT SECTIONNO", "SECTIONNAME", " SECTIONNO>0", "SECTIONNAME");
        //}





        //objCommon.FillDropDownList(ddlExamName, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " ED.FLDNAME IN('S3','EXTERMARK') AND EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "EXAMNAME");
        //****objCommon.FillDropDownList(ddlExamName, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " ED.FLDNAME IN('EXTERMARK') AND EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "EXAMNAME");
        #endregion commented old

    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.Visible = false;
        lvCourse.DataBind();
        ddlbatch.Items.Clear();
        ddlbatch.Items.Add(new ListItem("Please Select", "0"));
        //ddlsubexamname.Items.Clear();
        //ddlsubexamname.Items.Add(new ListItem("Please Select", "0"));
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ViewState["count"] = 0;
        int th_pr = Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "TH_PR", "SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue)));
        if (ddlSection.SelectedIndex > 0)
        {
            #region commented part
            //if (ddlSubjecttype.SelectedValue == "2" && Convert.ToInt32(Session["OrgId"]) == 6)// ADDED FOR RCPIPER 
            //{
            //    objCommon.FillDropDownList(ddlbatch, "ACD_SECTION A INNER JOIN ACD_BATCH B ON (A.SECTIONNO=B.SECTIONNO AND ISNULL(B.ACTIVESTATUS,0)=1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.BATCHNO=B.BATCHNO AND SR.SECTIONNO=A.SECTIONNO) ", " DISTINCT B.BATCHNO", "BATCHNAME", " BATCHNAME<>'' AND A.SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + "AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "BATCHNAME");

            //}
            //if (th_pr > 1 && Convert.ToInt32(Session["OrgId"]) == 16)// ADDED FOR MAHER BY INJAMAM ANSARI 
            //{
            //    objCommon.FillDropDownList(ddlbatch, "ACD_SECTION A INNER JOIN ACD_BATCH B ON (A.SECTIONNO=B.SECTIONNO AND ISNULL(B.ACTIVESTATUS,0)=1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.BATCHNO=B.BATCHNO AND SR.SECTIONNO=A.SECTIONNO) ", " DISTINCT B.BATCHNO", "BATCHNAME", " BATCHNAME<>'' AND A.SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + "AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "BATCHNAME");
            //}
            #endregion
            objCommon.FillDropDownList(ddlbatch, "ACD_SECTION A INNER JOIN ACD_BATCH B ON (A.SECTIONNO=B.SECTIONNO AND ISNULL(B.ACTIVESTATUS,0)=1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.BATCHNO=B.BATCHNO AND SR.SECTIONNO=A.SECTIONNO) ", " DISTINCT B.BATCHNO", "BATCHNAME", " BATCHNAME<>'' AND A.SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + "AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "BATCHNAME");
        }
        else
        {
            objCommon.FillDropDownList(ddlbatch, "ACD_SECTION A INNER JOIN ACD_BATCH B ON (A.SECTIONNO=B.SECTIONNO AND ISNULL(B.ACTIVESTATUS,0)=1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.BATCHNO=B.BATCHNO AND SR.SECTIONNO=A.SECTIONNO) ", " DISTINCT B.BATCHNO", "BATCHNAME", " BATCHNAME<>'' AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "BATCHNAME");
        }
        //objCommon.FillDropDownList(ddlSection, "ACD_SECTION", " DISTINCT SECTIONNO", "SECTIONNAME", " SECTIONNO>0", "SECTIONNAME");
        //objCommon.FillDropDownList(ddlExamName, "ACD_SECTION", " DISTINCT SECTIONNO", "SECTIONNAME", " 0> AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "SECTIONNAME");
        objCommon.FillDropDownList(ddlExamName, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "EXAMNAME");
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ViewState["count"] = 0;
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
        //*******objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND S.SEMESTERNO IN (" + ViewState["Semesterno"].ToString() + ")", "S.SEMESTERNO");

        objCommon.FillDropDownList(ddlSubjecttype, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "R.SUBID");

    }

    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //Function For Deleting the Selected EXAM TIMETABLE ENTRY.
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ViewState["count"] = null;
        GetCourses();
    }

    private void GetCourses()
    {
        //if (Convert.ToInt32(Session["OrgId"]) != 9)
        //{

        //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(6)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(6)').hide();});", true);

        //}

        int schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int collegeid = Convert.ToInt32(ViewState["college_id"]);
        int examno = Convert.ToInt32(ddlExamName.SelectedValue);
        int batchno = ddlbatch.SelectedValue == "" ? 0 : Convert.ToInt32(ddlbatch.SelectedValue);
        //int subexam = Convert.ToInt32(ddlsubexamname.SelectedValue); // commented on 15-06-2023 by Injamam
        //DataSet ds = objExamController.GetCourses(schemeno, semesterno, sessionno, collegeid, Convert.ToInt32(ddlSubjecttype.SelectedValue), sectionno, examno);   

        string proc_name1 = "PKG_GET_COURSE_ALL";
        string para_name1 = "@P_SCHEMENO,@P_SEMESTERNO,@P_SECTIONNO,@P_SESSIONNO,@P_COLLEGE_ID,@P_SUBID,@P_EXAMNO,@P_BATCHNO";
        string call_values1 = "" + schemeno + "," + semesterno + "," + sectionno + "," + sessionno + "," + collegeid + "," + Convert.ToInt32(ddlSubjecttype.SelectedValue) + "," + examno + "," + batchno + "";
        //string call_values1 = "" + schemeno + "," + semesterno + "," + sectionno + "," + sessionno + "," + collegeid + "," + Convert.ToInt32(ddlSubjecttype.SelectedValue) + "," + subexam + "," + batchno + "";    //subexam wise added by Injamam Ansari on 15-05-2023  // commented on 15-06-2023 by Injamam
        DataSet ds = objCommon.DynamicSPCall_Select(proc_name1, para_name1, call_values1);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["count"] = ds.Tables[0].Rows.Count;
            ViewState["ccode"] = ds.Tables[0].Rows[0]["CCODE"].ToString();
            lvCourse.DataSource = ds;
            lvCourse.DataBind();
            lvCourse.Visible = true;
            btnSubmit.Visible = true;
            //btnViewLogin.Visible = true;


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
                    //ddlSlot.Enabled = true;
                    //txtDate.Enabled = true;
                    count--;
                    //btnViewLogin.Visible = false;
                }

                if (count == 0)
                {
                    btnViewLogin.Visible = false;
                }
            }
            int count1 = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO= " + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)));
            if (count1 > 0)
            {
                btnSubmit.Visible = false;
                lvCourse.Enabled = false;
            }
            else
            {
                btnSubmit.Visible = true;
                lvCourse.Enabled = true;
            }
        }
        else
        {
            btnViewLogin.Visible = false;
            btnSubmit.Visible = false;
            lvCourse.DataSource = ds;
            lvCourse.DataBind();
            objCommon.DisplayMessage(updExamdate, "Record Not Found", this.Page);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        #region lookups
        //if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)) == "0")
        //int SecCheck = Convert.ToInt32((objCommon.LookUp("ACD_EXAM_DATE", "Count(*)", "schemeno=" + Convert.ToInt32(ViewState["schemeno"]) + "and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and subid=" + Convert.ToInt32(ddlSubjecttype.SelectedValue) + "and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + "and sectionno is not null")));

        //int Sectionnoss = Convert.ToInt32((objCommon.LookUp("ACD_EXAM_DATE", "Count(*)", "schemeno=" + Convert.ToInt32(ViewState["schemeno"]) + "and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and subid=" + Convert.ToInt32(ddlSubjecttype.SelectedValue) + "and semesterno=" + Convert.ToInt32(ddlSemester.SelectedValue) + "and sectionno=" + Convert.ToInt32(ddlSection.SelectedValue))));
        #endregion

        if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID=" + ViewState["college_id"] + " AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "AND BRANCHNO=" + Convert.ToInt32(ViewState["branchno"])) == "0")
        {
            objCommon.DisplayMessage(updExamdate, "Record not found", this);
            return;
        }
        else
        {
            #region Commented old
            //if (Convert.ToInt32(Session["OrgId"]) == 9) //ATLAS
            //{
            //    this.ShowReport("TimeTable_Report", "rptTimeTable_atlas.rpt");
            //}
            //else if (Convert.ToInt32(Session["OrgId"]) == 7)//RAJAGIRI
            //{
            //    this.ShowReport("TimeTable_Report", "rptTimeTable_rajagiri.rpt");
            //}
            //else if (Convert.ToInt32(Session["OrgId"]) == 3)  //CPU KOTA
            //{
            //    this.ShowReport("TimeTable_Report", "rptTimeTable_cpukota.rpt");
            //}
            //else if (Convert.ToInt32(Session["OrgId"]) == 6)  //RCPIPER
            //{
            //    if (ddlSubjecttype.SelectedValue == "2")
            //    {
            //        if (ddlSection.SelectedIndex > 0 && ddlbatch.SelectedIndex > 0)
            //        {
            //            this.ShowReport_BATCH("TimeTable_Report", "rptTimeTable_rcpiper_batchwise.rpt");
            //        }
            //        else
            //        {
            //            objCommon.DisplayMessage(updExamdate, "Please Select Section & Batch", this);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        this.ShowReport("TimeTable_Report", "rptTimeTable_rcpiper.rpt");
            //    }
            //}
            //else if (Convert.ToInt32(Session["OrgId"]) == 16)  //ADDED BY INJAMAM ANSARI FOR MAHER BATCHWISE REPORT 
            //{
            //    int th_pr = Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "TH_PR", "SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue)));
            //    if (th_pr > 1)
            //    {
            //        this.ShowReport_BATCH("TimeTable_Report", "rptTimeTable_batchwise.rpt");
            //    }
            //    else
            //    {
            //        this.ShowReport("TimeTable_Report", "rptTimeTable.rpt");
            //    }
            //}
            //else
            //{
            //    this.Show("TimeTable_Report", "rptTimeTable.rpt");
            //}
            #endregion commented

            //============ NEW FOR COMMONCODE CLIENT BATCH AND SECTION ADDED BY INJAMAM ANSARI
            int th_pr = Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "TH_PR", "SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue)));
            if (th_pr > 1)                          //FOR PRACTICAL COURSE 
            {
                if (Convert.ToInt32(Session["OrgId"]) == 6)  //RCPIPER
                {
                    if ((ddlSection.Visible == false || ddlbatch.Visible == false) || (ddlSection.SelectedIndex > 0 && ddlbatch.SelectedIndex > 0))
                    {
                        this.ShowReport_BATCH("TimeTable_Report", "rptTimeTable_rcpiper_batchwise.rpt");
                    }
                    else
                    {
                        objCommon.DisplayMessage(updExamdate, "Please Select Section & Batch", this);
                        return;
                    }
                }
                else
                {
                    this.ShowReport_BATCH("TimeTable_Report", "rptTimeTable_batchwise.rpt");
                }
            }
            else                                    // FOR THEORY COURSES
            {
                if (Convert.ToInt32(Session["OrgId"]) == 3)  //CPU KOTA
                {
                    this.ShowReport("TimeTable_Report", "rptTimeTable_cpukota.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6)  //RCPIPER
                {
                    this.ShowReport("TimeTable_Report", "rptTimeTable_rcpiper.rpt");
                }
                else
                {
                    this.ShowReport("TimeTable_Report", "rptTimeTable.rpt");
                }
            }
        }
    }

    protected void btnExamAttendence_Click(object sender, EventArgs e) //Added by Tejas Thakre on 09_12_2022
    {
        try
        {

            if ((Convert.ToInt32(Session["OrgId"]) == 7))// For Rajagiri Client
            {
                ShowReportDailyAttendence("Student_Daily_Attendence_Report", "rptDailyAttendanceSheet.rpt");
            }
            else if ((Convert.ToInt32(Session["OrgId"]) == 2))//For Crescent Client 
            {
                if (Convert.ToInt32(ViewState["count"]) == 0)
                {
                    objCommon.DisplayMessage(this.updExamdate, "Get Course List to See Attendance Report", this.Page);
                    return;
                }
                ShowReportDailyAttendenceCresent("Student_Daily_Attendence_Report", "rptDailyAttendanceSheet_Cresent.rpt");
            }
            else if ((Convert.ToInt32(Session["OrgId"]) == 3))//For CPU KOTA
            {
                ShowReportDailyAttendenceCupKota("Student_Daily_Attendence_Report", "rptAttendancesheet_CPUKOTA.rpt");
            }
            else if ((Convert.ToInt32(Session["OrgId"]) == 18))//For HITS ADDED BY SHUBHAM B
            {
                ShowReportDailyAttendenceCresent("Student_Daily_Attendence_Report", "rptDailyAttendanceSheet_HITS.rpt");
            }
            else if ((Convert.ToInt32(Session["OrgId"]) == 16)) // For Maher Added By TEJAS THAKRE 
            {
                if (ddlbatch.Visible == true)
                {
                    ShowReportDailyAttendenceMaher("Student_Daily_Attendence_Report", "rptAttendancesheet_MAHER.rpt");
                }
                else
                {
                    ShowReportDailyAttendenceCupKota("Student_Daily_Attendence_Report", "rptAttendancesheet.rpt");
                }
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 22) //For ADCET Added by Tejas 22_12_2023
            {
                ShowReportDailyAttendenceCupKota("Student_Daily_Attendence_Report", "rptAttendancesheet_ADCET.rpt");
            }
            else
            {
                ShowReportDailyAttendenceCupKota("Student_Daily_Attendence_Report", "rptAttendancesheet.rpt");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }

    private void Show(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
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
                //",@P_EXAM_NO=" + Convert.ToInt32(ddlsubexamname.SelectedValue) +  // Added By Injamam // commented on 15-06-2023 by Injamam
                 ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) +
                //",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                 ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.Show() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
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
                //",@P_EXAM_NO=" + Convert.ToInt32(ddlsubexamname.SelectedValue) +   //added by Injamam // commented on 15-06-2023 by Injamam
                 ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) +
                //",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                 ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.Show() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport_BATCH(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
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
                //",@P_EXAM_NO=" + Convert.ToInt32(ddlsubexamname.SelectedValue) + //Added by Injamam // commented on 15-06-2023 by Injamam
                  ",@P_BATCHNO=" + Convert.ToInt32(ddlbatch.SelectedValue) +
                 ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) +

                //",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                 ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.Show() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportDailyAttendence(string reportTitle, string rptFileName) //Added By Tejas Thakre on 09_12_2022
    {
        try
        {

            string Courseno = string.Empty;
            foreach (ListViewDataItem item in lvCourse.Items)
            {
                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                Label lblCourseno = item.FindControl("lblCourseno") as Label;

                if (chk.Checked)
                {
                    Courseno += ((item.FindControl("lblCourseno")) as Label).ToolTip + "$";
                }


            }
            Courseno = Courseno.TrimEnd('$');


            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            int Examno = Convert.ToInt32(ddlExamName.SelectedValue);
            //int Examno = Convert.ToInt32(ddlsubexamname.SelectedValue);  //added By Injama // commented on 15-06-2023 by Injamam
            //int Courseno = Convert.ToInt32(ddlCourse.SelectedValue); 
            int Schemeno = Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"]));
            //int College_Id = Convert.ToInt32(ddlCollege.SelectedValue);
            int SubId = Convert.ToInt32(ddlSubjecttype.SelectedValue);
            int Sectionno = Convert.ToInt32(ddlSection.SelectedValue);

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] +
                ",@P_SESSIONNO=" + Sessionno +
                ",@P_SEMESTERNO=" + Semesterno +
                ",@P_EXAMNO=" + Examno +
                ",@P_SUBID=" + SubId +
                //",@P_COURSENO=" + Courseno +
                ",@P_SCHEMENO=" + Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"])) +
                ",@P_COLLEGE_ID=" + ViewState["college_id"] +
                ",@P_SECTIONNO=" + Sectionno +
                ",@P_COUSRENO=" + Courseno + "";

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportDailyAttendenceCresent(string reportTitle, string rptFileName) //Added By Tejas Thakre on 21-12-2022
    {
        try
        {

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            int Examno = Convert.ToInt32(ddlExamName.SelectedValue);
            //int Examno = Convert.ToInt32(ddlsubexamname.SelectedValue); //added by Injamam // commented on 15-06-2023 by Injamam
            //int Courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            int Schemeno = Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"]));
            int College_Id = Convert.ToInt32(ddlCollege.SelectedValue);
            int SubId = Convert.ToInt32(ddlSubjecttype.SelectedValue);
            int section = 0;
            string Courseno = string.Empty;
            if (ddlSection.SelectedValue != null)
            {
                section = Convert.ToInt32(ddlSection.SelectedValue);
            }
            foreach (ListViewDataItem item in lvCourse.Items)
            {
                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                Label lblCourseno = item.FindControl("lblCourseno") as Label;
                if (chk.Checked)
                {
                    Courseno += ((item.FindControl("lblCourseno")) as Label).ToolTip + "$";
                }


            }
            Courseno = Courseno.TrimEnd('$');
            string procedure = "PKG_ACD_DAILY_ATTENDENCE_REPORT";
            string parameter = "@P_SESSIONNO,@P_SEMESTERNO,@P_EXAMNO,@P_SUBID,@P_SCHEMENO,@P_COLLEGE_ID,@P_COUSRENO,@P_SECTIONNO";
            string values = "" + Sessionno + "," + Semesterno + "," + Examno + "," + SubId + "," + Schemeno + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Courseno + "," + section + "";
            DataSet ds = objCommon.DynamicSPCall_Select(procedure, parameter, values);
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));   

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] +
             ",@P_SESSIONNO=" + Sessionno +
             ",@P_SEMESTERNO=" + Semesterno +
             ",@P_EXAMNO=" + Examno +
             ",@P_SUBID=" + SubId +
                    //",@P_COURSENO=" + Courseno +
             ",@P_SCHEMENO=" + Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"])) +
             ",@P_COLLEGE_ID=" + ViewState["college_id"] +
             ",@P_SECTIONNO=" + section +
             ",@P_COUSRENO=" + Courseno + "";
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "No data Found For the selection", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportDailyAttendenceCupKota(string reportTitle, string rptFileName) //Added By Tejas Thakre on 27_02_2023
    {
        try
        {

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            //int Courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            //int College_Id = Convert.ToInt32(ddlCollege.SelectedValue);
            int SubId = Convert.ToInt32(ddlSubjecttype.SelectedValue);
            int Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            string proc_ = "PKG_ATTENDANCE_CPUKA";
            string para_ = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_EXAM_NO,@P_SCHEMENO,@P_SECTIONNO,@P_PREV_STATUS,@P_SUBID,@P_COLLEGE_ID";
            string value_ = Convert.ToInt32(Sessionno) + "," + Convert.ToInt32(ViewState["degreeno"]) + "," + Convert.ToInt32(ViewState["branchno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlExamName.SelectedValue) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Sectionno + "," + Convert.ToInt32(0) + "," + SubId + "," + ViewState["college_id"];
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(proc_, para_, value_);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] +
                    ",@P_SESSIONNO=" + Convert.ToInt32(Sessionno) +
                    ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                    ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
                    ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) +
                    //",@P_COURSENO=" + Courseno +
                    ",@P_EXAM_NO=" + Convert.ToInt32(ddlExamName.SelectedValue) +
                    //",@P_EXAM_NO=" + Convert.ToInt32(ddlsubexamname.SelectedValue) + //Added by Injamam for Subexamvise // commented on 15-06-2023 by Injamam
                    ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) +
                    ",@P_SECTIONNO=" + Sectionno +
                    ",@P_PREV_STATUS=" + Convert.ToInt32(0) +
                    ",@P_SUBID=" + SubId +
                    ",@P_COLLEGE_ID=" + ViewState["college_id"] + "";
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "For Attendance Sheet Time Table Should be Created", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportDailyAttendenceMaher(string reportTitle, string rptFileName) //Added By Tejas Thakre on 27_02_2023
    {
        try
        {

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            //int Courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            //int College_Id = Convert.ToInt32(ddlCollege.SelectedValue);
            int SubId = Convert.ToInt32(ddlSubjecttype.SelectedValue);
            int Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            string proc_ = "PKG_ATTENDANCE_CPUKA";
            string para_ = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_EXAM_NO,@P_SCHEMENO,@P_SECTIONNO,@P_PREV_STATUS,@P_SUBID,@P_COLLEGE_ID";
            string value_ = Convert.ToInt32(Sessionno) + "," + Convert.ToInt32(ViewState["degreeno"]) + "," + Convert.ToInt32(ViewState["branchno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlExamName.SelectedValue) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Sectionno + "," + Convert.ToInt32(0) + "," + SubId + "," + ViewState["college_id"];
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(proc_, para_, value_);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] +
                    ",@P_SESSIONNO=" + Convert.ToInt32(Sessionno) +
                    ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                    ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) +
                    ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) +
                    //",@P_COURSENO=" + Courseno +
                    ",@P_EXAM_NO=" + Convert.ToInt32(ddlExamName.SelectedValue) +
                    //",@P_EXAM_NO=" + Convert.ToInt32(ddlsubexamname.SelectedValue) + //Added by Injamam for Subexamvise // commented on 15-06-2023 by Injamam
                    ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) +
                    ",@P_SECTIONNO=" + Sectionno +
                    ",@P_PREV_STATUS=" + Convert.ToInt32(0) +
                    ",@P_SUBID=" + SubId +
                    ",@P_COLLEGE_ID=" + ViewState["college_id"] +  ",@P_BATCHNO=" + Convert.ToInt32(ddlbatch.SelectedValue);
                   
                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "For Attendance Sheet Time Table Should be Created", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    DropDownList ddlSlot = e.Item.FindControl("ddlSlot") as DropDownList;
    //    if (ddlSlot.SelectedValue == "0")
    //    {
    //        ddlSlot.Enabled = false;
    //    }
    //}

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        //ddlsubexamname.Items.Clear();
        //ddlsubexamname.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjecttype.Items.Clear();
        ddlSubjecttype.Items.Add(new ListItem("Please Select", "0"));
        ddlbatch.Items.Clear();
        ddlbatch.Items.Add(new ListItem("Please Select", "0"));
        divbatch.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        ViewState["count"] = 0;
        if (ddlSession.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSubjecttype, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "R.SUBID");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        }
    }

    protected void ddlSubjecttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        //ddlSemester.Items.Clear();
        //ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName.Items.Clear();
        ddlExamName.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlbatch.Items.Clear();
        ddlbatch.Items.Add(new ListItem("Please Select", "0"));
        //ddlsubexamname.Items.Clear();
        //ddlsubexamname.Items.Add(new ListItem("Please Select", "0"));
        ViewState["count"] = 0;
        if (ddlSubjecttype.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlExamName, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue), "EXAMNAME");

            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SEC ON (SR.SECTIONNO=SEC.SECTIONNO)", " DISTINCT SR.SECTIONNO", "SEC.SECTIONNAME", "SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + ddlSemester.SelectedValue + "AND SR.SESSIONNO=" + ddlSession.SelectedValue + "AND ISNULL(SR.CANCEL,0)=0", "SEC.SECTIONNAME");

            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        }

        #region commented by Injamam  for Common
        //if (ddlSubjecttype.SelectedValue == "2" && Convert.ToInt32(Session["OrgId"]) == 6)// ADDED FOR RCPIPER 
        //{
        //    divbatch.Visible = true;
        //}
        //else if (Convert.ToInt32(Session["OrgId"]) == 16)// ADDED FOR MAHER BY INJAMAM ANSARI 
        //{
        //    //divbatch.Visible = true;
        //    int th_pr = Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "TH_PR", "SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue)));
        //    if (th_pr > 1)
        //    {
        //        divbatch.Visible = true;
        //    }
        //    else
        //    {
        //        ddlbatch.SelectedValue = "0";
        //        divbatch.Visible = false;
        //    }
        //}
        //else
        //{
        //    ddlbatch.SelectedValue = "0";
        //    divbatch.Visible = false;
        //}
        #endregion
        int batch = 0;
        string bat = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ISNULL(BATCH_TIMETABLE,0)", "");
        if (string.IsNullOrEmpty(bat)) { } else { batch = Convert.ToInt32(bat); }
        int th_pr = Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "TH_PR", "SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue)));
        if (th_pr > 1 && batch == 1)
        {
            divbatch.Visible = true;
            objCommon.FillDropDownList(ddlbatch, "ACD_SECTION A INNER JOIN ACD_BATCH B ON (A.SECTIONNO=B.SECTIONNO AND ISNULL(B.ACTIVESTATUS,0)=1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.BATCHNO=B.BATCHNO AND SR.SECTIONNO=A.SECTIONNO) ", " DISTINCT B.BATCHNO", "BATCHNAME", " BATCHNAME<>'' AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "BATCHNAME");
        }
        else
        {
            ddlbatch.SelectedValue = "0";
            divbatch.Visible = false;
        }

    }

    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        }
    }

    protected void btnViewLogin_Click(object sender, EventArgs e)
    {
        try
        {
            ExamController objExamController = new ExamController();
            CustomStatus cs = (CustomStatus)objExamController.GetViewOnStudentLock(Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSubjecttype.SelectedValue));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(updExamdate, "Record Update Successfully!", this.Page);
            }

        }
        catch (Exception EX)
        {

            throw;
        }

    }

    protected void ibtnEvalDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {

                    ImageButton ibtnEvalDelete = sender as ImageButton;
                    int exdtno = int.Parse(ibtnEvalDelete.CommandArgument);

                    //int retStatus = objExamController.DeleteTimeTable(exdtno);
                    int retStatus = objExamController.DeleteTimeTable(exdtno, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
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
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void TimeTableExcel()
    {
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
        //    objCommon.DisplayMessage("Record Not Found", this.Page);
        //}

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
        if (Convert.ToInt32(Session["OrgId"]) != 9)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(6)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(6)').hide();});", true);

        }
        this.TimeTableExcel();
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ViewState["count"] = 0;

    }

    protected void btnBranchWiserpt_Click(object sender, EventArgs e)
    {
        try
        {


            if ((Convert.ToInt32(Session["OrgId"]) == 6))
            {
                ShowBranchWiseTimeTable("Branch Wise Time Table Report", "rptTimeTableReportRCPIPER.rpt");
            }
            else
            {
                ShowBranchWiseTimeTableAllClient("Branch Wise Time Table Report", "rptTimeTableReportBranchwise.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCardReport.btnPrintReport_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowBranchWiseTimeTable(string reportTitle, string rptFileName) //Added By Tejas Thakre on 06-04-2023
    {
        try
        {

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Sessionno) +
                ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) +
                ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                ",@P_EXAM_NO=" + Convert.ToInt32(ddlExamName.SelectedValue) +
                ",@P_COLLEGE_ID=" + ViewState["college_id"] + "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowBranchWiseTimeTableAllClient(string reportTitle, string rptFileName) //Added By Injamam Ansari on 10-08-2023
    {
        try
        {

            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int Semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Sessionno) +
                ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) +
                ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                ",@P_EXAM_NO=" + Convert.ToInt32(ddlExamName.SelectedValue) +
                ",@P_COLLEGE_ID=" + ViewState["college_id"] +
                ",@P_COLLEGE_CODE=" + ViewState["college_id"] + "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Validation Added By Injamam Ansari
    private bool checkdate()
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
        return stat;
    }

    private bool checkblack()
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
                        checkbox();
                        return stat;
                    }
                    if (value == 0)
                    {
                        stat = false;
                        checkbox();
                        return stat;
                    }
                }
                catch (Exception)
                {
                    stat = false;
                    checkbox();
                    return stat;
                }
            }
        }
        checkbox();
        return stat;
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
                string elect = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + Convert.ToInt32(ccode)));
                foreach (ListViewDataItem row in lvCourse.Items)
                {
                    CheckBox chkBox1 = row.FindControl("chkAccept") as CheckBox;
                    if (chkBox1.Checked)
                    {
                        TextBox txtDate1 = row.FindControl("txtExamDate") as TextBox;
                        DropDownList dropslot1 = row.FindControl("ddlSlot") as DropDownList;
                        string ccode1 = (row.FindControl("lblCourseno") as Label).ToolTip;
                        string elect1 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + Convert.ToInt32(ccode1)));
                        if (elect == "True" && elect1 == "True") { }
                        else
                        {
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

        }
        return stat;
    }

    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList currentDropdown = (DropDownList)sender;
        ListViewItem currentItem = (ListViewItem)currentDropdown.NamingContainer;
        int currentIndex = currentItem.DisplayIndex;
        string selectedValue = currentDropdown.SelectedValue;
        string textdate = null;
        string elect1 = null;
        int course = 0;
        currentDropdown.Enabled = true;
        int slotno = Convert.ToInt32(currentDropdown.SelectedValue);
        string formattedDate = null;
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                TextBox txtDate = item.FindControl("txtExamDate") as TextBox;
                course = Convert.ToInt32((item.FindControl("lblCourseno") as Label).ToolTip);
                elect1 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + course + ""));
                textdate = txtDate.Text;
                DateTime date = DateTime.ParseExact(textdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                formattedDate = date.ToString("yyyy-MM-dd");
                txtDate.Enabled = true;
            }

        }
        //if (checkexisting(formattedDate, slotno, course) == false)
        //{
        //    currentDropdown.SelectedIndex = 0;
        //    checkbox();
        //    return;
        //}

        DataSet ds = objCommon.FillDropDown("ACD_EXAM_DATE", "COURSENO", "EXAMDATE,SLOTNO,SUBID", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + " AND EXAMDATE = '" + formattedDate + "' AND SLOTNO = " + slotno, "EXDTNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int dscourse = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSENO"]);
            int subid = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"]);
            string subjecttype = (objCommon.LookUp("ACD_SUBJECTTYPE", "SUBNAME", "SUBID=" + subid)).ToString();
            string elect2 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + dscourse + ""));
            string coursename = Convert.ToString(objCommon.LookUp("ACD_COURSE", "COURSE_NAME", "COURSENO=" + dscourse + ""));
            if ((course == dscourse && subid == Convert.ToInt32(ddlSubjecttype.SelectedValue)) || (subid == Convert.ToInt32(ddlSubjecttype.SelectedValue) && elect1 == elect2))
            {

            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "Same Date & Same Slot Already Exists For Course " + coursename + " In " + subjecttype + " Subject ", this.Page);
                currentDropdown.SelectedIndex = 0;
                checkbox();
                return;
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
                    int course1 = Convert.ToInt32((ROW.FindControl("lblCourseno") as Label).ToolTip);
                    string elect2 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + course1 + ""));
                    if (elect1 == "True" && elect2 == "True") { }
                    else
                    {
                        if (currentDropdown.Text == dropslot1.Text && textdate == txtDate1.Text)
                        {
                            objCommon.DisplayMessage(this.updExamdate, "Same Date & Same Slot Already Exists", this.Page);
                            currentDropdown.SelectedIndex = 0;
                            checkbox();
                            return;
                        }
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
        string elect1 = null;
        int slotno = 0;
        DateTime date = DateTime.ParseExact(selecteddate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string formattedDate = date.ToString("yyyy-MM-dd");
        int course = 0;
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                DropDownList dropslot = item.FindControl("ddlSlot") as DropDownList;
                course = Convert.ToInt32((item.FindControl("lblCourseno") as Label).ToolTip);
                elect1 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + course + ""));
                drpslot = dropslot.Text;
                slotno = Convert.ToInt32(dropslot.SelectedValue);
                dropslot.Enabled = true;
            }

        }

        DataSet ds = objCommon.FillDropDown("ACD_EXAM_DATE", "COURSENO", "EXAMDATE,SLOTNO,SUBID", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + " AND EXAMDATE = '" + formattedDate + "' AND SLOTNO = " + slotno, "EXDTNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int dscourse = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSENO"]);
            int subid = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"]);
            string subjecttype = (objCommon.LookUp("ACD_SUBJECTTYPE", "SUBNAME", "SUBID=" + subid)).ToString();
            string elect2 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + dscourse + ""));
            string coursename = Convert.ToString(objCommon.LookUp("ACD_COURSE", "COURSE_NAME", "COURSENO=" + dscourse + ""));
            if ((course == dscourse && subid == Convert.ToInt32(ddlSubjecttype.SelectedValue)) || (subid == Convert.ToInt32(ddlSubjecttype.SelectedValue) && elect1 == elect2))
            {

            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "Same Date & Same Slot Already Exists For Course " + coursename + " In " + subjecttype + " Subject ", this.Page);
                currentdate.Text = string.Empty;
                checkbox();
                return;
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
                    int course1 = Convert.ToInt32((ROW.FindControl("lblCourseno") as Label).ToolTip);
                    string elect2 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + course1 + ""));
                    if (elect1 == "True" && elect2 == "True") { }
                    else
                    {
                        if (drpslot == dropslot1.Text && selecteddate == txtDate1.Text)
                        {
                            objCommon.DisplayMessage(this.updExamdate, "Same Date & Same Slot Already Exists", this.Page);
                            currentdate.Text = string.Empty;
                            checkbox();
                            return;
                        }
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

    protected bool checkcourse()   //ADDED BY INJAMAM ANSARI FOR VALIDATION ON SUBMIT BUTTON FOR WITHOUT SELECTING ANY COURSE 
    {
        bool stat = true;
        int count = 0;
        foreach (ListViewDataItem ROW in lvCourse.Items)
        {
            CheckBox chkBox = ROW.FindControl("chkAccept") as CheckBox;
            if (chkBox.Checked)
            {
                count++;
            }
        }
        if (count == 0)
        {
            stat = false;
        }
        return stat;
    }

    protected bool checkexisting(string date, int slot, int courseno)
    {
        bool stat = true;
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_EXAM_DATE", "COURSENO", "EXAMDATE,SLOTNO,SUBID", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + " AND EXAMDATE = '" + date + "' AND SLOTNO = " + slot, "EXDTNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            int course = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSENO"]);
            int subid = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"]);
            string subjecttype = (objCommon.LookUp("ACD_SUBJECTTYPE", "SUBNAME", "SUBID=" + subid)).ToString();
            string elect1 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + course + ""));
            string elect2 = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "COURSENO=" + courseno + ""));
            if (course == courseno && subid == Convert.ToInt32(ddlSubjecttype.SelectedValue) && elect1 == elect2)
            {
                stat = true;
            }
            else
            {
                objCommon.DisplayMessage(this.updExamdate, "Same Date & Same Slot Already Exists For " + subjecttype + "", this.Page);
                stat = false;
            }
        }
        return stat;
    }
    #endregion

    #region for JECRC Exam Clash Report
    protected void btnClashExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            int degree = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + schemeno + ""));
            int branch = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + schemeno + ""));


            string proc_name = "PKG_ACD_EXAM_TIMETABLE_CLASHES_CC";
            string para_name = "@P_SESSIONNO,@P_DEGREENO,@P_SCHEMENO,@P_BRANCHNO,@P_SEMESTERNO";
            string call_values = "" + sessionno + "," + degree + "," + schemeno + "," + branch + "," + semesterno + "";
            DataSet dsExcel = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);

            GridView gv = new GridView();
            if (dsExcel != null && dsExcel.Tables.Count > 0 && dsExcel.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = dsExcel;
                gv.DataBind();
                string attachment = "attachment ; filename=Clash Time Table Report.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();
                objCommon.DisplayMessage("Record Not Found", this.Page);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

    protected void ddlExamName_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        btnViewLogin.Visible = false;
        btnSubmit.Visible = false;
        ViewState["count"] = 0;
        //objCommon.FillDropDownList(ddlsubexamname, "ACD_SUBEXAM_NAME", "DISTINCT SUBEXAMNO", "SUBEXAMNAME", "EXAMNO=" + Convert.ToInt32(ddlExamName.SelectedValue) + "AND SUBJECTTYPE=" + Convert.ToInt32(ddlSubjecttype.SelectedValue), "SUBEXAMNO");
        //objCommon.FillDropDownList(ddlsubexamname, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.SUBEXAMNO", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = " + Convert.ToInt32(ddlExamName.SelectedValue) + "  AND ISNULL(EP.ACTIVESTATUS,0) = 1 AND SE.SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjecttype.SelectedValue) + "", "SE.SUBEXAMNO");

    }

    protected void ddlsubexamname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        ViewState["count"] = 0;
    }

    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamName.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvCourse.Visible = false;
        btnSubmit.Visible = false;
        btnViewLogin.Visible = false;
    }

    #endregion Regular time table section end here

    #region Global Elective Course ADDED by Injamam Ansari
    protected void ddlSession1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubjecttype1.Items.Clear();
        ddlSubjecttype1.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
        if (ddlSession1.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlpattern, "ACD_EXAM_PATTERN EP INNER JOIN ACD_SCHEME S ON (EP.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_EXAM_NAME EN ON (EP.PATTERNNO=EN.PATTERNNO AND ISNULL(EN.ACTIVESTATUS,0) = 1) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT EP.PATTERNNO", "PATTERN_NAME", "EP.PATTERNNO > 0 AND ISNULL(EP.ACTIVESTATUS,0)=1 AND SESSIONID=" + Convert.ToInt32(ddlSession1.SelectedValue), "EP.PATTERNNO");
        }
        else
        {
            ddlSubjecttype1.Items.Clear();
            ddlSubjecttype1.Items.Add(new ListItem("Please Select", "0"));
            lvCourse1.Visible = false;
        }

    }

    protected void ddlpattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        objCommon.FillDropDownList(ddlSubjecttype1, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)  INNER JOIN ACD_SESSION_MASTER SM  ON (SM.SESSIONNO= R.SESSIONNO) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO=R.SCHEMENO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=SC.PATTERNNO)", " DISTINCT R.SUBID", "S.SUBNAME", "sm.SESSIONID=" + Convert.ToInt32(ddlSession1.SelectedValue) + "AND EP.PATTERNNO=" + Convert.ToInt32(ddlpattern.SelectedValue), "R.SUBID");
    }

    protected void ddlSubjecttype1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
        if (ddlSubjecttype1.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlExamName1, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO=S.SCHEMENO) INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND ISNULL(ACTIVESTATUS,0)=1 AND SUBID=" + Convert.ToInt32(ddlSubjecttype1.SelectedValue) + "AND S.PATTERNNO=" + Convert.ToInt32(ddlpattern.SelectedValue), "EXAMNAME");

        }

    }

    protected void ddlExamName1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;

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
            if (checkBlack1() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Please Select Date And Slot", this.Page);
                return;
            }
            if (checkDate1() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Invalid Date Format/Selected Date is previous Date than Today", this.Page);
                return;
            }
            if (Validate1() == false)
            {
                objCommon.DisplayMessage(updExamdate, "Same Date And Slot Already Exists", this.Page);
                return;
            }
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
                        int Modeexam = 0;
                        int sessionid = Convert.ToInt32(ddlSession1.SelectedValue);
                        objExam.Examdate = Convert.ToDateTime((dataitem.FindControl("txtExamDate") as TextBox).Text);
                        // CustomStatus cs = (CustomStatus)objExamController.AddExamDayElect(objExam, OrgID, Modeexam, ccode, sessionid);
                        CustomStatus cs = (CustomStatus)objExamController.AddExamDayElect(objExam, OrgID, Modeexam, ccode, sessionid, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(UpdatePanel2, "Exam Day(s) Saved Successfully!", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            objCommon.DisplayMessage(UpdatePanel2, "Exam Day(s) Updated Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(UpdatePanel2, "Error While Insert/Update", this.Page);
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
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnSubmit_Click1() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ddlSession1.SelectedIndex = 0;
        ddlSubjecttype1.Items.Clear();
        ddlSubjecttype1.Items.Add(new ListItem("Please Select", "0"));
        ddlExamName1.Items.Clear();
        ddlExamName1.Items.Add(new ListItem("Please Select", "0"));
        lvCourse1.DataSource = null;
        lvCourse1.DataBind();
        btnSubmit1.Enabled = false;
    }

    private void GetCourses1()
    {
        //if (Convert.ToInt32(Session["OrgId"]) != 9)
        //{

        //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#BatchTheory1').hide();$('td:nth-child(6)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#BatchTheory1').hide();$('td:nth-child(6)').hide();});", true);

        //}

        string pro_ = "PKG_GET_COURSE_ALL_GLOBALELE_COURSE_CC";
        string para = "@P_SESSIONID,@P_SUBJECTTYPE,@P_EXAMNO";
        string value = "" + Convert.ToInt16(ddlSession1.SelectedValue) + "," + Convert.ToInt16(ddlSubjecttype1.SelectedValue) + "," + Convert.ToInt16(ddlExamName1.SelectedValue) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(pro_, para, value);

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
            objCommon.DisplayMessage(this.UpdatePanel2, "Record Not Found", this.Page);
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
                        checkbox1();
                        return stat;
                    }
                    if (value == 0)
                    {
                        stat = false;
                        checkbox1();
                        return stat;
                    }
                }
                catch (Exception)
                {
                    stat = false;
                    checkbox1();
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
        //int slotno = Convert.ToInt32(currentDropdown.SelectedValue);
        //string formattedDate = null;
        //int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession1.SelectedValue)));
        foreach (ListViewDataItem item in lvCourse1.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                TextBox txtDate = item.FindControl("txtExamDate") as TextBox;
                textdate = txtDate.Text;
                //DateTime date = DateTime.ParseExact(textdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //formattedDate = date.ToString("yyyy-MM-dd");
                txtDate.Enabled = true;
            }

        }
        //int count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_DATE ED INNER JOIN ACD_COURSE C ON (C.COURSENO=ED.COURSENO)", "COUNT(EXDTNO)", "C.ELECT=0 AND GLOBALELE=0 AND EXAMDATE='" + formattedDate + "' AND SLOTNO=" + slotno + " AND ED.SESSIONNO=" + sessionno));
        //if (count > 0)
        //{
        //    objCommon.DisplayMessage(this.UpdatePanel2, "Same Date & Same Slot Already Exists", this.Page);
        //    currentDropdown.SelectedIndex = 0;
        //    checkbox1();
        //    return;
        //}
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
                        objCommon.DisplayMessage(this.UpdatePanel2, "Same Date & Same Slot Already Exists", this.Page);
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
        //int slotno = 0;
        //int course = 0;
        //DateTime date = DateTime.ParseExact(selecteddate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //string formattedDate = date.ToString("yyyy-MM-dd");
        //int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession1.SelectedValue)));
        foreach (ListViewDataItem item in lvCourse1.Items)
        {
            if (item.DisplayIndex == currentIndex)
            {
                DropDownList dropslot = item.FindControl("ddlSlot") as DropDownList;
                //slotno = Convert.ToInt32(dropslot.SelectedValue);
                drpslot = dropslot.Text;
                dropslot.Enabled = true;
            }

        }
        //int count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_DATE ED INNER JOIN ACD_COURSE C ON (C.COURSENO=ED.COURSENO)", "COUNT(EXDTNO)", "C.ELECT=0 AND GLOBALELE=0 AND EXAMDATE='" + formattedDate + "' AND SLOTNO=" + slotno + "AND ED.SESSIONNO=" + sessionno));
        //if (count > 0)
        //{
        //    objCommon.DisplayMessage(this.UpdatePanel2, "Same Date & Same Slot Already Exists", this.Page);
        //    currentdate.Text = string.Empty;
        //    checkbox1();
        //    return;
        //}
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
                        objCommon.DisplayMessage(this.UpdatePanel2, "Same Date & Same Slot Already Exists", this.Page);
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

    protected void ibtnEvalDelete1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int sessionid = Convert.ToInt32(ddlSession1.SelectedValue);
            int examno = Convert.ToInt32(ddlExamName1.SelectedValue);
            foreach (ListViewDataItem dataitem in lvCourse1.Items)
            {

                CheckBox chkBox = dataitem.FindControl("chkAccept") as CheckBox;

                if (chkBox.Checked)
                {

                    ImageButton ibtnEvalDelete1 = sender as ImageButton;
                    string ccode = (ibtnEvalDelete1.CommandArgument).ToString();
                    //int retStatus = Convert.ToInt32(objExamController.DeleteTimeTableElectiv_CC(ccode, sessionid, examno));
                    int retStatus = Convert.ToInt32(objExamController.DeleteTimeTableElectiv_CC(ccode, sessionid, examno, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString()));
                    if ((retStatus == 1) && (chkBox.Checked))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel2, "Time Table Cancelled Successfully", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel2, "Error in Cancelling Time Table", this.Page);
                    }
                    GetCourses1();
                    btnViewLogin1.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.UpdatePanel2, "Select CheckBox to delete Time Table", this.Page);
                    btnViewLogin1.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
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
            int examno = Convert.ToInt32(ddlExamName1.SelectedValue);
            string ccode = this.Getccode();
            CustomStatus cs = (CustomStatus)objExamController.GetViewOnStudentLock(sessionid, examno, ccode);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(UpdatePanel2, "Record Update Successfully!", this.Page);
            }

        }
        catch (Exception EX)
        {
            objCommon.DisplayMessage(UpdatePanel2, "Request Fail!", this.Page);
        }
    }

    private string Getccode()
    {
        string ccode = "";
        foreach (ListViewDataItem item in lvCourse1.Items)
        {
            CheckBox chk = item.FindControl("chkAccept") as CheckBox;
            Label lblCourseno = item.FindControl("lblCourseno") as Label;
            if (chk.Checked)
            {
                ccode += ((item.FindControl("lblCourseno")) as Label).ToolTip + ",";
            }
        }
        ccode = ccode.TrimEnd(',');
        return ccode;
    }

    #endregion

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
        string pro_ = "PKG_GET_COMMON_COURSE_TIMETABLE_CC";
        string para_ = "@P_SESSIONID,@P_SUBJECTTYPE,@P_EXAMNO,@P_COURSE_CATEGORY";
        string value = "" + Convert.ToInt16(ddlSession2.SelectedValue) + "," + Convert.ToInt16(ddlSubjecttype2.SelectedValue) + "," + Convert.ToInt16(ddlExamName2.SelectedValue) + "," + Convert.ToInt16(ddlcoursecat.SelectedValue);

        int section = 0;
        string sec = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ISNULL(SEC_TIMETABLE,0)", "");
        if (string.IsNullOrEmpty(sec)) { } else { section = Convert.ToInt32(sec); }
        divbatch.Visible = false;
        if (section == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#thsection').hide(); $('.tbl-panel2 table td:nth-child(4)').hide();$('#theadsection').hide(); $('.tbl-panel3 table td:nth-child(4)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thsection').hide(); $('.tbl-panel2 table td:nth-child(4)').hide();$('#theadsection').hide(); $('.tbl-panel3 table td:nth-child(4)').hide();});", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#thsection').show(); $('.tbl-panel2 table td:nth-child(4)').show();$('#theadsection').show(); $('.tbl-panel3 table td:nth-child(4)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thsection').show(); $('.tbl-panel2 table td:nth-child(4)').show();$('#theadsection').show(); $('.tbl-panel3 table td:nth-child(4)').show();});", true);
        }

        DataSet ds = objCommon.DynamicSPCall_Select(pro_, para_, value);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvtimetable.DataSource = ds;
            lvtimetable.DataBind();
            foreach (ListViewDataItem dataitem in lvtimetable.Items)
            {
                ListBox ddlschemelist = dataitem.FindControl("ddlschemelist") as ListBox;
                ListBox ddlsectionlist = dataitem.FindControl("ddlsectionlist") as ListBox;
                HiddenField hdn_schemeno = dataitem.FindControl("hdn_schemeno") as HiddenField;
                HiddenField hdn_section = dataitem.FindControl("hdn_section") as HiddenField;
                TableHeaderRow thsection1 = dataitem.FindControl("thsection1") as TableHeaderRow;

                DataSet ds1 = objCommon.FillDropDown("ACD_SCHEME  WITH (NOLOCK)", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO IN (" + hdn_schemeno.Value.ToString() + ")", "");
                DataSet ds2 = objCommon.FillDropDown("ACD_SECTION  WITH (NOLOCK)", "DISTINCT SECTIONNO", "SECTIONNAME", "SECTIONNO>0 AND (SECTIONNO IN (" + hdn_section.Value.ToString() + ") OR SECTIONNO=0)", "");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ddlschemelist.DataSource = ds1;
                    ddlschemelist.DataValueField = ds1.Tables[0].Columns[0].ToString();
                    ddlschemelist.DataTextField = ds1.Tables[0].Columns[1].ToString();
                    ddlschemelist.DataBind();
                }
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    ddlsectionlist.DataSource = ds2;
                    ddlsectionlist.DataValueField = ds2.Tables[0].Columns[0].ToString();
                    ddlsectionlist.DataTextField = ds2.Tables[0].Columns[1].ToString();
                    ddlsectionlist.DataBind();
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
                string sectionno = hdn_section.Value.ToString();
                for (int j = 0; j < sectionno.ToString().Length; j++)
                {
                    for (int k = 0; k < ddlsectionlist.Items.Count; k++)
                    {
                        if (sectionno.Contains(ddlsectionlist.Items[k].Value))
                        {
                            ddlsectionlist.Items[k].Selected = true;
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
            int subid = Convert.ToInt32(ddlSubjecttype2.SelectedValue);
            objExam.Exam_TT_Type = Convert.ToInt32(ddlExamName2.SelectedValue);
            foreach (ListViewDataItem items in lvcommoncourse.Items)
            {
                DropDownList ddlcommoncourse = items.FindControl("ddlcommoncourse") as DropDownList;
                ListBox ddlschemelist = items.FindControl("ddlschemelist") as ListBox;
                ListBox ddlsectionlist = items.FindControl("ddlsectionlist") as ListBox;
                DropDownList ddlSlot1 = items.FindControl("ddlSlot1") as DropDownList;
                TextBox txtExamDate1 = items.FindControl("txtExamDate1") as TextBox;
                string schemes = selectedMultipeScheme(ddlschemelist);
                string sectionno = selectedMultipesection(ddlsectionlist);
                string ccode = ddlcommoncourse.SelectedValue.ToString();
                objExam.Examdate = Convert.ToDateTime(txtExamDate1.Text.ToString());
                objExam.Slot = Convert.ToInt32(ddlSlot1.SelectedValue);
                //cs = (CustomStatus)objExamController.AddCommonCourseTimeTable(objExam, orgid, ccode, sessionid, subid, schemes, sectionno);
                cs = (CustomStatus)objExamController.AddCommonCourseTimeTable(objExam, orgid, ccode, sessionid, subid, schemes, sectionno, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
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
            HiddenField section = item.FindControl("hdn_section") as HiddenField;
            Label Date = item.FindControl("txtExamDate1") as Label;
            HiddenField slot = item.FindControl("hdf_slotno1") as HiddenField;
            stat = objExamController.GetViewOnStudentLock_CommonCourses(Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(ddlExamName2.SelectedValue), Convert.ToInt32(ddlSubjecttype2.SelectedValue), Convert.ToInt32(slot.Value), Convert.ToDateTime(Date.Text), ccode.Value, scheme.Value, section.Value);
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
                    dtNewTable.Columns.Add(new DataColumn("SECTIONNO", typeof(string)));
                    dtNewTable.Columns.Add(new DataColumn("SECTIONNAME", typeof(string)));

                    drCurrentRow = dtNewTable.NewRow();
                    drCurrentRow["CCODE"] = string.Empty;
                    drCurrentRow["COURSENAME"] = string.Empty;
                    drCurrentRow["SCHEME"] = string.Empty;
                    drCurrentRow["SCHEMENAME"] = string.Empty;
                    drCurrentRow["EXAMDATE"] = string.Empty;
                    drCurrentRow["SLOTNO"] = 0;
                    drCurrentRow["SLOT_NAME"] = string.Empty;
                    drCurrentRow["SECTIONNO"] = string.Empty;
                    drCurrentRow["SECTIONNAME"] = string.Empty;

                    for (int i = 0; i < lvcommoncourse.Items.Count; i++)
                    {
                        HiddenField hdfcourse = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdf_course");
                        HiddenField hdfslotno1 = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdf_slotno1");
                        HiddenField hdn_schemeno = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdn_schemeno");
                        HiddenField hdn_section = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdn_section");
                        DropDownList ddlcommoncourse = (DropDownList)lvcommoncourse.Items[rowIndex].FindControl("ddlcommoncourse");
                        ListBox ddlschemelist = (ListBox)lvcommoncourse.Items[rowIndex].FindControl("ddlschemelist");
                        ListBox ddlsectionlist = (ListBox)lvcommoncourse.Items[rowIndex].FindControl("ddlsectionlist");
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
                            hdn_section.Value = selectedMultipesection(ddlsectionlist);
                            hdfslotno1.Value = ddlSlot1.Text;
                            drCurrentRow = dtNewTable.NewRow();
                            drCurrentRow["CCODE"] = hdfcourse.Value;
                            drCurrentRow["COURSENAME"] = ddlcommoncourse.Text;
                            drCurrentRow["SCHEME"] = hdn_schemeno.Value;
                            drCurrentRow["SCHEMENAME"] = ddlschemelist.Text;
                            drCurrentRow["EXAMDATE"] = txtExamDate1.Text;
                            drCurrentRow["SLOTNO"] = hdfslotno1.Value;
                            drCurrentRow["SLOT_NAME"] = ddlSlot1.Text;
                            drCurrentRow["SECTIONNO"] = hdn_section.Value;
                            drCurrentRow["SECTIONNAME"] = ddlsectionlist.Text;
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
                    drCurrentRow["SECTIONNO"] = string.Empty;
                    drCurrentRow["SECTIONNAME"] = string.Empty;
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
        dt.Columns.Add(new DataColumn("SECTIONNO", typeof(string)));
        dt.Columns.Add(new DataColumn("SECTIONNAME", typeof(string)));

        dr = dt.NewRow();
        dr["CCODE"] = string.Empty;
        dr["COURSENAME"] = string.Empty;
        dr["SCHEME"] = string.Empty;
        dr["SCHEMENAME"] = string.Empty;
        dr["EXAMDATE"] = string.Empty;
        dr["SLOTNO"] = 0;
        dr["SLOT_NAME"] = string.Empty;
        dr["SECTIONNO"] = string.Empty;
        dr["SECTIONNAME"] = string.Empty;
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
            ListBox ddlsectionlist = dataitem.FindControl("ddlsectionlist") as ListBox;

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
                    HiddenField hdn_section = (HiddenField)lvcommoncourse.Items[rowIndex].FindControl("hdn_section");
                    DropDownList ddlcommoncourse = (DropDownList)lvcommoncourse.Items[rowIndex].FindControl("ddlcommoncourse");
                    DropDownList ddlSlot1 = (DropDownList)lvcommoncourse.Items[rowIndex].FindControl("ddlSlot1");
                    ListBox ddlschemelist = (ListBox)lvcommoncourse.Items[rowIndex].FindControl("ddlschemelist");
                    ListBox ddlsectionlist = (ListBox)lvcommoncourse.Items[rowIndex].FindControl("ddlsectionlist");
                    TextBox txtExamDate1 = (TextBox)lvcommoncourse.Items[rowIndex].FindControl("txtExamDate1");
                    ImageButton imgbtnrowremove = (ImageButton)lvcommoncourse.Items[rowIndex].FindControl("imgbtnrowremove");
                    ImageButton imgaddcourse = (ImageButton)lvcommoncourse.Items[rowIndex].FindControl("imgaddcourse");

                    hdf_course.Value = dt.Rows[i]["CCODE"].ToString();
                    hdn_schemeno.Value = dt.Rows[i]["SCHEME"].ToString();
                    hdf_slotno1.Value = dt.Rows[i]["SLOTNO"].ToString();
                    hdn_section.Value = dt.Rows[i]["SECTIONNO"].ToString();
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
                                ddlschemelist_SelectedIndexChanged(ddlschemelist, EventArgs.Empty);
                            }
                        }
                    }
                    string sectionnos = dt.Rows[i]["SECTIONNO"].ToString();
                    for (int l = 0; l < sectionnos.ToString().Length; l++)
                    {
                        for (int m = 0; m < ddlsectionlist.Items.Count; m++)
                        {
                            if (sectionnos.Contains(ddlsectionlist.Items[m].Value))
                            {
                                ddlsectionlist.Items[m].Selected = true;
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

    protected string selectedMultipesection(ListBox sectionlist)
    {
        string stat = "";
        foreach (ListItem item in sectionlist.Items)
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
            int count = 0;
            foreach (ListViewDataItem items in lvcommoncourse.Items)
            {
                DropDownList ddlcommoncourse = items.FindControl("ddlcommoncourse") as DropDownList;
                DropDownList ddlSlot1 = items.FindControl("ddlSlot1") as DropDownList;
                ListBox ddlschemelist = items.FindControl("ddlschemelist") as ListBox;
                ImageButton imgbtnrowremove = items.FindControl("imgbtnrowremove") as ImageButton;
                ImageButton imgaddcourse = items.FindControl("imgaddcourse") as ImageButton;
                objCommon.FillDropDownList(ddlcommoncourse, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON  (SR.CCODE=C.CCODE) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (ST.IDNO=SR.IDNO) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT C.CCODE ", "(C.CCODE +' - '+ COURSE_NAME) as COURSE_NAME", "ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(SR.REGISTERED,0)=1 AND ISNULL(SR.EXAM_REGISTERED,0)=1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession2.SelectedValue) + " AND SR.SUBID=" + Convert.ToInt32(ddlSubjecttype2.SelectedValue) + "AND (C.CATEGORYNO=" + Convert.ToInt32(ddlcoursecat.SelectedValue) + "OR 0=" + Convert.ToInt32(ddlcoursecat.SelectedValue) + ")", "C.CCODE");

                objCommon.FillDropDownList(ddlSlot1, "ACD_EXAM_TT_SLOT WITH (NOLOCK)", "SLOTNO", "SLOTNAME", "SLOTNO>0", "SLOTNO");
                if (ddlcommoncourse.Items.Count == 1 & count == 0)
                {
                    lvcommoncourse.DataSource = null;
                    lvcommoncourse.DataBind();
                    objCommon.DisplayMessage(updcommon, "No Courses Found for Given Selection", this.Page);
                }
                count++;
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
        HiddenField hdn_section = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdn_section");
        HiddenField hdf_slotno1 = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdf_slotno1");
        Label txtExamDate1 = (Label)lvtimetable.Items[rowIndex].FindControl("txtExamDate1");
        //int stat = objExamController.DeleteTimeTableCommonCourses(hdf_course.Value, Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(ddlExamName2.SelectedValue), Convert.ToInt32(ddlSubjecttype2.SelectedValue), hdn_schemeno.Value, Convert.ToDateTime(txtExamDate1.Text), Convert.ToInt32(hdf_slotno1.Value), hdn_section.Value);
        int stat = objExamController.DeleteTimeTableCommonCourses(hdf_course.Value, Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(ddlExamName2.SelectedValue), Convert.ToInt32(ddlSubjecttype2.SelectedValue), hdn_schemeno.Value, Convert.ToDateTime(txtExamDate1.Text), Convert.ToInt32(hdf_slotno1.Value), hdn_section.Value, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
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
        HiddenField hdn_section = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdn_section");
        HiddenField hdf_slotno1 = (HiddenField)lvtimetable.Items[rowIndex].FindControl("hdf_slotno1");
        Label txtExamDate1 = (Label)lvtimetable.Items[rowIndex].FindControl("txtExamDate1");
        ImageButton imgbtnrowremove = (ImageButton)lvtimetable.Items[rowIndex].FindControl("imgbtnrowremove");
        ImageButton imgaddcourse = (ImageButton)lvtimetable.Items[rowIndex].FindControl("imgaddcourse");
        foreach (ListViewDataItem item in lvcommoncourse.Items)
        {
            DropDownList ddlcommoncourse = item.FindControl("ddlcommoncourse") as DropDownList;
            DropDownList ddlSlot1 = item.FindControl("ddlSlot1") as DropDownList;
            ListBox ddlschemelist = item.FindControl("ddlschemelist") as ListBox;
            ListBox ddlsectionlist = item.FindControl("ddlsectionlist") as ListBox;
            TextBox txtExamDate = item.FindControl("txtExamDate1") as TextBox;
            HiddenField hdf_course1 = item.FindControl("hdf_course") as HiddenField;
            HiddenField hdn_schemeno1 = item.FindControl("hdn_schemeno") as HiddenField;
            HiddenField hdn_section1 = item.FindControl("hdn_section") as HiddenField;
            HiddenField hdf_slotno2 = item.FindControl("hdf_slotno1") as HiddenField;
            if (ddlSlot1.SelectedIndex == 0 && ddlcommoncourse.SelectedIndex == 0 && txtExamDate.Text == string.Empty && selectedScheme(ddlschemelist) == false)
            {
                hdf_course1.Value = hdf_course.Value;
                hdn_schemeno1.Value = hdn_schemeno.Value;
                hdn_section1.Value = hdn_section.Value;
                hdf_slotno2.Value = hdf_slotno1.Value;
                txtExamDate.Text = (Convert.ToDateTime(txtExamDate1.Text)).ToString();
                ddlcommoncourse.SelectedValue = hdf_course1.Value.ToString();
                ddlcommoncourse_SelectedIndexChanged(ddlcommoncourse, EventArgs.Empty);
                ddlSlot1.SelectedValue = hdf_slotno2.Value.ToString();
                string schemenos = hdn_schemeno1.Value.ToString();
                string sectionnos = hdn_section1.Value.ToString();

                for (int j = 0; j < schemenos.ToString().Length; j++)
                {
                    for (int k = 0; k < ddlschemelist.Items.Count; k++)
                    {
                        if (schemenos.Contains(ddlschemelist.Items[k].Value))
                        {
                            ddlschemelist.Items[k].Selected = true;
                            ddlschemelist_SelectedIndexChanged(ddlschemelist, EventArgs.Empty);
                        }
                    }
                }
                for (int j = 0; j < sectionnos.ToString().Length; j++)
                {
                    for (int k = 0; k < ddlsectionlist.Items.Count; k++)
                    {
                        if (sectionnos.Contains(ddlsectionlist.Items[k].Value))
                        {
                            ddlsectionlist.Items[k].Selected = true;
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
        dtNewTable.Columns.Add(new DataColumn("SECTIONNO", typeof(string)));
        dtNewTable.Columns.Add(new DataColumn("SECTIONNAME", typeof(string)));

        for (int i = 0; i < lvcommoncourse.Items.Count; i++)
        {

            HiddenField hdfcourse = (HiddenField)lvcommoncourse.Items[i].FindControl("hdf_course");
            HiddenField hdfslotno1 = (HiddenField)lvcommoncourse.Items[i].FindControl("hdf_slotno1");
            HiddenField hdn_schemeno = (HiddenField)lvcommoncourse.Items[i].FindControl("hdn_schemeno");
            HiddenField hdn_section = (HiddenField)lvcommoncourse.Items[i].FindControl("hdn_section");
            DropDownList ddlcommoncourse = (DropDownList)lvcommoncourse.Items[i].FindControl("ddlcommoncourse");
            ListBox ddlschemelist = (ListBox)lvcommoncourse.Items[i].FindControl("ddlschemelist");
            ListBox ddlsectionlist = (ListBox)lvcommoncourse.Items[i].FindControl("ddlsectionlist");
            DropDownList ddlSlot1 = (DropDownList)lvcommoncourse.Items[i].FindControl("ddlSlot1");
            TextBox txtExamDate1 = (TextBox)lvcommoncourse.Items[i].FindControl("txtExamDate1");
            hdfcourse.Value = ddlcommoncourse.Text;
            hdn_schemeno.Value = selectedMultipeScheme(ddlschemelist);
            hdn_section.Value = selectedMultipesection(ddlsectionlist);
            hdfslotno1.Value = ddlSlot1.Text;
            drCurrentRow = dtNewTable.NewRow();
            drCurrentRow["CCODE"] = hdfcourse.Value;
            drCurrentRow["COURSENAME"] = ddlcommoncourse.Text;
            drCurrentRow["SCHEME"] = hdn_schemeno.Value;
            drCurrentRow["SCHEMENAME"] = ddlschemelist.Text;
            drCurrentRow["EXAMDATE"] = txtExamDate1.Text;
            drCurrentRow["SLOTNO"] = hdfslotno1.Value;
            drCurrentRow["SLOT_NAME"] = ddlSlot1.Text;
            drCurrentRow["SECTIONNO"] = hdn_section.Value;
            drCurrentRow["SECTIONNAME"] = ddlsectionlist.Text;
            dtNewTable.Rows.Add(drCurrentRow);
        }
        ViewState["CurrentTable"] = dtNewTable;
    }

    protected void ddlschemelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string schemeno = "";
        foreach (ListViewDataItem listitem in lvcommoncourse.Items)
        {
            ListBox ddlschemelist = listitem.FindControl("ddlschemelist") as ListBox;
            ListBox ddlsectionlist = listitem.FindControl("ddlsectionlist") as ListBox;
            DropDownList ddlcommoncourse = listitem.FindControl("ddlcommoncourse") as DropDownList;
            foreach (ListItem items in ddlschemelist.Items)
            {
                schemeno += items.Value;
            }
            DataSet ds = null;
            ddlsectionlist.Items.Clear();
            if (schemeno != "")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION S ON (S.SECTIONNO=SR.SECTIONNO)", "DISTINCT SR.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO>0 AND SR.SCHEMENO IN (" + schemeno + ") AND SR.SESSIONNO IN (SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession2.SelectedValue) + ") AND SR.CCODE='" + ddlcommoncourse.SelectedValue + "' AND SR.SUBID=" + ddlSubjecttype2.SelectedValue, "SR.SECTIONNO");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HiddenField hdn_section = listitem.FindControl("hdn_section") as HiddenField;
                    ddlsectionlist.DataSource = ds;
                    ddlsectionlist.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlsectionlist.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlsectionlist.DataBind();
                    if (hdn_section.Value != "")
                    {
                        foreach (ListItem itm in ddlsectionlist.Items)
                        {
                            if (hdn_section.Value.Contains(itm.Value))
                            {
                                itm.Selected = true;
                            }
                        }
                    }
                }
            }
            schemeno = string.Empty;
        }
    }

    #endregion common Time Table End

}