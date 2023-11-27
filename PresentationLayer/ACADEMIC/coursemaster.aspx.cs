//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE CREATION                                                 
// CREATION DATE : 21-May-2009
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
// MODIFIED DATE : 10-NOV-2010
// MODIFIED BY   : MANGESH MOHATKAR
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

public partial class Administration_courseMaster : System.Web.UI.Page
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                string College_code = objCommon.LookUp("REFF", "College_code", "OrganizationId = '" + Session["OrgId"].ToString() + "'");
                ViewState["college_id"] = College_code;

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                string UserTypes = objCommon.LookUp("ACD_MODULE_CONFIG", "USER_TYPES_COURSE_CREATION", Session["usertype"].ToString() + "IN (select value from dbo.split(USER_TYPES_COURSE_CREATION,','))");
                if (UserTypes == "")
                {
                    objCommon.DisplayMessage(this.Page, "You are not authorized to view this page. Please Contact to Admin.", this.Page);
                    divMain.Visible = false;
                    return;
                }



                //Populate the DropDownList 
                PopulateDropDown();
                btnCheckListReport.Visible = false;
                lvCourseMaterial.DataSource = null;
                lvCourseMaterial.DataBind();
                ViewState["action"] = "add";
                //GetMarks();
                trbtn.Visible = false;
                chkGlobal.Enabled = true;
            }
        }
        divMsg.InnerHtml = string.Empty;
        ViewState["ipaddress"] = Request.ServerVariables["REMOTE_ADDR"];
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    #endregion

    #region Other Events
    // bind department on degree selection
    protected void ddlDegree_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "D.DEPTNAME");
                else
                    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "D.DEPTNAME");
            }
            else
            {
                ddlDept.SelectedIndex = 0;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    // bind Branch on department selection
    protected void ddlDept_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            rtpScheme.DataBind();
            lblStatus.Text = string.Empty;
            if (ddlDept.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "CB.BRANCHNO> 0 AND  CB.DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "AND CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            }
            else
            {
                ddlBranch.SelectedIndex = 0;
                ddlScheme.SelectedIndex = 0;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    // bind scheme on branch selection
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Course objc = new Course();
        if (ddlBranch.SelectedValue != "0")
        {
            //GetMarks1();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEPTNO=" + Convert.ToInt32(ddlDept.SelectedValue) + "  AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), string.Empty);
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO  <= (SELECT max(DURATION * 2 )FROM ACD_COLLEGE_DEGREE_BRANCH WHERE BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ")", "SEMESTERNO");
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER ", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO  <= (SELECT (DURATION * 2 )FROM ACD_COLLEGE_DEGREE_BRANCH WHERE DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ") ", "SEMESTERNO");
        }
        else
        {
            rtpScheme.DataSource = null;
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            rtpScheme.DataBind();
        }
    }

    // bind Scheme
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        // bind Exam Name
        GetExamName();
        //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO=C.SEMESTERNO) ", "distinct S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND S.SEMESTERNO  <= (SELECT max(DURATION * 2 ) FROM ACD_COLLEGE_DEGREE_BRANCH WHERE BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ") ", "S.SEMESTERNO");
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO  <= (SELECT max(DURATION * 2 )FROM ACD_COLLEGE_DEGREE_BRANCH WHERE BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ")", "SEMESTERNO");
    }

    // bind Existing courses
    private void FillDropDownCourse()
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SCHEMENO", Convert.ToInt32(ddlScheme.SelectedValue));
                objParams[1] = new SqlParameter("@P_SEMESTERNO", Convert.ToInt32(ddlSem.SelectedValue));

                DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_COURSES", objParams);

                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlExtCourse.DataSource = ds;
                    ddlExtCourse.DataTextField = "COURSENAME";
                    ddlExtCourse.DataValueField = "COURSENO";
                    ddlExtCourse.DataBind();

                    ddlExtCourse.Enabled = true;

                    lblStatus.Text = "Ready to Add New Course";
                    ViewState["action"] = "add";
                    ddlExtCourse.Focus();
                }
                else
                {
                    ddlExtCourse.Enabled = true;
                    ViewState["action"] = "add";
                }

                //bind course list
                // this.BindCourseList( Convert .ToInt32(ddlScheme.SelectedValue), "0",Convert .ToInt32(ddlSem.SelectedValue));

            }
            else
            {
                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));

                ddlScheme.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // bind Exam Name
    private void GetExamName()
    {
        try
        {
            if (ddlDept.SelectedIndex > 0)
            {
                SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                string[] ex1 = ddlScheme.SelectedValue.Split('-');
                int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(Patternno,0)", "schemeno='" + ddlScheme.SelectedValue + "'"));
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_PATTERNNO", patternno);
                DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_EXAM", objParams);
                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rtpScheme.DataSource = ds;
                    rtpScheme.DataBind();

                }
                else
                {
                    ddlExtCourse.Enabled = true;
                    ViewState["action"] = "add";
                }

                //bind course list
                //      this.BindCourseList(Convert.ToInt32(ex1[0]), "0");

            }
            else
            {
                ddlExtCourse.Items.Clear();
                ddlExtCourse.Items.Add(new ListItem("Please Select", "0"));

                ddlDept.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // bind Courses in repeater  
    private void BindCourseList(int schemeno, string coursesno, int semesterno)
    {
        DataSet ds = null;
        try
        {
            CourseController objCC = new CourseController();
            ds = objCC.GetAllCourse(schemeno, coursesno, semesterno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label 
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
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

    // check box click on listview
    protected void lvScheme_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = e.Item as ListViewDataItem;
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("edit"))
            {
                //Check and Select
                CheckBox cb = item.FindControl("cbRow") as CheckBox;
                cb.Checked = true;
            }
        }
    }

    //for courses checking
    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //ListViewDataItem item = e.Item as ListViewDataItem;

        //Label lbl = item.FindControl("lblCNO") as Label;
        //if (lbl != null)
        //{
        //    CheckBox cb = item.FindControl("cbRow") as CheckBox;
        //    if (lbl.Text != string.Empty)
        //        cb.Checked = true;
        //    else
        //        cb.Checked = false;
        //}
    }
    #endregion

    #region Click Events
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlExtCourse.SelectedIndex = 0;
        ddlExtCourse.SelectedIndex = 0;
        // ddlSpecialisation.Enabled = false;
        ddlElectiveGroup.SelectedIndex = 0;
        ddlCElectiveGroup.SelectedIndex = 0;
        btnCancel1.Visible = true;
        btnUpdate.Visible = true;
        lblMsg.Text = string.Empty;
        lblStatus.Text = string.Empty;
        txtCCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        txtLectures.Text = string.Empty;
        txtTheory.Text = string.Empty;
        txtTutorial.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtPract.Text = string.Empty;
        chkElective.Checked = false;
        chkGlobal.Checked = false;
        txtCourseName.Enabled = true;
        txtCCode.Enabled = true;
        ddlTP.Enabled = true;
        ClearControls();
        DataSet ds = null;
        lvCourse.DataSource = ds;
        lvCourse.DataBind();
        lvCourseMaterial.DataSource = null;
        lvCourseMaterial.DataBind();
        GetExamName();
        rtpScheme.DataSource = null;
        rtpScheme.DataBind();
        ViewState["action"] = null;

    }

    // Modification of previous courses 
    protected void btnModifyCourse_Click(object sender, EventArgs e)
    {
        try
        {
            string path = MapPath("~/CourseMaterial/");
            lvCourseMaterial.DataSource = null;
            lvCourseMaterial.DataBind();
            btnUpdate.Visible = false;
            btnCancel1.Visible = false;
            ddlElectiveGroup.Enabled = true;
            trbtn.Visible = true;

            if (ddlExtCourse.SelectedIndex == 0)
            {
                lblStatus.Text = "Please Select Existing Course";
            }
            else
            {
                ViewState["action"] = "edit";

                lblStatus.Text = "Ready to Edit Existing Course";

                //Retrieve the Existing Course Details
                string[] schno = ddlScheme.SelectedValue.Split('-');
                this.ShowDetails(Convert.ToInt32(ddlExtCourse.SelectedValue), Convert.ToInt32(schno[0]));
                //txtCCode.Enabled = false;
                //txtCourseName.Enabled = false;
                // ddlTP.Enabled = false;
                //getFiles
                string[] array1 = null;
                if (Directory.Exists(path))
                {
                    array1 = Directory.GetFiles(path);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Courseno");
                    dt.Columns.Add("Filename");
                    string courseno = string.Empty;
                    if (ViewState["action"].ToString() == "edit")
                    {
                        courseno = ddlExtCourse.SelectedValue;
                    }
                    else
                    {
                        courseno = objCommon.LookUp("ACD_COURSE", "ISNULL(MAX(COURSENO),0)", "COURSENO <> 0");
                        courseno = courseno + 1;
                    }
                    foreach (string str in array1)
                        if (str.Contains(path + courseno))
                        {
                            // dt.Rows.Add(new Object[] { 0, courseno});
                            dt.Rows.Add(new Object[] { courseno, str.ToString().Remove(str.IndexOf(path), path.Length) });
                        }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lvCourseMaterial.DataSource = dt;
                        lvCourseMaterial.DataBind();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    // Stored All details
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlDept.SelectedIndex == 0 && ddlDegree.SelectedIndex == 0 && ddlScheme.SelectedIndex == 0 || ViewState["action"] == null)
        {
            lblMsg.Text = "Please Select Proper Data for Course Creation/Modification";
            return;
        }

        if (chkElective.Checked == true && ddlElectiveGroup.SelectedIndex <= 0)
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Elective Group!!", this.Page);
            return;
        }
        try
        {
            CourseController objCourse = new CourseController();
            Course objc = new Course();
            objc.CourseName = txtCourseName.Text.Replace("'", "").Trim();
            objc.CourseShortName = txtCourseshortname.Text.Replace("'", "").Trim();
            objc.CCode = txtCCode.Text.Replace("'", "").Trim();
            if (!txtLectures.Text.Trim().Equals(string.Empty)) objc.Lecture = Convert.ToDecimal(txtLectures.Text.Trim());
            if (!txtPract.Text.Trim().Equals(string.Empty)) objc.Practical = Convert.ToDecimal(txtPract.Text.Trim());
            if (!txtTutorial.Text.Trim().Equals(string.Empty)) objc.Theory = Convert.ToDecimal(txtTutorial.Text.Trim());
            if (!txtTheory.Text.Trim().Equals(string.Empty)) objc.Credits = Convert.ToDecimal(txtTheory.Text.Trim());

            //if (!txtScaling.Text.Trim().Equals(string.Empty))
            //{
            //    objc.Scaling = Convert.ToInt32(txtScaling.Text.Trim());
            //}
            //else
            //{
            //    objc.Scaling = 0;
            //}

            objc.Elect = (chkElective.Checked == true ? 1 : 0);
            objc.GlobalEle = (chkGlobal.Checked == true ? 1 : 0);
            objc.ValueAdded = (ChkValueAdded.Checked == true ? 1 : 0);
            objc.Specialisation = (ChkSpecialization.Checked == true ? 1 : 0);
            objc.IsAudit = (chkAudit.Checked == true ? 1 : 0);

            objc.IsFeedback = (ChkIsFeedBack.Checked == true ? 1 : 0);
            objc.CGroupno = Convert.ToInt32(ddlCElectiveGroup.SelectedValue);
            objc.Groupno = Convert.ToInt32(ddlElectiveGroup.SelectedValue);
            objc.SubID = Convert.ToInt32(ddlTP.SelectedValue);
            objc.CollegeCode = Session["colcode"].ToString();
            objc.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objc.Deptno = Convert.ToInt32(ddlParentDept.SelectedValue);
            //objc.MinGrade = Convert.ToInt32(DDLMinGrade.SelectedValue);
            //objc.Grade = Convert.ToInt32(DDLGrade.SelectedValue);
            objc.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            // objc.Specialisation = 0;
            objc.Paper_hrs = Convert.ToInt32(txtPaper.Text.ToString() == "" ? "0" : txtPaper.Text.ToString());
            ////if (ddlSubcategory.SelectedIndex == 0)
            ////{
            ////    objc.Categoryno = 0;
            ////}
            ////else
            ////{
            ////    objc.Categoryno = Convert.ToInt32(ddlSubcategory.SelectedValue);
            ////}
            objc.Categoryno = 0;//****************
            foreach (RepeaterItem item in rtpScheme.Items)
            {
                TextBox txtMinMarks = item.FindControl("txtMinMarks") as TextBox;
                TextBox txtMaxMarks = item.FindControl("txtMaxMarks") as TextBox;
                Label lblFldName = item.FindControl("lblFldName") as Label;

                Label lblExamName = item.FindControl("lblExamName") as Label;

                switch (lblFldName.Text.ToString())
                {
                    case ("S1"):
                        objc.S1Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S1Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S2"):
                        objc.S2Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S2Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S3"):
                        objc.S3Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S3Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S4"):
                        objc.S4Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S4Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S5"):
                        objc.S5Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S5Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S6"):
                        objc.S6Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S6Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S7"):
                        objc.S7Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S7Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S8"):
                        objc.S8Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S8Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S9"):
                        objc.S9Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S9Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("S10"):
                        objc.S10Max = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.S10Min = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("EXTERMARK"):
                        objc.ExtermarkMax = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.ExtermarkMin = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                }
                /********************** AS per Discussion with Umesh sir & Ankhush sir Added by Dileep kare on 25.03.2022 ******************/
                switch (lblExamName.Text.ToString())
                {
                    case ("INTERNAL"):
                        objc.InterMarkMin = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        objc.MaxMarks_I = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        break;
                    case ("EXTERNAL"):
                        objc.ExtermarkMax = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        objc.ExtermarkMin = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        break;
                    case ("OVERALL TOTAL"):
                        objc.MinTotalMarks = txtMinMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMinMarks.Text.ToString());
                        objc.Total_Marks = txtMaxMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtMaxMarks.Text.ToString());
                        break;
                }
                //objc.Total_Marks = txtCourseTotalMarks.Text.ToString() == "" ? 0.0m : Convert.ToDecimal(txtCourseTotalMarks.Text.ToString());
                /************************************************************* END ******************************************************/

            }
            //if (!txtPreCredit.Text.Trim().Equals(string.Empty))
            //    objc.Prerequisite_cr = Convert.ToInt32(txtPreCredit.Text);
            //else
            //    objc.Prerequisite_cr = 0;

            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chk = dataitem.FindControl("cbRow") as CheckBox;
                if (chk.Checked == true)
                {
                    if (objc.Prerequisite != string.Empty) objc.Prerequisite += ",";
                    objc.Prerequisite += chk.ToolTip;
                }
            }
            objc.OrgId = Convert.ToInt32(Session["OrgId"]);//Added by Dileep Kare on 11.03.2022
            //Delete Scheme No
            string[] sno = ddlScheme.SelectedValue.Split('-');
            string delschno = string.Empty;
            string insschno = string.Empty;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    //Edit Course
                    objc.SchemeNo = Convert.ToInt32(sno[0]);
                    objc.CourseNo = Convert.ToInt32(ddlExtCourse.SelectedValue);
                    objc.SchNo = insschno;
                    objc.DelSchNo = delschno;
                    int uano = Convert.ToInt32(Session["userno"].ToString());

                    CustomStatus cs = (CustomStatus)objCourse.UpdateCourse(objc, ViewState["ipaddress"].ToString(), uano);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.UPDCOURSE, "Course Modified Successfully!!", this.Page);
                        this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), "0", Convert.ToInt32(ddlSem.SelectedValue));
                        ClearControls();
                    }
                    else
                        objCommon.DisplayMessage(this.UPDCOURSE, "Error!!", this.Page);
                }
                else
                {
                    //Add New Course
                    objc.SchNo = sno[0] + "," + insschno;

                    //To ckeck Course Code in current selected Scheme
                    int cnt = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COUNT(*)", "CCODE='" + txtCCode.Text + "' AND SCHEMENO=" + sno[0]));

                    if (cnt >= 1)
                    {
                        objCommon.DisplayMessage(this.UPDCOURSE, "Course with same Course Code. Already Exist!!", this.Page);
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objCourse.AddCourse(objc);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UPDCOURSE, "Course Added Successfully!!", this.Page);
                        FillDropDownCourse();
                        ClearControls();
                        this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), "0", Convert.ToInt32(ddlSem.SelectedValue));
                    }
                    else
                        objCommon.DisplayMessage(this.UPDCOURSE, "Error!!", this.Page);
                }
            }

        }
        catch
        {
            throw;
        }
    }

    // delete Courses
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        lvCourseMaterial.DataSource = null;
        lvCourseMaterial.DataBind();
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            string filename = btnDelete.ToolTip;
            string path = MapPath("~/CourseMaterial/");
            string courseno = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("CourseNo");
            dt.Columns.Add("Filename");
            if (File.Exists(path + filename))
            {
                File.Delete(path + filename);
                objCommon.DisplayMessage(pnl_course, "File Deleted Successfully!", this);
                string[] array1 = Directory.GetFiles(path);
                courseno = filename.Substring(0, filename.IndexOf(" - "));
                foreach (string str in array1)
                    if (str.Contains(path + courseno))
                        dt.Rows.Add(new Object[] { courseno, str.ToString().Remove(str.IndexOf(path), path.Length) });
                if (dt != null && dt.Rows.Count > 0)
                {
                    lvCourseMaterial.DataSource = dt;
                    lvCourseMaterial.DataBind();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "File  Not Found!", this);
                return;
            }
        }
        catch
        {
            throw;
        }
    }

    // Modify Marks
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ViewState["action"] == null)
        {
            CourseController objCourse = new CourseController();
            Course objc = new Course();
            foreach (RepeaterItem item in rtpScheme.Items)
            {
                TextBox txtMinMarks = item.FindControl("txtMinMarks") as TextBox;
                TextBox txtMaxMarks = item.FindControl("txtMaxMarks") as TextBox;
                Label lblFldName = item.FindControl("lblFldName") as Label;

                switch (lblFldName.Text.ToString())
                {
                    case ("S1"):
                        objc.S1Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S1Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S2"):
                        objc.S2Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S2Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S3"):
                        objc.S3Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S3Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S4"):
                        objc.S4Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S4Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S5"):
                        objc.S5Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S5Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;

                    case ("EXTERMARK"):
                        objc.ExtermarkMax = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.ExtermarkMin = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;

                    case ("S6"):
                        objc.S6Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S6Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S7"):
                        objc.S7Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S7Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S8"):
                        objc.S8Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S8Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S9"):
                        objc.S9Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S9Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                    case ("S10"):
                        objc.S10Max = Convert.ToInt16(txtMaxMarks.Text.ToString());
                        objc.S10Min = Convert.ToInt16(txtMinMarks.Text.ToString());
                        break;
                }
            }
            objc.OrgId = Convert.ToInt32(Session["OrgId"]);//Added by Dileep Kare on 11.03.2022

            int ret = objCourse.UpdateExamMarks(objc);
            if (ret == Convert.ToInt16(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Marks Updated SuccessFully...!", this);
                GetExamName();
            }
            else
                objCommon.DisplayMessage(this.UPDCOURSE, "Error!", this);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        rtpScheme.DataSource = null;
        rtpScheme.DataBind();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string filename = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
        string ContentType = string.Empty;

        //To Get the physical Path of the file(test.txt)
        string filepath = Server.MapPath("~/CourseMaterial/");

        // Create New instance of FileInfo class to get the properties of the file being downloaded
        FileInfo myfile = new FileInfo(filepath + filename);

        // Checking if file exists
        if (myfile.Exists)
        {
            // Clear the content of the response
            Response.ClearContent();

            // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header
            Response.AddHeader("Content-Disposition", "attachment; filename=" + myfile.Name);

            // Add the file size into the response header
            Response.AddHeader("Content-Length", myfile.Length.ToString());

            // Set the ContentType
            Response.ContentType = ReturnExtension(myfile.Extension.ToLower());

            // Write the file into the  response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
            Response.TransmitFile(myfile.FullName);

            // End the response
            Response.End();
        }

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "tab2();", true);
        DataTable dt = new DataTable();
        int courseno = 0;
        string path = MapPath("~/CourseMaterial/");

        dt.Columns.Add("Courseno");
        dt.Columns.Add("Filename");

        if (ViewState["action"].ToString() == "edit")
            courseno = Convert.ToInt16(ddlExtCourse.SelectedValue);
        else
        {
            courseno = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "ISNULL(MAX(COURSENO),0)", "COURSENO <> 0"));
            courseno = courseno + 1;
        }
        try
        {
            if (!(Directory.Exists(path)))
                Directory.CreateDirectory(path);

            if (refMaterial.HasFile)
            {
                string[] array1 = Directory.GetFiles(path);

                foreach (string str in array1)
                {
                    if ((path + courseno.ToString() + " - " + refMaterial.FileName.ToString()).Equals(str))
                    {
                        objCommon.DisplayMessage(pnl_course, "File Already Exists!", this);
                        return;
                    }
                    if (str.Contains(path + courseno.ToString()))
                    {
                        dt.Rows.Add(new Object[] { courseno, str.ToString().Remove(str.IndexOf(path), path.Length) });
                    }
                }
                refMaterial.SaveAs(MapPath("~/CourseMaterial/" + courseno.ToString() + " - " + refMaterial.FileName));
                dt.Rows.Add(new Object[] { courseno, courseno.ToString() + " - " + refMaterial.FileName.ToString() });
                objCommon.DisplayMessage(pnl_course, "File Uploaded SuccessFully...!", this);
                lvCourseMaterial.DataSource = dt;
                lvCourseMaterial.DataBind();

            }

            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Select File to Upload!", this);
                return;
            }

        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblMsg.Text = string.Empty;
    }
    #endregion

    #region User Methods

    private void BindSchemeListView()
    {
        try
        {
            char[] sep = { ',' };
            string[] semno = ViewState["sem_no"].ToString().Split(sep);
            //string stype = ddlScheme.SelectedItem.ToString().Substring(0, Convert.ToInt32(ddlScheme.SelectedItem.ToString().IndexOf("-")) - 1);
            string[] schno = ddlScheme.SelectedValue.Split('-');

            DataSet ds = null;
            CourseController objCC = new CourseController();

            if (ViewState["action"] == null)
            {
                ds = objCC.GetGElecScheme(txtCCode.Text.Trim(), ddlDegree.SelectedValue, Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(semno[Convert.ToInt32(ddlScheme.SelectedIndex) - 1]), Convert.ToInt32(schno[1]));
            }
            else
            {
                if (ViewState["action"].ToString().Equals("edit"))
                    ds = objCC.GetSchemeNoByCCode((ddlExtCourse.SelectedItem.Text.Substring(0, Convert.ToInt32(ddlExtCourse.SelectedItem.Text.IndexOf("-")) - 1)), ddlDegree.SelectedValue, Convert.ToInt32(semno[Convert.ToInt32(ddlScheme.SelectedIndex) - 1]), Convert.ToInt32(schno[1]));
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                //lvScheme.DataSource = ds;
                //lvScheme.DataBind();
            }

        }
        catch
        {
            throw;
        }
    }

    private void ClearControls()
    {
        ViewState["action"] = null;
        lvCourseMaterial.DataSource = null;
        lvCourseMaterial.DataBind();
        lblStatus.Text = string.Empty;
        txtPaper.Text = string.Empty;
        txtCCode.Text = string.Empty;
        txtCourseName.Text = string.Empty;
        txtCourseshortname.Text = string.Empty;
        txtLectures.Text = string.Empty;
        txtTutorial.Text = string.Empty;
        txtTotal.Text = string.Empty;
        txtPract.Text = string.Empty;
        ddlExtCourse.SelectedIndex = 0;
        ddlTP.SelectedIndex = 0;
        ddlParentDept.SelectedIndex = 0;
        ////ddlSubcategory.SelectedIndex = 0;
        DataSet ds = null;
        lvCourse.DataSource = ds;
        lvCourse.DataBind();
        //  pnlPreCorList.Visible = false;
        rtpScheme.DataSource = null;
        rtpScheme.DataBind();
        btnCancel1.Visible = true;
        btnUpdate.Visible = true;
        txtTheory.Text = string.Empty;
        //txtScaling.Text = string.Empty;
        //txtPreCredit.Text = string.Empty;
        ddlExtCourse.Items.Clear();
        ddlExtCourse.Items.Add("Please Select");
        ddlExtCourse.SelectedItem.Value = "0";
        ddlSemester.SelectedIndex = 0;
        if (ViewState["action"] != "edit")
            ddlSem.SelectedValue = "0";
        chkElective.Checked = false;
        chkGlobal.Checked = false;
        ChkSpecialization.Checked = false;
        chkAudit.Checked = false;                //Added as per ticket no- 47642
        ChkValueAdded.Checked = false;
        ChkIsFeedBack.Checked = false;
        ddlCElectiveGroup.SelectedIndex = 0;
        ddlElectiveGroup.SelectedIndex = 0;
        ddlSpecialisation.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = -1;      //Commented as per discussion with mannoj shanti sir and Umesh Sir ticket no-39018
        //ddlDept.SelectedIndex = -1;        //Commented as per discussion with mannoj shanti sir and Umesh Sir ticket no-39018
        //ddlBranch.SelectedIndex = -1;      //Commented as per discussion with mannoj shanti sir and Umesh Sir ticket no-39018
        //ddlScheme.SelectedIndex = -1;      //Commented as per discussion with mannoj shanti sir and Umesh Sir ticket no-39018
        ddlSem.SelectedIndex = -1;
        ddlExtCourse.SelectedIndex = -1;
        txtCourseTotalMarks.Text = string.Empty;
        trbtn.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        ShowReportnew("Check_List", "rptDegreeWiseCourseList.rpt");

    }

    private void ShowReportnew(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_DEGREENO=" + ddlDegree.SelectedValue
                + ",@P_BRANCHNO=" + ddlBranch.SelectedValue
                + ",@P_SEMESTERNO=" + ddlSem.SelectedValue
                + ",@P_SCHEMENO=" + ddlScheme.SelectedValue;


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UPDCOURSE, this.UPDCOURSE.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_RollListForScrutiny.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetails(int courseno, int schemeno)
    {
        try
        {
            CourseController objCC = new CourseController();
            SqlDataReader dr = objCC.GetCourses(courseno, schemeno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    //Course Details
                    txtCourseName.Text = dr["COURSE_NAME"] == DBNull.Value ? string.Empty : dr["COURSE_NAME"].ToString();
                    txtCourseshortname.Text = dr["SHORTNAME"] == DBNull.Value ? string.Empty : dr["SHORTNAME"].ToString();
                    txtLectures.Text = dr["LECTURE"] == DBNull.Value ? "0" : dr["LECTURE"].ToString();
                    txtTutorial.Text = dr["THEORY"] == DBNull.Value ? "0" : dr["THEORY"].ToString();
                    txtPract.Text = dr["PRACTICAL"] == DBNull.Value ? "0" : dr["PRACTICAL"].ToString();
                    txtTheory.Text = dr["CREDITS"] == DBNull.Value ? "0" : dr["CREDITS"].ToString();
                    txtCCode.Text = dr["CCODE"] == DBNull.Value ? string.Empty : dr["CCODE"].ToString();
                    txtPaper.Text = dr["PAPER_HRS"] == DBNull.Value ? string.Empty : dr["PAPER_HRS"].ToString();
                    txtTotal.Text = (Convert.ToDecimal(txtLectures.Text) + Convert.ToDecimal(txtTutorial.Text) + Convert.ToDecimal(txtPract.Text)).ToString();
                    //  txtScaling.Text = dr["SCALEDN_MARK"] == DBNull.Value ? "0" : dr["SCALEDN_MARK"].ToString();
                    ddlTP.SelectedValue = dr["SUBID"] == DBNull.Value ? "-1" : dr["SUBID"].ToString();
                    ddlParentDept.SelectedValue = dr["BOS_DEPTNO"] == DBNull.Value ? "-1" : dr["BOS_DEPTNO"].ToString();
                    //lvCourseMarks.DataSource = dr;
                    //lvCourseMarks.DataBind();
                    ddlSemester.SelectedValue = dr["SemesterNo"] == DBNull.Value ? "0" : dr["SemesterNo"].ToString();

                    ddlCElectiveGroup.SelectedValue = dr["CATEGORYNO"].ToString() == "" ? "0" : dr["CATEGORYNO"].ToString();
                    ////ddlSubcategory.SelectedValue = dr["CATEGORYNO"].ToString() == "" ? "0" : dr["CATEGORYNO"].ToString();

                    txtCourseTotalMarks.Text = dr["TOTAL_MARK"].ToString() == "" ? "0" : dr["TOTAL_MARK"].ToString();
                    //txtPreCredit.Text = dr["PreRequisite_Credit"].ToString() == "0" ? string.Empty : dr["PreRequisite_Credit"].ToString();

                    if (dr["ELECT"] != DBNull.Value)
                    {
                        chkElective.Checked = Convert.ToBoolean(dr["ELECT"]);
                        ddlElectiveGroup.SelectedValue = dr["GROUPNO"].ToString() == "" ? "0" : dr["GROUPNO"].ToString();
                        chkGlobal.Checked = Convert.ToBoolean(dr["GLOBALELE"]);
                    }

                    ChkValueAdded.Checked = Convert.ToBoolean(dr["ISVALUE_ADDED"]);

                    ChkSpecialization.Checked = Convert.ToBoolean(dr["IS_SPECIAL"]);

                    chkAudit.Checked = Convert.ToBoolean(dr["IS_AUDIT"]);

                    ChkIsFeedBack.Checked = Convert.ToBoolean(dr["IS_FEEFBACK"]);
                    //prerequisite
                    // string[] schno = ddlScheme.SelectedValue.Split('-');
                    // string[] semno = ddlSem.SelectedValue.Split('-');
                    if (dr["PreRequisite"].ToString() == string.Empty)
                        this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), "0", Convert.ToInt32(ddlSem.SelectedValue));
                    else
                        this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), dr["PreRequisite"].ToString(), Convert.ToInt32(ddlSem.SelectedValue));
                    #region Comment
                    //Pre-Defined Marks
                    //txtEndSem.Text = dr["MAXMARKS_E"] == null ? string.Empty : dr["MAXMARKS_E"].ToString();
                    //txtEndSemMin.Text = dr["MINMARKS"] == null ? string.Empty : dr["MINMARKS"].ToString();
                    //txtS1Max.Text = dr["S1MAX"] == null ? string.Empty : dr["S1MAX"].ToString();
                    //txtS2Max.Text = dr["S2MAX"] == null ? string.Empty : dr["S2MAX"].ToString();
                    //txtS3Max.Text = dr["S3MAX"] == null ? string.Empty : dr["S3MAX"].ToString();
                    //txtS4Max.Text = dr["S4MAX"] == null ? string.Empty : dr["S4MAX"].ToString();
                    //txtS5Max.Text = dr["S5MAX"] == null ? string.Empty : dr["S5MAX"].ToString();
                    //txtS6Max.Text = dr["S6MAX"] == null ? string.Empty : dr["S6MAX"].ToString();
                    //txtS7Max.Text = dr["S7MAX"] == null ? string.Empty : dr["S7MAX"].ToString();
                    //txtS8Max.Text = dr["S8MAX"] == null ? string.Empty : dr["S8MAX"].ToString();
                    //txtS9Max.Text = dr["S9MAX"] == null ? string.Empty : dr["S9MAX"].ToString();
                    //txtS10Max.Text = dr["S10MAX"] == null ? string.Empty : dr["S10MAX"].ToString();

                    //txtS1Min.Text = dr["S1MIN"] == null ? string.Empty : dr["S1MIN"].ToString();
                    //txtS2Min.Text = dr["S2MIN"] == null ? string.Empty : dr["S2MIN"].ToString();
                    //txtS3Min.Text = dr["S3MIN"] == null ? string.Empty : dr["S3MIN"].ToString();
                    //txtS4Min.Text = dr["S4MIN"] == null ? string.Empty : dr["S4MIN"].ToString();
                    //txtS5Min.Text = dr["S5MIN"] == null ? string.Empty : dr["S5MIN"].ToString();
                    //txtS6Min.Text = dr["S6MIN"] == null ? string.Empty : dr["S6MIN"].ToString();
                    //txtS7Min.Text = dr["S7MIN"] == null ? string.Empty : dr["S7MIN"].ToString();
                    //txtS8Min.Text = dr["S8MIN"] == null ? string.Empty : dr["S8MIN"].ToString();
                    //txtS9Min.Text = dr["S9MIN"] == null ? string.Empty : dr["S9MIN"].ToString();
                    //txtS10Min.Text = dr["S10MIN"] == null ? string.Empty : dr["S10MIN"].ToString();

                    //txtTotMinMarks.Text = dr["MINMARKS"] == null ? string.Empty : dr["MINMARKS"].ToString();

                    //DDLGrade.SelectedValue = dr["GRADE"] == null ? "0" : dr["GRADE"].ToString();
                    // DDLMinGrade.SelectedValue = dr["MINGRADE"] == null ? "0" : dr["MINGRADE"].ToString();
                    //ddlSpecialisation.SelectedValue = dr["SPECIALISATIONNO"] == null ? "0" : dr["SPECIALISATIONNO"].ToString();
                    #endregion
                }
                dr.Close();

                int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(PATTERNNO,0)", "schemeno='" + schemeno + "'"));
                DataSet ds = objCC.GetCoursesMarks(courseno, patternno);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rtpScheme.DataSource = ds;
                        rtpScheme.DataBind();
                    }
                    else
                    {
                        rtpScheme.DataSource = null;
                        rtpScheme.DataBind();
                    }
                }
            }
        }
        catch
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
                Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=coursemaster.aspx");
        }
    }

    //Load data on page Load
    private void PopulateDropDown()
    {
        try
        {
            //fill degree name
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "CD.DEGREENO=D.DEGREENO AND D.DEGREENO>0  AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                //if (dec == "0")
                //{
                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEPTNO=" + deptno,"");
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_BRANCH B ON D.DEGREENO=B.DEGREENO ", "distinct(D.DEGREENO)", "DEGREENAME", "DEPTNO=" + deptno, "DEGREENO");
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "D.DEGREENO");
                //}
            }
            else
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0", "D.DEGREENO");
            }

            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlTP, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1", "SUBID");
            //objCommon.FillDropDownList(ddlCElectiveGroup, "ACD_GROUP", "GROUPNO", "GROUPNAME", "GROUPNO > 0", "GROUPNO");  // commented by S.Patil
            objCommon.FillDropDownList(ddlCElectiveGroup, "ACD_COURSE_CATEGORY", "CATEGORYNO", "CATEGORYNAME", "CATEGORYNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "CATEGORYNO");
            objCommon.FillDropDownList(ddlElectiveGroup, "ACD_ELECTGROUP", "GROUPNO", "GROUPNAME" + " +' - Any '+ " + "cast(CHOICEFOR as varchar)  GROUPNAME", "GROUPNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "GROUPNO");

            //objCommon.FillDropDownList(ddlSpecialisation, "ACD_COURSE_SPECIALISATION", "SPECIALISATIONNO", "SPECIALISATIONNAME", "SPECIALISATIONNO > 0", "SPECIALISATIONNO");
            ////objCommon.FillDropDownList(ddlSubcategory, "ACD_COURSE_CATEGORY", "CATEGORYNO", "CATEGORYNAME", "CATEGORYNO > 0", "CATEGORYNO");//****************
            //fill exam name
            //objCommon.FillDropDownList(ddlDept, "ACD_EXAM_PATTERN", "PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0", "PATTERNNO");
            //FILL PARENT DEPARTMENT LIST
            //objCommon.FillDropDownList(ddlParentDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO <>0 AND DEPTNO IS NOT NULL ", "DEPTNAME");
            if (Session["usertype"].ToString() != "1")
                //objCommon.FillDropDownList(ddlParentDept, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", " DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO>0 AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "D.DEPTNAME");  //Commented as per discussion with mannoj shanti sir and Umesh Sir ticket no-39018
                objCommon.FillDropDownList(ddlParentDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO <>0 AND ISNULL(ACTIVESTATUS,0)=1AND DEPTNO IS NOT NULL ", "DEPTNAME");
            else
                objCommon.FillDropDownList(ddlParentDept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO <>0 AND ISNULL(ACTIVESTATUS,0)=1AND DEPTNO IS NOT NULL ", "DEPTNAME");
        }
        catch
        {
            throw;
        }
    }

    private string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
            case ".cs":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    public string GetFileNamePath(string filename)
    {
        string path = MapPath("~/CourseMaterial/");
        if (filename != null && filename.ToString() != "")
            return path.ToString() + filename.ToString().Replace("%2520", " ");
        else
            return "";
    }

    protected void btnCheckListReport_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue != "0")
        {
            int chkExit = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME SS WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME EN WITH (NOLOCK) ON(SS.PATTERNNO=EN.PATTERNNO)", "COUNT(*)", "SS.SCHEMENO=" + ddlScheme.SelectedValue + "")); //Added Mahesh on Dated 09-02-2020 due to Report Required Exam name(Pattern) without Exam Name showing . 
            if (chkExit > 0)
            {
                string[] sno = ddlScheme.SelectedValue.Split('-');
                ShowReport("Check_List", "rptSubjectCourseListSchemewise.rpt", 2, Convert.ToInt32(sno[0]));
            }
            else
            {
                objCommon.DisplayMessage(this.UPDCOURSE, "Exam pattern not found for selected scheme, Please check exam creation & pattern.!!!", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Please Select Scheme", this.Page);
            return;
        }

    }

    private void ShowReport(string reportTitle, string rptFileName, int type, int schemeno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + schemeno.ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UPDCOURSE, this.UPDCOURSE.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch
        {
            throw;
        }
    }

    #endregion

    private void BindListView()
    {
        try
        {
            SchemeController objSC = new SchemeController();
            DataSet ds = objSC.GetScheme(Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
            rtpScheme.DataSource = ds;
            rtpScheme.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExtCourse.Enabled = true;
        // bind  Existing Courses
        FillDropDownCourse();

        this.BindCourseList(Convert.ToInt32(ddlScheme.SelectedValue), "0", Convert.ToInt32(ddlSem.SelectedValue));

        // visible Existing Courses dropdown
        ddlExtCourse.Visible = true;
        // trbtn.Visible = true;
        ddlSemester.SelectedValue = ddlSem.SelectedValue;
        if (ViewState["action"] == "edit")
        {
            ClearControls();
        }
    }

    protected void chkElective_CheckedChanged(object sender, EventArgs e)
    {
        if (chkElective.Checked == true)
        {
            div5.Visible = true;
            ddlElectiveGroup.Enabled = true;
        }
        else
        {
            div5.Visible = false;
            ddlElectiveGroup.Enabled = false;
        }

    }


    protected void txtCCode_TextChanged(object sender, EventArgs e)
    {
        //string CCODE = objCommon.LookUp("ACD_COURSE", "COUNT(*)", "CCODE='" + txtCCode.Text + "' ");
        string CCODE = objCommon.LookUp("ACD_COURSE", "COUNT(*)", "CCODE='" + txtCCode.Text + "' AND SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "");
        if (Convert.ToInt32(CCODE) > 0)
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Course Code  Already Exist!!", this.Page);
            txtCCode.Text = string.Empty;
            txtCCode.Focus();
            txtCourseName.Text = string.Empty;
            return;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = objCC.RetrieveCourseMasterDataForExcel();

        ds.Tables[0].TableName = "Course Data Format";
        ds.Tables[1].TableName = "Subject Master";
        ds.Tables[2].TableName = "Semester Master";
        ds.Tables[3].TableName = "Scheme Master";
        ds.Tables[4].TableName = "BOS_Department Master";
        ds.Tables[5].TableName = "Elective Group";

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0 && ds.Tables[5].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadCourseData_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayMessage(this.UPDCOURSE, "Please Define All Masters!!", this.Page);
        }
    }
    protected void btnUploadexcel_Click(object sender, EventArgs e)
    {
        try
        {
            Uploaddata();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.btnSubjectwise_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void Uploaddata()
    {
        try
        {
            if (FUFile.HasFile)
            {
                string FileName = Path.GetFileName(FUFile.PostedFile.FileName);
                string Extension = Path.GetExtension(FUFile.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FUFile.SaveAs(FilePath);
                    ExcelToDatabase(FilePath, Extension, "yes");
                }
                else
                {
                    objCommon.DisplayMessage(updpnlImportData, "Only .xls or .xlsx extention is allowed", this.Page);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnlImportData, "Please select the Excel File to Upload", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EnquiryGeneration.Uploaddata()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    {
        int drawing = 0;
        CourseController objCC = new CourseController();
        Course objC = new Course();
        try
        {
            CustomStatus cs = new CustomStatus();
            string conStr = "";

            switch (Extension)
            {
                //case ".xls": //Excel 97-03
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                //case ".xlsx": //Excel 07
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            //Bind Excel to GridView
            DataSet ds = new DataSet();
            oda.Fill(ds);

            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "isnull(COURSENAME,'')<>''";
            DataTable dtNew = dv1.ToTable();

            lvStudData.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
            lvStudData.DataBind();
            int i = 0;

            for (i = 0; i < dtNew.Rows.Count; i++)
            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //foreach (DataRow dr in dt.Rows)
            {

                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                object Name = row[0];
                if (Name != null && !String.IsNullOrEmpty(Name.ToString().Trim()))
                {
                    //string city = string.Empty;
                    //string district = string.Empty;
                    //string state = string.Empty;
                    //string leadSource = string.Empty;
                    //string leadCollectedby = string.Empty;


                    if (!(dtNew.Rows[i]["COURSENAME"]).ToString().Equals(string.Empty))
                    {
                        objC.CourseName = (dtNew.Rows[i]["COURSENAME"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Course Name at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (!(dtNew.Rows[i]["SHORTNAME"]).ToString().Equals(string.Empty))
                    {
                        objC.CourseShortName = (dtNew.Rows[i]["SHORTNAME"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter CourseShortName at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (!(dtNew.Rows[i]["COURSECODE"]).ToString().Equals(string.Empty))
                    {
                        objC.CCode = (dtNew.Rows[i]["COURSECODE"]).ToString();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Course Code at Row no. " + (i + 1), this.Page);
                        return;
                    }


                    if (!(dtNew.Rows[i]["CREDITS"]).ToString().Equals(string.Empty))
                    {
                        objC.Credits = Convert.ToDecimal((dtNew.Rows[i]["CREDITS"]).ToString());
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please enter Credits at Row no. " + (i + 1), this.Page);
                        return;
                    }

                    if (dtNew.Rows[i]["ISELECTIVE"].Equals("YES") || dtNew.Rows[i]["ISELECTIVE"].Equals("Yes") || dtNew.Rows[i]["ISELECTIVE"].Equals("yes"))
                    {
                        objC.Elect = 1;
                        if (dtNew.Rows[i]["ELECTIVEGROUP"].ToString().Equals(string.Empty))
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Please enter Elective Group at Row no. " + (i + 1), this.Page);
                            return;
                        }
                    }
                    else if (dtNew.Rows[i]["ISELECTIVE"].Equals("NO") || dtNew.Rows[i]["ISELECTIVE"].Equals("No") || dtNew.Rows[i]["ISELECTIVE"].Equals("no") || dtNew.Rows[i]["ISELECTIVE"].Equals("nO"))
                    {
                        objC.Elect = 0;
                    }

                    //else
                    //{
                    //    objCommon.DisplayMessage(updpnlImportData, "Please enter Elective at Row no. " + (i + 1), this.Page);
                    //    return;
                    //}



                    if (dtNew.Rows[i]["SUBJECTTYPE"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please Enter Subject Type at Row no. " + (i + 1), this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }
                    else
                    {
                        string subid = objCommon.LookUp("ACD_SUBJECTTYPE", "COUNT(1)", "SUBNAME='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'");

                        if (Convert.ToInt32(subid) > 0)
                        {
                            objC.SubID = (objCommon.LookUp("ACD_SUBJECTTYPE", "SUBID", "SUBNAME ='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_SUBJECTTYPE", "SUBID", "SUBNAME ='" + dtNew.Rows[i]["SUBJECTTYPE"].ToString() + "'"));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Subject Type not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }

                    }

                    if (dtNew.Rows[i]["SEMESTER"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please Enter Semester at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string semesterno = objCommon.LookUp("acd_semester", "COUNT(1)", "SEMESTERNAME='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'");

                        if (Convert.ToInt32(semesterno) > 0)
                        {
                            objC.SemesterNo = Convert.ToInt32((objCommon.LookUp("acd_semester", "SemesterNo", "SEMESTERNAME ='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("acd_semester", "SemesterNo", "SEMESTERNAME ='" + dtNew.Rows[i]["SEMESTER"].ToString() + "'")));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Semester not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }
                    }
                    if (dtNew.Rows[i]["SCHEME"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please Enter Scheme at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string schemeno = objCommon.LookUp("acd_scheme", "COUNT(1)", "SCHEMENAME='" + dtNew.Rows[i]["SCHEME"].ToString() + "'");

                        if (Convert.ToInt32(schemeno) > 0)
                        {
                            objC.SchemeNo = Convert.ToInt32((objCommon.LookUp("acd_scheme", "SchemeNo", "SCHEMENAME ='" + dtNew.Rows[i]["SCHEME"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("acd_scheme", "SchemeNo", "SCHEMENAME ='" + dtNew.Rows[i]["SCHEME"].ToString() + "'")));

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Scheme not found in ERP Master at Row no. " + (i + 1), this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }

                    }

                    if (dtNew.Rows[i]["BOS_DEPT"].ToString() == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Please Enter BOS_DEPT at Row no. " + (i + 1), this.Page);
                        return;
                    }
                    else
                    {
                        string deptno = objCommon.LookUp("ACD_DEPARTMENT", "COUNT(1)", "DEPTNAME='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'");

                        if (Convert.ToInt32(deptno) > 0)
                        {
                            objC.Deptno = Convert.ToInt32((objCommon.LookUp("ACD_DEPARTMENT", "Deptno", "DEPTNAME ='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "Deptno", "DEPTNAME ='" + dtNew.Rows[i]["BOS_DEPT"].ToString() + "'")));

                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlImportData, "BOS_DEPT not found in ERP Master at Row no. " + (i + 1), this.Page);

                            return;
                        }

                    }

                    if (dtNew.Rows[i]["ISELECTIVE"].Equals("YES") || dtNew.Rows[i]["ISELECTIVE"].Equals("Yes") || dtNew.Rows[i]["ISELECTIVE"].Equals("yes"))
                    {
                        string electivegrpno = objCommon.LookUp("ACD_ELECTGROUP", "COUNT(1)", "GROUPNAME='" + dtNew.Rows[i]["ELECTIVEGROUP"].ToString() + "'");

                        if (Convert.ToInt32(electivegrpno) > 0)
                        {
                            objC.Electivegrpno = Convert.ToInt32((objCommon.LookUp("ACD_ELECTGROUP", "GROUPNO", "GROUPNAME ='" + dtNew.Rows[i]["ELECTIVEGROUP"].ToString() + "'")) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_ELECTGROUP", "GROUPNO", "GROUPNAME ='" + dtNew.Rows[i]["ELECTIVEGROUP"].ToString() + "'")));
                        }
                        else
                        {
                            objCommon.DisplayMessage(updpnlImportData, "ELECTIVEGROUP not found in ERP Master at Row no. " + (i + 1), this.Page);

                            return;
                        }
                    }
                    else
                    {
                        objC.Electivegrpno = 0;
                    }


                    if (!(dtNew.Rows[i]["MINMARK_I"]).ToString().Equals(string.Empty)) // Min Marks Internal
                    {
                        objC.InterMarkMin = Convert.ToDecimal(dtNew.Rows[i]["MINMARK_I"]);
                    }
                    else
                    {
                        objC.InterMarkMin = 0;
                    }


                    if (!(dtNew.Rows[i]["MAXMARKS_I"]).ToString().Equals(string.Empty)) // Max Marks Internal
                    {
                        objC.MaxMarks_I = Convert.ToDecimal(dtNew.Rows[i]["MAXMARKS_I"]);
                    }
                    else
                    {
                        objC.MaxMarks_I = 0;
                    }

                    if (!(dtNew.Rows[i]["MINMARK_E"]).ToString().Equals(string.Empty)) // Min Marks External
                    {
                        objC.ExtermarkMin = Convert.ToDecimal(dtNew.Rows[i]["MINMARK_E"]);
                    }
                    else
                    {
                        objC.ExtermarkMin = 0;
                    }

                    if (!(dtNew.Rows[i]["MAXMARK_E"]).ToString().Equals(string.Empty)) // Max Marks External
                    {
                        objC.ExtermarkMax = Convert.ToDecimal(dtNew.Rows[i]["MAXMARK_E"]);
                    }
                    else
                    {
                        objC.ExtermarkMax = 0;
                    }

                    if (!(dtNew.Rows[i]["MIN_TOTAL_MARKS"]).ToString().Equals(string.Empty)) // Min Total Marks
                    {
                        objC.MinTotalMarks = Convert.ToDecimal(dtNew.Rows[i]["MIN_TOTAL_MARKS"]);
                    }
                    else
                    {
                        objC.MinTotalMarks = 0;
                    }

                    if (!(dtNew.Rows[i]["TOTAL_MARK"]).ToString().Equals(string.Empty)) // Total Marks
                    {
                        objC.Total_Marks = Convert.ToDecimal(dtNew.Rows[i]["TOTAL_MARK"]);
                    }
                    else
                    {
                        objC.Total_Marks = 0;

                    }

                    if (dtNew.Rows[i]["ISVALUE_ADDED"].Equals("YES") || dtNew.Rows[i]["ISVALUE_ADDED"].Equals("Yes") || dtNew.Rows[i]["ISVALUE_ADDED"].Equals("yes"))
                    {
                        objC.ValueAdded = 1;
                    }
                    else
                    {
                        objC.ValueAdded = 0;
                    }

                    if (dtNew.Rows[i]["IS_GLOBAL_ELECTIVE"].Equals("YES") || dtNew.Rows[i]["IS_GLOBAL_ELECTIVE"].Equals("Yes") || dtNew.Rows[i]["IS_GLOBAL_ELECTIVE"].Equals("yes"))
                    {
                        objC.GlobalEle = 1;
                    }
                    else
                    {
                        objC.GlobalEle = 0;
                    }


                    if (dtNew.Rows[i]["IS_SPECIALZATION"].Equals("YES") || dtNew.Rows[i]["IS_SPECIALZATION"].Equals("Yes") || dtNew.Rows[i]["IS_SPECIALZATION"].Equals("yes"))
                    {
                        objC.Specialisation = 1;
                    }
                    else
                    {
                        objC.Specialisation = 0;
                    }

                    if (dtNew.Rows[i]["IS_CONSIDER_FOR_FEEDBACK"].Equals("YES") || dtNew.Rows[i]["IS_CONSIDER_FOR_FEEDBACK"].Equals("Yes") || dtNew.Rows[i]["IS_CONSIDER_FOR_FEEDBACK"].Equals("yes"))
                    {
                        objC.IsFeedback = 1;
                    }
                    else
                    {
                        objC.IsFeedback = 0;
                    }

                    if (dtNew.Rows[i]["IS_AUDIT"].Equals("YES") || dtNew.Rows[i]["IS_AUDIT"].Equals("Yes") || dtNew.Rows[i]["IS_AUDIT"].Equals("yes"))
                    {
                        objC.IsAudit = 1;
                    }
                    else
                    {
                        objC.IsAudit = 0;
                    }


                    if (!(dtNew.Rows[i]["LECTURE"]).ToString().Equals(string.Empty))
                    {
                        objC.Lecture = Convert.ToDecimal(dtNew.Rows[i]["LECTURE"]);
                    }
                    else
                    {
                        objC.Lecture = 0;
                    }

                    if (!(dtNew.Rows[i]["TUTORIAL"]).ToString().Equals(string.Empty))
                    {
                        objC.Theory = Convert.ToDecimal(dtNew.Rows[i]["TUTORIAL"]);
                    }
                    else
                    {
                        objC.Theory = 0;
                    }

                    if (!(dtNew.Rows[i]["PRACTICAL"]).ToString().Equals(string.Empty))
                    {
                        objC.Practical = Convert.ToDecimal(dtNew.Rows[i]["PRACTICAL"]);
                    }
                    else
                    {
                        objC.Practical = 0;
                    }



                    if (!(dtNew.Rows[i]["DRAWING"]).ToString().Equals(string.Empty))
                    {
                        drawing = Convert.ToInt32(dtNew.Rows[i]["DRAWING"]);
                    }
                    else
                    {
                        drawing = 0;
                    }


                    if (!(dtNew.Rows[i]["PAPER_HRS"]).ToString().Equals(string.Empty))
                    {
                        objC.Paper_hrs = Convert.ToInt32(dtNew.Rows[i]["PAPER_HRS"]);
                    }
                    else
                    {
                        objC.Paper_hrs = 0;
                    }

                    //objC.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                    cs = (CustomStatus)objCC.SaveExcelSheetCourseDataInDataBase(objC, drawing);
                    connExcel.Close();
                }

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                // BindListView();
                objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Uploaded Successfully!!", this.Page);
            }
            else
            {
                //BindListView();
                objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Updated Successfully!!", this.Page);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnlImportData, "Data not available in ERP Master", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);

                return;
            }
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnreportnew_Click(object sender, EventArgs e)
    {
        ExcelReport();
    }
    private void ExcelReport()
    {
        try
        {
            int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            int Deptno = ddlDept.SelectedIndex > 0 ? Convert.ToInt32(ddlDept.SelectedValue) : 0;
            int Schemeno = ddlScheme.SelectedIndex > 0 ? Convert.ToInt32(ddlScheme.SelectedValue) : 0;
            int Semesterno = ddlSem.SelectedIndex > 0 ? Convert.ToInt32(ddlSem.SelectedValue) : 0;
            CourseController objCC = new CourseController();
            DataSet ds = objCC.AllCourseDetailsforExcel(Degreeno, Branchno, Deptno, Schemeno, Semesterno);

            ds.Tables[0].TableName = "AllCourseDetails";
            if (ds.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        wb.Worksheets.Add(dt);
                    }

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=All_Couse_Details_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnlImportData, "Record Not Found.", this.Page);
            }
        }
        catch
        {

        }
    }

    protected void ddlExtCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;

        bool status = true;
        try
        {
            CourseController objCC = new CourseController();
            ds = objCC.GetDetailsOfLockUnlockStatusofCourse(Convert.ToInt32(ddlExtCourse.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                status = Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].ToString());
                if (status == false)
                {
                    ModifyCourse.Enabled = true;
                }
                else
                {
                    ModifyCourse.Enabled = false;
                }
            }
            else
            {
                ModifyCourse.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_courseMaster.ddlExtCourse_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
