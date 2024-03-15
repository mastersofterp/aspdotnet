//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : BATCH ALLOTMENT                                  
// CREATION DATE : 
// ADDED BY      : ASHISH DHAKATE                                                  
// ADDED DATE    : 03-Feb-2012
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
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
using System.Collections.Generic;
using System.Text.RegularExpressions;



using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_BatchAllotment : System.Web.UI.Page
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
                //if (Request.QueryString["pageno"] != null)
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                PopulateDropDownList();
                trFilter.Visible = false;
                trRollNo.Visible = false;
                trRdo.Visible = false;
                ddlBatch.Enabled = false;
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
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
                Response.Redirect("~/notauthorized.aspx?page=batchallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=batchallotment.aspx");
        }
    }

    #region Form Events

    //protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    //}

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_COURSE WITH (NOLOCK)", "COURSENO", "CCODE + ' - ' + COURSE_NAME", "OFFERED = 1 AND SCHEMENO = " + ddlBranch.SelectedValue, "CCODE");
            lblStatus2.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindListView();

        if (lvStudents.Items.Count > 0)
        {
            divlvStudentHeading.Visible = true;
        }
        else
        {
            divlvStudentHeading.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student_Acd objStudent = new Student_Acd();
            int i = 0;
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                    objStudent.StudId += chkBox.ToolTip + ",";
                else
                    i++;
            }

            if (i == lvStudents.Items.Count)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Please select at least one student from the student list.", this.Page);
                return;
            }

            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objStudent.DegreeNo = Convert.ToInt32(ViewState["degreeno"].ToString());
            objStudent.BranchNo = Convert.ToInt32(ViewState["branchno"].ToString());
            objStudent.SchemeNo = Convert.ToInt32(ViewState["schemeno"].ToString());
            objStudent.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
            objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            //AS THERE IS NO SUBID IN STUDENT_ACD, SO WE ARE USING THBATCHNO AS SUBID
            objStudent.ThBatchNo = Convert.ToInt32(ddlSubjectType.SelectedValue);
            // objStudent.ThBatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
            objStudent.BatchNo = Convert.ToInt32(ddlBatch.SelectedValue);
            if (ddlAttfor.SelectedItem.Text == "Tutorial")
            {
                objStudent.Pract_Theory = 3;
            }
            else if (ddlAttfor.SelectedItem.Text == "Practical")
            {
                objStudent.Pract_Theory = 2;
            }
            else
            {
                objStudent.Pract_Theory = 2;
            }
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            if (objSC.UpdateStudent_BatchAllot(objStudent, OrgId) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                //Clear();
                objCommon.DisplayMessage(this.UpdatePanel1, "Batch Allotted Successfully.", this.Page);
                BindListView();
            }
            else
                objCommon.DisplayMessage(this.UpdatePanel1, "Server Error...", this.Page);
        }
        catch
        {
            throw;
        }
    }


    #endregion

    #region Private Methods

    private void BindListView()
    {
        try
        {
            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            int branchNo = 0;
            if (ddlBranch.SelectedValue == "99")
                branchNo = 0;
            else
                branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            StudentController objSC = new StudentController();
            //DataSet ds = objSC.GetStudentsForBatchAllotment(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));
            DataSet ds = objSC.GetStudentsForBatchAllotment(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"].ToString()), Convert.ToInt32(ViewState["degreeno"].ToString()), Convert.ToInt32(ViewState["branchno"].ToString()), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), OrgId);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
                objCommon.FillDropDownList(ddlBatch, "ACD_BATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", " ACTIVESTATUS=1 and SECTIONNO = " + ddlSection.SelectedValue, "BATCHNO");
                ddlBatch.Enabled = true;
                txtEnrollFrom.Enabled = true;
                txtEnrollTo.Enabled = true;
                DataSet dssubtype = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID=S.SUBID)", "ISNULL(THEORY,0) AS THEORY,TH_PR,ISNULL(SEC_BATCH,0) AS SEC_BATCH,ISNULL(ISTUTORIAL,0) AS ISTUTORIAL", "", "C.COURSENO=" + ddlCourse.SelectedValue, "");
                if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "1" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
                {
                    ddlAttfor.Items.Clear();
                    List<ListItem> items = new List<ListItem>();
                    items.Add(new ListItem("Tutorial", "1"));
                    ddlAttfor.DataSource = items;
                    ddlAttfor.DataBind();
                    ddlAttfor.SelectedIndex = 0;
                }
                else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0")
                {
                    ddlAttfor.Items.Clear();
                    List<ListItem> items = new List<ListItem>();
                    items.Add(new ListItem("Practical", "2"));
                    ddlAttfor.DataSource = items;
                    ddlAttfor.DataBind();
                    ddlAttfor.SelectedIndex = 0;
                }
                if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "2" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
                {
                    if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                    {
                        ddlAttfor.Items.Clear();
                        List<ListItem> items = new List<ListItem>();
                        items.Add(new ListItem("Tutorial", "1"));
                        items.Add(new ListItem("Practical", "2"));
                        ddlAttfor.DataSource = items;
                        ddlAttfor.DataBind();
                        ddlAttfor.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlAttfor.Items.Clear();
                        List<ListItem> items = new List<ListItem>();
                        items.Add(new ListItem("Practical", "2"));
                        ddlAttfor.DataSource = items;
                        ddlAttfor.DataBind();
                        ddlAttfor.SelectedIndex = 0;
                    }
                }
                if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "0")
                {
                    ddlAttfor.Items.Clear();
                    List<ListItem> items = new List<ListItem>();
                    ddlAttfor.Items.Add(new ListItem("Practical", "2"));
                    ddlAttfor.DataSource = items;
                    ddlAttfor.DataBind();
                    ddlAttfor.SelectedIndex = 0;
                }
                else if (dssubtype.Tables[0].Rows[0]["SEC_BATCH"].ToString() == "3" && dssubtype.Tables[0].Rows[0]["ISTUTORIAL"].ToString() == "1")
                {
                    if (Convert.ToInt32(dssubtype.Tables[0].Rows[0]["THEORY"]) > 0)
                    {
                        ddlAttfor.Items.Clear();
                        List<ListItem> items = new List<ListItem>();
                        items.Add(new ListItem("Tutorial", "1"));
                        items.Add(new ListItem("Practical", "2"));
                        ddlAttfor.DataSource = items;
                        ddlAttfor.DataBind();
                        ddlAttfor.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlAttfor.Items.Clear();
                        List<ListItem> items = new List<ListItem>();
                        items.Add(new ListItem("Practical", "2"));
                        ddlAttfor.DataSource = items;
                        ddlAttfor.DataBind();
                        ddlAttfor.SelectedIndex = 0;
                    }
                }
                //if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 1)
                //{
                //    ddlAttfor.Items.Clear();

                //    List<ListItem> items = new List<ListItem>();
                //    items.Add(new ListItem("Tutorial", "1"));
                //    ddlAttfor.DataSource = items;
                //    ddlAttfor.DataBind();

                //    ddlAttfor.SelectedIndex = 0;
                //}
                //else
                //    if (Convert.ToInt32(ddlSubjectType.SelectedValue) == 2)
                //    {
                //        ddlAttfor.Items.Clear();
                //        List<ListItem> items = new List<ListItem>();
                //        items.Add(new ListItem("Practical", "2"));
                //        ddlAttfor.DataSource = items;
                //        ddlAttfor.DataBind();

                //        ddlAttfor.SelectedIndex = 0;
                //    }
                //    else
                //    {
                //        ddlAttfor.Items.Clear();
                //        List<ListItem> items = new List<ListItem>();
                //        items.Add(new ListItem("Practical", "2"));
                //        ddlAttfor.DataSource = items;
                //        ddlAttfor.DataBind();

                //        ddlAttfor.SelectedIndex = 0;
                //    }
                dvAttFor.Visible = true;
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                ddlBatch.Enabled = false;
                dvAttFor.Visible = false;
                txtEnrollFrom.Enabled = false;
                txtEnrollTo.Enabled = false;
                objCommon.DisplayMessage(this.UpdatePanel1, "Student Not Found!", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    private void Clear()
    {
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        txtFromRollNo.Text = string.Empty;
        txtToRollNo.Text = string.Empty;
        ddlBatch.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lblStatus2.Text = string.Empty;
    }

    private void PopulateDropDownList()
    {
        try
        {

            // ddlSession.SelectedIndex = 1;
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
                objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 AND DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "DEPTNAME ASC");
            }
            else
            {
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                objCommon.FillListBox(ddlDepartment, "ACD_DEPARTMENT A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEPT B WITH (NOLOCK) ON (A.DEPTNO=B.DEPTNO)", "DISTINCT A.DEPTNO", "A.DEPTNAME", "A.DEPTNO>0", "A.DEPTNAME ASC");
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            }
        }
        catch
        {
            throw;
        }
    }

    private void ClearDropDown(DropDownList ddlList)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlClgname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                }
            }
            ddlSemester.Focus();
            ClearDropDown(ddlCourse);
            ClearDropDown(ddlSection);
            ClearDropDown(ddlBatch);
            ClearDropDown(ddlSession);
            divlvStudentHeading.Visible = false;

            lvStudents.DataSource = null;
            lvStudents.DataBind();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO >0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");
            ddlSession.Focus();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSession_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        ddlCollege.SelectedIndex = 0;
        ClearDropDown(ddlDegree);
        ClearDropDown(ddlBranch);
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);

        ddlBatch.Enabled = false;
        ddlBatch.SelectedIndex = 0;
        dvAttFor.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;

        //Commented by As per Requirement Of Romal Saluja Sir dl
        //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ViewState["schemeno"].ToString(), "SR.SEMESTERNO");

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
        if (ds != null && ds.Tables.Count > 0)
        {
            ddlSemester.DataSource = ds;
            ddlSemester.DataTextField = "SEMESTERNAME";
            ddlSemester.DataValueField = "SEMESTERNO";
            ddlSemester.DataBind();
        }

        //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.PREV_STATUS = 0", "SR.SEMESTERNO");
        ddlSemester.Focus();
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

            //if (dec == "1")
            //{
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEPTNO=" + deptno,"");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_BRANCH B ON D.DEGREENO=B.DEGREENO ", "distinct(D.DEGREENO)", "DEGREENAME", "DEPTNO=" + deptno, "DEGREENO");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
            //}
        }
        else
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
        }
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO = D.DEGREENO)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", ", "D.DEGREENO");
        ddlDegree.Focus();

        ClearDropDown(ddlBranch);
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);

        ddlBatch.Enabled = false;
        ddlBatch.SelectedIndex = 0;
        dvAttFor.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;
    }



    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            else
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            ddlBranch.Focus();
        }
        else
        {
            //ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }

        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);

        ddlBatch.Enabled = false;
        dvAttFor.Visible = false;
        ddlBatch.SelectedIndex = 0;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() == "1")
            {

                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO = " + ddlDegree.SelectedValue, "SCHEMENO");
                ddlScheme.Focus();
            }
            else
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO=" + Session["userdeptno"].ToString(), "SCHEMENO");
                ddlScheme.Focus();
            }
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlBranch.SelectedIndex = 0;
        }

        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);

        ddlBatch.Enabled = false;
        ddlBatch.SelectedIndex = 0;
        dvAttFor.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;
    }

    protected void ddlScheme_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {

            }
            else
            {
                ddlSemester.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }

            ClearDropDown(ddlSubjectType);
            ClearDropDown(ddlCourse);
            ClearDropDown(ddlSection);

            ddlBatch.Enabled = false;
            ddlBatch.SelectedIndex = 0;
            dvAttFor.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            divlvStudentHeading.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_SCHEME M WITH (NOLOCK) ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND S.SUBID>0 and (S.SEC_BATCH >1 or ISTUTORIAL=1)", "C.SUBID");
            //ddlSubjectType.SelectedIndex = 1; //Comment by dileep kare on 13.01.2022 as per client requirement.
            ddlSubjectType.Focus();
        }
        else
        {
            ddlSubjectType.Items.Clear();
            ddlSemester.SelectedIndex = 0;
        }

        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);

        ddlBatch.Enabled = false;
        ddlBatch.SelectedIndex = 0;
        dvAttFor.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;
        //ddlSection.SelectedIndex = 0;
        //ddlSubjectType.SelectedIndex = 0;chkAccept
        //ddlCourse.SelectedIndex = 0;
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            if (ddlSubjectType.SelectedValue != "1" || (ddlSubjectType.SelectedValue == "1" && Session["OrgId"].ToString() != "1"))
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SCHEMENO = C.SCHEMENO)", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue, "COURSE_NAME");
                ddlCourse.Focus();
            }
            else
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON (SR.COURSENO = C.COURSENO AND SR.SCHEMENO = C.SCHEMENO)", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(C.THEORY,0)>0", "COURSE_NAME");
                ddlCourse.Focus();
            }
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlScheme.SelectedIndex = 0;
        }

        ClearDropDown(ddlSection);

        ddlAttfor.Items.Clear();
        List<ListItem> items = new List<ListItem>();
        items.Add(new ListItem("Please Select", "0"));
        ddlAttfor.DataSource = items;
        ddlAttfor.DataBind();

        ddlAttfor.SelectedIndex = 0;

        ddlBatch.Enabled = false;
        ddlBatch.SelectedIndex = 0;
        dvAttFor.Visible = false;

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;
        //ddlSection.SelectedIndex = 0;
        //ddlCourse.SelectedIndex = 0;
    }

    //protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    //{
    //    if (ddlSemester.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue, "CCODE");
    //        ddlCourse.Focus();
    //    }
    //    else
    //    {
    //        ddlCourse.Items.Clear();
    //        ddlScheme.SelectedIndex = 0;
    //    }

    //    lvStudents.DataSource = null;
    //    lvStudents.DataBind();
    //    ddlSection.SelectedIndex = 0;
    //    ddlCourse.SelectedIndex = 0;
    //}

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCourse.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN  ACD_STUDENT ST ON SR.IDNO = ST.IDNo INNER JOIN  ACD_SECTION S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND SR.PREV_STATUS = 0", "S.SECTIONNO");
            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN  ACD_STUDENT ST WITH (NOLOCK) ON SR.IDNO = ST.IDNO INNER JOIN  ACD_SECTION S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND SR.PREV_STATUS = 0", "S.SECTIONNO");
            // objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
            ddlSection.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlCourse.SelectedIndex = 0;
        }

        ddlBatch.Enabled = false;
        ddlBatch.SelectedIndex = 0;
        dvAttFor.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBatch.Enabled = false;
        dvAttFor.Visible = false;
        ddlBatch.SelectedIndex = 0;

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divlvStudentHeading.Visible = false;
    }

    #endregion

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                chkBox.Checked = false;
            }
            string EnrollFrom = string.Empty;
            string EnrollTo = string.Empty;
            string checkBox = string.Empty;

            if (lvStudents.Items.Count > 0)
            {
                lvStudents.Visible = true;
            }

            if (txtEnrollFrom.Text == "" || txtEnrollTo.Text == "")
            {
                objCommon.DisplayMessage("Please Enter Range of Roll No. to be Filter", this.Page);
                return;
            }
            if (txtEnrollFrom.Text != null && txtEnrollFrom.Text != ""
                && txtEnrollTo.Text != null && txtEnrollTo.Text != "")
            {
                foreach (ListViewDataItem item in lvStudents.Items)
                {
                    CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                    //Label lblregno = item.FindControl("lblRegno") as Label;
                    EnrollFrom = Regex.Replace(txtEnrollFrom.Text, @"[^\d]", "");
                    EnrollTo = Regex.Replace(txtEnrollTo.Text, @"[^\d]", "");
                    checkBox = Regex.Replace(chkBox.Text, @"[^\d]", "");
                    //lblregno.Text;
                    if (Convert.ToInt64(checkBox) >= Convert.ToInt64(EnrollFrom) && Convert.ToInt64(checkBox) <= Convert.ToInt64(EnrollTo))
                    {
                        chkBox.Checked = true;
                        count++;
                    }
                }
            }
            txtTotStud.Text = count.ToString();
        }
        catch
        {
            throw;
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dept = string.Empty;
        if (Session["usertype"].ToString() != "1")
        {
            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");

            if (ddlDepartment.SelectedIndex > 0)
            {
                foreach (ListItem items in ddlDepartment.Items)
                {
                    if (items.Selected == true)
                    {
                        dept += items.Value + ',';
                    }
                }
                if (!dept.ToString().Equals(string.Empty) || !dept.ToString().Equals(""))
                    dept = dept.Remove(dept.Length - 1);
            }
            else
            {
            }
        }
        else
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        //objCommon.FillDropDownList(ddlDegree, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlSchoolInstitute.SelectedValue + " AND A.DEPTNO=" + ddlDepartment.SelectedValue, "B.DEGREENAME ASC");
        objCommon.FillDropDownList(ddlDegree, "[DBO].[ACD_COLLEGE_DEGREE_BRANCH] A WITH (NOLOCK) INNER JOIN ACD_DEGREE B WITH (NOLOCK) ON(A.DEGREENO=B.DEGREENO)", "DISTINCT A.DEGREENO", "B.DEGREENAME", "B.DEGREENO > 0 AND A.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND A.DEPTNO IN(" + ddlDepartment.SelectedValue + ")", "B.DEGREENAME ASC");
        ClearDropDown(ddlBranch);
        ClearDropDown(ddlScheme);
        ClearDropDown(ddlSemester);
        ClearDropDown(ddlSubjectType);
        ClearDropDown(ddlCourse);
        ClearDropDown(ddlSection);
        ClearDropDown(ddlBatch);
        divlvStudentHeading.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

}
