
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using BusinessLogicLayer.BusinessLogic;


public partial class ACADEMIC_UnlockM : System.Web.UI.Page
{
     Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    CourseController objCourse = new CourseController();
    int Userno;
                string email ; 
                string Subject;
                string message;

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
            ClearAllDropDowns();
            ClearPanel();
            ddlDegree.SelectedIndex = 0;
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            //CHECK SESSION
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //PAGE AUTHORIZATION
                 this.CheckPageAuthorization();

                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();

                //LOAD PAGE HELP
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // if ((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || (Session["usertype"].ToString() == "3") || (Session["usertype"].ToString() == "1")) //Commented by Nikhil V.Lambe
                if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8") //Added By Nikhil V.Lambe on 24/02/2021 for page should be access by Admin and HOD.
                    this.FillDropdownList();
                else
                    Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
            }
        }
    }
    #endregion

    #region Click Events

    protected void btnShow_Click(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        BindExam();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //SAVE THE LOCK UNLOCK MARK STATUS
        try
        {

            if (txtremark.Text == string.Empty || txtremark.Text == "")                //added by prafull on dt 13042023
            {
                objCommon.DisplayMessage(this.updpnlExam, "Please Enter the Remark.", this);
                return;
            }


            foreach (ListViewDataItem dataRow in lvCourse.Items)
            {
                CheckBox chk = ((CheckBox)dataRow.FindControl("chklock"));
                if (chk.Checked == false && chk.Enabled == true)
                {
                    UpdateLockStatus(dataRow); 
                    if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                       GETEMAIL();
                    }
                    else
                    {
                       
                    }
                   

                }//UPDATE ONE BY ONE LOCK STATUS OF RECORD FOR COURSE
            }
            objCommon.DisplayMessage(this.updpnlExam, "Lock/Unlock Done Successfully.", this);
            BindExam();
            
            //string text = " Dear " + ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString() + "," + "<br>" + Session["OTP"].ToString() + " is your Identification Code to lock marks for " + "END SEM" + " Exam of " + course + " Course.";
             //string text="Dear Lalit Gaikwad I hope you all Best ";
             //ViewState["sms_text"] = text;
             //string mob="8554910155";
             //this.SendSMS(mob, text);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.btnSave_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void ClearDropDown()
    {
        ddlSession.SelectedIndex = 0;
        // ddlCollege.SelectedIndex = 0;
        // ddlDegree.SelectedIndex = 0;
        // ddlBranch.SelectedIndex = 0;
        // ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;

        ddlStuType.SelectedIndex = -1;
        ddlClgname.SelectedIndex = 0;

        btnSave.Visible = false;
        lvCourse.Visible = false;
        divSubExamType.Visible = false;
        rfvSubExamType.Enabled = false;
        ddlSubType.SelectedIndex = 0;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlSession.SelectedIndex = 0;

        //ddlSemester.SelectedIndex = 0;
        //ddlExamType.SelectedIndex = 0;

        //ddlStuType.SelectedIndex = -1;
        //ddlClgname.SelectedIndex = 0;

        //btnSave.Visible = false;
        //lvCourse.Visible = false;
        //divSubExamType.Visible = false;
        //rfvSubExamType.Enabled = false;
        //ddlSubType.SelectedIndex = 0;
        Response.Redirect(Request.Url.ToString());
        // ClearDropDown();
    }

    #endregion

    #region Other Events


    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAllDropDowns();
            ClearPanel();

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            if (ddlCollege.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.COLLEGE_ID = " + ddlCollege.SelectedValue + "AND CDB.UGPGOT IN (" + Session["ua_section"] + ")", "D.DEGREENAME");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE CD WITH (NOLOCK) ON (D.DEGREENO=CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB WITH (NOLOCK) ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.COLLEGE_ID = " + ddlCollege.SelectedValue, "D.DEGREENAME");
                ddlBranch.Focus();
            }
            ddlSubType.SelectedIndex = 0;
            ddlExamType.SelectedIndex = 0;
            divSubExamType.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlCollege_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearAllDropDowns();
            ClearPanel();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue.ToString() + " AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
                ddlBranch.Focus();
            }
            ddlSubType.SelectedIndex = 0;
            ddlExamType.SelectedIndex = 0;
            divSubExamType.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearPanel();
            ClearAllDropDowns();
            if (ddlBranch.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", "B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.BRANCHNO =" + ddlBranch.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "SCHEMENO");
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_SCHEME S WITH (NOLOCK) ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "S.SCHEMENAME DESC");
                ddlScheme.Focus();
            }
            ddlSubType.SelectedIndex = 0;
            ddlExamType.SelectedIndex = 0;
            divSubExamType.Visible = false;
            ddlStuType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //ddlSemester.Items.Clear();
    //        //ddlSemester.Items.Add(new ListItem("Please Select", "0"));

    //        //ddlStuType.SelectedIndex = 0;

    //        //ClearPanel();

    //        //if (ddlScheme.SelectedIndex > 0)
    //        //{
    //        // objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SR.SCHEMENO = " + ddlScheme.SelectedValue + "AND SR.SEMESTERNO=S.SEMESTERNO AND SR.SEMESTERNO > 0", "S.SEMESTERNAME");
    //        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
    //        //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "EXAMNO");
    //        //}
    //        pnlStudents.Visible = false;
    //        lvCourse.Visible = false;
    //        ddlSubType.SelectedIndex = 0;
    //        ddlExamType.SelectedIndex = 0;
    //        divSubExamType.Visible = false;
    //        ddlStuType.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearPanel();
        ddlExamType.SelectedIndex = 0;
    }

    #endregion

    #region User Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
        }
    }

    private void FillDropdownList()
    {
        //Fill Dropdown session 
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK = 1 ", "SESSIONNO desc"); //--AND FLOCK = 1




        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc"); //--AND FLOCK = 1





        // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 ", "C.COLLEGE_ID");

        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");


        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");

        objCommon.FillDropDownList(ddlSubType, "ACD_SUBJECTTYPE ", "SUBID", "SUBNAME", "SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1  ", "SUBID ");

        ddlStuType.Items.Clear();
        ddlStuType.Items.Add(new ListItem("Please Select", "-1"));
        ddlStuType.Items.Add(new ListItem("Regular", "0"));
        ddlStuType.Items.Add(new ListItem("Backlog", "1"));
    }

    private void ClearAllDropDowns()
    {
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
    }

    private void ClearPanel()
    {
        pnlStudents.Visible = false;
        lvCourse.Visible = false;
        divMsg.InnerHtml = string.Empty;
        btnSave.Visible = false;
    }

    private void UpdateLockStatus(ListViewDataItem dataRow)
    {
        //SAVE THE LOCK STATUS FOR EACH ROW
        try
        {
            //int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
            int CollegeId = Convert.ToInt32(ViewState["college_id"]);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int semester = Convert.ToInt32(ddlSemester.SelectedValue);
            // int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
            int examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            string courseno = ((Label)dataRow.FindControl("lblCourseNo")).ToolTip;
            string section = ((Label)dataRow.FindControl("lblsection")).ToolTip;
            string ccode = ((Label)dataRow.FindControl("lblCourseNo")).Text;

            string remark = txtremark.Text;

            CheckBox chk = ((CheckBox)dataRow.FindControl("chklock"));

            if (chk.Checked == false && chk.Enabled == true)
            {

                //COMMENTED ON 14-02-2020 BY VAISHALI
                //string facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                //string facultynopractical = ((Label)dataRow.FindControl("lblfacultypractical")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblfacultypractical")).ToolTip.ToString();

                // added on 14-02-2020 by Vaishali
                string facultynotheory = string.Empty, facultynopractical = string.Empty;


                if (Convert.ToInt32(Session["OrgId"]) == 2)               //added by prafull on dt 03-02-2023 for crescent
                {
                    if (ddlSubType.SelectedValue == "1")
                    {
                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        facultynopractical = "0";
                    }
                    else if (ddlSubType.SelectedValue == "2" || ddlSubType.SelectedValue == "4" || ddlSubType.SelectedValue == "7" || ddlSubType.SelectedValue == "12" || ddlSubType.SelectedValue == "15" || ddlSubType.SelectedValue == "17")
                    {
                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        facultynopractical = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        //facultynotheory = "0";
                    }
                    else
                    {
                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        facultynopractical = "0";
                    }
                }                         //from here For other Client
                else
                {
                    if (ddlSubType.SelectedValue == "1")
                    {
                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        facultynopractical = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString(); ;
                    }
                    else if (ddlSubType.SelectedValue == "2" || ddlSubType.SelectedValue == "11" || ddlSubType.SelectedValue == "12")
                    {
                        facultynopractical = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString(); ;
                    }
                    else if (ddlSubType.SelectedValue == "3")
                    {
                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        facultynopractical = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString(); ;
                    }
                    else if (ddlSubType.SelectedValue == "4")
                    {
                        facultynopractical = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();

                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        // facultynotheory = "0";
                    }
                    else
                    {
                        facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                        facultynopractical = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString(); ;
                    }
                }

                //string facultyno = string.Empty;
                //if (facultynotheory == "" && facultynopractical != "")
                //{
                //    facultyno = facultynopractical;
                //}
                //else if (facultynotheory != "" && facultynopractical == "")
                //{
                //    facultyno = facultynotheory;
                //}
                //else if (facultynotheory != "" && facultynopractical != "")
                //{
                //    facultyno = facultynotheory;
                //}
                //else if (facultynotheory == "" && facultynopractical == "")
                //{
                //    facultyno = "0";
                //}
                //else
                //{
                //    facultyno = facultynotheory;
                //}

                int lockunlock = 0;
                //string opid = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();            
                bool lockunlock_status = ((CheckBox)dataRow.FindControl("chklock")).Checked; //LOCK UNLOCK STATUS
                if (lockunlock_status == true)
                {
                    lockunlock = 1;
                }
                else
                    lockunlock = 0;
                string ipAddress = Request.ServerVariables["REMOTE_HOST"];
                //objMarksEntry.LockUnLockMarkEntryByAdmin(SessionNo, semester, schemeno, examtype, Convert.ToInt32(courseno), Convert.ToInt32(section), Convert.ToInt32(facultynotheory), Convert.ToInt32(facultynopractical), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"])); //SAVE ALL THE DETAILS IN THE DATABASE

                //added on 14-02-2020 by Vaishali
                //*DataSet dsmarkcount = objMarksEntry.GetMarkEntryNotDonecount(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue),Convert.ToInt32(courseno), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue.Trim() : string.Empty);
                DataSet dsmarkcount = objMarksEntry.GetMarkEntryNotDonecount(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(courseno), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue.Trim() : string.Empty, Convert.ToInt32(section));

                if (lockunlock_status == true)
                {
                    if (dsmarkcount != null && dsmarkcount.Tables[0].Rows.Count > 0)
                    {
                        if (dsmarkcount.Tables[0].Rows[0]["count"].ToString() != "0")
                        {
                            objCommon.DisplayMessage(this.updpnlExam, "Mark Entry not done for course " + ccode + " !!!!", this);
                            return;
                        }
                        else
                            objMarksEntry.LockUnLockMarkEntryByAdmin(SessionNo, semester, schemeno, examtype, Convert.ToInt32(courseno), Convert.ToInt32(section), Convert.ToInt32(facultynotheory), Convert.ToInt32(facultynopractical), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"]), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue : string.Empty, remark); //modified on 13-02-2020 by Vaishali    //added by prafull on dt 13042023 for unlocking remark
                    }
                    else
                        objMarksEntry.LockUnLockMarkEntryByAdmin(SessionNo, semester, schemeno, examtype, Convert.ToInt32(courseno), Convert.ToInt32(section), Convert.ToInt32(facultynotheory), Convert.ToInt32(facultynopractical), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"]), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue : string.Empty, remark); //modified on 13-02-2020 by Vaishali   //added by prafull on dt 13042023 for unlocking remark
                }
                else
                    objMarksEntry.LockUnLockMarkEntryByAdmin(SessionNo, semester, schemeno, examtype, Convert.ToInt32(courseno), Convert.ToInt32(section), Convert.ToInt32(facultynotheory), Convert.ToInt32(facultynopractical), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"]), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue : string.Empty, remark); //modified on 13-02-2020 by Vaishali     //added by prafull on dt 13042023 for unlocking remark
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.UpdateLockStatus --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowMarkListReport(string reportTitle, string rptFileName, string courseno, string ccode, string section, string uano, string examname)
    {
        try
        {
            int degreeno = Convert.ToInt16(ddlDegree.SelectedValue);
            string degreename = ddlDegree.SelectedItem.Text;
            string sessionname = ddlSession.SelectedItem.Text;
            string branchname = ddlBranch.SelectedItem.Text;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",@P_TERM=" + sessionname + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_UA_NO=" + uano + ",@P_COURSENO=" + courseno + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=" + section + ",@P_EXAM=" + examname;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ShowMarkListReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindExam()
    {
        try
        {
            DataSet dsCourse = null;

            //dsCourse = objCourse.GetCoursesForLockUnlock(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue));
            //*dsCourse = objCourse.GetCoursesForLockUnlock(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue.Trim() : string.Empty);
            //Convert.ToInt32(ViewState["schemeno"])
            //ViewState["degreeno"] = 
            //  ViewState["branchno"] = 
            // ViewState["college_id"]= 
            // ViewState["schemeno"] = 

            dsCourse = objCourse.GetCoursesForLockUnlock(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue.Trim() : string.Empty);
            //dsCourse = objCourse.GetCoursesForLockUnlock(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue));

            if (dsCourse.Tables.Count > 0 && dsCourse.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsCourse;
                lvCourse.DataBind();

                //added on 14-02-2020 by Vaishali
                if (ddlSubType.SelectedValue == "1")
                    (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name - Theory";
                else if (ddlSubType.SelectedValue == "2")
                    (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name - Laboratory";
                else if (ddlSubType.SelectedValue == "4")
                    (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name - Lab Integrated Theory";
                else
                    (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name";
                foreach (ListViewItem item in lvCourse.Items)
                {
                    Label lblactinestatus = item.FindControl("lblFaculty") as Label;

                    //Label lblactinestatus_practical = item.FindControl("lblfacultypractical") as Label;  //commented on  14-02-2020 by Vaishali
                    //if (lblactinestatus.Text == "" && lblactinestatus_practical.Text == "")  //commented on  14-02-2020 by Vaishali
                    if (lblactinestatus.Text == "")
                    {
                        lblactinestatus.Text = "Faculty Not Assigned";
                        //lblactinestatus_practical.Text = "Faculty Not Assigned"; //commented on  14-02-2020 by Vaishali
                        lblactinestatus.Style.Add("color", "Brown");
                        //lblactinestatus_practical.Style.Add("color", "Brown"); //commented on  14-02-2020 by Vaishali
                    }
                    //commented on  14-02-2020 by Vaishali
                    //else if (lblactinestatus.Text == "" && lblactinestatus_practical.Text != "")
                    //{
                    //    lblactinestatus.Text = "---";
                    //    lblactinestatus.Style.Add("color", "Green");
                    //}
                    //else if (lblactinestatus_practical.Text == "" && lblactinestatus.Text != "")
                    //{
                    //    lblactinestatus_practical.Text = "---";
                    //    lblactinestatus_practical.Style.Add("color", "Green");
                    //}
                }
                pnlStudents.Visible = true;
                lvCourse.Visible = true;
                btnSave.Visible = true;
                //int j = 0;
                //foreach (ListViewItem Item in lvCourse.Items)
                //{
                //    //LinkButton lbtnComplete = Item.FindControl("lbtnComplete") as LinkButton;
                //    //LinkButton lbtnInComplete = Item.FindControl("lbtnInComplete") as LinkButton;
                //    Label lblComplete = Item.FindControl("lblComplete") as Label;
                //    Label lblInComplete = Item.FindControl("lblInComplete") as Label;
                //    CheckBox chk = Item.FindControl("chklock") as CheckBox;

                //    int check = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_MARKS_ENTRY_OPERTOR O ON (A.OPID=O.OPID)", "COUNT(A.OPID)", "A.OPID=" + (dsCourse.Tables[0].Rows[j]["opid"].ToString())));

                //    if (check != 0)
                //    {                     
                //        lblComplete.Visible = true;                      //MARK ENTRY STAUTS COMPLETED
                //        lblInComplete.Visible = false;
                //        chk.Enabled =true ;     
                //    }
                //    else
                //    {

                //        lblComplete.Visible = false;
                //        lblInComplete.Visible = true;
                //        chk.Enabled = false;    
                //        //MARK ENTRY STAUTS INCOMPLETE
                //    }
                //    if (dsCourse.Tables[0].Rows[j]["LOCK"].ToString() == "True")     //LOCKS'1,2,3,4,5,6,EXEM' CHECKING THE LOCK STATUS FOR SELECTED EXAM TYPE
                //    {
                //        chk.Checked = true;                             //CHECKBOX SELECTED IF LOCK = 1;

                //    }
                //    else
                //    {
                //        chk.Checked = false;                           //CHECKBOX NOT SELECTED IF LOCK = 0;
                //                   //MARK ENTRY STAUTS INCOMPLETE
                //    }
                //    j++;                                                //TO GET NEXT ROW
                //}                
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(this.updpnlExam, "No Records Found!", this.Page);
                btnSave.Visible = false;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic__LockUnlockMarkEntry.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void GETEMAIL()
    {
        User_AccController objUC = new User_AccController();
        UserAcc objUA = new UserAcc();

        string CodeStandard = objCommon.LookUp("Reff", "CODE_STANDARD", "");
        string issendgrid = objCommon.LookUp("Reff", "SENDGRID_STATUS", "");

        //ds = objCommon.FillDropDown("ACD_MEETING_SCHEDULE A INNER JOIN   ", "AGENDATITAL,VENUE", "CONTENT_DETAILS", "VENUEID", "");
        //objCommon.FillDropDown("ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_VENUE V ON(V.PK_VENUEID=A.VENUEID)", "A.PK_AGENDA, A.AGENDANO, A.AGENDATITAL", "A.MEETINGDATE, A.MEETINGTIME,A.MEETINGTOTIME,V.VENUE,A.CONTENT_DETAILS", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "PK_AGENDA");

        string loginurl = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();
        int countparent = 0;
        //int countpartcheck = 0;
        
        foreach (ListViewDataItem item in lvCourse.Items)
        {
            countparent++;
            System.Web.UI.WebControls.CheckBox chkID = item.FindControl("chklock") as System.Web.UI.WebControls.CheckBox;
            System.Web.UI.WebControls.HiddenField hdfid = item.FindControl("hidStudentId") as System.Web.UI.WebControls.HiddenField;
            System.Web.UI.WebControls.Label lblcourse = item.FindControl("lblPstudent") as System.Web.UI.WebControls.Label;
            System.Web.UI.WebControls.Label lblSECTION = item.FindControl("lblSEC") as System.Web.UI.WebControls.Label;
            //CheckBox chk = item.FindControl("chklock") as CheckBox;
            //HiddenField hdfchk = item.FindControl("hidStudentId") as HiddenField;



            if (chkID.Checked == false && chkID.Enabled == true)
            {
                DataSet ds;
                //ds = objCommon.FillDropDown("ACD_MEETING_SCHEDULE A INNER JOIN ACD_MEETING_VENUE V ON(V.PK_VENUEID=A.VENUEID)", "A.PK_AGENDA, A.AGENDANO, A.AGENDATITAL", "A.MEETINGDATE, A.MEETINGTIME,A.MEETINGTOTIME,V.VENUE,A.CONTENT_DETAILS", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + "", "PK_AGENDA");
                ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME,UA_EMAIL,UA_MOBILE", "UA_NO=" + Convert.ToInt32(hdfid.Value) + "", "");
                string Emailid=ds.Tables[0].Rows[0]["UA_EMAIL"].ToString();
                countparent++;
                string FULLNAME = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                string MOBILENO = ds.Tables[0].Rows[0]["UA_MOBILE"].ToString();
               // string CONTENT_DETAILS = ds.Tables[0].Rows[0]["CONTENT_DETAILS"].ToString();
                string SUB=lblcourse.Text;
               
                string MESSAGEID = " marks is unlocked, if you are not unlock then contact the ADMIN immediately.";

                System.Web.UI.WebControls.Label lblFatherMobile = item.FindControl("lblFatherMobile") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblPstudent = item.FindControl("lblPstudent") as System.Web.UI.WebControls.Label;
                System.Web.UI.WebControls.Label lblPwd = item.FindControl("lblPreg") as System.Web.UI.WebControls.Label;
                email =Emailid; 
                Subject = CodeStandard + "Regarding Lock/Unlock Marks";
                message = "Dear " + FULLNAME + "<br />";
                message = message + "Your :" + SUB + "<br />";
                message = message + "SESSION:" +ddlSession.SelectedItem.Text+ "<br />";
                message = message + "SEMESTER :" +ddlSemester.SelectedItem.Text + "<br />";
                message = message + "SECTION :" + lblSECTION.Text + "<br />";
                message = message + "Subject" + MESSAGEID + "<br />";


                string subjectText = string.Empty;
                string templateText = string.Empty;
                int TemplateTypeId = 7;
                int TemplateId = 6;

                DataSet ds_mstQry = objUC.GetEmailTemplateConfigData(TemplateTypeId, TemplateId, 0, "43654587575");

                if (ds_mstQry != null && ds_mstQry.Tables.Count == 3)
                {
                    //ds_mstQry.Tables[0]====> Return DATAFIELDDISPLAY,DATAFIELD
                    //ds_mstQry.Tables[1]====> Return EMAILSUBJECT,TEMPLATETEXT
                    //ds_mstQry.Tables[2]====> Return USER_REGISTRATION Data
                    if (ds_mstQry.Tables[0].Rows.Count > 0 && ds_mstQry.Tables[1].Rows.Count > 0 && ds_mstQry.Tables[2].Rows.Count > 0)
                    {
                        DataTable dt_DataField = ds_mstQry.Tables[0];
                        subjectText = ds_mstQry.Tables[1].Rows[0]["EMAILSUBJECT"].ToString();
                        templateText = ds_mstQry.Tables[1].Rows[0]["TEMPLATETEXT"].ToString();

                        for (int i = 0; i < dt_DataField.Rows.Count; i++)
                        {
                            if (templateText.Contains(dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString()))
                            {
                                string dataFieldDisp = dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString();
                                templateText = templateText.Replace("[" + dt_DataField.Rows[i]["DATAFIELDDISPLAY"].ToString() + "]", ds_mstQry.Tables[2].Rows[0][dataFieldDisp].ToString());
                            }
                        }
                    }
                }

                //------------Code for sending email,It is optional---------------
                //added by kajal jaiswal  on 03-06-2023
             

            }
        }
            //countparent++;
            int status = 0;
            SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
            status = objSendEmail.SendEmail(email, message, Subject); //Calling Method
       
        // added by kajal jaiswal on 16-02-2023 for validating send email button 
        //if (countparent != 0)
        //{
        //    objCommon.DisplayMessage(Page, "Email send successfully to the Parent having Proper EmailId !", this.Page);
        //}
       
        //else
        //{
        //    objCommon.DisplayMessage(Page, "Email send successfully to the Parent having Proper EmailId !", this.Page);
        //}


    }

    //protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{

    //    Label lblComplete = e.Item.FindControl("lblComplete") as Label;
    //    Label lblInComplete = e.Item.FindControl("lblInComplete") as Label;
    //    Label lblFaculty = e.Item.FindControl("lblFaculty") as Label;
    //    CheckBox chk = e.Item.FindControl("chklock") as CheckBox;



    //    int check = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_MARKS_ENTRY_OPERTOR O ON (A.OPID=O.OPID)", "COUNT(A.OPID)", "A.OPID=" + (lblFaculty.ToolTip.ToString().Trim())));

    //    if (check != 0)       
    //    {
    //        chk.Checked = false;                            //CHECKBOX SELECTED IF LOCK = 1;
    //        lblComplete.Visible = true;                      //MARK ENTRY STAUTS COMPLETED
    //        lblInComplete.Visible = false;

    //    }
    //    else
    //    {
    //        chk.Checked = true;                     //CHECKBOX NOT SELECTED IF LOCK = 0;
    //        lblComplete.Visible = false;
    //        lblInComplete.Visible = true;          //MARK ENTRY STAUTS INCOMPLETE
    //    }




    //    btnSave.Visible = true;

    //}

    #endregion



    protected void btnShowStatus_Click(object sender, EventArgs e)
    {
        GridView GVStatus = new GridView();
        // string degree = ddlDegree.SelectedItem.Text;
        //string scheme = ddlScheme.SelectedItem.Text;
        string degree = ViewState["degreeno"].ToString();
        string scheme = ViewState["schemeno"].ToString();
        string ContentType = string.Empty;
        //*  DataSet ds = objMarksEntry.GetCourse_LockStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
        DataSet ds = objMarksEntry.GetCourse_LockStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            GVStatus.DataSource = ds;
            GVStatus.DataBind();
            string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + scheme.Replace(" ", "_") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVStatus.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            GVStatus.DataSource = ds;
            GVStatus.DataBind();
            objCommon.DisplayMessage("No record found...", this.Page);
        }
    }

    //added on 11-02-2020 by Vaishali
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();

        objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + " AND ISNULL(SE.ACTIVESTATUS,0)=1)", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
        int SUBEXAMNO = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + " AND ISNULL(SE.ACTIVESTATUS,0)=1)", "DISTINCT SE.SUBEXAMNO", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + ""));
        ViewState["SUBEXAMNO"] = SUBEXAMNO;
        //string fldname = objCommon.LookUp("ACD_EXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "EXAMNO>0 " + ddlExamType.SelectedValue);
        //if (fldname == "S1")
        //{
        //    divSubExamType.Visible = true;
        //    rfvSubExamType.Enabled = true;
        //   //string name = objCommon.LookUp("ACD_SUBJECTTYPE WITH (NOLOCK)", "DISTINCT SUBNAME", "SUBID" );
        //    //objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND S.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
        //    //*objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=1)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND SE.FLDNAME NOT IN('S1T5') AND S.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
        //    objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=1)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND SE.FLDNAME NOT IN('S1T5') AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");

        //}
        //else if (fldname == "S3")
        //{
        //    divSubExamType.Visible = true;
        //    rfvSubExamType.Enabled = true;
        //    //objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND S.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
        //    objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=2)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND SE.FLDNAME NOT IN('S1T5') AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
        //}
        //else
        //{
        //    divSubExamType.Visible = false;
        //    rfvSubExamType.Enabled = false;

        //}
        ddlStuType.SelectedIndex = 0;
        btnSave.Visible = false;
    }

    //added on 12-02-2020 by Vaishali
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        ddlExamType.SelectedIndex = 0;
        ddlStuType.SelectedIndex = 0;
        btnSave.Visible = false;

        objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + " AND ISNULL(SE.ACTIVESTATUS,0)=1) ", " DISTINCT ED.EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "EXAMNO");

        //objCommon.FillDropDownList(ddlExamType, "  ACD_EXAM_NAME S WITH (NOLOCK) INNER JOIN ACD_SUBEXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO)", " DISTINCT ED.SUBEXAMNO", "ED.SUBEXAMNAME", " ED.SUBEXAMNAME<>'' ", "ED.SUBEXAMNO");

        //string name = objCommon.LookUp("ACD_SUBJECTTYPE WITH (NOLOCK)", " SUBNAME", "SUBID>0" + ddlSubType.SelectedValue);
        //************************new*************
        //if (ddlSubType.SelectedValue == "1")//theory
        //{
        //    //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME NOT IN ('S6','S7')", "EXAMNO");
        //    //Added by Nikhil V.Lambe on 24/02/2021
        //    objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND FLDNAME  IN ('S1','S2','EXTERMARK')", "EXAMNO");


        //    divSubExamType.Visible = false;
        //    rfvSubExamType.Enabled = false;
        //}
        //else if (ddlSubType.SelectedValue == "2")//practical
        //{
        //    //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME IN ('S6','S7')", "EXAMNO");
        //    //Added by Nikhil V.Lambe on 24/02/2021
        //    //*objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME IN ('S3','EXTERMARK')", "EXAMNO");
        //    objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND FLDNAME IN ('S3','EXTERMARK')", "EXAMNO");


        //}
        ////Added by Nikhil V.Lambe on 24/02/2021
        //else if (ddlSubType.SelectedValue == "3")//sessinal
        //{
        //    //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME IN ('EXTERMARK')", "EXAMNO");
        //    objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND FLDNAME IN ('EXTERMARK')", "EXAMNO");

        //}
        //******************************
    }

    public void SendSMS(string MOBILENO, string MSG)
    {
        string status = "";
        //Your authentication key
        string authKey = "69072AzBuVtNC550d1635";
        string mobileNumber = string.Empty;

        if (MOBILENO.Trim().Length > 10)
        {
            mobileNumber = MOBILENO + "";
        }
        else
        {
            mobileNumber = "91" + MOBILENO + "";
        }
        //Multiple mobiles numbers separated by comma

        //Sender ID,While using route4 sender id should be 6 characters long.
        string senderId = "NITRPR";
        //Your message to send, Add URL encoding here.
        string message = System.Web.HttpUtility.UrlEncode("" + MSG + "");

        //Prepare you post parameters
        System.Text.StringBuilder sbPostData = new System.Text.StringBuilder();
        sbPostData.AppendFormat("authkey={0}", authKey);
        sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
        sbPostData.AppendFormat("&message={0}", message);
        sbPostData.AppendFormat("&sender={0}", senderId);
        sbPostData.AppendFormat("&route={0}", "4");

        try
        {
            //Call Send SMS API
            string sendSMSUri = "http://bulksms.ezzysms.in/sendhttp.php";
            //Create HTTPWebrequest
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            //Prepare and Add URL Encoded data
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            //Specify post method
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            //Get the response
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            status = reader.ReadToEnd();
            //Close the response
            reader.Close();
            response.Close();
        }
        catch (SystemException ex)
        {
            // MessageBox.Show(ex.Message.ToString());
        }
    }

    protected void ddlStuType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSubType.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        ddlStuType.SelectedIndex = 0;
        //* divSubExamType.Visible = false;
        lvCourse.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.Visible = false;
        ddlSubType.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        ddlStuType.SelectedIndex = 0;
        //*divSubExamType.Visible = false;
        btnSave.Visible = false;
        ddlSubExamType.SelectedIndex = 0;

    }
    protected void ddlSubExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStuType.SelectedIndex = 0;
        lvCourse.Visible = false;
        btnSave.Visible = false;
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            if (ddlClgname.SelectedIndex > 0)
            {
                Common objCommon = new Common();
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                }
            }


            //if (ddlClgname.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER ASM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SM  ON (ASM.SESSIONNO=SM.SESSIONNO) ", "ASM.SESSIONNO", "ASM.SESSION_PNAME", "ASM.SESSIONNO > 0 AND ISNULL(ASM.IS_ACTIVE,0)=1 AND ASM.SESSIONNO=" + ddlClgname.SelectedValue, "ASM.SESSIONNO desc");
            //}
            //ACD_COLLEGE_SCHEME_MAPPING SM
            //ddlSemester.Items.Clear();
            //ddlSemester.Items.Add(new ListItem("Please Select", "0"));

            //ddlStuType.SelectedIndex = 0;

            //ClearPanel();

            //if (ddlScheme.SelectedIndex > 0)
            //{
            // objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR, ACD_SEMESTER S", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SR.SCHEMENO = " + ddlScheme.SelectedValue + "AND SR.SEMESTERNO=S.SEMESTERNO AND SR.SEMESTERNO > 0", "S.SEMESTERNAME");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + ddlClgname.SelectedValue, "S.SEMESTERNO");
            //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "EXAMNO");
            //}
            pnlStudents.Visible = false;
            lvCourse.Visible = false;
            ddlSubType.SelectedIndex = 0;
            ddlExamType.SelectedIndex = 0;
            //* divSubExamType.Visible = false;
            ddlStuType.SelectedIndex = 0;
            btnSave.Visible = false;
            ddlSession.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme. ddlClgname_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }



    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void AddReportHeader(GridView gv)
    {
        try
        {

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header1Cell = new TableCell();

            Header1Cell.Text = "SRI VENKATESWARA COLLEGE OF ENGINEERING";
            Header1Cell.ColumnSpan = 17;
            Header1Cell.Font.Size = 14;
            Header1Cell.Font.Bold = true;
            Header1Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow1.Cells.Add(Header1Cell);
            gv.Controls[0].Controls.AddAt(0, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header2Cell = new TableCell();

            Header2Cell.Text = "PENNALUR, SRIPERUMBUDUR (Tk) - 602117";
            Header2Cell.ColumnSpan = 17;
            Header2Cell.Font.Size = 14;
            Header2Cell.Font.Bold = true;
            Header2Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow2.Cells.Add(Header2Cell);
            gv.Controls[0].Controls.AddAt(1, HeaderGridRow2);

            GridViewRow HeaderGridRow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header3Cell = new TableCell();

            //Header3Cell.Text = "Report : Students Strength " + ddlHostelNo.SelectedItem.Text;
            //Header3Cell.ColumnSpan = 10;
            //Header3Cell.Font.Size = 12;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            gv.HeaderRow.Visible = true;

            //GridViewRow HeaderGridRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell Header3Cell = new TableCell();


            //Header3Cell.Text = "BLOCK";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "I YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "II YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "II (LE)";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "III YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "IV YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "TOTAL STRENGTH";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnlock_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updpnlExam, "Please Select College & Regulation", this.Page);
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updpnlExam, "Please Select Session", this.Page);
        }
        else if (ddlSubExamType.SelectedIndex==0)
        {
            objCommon.DisplayMessage(this.updpnlExam, "Please Select SubExamType", this.Page);
        }
        else
        {
             try
            {

               
                string attachment = "attachment; filename=" + "LOCKUNLOCK.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                // string EXAM_DATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
                // string EXAM_DATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");



                string sp_procedure = "PKG_GET_LOCK_UNLOCK_REPORT";
                string sp_parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SUBEXAMNO";
                string sp_callValues = "" + ddlSession.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlExamType.SelectedValue) + "";
                DataSet DS = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

                DataGrid dg = new DataGrid();
                //DataTable dt = null;
                //dt = ds.
               
                    if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                    {


                        //dsfee.Tables[0].Columns.Remove("TIMEFROM");
                        //dsfee.Tables[0].Columns.Remove("TIMETO");
                        //dsfee.Tables[0].Columns.Remove("COLLEGE_ID");
                        //dsfee.Tables[0].Columns.Remove("ROOMNO");
                        //dsfee.Tables[0].Columns.Remove("SLOTNAME");
                        //dsfee.Tables[0].Columns.Remove("EXAMDATE");
                        //dsfee.Tables[0].Columns.Remove("SESSIONNAME");


                        dg.DataSource = DS.Tables[0];
                        dg.DataBind();
                        dg.HeaderStyle.Font.Bold = true;
                        dg.RenderControl(htw);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    else
                     {

                       objCommon.DisplayMessage(this.updpnlExam, "No Record Found", this.Page);
                     }
            } 
            catch (Exception ex)
            {

            }

        }

    }
   
}


