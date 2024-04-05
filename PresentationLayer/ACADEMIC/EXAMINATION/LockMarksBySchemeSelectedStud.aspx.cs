//======================================================================================
// PROJECT NAME  : UAIMS[RAIPUR]                                                              
// MODULE NAME   : ACADAMIC
// PAGE NAME     : LOCK UNLOCK [EXAMINATION]
// CREATION DATE : 17 Jan 2012                                        
// CREATED BY    : PRIYANKA KABADE          
// MODIFIED DATE : 03-Mar-2015 
// MODIFIED BY   : PRITY KHANDAIT                                                                     
// MODIFIED DESC :                                                  
//=======================================================================================

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_LockMarksBySchemeSelectedStud : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

    #region Log variables
    //*** Added By Raju...below variable use to maintain mark entry unlock log**//
    string Logmobilenos = string.Empty;
    string LogEmail = string.Empty;
    string LogCCEmails = string.Empty;
    int Count = 0;
    //**end**//
    #endregion Log variables

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //TO SET THE MASTERPAGE
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CHECK SESSION
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //PAGE AUTHORIZATION
                //this.CheckPageAuthorization();

                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();


                //dynamic lable
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
                //------


                this.FillDropdownList();
                //LOAD PAGE HELP
                //if (Request.QueryString["pageno"] != null)
                //{
                //    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                //if ((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || (Session["usertype"].ToString() == "3") || (Session["usertype"].ToString() == "1") || Session["usertype"].ToString() == "4")
                //    this.FillDropdownList();
                //else
                //    Response.Redirect("~/notauthorized.aspx?page=LockMarksBySchemeSelectedStud.aspx");
            }
            //**added by Raju on date 06/03/2019** to maintain subject for log//
            ViewState["emailfrom"] = "";
            ViewState["smstext"] = "";
            ViewState["emailtext"] = "";

        }
    }
    #endregion Page Events

    #region Click Events
    protected void btnShow_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true; //added by Raju to enable  submit button         
        BindStudents(); //Added by Irfan Shaikh 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //SAVE THE LOCK UNLOCK MARK STATUS
        try
        {
            btnSave.Enabled = false; //added by Raju to disable submit button after one click
            UpdateLockStatus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksBySchemeSelectedStud.btnSave_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        // ddlBranch.SelectedIndex = 0;
        // ddlDegree.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        // ddlScheme.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlStuType.SelectedIndex = 0;
        ddlsub.SelectedIndex = 0;
        ddlSubExam1.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;

        pnlStudents.Visible = false;
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add("Please Select");
        ddlFac.Items.Clear();
        ddlFac.Items.Add("Please Select");
        spnNote.Visible = false;
        activityname.Visible = false;
        activitystart.Visible = false;
        activityend.Visible = false;
    }
    #endregion Click Events

    #region Other Events
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlsub.Items.Clear();
            ddlsub.Items.Add(new ListItem("Please Select", "0"));
            ddlExamType.Items.Clear();
            ddlExamType.Items.Add(new ListItem("Please Select", "0"));
            ddlSubExam1.Items.Clear();
            ddlSubExam1.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlStuType.SelectedIndex = 0;
            ddlFac.Items.Clear();
            ddlFac.Items.Add(new ListItem("Please Select", "0"));
            btnSave.Visible = false;
            lvStudList.DataSource = null;
            lvStudList.DataBind();

            if (ddlSession.SelectedIndex > 0)
            {
                ClearAllDropDowns();
                ClearPanel();


                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");

                ddlSemester.Focus();
               

                //ddlDegree.SelectedIndex = 0;
                // ddlBranch.Items.Clear();
                // ddlBranch.Items.Add(new ListItem("Please Select", "0"));
                //objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_NAME", "FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !=''", "EXAMNO");

                //objCommon.FillDropDownList(ddlsub, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", " SUBID IS NOT NULL AND SUBID !='' AND SUBID >0", "SUBNAME ASC");
                //string cnt = objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO = " + ddlSession.SelectedValue);
                //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND ODD_EVEN=" + cnt, "SEMESTERNO");
            }
            else
            {
                ddlSemester.SelectedIndex = 0;
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                activityname.Visible = false;
                activitystart.Visible = false;
                activityend.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksBySchemeSelectedStud.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ClearAllDropDowns();
    //        ClearPanel();
    //        ddlBranch.Items.Clear();
    //        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
    //        if (ddlDegree.SelectedIndex > 0)
    //        {
    //            //if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1")
    //            //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO =" + ddlDegree.SelectedValue + " AND DEPTNO = " + Session["userdeptno"].ToString(), "BRANCHNO");
    //            //else
    //            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO =" + ddlDegree.SelectedValue, "BRANCHNO");
    //            ddlBranch.Focus();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_LockMarksBySchemeSelectedStud.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ClearPanel();
    //        ClearAllDropDowns();
    //        if (ddlBranch.SelectedIndex > 0)
    //        {               
    //            objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", "B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.BRANCHNO =" + ddlBranch.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "SCHEMENO");
    //            ddlScheme.Focus();

    //            string cnt = objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO = " + ddlSession.SelectedValue);
    //            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND ODD_EVEN=" + cnt, "SEMESTERNO");                
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_LockMarksBySchemeSelectedStud.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlExamType.Items.Clear();
    //    ddlExamType.Items.Add(new ListItem("Please Select", "0"));
    //    ddlSection.Items.Clear();
    //    ddlStuType.SelectedIndex = 0;
    //    ddlSemester.SelectedIndex = 0;
    //    ddlSection.Items.Add(new ListItem("Please Select", "0"));
    //    ClearPanel();

    //    if (ddlScheme.SelectedIndex > 0)
    //    {
    //        #region Commented
    //        //Fill Semester
    //        //if(ddlBranch.SelectedValue == "99")
    //        //    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO IN (1,2)", "S.SEMESTERNAME");
    //        //else
    //        //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SR.SCHEMENO = " + ddlScheme.SelectedValue + "AND SR.SESSIONNO = " + ddlSession.SelectedValue + "AND SR.SEMESTERNO=S.SEMESTERNO AND SR.SEMESTERNO > 0", "S.SEMESTERNAME");

    //        // FILL EXAM AS PER SCHEME SELECTED.
    //        //objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_NAME", "FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !=''", "EXAMNO");
    //        #endregion Commented
    //    }
    //}

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlsub.Items.Clear();
            ddlsub.Items.Add(new ListItem("Please Select", "0"));
            ddlExamType.Items.Clear();
            ddlExamType.Items.Add(new ListItem("Please Select", "0"));
            ddlSubExam1.Items.Clear();
            ddlSubExam1.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlStuType.SelectedIndex = 0;
            ddlFac.Items.Clear();
            ddlFac.Items.Add(new ListItem("Please Select", "0"));

            ClearPanel();

            objCommon.FillDropDownList(ddlsub, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT SR ON S.SUBID = SR.SUBID", "DISTINCT S.SUBID", "SUBNAME", "S.SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1 AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "S.SUBID ");

            objCommon.FillDropDownList(ddlSection, "ACD_SECTION S INNER JOIN ACD_STUDENT_RESULT R ON R.SECTIONNO=S.SECTIONNO", "DISTINCT S.SECTIONNO", "SECTIONNAME", "R.SCHEMENO=" + "AND SEMESTERNO=" + ddlSemester.SelectedValue, "S.SECTIONNO");
            ddlSection.SelectedIndex = 0;
            ddlSection.Focus();
            ddlExamType.SelectedIndex = 0;
            ddlStuType.SelectedIndex = 0;
            ddlsub.Focus();
            activityname.Visible = false;
            activitystart.Visible = false;
            activityend.Visible = false;
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksBySchemeSelectedStud.ddlSemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearPanel();
        activityname.Visible = false;
        activitystart.Visible = false;
        activityend.Visible = false;
        //ddlExamType.SelectedIndex = 0;
    }

    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubExam1.Items.Clear();
        ddlSubExam1.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlStuType.SelectedIndex = 0;
        ddlFac.Items.Clear();
        ddlFac.Items.Add(new ListItem("Please Select", "0"));

        lvStudList.DataSource = null;
        lvStudList.DataBind();
        btnSave.Visible = false;
        //objCommon.FillDropDownList(ddlSubExam1, "ACD_EXAM_NAME A inner join Acd_subexam_name B on A.EXAMNO= B.EXAMNO  ", "DISTINCT A.EXAMNO", "B.SUBEXAMNAME", "B.FLDNAME LIKE '%" + ddlExamType.SelectedValue + "%' AND SUBEXAM_SUBID="+ddlsub.SelectedValue, "B.SUBEXAMNAME ASC");
        int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "PATTERNNO", "SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"])));
        if (ddlExamType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubExam1, "ACD_EXAM_NAME A inner join Acd_subexam_name B on A.EXAMNO= B.EXAMNO  ", "DISTINCT B.FLDNAME", "B.SUBEXAMNAME", "B.FLDNAME LIKE '%" + ddlExamType.SelectedValue + "%' AND ISNULL(B.ACTIVESTATUS,0)=1 AND A.PATTERNNO=" + patternno + " AND  SUBEXAM_SUBID=" + ddlsub.SelectedValue, "B.SUBEXAMNAME ASC");

            ddlSubExam1.Focus();
            GETSTATUSDATE();
        }
        else
        {
            activityname.Visible = false;
            activitystart.Visible = false;
            activityend.Visible = false;
        }
        //if (ddlExamType.SelectedIndex > 0)
        //{
        //    GETSTATUSDATE();
        //}
        //else
        //{

        //}
        //btnSave.Visible = false;
        //spnNote.Visible = false;
        //lvStudList.DataSource = null;
        //lvStudList.DataBind();
        //ddlFac.Items.Clear();
        //ddlFac.Items.Add("Please Select");

        //ddlCourse.Items.Clear();
        //ddlCourse.Items.Add("Please Select");
        //objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT", "DISTINCT COURSENO", "COURSENAME", "COURSENO > 0 AND ISNULL(CANCEL,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(PREV_STATUS,0)=" + Convert.ToInt32(ddlStuType.SelectedValue) + " AND ISNULL(SUBID,0)=" + Convert.ToInt32(ddlsub.SelectedValue), "COURSENO");
    }
    
    protected void ddlStuType_SelectedIndexChanged(object sender, EventArgs e)
    {
        // btnSave.Visible = false;
        // lvStudList.DataSource = null;
        // lvStudList.DataBind();
        // //ddlFac.Items.Clear();
        // //ddlFac.Items.Add("Please Select");
        //// ddlExamType.Items.Clear();
        //// ddlExamType.Items.Add("Please Select");
        // ddlsub.SelectedIndex = 0;
        // spnNote.Visible = false;
        btnSave.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        spnNote.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        ddlFac.Items.Clear();
        ddlFac.Items.Add(new ListItem("Please Select", "0"));

        ddlStuType.SelectedIndex = 0;

        //objCommon.FillDropDownList(ddlFac, "ACD_STUDENT_RESULT", "DISTINCT CASE WHEN SUBID=1 THEN ISNULL(UA_NO,0) ELSE ISNULL(UA_NO_PRAC,0) END FAC_NO", "(CASE WHEN DBO.FN_DESC('UA',CASE WHEN SUBID=1 THEN ISNULL(UA_NO,0) ELSE ISNULL(UA_NO_PRAC,0) END) IS NULL THEN '' ELSE DBO.FN_DESC('UA',CASE WHEN SUBID=1 THEN ISNULL(UA_NO,0) ELSE ISNULL(UA_NO_PRAC,0) END) END) AS FACULTYNAME", "COURSENO > 0 AND ISNULL(CANCEL,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "FACULTYNAME ASC");
        objCommon.FillDropDownList(ddlFac, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SUBJECTTYPE [as] ON SR.SUBID = [as].SUBID", "DISTINCT CASE WHEN [as].TH_PR=1 THEN ISNULL(UA_NO,0) ELSE ISNULL(UA_NO_PRAC,0) END FAC_NO", "(CASE WHEN DBO.FN_DESC('UA',CASE WHEN [as].TH_PR=1 THEN ISNULL(UA_NO,0) ELSE ISNULL(UA_NO_PRAC,0) END) IS NULL THEN '' ELSE DBO.FN_DESC('UA',CASE WHEN [as].TH_PR=1 THEN ISNULL(UA_NO,0) ELSE ISNULL(UA_NO_PRAC,0) END) END) AS FACULTYNAME", "COURSENO > 0 AND ISNULL(CANCEL,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "FACULTYNAME ASC");
    }

    protected void ddlFac_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        spnNote.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
    }

    protected void ddlsub_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlExamType.Items.Clear();
        ddlExamType.Items.Add(new ListItem("Please Select", "0"));
        ddlSubExam1.Items.Clear();
        ddlSubExam1.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlStuType.SelectedIndex = 0;
        ddlFac.Items.Clear();
        ddlFac.Items.Add(new ListItem("Please Select", "0"));

        btnSave.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        //ddlFac.Items.Clear();
        //ddlFac.Items.Add("Please Select");
        //ddlExamType.Items.Clear();
        //ddlExamType.Items.Add("Please Select");
        //ddlCourse.Items.Clear();
        //ddlCourse.Items.Add("Please Select");
        spnNote.Visible = false;

        int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "PATTERNNO", "SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"])));

        if (ddlsub.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_NAME E INNER JOIN ACD_SUBEXAM_NAME SN ON(E.EXAMNO=SN.EXAMNO)", "DISTINCT E.FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !='' AND E.PATTERNNO=" + patternno + " AND ISNULL(E.ACTIVESTATUS,0)=1 AND  SUBEXAM_SUBID=" + ddlsub.SelectedValue, "EXAMNAME ASC");
            // DataSet ds = objCommon.FillDropDown("", "", "", "", "");
            ddlExamType.Focus();

        }
        else
        {
            activityname.Visible = false;
            activitystart.Visible = false;
            activityend.Visible = false;
        }
        //if (Convert.ToInt32(ddlsub.SelectedValue) > 0 && Convert.ToInt32(ddlsub.SelectedValue) == 1)
        //    objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_NAME", "FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !='' AND EXAMNO IN (1,2,6)", "EXAMNAME ASC");
        //else
        //    objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_NAME", "FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !=''  AND EXAMNO NOT IN (1,2,6)", "EXAMNAME ASC");
    }
    #endregion Other Events

    #region User Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LockMarksBySchemeSelectedStud.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LockMarksBySchemeSelectedStud.aspx");
        }
    }

    private void FillDropdownList()
    {
        //Fill Dropdown session 
        //   objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 ", "SESSIONNO desc"); //--AND FLOCK = 1

        //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING LM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (LM.OrganizationId = DB.OrganizationId AND LM.DEGREENO = DB.DEGREENO AND LM.BRANCHNO = DB.BRANCHNO AND LM.COLLEGE_ID = DB.COLLEGE_ID) ", "COSCHNO", "COL_SCHEME_NAME", "LM.COLLEGE_ID IN(" + Session["college_nos"] + ")AND COSCHNO>0 AND LM.COLLEGE_ID > 0 AND LM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");

        //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER", " COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME ASC");


        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");



        //objCommon.FillDropDownList(ddlsub, "ACD_SUBJECTTYPE ", "SUBID", "SUBNAME", "SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1  ", "SUBID ");



        //if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1")
        //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE d inner JOIN ACD_BRANCH B ON (D.DEGREENO = B.DEGREENO)", "D.DEGREENO", "DEGREENAME", "D.DEGREENO > 0 AND  B.DEPTNO = " + Session["userdeptno"].ToString(), "D.DEGREENO");
        //else
        //    if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "4")
        //        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

        ddlStuType.Items.Clear();
        ddlStuType.Items.Add(new ListItem("Please Select", "-1"));
        ddlStuType.Items.Add(new ListItem("Regular", "0"));
        ddlStuType.Items.Add(new ListItem("Backlog", "1"));
        ddlStuType.Items.Add(new ListItem("Revaluation", "2"));
        ddlStuType.Items.Add(new ListItem("PhotoCopy", "3"));
        ddlStuType.Items.Add(new ListItem("Substitute Student", "4"));
    }

    private void ClearAllDropDowns()
    {
        //ddlScheme.Items.Clear();
        //ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        //ddlExamType.Items.Clear();
        //ddlExamType.Items.Add(new ListItem("Please Select", "0"));

    }

    private void ClearPanel()
    {
        ddlFac.Items.Clear();
        ddlFac.Items.Add("Please Select");
        ddlExamType.Items.Clear();
        ddlExamType.Items.Add("Please Select");
        ddlStuType.SelectedIndex = 0;
        ddlsub.SelectedIndex = 0;
        pnlStudents.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        lvStudList.Visible = false;
        divMsg.InnerHtml = string.Empty;
        btnSave.Visible = false;
        spnNote.Visible = false;
        activityname.Visible = false;
        activitystart.Visible = false;
        activityend.Visible = false;
    }

    private void BindStudents()
    {
        try
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            int branchno = Convert.ToInt32(ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString());
            int schemeno = Convert.ToInt32(ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString());

            DataSet dsStudList = null;
            int subid = 0;
            if (Convert.ToInt16(ddlCourse.SelectedValue) > 0 && Convert.ToInt16(ddlFac.SelectedValue) > 0)
            {
                subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "DISTINCT SUBID", "COURSENO=" + Convert.ToInt16(ddlCourse.SelectedValue)));
            }
            else
            {
                objCommon.DisplayMessage(this, "Please select course and faculty.", this);
            }

            //dsStudList = objMarksEntry.GetStudentsForUnlock(schemeno, Convert.ToInt32(ddlSession.SelectedValue), branchno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), subid, Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlFac.SelectedValue), ddlExamType.SelectedValue.ToString());


            if (Convert.ToInt32(ddlStuType.SelectedValue) >= 2)
            {
                dsStudList = objMarksEntry.GetStudentsForUnlock_for_reval(schemeno, Convert.ToInt32(ddlSession.SelectedValue), branchno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), subid, Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlFac.SelectedValue), ddlExamType.SelectedValue.ToString(), ddlSubExam1.SelectedValue.ToString());
            }
            else
            {

                dsStudList = objMarksEntry.GetStudentsForUnlock(schemeno, Convert.ToInt32(ddlSession.SelectedValue), branchno, Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), subid, Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlFac.SelectedValue), ddlExamType.SelectedValue.ToString(), ddlSubExam1.SelectedValue.ToString());
            }


            if (dsStudList.Tables.Count > 0 && dsStudList.Tables[0].Rows.Count > 0)
            {
                #region data present
                lvStudList.DataSource = dsStudList;
                lvStudList.DataBind();

                spnNote.Visible = true;
                pnlStudents.Visible = true;
                lvStudList.Visible = true;

                int j = 0;
                foreach (ListViewItem Item in lvStudList.Items)
                {
                    Label lblLockStatus = Item.FindControl("lblLockStatus") as Label;

                    CheckBox chk = Item.FindControl("chklckStud") as CheckBox;

                    if (dsStudList.Tables[0].Rows[j]["LOCK"].ToString() == "LOCK")     //LOCKS'1,2,3,4,5,6,EXEM' CHECKING THE LOCK STATUS FOR SELECTED EXAM TYPE
                    {
                        chk.Checked = true;                          //CHECKBOX SELECTED IF LOCK = 1;
                        lblLockStatus.Text = "Lock";                   //IF LOCK = 1;                        
                    }
                    else
                    {
                        chk.Checked = false;                         //CHECKBOX NOT SELECTED IF LOCK = 0;
                        lblLockStatus.Text = "Unlock";                 //IF LOCK = 0;
                    }
                    j++;                                               //TO GET NEXT ROW
                }
                btnSave.Visible = true;
                #endregion data present
            }
            else
            {
                lvStudList.DataSource = null;
                lvStudList.DataBind();
                objCommon.DisplayMessage(this, "No Records Found!", this.Page);
                btnSave.Visible = false;
                spnNote.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_LockMarksBySchemeSelectedStud.BindStudents() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void UpdateLockStatus()
    {
        //SAVE THE LOCK STATUS FOR EACH ROW
        try
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString());
            int branchno = Convert.ToInt32(ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString());
            int schemeno = Convert.ToInt32(ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString());
            int sem = Convert.ToInt32(ddlSemester.SelectedValue);
            int prev_status = Convert.ToInt32(ddlStuType.SelectedValue);
            int uano = Convert.ToInt32(Session["userno"]);////For log info
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            string examType = ddlExamType.SelectedValue.ToString();
            string sub_exam_type = ddlSubExam1.SelectedValue.ToString();
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            string idnoS = "";
            string lockS = "";

            int j = 0;
            foreach (ListViewItem Item in lvStudList.Items)
            {
                Label lblLockStatus = Item.FindControl("lblLockStatus") as Label;
                Label lblRegNo = Item.FindControl("lblRegNo") as Label;
                CheckBox chk = Item.FindControl("chklckStud") as CheckBox;

                if ((chk.Checked == false && lblLockStatus.Text == "Lock"))  ////(chk.Checked == true && lblLockStatus.Text == "Unlock") || for Locking    
                {
                    idnoS = idnoS + lblRegNo.ToolTip + "$";
                    lockS = lockS + (Convert.ToInt32(chk.Checked).ToString() == "1" ? "1" : "0") + "$";
                }
                j++;    //TO GET NEXT ROW
            }

            idnoS = idnoS.TrimEnd('$');//To remove the last $ sign
            lockS = lockS.TrimEnd('$');//To remove the last $ sign

            if (string.IsNullOrEmpty(idnoS) && string.IsNullOrEmpty(lockS))
            {
                objCommon.DisplayMessage(pnlStudents, "Selected students mark are already unlocked.", this);
                BindStudents();
                return;
            }

            //return;

            int retStat = -99;
            string unlocked_success = string.Empty;
            btnSave.Enabled = false;  //added by Raju to disable submit button after one click

            if (Convert.ToInt32(ddlStuType.SelectedValue) >= 2)
            {
                retStat = objMarksEntry.ToggleMarkEntryLockByAdmin_Reval(idnoS, lockS, sessionno, degreeno, branchno, schemeno, sem, courseno, prev_status, uano, examType, sub_exam_type, ipAddress); //SAVE ALL THE DETAILS IN THE DATABASE
            }
            else
            {

                retStat = objMarksEntry.ToggleMarkEntryLockByAdmin(idnoS, lockS, sessionno, degreeno, branchno, schemeno, sem, courseno, prev_status, uano, examType, sub_exam_type, ipAddress); //SAVE ALL THE DETAILS IN THE DATABASE
            }
            if (retStat == 1)
            {
                objCommon.DisplayMessage(this, "Unlock Done Successfully for selected student(s).", this);
                // SendemailandSms(); //added by Raju ..caller**for send sms and email after mark entry unlock
                if (Count > 0)// added by Raju ..check condition,if mark entry unlock then maintain log.
                {
                    //int log = objMarksEntry.InsertLogUnlockMark(ViewState["smstext"].ToString(), ViewState["emailtext"].ToString(), LogEmail, ViewState["emailfrom"].ToString(), LogCCEmails, Logmobilenos.TrimEnd(','), Convert.ToInt32(Session["userno"].ToString()), (Request.ServerVariables["REMOTE_HOST"]).ToString());
                }
                BindStudents();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksBySchemeSelectedStud.UpdateLockStatus --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    // private void SendemailandSms()  //added by Raju** collee**to send sms and email after mark entry unlock
    //foreach (ListViewDataItem dataRow in lvCourse.Items)
    //{
    //    string CC_Email = string.Empty;
    //    string CC_Mobilenos = string.Empty;
    //    string ccode = ((Label)dataRow.FindControl("lblCourseNo")).Text;
    //    string courseno = ((Label)dataRow.FindControl("lblCourseName")).ToolTip;
    //    string CourseName = ((Label)dataRow.FindControl("lblCourseName")).Text;
    //    string CourseCode = ((Label)dataRow.FindControl("lblCourseNo")).Text;
    //    bool Current_Check_Value = ((CheckBox)dataRow.FindControl("chklock")).Checked;
    //    bool Prev_Hdn_Value = Convert.ToBoolean(((HiddenField)dataRow.FindControl("hdnCheckBox")).Value);
    //    if (Prev_Hdn_Value.ToString() == "True")
    //    {
    //        if (Current_Check_Value != Prev_Hdn_Value)
    //        {
    //            Count++;
    //            string ua_no = ((Label)dataRow.FindControl("lblFaculty")).ToolTip;
    //            DataSet Faculty_Details = objCommon.FillDropDown("USER_ACC", "UA_TYPE", "UA_FULLNAME,UA_EMAIL,UA_MOBILE", "UA_NO=" + ua_no, ""); // AlertsNo=2 (UNLOCK) 
    //            string ua_email = Faculty_Details.Tables[0].Rows[0]["UA_EMAIL"].ToString();
    //            string ua_mobile = Faculty_Details.Tables[0].Rows[0]["UA_MOBILE"].ToString();
    //            string ua_FullName = Faculty_Details.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
    //            DataSet Additional_UaNo = objCommon.FillDropDown("ACD_ALERT_STATUS", "isnull(AlertsNo,0)AlertsNo", "isnull(SEND_THROUGH,0)SEND_THROUGH,confirm_alert", "AlertsNo=2 and confirm_alert=1", "SEND_THROUGH"); // AlertsNo=2 (UNLOCK)
    //            if (Additional_UaNo != null && Additional_UaNo.Tables[0].Rows.Count > 0)
    //            {
    //                DataRow[] result = (Additional_UaNo.Tables[0].Select("SEND_THROUGH=1 and confirm_alert=1"));
    //                if (result != null && result.Length > 0)
    //                {
    //                    DataSet Additional_Emails = objCommon.FillDropDown("ACD_ALERT_USER_DETAILS UD INNER JOIN USER_ACC UA ON (UD.UA_NO = UA.UA_NO)", "AlertsNo", "UD.UA_NO,UA_EMAIL,UA_MOBILE", "Send_Through=1 and AlertsNo=2", ""); // AlertsNo=2 (UNLOCK)    
    //                    if (Additional_Emails.Tables[0].Rows.Count > 0)
    //                    {
    //                        for (int i = 0; i < Additional_Emails.Tables[0].Rows.Count; i++)
    //                        {
    //                            CC_Email += Additional_Emails.Tables[0].Rows[i]["UA_EMAIL"].ToString() + ",";
    //                        }
    //                        CC_Email = CC_Email.TrimEnd(',');
    //                        MessageBody(ua_FullName, CC_Email, ua_email, CourseName, CourseCode);
    //                    }
    //                }
    //                DataRow[] resultsms = (Additional_UaNo.Tables[0].Select("SEND_THROUGH=2 and confirm_alert=1"));
    //                if (resultsms != null && resultsms.Length > 0)
    //                {
    //                    DataSet Additional_sms = objCommon.FillDropDown("ACD_ALERT_USER_DETAILS UD INNER JOIN USER_ACC UA ON (UD.UA_NO = UA.UA_NO)", "AlertsNo", "ud.UA_NO,UA_EMAIL,UA_MOBILE", "Send_Through=2 and AlertsNo=2", ""); // AlertsNo=2 (UNLOCK)    
    //                    if (Additional_sms.Tables[0].Rows.Count > 0)
    //                    {
    //                        for (int i = 0; i < Additional_sms.Tables[0].Rows.Count; i++)
    //                        {
    //                            CC_Mobilenos += Additional_sms.Tables[0].Rows[i]["UA_MOBILE"].ToString() + ",";
    //                        }
    //                        //this.SendSMS(Faculty_Details.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear " + Faculty_Details.Tables[0].Rows[0]["UA_FULLNAME"].ToString() + " Mark entry successfully unlocked for Session :-" + ddlSession.SelectedItem.Text + ", Exam Name :-" + ddlExamType.SelectedItem.Text + " and Course :-" + CourseCode + '-' + CourseName + " ");
    //                        int smsStatus = objCommon.SENDMSG_PASS(Faculty_Details.Tables[0].Rows[0]["UA_MOBILE"].ToString(), "Dear " + Faculty_Details.Tables[0].Rows[0]["UA_FULLNAME"].ToString() + " Mark entry successfully unlocked for Session :-" + ddlSession.SelectedItem.Text + ", Exam Name :-" + ddlExamType.SelectedItem.Text + " and Course :-" + CourseCode + '-' + CourseName + " ");
    //                        CC_Mobilenos = CC_Mobilenos.TrimEnd(',');
    //                        string[] stringArray = (CC_Mobilenos).Split(',');
    //                        foreach (var i in stringArray)
    //                        {
    //                            //this.SendSMS(i, "Dear " + Faculty_Details.Tables[0].Rows[0]["UA_FULLNAME"].ToString() + " Mark entry successfully unlocked for Session :-" + ddlSession.SelectedItem.Text + ", Exam Name :-" + ddlExamType.SelectedItem.Text + " and Course :-" + CourseCode + '-' + CourseName + " ");
    //                            smsStatus = objCommon.SENDMSG_PASS(i, "Dear " + Faculty_Details.Tables[0].Rows[0]["UA_FULLNAME"].ToString() + " Mark entry successfully unlocked for Session :-" + ddlSession.SelectedItem.Text + ", Exam Name :-" + ddlExamType.SelectedItem.Text + " and Course :-" + CourseCode + '-' + CourseName + " ");
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    // }

    //Below method added by Raju to create message template
    public void MessageBody(string FullName, string CC_Email, string Email, string course_name, string CourseCode)
    {
        const string EmailTemplate = "<html><body>" +
                              "<div align=\"center\">" +
                              "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                               "<tr>" +
                               "<td>" + "</tr>" +
                               "<tr>" +
                              "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                              "</tr>" +
                              "</table>" +
                              "</div>" +
                              "</body></html>";
        StringBuilder mailBody = new StringBuilder();
        mailBody.AppendFormat("<h1>Greetings !!</h1>");
        mailBody.AppendFormat("Dear <b>{0}</b>,", FullName);   //b
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<br />");
        //   mailBody.AppendFormat("<p>Mark entry successfully saved for Session</p>", ddlSession.SelectedItem.Text + " and Course :-" + lblCourse.Text.Substring(0, lblCourse.Text.IndexOf('~')) + "</b>");    //b

        mailBody.AppendFormat("Mark entry successfully Unlocked for Session <b>{0}</b>", ddlSession.SelectedItem.Text);       //b

        mailBody.AppendFormat(" and Course :- <b>{0}</b>", CourseCode + '-' + course_name);       //b             

        //  mailBody.AppendFormat(" - <b>{0}</b>", course_name);
        mailBody.AppendFormat("<br /><br />This is an auto generated response to your email,Please do not reply to this email as it will not be received.For any discrepancy you may write to us at " + "<b>" + " mis.srinagar@iitms.co.in." + "</b><br />");   //bb              

        mailBody.AppendFormat("<br />Regards,<br />");   //bb
        mailBody.AppendFormat("MIS Team<br /><br />");   //bb                   
        string Mailbody = mailBody.ToString();
        string nMailbody = EmailTemplate.Replace("#content", Mailbody);
        string CCemail = CC_Email;
        ViewState["emailtext"] = "Mark entry successfully Unlocked for Session" + ddlSession.SelectedItem.Text + "and Course :-" + CourseCode + '-' + course_name;
        sendEmail(nMailbody, Email, "Regarding Mark Entry Unlock", CCemail);

    }

    //Below method added by Raju to send email
    public int sendEmail(string message, string emailid, string subject, string CCemail) //send email method
    {
        int ret = 0;
        try
        {
            string[] stringArray = (CCemail.ToString()).Split(',');

            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD,FASCILITY", string.Empty, string.Empty);
            string emailfrom = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            string emailpass = Common.DecryptPassword(dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString());
            int fascility = Convert.ToInt32(dsconfig.Tables[0].Rows[0]["FASCILITY"].ToString());

            string smtpAddress = "smtp.gmail.com";
            int port = 587;

            if (emailfrom.ToLower().Contains("gmail.com"))
            {
                smtpAddress = "smtp.gmail.com";
            }
            else if (emailfrom.ToLower().Contains("yahoo.com") || emailfrom.ToLower().Contains("yahoo.in") || emailfrom.ToLower().Contains("yahoo.co.in"))
            {
                smtpAddress = "smtp.mail.yahoo.com";
            }
            else if (emailfrom.ToLower().Contains("live.com") || emailfrom.ToLower().Contains("hotmail.com"))
            {
                smtpAddress = "smtp.live.com";
            }

            ///Added By Mr.Manish Walde on 19Nov2015
            if (fascility == 1 || fascility == 3)
            {
                if (emailfrom != "" && emailpass != "")
                {
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = smtpAddress, // smtp server address here...
                        Port = port,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(emailfrom, emailpass),
                        Timeout = 30000
                    };
                    MailMessage mailmsg = new MailMessage(emailfrom, emailid, subject, message);
                    foreach (var i in stringArray)
                    {
                        mailmsg.CC.Add(i);
                    }
                    LogEmail = emailid;
                    LogCCEmails = CCemail;
                    ViewState["emailfrom"] = emailfrom;
                    mailmsg.IsBodyHtml = true;
                    ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true; // add by roshan 15-03-2017
                    smtp.Send(mailmsg);
                    if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                    {
                        return ret = 1;
                        //Storing the details of sent email



                    }
                }
                return ret = 0;
            }
            return ret = 0;
        }
        catch (Exception ex)
        {
            return ret = 0;
        }

    }

    public void SendSMS(string Mobile, string text)  //added by Raju.. send sms method
    {

        string status = "";
        try
        {
            string Message = string.Empty;

            DataSet ds = objCommon.FillDropDown("Reff", "SMSSVCID", "SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + "www.SMSnMMS.co.in/sms.aspx" + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
                if (status != "" && status != "0")
                {
                    Logmobilenos += Mobile + ',';
                    ViewState["smstext"] = text;
                }
            }
            else
            {
                status = "0";
            }
        }
        catch
        {

        }
    }

    #endregion User Methods

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlsub.Items.Clear();
        ddlsub.Items.Add(new ListItem("Please Select", "0"));
        ddlExamType.Items.Clear();
        ddlExamType.Items.Add(new ListItem("Please Select", "0"));
        ddlSubExam1.Items.Clear();
        ddlSubExam1.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlStuType.SelectedIndex = 0;
        ddlFac.Items.Clear();
        ddlFac.Items.Add(new ListItem("Please Select", "0"));

        btnSave.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        spnNote.Visible = false;
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(FLOCK,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");


                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", " SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                ddlSession.Focus();
               


            }
            
        }
        else
        {
            activityname.Visible = false;
            activitystart.Visible = false;
            activityend.Visible = false;
        }

    }
    protected void ddlSubExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        spnNote.Visible = false;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        ddlFac.Items.Clear();
        ddlFac.Items.Add("Please Select");

        ddlStuType.SelectedIndex = 0;
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add("Please Select");
        if (ddlSubExam1.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT", "DISTINCT COURSENO", "COURSENAME", "COURSENO > 0 AND ISNULL(CANCEL,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(SUBID,0)=" + Convert.ToInt32(ddlsub.SelectedValue), "COURSENAME ASC");
        }
       // GETSTATUSDATE();
        //objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT","DISTINCT COURSENO", "COURSENAME", "COURSENO > 0 AND ISNULL(CANCEL,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "COURSENAME ASC");

    }
    public void GETSTATUSDATE()
    {
       // objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_NAME E INNER JOIN ACD_SUBEXAM_NAME SN ON(E.EXAMNO=SN.EXAMNO)", "DISTINCT E.FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !='' AND E.PATTERNNO=" + patternno + " AND ISNULL(E.ACTIVESTATUS,0)=1 AND  SUBEXAM_SUBID=" + ddlsub.SelectedValue, "EXAMNAME ASC");
       // ViewState["schemeno"]
        string patternno = objCommon.LookUp("acd_scheme", "patternno", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]));
       // int Examno = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_NAME E INNER JOIN ACD_SUBEXAM_NAME SN ON(E.EXAMNO=SN.EXAMNO)", "DISTINCT E.Examno", "EXAMNAME IS NOT NULL AND EXAMNAME !='' AND E.PATTERNNO=" + patternno + " AND ISNULL(E.ACTIVESTATUS,0)=1 AND  SUBEXAM_SUBID=" + ddlsub.SelectedValue));
        //DataSet ds = objCommon.FillDropDown("ACD_EXAM_NAME E INNER JOIN ACD_SUBEXAM_NAME SN ON(E.EXAMNO=SN.EXAMNO)", "DISTINCT E.FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !='' AND E.PATTERNNO=" + patternno + " AND ISNULL(E.ACTIVESTATUS,0)=1 AND  SUBEXAM_SUBID=" + ddlsub.SelectedValue, "EXAMNAME ASC");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ViewState["FLDNAME"] = ds.Tables[0].Rows[0]["FLDNAME"].ToString();
        //}
        //"
       // B.FLDNAME LIKE
       int sub= Convert.ToInt32(ddlsub.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlsub.SelectedValue);
      // string subname = Convert.ToString(ddlExamType.SelectedValue) == System.DBNull.Value ? System.DBNull.Value : Convert.ToString(ddlExamType.SelectedValue);
        string subname = Convert.ToString(ddlExamType.SelectedValue) == "0" ? "" : Convert.ToString(ddlExamType.SelectedValue);
       int Examno = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_NAME E INNER JOIN ACD_SUBEXAM_NAME SN ON(E.EXAMNO=SN.EXAMNO)", "DISTINCT E.Examno", "EXAMNAME IS NOT NULL AND EXAMNAME !='' AND E.PATTERNNO=" + patternno + " AND ISNULL(E.ACTIVESTATUS,0)=1 AND E.FLDNAME like  '%" + subname + "%' AND  SUBEXAM_SUBID=" + sub));

        DataSet DateSessionDs = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO) INNER JOIN ACCESS_LINK l ON(L.al_no IN (SELECT VALUE FROM DBO.SPLIT(page_link,',') WHERE VALUE <>''))", "SA.SESSION_NO", "substring(cast(SA.START_DATE as varchar),1,12)START_DATE,substring(cast(SA.END_DATE as varchar),1,12)END_DATE,ACTIVITY_NAME as ACTIVITYNAME", "COLLEGE_IDS in (" + ViewState["college_id"] + ") AND SESSION_NO like '%" + Convert.ToInt32(ddlSession.SelectedValue) + "%' AND EXAMNO=" + Examno + " AND AL_LINK LIKE '%MARK%' AND SEMESTER like '%'  + CAST('" + Convert.ToInt32(ddlSemester.SelectedValue) + "' AS NVARCHAR(5))  +'%'", "");
        //sessionno = Session["currentsession"].ToString();
        if (DateSessionDs.Tables.Count > 0)
        {
            if (DateSessionDs.Tables[0].Rows.Count > 0)
            {
                string sessionno = DateSessionDs.Tables[0].Rows[0]["SESSION_NO"].ToString();
                lblActivity.Text = DateSessionDs.Tables[0].Rows[0]["ACTIVITYNAME"].ToString();
                lblstart.Text = DateSessionDs.Tables[0].Rows[0]["START_DATE"].ToString();
                lblEnd.Text = DateSessionDs.Tables[0].Rows[0]["END_DATE"].ToString();
                ViewState["sessionno"] = sessionno;
                activityname.Visible = true;
                activitystart.Visible = true;
                activityend.Visible = true;
            }
            else
            {
                activityname.Visible = false;
                activitystart.Visible = false;
                activityend.Visible = false;
               // objCommon.DisplayMessage(this.Page, "Activity Not Created.", this.Page);
                //return false;
            }

        }
       
        

    }
}
