using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_EXAMINATION_CancelExamReg : System.Web.UI.Page
{
    Common objCommon = new Common();
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
                // CheckPageAuthorization();
                string deptnos = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();
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
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");
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
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND M.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSION_NAME DESC");
                }
                else if (Session["usertype"].ToString() == "3")
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND M.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + "AND (R.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR R.UA_NO_PRAC=" + Convert.ToInt32(Session["userno"]) + ")", "SESSION_NAME DESC");
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
            string proc_param = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_SUBJECT,@P_IDNO,@P_UANO,@P_IPADDRESS,@P_REMARK,@P_OUTPUT";
            string proc_name = "PKG_SP_CANCEL_STUDENTS_EXAM";
            //string sp_call = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + ddlSemester.SelectedValue + "," + ddlSubject.SelectedValue + "," + Idno + "," + Session["userno"].ToString() + "," + ipAddress + "," + Remark + "," + "0";
            foreach (ListViewDataItem lv in lvStudents.Items)
            {
                CheckBox chek = lv.FindControl("chkSelect") as CheckBox;
                if (chek.Checked)
                {
                    Idno = Convert.ToInt32(chek.ToolTip);
                    Remark = txtRemark_Multiple.Text.Trim();
                    checkCount++;
                    string proc_call = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + ddlSemester.SelectedValue + "," + ddlSubject.SelectedValue + "," + Idno + "," + Session["userno"].ToString() + "," + ipAddress + "," + Remark + "," + "0";

                    string ret = objCommon.DynamicSPCall_IUD(proc_name, proc_param, proc_call, true);
                    CustomStatus cs = (CustomStatus)Convert.ToInt32(ret);
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
                objCommon.DisplayMessage(this.Page, "Student Exam Registration Cancelled Successfully", this.Page);
                ClearListView();
                btnSubmit.Enabled = false;
                txtRemark_Multiple.Text = string.Empty;
                ClearFields();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Failed to Cancelled Student Exam Registration  ", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
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
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_SUBJECT";
        string sp_callVal = "" + ddlSession.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + ddlSemester.SelectedValue + "," + ddlSubject.SelectedValue;
        string sp_name = "PKG_SP_GET_STUDENTS_LIST_CANCEL_EXAM_REG";

        DataSet dsStudent = objCommon.DynamicSPCall_Select(sp_name, sp_para, sp_callVal);
        if (dsStudent.Tables[0].Rows.Count > 0)
        {
            pnlStudents.Visible = true;
            lvStudents.Visible = true;
            lvStudents.DataSource = dsStudent;
            lvStudents.DataBind();
            hdncount.Value = dsStudent.Tables[0].Rows.Count.ToString();
            btnSubmit.Enabled = true;
        }
        else
        {
            objCommon.DisplayMessage(updExam, "Data Not Found", this.Page);
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearListView();
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
        txtRemark_Multiple.Text = string.Empty;

    }

    #region Single Student Exam Registration Cancel
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
      //  divSubjectsList.Visible = false;
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
        //divSubjectsList.Visible = false;
        btnSubmitSingle.Enabled = false;
        txtRemark_Single.Text = string.Empty;
    }
    protected void btnCancelSingle_Click(object sender, EventArgs e)
    {
        ClearFieldsSingle();
    }
    protected void lvSubject()
    {
        lvSubjects.Visible = false;
        divSubjectsList.Visible = false;
        lvSubjects.DataSource = null;
        lvSubjects.DataBind();
    }
    private void ClearFieldsSingle() {
        lvSubject();
        ddlSessionSingle.SelectedIndex = 0;
        ddlSemesterSingle.SelectedIndex = 0;
        txtRegNo.Text = string.Empty;
        txtRemark_Single.Text = string.Empty;
    }
    protected void btnShowSingle_Click(object sender, EventArgs e)
    {
        try
        {
            //ClearListView();
            int Idno = 0;
            string Reg = "'" + txtRegNo.Text.Trim() + "'";
            Idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "REGNO= " + Reg));
            string p_name = "PKG_SP_GET_SUBJECTS_LIST_TO_CANCEL_EXAM_BY_IDNO";
            string p_para = "@P_SESSIONNO,@P_SEMESTERNO,@P_IDNO";
            string p_call = "" + ddlSessionSingle.SelectedValue + "," + ddlSemesterSingle.SelectedValue + ","+Idno;

            DataSet dsSubject=objCommon.DynamicSPCall_Select(p_name,p_para,p_call);
            if (dsSubject.Tables[0].Rows.Count > 0)
            {
                divSubjectsList.Visible = true;
                pnlSubjects.Visible = true;
                lvSubjects.DataSource = dsSubject;
                lvSubjects.DataBind();
                lvSubjects.Visible = true;
                hdncount.Value = dsSubject.Tables[0].Rows.Count.ToString();
                foreach (ListViewDataItem item in lvSubjects.Items)
                {
                    CheckBox chekSub = item.FindControl("chkSubject") as CheckBox;
                    Label lblStatus = item.FindControl("lblStatusSub") as Label;
                    HiddenField hdnCancel = item.FindControl("hdnCancel") as HiddenField;
                    if (hdnCancel.Value.Equals("1"))
                    {
                        lblStatus.Text = "Registered".ToString();
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        chekSub.Checked = true;
                        lblStatus.Text = "Canceled".ToString();
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
            string proc_param = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_SUBJECT,@P_IDNO,@P_UANO,@P_IPADDRESS,@P_REMARK,@P_OUTPUT";
            string proc_name = "PKG_SP_CANCEL_STUDENTS_EXAM";
           
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
                    string proc_call = "" + ddlSessionSingle.SelectedValue + "," + SchemeNo + "," + ddlSemesterSingle.SelectedValue + "," + CourseNo + "," + Id + "," + Session["userno"].ToString() + "," + ipAddress + "," + Remark +","+ "0";
                  //  CustomStatus cs = (CustomStatus)objCourse.UpdateStudents_Cancel_Course(Convert.ToInt32(ddlSessionSingle.SelectedValue), SchemeNo, Convert.ToInt32(ddlSemesterSingle.SelectedValue), CourseNo, Id, Convert.ToInt32(Session["userno"]), ipAddress, Remark);

                    string retSingle = objCommon.DynamicSPCall_IUD(proc_name, proc_param, proc_call, true);
                    CustomStatus cs = (CustomStatus)Convert.ToInt32(retSingle);
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
    #endregion
}