//=================================================================================
//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : TIME TABLE CREATION                                                 
// CREATION DATE : 1-JUNE-2011
// CREATED BY    : GAURAV S SONI                               
// MODIFIED BY   : PAVAN A. RAUT
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_TIMETABLE_TimeTableReport_New : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    GridView gvTemp = new GridView();

    //string Message = string.Empty;
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    static string session, scheme, semester, section, slotype, ExisitingDates, Startdate, Enddate, clgID, OrgId, Degreeno;
    //int id = 0;
    //int count = 0;
    //string value = string.Empty.Trim();
    //string value1 = string.Empty.Trim();
    //string value2, value3, value4 = string.Empty.Trim();
    //int sessionsrno, coursesrno, subjectsrno, teachersrno, slotsrno, no, thpr = 0;
    //int flag, flag1, flag2, flag3, flag4, flag5, flag6, flag7 = 0;
    //int flag301, flag402 = 0;
    //string str = string.Empty.Trim();
    //string str1 = string.Empty.Trim();
    //string str2 = string.Empty.Trim();
    //string str3 = string.Empty.Trim();
    //string str4 = string.Empty.Trim();
    //string str5 = string.Empty.Trim();
    //string str6 = string.Empty.Trim();
    //long ret;
    //long ret1;
    //string slot1, slot2, slot3, slot4, slot5, slot6, slot7 = string.Empty.Trim();
    //int[] mon = new int[7];
    //int[] tue = new int[7];
    //int[] wed = new int[7];
    //int[] thurs = new int[7];
    //int[] fri = new int[7];
    //int[] sat = new int[7];

    //int totlength, monlen, tuelen, wedlen, thurslen, frilen, satlen = 0;

    #region "Page Event"
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
                if (Session["usertype"].ToString() != "3" || (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1"))
                {
                    //btnfacultywiseReport.Visible = Session["usertype"].ToString() != "3" ? false : true;
                    btnCourseWise.Visible = true;
                    btnTimeTableReport.Visible = true;
                }
                else
                {
                    //btnfacultywiseReport.Visible = true;
                    btnCourseWise.Visible = false;
                    btnTimeTableReport.Visible = false;
                }
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                this.PopulateDropDownList();
                //FillTeacher();
                Session["transferTbl"] = null;
            }
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 30/01/2022
            //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 30/01/2022
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TimeTableReport_New.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TimeTableReport_New.aspx");
        }
    }

    #endregion "Page Event"

    #region "General"
    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                if (Session["dec"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ") AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                }
            }
            else
            {
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
            }
            ddlSession.SelectedIndex = 0;
            //  objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //if (Session["usertype"].ToString() != "1")
            //{
            //    string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
            //    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
            //    //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            //}
            //else
            //{
            //    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "C.COLLEGE_ID > 0 ", "C.COLLEGE_ID");
            //   // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
            //}

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void FillTeacher()
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[0];
            DataTable dtTeacher = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES", objParams).Tables[0];

            //DropDownList
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            ddlTeacher.DataSource = dtTeacher;
            ddlTeacher.DataTextField = dtTeacher.Columns["UA_FULLNAME"].ToString();
            ddlTeacher.DataValueField = dtTeacher.Columns["UA_NO"].ToString();
            ddlTeacher.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    //private void BindList()
    //{
    //    try
    //    {

    //        if (ddlScheme.SelectedIndex > 0)
    //        {
    //            DataSet ds = objAttC.GetCourseAllotment(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ddlCourse.Items.Clear();
    //                ddlCourse.Items.Add("Select");
    //                ddlCourse.SelectedItem.Value = "0";
    //                ddlCourse.DataTextField = "COURSE_NAME";
    //                ddlCourse.DataValueField = "COURSENO";
    //                ddlCourse.DataSource = ds;
    //                ddlCourse.DataBind();
    //                ddlCourse.SelectedIndex = 0;
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage(this.updTimeTable, "Course Teacher Allotment not done", this.Page);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_TIMETABLE_TimeTable.BindListView-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    #endregion "General"

    #region "Selected Index Changed"
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlFloor, "ACD_FLOOR", "FLOORNO", "FLOORNAME", "FLOORNO > 0", "FLOORNO");
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
        ddlFloor.SelectedIndex = 0;
        ddlRoom.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));

        //ddlSlotType.Items.Clear();
        //ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        //ddlSlotType.SelectedIndex = -1;
        if (ddlSession.SelectedIndex > 0)
        {
            this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["branchno"]));
            ddlSem.Focus();
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlBranch.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            ddlFloor.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
            //ddlScheme.SelectedIndex = 0;
            ddlBatch.SelectedIndex = 0;

            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]), "B.BRANCHNO");

                DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
                string BranchNos = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
                }
                DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
                //on faculty login to get only those dept which is related to logged in faculty
                objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
                ddlBranch.Focus();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            ddlFloor.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
            //ddlScheme.SelectedIndex = 0;
            ddlBatch.SelectedIndex = 0;

            //if (ddlBranch.SelectedIndex > 0)
            //{
            //}
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]) + " AND BRANCHNO =" + Convert.ToInt32(ViewState["branchno"]), "SCHEMENO DESC");

            ddlScheme.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSem.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlBatch.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;

            lblStatus.Text = string.Empty;
            ddlSem.Focus();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            ddlFloor.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
            ddlBatch.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));

            //ddlSlotType.Items.Clear();
            //ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlSlotType.SelectedIndex = -1;

            DateTime StartDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            DateTime EndDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            divDateDetails.Visible = true;
            lblTitleDate.Text = "Selected Session Start Date : " + StartDate.ToShortDateString() + " End Date : " + EndDate.ToShortDateString();



            if (ddlSem.SelectedIndex > 0)
            {
                if (ViewState["branchno"] == "99")
                    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR LEFT OUTER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_SCHEME WHERE DEGREENO = 1) AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                else
                    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");

                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SUBID<>9 and C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND C.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");

                objCommon.FillDropDownList(ddlRoom, "ACD_COURSE_TEACHER ct inner join ACD_ROOM r on ct.roomno=r.roomno", "DISTINCT r.ROOMNO", "r.ROOMNAME", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO = " + ddlSem.SelectedValue + " and sessionno = " + ddlSession.SelectedValue, "roomno");

                ddlSubjectType.Items.Add(new ListItem("Tutorial", "100"));
            }

            ddlSection.Focus();

        }
        catch
        {
            throw;
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int batchno = 0;
            if (ddlBatch.SelectedIndex > 0)
                batchno = Convert.ToInt32(ddlBatch.SelectedValue);

            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            ddlFloor.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
            ddlBatch.SelectedIndex = 0;
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));

            //ddlSlotType.Items.Clear();
            //ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
            ddlSlotType.SelectedIndex = -1;

            if (ddlSection.SelectedIndex > 0)
            {
                // The value '100' is used for the Tutorials
                if (ddlSection.SelectedValue == "100")
                {
                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE AC INNER JOIN ACD_OFFERED_COURSE OC ON(AC.COURSENO=OC.COURSENO)", "OC.COURSENO", "(OC.CCODE + ' - ' + COURSE_NAME) COURSE_NAME ", "OC.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SUBID = 1" + " AND OC.TO_TERM = " + ddlSem.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue, "OC.CCODE");
                }
                else
                {
                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER ct inner join ACD_COURSE c on ct.COURSENO=c.COURSENO", " Distinct c.COURSENO", "(C.CCODE + ' - ' + c.COURSE_NAME) COURSE_NAME ", "ct.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND ct.semesterno = " + ddlSem.SelectedValue + " AND ct.SESSIONNO = " + ddlSession.SelectedValue + " and ct.sectionno=" + ddlSection.SelectedValue, "C.COURSENO");
                }
                objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "DISTINCT SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO>0", "SLOTTYPENO");
                ddlCourse.Focus();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlSubjectType.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            ddlFloor.SelectedIndex = 0;
            ddlRoom.SelectedIndex = 0;
            ddlBatch.SelectedIndex = 0;

            if (ddlSubjectType.SelectedIndex > 0)
            {
                // The value '100' is used for the Tutorials
                if (ddlSubjectType.SelectedValue == "100")
                {
                    // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME ", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SUBID = 1" + " AND SEMESTERNO = " + ddlSem.SelectedValue + " AND THEORY = 1", "CCODE");
                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE AC INNER JOIN ACD_OFFERED_COURSE OC ON(AC.COURSENO=OC.COURSENO)", "OC.COURSENO", "(OC.CCODE + ' - ' + COURSE_NAME) COURSE_NAME ", "OC.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SUBID = 1" + " AND OC.TO_TERM = " + ddlSem.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue, "OC.CCODE");
                }
                else
                {
                    //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME ", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue + " AND SEMESTERNO = " + ddlSem.SelectedValue, "CCODE");
                    //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE AC INNER JOIN ACD_OFFERED_COURSE OC ON(AC.COURSENO=OC.COURSENO)", "OC.COURSENO", "(OC.CCODE + ' - ' + COURSE_NAME) COURSE_NAME ", "OC.SCHEMENO = " + ddlScheme.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue + " AND OC.TO_TERM = " + ddlSem.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue, "OC.CCODE");
                    objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER ct inner join ACD_COURSE c on ct.COURSENO=c.COURSENO", "c.COURSENO", "(C.CCODE + ' - ' + c.COURSE_NAME) COURSE_NAME ", "ct.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND ct.SUBID = " + ddlSubjectType.SelectedValue + " AND ct.semesterno = " + ddlSem.SelectedValue + " AND ct.SESSIONNO = " + ddlSession.SelectedValue + " and ct.sectionno=" + ddlSection.SelectedValue, "C.CCODE");
                }

                ddlCourse.Focus();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlSubjectType.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTeacher.SelectedIndex = 0;
        ddlRoom.SelectedIndex = 0;
        if (ddlBatch.SelectedIndex > 0)
        {
            DataSet ds = objAttC.GetFacultyBySelection(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlTeacher.Items.Clear();
                ddlTeacher.Items.Add("Please Select");
                ddlTeacher.SelectedItem.Value = "0";
                ddlTeacher.DataTextField = "ua_fullname";
                ddlTeacher.DataValueField = "UA_NO";
                ddlTeacher.DataSource = ds;
                ddlTeacher.DataBind();
                ddlTeacher.SelectedIndex = 0;
            }
            else
            {
                ddlTeacher.Items.Clear();
                ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
                ddlTeacher.Focus();
            }
        }

        else
        {
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            ddlTeacher.Focus();
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlTeacher.SelectedIndex = 0;
        ddlFloor.SelectedIndex = 0;
        ddlRoom.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlTeacher.Items.Clear();
        ddlTeacher.Items.Add(new ListItem("Please Select", "0"));

        //ddlSlotType.Items.Clear();
        //ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.SelectedIndex = -1;
        if (ddlCourse.SelectedIndex > 0)
        {
            if (ddlCourse.SelectedValue == "100")
            {
                objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSem.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + "AND CT.SUBID = 100 AND ISNULL(CT.CANCEL,0)=0", "UA.UA_FULLNAME");
            }
            else
                if (ddlCourse.SelectedIndex > 0)
                {
                    if (ddlCourse.SelectedIndex > 0)
                    {
                        DataSet ds = objAttC.GetFacultyBySelection(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlTeacher.Items.Clear();
                            ddlTeacher.Items.Add("Please Select");
                            ddlTeacher.SelectedItem.Value = "0";
                            ddlTeacher.DataTextField = "ua_fullname";
                            ddlTeacher.DataValueField = "UA_NO";
                            ddlTeacher.DataSource = ds;
                            ddlTeacher.DataBind();
                            ddlTeacher.SelectedIndex = 0;
                        }
                        else
                        {
                            ddlTeacher.Items.Clear();
                            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
                            ddlTeacher.Focus();
                        }
                    }
                    int Subid = Convert.ToInt32(objCommon.LookUp("acd_course", "subid", "courseno=" + ddlCourse.SelectedValue));

                    // Right This is for batch b
                    if (Subid == 2)
                    {
                        objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER T INNER JOIN ACD_BATCH B ON (B.BATCHNO = T.BATCHNO)", "DISTINCT T.BATCHNO", "B.BATCHNAME", "T.BATCHNO > 0 AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue), "T.BATCHNO");
                        trBatch.Visible = true;
                    }
                    else
                    {
                        trBatch.Visible = false;
                        trgpmodule.Visible = false;
                        ddlBatch.SelectedIndex = 0;
                        ddlGPModule.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlTeacher.Items.Clear();
                    ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
                    ddlBatch.Items.Clear();
                    ddlBatch.Items.Add(new ListItem("Please Select", "0"));
                    trBatch.Visible = false;
                }
        }
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));

            if (Convert.ToDateTime(txtFromDate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                txtFromDate.Text = string.Empty;
                txtFromDate.Focus();
            }
            else if (Convert.ToDateTime(txtFromDate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                txtFromDate.Text = string.Empty;
                txtFromDate.Focus();
            }
        }
        catch
        {
            txtFromDate.Text = string.Empty;
            txtFromDate.Focus();
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));

            if (Convert.ToDateTime(txtTodate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                //  objCommon.DisplayMessage(this.Page, "End date should be greater than session start date", this.Page);
                txtTodate.Text = string.Empty;
                txtTodate.Focus();
            }
            else if (Convert.ToDateTime(txtTodate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                // objCommon.DisplayMessage(this.Page, "End date should be less than session end date", this.Page);
                txtTodate.Text = string.Empty;
                txtTodate.Focus();
            }
        }
        catch
        {
            txtTodate.Text = string.Empty;
            txtTodate.Focus();
        }
    }

    #endregion "Selected Index Changed"

    //public bool validDate()
    //{
    //    try
    //    {
    //        string SDate = (objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
    //        string EDate = (objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));

    //        if (Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(SDate) && txtFromDate.Text != "")
    //        {
    //            objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToString(), this.Page);
    //            txtFromDate.Text = string.Empty;
    //            txtFromDate.Focus();
    //            return false;
    //        }
    //        else if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(EDate) && txtFromDate.Text != "")
    //        {
    //            objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToString(), this.Page);
    //            txtFromDate.Text = string.Empty;
    //            txtFromDate.Focus();
    //            return false;
    //        }
    //        else if (Convert.ToDateTime(txtTodate.Text) < Convert.ToDateTime(SDate) && txtTodate.Text != "")
    //        {
    //            objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToString(), this.Page);
    //            //  objCommon.DisplayMessage(this.Page, "End date should be greater than session start date", this.Page);
    //            txtTodate.Text = string.Empty;
    //            txtTodate.Focus();
    //            return false;
    //        }
    //        else if (Convert.ToDateTime(txtTodate.Text) > Convert.ToDateTime(EDate) && txtTodate.Text != "")
    //        {
    //            objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToString(), this.Page);
    //            // objCommon.DisplayMessage(this.Page, "End date should be less than session end date", this.Page);
    //            txtTodate.Text = string.Empty;
    //            txtTodate.Focus();
    //            return false;
    //        }
    //        else
    //        {
    //            return true;
    //        }
    //    }
    //    catch { return true; }
    //}



    protected void btnTimeTableReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text == "")
            {
                if (txtTodate.Text == "")
                {
                    if (ddlCourse.SelectedIndex > 0)
                    {
                        objCommon.DisplayMessage(this.updTimeTable, "For TimeTable Report Select Upto Section Only & Slot Type !!", this.Page);
                        return;
                    }

                    ShowReport("TIME TABLE", "rptAcadTimeTableReport_New.rpt");
                }
            }
            else
            {
                if (txtTodate.Text == "")
                {
                    objCommon.DisplayMessage(this.updTimeTable, "Please Select To Date !!", this.Page);
                    txtTodate.Focus();
                }
                else
                {
                    if (ddlCourse.SelectedIndex > 0)
                    {
                        objCommon.DisplayMessage(this.updTimeTable, "For TimeTable Report Select Upto Section Only & Slot Type !!", this.Page);
                        return;
                    }

                    ShowReport("TIME TABLE", "rptAcadTimeTableReport_New.rpt");
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnfacultywiseReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text == "")
            {
                if (txtTodate.Text == "")
                {
                    ShowReport("TIME", "rptAcadFacultyWiseReport_New.rpt");
                }
            }
            else
            {
                if (txtTodate.Text == "")
                {
                    objCommon.DisplayMessage(this.updTimeTable, "Please Select To Date !!", this.Page);
                    txtTodate.Focus();
                }
                else
                {
                    ShowReport("TIME", "rptAcadFacultyWiseReport_New.rpt");
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCourseWise_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text == "")
            {
                if (txtTodate.Text == "")
                {
                    ShowReport("Time Table Course Wise", "rptAcadTimeTableReport_New_Format_II.rpt");
                }
            }
            else
            {
                if (txtTodate.Text == "")
                {
                    objCommon.DisplayMessage(this.updTimeTable, "Please Select To Date !!", this.Page);
                    txtTodate.Focus();
                }
                else
                {
                    ShowReport("Time Table Format II", "rptAcadTimeTableReport_New_Format_II.rpt");
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ""

                   + ",@P_SEM=" + (ddlSem.SelectedItem.Text).ToString().Trim()
                //  + ",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim()
                   + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                   + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                   + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                   + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                   + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"])
                   + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue)
                   + ",@P_COURSENO=" + ddlCourse.SelectedValue
                   + ",@P_SLOTTYPE=" + ddlSlotType.SelectedValue + ",@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtTodate.Text + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTimeTable, this.updTimeTable.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    ///Added by Irfan Shaikh on 20190415---Start
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        AcdAttendanceController objAttCon = new AcdAttendanceController();
        DataSet ds = null;
        int SessionNo = 0, SchemeNo = 0, SemNo = 0, CourseNo = 0, UA_No = 0, SubID = 0, SectionNo = 0, BatchNo = 0;

        SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
        SemNo = Convert.ToInt32(ddlSem.SelectedValue);
        CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
        UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
        SubID = Convert.ToInt32(ddlSubjectType.SelectedValue);
        SectionNo = Convert.ToInt32(ddlSection.SelectedValue);
        BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);

        ds = objAttCon.RetrieveStudentAttDetailsExcel(SessionNo, SchemeNo, SemNo, CourseNo, UA_No, SubID, SectionNo, BatchNo);

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvTemp.DataSource = ds;
            gvTemp.DataBind();
            this.CallExcel();
        }
        else
        {
            objCommon.DisplayMessage("Record Not Found!!", this.Page);
            return;
        }
    }

    private void CallExcel()
    {
        string attachment = "attachment; filename=AttendanceDetails " + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + ".xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gvTemp.HeaderRow.Style.Add("background-color", "#e3ac9a");
        gvTemp.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }






    protected void btnRoomWiseReport_Click(object sender, EventArgs e)
    {
        ShowReportRoomWise("Timetable_Report", "rptAcadRoomWiseReport.rpt");
    }

    private void ShowReportRoomWise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["userfullname"].ToString()
                   + ",@P_DEGREE=" + ddlDegree.SelectedItem.Text.Trim()
                   + ",@P_BRANCH=" + ddlBranch.SelectedItem.Text.Trim().Replace("&", "AND")
                   + ",@P_SCHEME=" + (ddlScheme.SelectedItem).ToString().Trim().Replace("&", "AND")
                   + ",@P_SEM=" + (ddlSem.SelectedItem).ToString().Trim()
                   + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                   + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                   + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                   + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                   + ",@P_UA_NO=" + Convert.ToInt32(ddlTeacher.SelectedValue)
                   + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue)
                   + ",@P_COURSENO=" + ddlCourse.SelectedValue
                   + ",@P_ROOMNO=" + Convert.ToInt32(ddlRoom.SelectedValue)
                   + ",@P_SLOTTYPE=" + ddlSlotType.SelectedValue;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updTimeTable, this.updTimeTable.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlGPModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 9)
        {
            objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER T INNER JOIN ACD_BATCH B ON (B.BATCHNO = T.BATCHNO)", "DISTINCT T.BATCHNO", "B.BATCHNAME", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SECTIONNO =" + Convert.ToInt32(ddlSection.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND GPNO =" + Convert.ToInt32(ddlGPModule.SelectedValue), "T.BATCHNO");
            trBatch.Visible = true;
        }
        else
        {
            trBatch.Visible = false;
            trgpmodule.Visible = false;
            ddlBatch.SelectedIndex = 0;
            ddlGPModule.SelectedIndex = 0;
        }
    }

    protected void btnReport_trans_Click(object sender, EventArgs e)
    {
    }

    protected void btnSectionWiseReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportSectionWise("TIME", "rptAcadTimeTable.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportSectionWise(string reportTitle, string rptFileName)
    {
        try
        {
            int batchno = 0;
            if (ddlBatch.SelectedIndex > 0)
            {
                batchno = Convert.ToInt32(ddlBatch.SelectedValue);
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",username=" + Session["userfullname"].ToString()
                    + ",@P_DEGREE=" + ddlDegree.SelectedItem.Text.Trim()
                    + ",@P_BRANCH=" + ddlBranch.SelectedItem.Text.Trim().Replace("&", "AND")
                    + ",@P_SCHEME=" + (ddlScheme.SelectedItem).ToString().Trim().Replace("&", "AND")
                    + ",@P_SEM=" + (ddlSem.SelectedItem.Text).ToString().Trim()
                    + ",@P_SECTION=" + ddlSection.SelectedItem.Text.Trim()
                    + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                    + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
                    + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                    + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                    + ",@P_UA_NO=" + Convert.ToInt32(ddlTeacher.SelectedValue)
                    + ",@P_BATCHNO=" + batchno
                    + ",@P_COURSENO=" + ddlCourse.SelectedValue
                    + ",@P_SLOTTYPE=" + ddlSlotType.SelectedValue;

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTimeTable, this.updTimeTable.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "FLOORNO = " + ddlFloor.SelectedValue, "ROOMNO");
        ddlRoom.Focus();
        ddlRoom.SelectedIndex = 0;
    }

    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFloor.SelectedIndex = 0;
        ddlRoom.SelectedIndex = 0;
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        objCommon.FillDropDownList(ddlSlotType, "ACD_SLOTTYPE", "DISTINCT SLOTTYPENO", "SLOTTYPE_NAME", "SLOTTYPENO>0", "SLOTTYPENO");

    }

    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int branchno)
    {
        DataSet ds = objAttC.GetSemesterDurationwise(sessionno, branchno);
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add("Please Select");
        ddlsemester.SelectedItem.Value = "0";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlsemester.DataSource = ds;
            ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlsemester.DataBind();
            ddlsemester.SelectedIndex = 0;
        }
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.Items.Clear();
        ddlSlotType.Items.Add(new ListItem("Please Select", "0"));

        //ddlSlotType.Items.Clear();
        //ddlSlotType.Items.Add(new ListItem("Please Select", "0"));
        ddlSlotType.SelectedIndex = -1;
        if (ddlSchoolInstitute.SelectedIndex > 0)
        {

            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SESSIONNO = SM.SESSIONNO AND SM.COLLEGE_ID=CT.COLLEGE_ID)", "DISTINCT SM.SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 and ODD_EVEN<>3 AND SM.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND ISNULL(CT.CANCEL,0)=0", "SM.SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
    }

    public void LoadExisitingDates()
    {
        try
        {
            if (ddlSession.SelectedIndex > 0 && ddlSem.SelectedIndex > 0 && ddlSection.SelectedIndex > 0 && ddlSlotType.SelectedIndex > 0)
            {
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                ddlExistingDates.SelectedIndex = 0;
                // MODIFIED  BY SAFAL GUPTA ON 28042021
                DataSet dsGetExisitingDates = objCommon.FillDropDown("ACD_TIME_TABLE_CONFIG TT INNER JOIN  ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_TIME_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE", "DISTINCT CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10))  AS EXISTINGDATES ", "START_DATE,END_DATE,MONTH(START_DATE) as STARTDATEMONTH", "ISNULL(TT.CANCEL,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND CAST(convert(varchar(10),START_DATE,103) AS NVARCHAR(10))+' - '+CAST(convert(varchar(10),END_DATE,103) AS NVARCHAR(10)) IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " and SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " and SEMESTERNO=" + ddlSem.SelectedValue + " and SECTIONNO=" + ddlSection.SelectedValue + "and SLOTTYPE=" + ddlSlotType.SelectedValue + " AND TT.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND TT.ORGANIZATIONID=" + Session["OrgId"], "MONTH(START_DATE) ");
                if (dsGetExisitingDates.Tables[0].Rows.Count > 0)
                {
                    ddlExistingDates.DataSource = dsGetExisitingDates.Tables[0];
                    ddlExistingDates.DataTextField = "EXISTINGDATES";
                    ddlExistingDates.DataBind();
                }
                else
                {
                    ddlExistingDates.DataSource = null;
                    ddlExistingDates.DataBind();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSlotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSlotType.SelectedIndex > 0)
            {
                LoadExisitingDates();
                ddlExistingDates.SelectedIndex = 0;
                txtFromDate.Text = "";
                txtTodate.Text = "";
                ddlExistingDates.Focus();
                // pnlSlots.Visible = false;
                //dListFaculty.DataSource = null;
                //dListFaculty.DataBind();
                //divCourses.Visible = false;
                //ddlRevisedNo.SelectedIndex = 0;
                //slotype = "";
                //slotype = ddlSlotType.SelectedValue;
                //OrgId = Session["OrgId"].ToString();
                //AttendanceConfigDate();
            }
            else
            {
                // pnlSlots.Visible = false;
                //dListFaculty.DataSource = null;
                // dListFaculty.DataBind();
                // divCourses.Visible = false;
                ddlExistingDates.Items.Clear();
                ddlExistingDates.Items.Add(new ListItem("Please Select", "0"));
                txtFromDate.Text = "";
                txtTodate.Text = "";
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlExistingDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AcdAttendanceController objC = new AcdAttendanceController();
            if (ddlSlotType.SelectedIndex > 0)
            {
                if (ddlExistingDates.SelectedIndex > 0)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "test1();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () {test1();});", true);
                    string myStr = ddlExistingDates.SelectedItem.ToString();
                    string[] ssizes = myStr.Split(' ');
                    string startdate = ssizes[0].ToString();
                    string enddate = ssizes[2].ToString();
                    txtFromDate.Text = startdate;
                    txtTodate.Text = enddate;
                    Enddate = enddate;
                    Startdate = startdate;
                    //txtDate.Text = "";
                    //date = "";
                    ExisitingDates = "";
                    ExisitingDates = ddlExistingDates.SelectedItem.ToString();

                    // pnlSlots.Visible = false;
                    if (ddlSem.SelectedIndex > 0)
                    {
                        Boolean lockCT;
                        lockCT = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND ORGANIZATIONID=" + Session["OrgId"].ToString()) == string.Empty ? false : Convert.ToBoolean(objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT ISNULL(LOCK,0)", "SESSIONNO =" + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO =" + ddlSem.SelectedValue + "AND SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND ORGANIZATIONID=" + Session["OrgId"].ToString()));

                        Session["lockCT"] = lockCT;
                        // DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN User_Acc A ON (A.UA_NO = T.UA_NO OR A.UA_NO = T.ADTEACHER)", "T.CCODE+' - '+UPPER(A.UA_SHORTNAME)+(CASE WHEN ISNULL(T.UA_NO,0)<>0 THEN '<SPAN STYLE=" + "COLOR:#f20943;FONT-WEIGHT:BOLD" + "> $</SPAN>' WHEN ISNULL(T.UA_NO,0)=0 AND ISNULL(T.ADTEACHER,0)<>0 THEN ' <SPAN STYLE=" + "COLOR:#f8036b;FONT-WEIGHT:BOLD" + "> #</SPAN>' END)+' [SEC: '+ISNULL(DBO.FN_DESC('SECTIONNAME',T.SECTIONNO)COLLATE DATABASE_DEFAULT,'-')+']'+(CASE WHEN SUBID=2 THEN +'[BAT: '+ISNULL(DBO.FN_DESC('BATCH',T.BATCHNO)COLLATE DATABASE_DEFAULT,'-')+']' ELSE '' END)+' [RM:  '+ISNULL(DBO.FN_DESC('ROOM',T.ROOMNO)COLLATE DATABASE_DEFAULT,'-')+']'  FACULTY", "A.UA_NO,CT_NO", "SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SECTIONNO =" + ddlSection.SelectedValue+" AND ISNULL(CANCEL,0)=0" + "", "A.UA_SHORTNAME");
                        DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN User_Acc A ON (A.UA_NO = T.UA_NO OR A.UA_NO = T.ADTEACHER)", "UPPER(A.UA_FULLNAME) AS UA_FULLNAME,T.CCODE+' - '+UA_FULLNAME+(CASE WHEN ISNULL(T.UA_NO,0)<>0 THEN '<SPAN STYLE=" + "COLOR:#f20943;FONT-WEIGHT:BOLD" + "> $</SPAN>' WHEN ISNULL(T.UA_NO,0)=0 AND ISNULL(T.ADTEACHER,0)<>0 THEN ' <SPAN STYLE=" + "COLOR:#f8036b;FONT-WEIGHT:BOLD" + "> #</SPAN>' END)+' [SEC: '+ISNULL(DBO.FN_DESC('SECTIONNAME',T.SECTIONNO)COLLATE DATABASE_DEFAULT,'-')+']'+(CASE WHEN ((SUBID=2) OR (T.ORGANIZATIONID=2 AND SUBID IN (4,12,15)) OR (SUBID=1 AND  IS_TUTORIAL=1)) THEN +'[BAT: '+ISNULL(DBO.FN_DESC('BATCH',T.BATCHNO)COLLATE DATABASE_DEFAULT,'-')+']' ELSE '' END) FACULTY", "A.UA_NO,CT_NO", "SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SECTIONNO =" + ddlSection.SelectedValue + "AND COLLEGE_ID=" + ViewState["college_id"] + " AND ISNULL(CANCEL,0)=0" + " AND T.ORGANIZATIONID=" + Session["OrgId"].ToString(), "A.UA_FULLNAME");


                        if (ds.Tables[0].Rows.Count > 0 && ds != null)
                        {
                            if (lockCT == false)
                            {
                                //btnLock.Visible = true;
                                //  dListFaculty.DataSource = ds;
                                // dListFaculty.DataBind();
                                // divCourses.Visible = true;
                            }
                            else
                            {
                                //btnLock.Visible = false;
                                objCommon.DisplayMessage(this, "Time Table already locked! you cannot modified it.", this);
                                //  dListFaculty.DataSource = null;
                                //  dListFaculty.DataBind();
                                //  divCourses.Visible = false;
                                // divCourses.Visible = false;
                            }
                            // FillSlots();

                            //string revisedno = objCommon.LookUp("ACD_TIME_TABLE_CONFIG TT INNER JOIN ACD_COURSE_TEACHER CT ON CT.CT_NO=TT.CTNO INNER JOIN ACD_ACADEMIC_TT_SLOT TTS ON TTS.SLOTNO=TT.SLOTNO INNER JOIN ACD_SLOTTYPE ST ON ST.SLOTTYPENO=TTS.SLOTTYPE ", "DISTINCT max(isnull(REVISED_NO,0))", "ISNULL(TT.CANCEL,0)=0 AND TT.TIME_TABLE_DATE BETWEEN convert(Date,'" + (startdate) + "',103) and convert(Date,'" + (enddate) + "',103) AND REVISED_NO IS NOT NULL AND CT.SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SEMESTERNO=" + ddlSem.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND SLOTTYPE=" + ddlSlotType.SelectedValue);
                            //(string.IsNullOrEmpty(txtDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtDate.Text))

                            DateTime date1 = DateTime.MinValue;
                            // ShowFaculty(Convert.ToDateTime(startdate), Convert.ToDateTime(enddate), date1);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Course Teacher allotment Not Found for this selection !", this);
                            //dListFaculty.DataSource = null;
                            //dListFaculty.DataBind();
                            //divCourses.Visible = false;
                        }
                    }
                    //AttendanceConfigDate();
                }
                else
                {
                    //txtDate.Text = "";
                    // date = "";
                    //  ExisitingDates = "";
                    //  dListFaculty.DataSource = null;
                    // dListFaculty.DataBind();
                    // divCourses.Visible = false;
                    // divCourses.Visible = false;
                    // pnlSlots.Visible = false;
                    ddlSlotType.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please Select SlotType", this);
                //  dListFaculty.DataSource = null;
                // dListFaculty.DataBind();
                //  divCourses.Visible = false;
                //pnlSlots.Visible = false;s
                ddlSlotType.Focus();
                ddlExistingDates.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }
    #region "Time Table Report Format III"
    protected void btnReport_Click(object sender, EventArgs e)
    {
        AcdAttendanceController objAttCon = new AcdAttendanceController();
        DataSet ds = null;
        int SessionNo = 0, College_id = 0;

        SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        College_id = Convert.ToInt32(ViewState["college_id"]);

        ds = objAttCon.RetrieveStudentAttDetailsFormatIIIExcel();
        DataGrid dg = new DataGrid();

        if (ds.Tables[0].Rows.Count > 0)
        {
            //this.CallExcelIII();
            string attachment = "attachment; filename=MasterTimetable.xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("Record Not Found!!", this.Page);
            return;
        }
    }
    private void CallExcelIII()
    {
        string attachment = "attachment; filename=AttendanceDetails " + ddlSchoolInstitute.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + ".xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + ContentType;
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Response.Write(sw.ToString());
        Response.End();
    }
    #endregion

}
