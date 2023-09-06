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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_EXAMINATION_BacklogRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                //Fill DropDown List
                PopulateDropDownList();
               
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BacklogRegistration.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //Fill DropdownList
    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
            objCommon.FillDropDownList(ddlSemester, "ACD_COURSE A INNER JOIN ACD_SEMESTER B ON (A.SEMESTERNO = B.SEMESTERNO)", "distinct a.semesterno", "b.SEMESTERNAME", "a.semesterno>0", "a.semesterno");
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BacklogRegistration.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lvsession.DataSource = null;
        lvsession.DataBind();
        lblname.Text = string.Empty;
        pnlcourse.Visible = false;
        btnadd.Enabled = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        if (txtEnrollmentNo.Text != string.Empty)
        {
            if ((objCommon.LookUp("ACD_STUDENT_RESULT", "regno", "regno=" + "'" + txtEnrollmentNo.Text + "'")) != string.Empty)
            {
                ViewState["idno"] = (objCommon.LookUp("ACD_STUDENT_RESULT", "idno", "regno=" + "'" + txtEnrollmentNo.Text + "'"));
               string name= objCommon.LookUp("acd_student", "studname", "idno=" + Convert.ToInt32(ViewState["idno"]));
               lblname.Text = name;
                objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT_HIST A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO = B.SCHEMENO)", "distinct a.schemeno", "b.SCHEMENAME", "a.schemeno>0 and a.regno=" + "'" + txtEnrollmentNo.Text + "'", "a.schemeno");
                ddlScheme.Focus();
            }
            else
            {
                objCommon.DisplayMessage("Sorry....Registration Number Not Present", this.Page);
            }
        }
        
       
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlstatus.Items.Clear();
        ddlstatus.Items.Add(new ListItem("Please Select", "2"));
        ddlstatus.Items.Add(new ListItem("Regular", "0"));
        ddlstatus.Items.Add(new ListItem("BackLog", "1"));
        objCommon.FillDropDownList(ddlSubject, "ACD_COURSE", "COURSENO", "(CCODE+' -- '+COURSE_NAME)AS COURSE_NAME", "COURSENO>0 and SEMESTERNO=" + ddlSemester.SelectedValue + "and SCHEMENO=" + ddlScheme.SelectedValue, "COURSENO DESC");
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvsession.DataSource = null;
        lvsession.DataBind();
        if (ddlScheme.SelectedIndex == 0)
        {
            pnlcourse.Visible = false;
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            btnadd.Enabled = false;
        }
        else
        {
            pnlcourse.Visible = true;
            btnadd.Enabled = true;
            CourseBind();
        }
    }
    private void CourseBind()
    {
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST CR INNER JOIN ACD_COURSE C ON (CR.COURSENO = C.COURSENO)INNER JOIN ACD_SEMESTER S ON (CR.SEMESTERNO = S.SEMESTERNO)", "cr.COURSENO", "CR.IDNO,CR.CCODE,CR.COURSENAME,C.CREDITS,CR.SEMESTERNO,S.SEMESTERNAME,(CASE CR.ELECT WHEN 0 THEN 'NO' ELSE 'YES' END)ELECTIVE,(CASE CR.PREV_STATUS WHEN 0 THEN 'REGULAR' ELSE 'BACKLOG' END)STATUS,(CASE CR.SUBID WHEN 1 THEN 'THEORY' ELSE 'PRACTICAL' END)courseStatus", "CR.SESSIONNO=" + ddlSession.SelectedValue + "and CR.REGNO=" + "'" + txtEnrollmentNo.Text + "'" + "and CR.SCHEMENO=" + ddlScheme.SelectedValue, "CR.SEMESTERNO");
        lvCourse.DataSource = ds;
        lvCourse.DataBind();
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;
        BacklogRegistration objBaclog = new BacklogRegistration();
       
        int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        int semesterno = Convert.ToInt32(btnDel.AlternateText);
        int CourseNo = Convert.ToInt32(btnDel.CommandArgument);
        int idno = Convert.ToInt32(ViewState["idno"].ToString());
        string IPADDRESS = Session["ipAddress"].ToString();
        int UA_NO = Convert.ToInt32(Session["userno"]);

        if (Convert.ToInt16(objBaclog.DeleteCourse(SessionNo, SchemeNo, semesterno, CourseNo,idno,IPADDRESS,UA_NO))== Convert.ToInt16(CustomStatus.RecordUpdated))
        {
            CourseBind();
            objCommon.DisplayMessage("Course Deleted Successfully...",this.Page);
        }
        else
            objCommon.DisplayMessage("Course  cant be Deleted ", this.Page);
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (ddlstatus.SelectedValue == "2")
        {
            objCommon.DisplayMessage("Please Select Status", this.Page);
            return;
        }
        else
        {
            BacklogRegistration objBaclog = new BacklogRegistration();
            int SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            int SCHEMENO = Convert.ToInt32(ddlScheme.SelectedValue);
            int IDNO = Convert.ToInt32(ViewState["idno"].ToString());
            int SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
            int COURSENO = Convert.ToInt32(ddlSubject.SelectedValue);
            string IPADDRESS = Session["ipAddress"].ToString();
            int UA_NO = Convert.ToInt32(Session["userno"]);
            int status = Convert.ToInt32(ddlstatus.SelectedValue);
            int COUNT = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT_HIST", "COUNT(1)", "SESSIONNO=" + SESSIONNO + " AND SCHEMENO=" + SCHEMENO + " AND IDNO=" + IDNO + " AND SEMESTERNO=" + SEMESTERNO + " AND COURSENO=" + COURSENO + ""));
            if (COUNT > 0)
            {
                objCommon.DisplayMessage("Course Already Registered...", this.Page);
                return;
            }
            else
            {
                if (Convert.ToInt16(objBaclog.AddCourses(SESSIONNO, SCHEMENO, IDNO, SEMESTERNO, COURSENO, IPADDRESS, UA_NO, status)) == Convert.ToInt16(CustomStatus.RecordSaved))
                {
                    CourseBind();
                    objCommon.DisplayMessage("Course Added Successfully...", this.Page);
                }
                else
                    objCommon.DisplayMessage("Course cant be Added", this.Page);
            }

        }
       
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet ds;
        int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='"+ txtEnrollmentNo.Text+"'"));
        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST", "DISTINCT SESSIONNO", "DBO.FN_DESC('SESSIONNAME',SESSIONNO)SESSION", "COURSENO=" + ddlSubject.SelectedValue + " AND IDNO=" + IDNO, "SESSIONNO DESC");
        lvsession.DataSource = ds;
        lvsession.DataBind();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlcourse.Visible = false;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        ddlScheme.SelectedIndex = 0;
        lblname.Text = string.Empty;
        txtEnrollmentNo.Text = string.Empty;
        lvsession.DataSource = null;
        lvsession.DataBind();
        
    }
}
