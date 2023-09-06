//======================================================================================
// PROJECT NAME  : ARKA JAIN                                                                
// MODULE NAME   : EXAMINATION
// PAGE NAME     : BACKLOGEXAM.ASPX                                                   
// CREATION DATE : 15-APRIL-2019                                                        
// CREATED BY    : ROHIT KUMAR TIWARI
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_BacklogExam : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student_Acd objSA = new Student_Acd();
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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO ");

                ddlSession.Focus();
                ViewState["RightData"] = 0;
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BacklogExam.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BacklogExam.aspx");
        }
    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "B.LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue, "A.BRANCHNO");
            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        txtTotStud.Text = "0";
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {

            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            //objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO INNER JOIN ACD_STUDENT_RESULT_HIST SR ON (S.SCHEMENO = SR.SCHEMENO)", "DISTINCT S.SCHEMENO", "S.SCHEMENAME +'  ['+ CAST(S.SCHEMENO AS VARCHAR(6)) +']'", "B.BRANCHNO = " + ddlBranch.SelectedValue + " AND SR.SESSIONNO = " + ddlSession.SelectedValue, "S.SCHEMENO DESC");               
            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Focus();
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        txtTotStud.Text = "0";
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT_HIST SR WITH (NOLOCK) ON (S.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SESSIONNO<=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + ddlScheme.SelectedValue, "SEMESTERNO");
            // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT_HIST r inner join ACD_SEMESTER s on (r.SEMESTERNO=s.SEMESTERNO)", "distinct r.SEMESTERNO", "s.SEMESTERNAME", "SCHEMENO=" + ddlScheme.SelectedValue + "and SESSIONNO=" + ddlSession.SelectedValue, "r.SEMESTERNO");

            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        txtTotStud.Text = "0";
    }
    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        objSA.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objSA.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objSA.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
        DataSet ds = objSC.GetStudentFailListBacklog(objSA);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            hftot.Value = ds.Tables[0].Rows.Count.ToString();
            txtTotStud.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            objCommon.DisplayMessage(updStudent, "Student not found", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            hftot.Value = "0";
            txtTotStud.Text = "0";
        }
    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
        objSA.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
        objSA.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objSA.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
        objSA.IpAddress = Session["IPADDRESS"].ToString();
        string idnos = GetStudentID();
        CustomStatus cs = (CustomStatus)objSC.AddStudBacklogCourses(objSA, Convert.ToInt32(ddlSession.SelectedValue), idnos);

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            ShowMessage("Bulk Backlog Exam Registration Done Successfully!!!");
        }
        else
        {
            ShowMessage("Bulk Backlog Exam Registration Done with Error...");
        }
    }

    private string GetStudentID()
    {
        string studentId = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;

                if (chk.Checked)
                {
                    if (studentId.Length > 0)
                        studentId += ",";
                    studentId += chk.ToolTip;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_BacklogExam.GetStudentID() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentId;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        txtTotStud.Text = "0";
    }
}