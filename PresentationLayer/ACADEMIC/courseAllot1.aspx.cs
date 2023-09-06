//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : COURSE TEACHER ALLOTMMENT                                            
// CREATION DATE : 05-JULY-2011                                                          
// CREATED BY    : GAURAV SONI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================


using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Academic_courseAllot : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                if (Session["usertype"].ToString() != "1")
                {
                    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                    if (dec == "1")
                    {
                        deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Session["userno"].ToString());
                        ViewState["DEPTNO"] = deptno;
                    }
                    else
                    {
                        ViewState["DEPTNO"] = "0";
                    }
                }
                else
                {
                    ViewState["DEPTNO"] = "0";
                }
                
                PopulateDropDownList();
                FillTeacher();
                btnPrint.Enabled = false;

                BindListView();
            }
            Session["reportdata"] = null;
        }
        divMsg.InnerHtml = string.Empty;
        //objCommon.ReportPopUp(btnPrint, "pagetitle=UAIMS(Course Allotment Report)&path=~" + "," + "Reports" + "," + "Academic" + "," + "rptCourse_Allotment.rpt&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString() + "," + "@P_SESSIONNO=" + ddlSession.SelectedValue + "," + "@P_SCHEMENO=" + ddlScheme.SelectedValue + "," + "@P_SEMESTERNO=" + ddlSem.SelectedValue, "UAMIS");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
        }
    }

    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Scheme
    //        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME SC,ACD_STUDENT_RESULT SR", "distinct SR.SCHEMENO", "(convert(nvarchar(100), SR.batchno) + ' - ' + SC.SCHEMENAME) AS SCHEMENAME", "SR.SCHEMENO=SC.SCHEMENO AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "SCHEMENAME");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_courseAllot.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSection.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            ddlSem.Enabled = true;
            ddlSem.SelectedIndex = 0;
            txtTot.Text = string.Empty;
            lblStatus.Text = string.Empty;
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
            //lvAdTeacher.DataSource = null;
            //lvAdTeacher.DataBind();
            btnPrint.Enabled = false;
            ddlSem.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCourse()
    {
        try
        {
            ////Course
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "CCODE + ' - ' + COURSE_NAME AS COURSENAME", "OFFERED = 1 AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "COURSENO");
            ////hidden ddl
            //objCommon.FillDropDownList(ddlHidden, "ACD_COURSE", "SUBID", "COURSENO", "OFFERED = 1 AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "COURSENO");

            CourseController objCC = new CourseController();
            DataSet dsCourse = objCC.GetCourseForCourseAllotment(Convert.ToInt32(ddlScheme.SelectedValue));

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

            if (dsCourse.Tables.Count > 0)
            {
                ddlCourse.DataValueField = dsCourse.Tables[0].Columns[0].ColumnName;
                ddlCourse.DataTextField = dsCourse.Tables[0].Columns[1].ColumnName;
                ddlCourse.DataSource = dsCourse;
                ddlCourse.DataBind();

                ddlHidden.Items.Clear();
                ddlHidden.Items.Add(new ListItem("Please Select", "0"));
                ddlHidden.DataTextField = dsCourse.Tables[0].Columns[0].ColumnName;
                ddlHidden.DataValueField = dsCourse.Tables[0].Columns[2].ColumnName;
                ddlHidden.DataSource = dsCourse;
                ddlHidden.DataBind();
            }
            else
            {
                ddlCourse.DataSource = null;
                ddlCourse.DataBind();
                ddlHidden.DataSource = null;
                ddlHidden.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.FillCourse-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            CourseController objCC = new CourseController();
            if (ddlScheme.SelectedIndex > 0)
            {
                DataSet ds = objCC.GetCourseAllotment(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = ds;
                    lvCourse.DataBind();
                    lvCourse.Visible = true;
                    btnPrint.Enabled = true;
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    lvCourse.Visible = false;
                    btnPrint.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlHidden.SelectedIndex = ddlCourse.SelectedIndex;

        //if (ddlSession.SelectedIndex > -1 & ddlScheme.SelectedIndex > -1 & ddlCourse.SelectedIndex > -1)
        //{
        //    StudentController objSC = new StudentController();
        //    txtTot.Text = objSC.GetTotalNoStudents(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue)).ToString();
        //}

        ddlTeacher.SelectedIndex = 0;

        foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
        {
            (dataitem.FindControl("chkIDNo") as CheckBox).Checked = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        Response.Redirect(Request.Url.ToString());
    }

    private void Clear()
    {
        ddlScheme.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
        txtTot.Text = string.Empty;
        lblStatus.Text = string.Empty;
        //lvCourse.DataSource = null;
        //lvCourse.DataBind();
        //lvAdTeacher.DataSource = null;
        //lvAdTeacher.DataBind();
        //lvCourse.Visible = false;
        btnPrint.Enabled = false;
    }

    protected void btnAd_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            CourseController objCC = new CourseController();
            Student_Acd objStudent = new Student_Acd();
           
            foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
            {
                CheckBox chkIDNo = dataitem.FindControl("chkIDNo") as CheckBox;
                if (chkIDNo.Checked == true)
                {
                     //objStudent.AdTeacher += chkIDNo.ToolTip + ",";
                    objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                    objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                    objStudent.Sem = ddlSem.SelectedValue;
                    objStudent.Sectionno  = Convert.ToInt32(ddlSection.SelectedValue);
                    objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                    objStudent.Pract_Theory = Convert.ToInt32(ddlSubjectType.SelectedValue);
                    objStudent.UA_No = Convert.ToInt32(chkIDNo.ToolTip);
                    objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);
                    count = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND UA_NO=" + Convert.ToInt32(chkIDNo.ToolTip)));
                    if (count > 0)
                    {
                        objCommon.DisplayMessage("Course Teacher allotment is already done for " + chkIDNo.Text, this.Page);
                    }
                    else
                    {
                        objCC.AddCourseAllot(objStudent);
                    }
                }
            }
            objCommon.DisplayMessage( "Course Allotted Successfully.", this.Page);
            BindListView();
            foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
            {
                (dataitem.FindControl("chkIDNo") as CheckBox).Checked = false;
            }
            //if(objStudent.AdTeacher.Length <= 0 )
                //objStudent.AdTeacher += objStudent.UA_No.ToString() + "," ;

            //if (objCC.AddCourseAllot(objStudent) == 1)
            //{
            //    lblStatus.Text = "Course Successfully Alloted to Teacher";
            //    ddlSubjectType.SelectedIndex = 0;
            //    ddlCourse.SelectedIndex = 0;
            //    ddlCourse.Focus();
            //    BindListView();
            //    //Clear();
            //}
            //else
            //{
            //    lblStatus.Text = "Course Not Alloted to Teacher";
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            //Fill Department
            objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");
            ddlDeptName.SelectedValue = deptno;
            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillTeacher()
    {
        //try
        //{
        //    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        //    SqlParameter[] objParams = new SqlParameter[0];
        //    DataTable dtTeacher = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES", objParams).Tables[0];

        //    lvAdTeacher.DataSource = dtTeacher;
        //    lvAdTeacher.DataBind();

            ////DropDownList
            //ddlTeacher.Items.Clear();
            //ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            //ddlTeacher.DataSource = dtTeacher;
            //ddlTeacher.DataTextField = dtTeacher.Columns["UA_FULLNAME"].ToString();
            //ddlTeacher.DataValueField = dtTeacher.Columns["UA_NO"].ToString();
            //ddlTeacher.DataBind();           
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_courseAllot.FillTeacher-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
        try
        {
            //Populating Faculty dropdownlist
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddlDeptName.SelectedValue));

            DataTable dtFaculty = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
            if (dtFaculty.Rows.Count > 0)
            {
                lvAdTeacher.DataSource = dtFaculty;
                lvAdTeacher.DataBind();
            }
            else
            {
                lvAdTeacher.DataSource = null;
                lvAdTeacher.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.FillTeacher-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnDel = sender as ImageButton;

        CourseController objCC = new CourseController();
        Student_Acd objSA = new Student_Acd();
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objSA.UA_No = Convert.ToInt32(btnDel.AlternateText);
        objSA.CourseNo = Convert.ToInt32(btnDel.CommandArgument);
        objSA.Th_Pr = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfSubId") as HiddenField).Value);

        if (Convert.ToInt16(objCC.DeleteCourseAllot(objSA)) == Convert.ToInt16(CustomStatus.RecordUpdated))
        {
            lblStatus.Text = "Course Teacher Deleted Successfully...";
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlSubjectType.Focus();
            BindListView();
        }
        else
            lblStatus.Text = "Can Not Delete Course because Mark Entry has been done!";
    }

    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
            //SqlParameter[] objParams = new SqlParameter[0];
            //DataTable dtTeacher = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES", objParams).Tables[0];

            //DataView dv = new DataView(dtTeacher, "UA_NO <> " + ddlTeacher.SelectedValue, string.Empty, DataViewRowState.CurrentRows);

            ////Listview
            //lvAdTeacher.DataSource = dv;
            //lvAdTeacher.DataBind();

            //foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
            //{
            //    (dataitem.FindControl("chkIDNo") as CheckBox).Checked = false;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlTeacher_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetAdTeachers(object obj)
    { 
        DataTableReader dtr = objCommon.FillDropDown("USER_ACC","UA_FULLNAME","UA_NO","UA_NO IN (" + obj.ToString()  +")","UA_FULLNAME").CreateDataReader();
        string teachers = string.Empty;
        while (dtr.Read())
        {
            teachers += dtr["UA_FULLNAME"].ToString() + ", ";
        }
        dtr.Close();

        return teachers;
    }
   
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "4")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO =" + ddlDegree.SelectedValue, "LONGNAME");
                }
                else if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO =" + ddlDegree.SelectedValue + " AND DEPTNO=" + ViewState["DEPTNO"].ToString(), "LONGNAME");
                }
                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSem.SelectedValue + "AND SR.SECTIONNO =" + ddlSection.SelectedValue + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
                ddlCourse.Focus();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlSubjectType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSubjectType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedValue == "99")
                //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON S.SECTIONNO = SC.SECTIONNO", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSem.SelectedValue + " AND SC.SECTIONNO > 0", "SECTIONNAME");
                objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
            else
                //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_SECTION SC ON S.SECTIONNO = SC.SECTIONNO", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.BRANCHNO =" + ddlBranch.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue + " AND SEMESTERNO =" + ddlSem.SelectedValue, "SECTIONNAME");
                objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SC.SECTIONNAME");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue + " AND C.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");

            BindListView();
            btnPrint.Enabled = true;
            ddlSection.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            //lvAdTeacher.DataSource = null;
            //lvAdTeacher.DataBind();
            ddlSection.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;
            //lvAdTeacher.DataSource = null;
            //lvAdTeacher.DataBind();
            ddlSubjectType.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSection_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTeacher();
        //try
        //{
        //    //Populating Faculty dropdownlist
        //    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        //    SqlParameter[] objParams = new SqlParameter[1];
        //    objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddlDeptName.SelectedValue));

        //    DataTable dtFaculty = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
        //    if (dtFaculty.Rows.Count > 0)
        //    {
        //        lvAdTeacher.DataSource = dtFaculty;
        //        lvAdTeacher.DataBind();
        //    }
        //    else
        //    {
        //        lvAdTeacher.DataSource = null;
        //        lvAdTeacher.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_assignFacultyAdvisor.ddlDeptName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport(rdoReportType.SelectedValue, "rptCourse_Allotment1.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseAllotment.btnPrint_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "_" + ddlSection.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_DEGREENAME=" + ddlDegree.SelectedItem.Text + ",@P_BRANCHNAME=" + ddlBranch.SelectedItem.Text + ",@P_SEMESTER=" + ddlSem.SelectedItem.Text;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updPnl, this.updPnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
