//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Faculty Roll List Or Roster                                                
// CREATION DATE : 15-Jan-2024
// CREATED BY    : GOPAL MANDAOGADE                               
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System.Linq;
using System.Web;
using System.Xml.Linq;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;
using System.Data.OleDb;

public partial class Administration_FacultyRollListOrRoster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    Course objC = new Course();

    //ConnectionStrings
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                string College_code = objCommon.LookUp("REFF", "College_code", "OrganizationId = '" + Session["OrgId"].ToString() + "'");
                ViewState["college_id"] = College_code;

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
               
              
                //Populate the DropDownList 
             
                 var userNo = Session["userno"].ToString();
                 var uaType = Session["usertype"].ToString();
                 if (Session["usertype"].ToString() != "1")
                 {
                     Faculty_Div.Visible = false;
                 }
                 else
                 {
                     Faculty_Div.Visible = true;
                 }
                 PopulateDropDown();

            }
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipaddress"] = Request.ServerVariables["REMOTE_ADDR"];
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
        }
    }

    #endregion

    #region Dropdown List Events
    private void PopulateDropDown()
    {
        try
        {
             if (Session["usertype"].ToString() != "1")
            {
                //this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID) INNER JOIN ACD_STUDENT_RESULT SR ON(SM.SESSIONNO = SR.SESSIONNO)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 AND (SR.UA_NO = " + Convert.ToInt32(Session["userno"]) + " OR SR.UA_NO_PRAC = " + Convert.ToInt32(Session["userno"]) + " OR SR.UA_NO_TUTR = " + Convert.ToInt32(Session["userno"]) + ")", "S.SESSIONID DESC");
                this.objCommon.FillDropDownList(ddlSession, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON CT.SESSIONNO = SM.SESSIONNO INNER JOIN ACD_SESSION S WITH (NOLOCK) ON SM.SESSIONID = S.SESSIONID", "DISTINCT S.SESSIONID", "S.SESSION_NAME ", "ISNULL(CANCEL,0)=  0  AND  ISNULL(S.IS_ACTIVE,0) = 1 AND ISNULL(S.FLOCK,0) = 1  AND  CT.UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + "", "S.SESSIONID DESC");
            }
            else
            {
                this.objCommon.FillDropDownList(ddlSession, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON CT.SESSIONNO = SM.SESSIONNO INNER JOIN ACD_SESSION S WITH (NOLOCK) ON SM.SESSIONID = S.SESSIONID", "DISTINCT S.SESSIONID", "S.SESSION_NAME ", "ISNULL(CANCEL,0)=  0  AND  ISNULL(S.IS_ACTIVE,0) = 1 AND ISNULL(S.FLOCK,0) = 1", "S.SESSIONID DESC");
                //this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID", "S.SESSION_NAME", "ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1", "S.SESSIONID DESC");
                //Fill Dropdown Session 
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindCourseList(int sessionId, int faculty_uano, string semesterno, int coursetype, int courseno, int sectionno, int batchno, int tut_batchno)
    {
        DataSet ds = null;
        try
        {
            CourseController objCC = new CourseController();
            ds = objCC.GetStudentRollListAndRosterAllCourseRegistrationData(sessionId, faculty_uano, semesterno, coursetype, courseno, sectionno, batchno, tut_batchno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //Session["TempData"] = ds;
                hftot.Value = ds.Tables[0].Rows.Count.ToString();
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label 
                //Session["CheckExistsDB"] = ds.Tables[0];
                //Session["CheckExistsLUDB"] = ds.Tables[1];
                //Session["TempCourseDB"] = null;
            }
            else
            {

                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage(this.updpnlSection, "Record Not found!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.BindCourseList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   //for course check lock unlock
    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = e.Item as ListViewDataItem;

        Label lblStatus = item.FindControl("lblSTATUS") as Label;
        if (lblStatus.Text == "Approved")
        {
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblStatus.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    #region Course Show Data and Clear Events

    protected void btnShowData_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedValue != "" && ddlSession.SelectedValue != "0")
            {
                if (ddlFaculty.SelectedValue != "" && ddlFaculty.SelectedValue != "0")
                {
                    if (ddlSemester.SelectedValue != "" && ddlSemester.SelectedValue != "0")
                    {
                        if (ddlCourseType.SelectedValue != "" && ddlCourseType.SelectedValue != "0")
                        {
                            if (ddlCourse.SelectedValue != "" && ddlCourse.SelectedValue != "0")
                            {
                                var semesternos = string.Empty;//ddlSemester.SelectedValue;
                                pnlPreCorList.Visible = true;
                                lvCourse.Visible = true;

                                foreach (ListItem items in ddlSemester.Items)
                                {
                                    if (items.Selected == true)
                                        semesternos += (items.Value).Split('-')[0] + ',';
                                }
                                semesternos = semesternos.TrimEnd(',');

                                var courseType = ddlCourseType.SelectedValue == "" ? "0" : ddlCourseType.SelectedValue;
                                var courseNo = ddlCourse.SelectedValue == "" ? "0" : ddlCourse.SelectedValue;
                                var sectionNo = ddlSection.SelectedValue == "" ? "0" : ddlSection.SelectedValue;
                                var batchNo = ddlBatch.SelectedValue == "" ? "0" : ddlBatch.SelectedValue;
                                var tut_batchNo = ddlTutorialBatch.SelectedValue == "" ? "0" : ddlTutorialBatch.SelectedValue;

                                BindCourseList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlFaculty.SelectedValue), semesternos, Convert.ToInt32(courseType), Convert.ToInt32(courseNo), Convert.ToInt32(sectionNo), Convert.ToInt32(batchNo), Convert.ToInt32(tut_batchNo));

                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updpnlSection, "Please Select Course!", this.Page);
                                return;
                            }

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updpnlSection, "Please Select Course Type!", this.Page);
                            return;
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnlSection, "Please Select Semester!", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlSection, "Please Select Faculty!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Session!", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FacultyRollListOrRoster.btnShowData_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();     
    }

    protected void Clear()
    {
        ddlSession.SelectedIndex = -1;
        ddlFaculty.SelectedIndex = -1; 
        ddlSemester.SelectedIndex = -1;
        ddlCourseType.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        ddlSection.SelectedIndex = -1;
        ddlBatch.SelectedIndex = -1;
        ddlTutorialBatch.SelectedIndex = -1;
        lblMsg.Text = string.Empty;
      //  lblStatus.Text = string.Empty;
        DataSet ds = null;
        lvCourse.DataSource = ds;
        lvCourse.DataBind();
        pnlPreCorList.Visible = false;
        lvCourse.Visible = false;
    }
    #endregion

    #region Dropdown List Events1
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            int SessionId = Convert.ToInt32(ddlSession.SelectedValue);
            this.objCommon.FillDropDownList(ddlFaculty, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK)  ON CT.SESSIONNO = SM.SESSIONNO INNER JOIN USER_ACC AC WITH (NOLOCK)  ON (CT.UA_NO = AC.UA_NO OR CT.ADTEACHER = AC.UA_NO)", "DISTINCT AC.UA_NO", "AC.UA_FULLNAME ", "ISNULL(SM.IS_ACTIVE,0) = 1 AND ISNULL(CANCEL,0) = 0 AND SM.SESSIONID  = " + SessionId + "  ", "AC.UA_FULLNAME");

            if (Session["usertype"].ToString() != "1")
            {
                ddlFaculty.SelectedValue = Session["userno"].ToString();
                int FacultyUANo = Convert.ToInt32(Session["userno"].ToString());
                objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (CT.SEMESTERNO = S.SEMESTERNO ) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK)  ON CT.SESSIONNO = SM.SESSIONNO ", "DISTINCT ISNULL(S.SEMESTERNO,0) SEMESTERNO", "S.SEMESTERNAME", "(CT.UA_NO = " + FacultyUANo + " OR  CT.ADTEACHER = " + FacultyUANo + ") AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND ISNULL(CT.CANCEL, 0) = 0", "SEMESTERNO");

            }
             //objCommon.FillDropDownList(ddlFaculty, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK)  ON SR.SESSIONNO = SM.SESSIONNO INNER JOIN ACD_SESSION S WITH (NOLOCK)  ON  SM.SESSIONID = S.SESSIONID INNER JOIN USER_ACC AC WITH (NOLOCK)  ON (SR.UA_NO = AC.UA_NO OR SR.UA_NO_PRAC = AC.UA_NO  OR SR.UA_NO_TUTR = AC.UA_NO)", "DISTINCT AC.UA_NO", "AC.UA_FULLNAME ", "UA_TYPE=3 AND ISNULL(S.IS_ACTIVE,0) = 1  AND S.SESSIONID = " + SessionId + "  ", "AC.UA_FULLNAME");
        }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedIndex > 0)
        {
            int FacultyUANo = Convert.ToInt32(ddlFaculty.SelectedValue);
           // objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON SR.SEMESTERNO = S.SEMESTERNO", "DISTINCT ISNULL(S.SEMESTERNO,0) SEMESTERNO", "S.SEMESTERNAME", "SR.UA_NO = " + FacultyUANo + "  ", "S.SEMESTERNAME");
            objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (CT.SEMESTERNO = S.SEMESTERNO ) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK)  ON CT.SESSIONNO = SM.SESSIONNO ", "DISTINCT ISNULL(S.SEMESTERNO,0) SEMESTERNO", "S.SEMESTERNAME", "(CT.UA_NO = " + FacultyUANo + " OR  CT.ADTEACHER = " + FacultyUANo + ") AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND ISNULL(CT.CANCEL, 0) = 0", "SEMESTERNO");

        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            int FacultyUANo = Convert.ToInt32(ddlFaculty.SelectedValue);
            //objCommon.FillDropDownList(ddlCourseType, "ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE C ON SR.COURSENO = C.COURSENO LEFT JOIN ACD_SUBJECTTYPE ST ON (C.SUBID =  ST.SUBID)", "DISTINCT ST.SUBID", "ST.SUBNAME", "SR.UA_NO  = " + FacultyUANo + " AND SR.SEMESTERNO = " + SemesterNo + "  ", "ST.SUBNAME");
            objCommon.FillDropDownList(ddlCourseType, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON CT.COURSENO = C.COURSENO INNER JOIN ACD_SUBJECTTYPE ST ON (C.SUBID =  ST.SUBID) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK)  ON CT.SESSIONNO = SM.SESSIONNO", "DISTINCT ST.SUBID", "ST.SUBNAME", "(CT.UA_NO = " + FacultyUANo + " OR  CT.ADTEACHER = " + FacultyUANo + ")  AND CT.SEMESTERNO  = " + SemesterNo + "  AND SM.SESSIONID  = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND  ISNULL(CT.CANCEL, 0) = 0 ", "ST.SUBNAME");
           
        }
    }
    protected void ddlCourseType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourseType.SelectedIndex > 0)
        {
            int SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            int FacultyUANo = Convert.ToInt32(ddlFaculty.SelectedValue);
            //objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE C ON SR.COURSENO = C.COURSENO LEFT JOIN ACD_SUBJECTTYPE ST ON (C.SUBID =  ST.SUBID)", "DISTINCT C.COURSENO", "C.CCODE+ ' - ' +C.COURSE_NAME  AS COURSE_NAME", "SR.UA_NO  = " + FacultyUANo + " AND SR.SEMESTERNO = " + SemesterNo + " AND  ST.SUBID =  " + CourseSubId + "  ", "COURSE_NAME");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COURSE C ON CT.COURSENO = C.COURSENO LEFT JOIN ACD_SUBJECTTYPE ST ON (C.SUBID =  ST.SUBID) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK)  ON CT.SESSIONNO = SM.SESSIONNO", "DISTINCT C.COURSENO", "C.CCODE+ ' - ' +C.COURSE_NAME  AS COURSE_NAME", "(CT.UA_NO = " + FacultyUANo + " OR  CT.ADTEACHER = " + FacultyUANo + ") AND CT.SEMESTERNO  = " + SemesterNo + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND  CT.SUBID = " + Convert.ToInt32(ddlCourseType.SelectedValue) + " AND ISNULL(CT.CANCEL, 0) = 0 ", "COURSE_NAME");
            
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_SECTION S WITH (NOLOCK) ON (CT.SECTIONNO = S.SECTIONNO ) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK)  ON CT.SESSIONNO = SM.SESSIONNO", " DISTINCT ISNULL(S.SECTIONNO,0) SECTIONNO", " S.SECTIONNAME", "(CT.UA_NO =" + Convert.ToInt32(ddlFaculty.SelectedValue) + " OR  CT.ADTEACHER =" + Convert.ToInt32(ddlFaculty.SelectedValue) + ") AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + "  AND  CT.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CT.CANCEL, 0) = 0", "SECTIONNO"); 

            //this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlBatch, "ACD_BATCH A WITH (NOLOCK) INNER JOIN ACD_SECTION B WITH (NOLOCK) ON (A.SECTIONNO = B.SECTIONNO)", "DISTINCT (A.BATCHNO)", "A.BATCHNAME", "B.SECTIONNO > 0 AND A.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1 ", "A.BATCHNO");
            this.objCommon.FillDropDownList(ddlTutorialBatch, "ACD_BATCH A WITH (NOLOCK) INNER JOIN ACD_SECTION B WITH (NOLOCK) ON (A.SECTIONNO = B.SECTIONNO)", "DISTINCT (A.BATCHNO)", "A.BATCHNAME", "B.SECTIONNO > 0 AND A.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1 ", "A.BATCHNO");
        }
    }
    #endregion

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        CourseController objCC = new CourseController();
        if (ddlSession.SelectedValue != "" && ddlSession.SelectedValue != "0")
        {
            if (ddlFaculty.SelectedValue != "" && ddlFaculty.SelectedValue != "0")
            {
                if (ddlSemester.SelectedValue != "" && ddlSemester.SelectedValue != "0")
                {
                    if (ddlCourseType.SelectedValue != "" && ddlCourseType.SelectedValue != "0")
                    {
                        if (ddlCourse.SelectedValue != "" && ddlCourse.SelectedValue != "0")
                        {
                            
                            var semesternos = string.Empty;
                            foreach (ListItem items in ddlSemester.Items)
                            {
                                if (items.Selected == true)
                                    semesternos += (items.Value).Split('-')[0] + ',';
                            }
                            semesternos = semesternos.TrimEnd(',');

                            var courseType = ddlCourseType.SelectedValue == "" ? "0" : ddlCourseType.SelectedValue;
                            var courseNo = ddlCourse.SelectedValue == "" ? "0" : ddlCourse.SelectedValue;
                            var sectionNo = ddlSection.SelectedValue == "" ? "0" : ddlSection.SelectedValue;
                            var batchNo = ddlBatch.SelectedValue == "" ? "0" : ddlBatch.SelectedValue;
                            var tut_batchNo = ddlTutorialBatch.SelectedValue == "" ? "0" : ddlTutorialBatch.SelectedValue;

                           ds = objCC.GetStudentRollListAndRosterAllCourseRegistrationDataExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlFaculty.SelectedValue), semesternos, Convert.ToInt32(courseType), Convert.ToInt32(courseNo), Convert.ToInt32(sectionNo), Convert.ToInt32(batchNo), Convert.ToInt32(tut_batchNo));

                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updpnlSection, "Please Select Course!", this.Page);
                            return;
                        }

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpnlSection, "Please Select Course Type!", this.Page);
                        return;
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlSection, "Please Select Semester!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Faculty!", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updpnlSection, "Please Select Session!", this.Page);
            return;
        }


            GridView gv = new GridView();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds;
                gv.DataBind();
                string attachment = "attachment ; filename=FacultyWiseStudentRollListAndRoster" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.HeaderStyle.Font.Bold = true;
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else 
            {
                objCommon.DisplayMessage(this.updpnlSection, "Student Data Not Found!", this.Page);
                return;
            }

      
    }


}
