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
                }
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
         //   objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
           
        }
        catch
        {
            throw;
        }
    }
    #region Page Authorization
    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=CourseAllotment_Bulk.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=CourseAllotment_Bulk.aspx");
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion

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
            int semester = Convert.ToInt32(ddlSemester.SelectedValue);

            DataSet ds = objAttendC.GetCourseListForVerifyTeacherAllot(sessionno, schemeno, semester);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvBacklogCourse.DataSource = ds;
                lvBacklogCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvBacklogCourse);//Set label   
            }    
            else
            {
                objCommon.DisplayMessage(this, "No Teacher found for this selection!", this.Page);
            }

        }
        catch
        {
            throw;
        }
    }

    #region Backlog Course ItemDataBound  

    protected void lvBacklogCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
        int OrgId = Convert.ToInt32(Session["OrgId"].ToString());
        int semester = Convert.ToInt32(ddlSemester.SelectedValue);
        DataSet ds = objAttendC.GetCourseListForVerifyTeacherAllot(sessionno, schemeno, semester);

        DropDownList ddlTeacher = (DropDownList)e.Item.FindControl("ddlTeacher");
        HiddenField hdneditfield = (HiddenField)e.Item.FindControl("hdntecher");
        HiddenField hdnuano = (HiddenField)e.Item.FindControl("hdnuano");
        CheckBox chkCourseno = (CheckBox)e.Item.FindControl("chkCourseno");  

        objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_NO ASC");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
         if (ds.Tables[0].Rows[i]["UA_NO"].ToString().Equals(ds.Tables[0].Rows[i]["UA_NO"].ToString()))
         {
           if (e.Item.ItemType == ListViewItemType.DataItem)
           {
           

           // DataSet ds = objAttendC.GetBacklogCourseList(sessionno, schemeno);
                          
            //dataitem.FindControl("chkCourseno") as CheckBox;         

            
                    //foreach (ListViewDataItem dataitem in lvBacklogCourse.Items)
                    //{
                       
                                
                                //DropDownList ddlteacher = dataitem.FindControl("ddlTeacher") as DropDownList;                       
                                string aaa = ds.Tables[0].Rows[i]["UA_NO"].ToString();
                                //string aaa = "672";
                                ddlTeacher.SelectedValue = hdnuano.Value;
                               // objCommon.FillDropDownList(ddlTeacher, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_NO ASC");
                                //int Status = Convert.ToInt32(ds.Tables[0].Rows[i]["REG_BACKLOG"]);
                                //if (Status ==1)
                                //{
                                   // chkCourseno.Checked = true;
                               //}
                           }
                     }
               // }    
        }
    }

    #endregion

    #region Submit

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int validddlcount = 0;
        int chkCount=0;
        int Finalcount = 0;
        int schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int college_id = Convert.ToInt32(ViewState["college_id"]);
        int semester = Convert.ToInt32(ddlSemester.SelectedValue);
        try
        {
            foreach (ListViewDataItem dataitem in lvBacklogCourse.Items)
            {

                CheckBox chkCourseno = dataitem.FindControl("chkCourseno") as CheckBox;
                DropDownList ddlteacher = dataitem.FindControl("ddlTeacher") as DropDownList;
                if (chkCourseno.Checked && ddlteacher.SelectedIndex >0)
                {
                    chkCount++;


                    if (ddlteacher.SelectedIndex > 0 )
                    {
                        // validddlcount++;
                    }

                    int teacherUA_NO = Convert.ToInt32(ddlteacher.SelectedValue);
                    HiddenField sem = dataitem.FindControl("hdnSemesterno") as HiddenField;
                    HiddenField courseCode = dataitem.FindControl("hdnCourseCode") as HiddenField;


                    int courseno = Convert.ToInt32(chkCourseno.ToolTip);
                    // count++;
                    // if (validddlcount == chkCount)
                    //{
                    //CustomStatus cs = (CustomStatus)objAttendC.SaveBacklogCourseTeacherAllot(teacherUA_NO, courseno, Convert.ToInt32(sem.Value), college_id, sessionno, schemeno, courseCode.Value);
                    CustomStatus cs = (CustomStatus)objAttendC.VerifyTeacherAllotment(teacherUA_NO, courseno, semester, college_id, sessionno, schemeno, courseCode.Value);
                    //if (cs.Equals(CustomStatus.RecordSaved))
                    //{
                       Finalcount++;

                    // }
                }
                else if (chkCourseno.Checked && ddlteacher.SelectedIndex ==0)
                {
                    objCommon.DisplayMessage(this.Page, "Please Select teacher for selected courses.", this.Page);
                    return;

                }
                // if (chkCourseno.Checked==false)
                 
                }

                if (Finalcount > 0)
                {
                    objCommon.DisplayMessage(this.Page, "Verfiy Allotment Done Successfully!", Page);
                    BindListView();

                }
                
                 if (chkCount == 0)
                 {
                     // objCommon.DisplayMessage(this.Page, "Please Select teacher for selected courses.", this.Page);
                     objCommon.DisplayMessage(this.Page, "Please Select atleast one Course to Submit.", this.Page);
                     return;
                 }
            }
              
        catch
        {
            objCommon.DisplayMessage(this.Page, "Something Went Wrong!", Page);
        }
    }
    #endregion

    #region checkbox Event

    protected void chkCourseno_CheckedChanged(object sender, EventArgs e)
    {
        //foreach (ListViewDataItem lv in lvBacklogCourse.Items)
        //{
        //    DropDownList ddlteacher = lv.FindControl("ddlTeacher") as DropDownList;


        //    CheckBox chkCourseno = lv.FindControl("chkCourseno") as CheckBox;

        //    if (chkCourseno.Checked == true)
        //    {
        //        ddlteacher.Enabled = true;
        //    }
        //    else
        //    {
        //        ddlteacher.Enabled = false;
        //    }
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        int schemeno = Convert.ToInt32(ViewState["schemeno"].ToString());
        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + schemeno + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
        ddlSemester.Focus();
    }
}