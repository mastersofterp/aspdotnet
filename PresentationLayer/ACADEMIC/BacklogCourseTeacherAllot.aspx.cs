using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_BacklogCourseTeacherAllot : System.Web.UI.Page
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
                    Response.Redirect("~/notauthorized.aspx?page=BacklogCourseTeacherAllot.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=BacklogCourseTeacherAllot.aspx");
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

            objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + '- ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3", "UA_NO ASC");
        }
        catch
        {
            throw;
        }
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Visible = true;
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
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
            int OrgId = Convert.ToInt32(Session["OrgId"].ToString());


            if (rdoSelect.SelectedValue == "1")
            {
                DataSet ds = objAttendC.GetBacklogCourseList(sessionno, schemeno);
                if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lvBacklogCourse.DataSource = ds;
                    lvBacklogCourse.DataBind();
                    divFaculty.Visible = true;
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBacklogCourse);//Set label   
                }
                else
                {
                    objCommon.DisplayMessage(this, "No Teacher found for this selection!", this.Page);
                }
            }
            else
            {
                DataSet ds = objAttendC.GetRedoCourseRegistrationList(sessionno, schemeno);
                if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lvBacklogCourse.DataSource = ds;
                    lvBacklogCourse.DataBind();
                    divFaculty.Visible = true;
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBacklogCourse);//Set label   
                }
                else
                {
                    objCommon.DisplayMessage(this, "No Teacher found for this selection!", this.Page);
                }
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
        HiddenField hdneditfield = (HiddenField)e.Item.FindControl("hdntecher");
        HiddenField hdnuano = (HiddenField)e.Item.FindControl("hdnuano");
        CheckBox chkCourseno = (CheckBox)e.Item.FindControl("chkCourseno");

        if (rdoSelect.SelectedValue == "1")
        {
            DataSet ds = objAttendC.GetBacklogCourseList(sessionno, schemeno);
            objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3", "UA_NO ASC");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["UA_NO"].ToString().Equals(ds.Tables[0].Rows[i]["UA_NO"].ToString()))
                {
                    if (e.Item.ItemType == ListViewItemType.DataItem)
                    {
                        string aaa = ds.Tables[0].Rows[i]["UA_NO"].ToString();

                        ddlTeacher.SelectedValue = hdnuano.Value;
                    }
                }
            }
        }
        else
        {
            DataSet ds = objAttendC.GetRedoCourseRegistrationList(sessionno, schemeno);
            objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "UA_NO", "(UA_NAME COLLATE DATABASE_DEFAULT + ' - ' +UA_FULLNAME COLLATE DATABASE_DEFAULT) AS UA_FULLNAME", "UA_TYPE=3", "UA_NO ASC");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["UA_NO"].ToString().Equals(ds.Tables[0].Rows[i]["UA_NO"].ToString()))
                {
                    if (e.Item.ItemType == ListViewItemType.DataItem)
                    {
                        string aaa = ds.Tables[0].Rows[i]["UA_NO"].ToString();

                        ddlTeacher.SelectedValue = hdnuano.Value;
                    }
                }
            }
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
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
                DropDownList ddlteacher = dataitem.FindControl("ddlTeacher") as DropDownList;
                if (chkCourseno.Checked && ddlteacher.SelectedIndex > 0)
                {
                    chkCount++;


                    if (ddlteacher.SelectedIndex > 0)
                    {
                        // validddlcount++;
                    }

                    //int teacherUA_NO = Convert.ToInt32(ddlFaculty.SelectedValue); if we use single dropdown
                    int teacherUA_NO = Convert.ToInt32(ddlteacher.SelectedValue);
                    HiddenField sem = dataitem.FindControl("hdnSemesterno") as HiddenField;
                    HiddenField courseCode = dataitem.FindControl("hdnCourseCode") as HiddenField;


                    int courseno = Convert.ToInt32(chkCourseno.ToolTip);
                    // count++;
                    // if (validddlcount == chkCount)
                    //{
                    if (rdoSelect.SelectedValue == "1")
                    {
                        CustomStatus cs = (CustomStatus)objAttendC.SaveBacklogCourseTeacherAllot(teacherUA_NO, courseno, Convert.ToInt32(sem.Value), college_id, sessionno, schemeno, courseCode.Value);
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objAttendC.SaveRedoCourseTeacherAllot(teacherUA_NO, courseno, Convert.ToInt32(sem.Value), college_id, sessionno, schemeno, courseCode.Value);
                    }
                    //if (cs.Equals(CustomStatus.RecordSaved))
                    //{
                    Finalcount++;

                    // }
                }
                else if (chkCourseno.Checked && ddlteacher.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select teacher for selected courses.", this.Page);
                    return;

                }
                // if (chkCourseno.Checked==false)

            }

            if (Finalcount > 0)
            {
                objCommon.DisplayMessage(this.Page, "Data Saved Successfully", Page);
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
    protected void chkCourseno_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    private void ClearAll()
    {
        divFaculty.Visible = false;
        ddlScheme.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlFaculty.SelectedIndex = 0;
        lvBacklogCourse.DataSource = null;
        lvBacklogCourse.DataBind();
    }

    protected void rdoSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
    }
}