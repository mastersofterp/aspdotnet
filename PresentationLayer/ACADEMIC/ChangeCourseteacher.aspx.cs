using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ACADEMIC_ChangeCourseteacher : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttendC = new AcdAttendanceController();

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
                    this.FillDropdown();
                    CheckPageAuthorization();
                }
            }
            btnSubmit.Visible = false;
        }
        catch
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ChangeCourseteacher.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ChangeCourseteacher.aspx");
            }
        }
        catch
        {
            throw;
        }
    }
    private void FillDropdown()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COSCHNO");
            }

            //objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + '- ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3", "UA_NO ASC");
        }
        catch
        {
            throw;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //btnSubmit.Visible = true;
            this.BindListView();

        }
        catch
        {
            throw;
        }
    }
    private void BindListView()
    {
        try
        {
            if (ddlSemester.SelectedIndex <0)
            {
                objCommon.DisplayUserMessage(updPanel1, "Please select Semester.", this.Page);
                return;
            }
            if (ddlCourse.SelectedIndex <0)
            {
                objCommon.DisplayUserMessage(updPanel1, "Please select Course.", this.Page);
                return;
            }
            lvBacklogCourse.DataSource = null;
            lvBacklogCourse.DataBind();
            ddlFaculty.SelectedIndex = 0;
            txtRemark.Text = string.Empty;
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
            string semesterno = string.Empty;
            for (int i = 0; i < ddlSemester.Items.Count; i++)
            {
                if (ddlSemester.Items[i].Selected)
                {
                    if (semesterno == string.Empty)
                        semesterno = ddlSemester.Items[i].Value;
                    else
                        semesterno += ',' + ddlSemester.Items[0].Value;
                }
            }

            string courseno = string.Empty;
            for (int i = 0; i < ddlCourse.Items.Count; i++)
            {
                if (ddlCourse.Items[i].Selected)
                {
                    if (courseno == string.Empty)
                        courseno = ddlCourse.Items[i].Value;
                    else
                        courseno += ',' + ddlCourse.Items[i].Value;
                }
            }

            DataSet ds = objAttendC.GetRegularCourseList(sessionno, schemeno, Convert.ToInt32(ViewState["college_id"]), semesterno, courseno);
            ViewState["dsCourse"] = ds;
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
               // DataSet dsTeacher = objCommon.FillDropDown("USER_ACC CROSS APPLY DBO.FN_SPLIT(UA_COLLEGE_NOS,',') ", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3 AND UA_STATUS=0  AND VALUE=" + ViewState["college_id"], "UA_NO ASC");
               // ViewState["dsTeacher"] = dsTeacher;
                lvBacklogCourse.DataSource = ds;
                lvBacklogCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBacklogCourse);//Set label   
                btnSubmit.Visible = true;
            }
            else
            {
                lvBacklogCourse.DataSource = null;
                lvBacklogCourse.DataBind();
                objCommon.DisplayMessage(this, "No Teacher found for this selection!", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedIndex ==-1)
        {
            objCommon.DisplayUserMessage(updPanel1, "Please select Faculty.", this.Page);
            return;
        }
        if (ddlSemester.SelectedIndex <0)
        {
            objCommon.DisplayUserMessage(updPanel1, "Please select Semester.", this.Page);
            return;
        }
        if (ddlCourse.SelectedIndex <0)
        {
            objCommon.DisplayUserMessage(updPanel1, "Please select Course.", this.Page);
            return;
        }
        btnSubmit.Visible = true;
        int chkCount = 0;
        int Finalcount = 0;
        
        int schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int college_id = Convert.ToInt32(ViewState["college_id"]);

        try
        {
            foreach (ListViewDataItem dataitem in lvBacklogCourse.Items)
            {

                CheckBox chkCourseno = dataitem.FindControl("chkCourseno") as CheckBox;
                //  DropDownList ddlteacher = dataitem.FindControl("ddlTeacher") as DropDownList;
                DropDownList ddlNewTeacher = dataitem.FindControl("ddlNewTeacher") as DropDownList;
                HiddenField hdnUano = dataitem.FindControl("hdnuano") as HiddenField;

                if (chkCourseno.Checked)
                {
                    chkCount++;
                    if (txtRemark.Text == "")
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Remark.", this.Page);
                        return;
                    }
                    //int teacherUA_NO = Convert.ToInt32(ddlFaculty.SelectedValue); if we use single dropdown
                    int teacherUA_NO = Convert.ToInt32(hdnUano.Value);
                    int NewteacherUA_NO = Convert.ToInt32(ddlFaculty.SelectedValue);//Convert.ToInt32(ddlNewTeacher.SelectedValue);
                    HiddenField sem = dataitem.FindControl("hdnSemesterno") as HiddenField;
                    HiddenField courseCode = dataitem.FindControl("hdnCourseCode") as HiddenField;
                    HiddenField hfSectionNo = dataitem.FindControl("hfSectionNo") as HiddenField;
                    HiddenField hfCtno = dataitem.FindControl("hfCtno") as HiddenField;
                    string remark = txtRemark.Text;
                    int courseno = Convert.ToInt32(chkCourseno.ToolTip);
                    CustomStatus cs = (CustomStatus)objAttendC.SaveChangeCourseTeacherAllot(teacherUA_NO, courseno, Convert.ToInt32(sem.Value), college_id, sessionno, schemeno, courseCode.Value, NewteacherUA_NO, Convert.ToInt32(hfSectionNo.Value), Convert.ToInt32(hfCtno.Value), remark);

                    Finalcount++;
                }
                
                
                // if (chkCourseno.Checked==false)

            }
            if (Finalcount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please select at least one course from list for change course teacher.", this.Page);
                return;
            }

            if (Finalcount > 0)
            {
                objCommon.DisplayMessage(this.Page, "Data Saved Successfully", Page);
                ddlFaculty.SelectedIndex = 0;
                txtRemark.Text = string.Empty;
                BindListView();
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey1", "$('#ctl00_ContentPlaceHolder1_lvBacklogCourse_cbHead').prop('checked',false);", true);
            }
            if (chkCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Check Atleast one Course from List", this.Page);
                return;
            }
        }
        catch
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlScheme.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        lvBacklogCourse.DataSource = null;
        lvBacklogCourse.DataBind();
        txtRemark.Text = "";
        ddlFaculty.SelectedIndex = 0;
        ddlSemester.ClearSelection();
        ddlCourse.ClearSelection();
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvBacklogCourse.DataSource = null;
            lvBacklogCourse.DataBind();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlScheme.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
                ddlSession.Focus();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void lvBacklogCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
        int OrgId = Convert.ToInt32(Session["OrgId"].ToString());

        DropDownList ddlTeacher = (DropDownList)e.Item.FindControl("ddlTeacher");
        DropDownList ddlNewTeacher = (DropDownList)e.Item.FindControl("ddlNewTeacher");
        HiddenField hdneditfield = (HiddenField)e.Item.FindControl("hdntecher");
        HiddenField hdnuano = (HiddenField)e.Item.FindControl("hdnuano");
        CheckBox chkCourseno = (CheckBox)e.Item.FindControl("chkCourseno");
        DataSet ds = (DataSet)ViewState["dsCourse"]; //objAttendC.GetRegularCourseList(sessionno, schemeno);
        DataSet dsTeacher = (DataSet)ViewState["dsTeacher"];
        // objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3", "UA_NO ASC");
        // objCommon.FillDropDownList(ddlNewTeacher, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3 " , "UA_NO ASC");

        //ddlTeacher.Items.Clear();
        //ddlTeacher.Items.Add("Please Select");
        //ddlTeacher.SelectedItem.Value = "0";

        //if (dsTeacher.Tables[0].Rows.Count > 0)
        //{
        //    ddlTeacher.DataSource = dsTeacher;
        //    ddlTeacher.DataValueField = dsTeacher.Tables[0].Columns[0].ToString();
        //    ddlTeacher.DataTextField = dsTeacher.Tables[0].Columns[1].ToString();
        //    ddlTeacher.DataBind();
        //    ddlTeacher.SelectedIndex = 0;
        //}
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["UA_NO"].ToString().Equals(ds.Tables[0].Rows[i]["UA_NO"].ToString()))
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    DataTable dt = ds.Tables[0].Select("UA_NO<>" + Convert.ToInt32(hdnuano.Value).ToString()).CopyToDataTable();
                    string aaa = ds.Tables[0].Rows[i]["UA_NO"].ToString();
                    ddlNewTeacher.Items.Clear();
                    ddlNewTeacher.Items.Add("Please Select");
                    ddlNewTeacher.SelectedItem.Value = "0";

                    if (dsTeacher.Tables[0].Rows.Count > 0)
                    {
                        ddlNewTeacher.DataSource = dt;
                        ddlNewTeacher.DataValueField = dt.Columns[0].ToString();
                        ddlNewTeacher.DataTextField = dt.Columns[1].ToString();
                        ddlNewTeacher.DataBind();
                        ddlNewTeacher.SelectedIndex = 0;
                    }
                    //   ddlTeacher.SelectedValue = hdnuano.Value;
                    //objCommon.FillDropDownList(ddlNewTeacher, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3 AND UA_NO<>" + Convert.ToInt32(hdnuano.Value), "UA_NO ASC");
                }
            }
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(ddlSemester, "ACD_SEMESTER A INNER JOIN ACD_COURSE_TEACHER B ON A.SEMESTERNO=B.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "B.SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_ID=" + ViewState["college_id"] + "AND B.SCHEMENO=" + ViewState["schemeno"] + " AND ISNULL(B.CANCEL,0)=0", "A.SEMESTERNO");
        objCommon.FillListBox(ddlCourse, "ACD_COURSE A INNER JOIN ACD_COURSE_TEACHER B ON A.COURSENO=B.COURSENO", "DISTINCT A.COURSENO", "A.CCODE+'-'+COURSE_NAME", "B.SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_ID=" + ViewState["college_id"] + " AND B.SCHEMENO=" + ViewState["schemeno"] + " AND ISNULL(B.CANCEL,0)=0", "A.COURSENO");
        objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + '- ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3 AND UA_STATUS=0", "UA_NO ASC");
        lvBacklogCourse.DataSource = null;
        lvBacklogCourse.DataBind();
        txtRemark.Text = "";
        
    }
}