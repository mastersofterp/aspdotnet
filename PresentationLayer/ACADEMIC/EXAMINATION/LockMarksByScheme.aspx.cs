//======================================================================================
// PROJECT NAME  : UAIMS[RAIPUR]                                                              
// MODULE NAME   : ACADAMIC
// PAGE NAME     : LOCK UNLOCK [EXAMINATION]
// CREATION DATE : 17 Jan 2012                                        
// CREATED BY    : PRIYANKA KABADE          
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                  
//=======================================================================================

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

public partial class ACADEMIC_LockMarksByScheme : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    CourseController objCourse = new CourseController();

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
              //  this.CheckPageAuthorization();

                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();

                //LOAD PAGE HELP
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               // if ((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || (Session["usertype"].ToString() == "3") || (Session["usertype"].ToString() == "1")) //Commented by Nikhil V.Lambe
               if(Session["usertype"].ToString()=="1" || Session["usertype"].ToString()=="8") //Added By Nikhil V.Lambe on 24/02/2021 for page should be access by Admin and HOD.
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
            foreach (ListViewDataItem dataRow in lvCourse.Items)
            {
                UpdateLockStatus(dataRow); //UPDATE ONE BY ONE LOCK STATUS OF RECORD FOR COURSE
            }
            objCommon.DisplayMessage(this.updpnlExam, "Lock/Unlock Done Successfully.", this);
            BindExam();
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
            ddlBranch.Items.Add(new ListItem("Please Select","0"));
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


        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");


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
            int CollegeId=Convert.ToInt32(ViewState["college_id"]);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int semester = Convert.ToInt32(ddlSemester.SelectedValue);
           // int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
            int examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            string courseno = ((Label)dataRow.FindControl("lblCourseNo")).ToolTip;
            string section = ((Label)dataRow.FindControl("lblsection")).ToolTip;
            string ccode = ((Label)dataRow.FindControl("lblCourseNo")).Text;

            //COMMENTED ON 14-02-2020 BY VAISHALI
            //string facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
            //string facultynopractical = ((Label)dataRow.FindControl("lblfacultypractical")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblfacultypractical")).ToolTip.ToString();

            // added on 14-02-2020 by Vaishali
            string facultynotheory = string.Empty, facultynopractical = string.Empty;

            if (ddlSubType.SelectedValue == "1")
            {
                 facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                 facultynopractical = "0";
            }
            else if (ddlSubType.SelectedValue == "2")
            {
                facultynopractical = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                facultynotheory = "0";
            }
            else if (ddlSubType.SelectedValue == "3")
            {
                facultynotheory = ((Label)dataRow.FindControl("lblFaculty")).ToolTip == "" ? "0" : ((Label)dataRow.FindControl("lblFaculty")).ToolTip.ToString();
                facultynopractical = "0";
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

            int lockunlock = 0 ;
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
            DataSet dsmarkcount = objMarksEntry.GetMarkEntryNotDonecount(Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStuType.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(courseno), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue.Trim() : string.Empty);

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
                        objMarksEntry.LockUnLockMarkEntryByAdmin(SessionNo, semester, schemeno, examtype, Convert.ToInt32(courseno), Convert.ToInt32(section), Convert.ToInt32(facultynotheory), Convert.ToInt32(facultynopractical), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"]), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue : string.Empty); //modified on 13-02-2020 by Vaishali
                }
                else
                    objMarksEntry.LockUnLockMarkEntryByAdmin(SessionNo, semester, schemeno, examtype, Convert.ToInt32(courseno), Convert.ToInt32(section), Convert.ToInt32(facultynotheory), Convert.ToInt32(facultynopractical), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"]), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue : string.Empty); //modified on 13-02-2020 by Vaishali
            }
            else
                objMarksEntry.LockUnLockMarkEntryByAdmin(SessionNo, semester, schemeno, examtype, Convert.ToInt32(courseno), Convert.ToInt32(section), Convert.ToInt32(facultynotheory), Convert.ToInt32(facultynopractical), Convert.ToInt32(lockunlock), ipAddress, Convert.ToInt32(Session["userno"]), divSubExamType.Visible == true ? ddlSubExamType.SelectedValue : string.Empty); //modified on 13-02-2020 by Vaishali
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.UpdateLockStatus --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowMarkListReport(string reportTitle, string rptFileName,string courseno,string ccode,string section, string uano, string examname)
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
                     (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name - Practical";
                 else if (ddlSubType.SelectedValue == "3")
                     (lvCourse.FindControl("lblFacName") as Label).Text = "Faculty Name - Sessional";
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
        string fldname = objCommon.LookUp("ACD_EXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "EXAMNO = " + ddlExamType.SelectedValue);
        if (fldname == "S1")
        {
            divSubExamType.Visible = true;
            rfvSubExamType.Enabled = true;
            //objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND S.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
            //*objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=1)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND SE.FLDNAME NOT IN('S1T5') AND S.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
            objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=1)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND SE.FLDNAME NOT IN('S1T5') AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");

        }
        else if (fldname == "S3")
        {
            divSubExamType.Visible = true;
            rfvSubExamType.Enabled = true;
            //objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND S.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
            objCommon.FillDropDownList(ddlSubExamType, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=2)", "DISTINCT SE.FLDNAME", "SUBEXAMNAME", "EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND SE.FLDNAME NOT IN('S1T5') AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND SE.EXAMNO = " + ddlExamType.SelectedValue + "", "SE.FLDNAME");
        }
        else
        {
            divSubExamType.Visible = false;
            rfvSubExamType.Enabled = false;

        }
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


        //objCommon.FillDropDownList(ddlExamType, "  ACD_EXAM_NAME S WITH (NOLOCK) INNER JOIN ACD_SUBEXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO)", " DISTINCT ED.SUBEXAMNO", "ED.SUBEXAMNAME", " ED.SUBEXAMNAME<>'' ", "ED.SUBEXAMNO");

        if (ddlSubType.SelectedValue == "1")
        {
            //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME NOT IN ('S6','S7')", "EXAMNO");
            //Added by Nikhil V.Lambe on 24/02/2021
            objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND FLDNAME  IN ('S1','S2','EXTERMARK')", "EXAMNO");


            divSubExamType.Visible = false;
            rfvSubExamType.Enabled = false;
        }
        else if (ddlSubType.SelectedValue == "2")
        {
            //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME IN ('S6','S7')", "EXAMNO");
            //Added by Nikhil V.Lambe on 24/02/2021
            //*objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME IN ('S3','EXTERMARK')", "EXAMNO");
            objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND FLDNAME IN ('S3','EXTERMARK')", "EXAMNO");


        }
        //Added by Nikhil V.Lambe on 24/02/2021
        else if (ddlSubType.SelectedValue == "3")
        {
            //objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND FLDNAME IN ('EXTERMARK')", "EXAMNO");
            objCommon.FillDropDownList(ddlExamType, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO)", " DISTINCT EXAMNO", "EXAMNAME", " EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND FLDNAME IN ('EXTERMARK')", "EXAMNO");

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
        divSubExamType.Visible = false;
        lvCourse.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.Visible = false;
        ddlSubType.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        ddlStuType.SelectedIndex = 0;
        divSubExamType.Visible = false;
        btnSave.Visible = false;

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
            divSubExamType.Visible = false;
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
}
