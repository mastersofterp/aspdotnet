//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : TEACHER ALLOTMENT FOR REVALUATION                                      
// CREATION DATE : 04-APRIL-2023
// CREATED BY    : SACHIN A                                                
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_teacherallotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    //ddlSession.Items.Add(new ListItem(Session["sessionname"].ToString(), Session["currentsession"].ToString()));
                    //objCommon.FillDropDownList("Please Select", ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "FLOCK = 1", "SESSIONNO DESC"); 

                    ddlSession.SelectedIndex = 0;

                    //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1", "SESSIONNO DESC");

                    // objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
                    //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
                    //   objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 AND DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");

                    // this.PopulateDropDownList();

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
            }
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")// prog co-ordinator / faculty
            {
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                // string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                // objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND DEPTNO=" + Session["userdeptno"] + "", "SCHEMENO");
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                ViewState["DEPTNO"] = "0";
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");
                // objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0", "SCHEMENO");
            }
        }
        catch
        {
            throw;
        }

    }

    private void ClearDropDown(DropDownList ddlList)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlClgname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                }
            }
            ddlSemester.Focus();
            ClearDropDown(ddlCourse);
            ClearDropDown(ddlSection);
            ClearDropDown(ddlTeacher);
            ClearDropDown(ddlSession);
            ClearDropDown(ddlSemester);
            txtTotStud.Text = string.Empty;

            divlvStudentHeading.Visible = false;

            lvStudents.DataSource = null;
            lvStudents.DataBind();
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S WITH (NOLOCK) INNER JOIN ACD_REVAL_RESULT R ON (R.SESSIONNO=S.SESSIONNO)", "DISTINCT S.SESSIONNO", "SESSION_PNAME", "S.SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND S.COLLEGE_ID=" + ViewState["college_id"].ToString(), "S.SESSIONNO desc");

            ddlSession.Focus();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSession_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSchoolInstitute.SelectedIndex = 0;
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlTeacher);
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();

        // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.APP_TYPE LIKE '%REVAL%' ", "COURSE_NAME");
        ddlCourse.Focus();
        // objCommon.FillDropDownList(ddlSemester, "ACD_REVAL_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearDropDown(ddlCourse);
            ClearDropDown(ddlSection);
            ClearDropDown(ddlTeacher);

            divlvStudentHeading.Visible = false;

            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            ddlCourse.Focus();
            ClearDropDown(ddlTeacher);
        }
        else
        {
            ClearDropDown(ddlCourse);
            ClearDropDown(ddlTeacher);
        }


        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
    }



    protected void ddlCourse_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ClearDropDown(ddlTeacher);
        if (ddlCourse.SelectedIndex > 0)
        {

            this.FillTeacher();
            divlvStudDetails.Visible = false;
            divlvStudentHeading.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = string.Empty;
        }
    }

    protected void ddlThbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }

    private void FillTeacher()
    {
        // objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN USER_ACC UA WITH (NOLOCK) ON (CT.UA_NO=UA.UA_NO)", "DISTINCT CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.th_pr =" + ddlSubjectType.SelectedValue + " AND ISNULL(CANCEL,0)=0", "UA.UA_FULLNAME");
        objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "DISTINCT UA_NO", "(UA_FULLNAME  COLLATE DATABASE_DEFAULT + ' ' + '(' + UA_NAME COLLATE DATABASE_DEFAULT +')' ) UA_FULLNAME", "UA_NO > 0 AND UA_TYPE IN (3)", "UA_FULLNAME");
    }


    private void BindListView()
    {
        try
        {
            //Fill Teacher DropDown
            // this.FillTeacher();

            StudentController objSC = new StudentController();
            //DataSet ds = objSC.GetStudentsForTeacherAllotment(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddltheorypractical.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
            // DataSet ds = objSC.GetStudentsForTeacherAllotment(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlTutorial.SelectedValue));

            string sp_procedure = "PKG_STUDENT_REVAL_TEACHERALLOTMENT";
            string sp_parameters = "@P_SESSIONNO,@P_COURSENO";

            string sp_callValues = "" + Convert.ToInt16(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";
            DataSet ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label - 
                divlvStudDetails.Visible = true;
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.UpdatePanel1, "No Data Found.", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListView();

        if (lvStudents.Items.Count > 0)
        {
            divlvStudentHeading.Visible = true;
            divlvStudDetails.Visible = true;
        }
        else
        {
            divlvStudentHeading.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Validations
            if (ddlTeacher.SelectedIndex <= 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Teacher.", this.Page);
                return;
            }
            string stuids = string.Empty;
            StudentController objSC = new StudentController();
            Student_Acd objStudent = new Student_Acd();
            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "DISTINCT SCHEMENO", "COURSENO=" + ddlCourse.SelectedValue));

            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                    stuids += chkBox.ToolTip + "$";
            }

            if (stuids.Length <= 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Student.", this.Page);
                return;
            }
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            //if (objSC.UpdateStudent_TeachAllot(objStudent, OrgId) == Convert.ToInt32(CustomStatus.RecordUpdated))
            //    objCommon.DisplayMessage(this.UpdatePanel1, "Teacher Alloted Sucessfully..", this.Page);

            // string idnos = stuids.TrimEnd('$');

            string SP_Name = "PKG_STUDENT_REVALUATION_UPD_FACULTY";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_COURSENO,@P_UA_NO,@P_STUDID,@P_ORGID,@P_SUBMITTEDBY,@P_IPADDRESS,@P_OUT";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + schemeno + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlTeacher.SelectedValue) + "," + Convert.ToString(stuids) + "," + Convert.ToInt32(Session["OrgId"]) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + Convert.ToString(ViewState["ipAddress"]) + ",0";
            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            if (que_out != "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Teacher Alloted Sucessfully..", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Server Error", this.Page);
            }

            this.BindListView();

            //foreach (ListViewDataItem lvItem in lvStudents.Items)
            //{
            //    CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
            //    //CheckBox chkhead = lvItem.FindControl("cbHead") as CheckBox;
            //    chkBox.Checked = false;
            //    //if (chkhead.Checked == true) { chkhead.Checked = false; }
            //}
        }
        catch
        {
            throw;
        }
    }

}
