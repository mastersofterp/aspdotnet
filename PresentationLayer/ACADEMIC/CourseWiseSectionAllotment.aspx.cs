//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COURSE WISE SECTION ALLOTMENT                                                     
// CREATION DATE : 27-DEC-2018                                                          
// CREATED BY    : HEMANTH G  
// DESCRIPTION   : THIS PAGE IS USED TO UPDATE SECTION COURSE WISE SECTION ALLOTMENT STORED IN ACD_STUDENT_RESULT                              
// MODIFIED DATE :                                                          
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ACADEMIC_CourseWiseSectionAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["userdeptno"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDownList();
            }

        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND FLOCK=1", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONID = S.SESSIONID) ", "DISTINCT S.SESSIONID", "S.SESSION_NAME", " ISNULL(S.FLOCK,0)=1 AND ISNULL(S.IS_ACTIVE,0)=1 ", "S.SESSIONID DESC");
            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + "", "DEGREENO");
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO = " + Session["userdeptno"].ToString(), "A.LONGNAME");
                else
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add("Please Select");
                ddlScheme.SelectedItem.Value = "0";
                ddlSem.Items.Clear();
                ddlSem.Items.Add("Please Select");
                ddlSem.SelectedItem.Value = "0";
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedItem.Value = "0";
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
                ddlSem.Items.Clear();
                ddlSem.Items.Add("Please Select");
                ddlSem.SelectedItem.Value = "0";
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedItem.Value = "0";

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlSem.Items.Clear();
                ddlCourse.Items.Clear();
                ddlBranch.SelectedIndex = 0;

            }
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSem, "ACD_COURSE AC INNER JOIN ACD_SEMESTER S ON (AC.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_STUDENT_RESULT SR ON(SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO)", "DISTINCT AC.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "", "AC.SEMESTERNO");//AND SR.PREV_STATUS = 0
            }
            else
            {
                ddlSem.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["schemeno"]) > 0)
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO)", "DISTINCT AC.COURSENO", "AC.CCODE+ ' - ' +AC.COURSE_NAME", "SR.SESSIONNO IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + ") AND ISNULL(AC.ELECT,0)=1 AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0", "AC.COURSENO");
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add("Please Select");
                ddlCourse.SelectedItem.Value = "0";
            }
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            btnSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        MainDivSection.Visible = true;
        this.BindListView();


    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlClgname.SelectedIndex = -1;
        ddlSession.SelectedIndex = -1;
        //ddlColg.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlTutorialBatch.SelectedIndex = 0;
        pnlStudent.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
        DivSection.Visible = false;
        DivBatch.Visible = false;
        DivTutorialBatch.Visible = false;
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = null;
            if (ddlSection.SelectedIndex > 0 && DivSection.Visible == true)
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO) INNER JOIN ACD_STUDENT ST ON(ST.IDNO=SR.IDNO AND ST.SCHEMENO=SR.SCHEMENO) LEFT OUTER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT ST.IDNO,ST.REGNO,ST.STUDNAME,ISNULL(SR.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,SR.SESSIONNO", "SR.SCHEMENO,SR.SEMESTERNO,SR.COURSENO, SR.BATCHNO, SR.TH_BATCHNO, BATCHNAME=(SELECT BATCHNAME FROM ACD_BATCH WHERE BATCHNO=SR.BATCHNO), TH_BATCHNAME = (SELECT BATCHNAME FROM ACD_BATCH WHERE BATCHNO = SR.TH_BATCHNO)", "SR.SESSIONNO = (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + ") AND ST.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ST.DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "AND ST.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + "AND SR.REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0", "ST.REGNO");
            }
            else
            {
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO) INNER JOIN ACD_STUDENT ST ON(ST.IDNO=SR.IDNO AND ST.SCHEMENO=SR.SCHEMENO) LEFT OUTER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO)", "DISTINCT ST.IDNO,ST.REGNO,ST.STUDNAME,ISNULL(SR.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,SR.SESSIONNO", "SR.SCHEMENO,SR.SEMESTERNO,SR.COURSENO, SR.BATCHNO, SR.TH_BATCHNO, BATCHNAME=(SELECT BATCHNAME FROM ACD_BATCH WHERE BATCHNO=SR.BATCHNO), TH_BATCHNAME = (SELECT BATCHNAME FROM ACD_BATCH WHERE BATCHNO = SR.TH_BATCHNO)", "SR.SESSIONNO = (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + ") AND ST.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ST.DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "AND ST.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0", "ST.REGNO");
            }
            // ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_COURSE AC ON(SR.COURSENO=AC.COURSENO AND SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO) INNER JOIN ACD_STUDENT ST ON(ST.IDNO=SR.IDNO AND ST.SCHEMENO=SR.SCHEMENO) LEFT OUTER JOIN ACD_SECTION SC ON (SR.SECTIONNO = SC.SECTIONNO) LEFT OUTER JOIN ACD_BATCH BH ON (SR.SECTIONNO = BH.SECTIONNO)", "DISTINCT ST.IDNO", "ST.REGNO,ST.STUDNAME,ISNULL(SR.SECTIONNO,0)SECTIONNO,SC.SECTIONNAME,SR.SESSIONNO,SR.SCHEMENO,SR.SEMESTERNO,SR.COURSENO,BH.BATCHNAME,SR.BATCHNO, SR.TH_BATCHNO", "SR.SESSIONNO = (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + ") AND ST.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ST.DEGREENO= " + Convert.ToInt32(ViewState["degreeno"]) + "AND ST.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0 AND BH.BATCHNO = SR.BATCHNO", "ST.REGNO");         
            //SR.BATCHNO, BH.BATCHNAME, TH_BATCHNAME = (SELECT BATCHNAME FROM ACD_BATCH WHERE BATCHNO = SR.TH_BATCHNO ), SR.TH_BATCHNO
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlStudent.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                btnSubmit.Enabled = true;
            }
            else
            {
                pnlStudent.Visible = false;
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage(this.updpnlSection, "No Students found for selected criteria!", this.Page);
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlsec = e.Item.FindControl("ddlsec") as DropDownList;
            DataSet ds = objCommon.FillDropDown("ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNAME");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlsec.Items.Add(new ListItem(dtr["SECTIONNAME"].ToString(), dtr["SECTIONNO"].ToString()));
                }
            }
            //if (Convert.ToInt32(ddlsec.ToolTip) == 0)
            //{
            //    ddlsec.Enabled = true;
            //}
            //else
            //{
            //    ddlsec.Enabled = false;
            //}
            ddlsec.SelectedValue = ddlsec.ToolTip;
        }
        catch
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (Convert.ToInt32(ddlSection.SelectedValue) == 0)
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Section", this.Page);
                return;
            }
            if (DivBatch.Visible == true && ddlBatch.SelectedIndex == 0) 
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Batch", this.Page);
                return;
            }
            if (DivTutorialBatch.Visible == true && ddlTutorialBatch.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Tutorial Batch", this.Page);
                return;
            }


            StudentController objSC = new StudentController();
            string studids = string.Empty;
            string sections = string.Empty;
            string rollnos = string.Empty;
            int sessiono = Convert.ToInt32(ddlSession.SelectedValue);
            int college_id = Convert.ToInt32(ViewState["college_id"]);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int branchno = Convert.ToInt32(ViewState["branchno"]);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            int userno = Convert.ToInt32(Session["userno"]);
            int count = 0;
            int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            int batchno = Convert.ToInt32(ddlBatch.SelectedValue);
            int tutbatchno = Convert.ToInt32(ddlTutorialBatch.SelectedValue);
            string batch = string.Empty;
            string tut_batch = string.Empty;
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                   CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                   if (chkBox.Checked == true)
                       count++;
            }

            if (count  == 0)
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Student.", this.Page);
                return;
            }
            if (sessiono > 0 && college_id > 0 && degreeno > 0 & branchno > 0 && schemeno > 0 && semesterno > 0 && courseno > 0)
            {
                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    //if (Convert.ToInt32((lvItem.FindControl("ddlsec") as DropDownList).SelectedValue) > 0 && (lvItem.FindControl("ddlsec") as DropDownList).Enabled == true)
                    //{
                    //    studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                    //    sections += (lvItem.FindControl("ddlsec") as DropDownList).SelectedValue + "$";
                    //}
                    if(Convert.ToInt32(ddlSection.SelectedValue) > 0 && (lvItem.FindControl("cbRow") as CheckBox).Checked==true)
                    {
                        studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                        sections += ddlSection.SelectedValue + "$";

                        if (Convert.ToInt32(ddlBatch.SelectedValue) > 0)
                        {
                            batch += ddlBatch.SelectedValue + "$";
                        }
                        else
                        {
                            batch += "$";
                        }
                        if (Convert.ToInt32(ddlTutorialBatch.SelectedValue) > 0)
                        {
                            tut_batch += ddlTutorialBatch.SelectedValue + "$";
                        }
                        else 
                        {
                            tut_batch += "$";
                        }

                    }
                }
             
                if (studids.Length <= 0 && sections.Length <= 0)
                {
                    objCommon.DisplayMessage(this.updpnlSection, "Please Select Student/Section", this.Page);
                    return;
                }
                if (objSC.UpdateStudentCourseWiseSection(sessiono, college_id, degreeno, branchno, schemeno, semesterno, courseno, studids, sections, userno, batch, tut_batch) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    ddlSection.SelectedIndex = 0;
                    ddlBatch.SelectedIndex = 0;
                    ddlTutorialBatch.SelectedIndex = 0;
                    this.BindListView();
                    objCommon.DisplayMessage(this.updpnlSection, "Student Section Alloted Successfully!!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpnlSection, "Server Error...", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlSection, "Please Select Session/College/Degree/Branch/Scheme/Semester/Course!!", this.Page);
            }
        }
        catch 
        {
            throw;
        }
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        MainDivSection.Visible = false;
        pnlStudent.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
        int ddlCourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
        //DataSet ds = objCommon.FillDropDown("FROM ACD_COURSE C LEFT JOIN ACD_SUBJECTTYPE S ON C.SUBID = S.SUBID", "DISTINCT S.SUBID, S.SUBNAME, S.TH_PR, ISNULL(S.SEC_BATCH,0) AS SECTIONNO, CASE WHEN ISNULL(S.TH_PR,1)=1 THEN 'THEORY' WHEN ISNULL(S.TH_PR,1)=2 THEN 'PRACTICAL' ELSE 'THEORY & PRACTICAL'  END AS TH_PR", ",CASE WHEN ISNULL(S.SEC_BATCH,1)=1 THEN 'SECTION' WHEN ISNULL(S.SEC_BATCH,1)=2 THEN 'BATCH' ELSE 'SECTION & BATCH' END AS SEC_BATCH", "C.COURSENO=" + ddlCourseNo + " AND  S.ACTIVESTATUS = 1", "S.SUBID");
        DataSet ds = objCommon.FillDropDown("ACD_COURSE C LEFT JOIN ACD_SUBJECTTYPE S ON C.SUBID = S.SUBID", "DISTINCT S.SUBID, S.SUBNAME, ISNULL(S.ISTUTORIAL,0) AS ISTUTORIAL, ISNULL(THEORY,0) AS THEORY", "ISNULL(S.TH_PR,0) AS TH_PR, ISNULL(S.SEC_BATCH,0) AS SEC_BATCH", "C.COURSENO=" + ddlCourseNo + " AND  S.ACTIVESTATUS = 1", "S.SUBID");

        if (ds.Tables[0].Rows.Count > 0) 
        {
            int ISTUTORIAL = Convert.ToInt32(ds.Tables[0].Rows[0]["ISTUTORIAL"]);
            int THEORY = Convert.ToInt32(ds.Tables[0].Rows[0]["THEORY"]);
            int SEC_BATCH = Convert.ToInt32(ds.Tables[0].Rows[0]["SEC_BATCH"]);
            DivSection.Visible = false;
            DivBatch.Visible = false;
            DivTutorialBatch.Visible = false;

            switch (SEC_BATCH)
            {
                case 1:
                    if (ISTUTORIAL > 0 && THEORY > 0 && SEC_BATCH == 1)  // && THEORY > 0
                    {
                        DivSection.Visible = true;
                        //DivBatch.Visible = true;
                        DivTutorialBatch.Visible = true;
                        this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
                    }
                    else
                    {
                        DivSection.Visible = true;
                        // DivBatch.Visible = true;
                        this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
                    }
                    break;

                case 2:
                    if (ISTUTORIAL > 0 && THEORY > 0 && SEC_BATCH == 2)   // && THEORY > 0
                    {
                        DivSection.Visible = true;
                        DivBatch.Visible = true;
                        DivTutorialBatch.Visible = true;
                        this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
                    }
                    else
                    {
                        DivSection.Visible = true;
                        DivBatch.Visible = true;
                        this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
                    }
                    break;

                case 3:
                    if (ISTUTORIAL > 0 && THEORY > 0 && SEC_BATCH == 3)  // && THEORY > 0
                    {
                        DivSection.Visible = true;
                        DivBatch.Visible = true;
                        DivTutorialBatch.Visible = true;
                        this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
                    }
                    else
                    {
                        DivSection.Visible = true;
                        DivBatch.Visible = true;
                        this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
                    }
                    break;

                default:
                    DivSection.Visible = true;
                    //DivBatch.Visible = true;
                    this.objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0 AND ACTIVESTATUS=1", "SECTIONNAME");
                    break;
            }
        }
      
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlStudent.Visible = false;
            lvStudents.DataSource = null;
            lvStudents.DataBind();

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
                    //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT (SESSIONNO)", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                    //ddlSession.Focus();
                    objCommon.FillDropDownList(ddlSem, "ACD_COURSE AC INNER JOIN ACD_SEMESTER S ON (AC.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_STUDENT_RESULT SR ON(SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO)", "DISTINCT AC.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COLLEGE_ID= " + Convert.ToInt32(ViewState["college_id"]) + ") AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "", "AC.SEMESTERNO");//AND SR.PREV_STATUS = 0

                    ddlSem.Focus();
                }
            }
            else
            {
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
                ddlSem.Items.Clear();
                ddlSem.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudent.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SC ON (SC.SCHEMENO = CT.SCHEMENO)", "DISTINCT SC.COSCHNO", "SC.COL_SCHEME_NAME", "CT.SESSIONNO IN (" + "SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ")", "SC.COSCHNO");
            //objCommon.FillDropDownList(ddlSem, "ACD_COURSE AC INNER JOIN ACD_SEMESTER S ON (AC.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_STUDENT_RESULT SR ON(SR.SCHEMENO=AC.SCHEMENO AND SR.SEMESTERNO=AC.SEMESTERNO)", "DISTINCT AC.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "", "AC.SEMESTERNO");//AND SR.PREV_STATUS = 0
            //ddlSem.Focus();
            ddlClgname.Focus();
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        StudentController objSC = new StudentController();
        int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int Semesterno = Convert.ToInt32(ddlSem.SelectedValue);
        int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        int college_id = Convert.ToInt32(ViewState["college_id"]);
        DataSet ds = objSC.Get_Coursewise_Section_Allotment_Report(Sessionno, Schemeno, Semesterno, courseno, OrgId, college_id);
        GridView gv = new GridView();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            string attachment = "attachment ; filename=Coursewise_Section_Allotment_Report"+"_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
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
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedIndex > 0)
        {           
            this.objCommon.FillDropDownList(ddlBatch, "ACD_BATCH A WITH (NOLOCK) INNER JOIN ACD_SECTION B WITH (NOLOCK) ON (A.SECTIONNO = B.SECTIONNO)", "DISTINCT (A.BATCHNO)", "A.BATCHNAME", "B.SECTIONNO > 0 AND A.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1 ", "A.BATCHNO");
            this.objCommon.FillDropDownList(ddlTutorialBatch, "ACD_BATCH A WITH (NOLOCK) INNER JOIN ACD_SECTION B WITH (NOLOCK) ON (A.SECTIONNO = B.SECTIONNO)", "DISTINCT (A.BATCHNO)", "A.BATCHNAME", "B.SECTIONNO > 0 AND A.SECTIONNO =" + ddlSection.SelectedValue + " AND ISNULL(A.ACTIVESTATUS,0)=1 ", "A.BATCHNO");          
            ddlBatch.Enabled = true;
            ddlTutorialBatch.Enabled = true;
        }
        else
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlBatch.SelectedIndex = 0;
            ddlTutorialBatch.Items.Clear();
            ddlTutorialBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlTutorialBatch.SelectedIndex = 0;
        }
    }
}