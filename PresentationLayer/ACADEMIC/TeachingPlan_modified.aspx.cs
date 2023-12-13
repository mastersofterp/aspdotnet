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

using System.Text;
using System.Data.OleDb;
using System.Data.Common;
using ClosedXML.Excel;


public partial class ACADEMIC_TeachingPlan_modified : System.Web.UI.Page
{

    #region Page Events

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    CourseController objCC = new CourseController(); // For Global Elective
    TeachingPlanController objTeachingPlanController = new TeachingPlanController();

    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
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


                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["degreeno"] = 0;
                    ViewState["branchno"] = 0;
                    ViewState["college_id"] = 0;
                    ViewState["schemeno"] = 0;
                    this.PopulateDropDown();
                    //this.BindTeachingPlan();
                    divMsg.InnerHtml = string.Empty;
                    //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
                    //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021

                    if (Convert.ToInt32(Session["usertype"]) > 1)
                    {
                        admindegree.Visible = false;
                        adminBranch.Visible = false;
                        //adminScheme.Visible = false;
                    }
                }
                if (Session["usertype"].ToString() != "1")
                {
                    //lblHeading.Text = "Teaching Plan Entry";
                    trTeacher.Visible = false;
                    ddlUnitNodiv.Visible = true;
                    ddlLectureNodiv.Visible = true;
                    txtLectureTopicdiv.Visible = true;
                    //pnlUP.Visible = true;
                    Lecturedates.Visible = true;
                    lectureSlot.Visible = true;
                    rfvSection.Enabled = true;
                    spanSection.Visible = true;
                }
                else
                {
                    //lblHeading.Text = "Teaching Plan Report";
                    trTeacher.Visible = true;
                    ddlUnitNodiv.Visible = false;
                    ddlLectureNodiv.Visible = false;
                    txtLectureTopicdiv.Visible = false;
                    pnlUP.Visible = false;
                    Lecturedates.Visible = false;
                    lectureSlot.Visible = false;
                    btnSubmit.Visible = false;
                    rfvSection.Enabled = false;
                    spanSection.Visible = false;
                }
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            divMsg.InnerHtml = string.Empty;
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
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TeachingPlan.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=TeachingPlan.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_FULLNAME");
            if (Session["usertype"].ToString() != "1" && Session["usertype"].ToString() != "8")
            {
                //objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO) INNER JOIN ACD_COURSE_TEACHER CT ON(SM.COLLEGE_ID=CT.COLLEGE_ID AND CT.SCHEMENO = SC.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND ISNULL(CANCEL,0)=0", "COSCHNO"); //old scheme bind 

                //new Scheme bind added by Ro-hit M on 17-10-2023 as per ticket no  48557
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID) INNER JOIN ACD_COURSE_TEACHER CT ON(SM.COLLEGE_ID=CT.COLLEGE_ID AND CT.SESSIONNO = SM.SESSIONNO)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ") AND ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 AND ISNULL(CT.CANCEL,0)=0", "S.SESSIONID DESC");

                objCommon.FillDropDownList(ddlSessionGlobal, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID) INNER JOIN ACD_COURSE_TEACHER CT ON(SM.COLLEGE_ID=CT.COLLEGE_ID AND CT.SESSIONNO = SM.SESSIONNO)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ") AND ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 AND ISNULL(CT.CANCEL,0)=0", "S.SESSIONID DESC");
            }
            else
            {
                //objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO) INNER JOIN ACD_COURSE_TEACHER CT ON(SM.COLLEGE_ID=CT.COLLEGE_ID)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

                //new Scheme bind added by Ro-hit M on 17-10-2023 as per ticket no  48557
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID) INNER JOIN ACD_COURSE_TEACHER CT ON(SM.COLLEGE_ID=CT.COLLEGE_ID AND CT.SESSIONNO = SM.SESSIONNO)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "(CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ") AND ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 AND ISNULL(CT.CANCEL,0)=0", "S.SESSIONID DESC");

                objCommon.FillDropDownList(ddlSessionGlobal, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindTeachingPlan()
    {
        try
        {
            DataSet ds = new DataSet();
            int ua_no = ua_no = Convert.ToInt32(Session["userno"]);
            int tutorial = Convert.ToInt32(ddlTutorial.SelectedValue);

            ds = objTeachingPlanController.GetAllTEACHING_PLAN(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["semesterno"].ToString()), ua_no, Convert.ToInt32(ViewState["courseno"].ToString()), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlBatch.SelectedValue), tutorial, Convert.ToInt32(ViewState["college_id"].ToString()), Convert.ToInt32(Session["OrgId"]));
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvTeachingPlan.Visible = true;
                    lvTeachingPlan.DataSource = ds.Tables[0];
                    lvTeachingPlan.DataBind();
                }
                else
                {
                    lvTeachingPlan.Visible = false;
                    lvTeachingPlan.DataSource = null;
                    lvTeachingPlan.DataBind();
                }
                if (Convert.ToInt32(ds.Tables[1].Rows[0]["TP_FLAG"]) == 1)
                {
                    divTpSlots.Visible = true;
                }
            }
            else
            {
                lvTeachingPlan.Visible = false;
                lvTeachingPlan.DataSource = null;
                lvTeachingPlan.DataBind();
            }
        }
        catch
        {
            //  throw;
        }
    }

    #endregion

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubjectType.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        ddlSubjectType.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlUnitNo.SelectedIndex = -1;
        ddlUnitNo.SelectedIndex = -1;
        ddlLectureNo.SelectedIndex = -1;
        lvTeachingPlan.DataSource = null;
        lvTeachingPlan.DataBind();
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();

        objCommon.FillDropDownList(ddlScheme, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SC ON (SC.SCHEMENO = CT.SCHEMENO)", "DISTINCT SC.COSCHNO", "SC.COL_SCHEME_NAME", "CT.SESSIONNO  IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AND (CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "SC.COSCHNO");

        //objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "");
        // ddlSemester.Focus();
        ddlScheme.Focus();
        //objCommon.FillDropDownList(ddlScheme, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO = CT.SCHEMENO)", "DISTINCT CT.SCHEMENO", "SC.SCHEMENAME", "(CT.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + " ) AND CT.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "CT.SCHEMENO");

        //lvTeachingPlan.DataSource = null;
        //lvTeachingPlan.DataBind();
        //COMMENTED BY SUMIT ON 24-01-2020
        // ddlSemester.Focus();
    }
    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.SelectedIndex = -1;
        ddlSubjectType.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        ddlSection.SelectedIndex = -1;
        ddlBatch.SelectedIndex = -1;
        ddlUnitNo.SelectedIndex = -1;
        ddlLectureNo.SelectedIndex = -1;
        lvTeachingPlan.DataSource = null;
        lvTeachingPlan.DataBind();
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();
        if (ddlTeacher.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1" && Session["usertype"].ToString() != "8")
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.DEPTNO=" + Session["userdeptno"].ToString(), "D.DEGREENAME");
            }
            else
            {
                if (ddlTeacher.SelectedIndex > 0)
                {
                    int deptno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "ISNULL(UA_DEPTNO,0)", "UA_NO=" + Convert.ToInt32(ddlTeacher.SelectedValue)));
                    if (deptno != 0)
                    {
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND (CD.DEPTNO=" + deptno + " OR " + deptno + "=0)", "D.DEGREENAME");
                    }
                    else
                    {
                        ddlDegree.Items.Clear();
                        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                    }
                    //objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "(CT.UA_NO = " + Convert.ToInt32(ddlTeacher.SelectedValue) + " OR " + Convert.ToInt32(ddlTeacher.SelectedValue) + "=0)  AND CT.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue), "CT.SEMESTERNO");
                    objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "(CT.UA_NO = " + Convert.ToInt32(ddlTeacher.SelectedValue) + " OR CT.ADTEACHER = " + Convert.ToInt32(ddlTeacher.SelectedValue) + ") AND CT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"].ToString()), "CT.SEMESTERNO");
                }
                else
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "CT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"].ToString()), "CT.SEMESTERNO");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "D.DEGREENAME");
                }
            }
        }
        else
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0", "D.DEGREENAME");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"].ToString() + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");

                }
                else
                {
                    if (ddlTeacher.SelectedIndex > 0)
                    {
                        int deptno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "ISNULL(UA_DEPTNO,0)", "UA_NO=" + Convert.ToInt32(ddlTeacher.SelectedValue)));
                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"].ToString() + " AND (B.DEPTNO=" + deptno + " OR " + deptno + "=0)", "A.LONGNAME");
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ViewState["degreeno"].ToString(), "A.LONGNAME");
                    }
                }
                ddlBranch.Focus();
                BindTeachingPlan();
            }
            else
            {
                ddlBranch.SelectedIndex = 0;
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
            if (ddlBranch.SelectedIndex > 0)
            {
                if (ViewState["degreeno"].ToString() == "1" && ViewState["branchno"].ToString() == "99")
                {
                    ddlScheme.Items.Clear();
                    ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                    ddlScheme.Items.Add(new ListItem("FIRST YEAR [R.T.M]", "24"));
                    ddlScheme.Items.Add(new ListItem("FIRST YEAR [AUTONOMOUS]", "1"));
                    ddlScheme.Focus();
                }
                else
                {
                    objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ViewState["branchno"].ToString()), "B.BRANCHNO");
                    ddlScheme.Focus();
                    this.BindTeachingPlan();
                }
            }
            else
            {
                ddlScheme.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlSession.Items.Clear();
        //ddlSession.Items.Add(new ListItem("Please Select", "0"));
        //ddlTeacher.SelectedIndex = -1;
        //ddlSemester.Items.Clear();
        // ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        //ddlSubjectType.SelectedIndex = -1;
        //ddlCourse.SelectedIndex = -1;
        //ddlSection.Items.Clear();
        // ddlSection.Items.Add(new ListItem("Please Select", "0"));
        //ddlBatch.Items.Clear();
        // ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        // ddlUnitNo.SelectedIndex = -1;
        //ddlLectureNo.SelectedIndex = -1;
        if (ddlScheme.SelectedValue != "0")
        {
            lvTeachingPlan.DataSource = null;
            lvTeachingPlan.DataBind();
            lvTeachingPlanGlobalElective.DataSource = null;
            lvTeachingPlanGlobalElective.DataBind();
            if (ddlScheme.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlScheme.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                    //FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]));
                }
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO) INNER JOIN ACD_SUBJECTTYPE S ON (CT.SUBID = S.SUBID)INNER JOIN ACD_COURSE C ON (C.COURSENO = CT.COURSENO) INNER JOIN ACD_SCHEME SC ON (CT.SCHEMENO =SC.SCHEMENO)", "DISTINCT  CAST(CT.SEMESTERNO AS NVARCHAR(15))+' - '+CAST(CT.SUBID AS NVARCHAR(15))+' - '+CAST(C.COURSENO AS NVARCHAR(15)) AS NO", "SEM.SEMESTERNAME +' - '+S.SUBNAME+' - '+C.CCODE + ' - ' + C.COURSE_NAME AS ID", "(CT.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ") AND ISNULL(C.GLOBALELE,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND CT.SESSIONNO IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"].ToString()), "");
                }
                else
                {
                    if (ddlTeacher.SelectedIndex > 0)
                    {
                        //
                        //objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "(CT.UA_NO = " + Convert.ToInt32(ddlTeacher.SelectedValue) + " OR " + Convert.ToInt32(ddlTeacher.SelectedValue) + "=0)  AND CT.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue), "CT.SEMESTERNO");
                        objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "(CT.UA_NO = " + Convert.ToInt32(ddlTeacher.SelectedValue) + " OR CT.ADTEACHER = " + Convert.ToInt32(ddlTeacher.SelectedValue) + ") AND CT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"].ToString()), "CT.SEMESTERNO");
                    }
                    else
                    {
                        //objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "CT.SCHEMENO =" + Convert.ToInt32(ddlScheme.SelectedValue), "CT.SEMESTERNO");
                        objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "CT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"].ToString()), "CT.SEMESTERNO");
                    }
                }

                //objCommon.FillDropDownList(ddlSession, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=CT.SESSIONNO AND CT.COLLEGE_ID=SM.COLLEGE_ID)", "DISTINCT CT.SESSIONNO", "SM.SESSION_PNAME", "SM.SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND CT.COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
                //ddlSession.Focus();
                ddlSemester.Focus();
            }


            //this.BindTeachingPlan();
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        lvTeachingPlan.DataSource = null;
        lvTeachingPlan.DataBind();
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();
        //ddlTutorial.SelectedIndex = 0;

        if (ddlSemester.SelectedIndex > 0)
        {
            FillDropdownTutorial();
            string MSG = ddlSemester.SelectedValue.ToString();// Request.Form["msg"].ToString();
            string[] repoarray;
            repoarray = MSG.Split('-');
            string semesterno = repoarray[0].ToString();
            string SUBID = repoarray[1].ToString();
            string courseno = repoarray[2].ToString();
            ViewState["semesterno"] = semesterno;
            ViewState["SUBID"] = SUBID;
            ViewState["courseno"] = courseno;
            //objCommon.FillDropDownList(ddlTimeTable, "ACD_TIME_TABLE_CONFIG TT INNER JOIN ACD_COURSE_TEACHER CT ON (CT.CT_NO=TT.CTNO)", "DISTINCT CONCAT(CAST(START_DATE AS DATE),'-',CAST(END_DATE AS DATE)) AS TT_DATE", "CAST(CONVERT(VARCHAR(10) ,START_DATE,103) AS NVARCHAR(15)) + ' - '+ CAST(CONVERT(VARCHAR(10),END_DATE ,103) AS NVARCHAR(15))", "CT.COLLEGE_ID=" + ViewState["college_id"] + " AND CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SEMESTERNO=" + Convert.ToInt32(semesterno) + " AND SUBID=" + Convert.ToInt32(SUBID) + " AND CT.SCHEMENO=" + ViewState["schemeno"] + " AND ISNULL(TT.CANCEL,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND ISNULL(ISEXTERNAL,0)=0", "TT_DATE");
            ddlTimeTable.Focus();
            ddlTimeTable.Enabled = true;


            //ddlBatch.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;
            ddlUnitNo.SelectedIndex = -1;
            ddlLectureNo.SelectedIndex = -1;
            txtLectureTopic.Text = string.Empty;
            ddlMon.Enabled = false;
            ddlTues.Enabled = false;
            ddlWed.Enabled = false;
            ddlThurs.Enabled = false;
            ddlFri.Enabled = false;
            ddlSat.Enabled = false;
            ddlMonSlot.Enabled = false;
            ddlTueSlot.Enabled = false;
            ddlWedSlot.Enabled = false;
            ddlThusSlot.Enabled = false;
            ddlFriSlot.Enabled = false;
            ddlSatSlot.Enabled = false;
            lvTeachingPlan.DataSource = null;
            lvTeachingPlan.DataBind();
            lvTeachingPlanGlobalElective.DataSource = null;
            lvTeachingPlanGlobalElective.DataBind();

            FillSectionBatch();

            ddlSection.Focus();
            ddlSection.Enabled = true;
            ResetDateDropdown();
        }
        else
        {
            pnlUP.Visible = false;
            ddlTimeTable.Enabled = false;
            ddlTutorial.Items.Clear();
            ddlTutorial.Items.Add(new ListItem("Please Select", "0"));
        }
        ddlTutorial_SelectedIndexChanged(sender, e);
    }

    private void FillDropdownTutorial()
    {
        ddlTutorial.Items.Clear();
        ddlTutorial.Items.Add(new ListItem("Theory", "1"));
        ddlTutorial.Items.Add(new ListItem("Tutorial", "2"));
        ddlTutorial.Items.Add(new ListItem("Practical", "3"));
    }

    private void FillSectionBatch()
    {
        DataSet ds = objTeachingPlanController.FillSectionBatchTeachingPlan(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["courseno"]), Convert.ToInt32(ddlTutorial.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add("Select Section");
            ddlSection.SelectedItem.Value = "0";
            ddlSection.DataSource = ds.Tables[0];
            ddlSection.DataValueField = "SECTIONNO";
            ddlSection.DataTextField = "SECTIONNAME";
            ddlSection.DataBind();

        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add("Select Batch");
            ddlBatch.SelectedItem.Value = "0";
            ddlBatch.DataSource = ds.Tables[1];
            ddlBatch.DataValueField = "BATCHNO";
            ddlBatch.DataTextField = "BATCHNAME";
            ddlBatch.DataBind();
            Session["TimeTableDates"] = ds.Tables[1];
        }

        if (ds.Tables[2].Rows.Count > 0)
        {
            ViewState["TH_PR"] = ds.Tables[2].Rows[0]["THEORY_PRAC"].ToString(); //Use for Theory or practical - course type
        }
        if (ds.Tables[3].Rows.Count > 0 && ds.Tables[3].Rows.Count != null)
        {
            //ViewState["TUTORIAL_COURSE"] = Convert.ToDecimal(ds.Tables[3].Rows[0]["THEORY"].ToString());
            ViewState["IS_THEORY"] = Convert.ToInt32(ds.Tables[3].Rows[0]["THEORY"]);
            ViewState["IS_PRACTICAL"] = Convert.ToInt32(ds.Tables[3].Rows[0]["PRACTICAL"]);
            ViewState["IS_TUTORIAL"] = Convert.ToInt32(ds.Tables[3].Rows[0]["TUTORIAL"]);

            if (Convert.ToInt32(ViewState["IS_THEORY"]) > 0 && Convert.ToInt32(ViewState["IS_PRACTICAL"]) > 0 && Convert.ToInt32(ViewState["IS_TUTORIAL"]) > 0)  //theory+prac+tutorial
            {
                dvTutorial.Visible = true;
            }
            else if (Convert.ToInt32(ViewState["IS_THEORY"]) > 0 && Convert.ToInt32(ViewState["IS_TUTORIAL"]) > 0)  //theory++tutorial
            {
                dvTutorial.Visible = true;
                ddlTutorial.Items.Remove(ddlTutorial.Items.FindByValue("3"));
            }
            else if (Convert.ToInt32(ViewState["IS_PRACTICAL"]) > 0 && Convert.ToInt32(ViewState["IS_TUTORIAL"]) > 0)  //practical+tutorial
            {
                dvTutorial.Visible = true;
                ddlTutorial.Items.Remove(ddlTutorial.Items.FindByValue("1"));
            }
            else if (Convert.ToInt32(ViewState["IS_PRACTICAL"]) > 0 && Convert.ToInt32(ViewState["IS_THEORY"]) > 0)  //practical+theory
            {
                dvTutorial.Visible = true;
                ddlTutorial.Items.Remove(ddlTutorial.Items.FindByValue("2"));
            }
            else
            {
                dvTutorial.Visible = false;
            }

            if (ViewState["TH_PR"] == "2" || (Convert.ToDecimal(ViewState["IS_TUTORIAL"]) > 0 && Convert.ToInt32(ddlTutorial.SelectedValue) != 1))
            {
                rfvBatch.Visible = false;
                ddlBatch.Visible = false;
                rfvSection.Visible = true;
                ddlSection.Visible = true;
                lblBatch.Visible = false;
            }
            else
            {
                rfvSection.Visible = true;
                ddlSection.Visible = true;
                rfvBatch.Visible = false;
                ddlBatch.Visible = false;
                lblBatch.Visible = false;
            }
        }
        if (ds.Tables[4].Rows.Count > 0 && ds.Tables[4].Rows.Count != null)
        {
            Session["TimeTableDates"] = ds;
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourse.SelectedIndex = -1;
        ddlSection.SelectedIndex = -1;
        ddlBatch.SelectedIndex = -1;
        ddlUnitNo.SelectedIndex = -1;
        ddlLectureNo.SelectedIndex = 0;
        lvTeachingPlan.DataSource = null;
        lvTeachingPlan.DataBind();
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();
        if (ddlSubjectType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(Session["usertype"]) > 1)
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO = CT.COURSENO) INNER JOIN ACD_SCHEME SC ON (CT.SCHEMENO =SC.SCHEMENO)", "DISTINCT C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSENAME", "(CT.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + " ) AND C.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + "AND C.SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + "AND C.SUBID =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND ISNULL(CT.CANCEL,0)=0", "C.COURSENO");

            }
            else
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO = CT.COURSENO) INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON C.SEMESTERNO = SM.SEMESTERNO INNER JOIN ACD_ELECTGROUP G ON C.GROUPNO = G.GROUPNO INNER JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO ", "C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSENAME", "(CT.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + "AND C.SUBID =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + "AND C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"].ToString()) + "AND C.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue) + "AND ISNULL(CT.CANCEL,0)=0", "C.CCODE");
            }
        }
        else
        {
            ddlCourse.SelectedIndex = 0;
        }

        lvTeachingPlan.DataSource = null;
        lvTeachingPlan.DataBind();
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();
        ddlCourse.Focus();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvTeachingPlan.DataSource = null;
        lvTeachingPlan.DataBind();
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();
        ddlTutorial.SelectedIndex = 0;
        if (ddlCourse.SelectedIndex > 0)
        {
            char ch = '-';
            string[] course = ddlCourse.SelectedItem.Text.Split(ch);
            string ccode = course[0].Trim().ToString();
            //objCommon.FillDropDownList(ddlTimeTable, "ACD_TIME_TABLE_CONFIG TT INNER JOIN ACD_COURSE_TEACHER CT ON (CT.CT_NO=TT.CTNO)", " DISTINCT CAST(START_DATE AS DATE) START_DATE", "CAST(CONVERT(VARCHAR(10) ,START_DATE,103) AS NVARCHAR(15)) + ' - '+ CAST(CONVERT(VARCHAR(10),END_DATE ,103) AS NVARCHAR(15))", "CT.COLLEGE_ID=" + ViewState["college_id"] + " AND CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SEMESTERNO=" + ddlSemester.SelectedValue + " AND SUBID=" + ddlSubjectType.SelectedValue + " AND CT.SCHEMENO=" + ViewState["schemeno"] + " AND ISNULL(TT.CANCEL,0)=0 AND ISNULL(CT.CANCEL,0)=0", "START_DATE");
            ddlTimeTable.Focus();
            decimal tutorial = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "THEORY", "COURSENO=" + ddlCourse.SelectedValue));
            if (tutorial > 0)
            {
                dvTutorial.Visible = true;
            }
            else
            {
                dvTutorial.Visible = false;
            }
            int subId = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "DISTINCT SUBID", "COURSENO = " + ddlCourse.SelectedValue));
            if (subId == 2)
            {
                rfvBatch.Visible = true;
                ddlBatch.Visible = true;
                rfvSection.Visible = true;
                ddlSection.Visible = true;
                lblBatch.Visible = true;

            }
            else
            {
                rfvSection.Visible = true;
                ddlSection.Visible = true;
                rfvBatch.Visible = false;
                ddlBatch.Visible = false;
                lblBatch.Visible = false;
                //if (Session["usertype"].ToString() != "1")
                //{
                //    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER R, ACD_SECTION S", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO = R.SECTIONNO AND SESSIONNO = " + ddlSession.SelectedValue + " AND (UA_NO = " + Session["userno"].ToString() + " OR ADTEACHER = " + Session["userno"].ToString() + ") AND COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
                //}
                //else
                //{
                //    objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER R, ACD_SECTION S", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO = R.SECTIONNO AND SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
                //}

                //ddlSection.Focus();
            }

            if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 9)
            {
                string[] code = ddlCourse.SelectedItem.Text.Split('-');
                int count = Convert.ToInt32(objCommon.LookUp("ACD_GP_MASTER", "COUNT(1)", " CCODE='" + code[0] + "'"));
                if (count > 1)
                {
                    rfvBatch.Visible = true;
                    ddlBatch.Visible = true;
                    rfvSection.Visible = true;
                    ddlSection.Visible = true;
                    lblBatch.Visible = true;
                    if (ddlSemester.SelectedValue == "1")
                    {
                        objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER R, ACD_SECTION S", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO = R.SECTIONNO AND SESSIONNO = " + ddlSession.SelectedValue + " AND (UA_NO = " + Session["userno"].ToString() + " OR ADTEACHER = " + Session["userno"].ToString() + ") AND COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
                        //objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER R, ACD_BATCH B", "DISTINCT B.BATCHNO", "B.BATCHNAME", "B.BATCHNO = R.BATCHNO AND R.SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO = " + Session["userno"].ToString() + " AND CCODE = '" + ccode + "'", "B.BATCHNO");
                        objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER R INNER JOIN ACD_STUDENT_RESULT SR ON (R.SESSIONNO=SR.SESSIONNO AND R.COURSENO=SR.COURSENO AND R.SCHEMENO=SR.SCHEMENO AND R.SEMESTERNO=SR.SEMESTERNO AND R.SECTIONNO=SR.SECTIONNO) INNER JOIN ACD_BATCH B ON(SR.BATCHNO=B.BATCHNO)", "DISTINCT SR.BATCHNO", "B.BATCHNAME", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND (R.UA_NO = " + Session["userno"].ToString() + " OR R.ADTEACHER = " + Session["userno"].ToString() + ") AND R.COURSENO = " + ddlCourse.SelectedValue + "", "");
                    }
                    else
                    {
                        //objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER R, ACD_SECTION S", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO = R.SECTIONNO AND SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO = " + Session["userno"].ToString() + " AND COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
                        if (Session["usertype"].ToString() != "1")
                        {
                            objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER R, ACD_SECTION S", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO = R.SECTIONNO AND SESSIONNO = " + ddlSession.SelectedValue + " AND (UA_NO = " + Session["userno"].ToString() + " OR ADTEACHER = " + Session["userno"].ToString() + ") AND COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER R, ACD_SECTION S", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "S.SECTIONNO = R.SECTIONNO AND SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue, "S.SECTIONNO");
                        }
                        //objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER R, ACD_BATCH B", "DISTINCT B.BATCHNO", "B.BATCHNAME", "B.BATCHNO = R.BATCHNO AND R.SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO = " + Session["userno"].ToString() + " AND COURSENO = " + ddlCourse.SelectedValue, "B.BATCHNO");
                        objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER R INNER JOIN ACD_STUDENT_RESULT SR ON (R.SESSIONNO=SR.SESSIONNO AND R.COURSENO=SR.COURSENO AND R.SCHEMENO=SR.SCHEMENO AND R.SEMESTERNO=SR.SEMESTERNO AND R.SECTIONNO=SR.SECTIONNO) INNER JOIN ACD_BATCH B ON(SR.BATCHNO=B.BATCHNO)", "DISTINCT SR.BATCHNO", "B.BATCHNAME", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND (R.UA_NO = " + Session["userno"].ToString() + " OR R.ADTEACHER = " + Session["userno"].ToString() + ") AND R.COURSENO = " + ddlCourse.SelectedValue + "", "");
                    }
                    ddlSection.Focus();
                }
            }
            objCommon.FillDropDownList(ddlDegreeEX, "ACD_DEGREE D INNER JOIN ACD_SCHEME S ON(S.DEGREENO=D.DEGREENO)", "d.DEGREENO", "DEGREENAME", "d.DEGREENO > 0 AND S.SCHEMENO  IN (SELECT SCHEMENO FROM ACD_COURSE WHERE COURSENO=" + ddlCourse.SelectedValue + ")", "d.DEGREENO");

        }
        else
        {
            pnlUP.Visible = false;
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add(new ListItem("Please Select", "0"));
        //ddlTimeTable.Items.Clear();
        //ddlTimeTable.Items.Add("Please Select");
        if (ddlBatch.Visible == true)
        {
            ddlBatch.Focus();
        }
        else
        {
            ddlUnitNo.Focus();
            if (Session["usertype"].ToString() != "1")
            {
                pnlUP.Visible = true;
            }
            else
            {
                pnlUP.Visible = false;
            }
        }

        this.ResetDropdown();
        ddlTimeTable.SelectedIndex = 0;

        if (ddlSection.SelectedIndex > 0 && (Convert.ToInt32(ViewState["IS_PRACTICAL"]) > 0 || Convert.ToInt32(ViewState["IS_TUTORIAL"]) > 0) && (Convert.ToInt32(ddlTutorial.SelectedValue) != 1 || (Convert.ToInt32(ViewState["IS_TUTORIAL"]) > 0 && Convert.ToInt32(ViewState["IS_PRACTICAL"]) == 0 && Convert.ToInt32(ViewState["IS_THEORY"]) == 0) || (Convert.ToInt32(ViewState["IS_TUTORIAL"]) == 0 && Convert.ToInt32(ViewState["IS_PRACTICAL"]) > 0 && Convert.ToInt32(ViewState["IS_THEORY"]) == 0)))
        {
            char ch = '-';
            string[] course = ddlSemester.SelectedItem.Text.Split(ch);
            string ccode = course[2].Trim().ToString();

            //objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER R INNER JOIN ACD_BATCH B ON(R.BATCHNO=B.BATCHNO)", "DISTINCT B.BATCHNO", "B.BATCHNAME", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND (R.UA_NO = " + Session["userno"].ToString() + " OR R.ADTEACHER ='" + Session["userno"].ToString() + "') AND R.COURSENO = " + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND R.SECTIONNO=" + ddlSection.SelectedValue + " AND R.COLLEGE_ID= " + ViewState["college_id"].ToString(), "");

            BindBatch(ddlBatch, "BATCHNAME", "BATCHNO", "SECTIONNO=" + ddlSection.SelectedValue, 1);
            //Session["TimeTableDates"]

            lblBatch.Visible = true;
            ddlBatch.Visible = true;
            ddlBatch.Focus();
        }
        else
        {
            lblBatch.Visible = false;
            ddlBatch.Visible = false;
        }
        if (Session["usertype"].ToString() != "1")
        {
            this.BindTeachingPlan();
            this.BindTopiccodeUnit();
            btnLock.Enabled = false;
            this.CheckLockStatus();
        }


        //Session["TimeTableDates"]
        if (ddlSection.SelectedIndex > 0)
        {
            BindDropDowns(ddlTimeTable, "TIME_TABLE_DATE", "TT_DATE", "SECTIONNO=" + ddlSection.SelectedValue, 4);
        }
    }


    public void BindDropDowns(DropDownList ddl, string columnname, string columno, string expression, int table)
    {
        DataSet ds = (DataSet)Session["TimeTableDates"];
        DataTable distinctValues = ds.Tables[table].Select(expression).CopyToDataTable();
        DataView view = new DataView(distinctValues);
        distinctValues = view.ToTable(true, columno, columnname);
        ddl.Items.Clear();
        ddl.Items.Add("Please Select");
        ddl.SelectedItem.Value = "0";
        ddl.DataSource = distinctValues;
        ddl.DataTextField = distinctValues.Columns[1].ToString();
        ddl.DataValueField = distinctValues.Columns[0].ToString();
        ddl.DataBind();
        ViewState["RECORDCHECK"] = null;
    }
    public void BindBatch(DropDownList ddl, string columnname, string columno, string expression, int table)
    {
        DataSet ds = (DataSet)Session["TimeTableDates"];
        DataTable distinctValues = ds.Tables[table].Select(expression).CopyToDataTable();
        DataView view = new DataView(distinctValues);
        distinctValues = view.ToTable(true, columno, columnname);
        ddl.Items.Clear();
        ddl.Items.Add("Please Select");
        ddl.SelectedItem.Value = "0";
        ddl.DataSource = distinctValues;
        ddl.DataTextField = distinctValues.Columns[1].ToString();
        ddl.DataValueField = distinctValues.Columns[0].ToString();
        ddl.DataBind();
        ViewState["RECORDCHECK"] = null;
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ResetDropdown();
        ddlTimeTable.SelectedIndex = 0;
        this.BindTeachingPlan();
        this.BindTopiccodeUnit();
        btnLock.Enabled = false;
        //this.CheckLockStatus();
        if (ddlBatch.SelectedIndex > 0 && ddlSection.SelectedIndex > 0)
        {
            BindDropDowns(ddlTimeTable, "TIME_TABLE_DATE", "TT_DATE", "SECTIONNO=" + ddlSection.SelectedValue + " AND BATCHNO=" + ddlBatch.SelectedValue, 4);
        }

    }

    protected void ddlTimeTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTimeTable.SelectedIndex > 0)
        {
            this.ResetDropdown();
            if (ddlSection.SelectedIndex > 0)
            {
                string startDate; string endDate;
                string course = ddlSemester.SelectedItem.ToString();
                string[] coursecode = course.Split('-');
                string ccode = coursecode[2].Trim();
                DataSet ds = null;
                int Sessionno = ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0;
                int Sectionno = ddlSection.SelectedIndex > 0 ? Convert.ToInt32(ddlSection.SelectedValue) : 0;
                int batchno = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
                int Courseno = !ViewState["courseno"].ToString().Equals(string.Empty) ? Convert.ToInt32(ViewState["courseno"].ToString()) : 0;
                int Subid = !ViewState["SUBID"].ToString().Equals(string.Empty) ? Convert.ToInt32(ViewState["SUBID"].ToString()) : 0;
                int Schemeno = ddlScheme.SelectedIndex > 0 ? Convert.ToInt32(ViewState["schemeno"].ToString()) : 0;
                int Semesterno = !ViewState["semesterno"].ToString().Equals(string.Empty) ? Convert.ToInt32(ViewState["semesterno"].ToString()) : 0;
                int ua_no = Convert.ToInt32(Session["userno"].ToString());
                int College_id = Convert.ToInt32(ViewState["college_id"].ToString());
                string[] ttDates = ddlTimeTable.SelectedItem.Text.Split('-');
                startDate = ttDates[0].Trim();
                endDate = ttDates[1].Trim();
                ds = objTeachingPlanController.GetDayTimeSlots(Sessionno, Semesterno, ua_no, Sectionno, Courseno, Subid, Schemeno, startDate, endDate, batchno, College_id);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    int mon = 0, tue = 0, wed = 0, thu = 0, fri = 0, sat = 0;
                    if (!(ds.Tables[0].Rows[0]["DAYNO"].ToString() == string.Empty))
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 1)
                                mon = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 1 ? 1 : 0;
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 2)
                                tue = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 2 ? 1 : 0;
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 3)
                                wed = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 3 ? 1 : 0;
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 4)
                                thu = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 4 ? 1 : 0;
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 5)
                                fri = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 5 ? 1 : 0;
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 6)
                                sat = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 6 ? 1 : 0;
                        }
                    }

                    DataSet ds1 = objCommon.FillDropDown("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME SC ON A.SCHEMETYPE=SC.SCHEMETYPE", "START_DATE", "END_DATE", "SESSIONNO  IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AND A.SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"].ToString()) + "AND A.DEGREENO=" + ViewState["degreeno"].ToString() + " AND SC.SCHEMENO=" + ViewState["schemeno"].ToString() + "  AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        string stdate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(ds1.Tables[0].Rows[0]["START_DATE"].ToString()));
                        string enddate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(ds1.Tables[0].Rows[0]["END_DATE"].ToString()));
                        string[] dates = ddlTimeTable.SelectedItem.Text.Split('-');
                        stdate = dates[0].Trim();
                        enddate = dates[1].Trim();
                        if (mon == 1)
                        {
                            // for Monday, no. is 1
                            this.FillDatesDropDown(ddlMon, 1, stdate, enddate);
                            this.FillSlotDropDown(ddlMonSlot, "SLOT1", 1, stdate, enddate);
                            ddlMon.Enabled = true;
                        }
                        if (tue == 1)
                        {
                            // for Tuesday, no. is 2
                            this.FillDatesDropDown(ddlTues, 2, stdate, enddate);
                            this.FillSlotDropDown(ddlTueSlot, "SLOT2", 2, stdate, enddate);
                            ddlTues.Enabled = true;
                        }
                        if (wed == 1)
                        {
                            // for Wednesday, no. is 3
                            this.FillDatesDropDown(ddlWed, 3, stdate, enddate);
                            this.FillSlotDropDown(ddlWedSlot, "SLOT3", 3, stdate, enddate);
                            ddlWed.Enabled = true;
                        }
                        if (thu == 1)
                        {
                            // for Thursday, no. is 4
                            this.FillDatesDropDown(ddlThurs, 4, stdate, enddate);
                            this.FillSlotDropDown(ddlThusSlot, "SLOT4", 4, stdate, enddate);
                            ddlThurs.Enabled = true;
                        }
                        if (fri == 1)
                        {
                            // for Friday, no. is 5
                            this.FillDatesDropDown(ddlFri, 5, stdate, enddate);
                            this.FillSlotDropDown(ddlFriSlot, "SLOT5", 5, stdate, enddate);
                            ddlFri.Enabled = true;
                        }
                        if (sat == 1)
                        {
                            // for Saturday, no. is 6
                            this.FillDatesDropDown(ddlSat, 6, stdate, enddate);
                            this.FillSlotDropDown(ddlSatSlot, "SLOT6", 6, stdate, enddate);
                            ddlSat.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                lblBatch.Visible = false;
                ddlBatch.Visible = false;
            }
        }
        else
        {
            this.ResetDropdown();
        }
    }


    protected void ddlTutorial_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSection.Items.Clear();
        ddlSection.Items.Add("Please Select");
        ddlBatch.Items.Clear();
        ddlBatch.Items.Add("Please Select");
        //ddlTimeTable.Items.Clear();
        //ddlTimeTable.Items.Add("Please Select");
        ddlUnitNo.SelectedIndex = -1;
        ddlLectureNo.SelectedIndex = -1;
        txtLectureTopic.Text = string.Empty;
        ddlMon.Enabled = false;
        ddlTues.Enabled = false;
        ddlWed.Enabled = false;
        ddlThurs.Enabled = false;
        ddlFri.Enabled = false;
        ddlSat.Enabled = false;
        ddlMonSlot.Enabled = false;
        ddlTueSlot.Enabled = false;
        ddlWedSlot.Enabled = false;
        ddlThusSlot.Enabled = false;
        ddlFriSlot.Enabled = false;
        ddlSatSlot.Enabled = false;
        lvTeachingPlan.DataSource = null;
        lvTeachingPlan.DataBind();
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();

        if (ddlTutorial.SelectedValue == "2")
        {
            rfvBatch.Visible = true;
            ddlBatch.Visible = true;
            rfvSection.Visible = true;
            ddlSection.Visible = true;
            lblBatch.Visible = true;
            FillSectionBatch();
        }
        else
        {
            rfvBatch.Visible = true;
            ddlBatch.Visible = true;
            rfvSection.Visible = true;
            ddlSection.Visible = true;
            lblBatch.Visible = false;
            FillSectionBatch();
        }
    }
    private void FillDatesDropDown(DropDownList ddldate, int day, string stdate, string enddate)
    {
        DataSet ds = objTeachingPlanController.GetTeachingPlanDate(stdate, enddate, day, Convert.ToInt32(ViewState["college_id"].ToString()));

        ddldate.Items.Clear();
        ddldate.Items.Add("Please Select");
        ddldate.SelectedItem.Value = "0";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldate.DataSource = ds;
            ddldate.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddldate.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddldate.DataBind();
            ddldate.SelectedIndex = 0;
        }

        ddldate.Enabled = true;
    }

    private void ResetDateDropdown()
    {
        //if (ddlMon.Enabled == true)
        //    ddlMon.SelectedIndex = 0;
        //if (ddlTues.Enabled == true)
        //    ddlTues.SelectedIndex = 0;
        //if (ddlWed.Enabled == true)
        //    ddlWed.SelectedIndex = 0;
        //if (ddlThurs.Enabled == true)
        //    ddlThurs.SelectedIndex = 0;
        //if (ddlFri.Enabled == true)
        //    ddlFri.SelectedIndex = 0;
        //if (ddlSat.Enabled == true)
        //    ddlSat.SelectedIndex = 0;

        //if (ddlMonSlot.Enabled == true)
        //    ddlMonSlot.SelectedIndex = 0;
        //if (ddlTueSlot.Enabled == true)
        //    ddlTueSlot.SelectedIndex = 0;
        //if (ddlWedSlot.Enabled == true)
        //    ddlWedSlot.SelectedIndex = 0;
        //if (ddlThusSlot.Enabled == true)
        //    ddlThusSlot.SelectedIndex = 0;
        //if (ddlFriSlot.Enabled == true)
        //    ddlFriSlot.SelectedIndex = 0;
        //if (ddlSatSlot.Enabled == true)
        //    ddlSatSlot.SelectedIndex = 0;

        if (ddlMon.Enabled == false)
            ddlMon.SelectedIndex = 0;
        if (ddlTues.Enabled == false)
            ddlTues.SelectedIndex = 0;
        if (ddlWed.Enabled == false)
            ddlWed.SelectedIndex = 0;
        if (ddlThurs.Enabled == false)
            ddlThurs.SelectedIndex = 0;
        if (ddlFri.Enabled == false)
            ddlFri.SelectedIndex = 0;
        if (ddlSat.Enabled == false)
            ddlSat.SelectedIndex = 0;

        if (ddlMonSlot.Enabled == false)
            ddlMonSlot.SelectedIndex = 0;
        if (ddlTueSlot.Enabled == false)
            ddlTueSlot.SelectedIndex = 0;
        if (ddlWedSlot.Enabled == false)
            ddlWedSlot.SelectedIndex = 0;
        if (ddlThusSlot.Enabled == false)
            ddlThusSlot.SelectedIndex = 0;
        if (ddlFriSlot.Enabled == false)
            ddlFriSlot.SelectedIndex = 0;
        if (ddlSatSlot.Enabled == false)
            ddlSatSlot.SelectedIndex = 0;

        txtSessionReq.Text = string.Empty;
        lblDate.Visible = false;
        datelbl.Visible = false;
        lblSlot.Visible = false;
    }

    protected void ddlMon_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTues.Enabled == true)
            ddlTues.SelectedIndex = 0;
        if (ddlWed.Enabled == true)
            ddlWed.SelectedIndex = 0;
        if (ddlThurs.Enabled == true)
            ddlThurs.SelectedIndex = 0;
        if (ddlFri.Enabled == true)
            ddlFri.SelectedIndex = 0;
        if (ddlSat.Enabled == true)
            ddlSat.SelectedIndex = 0;

        if (ddlMon.SelectedIndex > 0)
        {
            ddlMonSlot.Enabled = true;
            ddlTueSlot.Enabled = false;
            if (ddlTueSlot.SelectedIndex > 0)
                ddlTueSlot.SelectedIndex = 0;
            ddlWedSlot.Enabled = false;
            if (ddlWedSlot.SelectedIndex > 0)
                ddlWedSlot.SelectedIndex = 0;
            ddlThusSlot.Enabled = false;
            if (ddlThusSlot.SelectedIndex > 0)
                ddlThusSlot.SelectedIndex = 0;
            ddlFriSlot.Enabled = false;
            if (ddlFriSlot.SelectedIndex > 0)
                ddlFriSlot.SelectedIndex = 0;
            ddlSatSlot.Enabled = false;
            if (ddlSatSlot.SelectedIndex > 0)
                ddlSatSlot.SelectedIndex = 0;
        }
        BindTeachingPlan();
    }

    protected void ddlTues_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMon.Enabled == true)
            ddlMon.SelectedIndex = 0;
        if (ddlWed.Enabled == true)
            ddlWed.SelectedIndex = 0;
        if (ddlThurs.Enabled == true)
            ddlThurs.SelectedIndex = 0;
        if (ddlFri.Enabled == true)
            ddlFri.SelectedIndex = 0;
        if (ddlSat.Enabled == true)
            ddlSat.SelectedIndex = 0;

        if (ddlTues.SelectedIndex > 0)
        {
            ddlMonSlot.Enabled = false;
            if (ddlMonSlot.SelectedIndex > 0)
                ddlMonSlot.SelectedIndex = 0;
            ddlTueSlot.Enabled = true;
            ddlWedSlot.Enabled = false;
            if (ddlWedSlot.SelectedIndex > 0)
                ddlWedSlot.SelectedIndex = 0;
            ddlThusSlot.Enabled = false;
            if (ddlThusSlot.SelectedIndex > 0)
                ddlThusSlot.SelectedIndex = 0;
            ddlFriSlot.Enabled = false;
            if (ddlFriSlot.SelectedIndex > 0)
                ddlFriSlot.SelectedIndex = 0;
            ddlSatSlot.Enabled = false;
            if (ddlSatSlot.SelectedIndex > 0)
                ddlSatSlot.SelectedIndex = 0;
        }
        BindTeachingPlan();
    }

    protected void ddlWed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMon.Enabled == true)
            ddlMon.SelectedIndex = 0;
        if (ddlTues.Enabled == true)
            ddlTues.SelectedIndex = 0;
        if (ddlThurs.Enabled == true)
            ddlThurs.SelectedIndex = 0;
        if (ddlFri.Enabled == true)
            ddlFri.SelectedIndex = 0;
        if (ddlSat.Enabled == true)
            ddlSat.SelectedIndex = 0;

        if (ddlWed.SelectedIndex > 0)
        {
            ddlMonSlot.Enabled = false;
            if (ddlMonSlot.SelectedIndex > 0)
                ddlMonSlot.SelectedIndex = 0;
            ddlTueSlot.Enabled = false;
            if (ddlTueSlot.SelectedIndex > 0)
                ddlTueSlot.SelectedIndex = 0;
            ddlWedSlot.Enabled = true;

            ddlThusSlot.Enabled = false;
            if (ddlThusSlot.SelectedIndex > 0)
                ddlThusSlot.SelectedIndex = 0;
            ddlFriSlot.Enabled = false;
            if (ddlFriSlot.SelectedIndex > 0)
                ddlFriSlot.SelectedIndex = 0;
            ddlSatSlot.Enabled = false;
            if (ddlSatSlot.SelectedIndex > 0)
                ddlSatSlot.SelectedIndex = 0;
        }
        BindTeachingPlan();
    }

    protected void ddlThurs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMon.Enabled == true)
            ddlMon.SelectedIndex = 0;
        if (ddlTues.Enabled == true)
            ddlTues.SelectedIndex = 0;
        if (ddlWed.Enabled == true)
            ddlWed.SelectedIndex = 0;
        if (ddlFri.Enabled == true)
            ddlFri.SelectedIndex = 0;
        if (ddlSat.Enabled == true)
            ddlSat.SelectedIndex = 0;

        if (ddlThurs.SelectedIndex > 0)
        {
            ddlMonSlot.Enabled = false;
            ddlTueSlot.Enabled = false;
            ddlWedSlot.Enabled = false;
            ddlThusSlot.Enabled = true;
            ddlFriSlot.Enabled = false;
            ddlSatSlot.Enabled = false;

            if (ddlMonSlot.SelectedIndex > 0)
                ddlMonSlot.SelectedIndex = 0;
            if (ddlTueSlot.SelectedIndex > 0)
                ddlTueSlot.SelectedIndex = 0;
            if (ddlWedSlot.SelectedIndex > 0)
                ddlWedSlot.SelectedIndex = 0;
            if (ddlFriSlot.SelectedIndex > 0)
                ddlFriSlot.SelectedIndex = 0;
            if (ddlSatSlot.SelectedIndex > 0)
                ddlSatSlot.SelectedIndex = 0;
        }
        BindTeachingPlan();
    }

    protected void ddlFri_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMon.Enabled == true)
            ddlMon.SelectedIndex = 0;
        if (ddlTues.Enabled == true)
            ddlTues.SelectedIndex = 0;
        if (ddlWed.Enabled == true)
            ddlWed.SelectedIndex = 0;
        if (ddlThurs.Enabled == true)
            ddlThurs.SelectedIndex = 0;
        if (ddlSat.Enabled == true)
            ddlSat.SelectedIndex = 0;

        if (ddlFri.SelectedIndex > 0)
        {
            ddlMonSlot.Enabled = false;
            if (ddlMonSlot.SelectedIndex > 0)
                ddlMonSlot.SelectedIndex = 0;
            ddlTueSlot.Enabled = false;
            if (ddlTueSlot.SelectedIndex > 0)
                ddlTueSlot.SelectedIndex = 0;
            ddlWedSlot.Enabled = false;
            if (ddlWedSlot.SelectedIndex > 0)
                ddlWedSlot.SelectedIndex = 0;
            ddlThusSlot.Enabled = false;
            if (ddlThusSlot.SelectedIndex > 0)
                ddlThusSlot.SelectedIndex = 0;
            ddlFriSlot.Enabled = true;
            ddlSatSlot.Enabled = false;
            if (ddlSatSlot.SelectedIndex > 0)
                ddlSatSlot.SelectedIndex = 0;
        }
        BindTeachingPlan();
    }

    protected void ddlSat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMon.Enabled == true)
            ddlMon.SelectedIndex = 0;
        if (ddlTues.Enabled == true)
            ddlTues.SelectedIndex = 0;
        if (ddlWed.Enabled == true)
            ddlWed.SelectedIndex = 0;
        if (ddlThurs.Enabled == true)
            ddlThurs.SelectedIndex = 0;
        if (ddlFri.Enabled == true)
            ddlFri.SelectedIndex = 0;

        if (ddlSat.SelectedIndex > 0)
        {
            ddlMonSlot.Enabled = false;
            if (ddlMonSlot.SelectedIndex > 0)
                ddlMonSlot.SelectedIndex = 0;
            ddlTueSlot.Enabled = false;
            if (ddlTueSlot.SelectedIndex > 0)
                ddlTueSlot.SelectedIndex = 0;
            ddlWedSlot.Enabled = false;
            if (ddlWedSlot.SelectedIndex > 0)
                ddlWedSlot.SelectedIndex = 0;
            ddlThusSlot.Enabled = false;
            if (ddlThusSlot.SelectedIndex > 0)
                ddlThusSlot.SelectedIndex = 0;
            ddlFriSlot.Enabled = false;
            if (ddlFriSlot.SelectedIndex > 0)
                ddlFriSlot.SelectedIndex = 0;
            ddlSatSlot.Enabled = true;
        }
        BindTeachingPlan();
    }

    private int CheckDropdownSelect()
    {
        int count = 0;
        if (ddlMon.Enabled == true && ddlMon.SelectedIndex > 0)
            count++;
        if (ddlTues.Enabled == true && ddlTues.SelectedIndex > 0)
            count++;
        if (ddlWed.Enabled == true && ddlWed.SelectedIndex > 0)
            count++;
        if (ddlThurs.Enabled == true && ddlThurs.SelectedIndex > 0)
            count++;
        if (ddlFri.Enabled == true && ddlFri.SelectedIndex > 0)
            count++;
        if (ddlSat.Enabled == true && ddlSat.SelectedIndex > 0)
            count++;
        return count;
    }

    private int CheckSlotSelect()
    {
        int count = 0;
        if (ddlMonSlot.Enabled == true && ddlMonSlot.SelectedIndex > 0)
            count++;
        if (ddlTueSlot.Enabled == true && ddlTueSlot.SelectedIndex > 0)
            count++;
        if (ddlWedSlot.Enabled == true && ddlWedSlot.SelectedIndex > 0)
            count++;
        if (ddlThusSlot.Enabled == true && ddlThusSlot.SelectedIndex > 0)
            count++;
        if (ddlFriSlot.Enabled == true && ddlFriSlot.SelectedIndex > 0)
            count++;
        if (ddlSatSlot.Enabled == true && ddlSatSlot.SelectedIndex > 0)
            count++;
        return count;
    }

    private void BindTopiccodeUnit()
    {
        ddlLectureNo.Items.Clear();
        ddlUnitNo.Items.Clear();
        ddlLectureNo.Items.Insert(0, new ListItem("Please Select", "0"));

        ddlUnitNo.Items.Add(new ListItem("Please Select", "0"));
        for (int i = 1; i <= 30; i++)
        {
            ddlUnitNo.Items.Add(new ListItem(i.ToString()));
        }
    }

    private void CheckLockStatus()
    {
        int session = Convert.ToInt32(ddlSession.SelectedValue);
        int ua_no = Convert.ToInt32(Session["userno"]);
        int section = Convert.ToInt32(ddlSection.SelectedValue);
        int courseno = Convert.ToInt32(ViewState["courseno"].ToString());

        int count = Convert.ToInt32(objCommon.LookUp("ACD_TEACHINGPLAN", "COUNT(*)", "SESSIONNO =" + session + " AND UA_NO =" + ua_no + " AND (CANCEL IS NULL OR CANCEL = 0) AND (COURSENO =" + courseno + " OR " + courseno + " = 0)	AND (SECTIONNO =" + section + " OR " + section + "=0) and lock =1"));

        if (count == 0)
        {
            btnLock.Text = "Lock";
        }
        else
        {
            btnLock.Text = "UnLock";
        }
    }

    protected void ddlMonSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTueSlot.Enabled == true)
            ddlTueSlot.SelectedIndex = 0;
        if (ddlWedSlot.Enabled == true)
            ddlWedSlot.SelectedIndex = 0;
        if (ddlThusSlot.Enabled == true)
            ddlThusSlot.SelectedIndex = 0;
        if (ddlFriSlot.Enabled == true)
            ddlFriSlot.SelectedIndex = 0;
        if (ddlSatSlot.Enabled == true)
            ddlSatSlot.SelectedIndex = 0;
    }

    protected void ddlTueSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonSlot.Enabled == true)
            ddlMonSlot.SelectedIndex = 0;
        if (ddlWedSlot.Enabled == true)
            ddlWedSlot.SelectedIndex = 0;
        if (ddlThusSlot.Enabled == true)
            ddlThusSlot.SelectedIndex = 0;
        if (ddlFriSlot.Enabled == true)
            ddlFriSlot.SelectedIndex = 0;
        if (ddlSatSlot.Enabled == true)
            ddlSatSlot.SelectedIndex = 0;
    }

    protected void ddlWedSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonSlot.Enabled == true)
            ddlMonSlot.SelectedIndex = 0;
        if (ddlTueSlot.Enabled == true)
            ddlTueSlot.SelectedIndex = 0;
        if (ddlThusSlot.Enabled == true)
            ddlThusSlot.SelectedIndex = 0;
        if (ddlFriSlot.Enabled == true)
            ddlFriSlot.SelectedIndex = 0;
        if (ddlSatSlot.Enabled == true)
            ddlSatSlot.SelectedIndex = 0;
    }

    protected void ddlThusSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonSlot.Enabled == true)
            ddlMonSlot.SelectedIndex = 0;
        if (ddlTueSlot.Enabled == true)
            ddlTueSlot.SelectedIndex = 0;
        if (ddlWedSlot.Enabled == true)
            ddlWedSlot.SelectedIndex = 0;
        if (ddlFriSlot.Enabled == true)
            ddlFriSlot.SelectedIndex = 0;
        if (ddlSatSlot.Enabled == true)
            ddlSatSlot.SelectedIndex = 0;
    }

    protected void ddlFriSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonSlot.Enabled == true)
            ddlMonSlot.SelectedIndex = 0;
        if (ddlTueSlot.Enabled == true)
            ddlTueSlot.SelectedIndex = 0;
        if (ddlWedSlot.Enabled == true)
            ddlWedSlot.SelectedIndex = 0;
        if (ddlThusSlot.Enabled == true)
            ddlThusSlot.SelectedIndex = 0;
        if (ddlSatSlot.Enabled == true)
            ddlSatSlot.SelectedIndex = 0;
    }

    protected void ddlSatSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonSlot.Enabled == true)
            ddlMonSlot.SelectedIndex = 0;
        if (ddlTueSlot.Enabled == true)
            ddlTueSlot.SelectedIndex = 0;
        if (ddlWedSlot.Enabled == true)
            ddlWedSlot.SelectedIndex = 0;
        if (ddlThusSlot.Enabled == true)
            ddlThusSlot.SelectedIndex = 0;
        if (ddlFriSlot.Enabled == true)
            ddlFriSlot.SelectedIndex = 0;
    }

    private void FillSlotDropDown(DropDownList ddllist, string slot, int DAYNO, string startdate, string enddate)
    {
        TeachingPlanController objTPC = new TeachingPlanController();
        int session = Convert.ToInt32(ddlSession.SelectedValue);
        int ua_no = Convert.ToInt32(Session["userno"]);
        int section = Convert.ToInt32(ddlSection.SelectedValue);
        int courseno = Convert.ToInt32(ViewState["courseno"].ToString());
        int semesterno = Convert.ToInt32(ViewState["semesterno"].ToString());
        int subid = Convert.ToInt32(ddlSubjectType.SelectedValue);
        int scheme = Convert.ToInt32(ViewState["schemeno"].ToString());
        int College_id = Convert.ToInt32(ViewState["college_id"].ToString());
        //added buy sumit on 07022020
        //string DEGREENO = objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_COURSE WHERE COURSENO = " + Convert.ToInt32(ViewState["courseno"].ToString()) + ")");
        int Degreeno = Convert.ToInt32(ViewState["degreeno"]);
        int batchno = 0;
        if (ddlBatch.SelectedIndex > 0)
            batchno = Convert.ToInt32(ddlBatch.SelectedValue);

        DataSet ds = objTPC.GetSlot(session, scheme, ua_no, courseno, semesterno, section, subid, slot, batchno, Degreeno, DAYNO, College_id, startdate, enddate);

        ddllist.Items.Clear();
        ddllist.Items.Add("Please Select");
        ddllist.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddllist.DataSource = ds;
            ddllist.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddllist.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddllist.DataBind();
            ddllist.SelectedIndex = 0;
        }
        ddllist.Enabled = true;
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        TeachingPlanController objTPC = new TeachingPlanController();
        int session = Convert.ToInt32(ddlSession.SelectedValue);
        int ua_no = Convert.ToInt32(Session["userno"]);
        int section = Convert.ToInt32(ddlSection.SelectedValue);
        int courseno = Convert.ToInt32(ViewState["courseno"].ToString());
        int College_Id = Convert.ToInt32(ViewState["college_id"]);
        int OrgID = Convert.ToInt32(Session["OrgId"]);
        if (btnLock.Text == "Lock")
        {
            objTPC.AddTeachingplanLock(session, ua_no, section, courseno, College_Id, OrgID);
            btnLock.Text = "UnLock";
            btnSubmit.Enabled = false;
        }
        else if (btnLock.Text == "UnLock")
        {
            btnLock.Text = "Lock";
            btnSubmit.Enabled = true;
        }
    }

    protected void btnDownload1_Click(object sender, EventArgs e)
    {
        string filename = string.Empty;
        string ContentType = string.Empty;



        string filepath = Server.MapPath("~/ExcelData/");

        if (ddlDegreeEX.SelectedValue == "1")
            filename = "tp_Degree_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
        else if (ddlDegreeEX.SelectedValue == "2")
            filename = "tp_MTECH_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
        else if (ddlDegreeEX.SelectedValue == "4")
            filename = "tp_MCA_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
        else if (ddlDegreeEX.SelectedValue == "5")
            filename = "tp_MBA_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
        else if (ddlDegreeEX.SelectedValue == "6")
            filename = "tp_ME_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
        else
        {
            objCommon.DisplayMessage(updTeach, "Please Select Degree!", this);
            return;
        }
        if (!(Directory.Exists(MapPath("~/PresentationLayer/ExcelData"))))
            Directory.CreateDirectory(filepath);
        FileInfo myfile = new FileInfo(filepath + filename);
        string[] array1 = Directory.GetFiles(filepath);
        if (array1.Length == 0)
        {
            objCommon.DisplayMessage(updTeach, "No File exists!", this);
            return;
        }

        #region Comment

        // Checking if file exists
        if (myfile.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", myfile.Length.ToString());

            // Set the ContentType
            Response.ContentType = "application/vnd.ms-excel";

            // Write the file into the  response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(myfile.FullName);

            // End the response
            Response.End();
        }
        #endregion




    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                if (ddlSession.SelectedIndex > 0)
                {
                    if (ddlSemester.SelectedIndex > 0)
                    {
                        if (ddlSection.SelectedIndex > 0)
                        {
                            if (ddlBatch.Visible == true && ddlBatch.SelectedIndex == 0)
                            {
                                objCommon.DisplayMessage(updTeach, "Please Select Batch.", this);
                                return;
                            }
                            string path = MapPath("~/ExcelData/");
                            if (btnBrowse.HasFile)
                            {
                                string filename = btnBrowse.FileName.ToString();
                                string Extension = Path.GetExtension(filename);
                                if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                                {
                                    if (!(Directory.Exists(MapPath("~/ExcelData"))))
                                        Directory.CreateDirectory(path);

                                    if (filename.Contains(".xls"))
                                    {
                                        filename = filename.Replace(".xls", "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + Session["userno"].ToString() + ".xls");
                                    }
                                    else if (filename.Contains(".xlsx"))
                                    {
                                        filename = filename.Replace(".xlsx", "_" + DateTime.Now.ToString("dd-MM-yyyy") + "_" + Session["userno"].ToString() + ".xlsx");
                                    }

                                    string[] array1 = Directory.GetFiles(path);
                                    foreach (string str in array1)
                                    {
                                        if ((path + filename).Equals(str))
                                        {
                                            objCommon.DisplayMessage(updTeach, "File with similar name already exists!", this);
                                            return;
                                        }
                                    }




                                    ViewState["FileName"] = filename;
                                    //btnBrowse.SaveAs(path + filename + ".xls");
                                    btnBrowse.SaveAs(path + filename);
                                    string Filepath = Server.MapPath("~/ExcelData/" + filename);
                                    if (!CheckFormatAndImport(Extension, Filepath, "yes"))
                                    {
                                        if (Session["Count"] == "-101")
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(updTeach, "File is not in correct format!!Please check if the data is saved in SHEET1. Else check if  the column names have changed!", this.Page);
                                        }
                                    }
                                    else
                                    {
                                        if (Session["Count"] == "-101")
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(updTeach, "Teaching Plan Uploaded Successfully !", this);
                                        }
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(updTeach, "Only Excel Sheet is Allowed!", this);
                                    return;
                                }
                            }
                            else
                            {
                                objCommon.DisplayMessage(updTeach, "Select File to Upload!", this);
                                return;
                            }
                            this.BindTeachingPlan();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updTeach, "Please Select Section", this);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updTeach, "Please Select Semester/Course", this);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updTeach, "Please Select Session", this);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updTeach, " Please Select School & Scheme", this);
                return;
            }

        }
        catch
        {
            throw;
        }
    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string path = MapPath("~/ExcelData/");
    //        if (btnBrowse.HasFile)
    //        {
    //            string filename = btnBrowse.FileName.ToString();
    //            if (filename.Contains(".xls") || filename.Contains(".xlsx"))
    //            {
    //                if (!(Directory.Exists(MapPath("~/ExcelData"))))
    //                    Directory.CreateDirectory(path);

    //                string[] array1 = Directory.GetFiles(path);

    //                if (filename.Contains(".xls"))
    //                {
    //                    filename = filename.Replace(".xls", "_" + DateTime.Now.ToString("dd-MM-yyyy-hmm") + "_" + Session["userfullname"].ToString()+".xls");
    //                }
    //                else if (filename.Contains(".xlsx"))
    //                {
    //                    filename = filename.Replace(".xlsx", "_"+ DateTime.Now.ToString("dd-MM-yyyy-hmm") +"_"+Session["userfullname"].ToString()+".xlsx");
    //                }

    //                foreach (string str in array1)
    //                {
    //                    if ((path + filename).Equals(str))
    //                    {
    //                        objCommon.DisplayMessage(updTeach, "File with similar name already exists!", this);
    //                        return;
    //                    }
    //                }
    //                ViewState["FileName"] = filename; ;
    //                btnBrowse.SaveAs(path + filename+".xlsx");
    //                objCommon.DisplayMessage(updTeach, "Teaching Plan Uploaded Successfully !", this);
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage(updTeach, "Only Excel Sheet is Allowed!", this);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updTeach, "Select File to Upload!", this);
    //            return;
    //        }
    //        this.BindTeachingPlan();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private bool CheckFormatAndImport(string Extension, string FilePath, string isHDR)
    {
        string filename = ViewState["FileName"].ToString();
        Exam objExam = new Exam();
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath);

        string Message = string.Empty;
        int count = 0;
        OleDbConnection connExcel = new OleDbConnection(conStr);
        try
        {

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataSet ds = null;
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[2]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            count = dt.Rows.Count;
            int i = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                string isDuplicateUnit = string.Empty;
                string isDuplicateTopicCode = string.Empty;
                string NoDuplicateUnit = string.Empty;
                string NoDuplicateTopicCode = string.Empty;
                int savedCount = 0;
                string msg = string.Empty;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TOPIC_COVERED"].ToString() == string.Empty)  // Topic covered
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Topic Covered at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return false;
                    }
                    else
                    {
                        objExam.Topic_Covered = dt.Rows[i]["TOPIC_COVERED"].ToString().Trim();
                    }




                    if (dt.Rows[i]["UNIT_NO"].ToString() == string.Empty)  // Topic covered
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Unit No at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return false;
                    }
                    else
                    {
                        objExam.UnitNo = Convert.ToInt16(dt.Rows[i]["UNIT_NO"] == DBNull.Value ? "0" : dt.Rows[i]["UNIT_NO"].ToString().Trim());
                    }




                    if (dt.Rows[i]["LECTURE_NO"].ToString() == string.Empty)  // Topic covered
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Lecture No at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return false;
                    }
                    else
                    {
                        objExam.Lecture_No = Convert.ToInt16(dt.Rows[i]["LECTURE_NO"] == DBNull.Value ? "0" : dt.Rows[i]["LECTURE_NO"].ToString().Trim());
                    }



                    if (dt.Rows[i]["SESSION_REQUIRED"].ToString() == string.Empty)  // Topic covered
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Session Required at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return false;
                    }
                    else
                    {
                        objExam.sessionPlan = Convert.ToInt16(dt.Rows[i]["SESSION_REQUIRED"] == DBNull.Value ? "0" : dt.Rows[i]["SESSION_REQUIRED"].ToString().Trim());
                    }





                    //if (dt.Rows[i]["SECTIONNAME"].ToString() == string.Empty)  // Topic covered
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Please Enter Section at Row no. " + (i + 1), this.Page);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    //    return false;
                    //}
                    //else
                    //{
                    //    string section = objCommon.LookUp("ACD_SECTION", "SECTIONNO", "SECTIONNAME ='" + dt.Rows[i]["SECTIONNAME"].ToString().Trim() + "'");
                    //    objExam.Sectionno = Convert.ToInt16(section == "" ? "0" : section);
                    //}








                    try
                    {
                        if (dt.Rows[i]["DATE"].ToString() == "" || dt.Rows[i]["DATE"].ToString() == null)
                        {
                            objExam.Date = Convert.ToDateTime("01/01/1753 00:00:00");
                        }
                        else
                        {
                            objExam.Date = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["DATE"].ToString()).ToString("yyyy-MM-dd"));
                        }
                    }
                    catch (Exception ex1)
                    {
                        objCommon.DisplayMessage(updTeach, "Teaching Plan Date Format is not Proper!", this.Page);
                        return false;
                    }





                    objExam.Remark = dt.Rows[i]["REMARK"].ToString().Trim();

                    //string batch = objCommon.LookUp("ACD_BATCH", "BATCHNO", "BATCHNAME ='" + dt.Rows[i]["BATCH"].ToString().Trim() + "'");
                    //objExam.BatchNo = Convert.ToInt16(batch == "" ? "0" : batch);

                    //string slot = objCommon.LookUp("ACD_TIME_SLOT", "SLOTNO", "SLOTNAME ='" + dt.Rows[i]["SLOTNAME"].ToString().Trim() + "'");
                    //objExam.Slot = Convert.ToInt16(slot == "" ? "0" : slot);


                    string slottypeno = string.Empty;


                    //if (dt.Rows[i]["SLOTYPENAME"].ToString() == string.Empty)
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Please Enter Slot Type Name at Row no. " + (i + 1), this.Page);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    //    return false;
                    //}
                    //else
                    //{
                    slottypeno = objCommon.LookUp("ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME ='" + dt.Rows[i]["SLOTYPENAME"].ToString().Trim() + "'");

                    if (slottypeno != "")
                    {
                        objExam.Slotno = (objCommon.LookUp("ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME ='" + dt.Rows[i]["SLOTYPENAME"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SLOTTYPE", "SLOTTYPENO", "SLOTTYPE_NAME ='" + dt.Rows[i]["SLOTYPENAME"].ToString() + "'"));
                    }
                    else
                    {
                        objExam.Slotno = 0;
                    }
                    //else
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Slot Type Name not found in ERP Master at Row no. " + (i + 1), this.Page);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    //    return false;
                    //}
                    //}



                    //if (dt.Rows[i]["SLOTNAME"].ToString() == string.Empty)
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Please Enter Slot Name at Row no. " + (i + 1), this.Page);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    //    return false;
                    //}
                    //else
                    //{
                    if (slottypeno != "")
                    {
                        string slot = objCommon.LookUp("ACD_TIME_SLOT", "SLOTNO", "SLOTNAME ='" + dt.Rows[i]["SLOTNAME"].ToString().Trim() + "' AND SLOTTYPE = " + slottypeno + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + " AND DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "AND ACTIVESTATUS=1");

                        if (slot != "")
                        {
                            objExam.Slot = (objCommon.LookUp("ACD_TIME_SLOT", "SLOTNO", "SLOTNAME ='" + dt.Rows[i]["SLOTNAME"].ToString() + "' AND SLOTTYPE = " + slottypeno + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + " AND DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "AND ACTIVESTATUS=1")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_TIME_SLOT", "SLOTNO", "SLOTNAME ='" + dt.Rows[i]["SLOTNAME"].ToString() + "' AND SLOTTYPE = " + slottypeno + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + " AND DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "AND ACTIVESTATUS=1"));
                        }
                    }
                    else
                    {
                        objExam.Slot = 0;
                    }
                    //else
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Slot Name not found in ERP Master at Row no. " + (i + 1), this.Page);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    //    return false;
                    //}
                    //}

                    objExam.Courseno = Convert.ToInt32(ViewState["courseno"].ToString());
                    objExam.SemesterNo = Convert.ToInt32(ViewState["semesterno"].ToString());

                    //if (!(Convert.ToInt32(Session["usertype"]) > 1))
                    objExam.SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());
                    //else
                    //    objExam.SchemeNo = 0;//NOTE : FOR TEACHER LOGIN WE GO BY UNIQUE COURSE NO.

                    objExam.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
                    objExam.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
                    if (ddlSession.SelectedIndex <= 0)
                        objExam.SessionNo = Convert.ToInt32(Session["currentsession"].ToString());
                    else
                        objExam.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

                    objExam.Ua_No = Convert.ToInt16(Session["userno"].ToString());
                    //int Istutorial = ddlTutorial.SelectedValue == "2" ? 1 : 0;
                    int Istutorial = ddlTutorial.SelectedValue == "2" || (Convert.ToInt32(ViewState["IS_TUTORIAL"]) > 0 && Convert.ToInt32(ViewState["IS_PRACTICAL"]) == 0 && Convert.ToInt32(ViewState["IS_THEORY"]) == 0) ? 1 : 0;
                    int OrgId = Convert.ToInt32(Session["OrgId"]);
                    objExam.collegeid = Convert.ToInt32(ViewState["college_id"].ToString());//Added by Dileep on 12.04.2021

                    if (CheckDuplicateUploadEntry(objExam.UnitNo, objExam.Lecture_No, objExam.Sectionno, objExam.BatchNo, objExam.Slot) == true)
                    {
                        //if (isDuplicateUnit.Length > 0)
                        //{
                        //    isDuplicateUnit += ",";
                        //}
                        //else
                        //{
                        isDuplicateUnit += objExam.UnitNo;
                        //}

                        //if (isDuplicateTopicCode.Length > 0)
                        //{
                        //    isDuplicateTopicCode += ",";
                        //}
                        //else
                        //{
                        isDuplicateTopicCode += objExam.Lecture_No;
                        //}
                        if (msg.Length > 0)
                        {
                            //msg += "\\n";
                            msg += "\\nUnit No. : " + isDuplicateUnit + " and Topic Code : " + isDuplicateTopicCode + "";
                            isDuplicateUnit = string.Empty;
                            isDuplicateTopicCode = string.Empty;
                        }
                        else
                        {
                            msg += "Teaching Plan Entry For Unit No. : " + isDuplicateUnit + " and Topic Code : " + isDuplicateTopicCode + "";
                            isDuplicateUnit = string.Empty;
                            isDuplicateTopicCode = string.Empty;
                        }

                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objTeachingPlanController.UploadTeachingPlan(objExam, Istutorial, OrgId);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            savedCount++;

                        }
                        else if (cs.Equals(CustomStatus.RecordNotFound))
                        {

                            objCommon.DisplayMessage(this.updTeach, "Time Table is not Defined for Entered Slotname and Date at Row no." + (i + 1), this.Page);

                            //objCommon.DisplayMessage(this.Page, "Slot Type Name not found in ERP Master at Row no. " + (i + 1), this.Page);

                            //isDuplicateUnit = string.Empty;
                            //isDuplicateTopicCode = string.Empty;
                            Session["Count"] = "-101";
                            return false;

                        }
                    }
                    //}
                }
                if (savedCount != 0 && msg != string.Empty)
                {
                    objCommon.DisplayMessage(this.updTeach, "Teaching Plan Saved Successfully." + "\\nBut " + msg + " \\nAlready Entered. Please Verify.", this.Page);
                }
                if (msg != string.Empty)
                {
                    objCommon.DisplayMessage(this.updTeach, msg + " \\nAre Already Entered. Please Verify..", this.Page);
                }
                if (savedCount != 0)
                {
                    objCommon.DisplayMessage(this.updTeach, "Teaching Plan Saved Successfully.", this.Page);
                }
                this.ResetDateDropdown();
                this.BindTopiccodeUnit();
            }
            if (count == i)
            {
                //objCommon.DisplayMessage(updTeach, "Data Saved Successfully!", this.Page);
                return true;
            }
            else
            {
                //objCommon.DisplayMessage(updTeach, "Please check the format of file & upload again!", this.Page);
                return false;
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            connExcel.Close();
            connExcel.Dispose();
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        // DataSet ds = objCommon.FillDropDown("ACD_TEACHINGPLAN TP INNER JOIN ACD_COURSE C ON (TP.COURSENO = C.COURSENO) INNER JOIN ACD_SCHEME SC ON (C.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN  ACD_TIME_SLOT SL ON (TP.SLOT = SL.SLOTNO AND TP.SESSIONNO = SL.SESSIONNO AND SL.DEGREENO = SC.DEGREENO)", "CONVERT(NVARCHAR(30),DATE,101)DATE,DBO.FN_DESC('SECTION',SECTIONNO)SECTIONNAME", "TOPIC_COVERED,UNIT_NO,LECTURE_NO,REMARK,DBO.FN_DESC('BATCH',BATCHNO)BATCH,SL.SLOTNAME", "UA_NO = " + Session["userno"].ToString() + " AND TP.SESSIONNO = " + ddlSession.SelectedValue + " AND TP.COURSENO = " + ddlCourse.SelectedValue + " AND TUTORIAL = " + (ddlSubjectType.SelectedValue == "100" ? "1" : "0") + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SC.DEGREENO = " + ddlDegreeEX.SelectedValue, "CAST(DATE AS DATETIME),TP.SECTIONNO,SLOT,BATCHNO");
        DataSet ds = objCommon.FillDropDown("ACD_TEACHINGPLAN TP INNER JOIN ACD_COURSE C ON (TP.COURSENO = C.COURSENO) INNER JOIN ACD_SCHEME SC ON (C.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN  ACD_TIME_SLOT SL ON (TP.SLOT = SL.SLOTNO AND TP.SESSIONNO = SL.SESSIONNO AND SL.DEGREENO = SC.DEGREENO)", "CONVERT(NVARCHAR(30),TP.DATE,101)DATE,DBO.FN_DESC('SECTIONNAME',SECTIONNO)SECTIONNAME", "TOPIC_COVERED,UNIT_NO,LECTURE_NO,TP.REMARK,DBO.FN_DESC('BATCH',BATCHNO)BATCH,SL.SLOTNAME", "UA_NO = " + Session["userno"].ToString() + " AND TP.SESSIONNO = " + ddlSession.SelectedValue + " AND TP.COURSENO = " + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND TUTORIAL = " + (ddlSubjectType.SelectedValue == "100" ? "1" : "0") + " AND C.SEMESTERNO = " + Convert.ToInt32(ViewState["semesterno"].ToString()) + " AND SC.DEGREENO = " + ddlDegreeEX.SelectedValue, "CAST(TP.DATE AS DATETIME),TP.SECTIONNO,SLOT,BATCHNO");
        DataSet ds1 = objCommon.FillDropDown("ACD_TIME_SLOT", " SLOTNAME", "TIMEFROM,TIMETO", "SESSIONNO = " + ddlSession.SelectedValue + " AND DEGREENO = " + ddlDegreeEX.SelectedValue, "SLOTNO");

        HtmlTable dt = new HtmlTable();
        HtmlTableRow dr = new HtmlTableRow();

        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            HtmlTableCell dc1 = new HtmlTableCell();

            dc1.InnerText = ds.Tables[0].Columns[i].ToString();
            dr.Cells.Add(dc1);
            dt.Rows.Add(dr);
        }

        int count = 0;
        int l = 0;
        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        {
            HtmlTableRow dr1 = new HtmlTableRow();

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                HtmlTableCell dc1 = new HtmlTableCell();
                dc1.InnerText = ds.Tables[0].Rows[j][i].ToString();
                dr1.Cells.Add(dc1);
            }
            dt.Rows.Add(dr1);
        }
        #region comment
        //for (int m = 2; m <= ds1.Tables[0].Rows.Count + 2; m++)
        //{
        //    HtmlTableRow dr1 = new HtmlTableRow();
        //    if (m == 2 && count == 0)
        //    {
        //        for (int k = -2; k < ds1.Tables[0].Columns.Count; k++)
        //        {
        //            if (k >= 0)
        //            {
        //                HtmlTableCell dc1 = new HtmlTableCell();
        //                dc1.InnerText = ds1.Tables[0].Columns[k].ToString();
        //                dr1.Cells.Add(dc1);
        //            }
        //            else
        //            {
        //                HtmlTableCell dc1 = new HtmlTableCell();
        //                dc1.InnerText = "";
        //                dr1.Cells.Add(dc1);
        //            }
        //        }
        //        dt.Rows.Add(dr1);
        //        count++;
        //    }
        //    else
        //    {
        //        if (m > 2 && count != 0)
        //        {
        //            for (int i = -2; i < ds1.Tables[0].Columns.Count; i++)
        //            {
        //                if (l >= 0 && l < ds1.Tables[0].Rows.Count)
        //                {
        //                    if (i >= 0)
        //                    {
        //                        HtmlTableCell dc1 = new HtmlTableCell();
        //                        dc1.InnerText = ds1.Tables[0].Rows[l][i].ToString();
        //                        dr1.Cells.Add(dc1);
        //                    }
        //                    else
        //                    {
        //                        HtmlTableCell dc1 = new HtmlTableCell();
        //                        dc1.InnerText = "";
        //                        dr1.Cells.Add(dc1);
        //                    }
        //                }
        //            }
        //            dt.Rows.Add(dr1);
        //            count++;
        //            l++;
        //        }
        //    }
        //}
        #endregion
        if (dt != null)
        {
            HtmlTable ht = dt as HtmlTable;
            string strBody = HtmlTable2ExcelString(ht);

            Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            Response.AppendHeader("Content-disposition", "attachment; filename=Teachingplan.xls");
            Response.Clear();
            Response.Write(strBody);
            Response.End();
        }
        else
            objCommon.DisplayMessage(updTeach, "There is no data available", this.Page);
    }

    public string HtmlTable2ExcelString(HtmlTable dt)
    {
        StringBuilder sbTop = new StringBuilder();
        sbTop.Append("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
        sbTop.Append("xmlns=\"http://www.w3.org/TR/REC-html40\"><head><meta http-equiv=Content-Type content=\"text/html; charset=windows-1252\">");
        sbTop.Append("<meta name=ProgId content=Excel.Sheet><meta name=Generator content=\"Microsoft Excel 9\"><!--[if gte mso 9]>");
        sbTop.Append("<xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>SHEET1</x:Name><x:WorksheetOptions>");
        sbTop.Append("<x:Selected/><x:ProtectContents>False</x:ProtectContents><x:ProtectObjects>False</x:ProtectObjects>");
        sbTop.Append("<x:ProtectScenarios>False</x:ProtectScenarios></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets>");
        sbTop.Append("<x:ProtectStructure>False</x:ProtectStructure><x:ProtectWindows>False</x:ProtectWindows></x:ExcelWorkbook></xml>");
        sbTop.Append("<![endif]--></head><body><table>");
        string bottom = "</table></body></html>";

        StringBuilder sb = new StringBuilder();

        //All Items
        for (int x = 0; x < dt.Rows.Count; x++)
        {
            sb.Append("<tr>");
            for (int i = 0; i < dt.Rows[x].Cells.Count; i++)
            {
                sb.Append("<td>" + dt.Rows[x].Cells[i].InnerText + "</td>");
            }
            sb.Append("</tr>");
        }

        string SSxml = sbTop.ToString() + sb.ToString() + bottom;

        return SSxml;
    }

    protected void btnBlankDownld_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlScheme.SelectedIndex > 0)
            //{
            //    if (ddlSession.SelectedIndex > 0)
            //    {
            //if (ddlSemester.SelectedIndex > 0)
            //{
            string filename = string.Empty;
            //if (ddlDegreeEX.SelectedValue != "0")
            //{
            filename = "BlankExcelFormat.xls";
            DataSet ds = objTeachingPlanController.GetBlankExcelforTeachingPlan1(Convert.ToInt32(ViewState["courseno"]), Convert.ToInt32(ViewState["college_id"]));
            GridView gv = new GridView();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Create New instance of FileInfo class to get the properties of the file being downloaded


                    gv.DataSource = ds;
                    gv.DataBind();
                    string Attachment = "Attachment; filename=Month_Wise_User_Count.xls";
                    ds.Tables[0].TableName = "Sheet1";
                    ds.Tables[1].TableName = "Section_Master";
                    ds.Tables[2].TableName = "Batch_Mater";
                    ds.Tables[3].TableName = "Slot Master";
                    //ds.Tables[4].TableName = "Slot Type Master";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in ds.Tables)
                        {
                            //Add System.Data.DataTable as Worksheet.
                            wb.Worksheets.Add(dt);
                        }

                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=BlankExcelFormat_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
            #region comment
            //COMMENTED BY SUMIT 0N 06-02-2020

            //if (ddlDegreeEX.SelectedValue == "2")
            //    filename = "BlankExcelFormat.xls";
            //if (ddlDegreeEX.SelectedValue == "4")
            //    filename = "BlankExcelFormat.xls";
            //if (ddlDegreeEX.SelectedValue == "5")
            //    filename = "BlankExcelFormat.xls";
            //if (ddlDegreeEX.SelectedValue == "6")
            //    filename = "BlankExcelFormat.xls";

                    //Commented by Dileep on 07.03.2022
            //string path = MapPath("~/ExcelData/") + filename;

            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    Response.Clear();
            //    Response.ClearHeaders();
            //    Response.ClearContent();
            //    Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
            //    Response.AppendHeader("Content-disposition", "attachment; filename=BlankExcelFormat.xls");
            //    Response.ContentType = "application/vnd.xls";
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    Response.Clear();
            //    Response.WriteFile(file.FullName);
            //    Response.End();
            //}
            #endregion
            else
            {
                objCommon.DisplayMessage(updTeach, "This file does not exist.", this);
                return;
            }
            // }
            //else
            //{
            //    objCommon.DisplayMessage(updTeach, "Please Select Semester/Course", this);
            //    return;
            //}
            // }
            //else
            //{
            //    objCommon.DisplayMessage(updTeach, "Please Select Session", this);
            //    return;
            //}
            // }
            //else
            //{
            //    objCommon.DisplayMessage(updTeach, " Please Select School & Scheme", this);
            //    return;
            //}

        }
        catch
        {
            throw;
        }

        //else
        //{
        //    objCommon.DisplayMessage(this.updTeach, "Please select Degree.", this.Page);
        //    return;
        //}

    }


    private void ShowReportNew(string reportTitle, string rptFileName)
    {
        try
        {
            int ua_no = 0;
            if (Session["usertype"].ToString() != "1")
            {
                ua_no = Convert.ToInt32(Session["userno"]);
            }
            else
            {
                ua_no = Convert.ToInt32(ddlTeacher.SelectedValue);
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UA_NO=" + ua_no
                    + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                     + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString())
                      + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"].ToString())
                    + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                    + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString())
                    + ",@P_BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue)
                    + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeach, this.updTeach.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
            throw;
        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int ua_no = 0;
            if (Session["usertype"].ToString() != "1")
            {
                ua_no = Convert.ToInt32(Session["userno"]);
            }
            else
            {
                ua_no = Convert.ToInt32(ddlTeacher.SelectedValue);
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UA_NO=" + ua_no
                    + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                     + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString())
                      + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["semesterno"].ToString())
                    + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
                    + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString())
                    + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]).ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeach, this.updTeach.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
            throw;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string argument = ((LinkButton)sender).CommandArgument;
            if (argument.ToString() == "1")
            {
                ShowReport("Course wise Teaching plan", "rptAdmin_Coursewise_Teachingplan.rpt");
            }
            if (argument.ToString() == "2")
            {
                ShowReportNew("Course wise Teaching plan", "rptPlanned_Executed_Teachingplan.rpt");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Exam objExam = new Exam();

            objExam.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objExam.Ua_No = Convert.ToInt32(Session["userno"]);

            if (ddlMon.SelectedIndex > 0)
                objExam.Date = Convert.ToDateTime(ddlMon.SelectedValue);
            else if (ddlTues.SelectedIndex > 0)
                objExam.Date = Convert.ToDateTime(ddlTues.SelectedValue);
            else if (ddlWed.SelectedIndex > 0)
                objExam.Date = Convert.ToDateTime(ddlWed.SelectedValue);
            else if (ddlThurs.SelectedIndex > 0)
                objExam.Date = Convert.ToDateTime(ddlThurs.SelectedValue);
            else if (ddlFri.SelectedIndex > 0)
                objExam.Date = Convert.ToDateTime(ddlFri.SelectedValue);
            else if (ddlSat.SelectedIndex > 0)
                objExam.Date = Convert.ToDateTime(ddlSat.SelectedValue);
            else objExam.Date = Convert.ToDateTime("01/01/1753 00:00:00");


            if (ddlMonSlot.SelectedIndex > 0)
                objExam.Slot = Convert.ToInt32(ddlMonSlot.SelectedValue);
            else if (ddlTueSlot.SelectedIndex > 0)
                objExam.Slot = Convert.ToInt32(ddlTueSlot.SelectedValue);
            else if (ddlWedSlot.SelectedIndex > 0)
                objExam.Slot = Convert.ToInt32(ddlWedSlot.SelectedValue);
            else if (ddlThusSlot.SelectedIndex > 0)
                objExam.Slot = Convert.ToInt32(ddlThusSlot.SelectedValue);
            else if (ddlFriSlot.SelectedIndex > 0)
                objExam.Slot = Convert.ToInt32(ddlFriSlot.SelectedValue);
            else if (ddlSatSlot.SelectedIndex > 0)
                objExam.Slot = Convert.ToInt32(ddlSatSlot.SelectedValue);
            else objExam.Slot = Convert.ToInt32(0);

            objExam.Lecture_No = Convert.ToInt32(ddlLectureNo.SelectedValue);
            objExam.Courseno = Convert.ToInt32(ViewState["courseno"].ToString());
            if (ViewState["semesterno"] == null)
            {
                objExam.SemesterNo = 0;
            }
            else
            {
                objExam.SemesterNo = Convert.ToInt32(ViewState["semesterno"].ToString());
            }

            //if (!(Convert.ToInt32(Session["usertype"]) > 1))
            objExam.SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());
            //else
            //    objExam.SchemeNo = 0; //NOTE : FOR TEACHER LOGIN WE GO BY UNIQUE COURSE NO.

            objExam.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            objExam.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
            objExam.Topic_Covered = Convert.ToString(txtLectureTopic.Text);
            objExam.UnitNo = Convert.ToInt32(ddlUnitNo.SelectedValue);
            objExam.collegeid = Convert.ToInt32(ViewState["college_id"].ToString());//Added by Dileep on 12.04.2021
            objExam.sessionPlan = Convert.ToInt32(txtSessionReq.Text);
            int Istutorial = ddlTutorial.SelectedValue == "2" || (Convert.ToInt32(ViewState["IS_TUTORIAL"]) > 0 && Convert.ToInt32(ViewState["IS_PRACTICAL"]) == 0 && Convert.ToInt32(ViewState["IS_THEORY"]) == 0) ? 1 : 0;
            int count = 0;
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            // if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 100)
            if (Convert.ToInt32(ddlTutorial.SelectedValue) == 2)
            {
                Istutorial = 1;
                count = Convert.ToInt32(objCommon.LookUp("ACD_TEACHINGPLAN", "COUNT(*)", "SESSIONNO IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "AND SECTIONNO =" + Convert.ToInt32(ddlSection.SelectedValue) + "AND LECTURE_NO =" + Convert.ToInt32(ddlLectureNo.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + " AND (CANCEL IS NULL OR CANCEL = '') AND TUTORIAL = 1 AND COLLEGE_ID=" + ViewState["college_id"].ToString()));
            }
            else
            {
                count = Convert.ToInt32(objCommon.LookUp("ACD_TEACHINGPLAN", "COUNT(*)", "SESSIONNO IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AND UA_NO =" + Convert.ToInt32(Session["userno"].ToString()) + "AND SECTIONNO =" + Convert.ToInt32(ddlSection.SelectedValue) + "AND LECTURE_NO =" + Convert.ToInt32(ddlLectureNo.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + " AND (CANCEL IS NULL OR CANCEL = '') AND (TUTORIAL = 0 or tutorial is null) AND COLLEGE_ID=" + ViewState["college_id"].ToString()));
            }
            //if (count != 0 && (ddlLectureNo.SelectedValue != ViewState["LECT_NO"].ToString()))
            //{
            //    objCommon.DisplayMessage(this.updTeach, "Entry for this Topic Code Already Done!", this.Page);
            //}
            //else
            //{
            if (ViewState["TP_NO"] == null || ViewState["TP_NO"] == "0")
            {
                if (CheckDuplicateEntry() == true)
                {
                    objCommon.DisplayMessage(this.updTeach, "Entry for this Unit No. and Topic Code Already Done!", this.Page);
                    return;
                }

                CustomStatus cs = (CustomStatus)objTeachingPlanController.AddTeachingPlan(objExam, Istutorial, OrgId);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updTeach, "Teaching Plan Saved Successfully!", this.Page);
                }
                ddlSession.Enabled = true;
                ddlSection.Enabled = true;
                rfvSession.Enabled = true;
                rfvSession1.Enabled = true;
                rfvSection.Enabled = true;


                this.ResetDateDropdown();
                //ddlCourse.SelectedIndex = 0;
                //ddlSession.SelectedIndex = 0;
                //ddlScheme.SelectedIndex = 0;
                //ddlSubjectType.SelectedIndex = 0;
                //ddlSection.SelectedIndex = 0;
                //ddlSemester.SelectedIndex = 0;
                this.BindTopiccodeUnit();
            }
            else
            {
                objExam.TP_NO = Convert.ToInt32(ViewState["TP_NO"]);
                if (CheckDuplicateEntryEdit() == true)
                {
                    objCommon.DisplayMessage(this.updTeach, "Entry for this Unit No. and Topic Code Already Done!", this.Page);
                    return;
                }
                CustomStatus cs = (CustomStatus)objTeachingPlanController.UpdateTeachingPlan(objExam);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updTeach, "Teaching Plan Updated Successfully!", this.Page);
                }

                this.ResetDateDropdown();
                //ddlCourse.SelectedIndex = 0;
                //ddlSession.SelectedIndex = 0;
                //ddlScheme.SelectedIndex = 0;
                //ddlSubjectType.SelectedIndex = 0;
                //ddlSection.SelectedIndex = 0;
                //ddlSemester.SelectedIndex = 0;
                this.BindTopiccodeUnit();

                objCommon.DisplayMessage(this.updTeach, "Teaching plan cannot be edited, Please contact administrator", this.Page);
            }
            //}



            this.BindTeachingPlan();
            this.EnableControls(true);
            this.Clear();
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClearAllDates();
            ImageButton btnEditRecord = sender as ImageButton;
            int tp_no = Convert.ToInt32(btnEditRecord.CommandArgument);

            ViewState["EditTP_No"] = tp_no;

            #region Core Course

            DataSet dsedit = objCommon.FillDropDown("ACD_TEACHINGPLAN", "SESSIONNO", "UA_NO, DATE, COURSENO, CCODE, LECTURE_NO,SCHEMENO, ISNULL(SECTIONNO,0) SECTIONNO, ISNULL(BATCHNO,0) BATCHNO,ISNULL(TUTORIAL,0)TUTORIAL,ISNULL(SLOT,0)SLOTNO,SESSION_PLAN", "TP_NO =" + tp_no + " AND UA_NO=" + Session["userno"].ToString() + " AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "");
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);//Convert.ToInt32(dsedit.Tables[0].Rows[0]["SESSIONNO"].ToString());
            int uano = Convert.ToInt32(dsedit.Tables[0].Rows[0]["UA_NO"].ToString());
            DateTime date = Convert.ToDateTime(dsedit.Tables[0].Rows[0]["DATE"].ToString());
            int courseno = Convert.ToInt32(dsedit.Tables[0].Rows[0]["COURSENO"].ToString());
            string ccode = dsedit.Tables[0].Rows[0]["CCODE"].ToString();
            int lectureno = Convert.ToInt32(dsedit.Tables[0].Rows[0]["LECTURE_NO"].ToString());
            int sectionno = Convert.ToInt32(dsedit.Tables[0].Rows[0]["SECTIONNO"].ToString());
            int batchno = Convert.ToInt32(dsedit.Tables[0].Rows[0]["BATCHNO"].ToString());
            int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO =" + courseno));
            int slotno = Convert.ToInt32(dsedit.Tables[0].Rows[0]["SLOTNO"].ToString());
            int tutorial = Convert.ToInt32(dsedit.Tables[0].Rows[0]["TUTORIAL"].ToString());
            //int count = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(*)", "SESSIONNO =" + sessionno + " AND UA_NO =" + uano + " AND COURSENO =" + courseno + " AND SLOTNO =" + slotno + "AND BATCHNO=" + batchno + " AND (SECTIONNO=" + sectionno + " OR " + sectionno + "=0) AND ISNULL(ISTUTORIAL,0)=" + tutorial + " AND SCHEMENO =" + schemeno + " AND CONVERT(DATETIME,ATT_DATE,103)=CONVERT(DATETIME,'" + date.ToShortDateString() + "',103) AND COLLEGE_ID=" + ViewState["college_id"].ToString()));

            //if (count == 0)
            //{
            ddlLectureNo.Items.Clear();
            ddlLectureNo.Items.Add(new ListItem("Please Select", "0"));
            for (int i = 1; i <= 80; i++)
            {
                ddlLectureNo.Items.Add(new ListItem(i.ToString()));
            }

            DataSet ds = objTeachingPlanController.GetSingleTeachingPlanEntry(int.Parse(btnEditRecord.CommandArgument), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["college_id"].ToString()));
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.ResetDateDropdown();
                    ViewState["TP_NO"] = btnEditRecord.CommandArgument;
                    //Session
                    // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO <> 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
                    ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();

                    //objCommon.FillDropDownList(ddlSemester, "acd_course_teacher ct inner join acd_course c on (c.courseno = ct.courseno) inner join acd_semester sem on (sem.semesterno = c.semesterno)", "c.semesterno", "sem.SEMFULLNAME", "ua_no = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + "AND SESSIONNO =" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + "AND C.COURSENO = " + ds.Tables[0].Rows[0]["COURSENO"].ToString(), "C.SEMESTERNO");
                    //objCommon.FillDropDownList(ddlSemester, "acd_course_teacher ct inner join acd_course c on (c.courseno = ct.courseno) inner join acd_semester sem on (sem.semesterno = c.semesterno)", "c.semesterno", "sem.SEMFULLNAME", "(ua_no = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + " OR ADTEACHER ='" + ds.Tables[0].Rows[0]["UA_NO"].ToString() + "') AND SESSIONNO =" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + "AND C.COURSENO = " + ds.Tables[0].Rows[0]["COURSENO"].ToString() + " AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "C.SEMESTERNO");
                    objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO) INNER JOIN ACD_SUBJECTTYPE S ON (CT.SUBID = S.SUBID)INNER JOIN ACD_COURSE C ON (C.COURSENO = CT.COURSENO) INNER JOIN ACD_SCHEME SC ON (CT.SCHEMENO =SC.SCHEMENO)", "DISTINCT  CAST(CT.SEMESTERNO AS NVARCHAR(15))+' - '+CAST(CT.SUBID AS NVARCHAR(15))+' - '+CAST(C.COURSENO AS NVARCHAR(15)) AS NO", "SEM.SEMESTERNAME +' - '+S.SUBNAME+' - '+C.CCODE + ' - ' + C.COURSE_NAME AS ID", "(CT.UA_NO = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + " OR CT.ADTEACHER = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + ") AND CT.SESSIONNO  IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + ") AND CT.SCHEMENO =" + schemeno, "");


                    //ACD_COURSE

                    //DataSet ds1 = objCommon.FillDropDown("acd_course_teacher ct inner join acd_course c on (c.courseno = ct.courseno) inner join acd_semester sem on (sem.semesterno = c.semesterno)", "c.semesterno", "sem.SEMFULLNAME", "ua_no = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + "AND SESSIONNO =" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + "AND C.COURSENO = " + ds.Tables[0].Rows[0]["COURSENO"].ToString(), "C.SEMESTERNO");
                    // DataSet ds1 = objCommon.FillDropDown("acd_course_teacher ct inner join acd_course c on (c.courseno = ct.courseno) inner join acd_semester sem on (sem.semesterno = c.semesterno)", "c.semesterno", "sem.SEMFULLNAME", "(ua_no = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + " OR ADTEACHER ='" + ds.Tables[0].Rows[0]["UA_NO"].ToString() + "') AND SESSIONNO =" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + "AND C.COURSENO = " + ds.Tables[0].Rows[0]["COURSENO"].ToString() + " AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "C.SEMESTERNO");
                    DataSet ds1 = objCommon.FillDropDown("ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO) INNER JOIN ACD_SUBJECTTYPE S ON (CT.SUBID = S.SUBID)INNER JOIN ACD_COURSE C ON (C.COURSENO = CT.COURSENO) INNER JOIN ACD_SCHEME SC ON (CT.SCHEMENO =SC.SCHEMENO)", "DISTINCT  CAST(CT.SEMESTERNO AS NVARCHAR(15))+' - '+CAST(CT.SUBID AS NVARCHAR(15))+' - '+CAST(C.COURSENO AS NVARCHAR(15)) AS NO", "SEM.SEMESTERNAME +' - '+S.SUBNAME+' - '+C.CCODE + ' - ' + C.COURSE_NAME AS ID", "(CT.UA_NO = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + " OR CT.ADTEACHER = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + ") AND CT.SESSIONNO  IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + ") AND CT.SCHEMENO =" + schemeno + "AND CT.COURSENO=" + courseno, "");
                    ddlSemester.SelectedValue = ds1.Tables[0].Rows[0]["NO"].ToString();
                    // semesterno = ds1.Tables[0].Rows[0]["semesterno"].ToString() + '-' + subid + '-' + courseno;
                    //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO = CT.COURSENO) INNER JOIN ACD_SCHEME SC ON (CT.SCHEMENO =SC.SCHEMENO)", "C.COURSENO", "COURSE_NAME + ' ('+ SC.SCHEMENAME + ' )' AS COURSENAME", "ua_no = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + "AND SESSIONNO =" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + "AND C.SEMESTERNO in( " + ds1.Tables[0].Rows[0]["SEMESTERNO"].ToString() + ")", "C.COURSENO");
                    // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO = CT.COURSENO) INNER JOIN ACD_SCHEME SC ON (CT.SCHEMENO =SC.SCHEMENO)", "C.COURSENO", "COURSE_NAME + ' ('+ SC.SCHEMENAME + ' )' AS COURSENAME", "(ua_no = " + ds.Tables[0].Rows[0]["UA_NO"].ToString() + " OR ADTEACHER ='" + ds.Tables[0].Rows[0]["UA_NO"].ToString() + "') AND SESSIONNO =" + ds.Tables[0].Rows[0]["SESSIONNO"].ToString() + "AND C.SEMESTERNO in( " + ds1.Tables[0].Rows[0]["SEMESTERNO"].ToString() + ") AND COLLEGE_ID=" + ViewState["college_id"].ToString() + " AND ISNULL(CT.CANCEL,0)=0", "C.COURSENO");
                    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID");
                    // ddlCourse.SelectedValue = ds.Tables[0].Rows[0]["courseno"].ToString();
                    if (ds.Tables[0].Rows[0]["BATCHNO"].ToString() != "" || ds.Tables[0].Rows[0]["BATCHNO"].ToString() != "0")
                    {
                        objCommon.FillDropDownList(ddlBatch, "ACD_STUDENT_RESULT R, ACD_BATCH B", "DISTINCT B.BATCHNO", "B.BATCHNAME", "B.BATCHNO > 0", "B.BATCHNO");
                        ddlBatch.SelectedValue = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                    }
                    ddlUnitNo.SelectedValue = ds.Tables[0].Rows[0]["UNIT_NO"].ToString();
                    ddlLectureNo.SelectedValue = ds.Tables[0].Rows[0]["LECTURE_NO"].ToString();
                    txtSessionReq.Text = ds.Tables[0].Rows[0]["SESSION_PLAN"].ToString();
                    ViewState["LECT_NO"] = ds.Tables[0].Rows[0]["LECTURE_NO"].ToString();
                    txtLectureTopic.Text = ds.Tables[0].Rows[0]["TOPIC_COVERED"].ToString();
                    DateTime lectDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"].ToString());
                    string lectDay = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"].ToString()).DayOfWeek.ToString();
                    ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["COSCHNO"].ToString();


                    //string tt = ddlTimeTable.SelectedItem.Text;


                    //string TimeTableDates = ddlTimeTable.SelectedItem.Text;// Request.Form["msg"].ToString();
                    //string[] TimeTT;
                    //TimeTT = TimeTableDates.Split('-');
                    //DateTime fromDate = Convert.ToDateTime(TimeTT[0]);
                    //DateTime toDate = Convert.ToDateTime(TimeTT[1]);


                    //if (lectDate >= fromDate && lectDate <= toDate)
                    //{

                    //}

                    //if (ds.Tables[1].Rows.Count > 0)
                    //{

                    //}
                    // if (!lectDate.Equals("01/01/1753 00:00:00"))
                    //if (lectDate != Convert.ToDateTime("01/01/1753 00:00:00") && lectDate >= fromDate && lectDate <= toDate)
                    if (lectDate != Convert.ToDateTime("01/01/1753 00:00:00"))
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            ddlTimeTable.SelectedValue = ds.Tables[1].Rows[0]["TT_DATE_NO"].ToString();
                        }
                        else
                        {
                            ddlTimeTable.SelectedIndex = 0;
                            this.ResetDropdown();
                            return;
                        }
                        string stdate = string.Empty;//string.Format("{0:dd/MM/yyyy}");
                        string enddate = string.Empty;//string.Format("{0:dd/MM/yyyy}");
                        string[] dates = ds.Tables[1].Rows[0]["TT_DATE"].ToString().Split('-');
                        stdate = dates[0].Trim();
                        enddate = dates[1].Trim();
                        if (lectDay == "Monday")
                        {
                            // for Monday, no. is 1
                            this.FillDatesDropDown(ddlMon, 1, stdate, enddate);
                            this.FillSlotDropDown(ddlMonSlot, "SLOT1", 1, stdate, enddate);
                            ddlMon.Enabled = true;
                        }
                        if (lectDay == "Tuesday")
                        {
                            // for Tuesday, no. is 2
                            this.FillDatesDropDown(ddlTues, 2, stdate, enddate);
                            this.FillSlotDropDown(ddlTueSlot, "SLOT2", 2, stdate, enddate);
                            ddlTues.Enabled = true;
                        }
                        if (lectDay == "Wednesday")
                        {
                            // for Wednesday, no. is 3
                            this.FillDatesDropDown(ddlWed, 3, stdate, enddate);
                            this.FillSlotDropDown(ddlWedSlot, "SLOT3", 3, stdate, enddate);
                            ddlWed.Enabled = true;
                        }
                        if (lectDay == "Thursday")
                        {
                            // for Thursday, no. is 4
                            this.FillDatesDropDown(ddlThurs, 4, stdate, enddate);
                            this.FillSlotDropDown(ddlThusSlot, "SLOT4", 4, stdate, enddate);
                            ddlThurs.Enabled = true;
                        }
                        if (lectDay == "Friday")
                        {
                            // for Friday, no. is 5
                            this.FillDatesDropDown(ddlFri, 5, stdate, enddate);
                            this.FillSlotDropDown(ddlFriSlot, "SLOT5", 5, stdate, enddate);
                            ddlFri.Enabled = true;
                        }
                        if (lectDay == "Saturday")
                        {
                            // for Saturday, no. is 6
                            this.FillDatesDropDown(ddlSat, 6, stdate, enddate);
                            this.FillSlotDropDown(ddlSatSlot, "SLOT6", 6, stdate, enddate);
                            ddlSat.Enabled = true;
                        }

                        //ddlSemester.SelectedValue = semesterno.ToString();
                        if (lectDay == "Monday")
                        {
                            ddlMon.SelectedValue = ds.Tables[0].Rows[0]["DATE"].ToString();
                            if (ds.Tables[0].Rows[0]["SLOT"].ToString() != "")
                            {
                                ddlMonSlot.SelectedValue = ds.Tables[0].Rows[0]["SLOT"].ToString();
                            }
                            else
                            {
                                lblSlot.Text = "You haven't assign Slot";
                                lblSlot.Visible = true;
                            }
                        }
                        else if (lectDay == "Tuesday")
                        {
                            ddlTues.SelectedValue = ds.Tables[0].Rows[0]["DATE"].ToString();
                            if (ds.Tables[0].Rows[0]["SLOT"].ToString() != "")
                            {
                                ddlTueSlot.SelectedValue = ds.Tables[0].Rows[0]["SLOT"].ToString();
                            }
                            else
                            {
                                lblSlot.Text = "You haven't assign Slot";
                                lblSlot.Visible = true;
                            }

                        }
                        else if (lectDay == "Wednesday")
                        {
                            ddlWed.SelectedValue = ds.Tables[0].Rows[0]["DATE"].ToString();
                            if (ds.Tables[0].Rows[0]["SLOT"].ToString() != "")
                            {
                                ddlWedSlot.SelectedValue = ds.Tables[0].Rows[0]["SLOT"].ToString();
                            }
                            else
                            {
                                lblSlot.Text = "You haven't assign Slot";
                                lblSlot.Visible = true;
                            }
                        }
                        else if (lectDay == "Thursday")
                        {
                            ddlThurs.SelectedValue = ds.Tables[0].Rows[0]["DATE"].ToString();
                            if (ds.Tables[0].Rows[0]["SLOT"].ToString() != "")
                            {
                                ddlThusSlot.SelectedValue = ds.Tables[0].Rows[0]["SLOT"].ToString();
                            }
                            else
                            {
                                lblSlot.Text = "You haven't assign Slot";
                                lblSlot.Visible = true;
                            }
                        }
                        else if (lectDay == "Friday")
                        {
                            ddlFri.SelectedValue = ds.Tables[0].Rows[0]["DATE"].ToString();
                            if (ds.Tables[0].Rows[0]["SLOT"].ToString() != "")
                            {
                                ddlFriSlot.SelectedValue = ds.Tables[0].Rows[0]["SLOT"].ToString();
                            }
                            else
                            {
                                lblSlot.Text = "You haven't assign Slot";
                                lblSlot.Visible = true;
                            }
                        }
                        else if (lectDay == "Saturday")
                        {
                            ddlSat.SelectedValue = ds.Tables[0].Rows[0]["DATE"].ToString();
                            if (ds.Tables[0].Rows[0]["SLOT"].ToString() != "")
                            {
                                ddlSatSlot.SelectedValue = ds.Tables[0].Rows[0]["SLOT"].ToString();
                            }
                            else
                            {
                                lblSlot.Text = "You haven't assign Slot";
                                lblSlot.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        ddlTimeTable.SelectedIndex = 0;
                        this.ResetDropdown();
                    }
                    this.EnableControls(false);
                    txtLectureTopic.Focus();

                }
            }
            #endregion



        }
        catch
        {
            throw;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TeachingPlanController objTPC = new TeachingPlanController();
            ImageButton btnDelete = sender as ImageButton;
            int tp_no = Convert.ToInt32(btnDelete.CommandArgument);
            int batchno = 0;
            if (ddlBatch.SelectedIndex > 0)
            {
                batchno = Convert.ToInt32(ddlBatch.SelectedValue);
            }

            //as per discussed with sagar pandya sir we have added delete condition for pas date teaching plan
            //DateTime dtTP = Convert.ToDateTime(objCommon.LookUp("ACD_TEACHINGPLAN", "[DATE]", "TP_NO=" + tp_no));
            //DateTime curDate = Convert.ToDateTime(objCommon.LookUp("REFF", "GETDATE()", ""));
            //if (dtTP.Date < curDate.Date)
            //{
            //    objCommon.DisplayMessage(this.updTeach, "You cannot Delete the teaching plan for a past Date !!", this.Page);
            //    return;
            //}
            int Sessionno = int.Parse(btnDelete.CommandArgument);
            DataSet ds = objCommon.FillDropDown("ACD_TEACHINGPLAN", "SESSIONNO", "UA_NO, DATE, COURSENO, CCODE, LECTURE_NO,SCHEMENO,ISNULL(SECTIONNO,0) SECTIONNO,ISNULL(TUTORIAL,0)TUTORIAL,ISNULL(SLOT,0)SLOTNO", "TP_NO =" + Convert.ToInt32(btnDelete.ToolTip) + " AND UA_NO=" + Session["userno"].ToString(), "");
            int sessionno = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONNO"].ToString());
            int uano = Convert.ToInt32(ds.Tables[0].Rows[0]["UA_NO"].ToString());
            DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"].ToString());
            int sectionno = Convert.ToInt32(ds.Tables[0].Rows[0]["SECTIONNO"].ToString());
            int courseno = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSENO"].ToString());
            string ccode = ds.Tables[0].Rows[0]["CCODE"].ToString();
            int lectureno = Convert.ToInt32(ds.Tables[0].Rows[0]["LECTURE_NO"].ToString());
            int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO =" + courseno));
            int slotno = Convert.ToInt32(ds.Tables[0].Rows[0]["SLOTNO"].ToString());
            int subid = 0;
            int TUTORIAL = Convert.ToInt32(ds.Tables[0].Rows[0]["TUTORIAL"].ToString());


            //if (TUTORIAL == 1)
            //    subid = 100;
            //else
            subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO =" + courseno));

            int count = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(*)", "SESSIONNO =" + sessionno + " AND UA_NO =" + uano + " AND COURSENO =" + courseno + " AND SLOTNO =" + slotno + " AND (BATCHNO =" + batchno + " OR " + batchno + " = 0) AND ISNULL(ISTUTORIAL,0)=" + TUTORIAL + " AND SUBID = " + subid + " AND (SECTIONNO = " + sectionno + " OR SECTIONNO IS NULL ) AND SCHEMENO =" + schemeno + " AND CONVERT(DATETIME,ATT_DATE,103)=CONVERT(DATETIME,'" + date.ToShortDateString() + "',103) AND ISNULL(CANCEL,0)=0"));

            //DateTime curDate = Convert.ToDateTime(objCommon.LookUp("REFF", "GETDATE()", ""));
            //if (dtTP.Date < curDate.Date)
            //{
            //    objCommon.DisplayMessage(this.updTeach, "You cannot Edit the teaching plan for a past Date !!", this.Page);
            //    return;
            //}

            //Delete 
            if (count == 0)
            {
                CustomStatus cs = (CustomStatus)objTPC.DeleteTeachingPlan(Convert.ToInt32(btnDelete.ToolTip), Convert.ToInt32(Session["userno"].ToString()));
                objCommon.DisplayMessage(this.updTeach, "Teaching Plan Entry Deleted Succesfully !!", this.Page);
                this.BindTeachingPlan();
            }
            else
            {
                objCommon.DisplayMessage(this.updTeach, "You cannot delete the teaching plan as Attendance for this Teaching Plan is done !!", this.Page);
            }
            this.Clear();
            return;
        }
        catch
        {
            throw;
        }
    }

    private void ResetDropdown()
    {
        ddlMon.Enabled = false;
        if (ddlMon.SelectedIndex > 0)
            ddlMon.SelectedIndex = 0;
        ddlTues.Enabled = false;
        if (ddlTues.SelectedIndex > 0)
            ddlTues.SelectedIndex = 0;
        ddlWed.Enabled = false;
        if (ddlWed.SelectedIndex > 0)
            ddlWed.SelectedIndex = 0;
        ddlThurs.Enabled = false;
        if (ddlThurs.SelectedIndex > 0)
            ddlThurs.SelectedIndex = 0;
        ddlFri.Enabled = false;
        if (ddlFri.SelectedIndex > 0)
            ddlFri.SelectedIndex = 0;
        ddlSat.Enabled = false;
        if (ddlSat.SelectedIndex > 0)
            ddlSat.SelectedIndex = 0;

        ddlMonSlot.Enabled = false;
        if (ddlMonSlot.SelectedIndex > 0)
            ddlMonSlot.SelectedIndex = 0;
        ddlTueSlot.Enabled = false;
        if (ddlTueSlot.SelectedIndex > 0)
            ddlTueSlot.SelectedIndex = 0;
        ddlWedSlot.Enabled = false;
        if (ddlWedSlot.SelectedIndex > 0)
            ddlWedSlot.SelectedIndex = 0;
        ddlThusSlot.Enabled = false;
        if (ddlThusSlot.SelectedIndex > 0)
            ddlThusSlot.SelectedIndex = 0;
        ddlFriSlot.Enabled = false;
        if (ddlFriSlot.SelectedIndex > 0)
            ddlFriSlot.SelectedIndex = 0;
        ddlSatSlot.Enabled = false;
        if (ddlSatSlot.SelectedIndex > 0)
            ddlSatSlot.SelectedIndex = 0;
    }

    private void ClearAllDates()
    {
        ///Dates
        ddlMon.SelectedIndex = 0;
        ddlTues.SelectedIndex = 0;
        ddlWed.SelectedIndex = 0;
        ddlThurs.SelectedIndex = 0;
        ddlFri.SelectedIndex = 0;
        ddlSat.SelectedIndex = 0;

        ///Slots
        ddlMonSlot.SelectedIndex = 0;
        ddlTueSlot.SelectedIndex = 0;
        ddlWedSlot.SelectedIndex = 0;
        ddlThusSlot.SelectedIndex = 0;
        ddlFriSlot.SelectedIndex = 0;
        ddlSatSlot.SelectedIndex = 0;
    }

    private bool CheckDuplicateUploadEntry(int unit_no, int lecture_no, int sectionno, int batchno, int slot)
    {
        bool flag = false;
        try
        {
            string TP_NO = string.Empty;
            if (Convert.ToInt32(ddlTutorial.SelectedValue) == 2)
            {
                //TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN", "TP_NO", "COURSENO=" + ddlCourse.SelectedValue + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND UNIT_NO=" + unit_no + " AND LECTURE_NO=" + lecture_no + " AND SECTIONNO = " + sectionno + " AND BATCHNO=" + batchno + " AND SLOT=" + slot + "  AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + "AND TUTORIAL = 1");
                TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN", "TP_NO", "COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND 2793SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID =" + ddlSession.SelectedValue + ") AND TERM=" + ddlSemester.SelectedValue + " AND UNIT_NO=" + unit_no + " AND LECTURE_NO=" + lecture_no + " AND SECTIONNO = " + sectionno + " AND BATCHNO=" + batchno + " AND SLOT=" + slot + "  AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + "AND TUTORIAL = 1 AND OrganizationId=" + Session["OrgId"].ToString());
            }
            else
            {
                TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN", "TP_NO", "COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID =" + ddlSession.SelectedValue + ") AND TERM=" + ddlSemester.SelectedValue + " AND UNIT_NO=" + unit_no + " AND LECTURE_NO=" + lecture_no + " AND SECTIONNO = " + sectionno + " AND BATCHNO=" + batchno + " AND SLOT=" + slot + " AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + "AND (TUTORIAL = 0 or TUTORIAL IS NULL) AND OrganizationId=" + Session["OrgId"].ToString());
            }
            if (TP_NO != null && TP_NO != string.Empty)
            {
                flag = true;
            }
        }
        catch
        {
            throw;
        }
        return flag;
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            string TP_NO = string.Empty;
            if (Convert.ToInt32(ViewState["SUBID"].ToString()) == 100)
            {
                TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN", "TP_NO", "COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND UNIT_NO=" + ddlUnitNo.SelectedValue + " AND LECTURE_NO=" + ddlLectureNo.SelectedValue + " AND SECTIONNO = " + ddlSection.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + "  AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + "AND TUTORIAL = 1 AND COLLEGE_ID=" + ViewState["college_id"].ToString());
            }
            else
            {
                TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN", "TP_NO", "COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND UNIT_NO=" + ddlUnitNo.SelectedValue + " AND LECTURE_NO=" + ddlLectureNo.SelectedValue + " AND SECTIONNO = " + ddlSection.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + " AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + "AND (TUTORIAL = 0 or TUTORIAL IS NULL) AND COLLEGE_ID=" + ViewState["college_id"].ToString());
            }
            if (TP_NO != null && TP_NO != string.Empty)
            {
                flag = true;
            }
        }
        catch
        {
            throw;
        }
        return flag;
    }

    private bool CheckDuplicateEntryEdit()
    {
        bool flag = false;
        try
        {
            string TP_NO = string.Empty;
            if (Convert.ToInt32(ViewState["SUBID"].ToString()) == 100)
            {
                TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN", "TP_NO", "COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND UNIT_NO=" + ddlUnitNo.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + " AND LECTURE_NO=" + ddlLectureNo.SelectedValue + " AND UA_NO=" + Session["userno"].ToString() + " AND TERM=" + ddlSemester.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + "AND TUTORIAL = 1 AND TP_NO !=" + ViewState["TP_NO"] + " AND COLLEGE_ID=" + ViewState["college_id"].ToString());
            }
            else
            {
                TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN", "TP_NO", "COURSENO=" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND UNIT_NO=" + ddlUnitNo.SelectedValue + " AND BATCHNO=" + Convert.ToInt32(ddlBatch.SelectedValue) + " AND LECTURE_NO=" + ddlLectureNo.SelectedValue + " AND UA_NO=" + Session["userno"].ToString() + " AND TERM=" + ddlSemester.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND TP_NO != " + ViewState["TP_NO"] + " AND COLLEGE_ID=" + ViewState["college_id"].ToString());
            }
            if (TP_NO != null && TP_NO != string.Empty)
            {
                flag = true;
            }
        }
        catch
        {
            throw;
        }
        return flag;
    }

    private void EnableControls(bool flag)
    {
        ddlSession.Enabled = flag;
        ddlSemester.Enabled = flag;
        ddlCourse.Enabled = flag;
        ddlSection.Enabled = flag;
        ddlScheme.Enabled = flag;
        ddlSubjectType.Enabled = flag;
    }

    private void Clear()
    {
        txtLectureTopic.Text = string.Empty;
        ViewState["TP_NO"] = null;
        ViewState["delete"] = null;
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegreeEX.SelectedIndex = 0;
            btnDownload.Enabled = false;
            btnUpload.Enabled = false;
            btnBlankDownld.Enabled = false;
            BindTeachingPlan();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlDegreeEX_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegreeEX.SelectedIndex > 0)
        {
            btnDownload.Enabled = true;
            btnBlankDownld.Enabled = true;
            btnUpload.Enabled = true;
        }
    }
    protected void ddlUnitNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUnitNo.SelectedIndex > 0)
            {
                ddlLectureNo.Items.Clear();
                ddlLectureNo.Items.Insert(0, new ListItem("Please Select", "0"));


                int batchno = 0;
                if (ddlBatch.SelectedIndex > 0)
                {
                    batchno = Convert.ToInt32(ddlBatch.SelectedValue);
                }
                if (Convert.ToInt32(ViewState["SUBID"].ToString()) == 100)
                {
                    for (int i = 1; i <= 80; i++)
                    {
                        int count = Convert.ToInt32(objCommon.LookUp("ACD_TEACHINGPLAN", "COUNT(*)", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND UA_NO =" + Session["userno"] + "AND SECTIONNO =" + Convert.ToInt32(ddlSection.SelectedValue) + " AND (BATCHNO = " + batchno + " OR " + batchno + " = 0) AND COURSENO =" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND UNIT_NO=" + Convert.ToInt32(ddlUnitNo.SelectedValue) + "AND LECTURE_NO=" + Convert.ToInt32(i) + " AND TUTORIAL = 1 AND COLLEGE_ID=" + ViewState["college_id"].ToString()));
                        if (count == 0)
                        {
                            ddlLectureNo.Items.Add(new ListItem(i.ToString()));
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 80; i++)
                    {
                        int count = Convert.ToInt32(objCommon.LookUp("ACD_TEACHINGPLAN", "COUNT(*)", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND UA_NO =" + Session["userno"] + "AND SECTIONNO =" + Convert.ToInt32(ddlSection.SelectedValue) + " AND (BATCHNO = " + batchno + " OR " + batchno + " = 0) AND COURSENO =" + Convert.ToInt32(ViewState["courseno"].ToString()) + " AND UNIT_NO=" + Convert.ToInt32(ddlUnitNo.SelectedValue) + "AND LECTURE_NO=" + Convert.ToInt32(i) + " AND TUTORIAL = 0  AND ISNULL(CANCEL,0)=0 AND COLLEGE_ID=" + ViewState["college_id"].ToString() + " AND SCHEMENO =" + ViewState["schemeno"].ToString() + ""));
                        if (count == 0)
                        {
                            ddlLectureNo.Items.Add(new ListItem(i.ToString()));
                        }
                    }
                }

            }
            else
            {
                ddlLectureNo.Items.Clear();
                ddlLectureNo.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO = CT.SCHEMENO)", "DISTINCT CT.SCHEMENO", "SC.SCHEMENAME", "(CT.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR CT.ADTEACHER = " + Convert.ToInt32(Session["userno"]) + " ) AND CT.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"].ToString()), "CT.SCHEMENO");

            lvTeachingPlan.DataSource = null;
            lvTeachingPlan.DataBind();
            lvTeachingPlanGlobalElective.DataSource = null;
            lvTeachingPlanGlobalElective.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        int colgid = Convert.ToInt32(ViewState["college_id"]) == null ? 0 : Convert.ToInt32(ViewState["college_id"]);
        int uano = Convert.ToInt32(Session["userno"]) == null ? 0 : Convert.ToInt32(Session["userno"]);
        int schemeno = Convert.ToInt32(ViewState["schemeno"]) == null ? 0 : Convert.ToInt32(ViewState["schemeno"]);
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);

        string MSG = ddlSemester.SelectedValue.ToString();// Request.Form["msg"].ToString();
        string[] repoarray;
        repoarray = MSG.Split('-');
        int semesterno = Convert.ToInt32(repoarray[0]);
        int SUBID = Convert.ToInt32(repoarray[1]);
        int courseno = Convert.ToInt32(repoarray[2]);

        int sectionno = Convert.ToInt32(ddlSection.SelectedIndex) > 0 ? Convert.ToInt32(ddlSection.SelectedValue) : 0;
        int batchno = Convert.ToInt32(ddlBatch.SelectedIndex) > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
        int is_tutorial = Convert.ToInt32(ddlTutorial.SelectedIndex) > 0 ? Convert.ToInt32(ddlTutorial.SelectedValue) : 0;

        DataSet ds = objTeachingPlanController.GetTeachingPlanExcelReport(colgid, schemeno, sessionno, semesterno, courseno, uano, sectionno, batchno, is_tutorial);

        ds.Tables[0].TableName = "ActualVSProposedTP";


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=TeachingPlanExcel_ActualVSProposed_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updTeach, "No Data Found!!", this.Page);
        }
    }

    #region new Global

    protected void btnEditGlobalElective_Click(object sender, ImageClickEventArgs e)
    {

        this.ClearAllDates();
        ImageButton btnEditGlobalElective = sender as ImageButton;
        int tp_no = Convert.ToInt32(btnEditGlobalElective.CommandArgument);

        ViewState["EditGlobalTP_No"] = tp_no;


        BindTopiccodeUnitGlobalElective();
        ddlLectureNoGlobal.Items.Clear();
        ddlLectureNoGlobal.Items.Add(new ListItem("Please Select", "0"));
        for (int i = 1; i <= 80; i++)
        {
            ddlLectureNoGlobal.Items.Add(new ListItem(i.ToString()));
        }


        DataSet dsGlobal = objTeachingPlanController.GetSingleTeachingPlanEntryGlobalElectiveModified(tp_no, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlSessionGlobal.SelectedValue));
        if (dsGlobal != null && dsGlobal.Tables.Count > 0)
        {
            if (dsGlobal.Tables[0].Rows.Count > 0)
            {
                this.ResetDateDropdownGlobalElective();
                ViewState["GlobalTP_NO"] = btnEditGlobalElective.CommandArgument;
                //Session

                ddlUnitNoGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["UNIT_NO"].ToString();
                ddlLectureNoGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["LECTURE_NO"].ToString();
                ViewState["GlobalLECT_NO"] = dsGlobal.Tables[0].Rows[0]["LECTURE_NO"].ToString();
                txtLectureTopicGlobal.Text = dsGlobal.Tables[0].Rows[0]["TOPIC_COVERED"].ToString();
                DateTime lectDate = Convert.ToDateTime(dsGlobal.Tables[0].Rows[0]["DATE"].ToString());
                string lectDay = Convert.ToDateTime(dsGlobal.Tables[0].Rows[0]["DATE"].ToString()).DayOfWeek.ToString();
                //ddlScheme.SelectedValue = dsGlobal.Tables[0].Rows[0]["COSCHNO"].ToString();
                ddlSessionGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["SESSIONID"].ToString();

                string[] ttDates = ddlTimeTableDateGlobal.SelectedItem.Text.Split('-');
                string startDate; string endDate;
                startDate = ttDates[0].Trim();
                endDate = ttDates[1].Trim();

                //ddlSemester.SelectedValue = semesterno.ToString();
                if (lectDay == "Monday")
                {
                    this.FillDatesDropDownGlobalElective(ddlMonGlobal, 1, startDate, endDate);
                    this.FillSlotDropDownGlobalElective(ddlMonSlotGlobal, "SLOT1", 1, startDate, endDate);


                    ddlMonGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["DATE"].ToString();
                    if (dsGlobal.Tables[0].Rows[0]["SLOT"].ToString() != "")
                    {
                        ddlMonSlotGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["SLOT"].ToString();
                    }
                    else
                    {
                        lblSlot.Text = "You haven't assign Slot";
                        lblSlot.Visible = true;
                    }
                }
                else if (lectDay == "Tuesday")
                {
                    this.FillDatesDropDownGlobalElective(ddlTuesGlobal, 1, startDate, endDate);
                    this.FillSlotDropDownGlobalElective(ddlTueSlotGlobal, "SLOT1", 1, startDate, endDate);

                    ddlTuesGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["DATE"].ToString();
                    if (dsGlobal.Tables[0].Rows[0]["SLOT"].ToString() != "")
                    {
                        ddlTueSlotGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["SLOT"].ToString();
                    }
                    else
                    {
                        lblSlot.Text = "You haven't assign Slot";
                        lblSlot.Visible = true;
                    }

                }
                else if (lectDay == "Wednesday")
                {
                    this.FillDatesDropDownGlobalElective(ddlWedGlobal, 1, startDate, endDate);
                    this.FillSlotDropDownGlobalElective(ddlWedSlotGlobal, "SLOT1", 1, startDate, endDate);

                    ddlWedGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["DATE"].ToString();
                    if (dsGlobal.Tables[0].Rows[0]["SLOT"].ToString() != "")
                    {
                        ddlWedSlotGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["SLOT"].ToString();
                    }
                    else
                    {
                        lblSlot.Text = "You haven't assign Slot";
                        lblSlot.Visible = true;
                    }
                }
                else if (lectDay == "Thursday")
                {
                    this.FillDatesDropDownGlobalElective(ddlThursGlobal, 1, startDate, endDate);
                    this.FillSlotDropDownGlobalElective(ddlThusSlotGlobal, "SLOT1", 1, startDate, endDate);

                    ddlThursGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["DATE"].ToString();
                    if (dsGlobal.Tables[0].Rows[0]["SLOT"].ToString() != "")
                    {
                        ddlThusSlotGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["SLOT"].ToString();
                    }
                    else
                    {
                        lblSlot.Text = "You haven't assign Slot";
                        lblSlot.Visible = true;
                    }
                }
                else if (lectDay == "Friday")
                {
                    this.FillDatesDropDownGlobalElective(ddlFriGlobal, 1, startDate, endDate);
                    this.FillSlotDropDownGlobalElective(ddlFriSlotGlobal, "SLOT1", 1, startDate, endDate);

                    ddlFriGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["DATE"].ToString();
                    if (dsGlobal.Tables[0].Rows[0]["SLOT"].ToString() != "")
                    {
                        ddlFriSlotGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["SLOT"].ToString();
                    }
                    else
                    {
                        lblSlot.Text = "You haven't assign Slot";
                        lblSlot.Visible = true;
                    }
                }
                else if (lectDay == "Saturday")
                {
                    this.FillDatesDropDownGlobalElective(ddlSatGlobal, 1, startDate, endDate);
                    this.FillSlotDropDownGlobalElective(ddlSatSlotGlobal, "SLOT1", 1, startDate, endDate);

                    ddlSatGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["DATE"].ToString();
                    if (dsGlobal.Tables[0].Rows[0]["SLOT"].ToString() != "")
                    {
                        ddlSatSlotGlobal.SelectedValue = dsGlobal.Tables[0].Rows[0]["SLOT"].ToString();
                    }
                    else
                    {
                        lblSlot.Text = "You haven't assign Slot";
                        lblSlot.Visible = true;
                    }
                }

                this.EnableControlsGlobalElective(false);
                txtLectureTopicGlobal.Focus();
            }
        }

    }
    protected void btnDeleteGlobalElective_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            TeachingPlanController objTPC = new TeachingPlanController();
            ImageButton btnDeleteGlobalElective = sender as ImageButton;
            int tp_no = Convert.ToInt32(btnDeleteGlobalElective.CommandArgument);
            int batchno = 0;



            DataSet ds = objCommon.FillDropDown("ACD_TEACHINGPLAN TP INNER JOIN ACD_SESSION_MASTER SM ON(TP.SESSIONNO=SM.SESSIONNO AND TP.COLLEGE_ID= SM.COLLEGE_ID)", "DISTINCT UA_NO", "DATE, COURSENO, CCODE, LECTURE_NO,ISNULL(SLOT,0)SLOTNO,SM.SESSIONID", "TP_NO =" + Convert.ToInt32(btnDeleteGlobalElective.ToolTip) + " AND UA_NO=" + Session["userno"].ToString(), "");

            int uano = Convert.ToInt32(ds.Tables[0].Rows[0]["UA_NO"].ToString());
            DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[0]["DATE"].ToString());

            int courseno = Convert.ToInt32(ds.Tables[0].Rows[0]["COURSENO"].ToString());
            string ccode = ds.Tables[0].Rows[0]["CCODE"].ToString();
            int lectureno = Convert.ToInt32(ds.Tables[0].Rows[0]["LECTURE_NO"].ToString());
            int sessionno = Convert.ToInt32(ds.Tables[0].Rows[0]["SESSIONID"].ToString());
            int slotno = Convert.ToInt32(ds.Tables[0].Rows[0]["SLOTNO"].ToString());
            int subid = 0;


            subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO =" + courseno));

            int count = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "COUNT(*)", "UA_NO =" + uano + " AND COURSENO =" + courseno + " AND SLOTNO =" + slotno + " AND SUBID = " + subid + " AND SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + sessionno + ") AND CONVERT(DATETIME,ATT_DATE,103)=CONVERT(DATETIME,'" + date.ToShortDateString() + "',103)"));

            if (count == 0)
            {
                CustomStatus cs = (CustomStatus)objTPC.DeleteTeachingPlan(Convert.ToInt32(btnDeleteGlobalElective.ToolTip), Convert.ToInt32(Session["userno"].ToString()));
                objCommon.DisplayMessage(this.updGlobal, "Teaching Plan Entry Deleted Succesfully !!", this.Page);
                this.BindTeachingPlanGlobalElective();
            }
            else
            {
                objCommon.DisplayMessage(this.updGlobal, "You cannot delete the teaching plan as Attendance for this Teaching Plan is done !!", this.Page);
            }
            this.ClearGlobalElective();
            return;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSessionGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemesterGlobal.SelectedIndex = -1;
        ddlUnitNoGlobal.SelectedIndex = -1;
        ddlLectureNoGlobal.SelectedIndex = -1;
        ddlTimeTableDateGlobal.SelectedIndex = -1;
        txtLectureTopicGlobal.Text = "";
        ddlLectureNoGlobal.SelectedIndex = -1;
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();

        if (ddlSessionGlobal.SelectedIndex > 0)
        {


            DataSet ds1 = objCC.GetGlobalOfferedCourseList(Convert.ToInt32(ddlSessionGlobal.SelectedValue), 0, Convert.ToInt32(Session["userno"]), 8);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlSemesterGlobal.DataSource = ds1;
                ddlSemesterGlobal.DataValueField = ds1.Tables[0].Columns[0].ToString();
                ddlSemesterGlobal.DataTextField = ds1.Tables[0].Columns[1].ToString();
                ddlSemesterGlobal.DataBind();
                //ddlSession.SelectedIndex = 0;
            }


        }
        else
        {
            ddlSemesterGlobal.Items.Clear();
            ddlSemesterGlobal.Items.Add(new ListItem("Please Select", "0"));
            ddlTimeTableDateGlobal.Items.Clear();
            ddlTimeTableDateGlobal.Items.Add(new ListItem("Please Select", "0"));
            ddlGlobalSection.Items.Clear();
            ddlGlobalSection.Items.Add(new ListItem("Please Select", "0"));
            ddlUnitNoGlobal.Items.Clear();
            ddlUnitNoGlobal.Items.Add(new ListItem("Please Select", "0"));
            ddlLectureNoGlobal.Items.Clear();
            ddlLectureNoGlobal.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    protected void ddlSemesterGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();
        txtLectureTopicGlobal.Text = "";
        if (ddlSemesterGlobal.SelectedIndex > 0)
        {

            string MSG = ddlSemesterGlobal.SelectedValue.ToString();// Request.Form["msg"].ToString();
            string[] repoarray;
            repoarray = MSG.Split('-');
            string SUBID = repoarray[0].ToString();
            string courseno = repoarray[1].ToString();
            ViewState["globalSUBID"] = SUBID;
            ViewState["globalcourseno"] = courseno;
            objCommon.FillDropDownList(ddlGlobalSection, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SECTION S ON(CT.SECTIONNO=S.SECTIONNO) INNER JOIN ACD_TIME_TABLE_CONFIG TTG ON(TTG.CTNO= CT.CT_NO) INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONNO=CT.SESSIONNO)", "DISTINCT CT.SECTIONNO", "S.SECTIONNAME", "CT.COURSENO=" + Convert.ToInt32(courseno) + " AND SM.SESSIONID =" + Convert.ToInt32(ddlSessionGlobal.SelectedValue) + " AND ISNULL(TTG.CANCEL,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND (CT.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR ISNULL(CT.ADTEACHER,0) =" + Convert.ToInt32(Session["userno"]) + ")", "CT.SECTIONNO");
            ddlGlobalSection.Focus();

        }
        else
        {
           
            ddlTimeTableDateGlobal.Items.Clear();
            ddlTimeTableDateGlobal.Items.Add(new ListItem("Please Select", "0"));
            ddlGlobalSection.Items.Clear();
            ddlGlobalSection.Items.Add(new ListItem("Please Select", "0"));
            ddlUnitNoGlobal.Items.Clear();
            ddlUnitNoGlobal.Items.Add(new ListItem("Please Select", "0"));
            ddlLectureNoGlobal.Items.Clear();
            ddlLectureNoGlobal.Items.Add(new ListItem("Please Select", "0"));
        }

    }
    protected void ddlTimeTableDateGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTimeTableDateGlobal.SelectedIndex > 0)
        {

            txtLectureTopicGlobal.Text = string.Empty;
            ddlMonGlobal.Enabled = false;
            ddlTuesGlobal.Enabled = false;
            ddlWedGlobal.Enabled = false;
            ddlThursGlobal.Enabled = false;
            ddlFriGlobal.Enabled = false;
            ddlSatGlobal.Enabled = false;
            ddlMonSlotGlobal.Enabled = false;
            ddlTueSlotGlobal.Enabled = false;
            ddlWedSlotGlobal.Enabled = false;
            ddlThusSlotGlobal.Enabled = false;
            ddlFriSlotGlobal.Enabled = false;
            ddlSatSlotGlobal.Enabled = false;
            lvTeachingPlanGlobalElective.DataSource = null;
            lvTeachingPlanGlobalElective.DataBind();
            string MSG = ddlSemesterGlobal.SelectedValue.ToString();// Request.Form["msg"].ToString();
            string[] repoarray;
            repoarray = MSG.Split('-');
            string SUBID = repoarray[0].ToString();
            string courseno = repoarray[1].ToString();
            string[] ttDates = ddlTimeTableDateGlobal.SelectedItem.Text.Split('-');
            string startDate; string endDate;
            startDate = ttDates[0].Trim();
            endDate = ttDates[1].Trim();

            DataSet ds = objTeachingPlanController.GetDayTimeSlotsGlobalElective(Convert.ToInt32(Session["userno"]), Convert.ToInt32(courseno), Convert.ToInt32(SUBID), Convert.ToInt32(ddlSessionGlobal.SelectedValue), startDate, endDate);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int mon = 0, tue = 0, wed = 0, thu = 0, fri = 0, sat = 0;
                if (!(ds.Tables[0].Rows[0]["DAYNO"].ToString() == string.Empty))
                { //(!(ds.Tables[0].Rows[0]["DAY1"].ToString() == string.Empty))
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 1)
                            mon = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 1 ? 1 : 0;
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 2)
                            tue = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 2 ? 1 : 0;
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 3)
                            wed = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 3 ? 1 : 0;
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 4)
                            thu = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 4 ? 1 : 0;
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 5)
                            fri = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 5 ? 1 : 0;
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 6)
                            sat = Convert.ToInt32(ds.Tables[0].Rows[i]["DAYNO"].ToString()) == 6 ? 1 : 0;
                    }
                }

                DataSet ds1 = objCommon.FillDropDown("ACD_GLOBAL_OFFERED_COURSE AS S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONNO=SM.SESSIONNO) INNER JOIN ACD_ATTENDANCE_CONFIG AC ON(AC.SESSIONNO = S.SESSIONNO AND AC.COLLEGE_ID = S.COLLEGE_ID)", "DISTINCT START_DATE", "END_DATE", "SM.SESSIONID=" + Convert.ToInt32(ddlSessionGlobal.SelectedValue) + " AND ISNULL(S.GLOBAL_OFFERED,0) = 1 AND ISNULL(SM.FLOCK,0)=1 AND ISNULL(SM.IS_ACTIVE,0)=1 AND ISNULL(AC.GLOBAL_ELECTIVE,0)=1 AND ISNULL(AC.ACTIVE,0) =1", "");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    // string stdate = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(ds1.Tables[0].Rows[0]["START_DATE"].ToString()));
                    //string enddate = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(ds1.Tables[0].Rows[0]["END_DATE"].ToString()));
                    string stdate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(ds1.Tables[0].Rows[0]["START_DATE"].ToString()));
                    string enddate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(ds1.Tables[0].Rows[0]["END_DATE"].ToString()));
                    string[] dates = ddlTimeTableDateGlobal.SelectedItem.Text.Split('-');
                    stdate = dates[0].Trim();
                    enddate = dates[1].Trim();
                    if (mon == 1)
                    {
                        // for Monday, no. is 1
                        this.FillDatesDropDownGlobalElective(ddlMonGlobal, 1, stdate, enddate);
                        this.FillSlotDropDownGlobalElective(ddlMonSlotGlobal, "SLOT1", 1, stdate, enddate);
                        ddlMonGlobal.Enabled = true;
                    }
                    if (tue == 1)
                    {
                        // for Tuesday, no. is 2
                        this.FillDatesDropDownGlobalElective(ddlTuesGlobal, 2, stdate, enddate);
                        this.FillSlotDropDownGlobalElective(ddlTueSlotGlobal, "SLOT2", 2, stdate, enddate);
                        ddlTuesGlobal.Enabled = true;
                    }
                    if (wed == 1)
                    {
                        // for Wednesday, no. is 3
                        this.FillDatesDropDownGlobalElective(ddlWedGlobal, 3, stdate, enddate);
                        this.FillSlotDropDownGlobalElective(ddlWedSlotGlobal, "SLOT3", 3, stdate, enddate);
                        ddlWedGlobal.Enabled = true;
                    }
                    if (thu == 1)
                    {
                        // for Thursday, no. is 4
                        this.FillDatesDropDownGlobalElective(ddlThursGlobal, 4, stdate, enddate);
                        this.FillSlotDropDownGlobalElective(ddlThusSlotGlobal, "SLOT4", 4, stdate, enddate);
                        ddlThurs.Enabled = true;
                    }
                    if (fri == 1)
                    {
                        // for Friday, no. is 5
                        this.FillDatesDropDownGlobalElective(ddlFriGlobal, 5, stdate, enddate);
                        this.FillSlotDropDownGlobalElective(ddlFriSlotGlobal, "SLOT5", 5, stdate, enddate);
                        ddlFriGlobal.Enabled = true;
                    }
                    if (sat == 1)
                    {
                        // for Saturday, no. is 6
                        this.FillDatesDropDownGlobalElective(ddlSatGlobal, 6, stdate, enddate);
                        this.FillSlotDropDownGlobalElective(ddlSatSlotGlobal, "SLOT6", 6, stdate, enddate);
                        ddlSatGlobal.Enabled = true;
                    }
                }
            }

            if (Session["usertype"].ToString() != "1")
            {
                this.BindTeachingPlanGlobalElective();
                this.BindTopiccodeUnitGlobalElective();
                //btnLock.Enabled = false;
                //this.CheckLockStatus();
            }
        }

    }

    //Added by Swapnil For Global Elective
    private void FillDatesDropDownGlobalElective(DropDownList ddldate, int day, string stdate, string enddate)
    {
        DataSet ds = objTeachingPlanController.GetTeachingPlanDateGlobalElective(stdate, enddate, day, Convert.ToInt32(ddlSessionGlobal.SelectedValue));

        ddldate.Items.Clear();
        ddldate.Items.Add("Please Select");
        ddldate.SelectedItem.Value = "0";
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldate.DataSource = ds;
            ddldate.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddldate.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddldate.DataBind();
            ddldate.SelectedIndex = 0;
        }

        ddldate.Enabled = true;
    }

    //Added by Swapnil for Global Elective
    private void FillSlotDropDownGlobalElective(DropDownList ddllist, string slot, int DAYNO, string startdate, string enddate)
    {
        TeachingPlanController objTPC = new TeachingPlanController();
        int session = Convert.ToInt32(ddlSessionGlobal.SelectedValue);
        int ua_no = Convert.ToInt32(Session["userno"]);
        int courseno = Convert.ToInt32(ViewState["globalcourseno"].ToString());
        int sectionno = Convert.ToInt32(ddlGlobalSection.SelectedValue);

        //added buy sumit on 07022020
        //string DEGREENO = objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_COURSE WHERE COURSENO = " + Convert.ToInt32(ViewState["courseno"].ToString()) + ")");


        DataSet ds = objTPC.GetSlotGlobalElective(Convert.ToInt32(ddlSessionGlobal.SelectedValue), ua_no, courseno, DAYNO, startdate, enddate, sectionno);

        ddllist.Items.Clear();
        ddllist.Items.Add("Please Select");
        ddllist.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddllist.DataSource = ds;
            ddllist.DataValueField = ds.Tables[0].Columns[1].ToString();
            ddllist.DataTextField = ds.Tables[0].Columns[0].ToString();
            ddllist.DataBind();
            ddllist.SelectedIndex = 0;
        }
        ddllist.Enabled = true;
    }

    private void BindTeachingPlanGlobalElective()
    {
        try
        {
            DataSet ds = new DataSet();
            int ua_no = ua_no = Convert.ToInt32(Session["userno"]);

            ds = objTeachingPlanController.GetAllTEACHING_PLANGlobalElective(ua_no, Convert.ToInt32(ViewState["globalcourseno"].ToString()), Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(ddlGlobalSection.SelectedValue));

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvTeachingPlanGlobalElective.Visible = true;
                    lvTeachingPlanGlobalElective.DataSource = ds.Tables[0];
                    lvTeachingPlanGlobalElective.DataBind();
                }
                else
                {
                    lvTeachingPlanGlobalElective.Visible = false;
                    lvTeachingPlanGlobalElective.DataSource = null;
                    lvTeachingPlanGlobalElective.DataBind();
                }
            }
            else
            {
                lvTeachingPlanGlobalElective.Visible = false;
                lvTeachingPlanGlobalElective.DataSource = null;
                lvTeachingPlanGlobalElective.DataBind();
            }

        }
        catch
        {
            //  throw;
        }
    }

    private void BindTopiccodeUnitGlobalElective()
    {
        ddlLectureNoGlobal.Items.Clear();
        ddlUnitNoGlobal.Items.Clear();
        ddlLectureNoGlobal.Items.Insert(0, new ListItem("Please Select", "0"));

        ddlUnitNoGlobal.Items.Add(new ListItem("Please Select", "0"));
        for (int i = 1; i <= 30; i++)
        {
            ddlUnitNoGlobal.Items.Add(new ListItem(i.ToString()));
        }
    }

    protected void ddlUnitNoGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUnitNoGlobal.SelectedIndex > 0)
            {
                ddlLectureNoGlobal.Items.Clear();
                ddlLectureNoGlobal.Items.Insert(0, new ListItem("Please Select", "0"));


                for (int i = 1; i <= 80; i++)
                {
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_TEACHINGPLAN", "COUNT(*)", "UA_NO =" + Session["userno"] + " AND COURSENO =" + Convert.ToInt32(ViewState["globalcourseno"].ToString()) + " AND UNIT_NO=" + Convert.ToInt32(ddlUnitNoGlobal.SelectedValue) + " AND LECTURE_NO=" + Convert.ToInt32(i) + " AND TUTORIAL = 0  AND ISNULL(CANCEL,0)=0 AND SECTIONNO="+ Convert.ToInt32(ddlGlobalSection.SelectedValue) +" AND SESSIONNO IN( SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSessionGlobal.SelectedValue) + ")"));
                    if (count == 0)
                    {
                        ddlLectureNoGlobal.Items.Add(new ListItem(i.ToString()));
                    }
                }

            }
            else
            {
                ddlLectureNoGlobal.Items.Clear();
                ddlLectureNoGlobal.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlMonGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTuesGlobal.Enabled == true)
            ddlTuesGlobal.SelectedIndex = 0;
        if (ddlWedGlobal.Enabled == true)
            ddlWedGlobal.SelectedIndex = 0;
        if (ddlThursGlobal.Enabled == true)
            ddlThursGlobal.SelectedIndex = 0;
        if (ddlFriGlobal.Enabled == true)
            ddlFriGlobal.SelectedIndex = 0;
        if (ddlSatGlobal.Enabled == true)
            ddlSatGlobal.SelectedIndex = 0;

        if (ddlMonGlobal.SelectedIndex > 0)
        {
            ddlMonSlotGlobal.Enabled = true;
            ddlTueSlotGlobal.Enabled = false;
            if (ddlTueSlotGlobal.SelectedIndex > 0)
                ddlTueSlotGlobal.SelectedIndex = 0;
            ddlWedSlotGlobal.Enabled = false;
            if (ddlWedSlotGlobal.SelectedIndex > 0)
                ddlWedSlotGlobal.SelectedIndex = 0;
            ddlThusSlotGlobal.Enabled = false;
            if (ddlThusSlotGlobal.SelectedIndex > 0)
                ddlThusSlotGlobal.SelectedIndex = 0;
            ddlFriSlotGlobal.Enabled = false;
            if (ddlFriSlotGlobal.SelectedIndex > 0)
                ddlFriSlotGlobal.SelectedIndex = 0;
            ddlSatSlotGlobal.Enabled = false;
            if (ddlSatSlotGlobal.SelectedIndex > 0)
                ddlSatSlotGlobal.SelectedIndex = 0;
        }
        BindTeachingPlanGlobalElective();
    }
    protected void ddlTuesGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonGlobal.Enabled == true)
            ddlMonGlobal.SelectedIndex = 0;
        if (ddlWedGlobal.Enabled == true)
            ddlWedGlobal.SelectedIndex = 0;
        if (ddlThursGlobal.Enabled == true)
            ddlThursGlobal.SelectedIndex = 0;
        if (ddlFriGlobal.Enabled == true)
            ddlFriGlobal.SelectedIndex = 0;
        if (ddlSatGlobal.Enabled == true)
            ddlSatGlobal.SelectedIndex = 0;

        if (ddlTuesGlobal.SelectedIndex > 0)
        {
            ddlMonSlotGlobal.Enabled = false;
            if (ddlMonSlotGlobal.SelectedIndex > 0)
                ddlMonSlotGlobal.SelectedIndex = 0;
            ddlTueSlotGlobal.Enabled = true;
            ddlWedSlotGlobal.Enabled = false;
            if (ddlWedSlotGlobal.SelectedIndex > 0)
                ddlWedSlotGlobal.SelectedIndex = 0;
            ddlThusSlotGlobal.Enabled = false;
            if (ddlThusSlotGlobal.SelectedIndex > 0)
                ddlThusSlotGlobal.SelectedIndex = 0;
            ddlFriSlotGlobal.Enabled = false;
            if (ddlFriSlotGlobal.SelectedIndex > 0)
                ddlFriSlotGlobal.SelectedIndex = 0;
            ddlSatSlotGlobal.Enabled = false;
            if (ddlSatSlotGlobal.SelectedIndex > 0)
                ddlSatSlotGlobal.SelectedIndex = 0;
        }
        BindTeachingPlanGlobalElective();

    }
    protected void ddlWedGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonGlobal.Enabled == true)
            ddlMonGlobal.SelectedIndex = 0;
        if (ddlTuesGlobal.Enabled == true)
            ddlTuesGlobal.SelectedIndex = 0;
        if (ddlThursGlobal.Enabled == true)
            ddlThursGlobal.SelectedIndex = 0;
        if (ddlFriGlobal.Enabled == true)
            ddlFriGlobal.SelectedIndex = 0;
        if (ddlSatGlobal.Enabled == true)
            ddlSatGlobal.SelectedIndex = 0;

        if (ddlWedGlobal.SelectedIndex > 0)
        {
            ddlMonSlotGlobal.Enabled = false;
            if (ddlMonSlotGlobal.SelectedIndex > 0)
                ddlMonSlotGlobal.SelectedIndex = 0;
            ddlTueSlotGlobal.Enabled = false;
            if (ddlTueSlotGlobal.SelectedIndex > 0)
                ddlTueSlotGlobal.SelectedIndex = 0;
            ddlWedSlotGlobal.Enabled = true;

            ddlThusSlotGlobal.Enabled = false;
            if (ddlThusSlotGlobal.SelectedIndex > 0)
                ddlThusSlotGlobal.SelectedIndex = 0;
            ddlFriSlotGlobal.Enabled = false;
            if (ddlFriSlotGlobal.SelectedIndex > 0)
                ddlFriSlotGlobal.SelectedIndex = 0;
            ddlSatSlotGlobal.Enabled = false;
            if (ddlSatSlotGlobal.SelectedIndex > 0)
                ddlSatSlotGlobal.SelectedIndex = 0;
        }
        BindTeachingPlanGlobalElective();

    }
    protected void ddlThursGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonGlobal.Enabled == true)
            ddlMonGlobal.SelectedIndex = 0;
        if (ddlTuesGlobal.Enabled == true)
            ddlTuesGlobal.SelectedIndex = 0;
        if (ddlWedGlobal.Enabled == true)
            ddlWedGlobal.SelectedIndex = 0;
        if (ddlFriGlobal.Enabled == true)
            ddlFriGlobal.SelectedIndex = 0;
        if (ddlSatGlobal.Enabled == true)
            ddlSatGlobal.SelectedIndex = 0;

        if (ddlThursGlobal.SelectedIndex > 0)
        {
            ddlMonSlotGlobal.Enabled = false;
            ddlTueSlotGlobal.Enabled = false;
            ddlWedSlotGlobal.Enabled = false;
            ddlThusSlotGlobal.Enabled = true;
            ddlFriSlotGlobal.Enabled = false;
            ddlSatSlotGlobal.Enabled = false;

            if (ddlMonSlotGlobal.SelectedIndex > 0)
                ddlMonSlotGlobal.SelectedIndex = 0;
            if (ddlTueSlotGlobal.SelectedIndex > 0)
                ddlTueSlotGlobal.SelectedIndex = 0;
            if (ddlWedSlotGlobal.SelectedIndex > 0)
                ddlWedSlotGlobal.SelectedIndex = 0;
            if (ddlFriSlotGlobal.SelectedIndex > 0)
                ddlFriSlotGlobal.SelectedIndex = 0;
            if (ddlSatSlotGlobal.SelectedIndex > 0)
                ddlSatSlotGlobal.SelectedIndex = 0;
        }
        BindTeachingPlanGlobalElective();

    }
    protected void ddlFriGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonGlobal.Enabled == true)
            ddlMonGlobal.SelectedIndex = 0;
        if (ddlTuesGlobal.Enabled == true)
            ddlTuesGlobal.SelectedIndex = 0;
        if (ddlWedGlobal.Enabled == true)
            ddlWedGlobal.SelectedIndex = 0;
        if (ddlThursGlobal.Enabled == true)
            ddlThursGlobal.SelectedIndex = 0;
        if (ddlSatGlobal.Enabled == true)
            ddlSatGlobal.SelectedIndex = 0;

        if (ddlFriGlobal.SelectedIndex > 0)
        {
            ddlMonSlotGlobal.Enabled = false;
            if (ddlMonSlotGlobal.SelectedIndex > 0)
                ddlMonSlotGlobal.SelectedIndex = 0;
            ddlTueSlotGlobal.Enabled = false;
            if (ddlTueSlotGlobal.SelectedIndex > 0)
                ddlTueSlotGlobal.SelectedIndex = 0;
            ddlWedSlotGlobal.Enabled = false;
            if (ddlWedSlotGlobal.SelectedIndex > 0)
                ddlWedSlotGlobal.SelectedIndex = 0;
            ddlThusSlotGlobal.Enabled = false;
            if (ddlThusSlotGlobal.SelectedIndex > 0)
                ddlThusSlotGlobal.SelectedIndex = 0;
            ddlFriSlotGlobal.Enabled = true;
            ddlSatSlotGlobal.Enabled = false;
            if (ddlSatSlotGlobal.SelectedIndex > 0)
                ddlSatSlotGlobal.SelectedIndex = 0;
        }
        BindTeachingPlanGlobalElective();

    }
    protected void ddlSatGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonGlobal.Enabled == true)
            ddlMonGlobal.SelectedIndex = 0;
        if (ddlTuesGlobal.Enabled == true)
            ddlTuesGlobal.SelectedIndex = 0;
        if (ddlWedGlobal.Enabled == true)
            ddlWedGlobal.SelectedIndex = 0;
        if (ddlThursGlobal.Enabled == true)
            ddlThursGlobal.SelectedIndex = 0;
        if (ddlFriGlobal.Enabled == true)
            ddlFriGlobal.SelectedIndex = 0;

        if (ddlSatGlobal.SelectedIndex > 0)
        {
            ddlMonSlotGlobal.Enabled = false;
            if (ddlMonSlotGlobal.SelectedIndex > 0)
                ddlMonSlotGlobal.SelectedIndex = 0;
            ddlTueSlotGlobal.Enabled = false;
            if (ddlTueSlotGlobal.SelectedIndex > 0)
                ddlTueSlotGlobal.SelectedIndex = 0;
            ddlWedSlotGlobal.Enabled = false;
            if (ddlWedSlotGlobal.SelectedIndex > 0)
                ddlWedSlotGlobal.SelectedIndex = 0;
            ddlThusSlotGlobal.Enabled = false;
            if (ddlThusSlotGlobal.SelectedIndex > 0)
                ddlThusSlotGlobal.SelectedIndex = 0;
            ddlFriSlotGlobal.Enabled = false;
            if (ddlFriSlotGlobal.SelectedIndex > 0)
                ddlFriSlotGlobal.SelectedIndex = 0;
            ddlSatSlotGlobal.Enabled = true;
        }
        BindTeachingPlanGlobalElective();

    }
    protected void ddlMonSlotGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlTueSlotGlobal.Enabled == true)
            ddlTueSlotGlobal.SelectedIndex = 0;
        if (ddlWedSlotGlobal.Enabled == true)
            ddlWedSlotGlobal.SelectedIndex = 0;
        if (ddlThusSlotGlobal.Enabled == true)
            ddlThusSlotGlobal.SelectedIndex = 0;
        if (ddlFriSlotGlobal.Enabled == true)
            ddlFriSlotGlobal.SelectedIndex = 0;
        if (ddlSatSlotGlobal.Enabled == true)
            ddlSatSlotGlobal.SelectedIndex = 0;


    }
    protected void ddlTueSlotGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonSlotGlobal.Enabled == true)
            ddlMonSlotGlobal.SelectedIndex = 0;
        if (ddlWedSlotGlobal.Enabled == true)
            ddlWedSlotGlobal.SelectedIndex = 0;
        if (ddlThusSlotGlobal.Enabled == true)
            ddlThusSlotGlobal.SelectedIndex = 0;
        if (ddlFriSlotGlobal.Enabled == true)
            ddlFriSlotGlobal.SelectedIndex = 0;
        if (ddlSatSlotGlobal.Enabled == true)
            ddlSatSlotGlobal.SelectedIndex = 0;
    }
    protected void ddlWedSlotGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonSlotGlobal.Enabled == true)
            ddlMonSlotGlobal.SelectedIndex = 0;
        if (ddlTueSlotGlobal.Enabled == true)
            ddlTueSlotGlobal.SelectedIndex = 0;
        if (ddlThusSlotGlobal.Enabled == true)
            ddlThusSlotGlobal.SelectedIndex = 0;
        if (ddlFriSlotGlobal.Enabled == true)
            ddlFriSlotGlobal.SelectedIndex = 0;
        if (ddlSatSlotGlobal.Enabled == true)
            ddlSatSlotGlobal.SelectedIndex = 0;

    }
    protected void ddlThusSlotGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonSlotGlobal.Enabled == true)
            ddlMonSlotGlobal.SelectedIndex = 0;
        if (ddlTueSlotGlobal.Enabled == true)
            ddlTueSlotGlobal.SelectedIndex = 0;
        if (ddlWedSlotGlobal.Enabled == true)
            ddlWedSlotGlobal.SelectedIndex = 0;
        if (ddlFriSlotGlobal.Enabled == true)
            ddlFriSlotGlobal.SelectedIndex = 0;
        if (ddlSatSlotGlobal.Enabled == true)
            ddlSatSlotGlobal.SelectedIndex = 0;

    }
    protected void ddlFriSlotGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonSlotGlobal.Enabled == true)
            ddlMonSlotGlobal.SelectedIndex = 0;
        if (ddlTueSlotGlobal.Enabled == true)
            ddlTueSlotGlobal.SelectedIndex = 0;
        if (ddlWedSlotGlobal.Enabled == true)
            ddlWedSlotGlobal.SelectedIndex = 0;
        if (ddlThusSlotGlobal.Enabled == true)
            ddlThusSlotGlobal.SelectedIndex = 0;
        if (ddlSatSlotGlobal.Enabled == true)
            ddlSatSlotGlobal.SelectedIndex = 0;

    }
    protected void ddlSatSlotGlobal_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMonSlotGlobal.Enabled == true)
            ddlMonSlotGlobal.SelectedIndex = 0;
        if (ddlTueSlotGlobal.Enabled == true)
            ddlTueSlotGlobal.SelectedIndex = 0;
        if (ddlWedSlotGlobal.Enabled == true)
            ddlWedSlotGlobal.SelectedIndex = 0;
        if (ddlThusSlotGlobal.Enabled == true)
            ddlThusSlotGlobal.SelectedIndex = 0;
        if (ddlFriSlotGlobal.Enabled == true)
            ddlFriSlotGlobal.SelectedIndex = 0;

    }

    protected void btnSubmitGlobal_Click(object sender, EventArgs e)
    {
        try
        {
            Exam objExam = new Exam();

            objExam.SessionNo = Convert.ToInt32(ddlSessionGlobal.SelectedValue);
            objExam.Ua_No = Convert.ToInt32(Session["userno"]);
            objExam.Sectionno = Convert.ToInt32(ddlGlobalSection.SelectedValue);

            int Counter = CheckDropdownSelectGlobalElective();
            int counterSlot = this.CheckSlotSelectGlobalElective();
            if (Counter == 0 && counterSlot == 0)
            {
                objCommon.DisplayMessage(this.updGlobal, "Please Select Date and Slot!", this.Page);
            }
            else if (Counter == 0)
            {
                objCommon.DisplayMessage(this.updGlobal, "Please Select Date!", this.Page);
            }
            else if (counterSlot == 0)
            {
                objCommon.DisplayMessage(this.updGlobal, "Please Select Slot!", this.Page);
            }
            else
            {
                if (ddlMonGlobal.SelectedIndex > 0)
                    objExam.Date = Convert.ToDateTime(ddlMonGlobal.SelectedValue);
                else if (ddlTuesGlobal.SelectedIndex > 0)
                    objExam.Date = Convert.ToDateTime(ddlTuesGlobal.SelectedValue);
                else if (ddlWedGlobal.SelectedIndex > 0)
                    objExam.Date = Convert.ToDateTime(ddlWedGlobal.SelectedValue);
                else if (ddlThursGlobal.SelectedIndex > 0)
                    objExam.Date = Convert.ToDateTime(ddlThursGlobal.SelectedValue);
                else if (ddlFriGlobal.SelectedIndex > 0)
                    objExam.Date = Convert.ToDateTime(ddlFriGlobal.SelectedValue);
                else if (ddlSatGlobal.SelectedIndex > 0)
                    objExam.Date = Convert.ToDateTime(ddlSatGlobal.SelectedValue);

                if (ddlMonSlotGlobal.SelectedIndex > 0)
                    objExam.Slot = Convert.ToInt32(ddlMonSlotGlobal.SelectedValue);
                else if (ddlTueSlotGlobal.SelectedIndex > 0)
                    objExam.Slot = Convert.ToInt32(ddlTueSlotGlobal.SelectedValue);
                else if (ddlWedSlotGlobal.SelectedIndex > 0)
                    objExam.Slot = Convert.ToInt32(ddlWedSlotGlobal.SelectedValue);
                else if (ddlThusSlotGlobal.SelectedIndex > 0)
                    objExam.Slot = Convert.ToInt32(ddlThusSlotGlobal.SelectedValue);
                else if (ddlFriSlotGlobal.SelectedIndex > 0)
                    objExam.Slot = Convert.ToInt32(ddlFriSlotGlobal.SelectedValue);
                else if (ddlSatSlotGlobal.SelectedIndex > 0)
                    objExam.Slot = Convert.ToInt32(ddlSatSlotGlobal.SelectedValue);

                objExam.Lecture_No = Convert.ToInt32(ddlLectureNoGlobal.SelectedValue);
                objExam.Courseno = Convert.ToInt32(ViewState["globalcourseno"].ToString());
                //if (ViewState["semesterno"] == null)
                //{
                //    objExam.SemesterNo = 0;
                //}
                //else
                //{
                //    objExam.SemesterNo = Convert.ToInt32(ViewState["semesterno"].ToString());
                //}


                //objExam.SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());

                objExam.Topic_Covered = Convert.ToString(txtLectureTopicGlobal.Text);
                objExam.UnitNo = Convert.ToInt32(ddlUnitNoGlobal.SelectedValue);
                objExam.collegeid = Convert.ToInt32(ViewState["college_id"].ToString());//Added by Dileep on 12.04.2021

                int count = 0;
                int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
                // if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 100)

                if (ViewState["GlobalTP_NO"] == null || ViewState["GlobalTP_NO"] == "0")
                {
                    if (CheckDuplicateEntryGlobalElective() == true)
                    {
                        objCommon.DisplayMessage(this.updGlobal, "Entry for this Unit No. and Topic Code Already Done!", this.Page);
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objTeachingPlanController.AddTeachingPlanGlobalElectiveModified(objExam, OrgId);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updGlobal, "Teaching Plan Saved Successfully!", this.Page);
                    }
                    ddlSessionGlobal.Enabled = false;
                    ddlSessionGlobal.Enabled = false;

                    this.ResetDateDropdownGlobalElective();

                    this.BindTopiccodeUnitGlobalElective();
                }
                else
                {
                    objExam.TP_NO = Convert.ToInt32(ViewState["GlobalTP_NO"]);
                    if (CheckDuplicateEntryEditGlobalElective() == true)
                    {
                        objCommon.DisplayMessage(this.updGlobal, "Entry for this Unit No. and Topic Code Already Done!", this.Page);
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objTeachingPlanController.UpdateTeachingPlanGlobalElectiveModified(objExam);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updGlobal, "Teaching Plan Updated Successfully!", this.Page);
                    }
                    ddlSessionGlobal.Enabled = false;
                    ddlSessionGlobal.Enabled = false;

                    this.ResetDateDropdownGlobalElective();

                    this.BindTopiccodeUnitGlobalElective();

                    objCommon.DisplayMessage(this.updGlobal, "Teaching plan cannot be edited, Please contact administrator", this.Page);
                }
                //}

            }

            this.BindTeachingPlanGlobalElective();
            this.EnableControlsGlobalElective(true);
            this.ClearGlobalElective();
        }
        catch
        {
            throw;
        }
    }
    protected void btnReportGlobal_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancelGlobal_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private int CheckDropdownSelectGlobalElective()
    {
        int count = 0;
        if (ddlMonGlobal.Enabled == true && ddlMonGlobal.SelectedIndex > 0)
            count++;
        if (ddlTuesGlobal.Enabled == true && ddlTuesGlobal.SelectedIndex > 0)
            count++;
        if (ddlWedGlobal.Enabled == true && ddlWedGlobal.SelectedIndex > 0)
            count++;
        if (ddlThursGlobal.Enabled == true && ddlThursGlobal.SelectedIndex > 0)
            count++;
        if (ddlFriGlobal.Enabled == true && ddlFriGlobal.SelectedIndex > 0)
            count++;
        if (ddlSatGlobal.Enabled == true && ddlSatGlobal.SelectedIndex > 0)
            count++;
        return count;
    }

    private int CheckSlotSelectGlobalElective()
    {
        int count = 0;
        if (ddlMonSlotGlobal.Enabled == true && ddlMonSlotGlobal.SelectedIndex > 0)
            count++;
        if (ddlTueSlotGlobal.Enabled == true && ddlTueSlotGlobal.SelectedIndex > 0)
            count++;
        if (ddlWedSlotGlobal.Enabled == true && ddlWedSlotGlobal.SelectedIndex > 0)
            count++;
        if (ddlThusSlotGlobal.Enabled == true && ddlThusSlotGlobal.SelectedIndex > 0)
            count++;
        if (ddlFriSlotGlobal.Enabled == true && ddlFriSlotGlobal.SelectedIndex > 0)
            count++;
        if (ddlSatSlotGlobal.Enabled == true && ddlSatSlotGlobal.SelectedIndex > 0)
            count++;
        return count;
    }

    private void ResetDateDropdownGlobalElective()
    {

        if (ddlMonGlobal.Enabled == false)
            ddlMonGlobal.SelectedIndex = 0;
        if (ddlTuesGlobal.Enabled == false)
            ddlTuesGlobal.SelectedIndex = 0;
        if (ddlWedGlobal.Enabled == false)
            ddlWedGlobal.SelectedIndex = 0;
        if (ddlThursGlobal.Enabled == false)
            ddlThursGlobal.SelectedIndex = 0;
        if (ddlFriGlobal.Enabled == false)
            ddlFriGlobal.SelectedIndex = 0;
        if (ddlSatGlobal.Enabled == false)
            ddlSatGlobal.SelectedIndex = 0;

        if (ddlMonSlotGlobal.Enabled == false)
            ddlMonSlotGlobal.SelectedIndex = 0;
        if (ddlTueSlotGlobal.Enabled == false)
            ddlTueSlotGlobal.SelectedIndex = 0;
        if (ddlWedSlotGlobal.Enabled == false)
            ddlWedSlotGlobal.SelectedIndex = 0;
        if (ddlThusSlotGlobal.Enabled == false)
            ddlThusSlotGlobal.SelectedIndex = 0;
        if (ddlFriSlotGlobal.Enabled == false)
            ddlFriSlotGlobal.SelectedIndex = 0;
        if (ddlSatSlotGlobal.Enabled == false)
            ddlSatSlotGlobal.SelectedIndex = 0;


    }
    private void EnableControlsGlobalElective(bool flag)
    {
        ddlSessionGlobal.Enabled = flag;
        ddlSemesterGlobal.Enabled = flag;

    }

    private void ClearGlobalElective()
    {
        txtLectureTopicGlobal.Text = string.Empty;
        ViewState["GlobalTP_NO"] = null;
    }

    private bool CheckDuplicateEntryGlobalElective()
    {
        bool flag = false;
        try
        {
            string TP_NO = string.Empty;

            TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN TP INNER JOIN ACD_SESSION_MASTER SM ON(TP.SESSIONNO=SM.SESSIONNO AND TP.COLLEGE_ID= SM.COLLEGE_ID)", "TP_NO", "TP.COURSENO=" + Convert.ToInt32(ViewState["globalcourseno"].ToString()) + " AND SM.SESSIONID=" + Convert.ToInt32(ddlSessionGlobal.SelectedValue) + " AND UNIT_NO=" + ddlUnitNoGlobal.SelectedValue + " AND LECTURE_NO=" + ddlLectureNoGlobal.SelectedValue + " AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + " AND SECTIONNO=" + Convert.ToInt32(ddlGlobalSection.SelectedValue)+ " AND (TUTORIAL = 0 or TUTORIAL IS NULL)");
            if (TP_NO != null && TP_NO != string.Empty)
            {
                flag = true;
            }
        }
        catch
        {
            throw;
        }
        return flag;
    }

    private bool CheckDuplicateEntryEditGlobalElective()
    {
        bool flag = false;
        try
        {
            string TP_NO = string.Empty;
            TP_NO = objCommon.LookUp("ACD_TEACHINGPLAN TP INNER JOIN ACD_SESSION_MASTER SM ON(TP.SESSIONNO=SM.SESSIONNO AND TP.COLLEGE_ID= SM.COLLEGE_ID)", "TP_NO", "TP.COURSENO=" + Convert.ToInt32(ViewState["globalcourseno"].ToString()) + " AND SM.SESSIONID=" + Convert.ToInt32(ddlSessionGlobal.SelectedValue) + " AND UNIT_NO=" + ddlUnitNoGlobal.SelectedValue + " AND LECTURE_NO=" + ddlLectureNoGlobal.SelectedValue + " AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + " AND TP_NO != " + ViewState["GlobalTP_NO"] + " AND SECTIONNO=" + Convert.ToInt32(ddlGlobalSection.SelectedValue) + " AND (TUTORIAL = 0 or TUTORIAL IS NULL)");

            if (TP_NO != null && TP_NO != string.Empty)
            {
                flag = true;
            }
        }
        catch
        {
            throw;
        }
        return flag;
    }
    #endregion



    protected void ddlGlobalSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtLectureTopicGlobal.Text = "";
        lvTeachingPlanGlobalElective.DataSource = null;
        lvTeachingPlanGlobalElective.DataBind();
        if (ddlSemesterGlobal.SelectedIndex > 0)
        {

            string MSG = ddlSemesterGlobal.SelectedValue.ToString();// Request.Form["msg"].ToString();
            string[] repoarray;
            repoarray = MSG.Split('-');
            string SUBID = repoarray[0].ToString();
            string courseno = repoarray[1].ToString();
            ViewState["globalSUBID"] = SUBID;
            ViewState["globalcourseno"] = courseno;
            objCommon.FillDropDownList(ddlTimeTableDateGlobal, "ACD_TIME_TABLE_CONFIG TT INNER JOIN ACD_COURSE_TEACHER CT ON (CT.CT_NO=TT.CTNO)", "DISTINCT CONCAT(CAST(START_DATE AS DATE),'-',CAST(END_DATE AS DATE)) AS TT_DATE", "CAST(CONVERT(VARCHAR(10) ,START_DATE,103) AS NVARCHAR(15)) + ' - '+ CAST(CONVERT(VARCHAR(10),END_DATE ,103) AS NVARCHAR(15))", "CT.COURSENO=" + Convert.ToInt32(courseno) + " AND SUBID=" + Convert.ToInt32(SUBID) + " AND CT.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID =" + Convert.ToInt32(ddlSessionGlobal.SelectedValue) + ") AND ISNULL(TT.CANCEL,0)=0 AND ISNULL(CT.CANCEL,0)=0 AND (CT.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR CT.ADTEACHER =" + Convert.ToInt32(Session["userno"]) + ") AND CT.SECTIONNO =" + Convert.ToInt32(ddlGlobalSection.SelectedValue), "TT_DATE");
            ddlTimeTableDateGlobal.Focus();
            //decimal tutorial = Convert.ToDecimal(objCommon.LookUp("ACD_COURSE", "THEORY", "COURSENO=" + Convert.ToInt32(courseno)));
            //if (tutorial > 0)
            //{
            //    dvTutorial.Visible = true;
            //}
            //else
            //{
            //    dvTutorial.Visible = false;
            //}
        }
        else
        {
            ddlTimeTableDateGlobal.Items.Clear();
            ddlTimeTableDateGlobal.Items.Add(new ListItem("Please Select", "0"));
            ddlUnitNoGlobal.Items.Clear();
            ddlUnitNoGlobal.Items.Add(new ListItem("Please Select", "0"));
            ddlLectureNoGlobal.Items.Clear();
            ddlLectureNoGlobal.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}