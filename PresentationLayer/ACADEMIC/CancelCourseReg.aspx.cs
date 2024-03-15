using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using mastersofterp_MAKAUAT;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using BusinessLogicLayer.BusinessLogic.Academic;
using System.IO;
public partial class ACADEMIC_CancelCourseReg : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GradeController objG = new GradeController();
    CourseController objCourse = new CourseController();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                string deptnos = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();
                //objCommon.FillDropDownList(ddlCollegeSingle, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
                AcademinDashboardController objADEController = new AcademinDashboardController();
                DataSet ds = objADEController.Get_College_Session(2, Session["college_nos"].ToString());
                ddlSessionSingle.Items.Clear();
                ddlSessionSingle.Items.Add("Please Select");
                ddlSessionSingle.SelectedItem.Value = "0";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSessionSingle.DataSource = ds;
                    ddlSessionSingle.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlSessionSingle.DataTextField = ds.Tables[0].Columns[4].ToString();
                    ddlSessionSingle.DataBind();
                    ddlSessionSingle.SelectedIndex = 0;
                }

                if (Session["usertype"].ToString() != "1")
                {
                    if (Session["usertype"].ToString() == "3")
                    {
                        divdepartment.Visible = false;
                        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_COURSE_TEACHER CR ON (SM.SCHEMENO=CR.SCHEMENO )", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0') AND (CR.UA_NO =" + Convert.ToInt32(Session["userno"]) + ")", "");
                    }
                    else
                    {
                        divdepartment.Visible = true;
                        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
                    }
                }
                else
                {
                    divdepartment.Visible = true;
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
            }

        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CancelCourseReg.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CancelCourseReg.aspx");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
            {
                if (Session["usertype"].ToString() == "3")
                {
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "AND (SR.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR SR.UA_NO_PRAC=" + Convert.ToInt32(Session["userno"]) + ")", "SR.SEMESTERNO");
                }
                else
                {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND B.DEPTNO IN(" + Session["userdeptno"].ToString() + ") AND B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "B.DEPTNO");
                }
            }
            else
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND B.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "B.DEPTNO");
            }
            ddlDepartment.Focus();
        }
        else
        {
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
        }
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            ddlDegree.Focus();
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "B.DEPTNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue, "B.DEPTNO");
            }
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlCollege.SelectedIndex = 0;
        }
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Focus();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            if (Session["usertype"].ToString() != "1")
            {
                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");

                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND (B.DEPTNO IN(" + Session["userdeptno"].ToString() + ") OR (" + Session["userdeptno"].ToString() + ") = '0') AND B.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "A.LONGNAME");
                ddlBranch.Focus();
                //ddlAdmbatch.SelectedIndex = 0;
            }
            else
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
                ddlBranch.Focus();
            }
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

        }
        else
        {
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
        }
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");

        }
        else
        {
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
        }
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSemester.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");

        }
        else
        {
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
        }
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            ddlSubject.Focus();
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            if (Session["usertype"].ToString() == "3")
            {
                objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "Case When ISNULL(GLOBALELE,0)=1 then (SR.CCODE + ' - ' + SR.COURSENAME +' [Global]') Else (SR.CCODE + ' - ' + SR.COURSENAME) End as COURSENAME", "SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND REGISTERED=1 AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND (SR.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR SR.UA_NO_PRAC=" + Convert.ToInt32(Session["userno"]) + ")", "");
            }
            else 
            {
                objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "Case When ISNULL(GLOBALELE,0)=1 then (SR.CCODE + ' - ' + SR.COURSENAME +' [Global]') Else (SR.CCODE + ' - ' + SR.COURSENAME) End as COURSENAME", "SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND REGISTERED=1 AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "");
            }
        }
        else
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
        }
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //DataSet ds = objG.GetStudentsList_For_I_Grade(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue));
            DataSet ds = objCourse.GetStudentsList_For_Cancel_Course_Reg(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlStudents.Visible = true;
                lvStudents.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                hdncount.Value = ds.Tables[0].Rows.Count.ToString();
                foreach (ListViewDataItem item in lvStudents.Items)
                {
                    CheckBox chek = item.FindControl("chkSelect") as CheckBox;
                    Label lbl = item.FindControl("lblStatus") as Label;
                    HiddenField hdnCancel = item.FindControl("hdnCancel") as HiddenField;
                    if (hdnCancel.Value.Equals("1"))
                    {
                        chek.Checked = true;
                        lbl.Text = "Done".ToString();
                        lbl.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl.Text = "Not Done".ToString();
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }
                btnSubmit.Enabled = true;
            }
            else
            {
                objCommon.DisplayMessage(updCourse, "Data Not Found", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        ddlSubject.Items.Clear();
        ddlSubject.Items.Add(new ListItem("Please Select", "0"));
        txtRemark_Multiple.Text = string.Empty;
        pnlStudents.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
    }
    protected void ClearListView()
    {
        pnlStudents.Visible = false;
        lvStudents.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int checkCount = 0;
            int updCount = 0;
            int Idno = 0;
            string Remark = string.Empty;
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (ListViewDataItem lv in lvStudents.Items)
            {
                CheckBox chek = lv.FindControl("chkSelect") as CheckBox;
                //TextBox txt = lv.FindControl("txtGrade") as TextBox;
                if (chek.Checked)
                {
                    //if (txt.Text.Equals(string.Empty))
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Please Enter Grade For Selected Students", this.Page);
                    //    return;
                    //}
                    Idno = Convert.ToInt32(chek.ToolTip);
                    Remark = txtRemark_Multiple.Text.Trim();
                    checkCount++;
                    CustomStatus cs = (CustomStatus)objCourse.UpdateStudents_Cancel_Course(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue), Idno, Convert.ToInt32(Session["userno"]), ipAddress, Remark);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        updCount++;
                    }
                }
            }
            if (checkCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student From List", this.Page);
                return;
            }
            else if (checkCount == updCount)
            {
                objCommon.DisplayMessage(this.Page, "Student Course Cancelled Successfully", this.Page);
                ClearListView();
                btnSubmit.Enabled = false;
                txtRemark_Multiple.Text = string.Empty;
                ClearFields();
                return;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void rdoMultiple_CheckedChanged(object sender, EventArgs e)
    {
        divMultiple.Visible = true;
        divSingle.Visible = false;
        ClearListView();
        divSubjectsList.Visible = false;
        ddlClgname.Focus();
    }
    protected void ddlSessionSingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionSingle.SelectedIndex > 0)
        {
            ddlSemesterSingle.Focus();
            ddlSemesterSingle.Items.Clear();
            ddlSemesterSingle.Items.Add(new ListItem("Please Select", "0"));
            txtRegNo.Text = string.Empty;
            objCommon.FillDropDownList(ddlSemesterSingle, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSessionSingle.SelectedValue + " ", "SR.SEMESTERNO");
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
        divSubjectsList.Visible = false;
        btnSubmitSingle.Enabled = false;
        txtRemark_Single.Text = string.Empty;

    }
    protected void ddlSemesterSingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemesterSingle.SelectedIndex > 0)
        {
            txtRegNo.Text = string.Empty;
            txtRegNo.Focus();
        }
        divSubjectsList.Visible = false;
        btnSubmitSingle.Enabled = false;
        txtRemark_Single.Text = string.Empty;
    }
    protected void btnShowSingle_Click(object sender, EventArgs e)
    {
        try
        {
            ClearListView();
            int Idno = 0;
            string Reg = "'" + txtRegNo.Text.Trim() + "'";
            Idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "REGNO= " + Reg));
            //DataSet dsSubject = objG.GetSubjectsList_For_I_Grade_ByIdno(Convert.ToInt32(ddlSessionSingle.SelectedValue), Convert.ToInt32(ddlSemesterSingle.SelectedValue), Idno);
            DataSet dsSubject = objCourse.GetStudentsListToCancelCourse_ByIdno(Convert.ToInt32(ddlSessionSingle.SelectedValue), Convert.ToInt32(ddlSemesterSingle.SelectedValue), Idno);
            if (dsSubject.Tables[0].Rows.Count > 0)
            {
                divSubjectsList.Visible = true;
                pnlSubjects.Visible = true;
                lvSubjects.DataSource = dsSubject;
                lvSubjects.DataBind();
                hdncount.Value = dsSubject.Tables[0].Rows.Count.ToString();
                foreach (ListViewDataItem item in lvSubjects.Items)
                {
                    CheckBox chekSub = item.FindControl("chkSubject") as CheckBox;
                    Label lblStatus = item.FindControl("lblStatusSub") as Label;
                    HiddenField hdnCancel = item.FindControl("hdnCancel") as HiddenField;
                    if (hdnCancel.Value.Equals("1"))
                    {
                        chekSub.Checked = true;
                        lblStatus.Text = "Done".ToString();
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblStatus.Text = "Not Done".ToString();
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                btnSubmitSingle.Enabled = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void btnCancelSingle_Click(object sender, EventArgs e)
    {
        ClearFieldsSingle();
    }

    private void ClearFieldsSingle()
    {
        //ddlCollegeSingle.SelectedIndex = 0;
        //ddlSessionSingle.Items.Clear();
        //ddlSessionSingle.Items.Add(new ListItem("Please Select", "0"));
        ddlSessionSingle.SelectedIndex = 0;
        ddlSemesterSingle.Items.Clear();
        ddlSemesterSingle.Items.Add(new ListItem("Please Select", "0"));
        txtRegNo.Text = string.Empty;
        txtRemark_Single.Text = string.Empty;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        pnlStudents.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
    }

    protected void rdoSingle_CheckedChanged1(object sender, EventArgs e)
    {
        divMultiple.Visible = false;
        divSingle.Visible = true;
        ClearListView();
        //objCommon.FillDropDownList(ddlSessionSingle, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO >0 AND IS_ACTIVE=1", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSessionSingle, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1", "SESSION_NAME DESC");

        //ddlCollegeSingle.Focus();
        ddlSessionSingle.Focus();
        ddlSemesterSingle.SelectedIndex = 0;
        txtRegNo.Text = string.Empty;

    }

    protected void btnSubmitSingle_Click(object sender, EventArgs e)
    {
        try
        {
            int checkCount = 0;
            int updCount = 0;
            int Sub = 0;
            int CourseNo = 0;
            int SchemeNo = 0;
            string Remark = string.Empty;
            string RegNo = "'" + txtRegNo.Text.Trim() + "'";
            int Id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO= " + RegNo));
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (ListViewDataItem lv in lvSubjects.Items)
            {
                CheckBox checkSub = lv.FindControl("chkSubject") as CheckBox;
                Label lblCourse = lv.FindControl("lblCcode") as Label;
                Label lblScheme = lv.FindControl("lblCourseName") as Label;
                if (checkSub.Checked)
                {
                    CourseNo = Convert.ToInt32(lblCourse.ToolTip);
                    SchemeNo = Convert.ToInt32(lblScheme.ToolTip);
                    Sub = Convert.ToInt32(checkSub.ToolTip);
                    Remark = txtRemark_Single.Text.Trim();
                    checkCount++;
                    CustomStatus cs = (CustomStatus)objCourse.UpdateStudents_Cancel_Course(Convert.ToInt32(ddlSessionSingle.SelectedValue), SchemeNo, Convert.ToInt32(ddlSemesterSingle.SelectedValue), CourseNo, Id, Convert.ToInt32(Session["userno"]), ipAddress, Remark);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        updCount++;
                    }
                }
            }
            if (checkCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student From List", this.Page);
                return;
            }
            else if (checkCount == updCount)
            {
                objCommon.DisplayMessage(this.Page, "Student Course Cancelled Successfully", this.Page);
                divSubjectsList.Visible = false;
                txtRegNo.Text = string.Empty;
                btnSubmitSingle.Enabled = false;
                txtRemark_Single.Text = string.Empty;
                ClearFieldsSingle();
                return;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ClearSingleField()
    {
        ddlSessionSingle.SelectedIndex = 0;
        ddlSemesterSingle.SelectedIndex = 0;
        txtRegNo.Text = string.Empty;
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO =" + Convert.ToInt32(ddlDepartment.SelectedValue), "D.DEGREENO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
        }
        ClearListView();
        btnSubmit.Enabled = false;
        txtRemark_Multiple.Text = string.Empty;
    }
    protected void ddlCollegeSingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCollegeSingle.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSessionSingle, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollegeSingle.SelectedValue) + " AND M.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSION_NAME DESC");
        //    ddlSessionSingle.Focus();
        //}
        //else
        //{
        //    ddlSessionSingle.Items.Clear();
        //    ddlSessionSingle.Items.Add(new ListItem("Please Select", "0"));
        //    ddlSemesterSingle.Items.Clear();
        //    ddlSemesterSingle.Items.Add(new ListItem("Please Select", "0"));
        //}
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8")
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_PNAME", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND M.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "R.SESSIONNO DESC");
                }
                else if (Session["usertype"].ToString() == "3")
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_PNAME", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND M.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + "AND (R.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR R.UA_NO_PRAC=" + Convert.ToInt32(Session["userno"]) + ")", "R.SESSIONNO DESC");
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=CancelCourseReg.aspx");
                }
                ddlSession.Focus();
            }
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            ddlClgname.Focus();
        }
    }
}