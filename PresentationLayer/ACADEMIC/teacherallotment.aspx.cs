//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE ALLOTMENT                                      
// CREATION DATE : 15-OCT-2011
// CREATED BY    :                                                 
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

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc");

                    ddlSession.SelectedIndex = 0;
                    if (Session["usertype"].ToString() == "1")
                    {
                        objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    }
                    else
                    {
                        // objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
                        //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
                        //   objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 AND DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
                    }

                    //if (Session["usertype"].ToString() == "1")
                    //{
                    //    divDegree.Visible = true;
                    //    divBranch.Visible = true;
                    //}
                    //else
                    //{
                    //    divDegree.Visible = false;
                    //    divBranch.Visible = false;
                    //}
                    this.PopulateDropDownList();
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
            }
        }
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //if ((e.Item.FindControl("lblAdTeacher") as Label).ToolTip != string.Empty)
        //{
        //    DataSet ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO IN(" + (e.Item.FindControl("lblAdTeacher") as Label).ToolTip + ")", "UA_NO");
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTableReader dtr = ds.Tables[0].CreateDataReader();
        //        while (dtr.Read())
        //        {
        //            (e.Item.FindControl("lblAdTeacher") as Label).Text += dtr["UA_FULLNAME"].ToString() + ",";
        //        }
        //        dtr.Close();
        //    }
        //}
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")// prog co-ordinator / faculty
            {
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                // string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                // objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO>0 AND DEPTNO=" + Session["userdeptno"] + "", "SCHEMENO");
                // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            }
            else
            {
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
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
            ClearDropDown(ddlBatch);
            ClearDropDown(ddlSession);
            ClearDropDown(ddlSemester);
            ClearDropDown(ddlSubjectType);
            txtTotStud.Text = string.Empty;

            divlvStudentHeading.Visible = false;

            lvStudents.DataSource = null;
            lvStudents.DataBind();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
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
        //ddlDepartment.SelectedIndex = 0;
        ClearDropDown(ddlDegree);
        ClearDropDown(ddlBranch);
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);
        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();

        //Commented by As per Requirement Of Romal Saluja Sir 

        //objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER S ON (CT.SEMESTERNO = S.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "S.SEMESTERNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0", "CT.SEMESTERNO");//AND SR.PREV_STATUS = 0

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));

        int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
        if (ds != null && ds.Tables.Count > 0)
        {
            ddlSemester.DataSource = ds;
            ddlSemester.DataTextField = "SEMESTERNAME";
            ddlSemester.DataValueField = "SEMESTERNO";
            ddlSemester.DataBind();
        }
        ddlSemester.Focus();
        //objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0", "A.DEPTNAME ASC");
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0 AND B.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + "", "A.DEPTNAME ASC");
            objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0 AND B.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "A.DEPTNAME ASC");

        }
        else
        {
            ddlDepartment.SelectedIndex = 0;
        }
        ClearDropDown(ddlDegree);
        ClearDropDown(ddlBranch);
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);
        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dept = string.Empty;
        if (Session["usertype"].ToString() != "1")
        {
            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");

            if (ddlDepartment.SelectedIndex > 0)
            {
                foreach (ListItem items in ddlDepartment.Items)
                {
                    if (items.Selected == true)
                    {
                        dept += items.Value + ',';
                    }
                }
                if (!dept.ToString().Equals(string.Empty) || !dept.ToString().Equals(""))
                    dept = dept.Remove(dept.Length - 1);
            }
            else
            {
            }
        }
        else
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        //objCommon.FillDropDownList(ddlDegree, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + " AND A.DEPTNO=" + ddlDepartment.SelectedValue, "B.DEGREENAME ASC");
        objCommon.FillDropDownList(ddlDegree, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + " AND A.DEPTNO IN(" + ddlDepartment.SelectedValue + ")", "B.DEGREENAME ASC");
        ClearDropDown(ddlBranch);
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);
        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegree.SelectedIndex > 0)
        //{
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + ddlDepartment.SelectedValue, "A.LONGNAME");
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN(" + ddlDepartment.SelectedItem + ")", "A.LONGNAME");
        //if (Session["usertype"].ToString() != "1")
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
        //else
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
        ddlBranch.Focus();
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);
        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);
        //}
        //else
        //{
        //    ddlBranch.Items.Clear();
        //    ddlDegree.SelectedIndex = 0;
        //}
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBranch.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + "AND DEGREENO= " + ddlDegree.SelectedValue, "SCHEMENO DESC");
        //    ddlScheme.Focus();
        //}
        //else
        //{
        //    ddlScheme.Items.Clear();
        //    ddlBranch.SelectedIndex = 0;
        //}
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO IN(" + ddlDepartment.SelectedItem + ")" + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENAME DESC");
        ddlScheme.Focus();
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);
        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);

        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlScheme.SelectedIndex > 0)
            //{
            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            ddlSemester.Focus();
            //}
            //else
            //{
            //    ddlSemester.Items.Clear();
            //    ddlScheme.SelectedIndex = 0;
            //}

            ClearDropDown(ddlCourse);
            ClearDropDown(ddlSection);
            ClearDropDown(ddlTeacher);
            ClearDropDown(ddlBatch);

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
        //if (ddlSemester.SelectedIndex > 0)
        //{
        //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
        //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO >0", "C.SUBID");
        objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]), "C.SUBID");
        ddlSubjectType.Focus();
        //}
        //else
        //{
        //    ddlSubjectType.Items.Clear();
        //    ddlSemester.SelectedIndex = 0;
        //}

        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);
        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);

        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
        //ddlSection.SelectedIndex = 0;
        //ddlSubjectType.SelectedIndex = 0;
        //ddlCourse.SelectedIndex = 0;
        //ddlTeacher.SelectedIndex = 0;
    }

    protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //if (ddlSemester.SelectedIndex > 0)
        //{
        //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME", "OFFERED = 1 AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue, "CCODE");
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME", "SR.SCHEMENO = " + ViewState["schemeno"] + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(SR.CANCEL,0) = 0", "COURSE_NAME");
        ddlCourse.Focus();
        int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
        if (ddlSubjectType.SelectedValue == "2" || (ddlSubjectType.SelectedValue == "12" && OrgId == 1))
        {
            dvBatch.Visible = true;
            rfvBatch.Visible = true;
        }
        else
        {
            dvBatch.Visible = false;
            rfvBatch.Visible = false;
        }
        ClearDropDown(ddlSection);
        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);

        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
        //ddlSection.SelectedIndex = 0;
        //ddlCourse.SelectedIndex = 0;
        //ddlTeacher.SelectedIndex = 0;
    }


    private DataSet GetDropDowns(int sessionno, int schemeno, int colgId, int sem, int courseno, int tutorial, int sectionno, int batchno, int flag)
    {
        string SP_Parameters = ""; string Call_Values = ""; string SP_Name = "";
        DataSet ds = new DataSet();
        SP_Name = "PKG_ACD_GET_SECTION_BATCH_FOR_TS";
        SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_COLLEGEID,@P_SEMESTERNO,@P_COURSENO,@P_ISTUTORIAL,@P_SECTIONNO,@P_BATCHNO,@P_FLAG";
        Call_Values = "" + sessionno + "," + schemeno + "," + colgId + "," + sem + "," + courseno + "," + tutorial + "," + sectionno + "," + batchno + "," + flag + "";
        ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        return ds;
    }

    protected void ddlCourse_SelectedIndexChanged1(object sender, EventArgs e)
    {

        ClearDropDown(ddlTeacher);
        ClearDropDown(ddlBatch);
        if (ddlCourse.SelectedIndex > 0)
        {
            //decimal tutorial = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "THEORY", "COURSENO=" + ddlCourse.SelectedValue));
            //if (tutorial > 0)
            //{
            //    divTutorial.Visible = true;
            //}
            //else
            //{
            //    divTutorial.Visible = false;
            //}
            ddlTutorial.Items.Clear();
            DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "ISNULL(THEORY,0) AS THEORY,TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlCourse.SelectedValue, "");
            if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2"))
            {
                divTutorial.Visible = false;
                ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                ddlTutorial.SelectedValue = "1";
            }
            else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2"))
            {
                if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                {
                    divTutorial.Visible = true;
                    ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorial.Items.Add(new ListItem("Tutorial", "3"));
                    ddlTutorial.SelectedValue = "1";
                }
                else
                {
                    divTutorial.Visible = false;
                    ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorial.SelectedValue = "1";
                }
            }
            else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1"))
            {
                divTutorial.Visible = false;
                ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                ddlTutorial.SelectedValue = "2";
            }
            else if ((dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "2") || (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["TH_PR"].ToString() == "1"))
            {
                if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                {
                    divTutorial.Visible = true;
                    ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorial.Items.Add(new ListItem("Tutorial", "3"));
                    ddlTutorial.SelectedValue = "2";
                }
                else
                {
                    divTutorial.Visible = false;
                    ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorial.SelectedValue = "2";
                }
            }
            else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0")
            {
                divTutorial.Visible = true;
                ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                ddlTutorial.SelectedValue = "1";
            }
            else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
            {
                if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                {
                    divTutorial.Visible = true;
                    ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorial.Items.Add(new ListItem("Tutorial", "3"));
                    ddlTutorial.SelectedValue = "1";
                }
                else
                {
                    divTutorial.Visible = true;
                    ddlTutorial.Items.Add(new ListItem("Theory", "1"));
                    ddlTutorial.Items.Add(new ListItem("Practical", "2"));
                    ddlTutorial.SelectedValue = "1";
                }
            }
            this.FillTeacher();
            //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN  ACD_STUDENT ST ON- SR.IDNO = ST.IDNo INNER JOIN  ACD_SECTION S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND SR.PREV_STATUS = 0", "S.SECTIONNO");
            //objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER CT INNER JOIN  ACD_SECTION S ON (CT.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO > 0 AND CT.SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorial.SelectedValue + "=3 THEN 1 ELSE 0 END) AND ISNULL(CANCEL,0)=0 AND CT.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorial.SelectedValue, "S.SECTIONNO");

            DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), 1);
            if (resultDataSet != null && resultDataSet.Tables[0].Rows.Count > 0)
            {
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                ddlSection.DataSource = resultDataSet;
                ddlSection.DataTextField = "SECTIONNAME";
                ddlSection.DataValueField = "SECTIONNO";
                ddlSection.DataBind();
                ddlSection.Focus();
            }

            //}
            //else
            //{

            //    ddlSection.Items.Clear();
            //    ddlCourse.SelectedIndex = 0;
            //}

            divlvStudentHeading.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            txtTotStud.Text = string.Empty;
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTeacher();

        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
        dvBatch.Visible = false;
        rfvBatch.Visible = false;
        ddltheorypractical.SelectedIndex = 0;
        int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
        int subid = ddlSubjectType.SelectedIndex > 0 ? Convert.ToInt32(ddlSubjectType.SelectedValue) : 0;

        DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "ISNULL(THEORY,0) AS THEORY,TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlCourse.SelectedValue, "");
        //if ((ddlTutorial.SelectedValue == "2" && subid == 1) || (OrgId == 1 && subid == 11) || (OrgId == 2 && (subid == 12 || subid == 4 || subid == 15)) || subid == 2)--commented by rahul
        if ((ddlTutorial.SelectedValue == "2"))//Added By Rahul
        {
            if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" || dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3")
            {
                //objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (B.BATCHNO = CT.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + "AND B.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(IS_TUTORIAL,0)=CASE WHEN " + ddlTutorial.SelectedValue + "=3 THEN 1 ELSE 0 END", "B.BATCHNO");
                DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), 2);
                if (resultDataSet != null && resultDataSet.Tables.Count > 0)
                {
                    ddlBatch.Items.Clear();
                    ddlBatch.Items.Add(new ListItem("Please Select", "0"));
                    ddlBatch.DataSource = resultDataSet;
                    ddlBatch.DataTextField = "BATCHNAME";
                    ddlBatch.DataValueField = "BATCHNO";
                    ddlBatch.DataBind();
                    ddlBatch.Focus();
                }
                dvBatch.Visible = true;
                rfvBatch.Visible = true;
            }
            else
            {
                dvBatch.Visible = false;
                rfvBatch.Visible = false;
            }
        }
        else if (ddlTutorial.SelectedValue == "3")
        {
            //objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (B.BATCHNO = CT.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + "AND B.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(IS_TUTORIAL,0)=CASE WHEN " + ddlTutorial.SelectedValue + "=3 THEN 1 ELSE 0 END", "B.BATCHNO");
            DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), 2);
            if (resultDataSet != null && resultDataSet.Tables.Count > 0)
            {
                ddlBatch.Items.Clear();
                ddlBatch.Items.Add(new ListItem("Please Select", "0"));
                ddlBatch.DataSource = resultDataSet;
                ddlBatch.DataTextField = "BATCHNAME";
                ddlBatch.DataValueField = "BATCHNO";
                ddlBatch.DataBind();
                ddlBatch.Focus();
            }
            dvBatch.Visible = true;
            rfvBatch.Visible = true;
        }
        else
        {
            if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2")
            {
                DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), 2);
                if (resultDataSet != null && resultDataSet.Tables.Count > 0)
                {
                    ddlBatch.Items.Clear();
                    ddlBatch.Items.Add(new ListItem("Please Select", "0"));
                    ddlBatch.DataSource = resultDataSet;
                    ddlBatch.DataTextField = "BATCHNAME";
                    ddlBatch.DataValueField = "BATCHNO";
                    ddlBatch.DataBind();
                    ddlBatch.Focus();
                }
                //objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (B.BATCHNO = CT.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + "AND B.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(IS_TUTORIAL,0)=CASE WHEN " + ddlTutorial.SelectedValue + "=3 THEN 1 ELSE 0 END", "B.BATCHNO");
                dvBatch.Visible = true;
                rfvBatch.Visible = true;
            }
            else
            {
                dvBatch.Visible = false;
                rfvBatch.Visible = false;
            }
        }


    }

    protected void ddlbatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
        ddlTeacher.Items.Clear();
        ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
        ddladteacher.Items.Clear();
        ddladteacher.Items.Add(new ListItem("Please Select", "0"));
        //objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN USER_ACC UA WITH (NOLOCK) ON (CT.UA_NO=UA.UA_NO)", "DISTINCT CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SUBID =" + ddlSubjectType.SelectedValue + " AND BATCHNO=" + ddlBatch.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorial.SelectedValue, "UA.UA_FULLNAME");
        DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), 3);
        if (resultDataSet != null && resultDataSet.Tables[0].Rows.Count > 0)
        {
            ddlTeacher.DataSource = resultDataSet.Tables[0];
            ddlTeacher.DataTextField = "UA_FULLNAME";
            ddlTeacher.DataValueField = "UA_NO";
            ddlTeacher.DataBind();
            ddlTeacher.Focus();
        }
        if (resultDataSet != null && resultDataSet.Tables[1].Rows.Count > 0)
        {
            ddladteacher.DataSource = resultDataSet.Tables[1];
            ddladteacher.DataTextField = "UA_FULLNAME";
            ddladteacher.DataValueField = "ADTEACHER";
            ddladteacher.DataBind();
            ddladteacher.Focus();
        }

        //objCommon.FillDropDownList(ddladteacher, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN USER_ACC UA WITH (NOLOCK) ON (CT.ADTEACHER=UA.UA_NO)", "DISTINCT CT.ADTEACHER", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SUBID =" + ddlSubjectType.SelectedValue + " AND BATCHNO=" + ddlBatch.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND IS_THPR_BOTH=" + ddlTutorial.SelectedValue, "UA.UA_FULLNAME");
    }

    protected void ddltheorypractical_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltheorypractical.SelectedValue == "2" || ddltheorypractical.SelectedValue == "3")
        {
            dvBatch.Visible = true;
            rfvBatch.Visible = true;

            if (ddlSection.SelectedIndex > 0 && ddltheorypractical.SelectedValue != "1")
            {
                if (ddltheorypractical.SelectedValue == "2")
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (B.BATCHNO = SR.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + "AND SR.SECTIONNO =" + ddlSection.SelectedValue, "B.BATCHNO");
                else
                    objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (B.BATCHNO = SR.TH_BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + "AND SR.SECTIONNO =" + ddlSection.SelectedValue, "B.BATCHNO");

                ddlBatch.Focus();
            }
            else
            {
                ddlSection.Items.Clear();
                ddlSection.SelectedIndex = 0;
                ddlBatch.Items.Clear();
                ddlBatch.SelectedIndex = 0;
            }
        }
        else
        {
            dvBatch.Visible = false;
            rfvBatch.Visible = false;
        }

        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        txtTotStud.Text = string.Empty;
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
        //objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN USER_ACC UA WITH (NOLOCK) ON (CT.UA_NO=UA.UA_NO)", "DISTINCT CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SUBID =" + ddlSubjectType.SelectedValue + " AND BATCHNO=" + ddlBatch.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND IS_THPR_BOTH=" + ddlTutorial.SelectedValue, "UA.UA_FULLNAME");
        //objCommon.FillDropDownList(ddladteacher, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN USER_ACC UA WITH (NOLOCK) ON (CT.ADTEACHER=UA.UA_NO)", "DISTINCT CT.ADTEACHER", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SUBID =" + ddlSubjectType.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND IS_THPR_BOTH=" + ddlTutorial.SelectedValue, "UA.UA_FULLNAME");
        DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), 3);
        if (resultDataSet != null && resultDataSet.Tables[0].Rows.Count > 0)
        {
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            ddlTeacher.DataSource = resultDataSet.Tables[0];
            ddlTeacher.DataTextField = "UA_FULLNAME";
            ddlTeacher.DataValueField = "UA_NO";
            ddlTeacher.DataBind();
            ddlTeacher.Focus();
        }
        if (resultDataSet != null && resultDataSet.Tables[1].Rows.Count > 0)
        {
            ddladteacher.Items.Clear();
            ddladteacher.Items.Add(new ListItem("Please Select", "0"));
            ddladteacher.DataSource = resultDataSet.Tables[1];
            ddladteacher.DataTextField = "UA_FULLNAME";
            ddladteacher.DataValueField = "ADTEACHER";
            ddladteacher.DataBind();
            ddladteacher.Focus();
        }
    }

    private void BindListView()
    {
        try
        {
            //Fill Teacher DropDown
            this.FillTeacher();

            StudentController objSC = new StudentController();
            //DataSet ds = objSC.GetStudentsForTeacherAllotment(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddltheorypractical.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue));
            DataSet ds = objSC.GetStudentsForTeacherAllotment(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlTutorial.SelectedValue));
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label - 
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

            StudentController objSC = new StudentController();
            Student_Acd objStudent = new Student_Acd();
            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objStudent.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objStudent.Sem = ddlSemester.SelectedValue;
            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
            //objStudent.Th_Pr = Convert.ToInt32(ddltheorypractical.SelectedValue);
            objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);
            objStudent.isTutorial = Convert.ToInt32(ddlTutorial.SelectedValue);
            objStudent.AdditionalTeacher = ddladteacher.SelectedValue;
            objStudent.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                    objStudent.StudId += chkBox.ToolTip + ",";
            }

            if (objStudent.StudId.Length <= 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Student.", this.Page);
                return;
            }
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            if (UpdateStudent_TeachAllot(objStudent, OrgId) == Convert.ToInt32(CustomStatus.RecordUpdated))
                objCommon.DisplayMessage(this.UpdatePanel1, "Teacher Alloted Sucessfully..", this.Page);
            else
                objCommon.DisplayMessage(this.UpdatePanel1, "Server Error", this.Page);

            this.BindListView();
        }
        catch
        {
            throw;
        }
    }



    protected void lvStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Faculty_Wise_Student_List", "rptFacultyWiseStudentList.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedIndex) > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0;
        int college_id = Convert.ToInt32(ViewState["college_id"]) > 0 ? Convert.ToInt32(ViewState["college_id"]) : 0;
        int degreeno = Convert.ToInt32(ViewState["degreeno"]) > 0 ? Convert.ToInt32(ViewState["degreeno"]) : 0;
        int branchno = Convert.ToInt32(ViewState["branchno"]) > 0 ? Convert.ToInt32(ViewState["branchno"]) : 0;
        int schemeno = Convert.ToInt32(ViewState["schemeno"]) > 0 ? Convert.ToInt32(ViewState["schemeno"]) : 0;
        int sectionno = Convert.ToInt32(ddlSection.SelectedIndex) > 0 ? Convert.ToInt32(ddlSection.SelectedValue) : 0;
        int subid = Convert.ToInt32(ddlSubjectType.SelectedIndex) > 0 ? Convert.ToInt32(ddlSubjectType.SelectedValue) : 0;
        int semesterno = Convert.ToInt32(ddlSemester.SelectedIndex) > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        int courseno = Convert.ToInt32(ddlCourse.SelectedIndex) > 0 ? Convert.ToInt32(ddlCourse.SelectedValue) : 0;
        int ua_no = Convert.ToInt32(ddlTeacher.SelectedIndex) > 0 ? Convert.ToInt32(ddlTeacher.SelectedValue) : 0;
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + sessionno + ",@P_COLLEGE_ID=" + college_id + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_SCHEMENO=" + schemeno + ",@P_SECTIONNO=" + sectionno + ",@P_COURSENO=" + courseno + ",@P_UA_NO=" + ua_no + ",@P_SEMESTERNO=" + semesterno + ",@P_SUBID=" + subid;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
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

    protected void ddlTutorial_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();

        ddltheorypractical.SelectedIndex = 0;
        //objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER CT INNER JOIN  ACD_SECTION S ON (CT.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO > 0 AND CT.SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(IS_TUTORIAL,0)=(CASE WHEN " + ddlTutorial.SelectedValue + "=3 THEN 1 ELSE 0 END) AND ISNULL(CANCEL,0)=0 AND ISNULL(IS_THPR_BOTH,0)=" + ddlTutorial.SelectedValue + "AND CT.COURSENO=" + ddlCourse.SelectedValue, "S.SECTIONNO");
        //ddlSection.Focus();
        DataSet resultDataSet = GetDropDowns(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), 1);
        if (resultDataSet != null && resultDataSet.Tables[0].Rows.Count > 0)
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.DataSource = resultDataSet;
            ddlSection.DataTextField = "SECTIONNAME";
            ddlSection.DataValueField = "SECTIONNO";
            ddlSection.DataBind();
            ddlSection.Focus();
        }
        if (ddlTutorial.SelectedValue == "2" || ddlTutorial.SelectedValue == "3")
        {
            dvBatch.Visible = true;
            rfvBatch.Visible = true;
        }
        else
        {
            dvBatch.Visible = false;
            rfvBatch.Visible = false;
            ddlBatch.SelectedIndex = 0;
        }

    }

    public int UpdateStudent_TeachAllot(Student_Acd objStudent, int OrgId)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = new SqlParameter[15]; objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
            objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
            objParams[2] = new SqlParameter("@P_SEMESTERNO", objStudent.Sem);
            objParams[3] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
            objParams[4] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
            objParams[5] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
            objParams[6] = new SqlParameter("@P_STUDID", objStudent.StudId);
            objParams[7] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
            objParams[8] = new SqlParameter("@P_ISTUTORIAL", objStudent.isTutorial);//Added by Dieep Kare on 28.01.2022
            objParams[9] = new SqlParameter("@P_ORGID", OrgId);
            objParams[10] = new SqlParameter("@P_SUBMITTEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
            objParams[11] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
            objParams[12] = new SqlParameter("@P_ADTEACHER", objStudent.AdditionalTeacher);
            objParams[13] = new SqlParameter("@P_BATCHNO", objStudent.BatchNo);  // Added By Rishabh B. on 19012024
            objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[14].Direction = ParameterDirection.Output;
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_BYFACULTY", objParams, false);
            if (ret != null)
                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
        }
        return retStatus;
    }
}
