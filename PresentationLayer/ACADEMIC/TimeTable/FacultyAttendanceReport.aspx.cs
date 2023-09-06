using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Drawing;

public partial class ACADEMIC_TimeTable_FacultyAttendanceReport : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();

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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    PopulateDropDownList();

                    if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        divFaculty.Visible = false;
                    }
                    else
                    {
                        divFaculty.Visible = true;
                    }
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
        }
    }


    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() == "3")
            {
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING CM INNER JOIN ACD_COURSE_TEACHER CT ON (CM.COLLEGE_ID = CT.COLLEGE_ID)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "UA_NO = " + Session["userno"] + " AND ISNULL(CT.CANCEL,0)=0 ", "");
            }
            else
            {
                objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControls()
    {
        ddlSchoolInstitute.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add("Please Select");
        ddlSem.Items.Clear();
        ddlSem.Items.Add("Please Select");
        ddlSection.Items.Clear();
        ddlSection.Items.Add("Please Select");
        ddlSubject.Items.Clear();
        ddlSubject.Items.Add("Please Select");
        ddlFaculty.Items.Clear();
        ddlFaculty.Items.Add("Please Select");
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 and DEPTNO=" + Session["userdeptno"].ToString() + "", "DEPTNAME ASC");
            }
            else
            {
                // clgID = ddlSchoolInstitute.SelectedIndex > 0 ? ddlSchoolInstitute.SelectedValue : "0";
                objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON D.DEPTNO=CDB.DEPTNO", "DISTINCT D.DEPTNO", "DEPTNAME", "D.DEPTNO>0 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "", "DEPTNAME ASC");
            }
            ddlDepartment.Focus();
        }
        else
        {
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add("Please Select");
            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSection.Items.Clear();
            ddlSection.Items.Add("Please Select");
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("Please Select");
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
        }
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
            if (Convert.ToInt32(Session["usertype"]) == 3)
            {

                objCommon.FillDropDownList(ddlSection, "ACD_SECTION SEC INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SECTIONNO=SEC.SECTIONNO)", "DISTINCT CT.SECTIONNO", "SEC.SECTIONNAME", "CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND SEC.SECTIONNO>0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.SECTIONNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlSection, "ACD_SECTION SEC INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SECTIONNO=SEC.SECTIONNO)", "DISTINCT CT.SECTIONNO", "SEC.SECTIONNAME", "CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND SEC.SECTIONNO>0", "CT.SECTIONNO");
            }
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add("Please Select");
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("Please Select");
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
        }
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedIndex > 0)
        {
            if (Convert.ToInt32(Session["usertype"]) == 3)
            {
                //objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + ddlScheme.SelectedValue + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0", "CT.COURSENO");
                objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.COURSENO");
            }
            else
            {
                //objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + ddlScheme.SelectedValue + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "CT.COURSENO");
                objCommon.FillDropDownList(ddlSubject, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON (C.COURSENO=CT.COURSENO)", "DISTINCT CT.COURSENO", "C.CCODE+' - '+C.COURSE_NAME AS COURSE_NAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SEMESTERNO=" + ddlSem.SelectedValue + " AND CT.SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0", "CT.COURSENO");
            }
            ddlSubject.Focus();
        }
        else
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("Please Select");
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
        }
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubject.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC U INNER JOIN ACD_COURSE_TEACHER CT ON (CT.UA_NO=U.UA_NO)", "DISTINCT CT.UA_NO", "U.UA_FULLNAME", "CT.COURSENO=" + ddlSubject.SelectedValue + " AND SECTIONNO=" + ddlSection.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND SESSIONNO=" + ddlSession.SelectedValue + "", "U.UA_FULLNAME");
            ddlFaculty.Focus();
        }
        else
        {
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
        }
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }

    protected void btnAttReport_Click(object sender, EventArgs e)
    {
        int facultyno = 0;
        if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            facultyno = Convert.ToInt32(Session["userno"]);
        }
        else
        {
            facultyno = Convert.ToInt32(ddlFaculty.SelectedValue);
        }

        DataSet ds = objAttC.GetFacultywiseAttendanceStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue), facultyno, Convert.ToInt32(ViewState["college_id"]));

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvAttStatus.DataSource = ds;
            lvAttStatus.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvAttStatus);//Set label
            lvAttStatus.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(updAttStatus, "No Record Found !", this.Page);
            lvAttStatus.Visible = false;
        }
    }

    protected void ddlSchoolInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchoolInstitute.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 and ODD_EVEN<>3 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            ddlSession.Focus();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add("Please Select");
            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSection.Items.Clear();
            ddlSection.Items.Add("Please Select");
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("Please Select");
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
        }
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string clgID = ddlSchoolInstitute.SelectedIndex > 0 ?ddlSchoolInstitute.SelectedValue: "0";
        int Deptno = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        if (ddlDepartment.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Deptno + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER SEM, ACD_SESSION_MASTER SM", "DISTINCT SEM.SEMESTERNO", "SEM.SEMESTERNAME", "SEM.SEMESTERNO%2 IN(CASE WHEN SM.ODD_EVEN=1 THEN SM.ODD_EVEN ELSE 0 END) AND SM.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEM.SEMESTERNO>0", "SEM.SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSection.Items.Clear();
            ddlSection.Items.Add("Please Select");
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add("Please Select");
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add("Please Select");
        }
        lvAttStatus.DataSource = null;
        lvAttStatus.DataBind();
    }
}