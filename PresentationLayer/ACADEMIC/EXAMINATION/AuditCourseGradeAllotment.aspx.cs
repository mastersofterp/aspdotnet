//=================================================================================
// PROJECT NAME  : YCCE                                                       
// MODULE NAME   : Academic
//Page Name      : Audit Course Grade Allotment                                      
// CREATION DATE :                                                     
// CREATED BY    : Abhijit Deshpande                                        
// MODIFIED BY   :                                                     
// MODIFIED DESC :        
//=================================================================================

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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_AuditCourseGradeAllotment : System.Web.UI.Page
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
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                if (Session["usertype"].ToString() == "1")
                {
                    btnUnlock.Visible = true;

                }
                PopulateDropDownList();
                // ddlSession.Focus();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AuditCourseGradeAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AuditCourseGradeAllotment.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {

            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + (Session["userdeptno"]) + "))", "");

            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO in  (" + (Session["userdeptno"]) + ")", "");





            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");

            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResultList.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Fill DropDownList

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            //ddlBranch.Focus();
            //ddlScheme.Items.Clear();
            //ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            //ddlSem.Items.Clear();
            //ddlSem.Items.Add(new ListItem("Please Select", "0"));
            //ddlBranch.Focus();
        }
        else
        {
            ClearControls();
        }
    }

    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlBranch.SelectedIndex > 0)
    //    {
    //        lvStudents.DataSource = null;
    //        lvStudents.DataBind();
    //        ddlScheme.Items.Clear();
    //        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMETYPE=1 AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
    //        ddlScheme.Focus();
    //        ddlSem.Items.Clear();
    //        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    //        ddlScheme.Focus();
    //    }
    //    else
    //    {
    //        ddlSem.Items.Clear();
    //        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    //    }


    //}

    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlScheme.SelectedIndex > 0)
    //    {
    //        lvStudents.DataSource = null;
    //        lvStudents.DataBind();
    //        ddlSem.Items.Clear();
    //        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
    //        ddlSem.Focus();
    //    }
    //    else
    //    {
    //        ddlSem.Items.Clear();
    //        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSession.Items.Add(new ListItem("Please Select", "0"));
    //    }
    //}

    #endregion

    private void ClearControls()
    {
        lvStudents.Visible = false;
        ddlSem.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        btnReport.Visible = false;
        btnSubmit.Visible = false;
        btnLock.Visible = false;

        ddlClgname.Focus();
    }

    protected void btnShowResult_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MarksEntryController objMarkEntryController = new MarksEntryController();
        MarkEntry objMarkEntry = new MarkEntry();

        objMarkEntry.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objMarkEntry.SchemeNo = 0;
        objMarkEntry.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
        //int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        string grades = string.Empty;
        string idnos = string.Empty;
        string courseno = string.Empty;
        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            DropDownList ddlGrades = lvItem.FindControl("ddlGrades") as DropDownList;
            Label lblIDNO = lvItem.FindControl("lblIDNO") as Label;
            if (ddlGrades.SelectedIndex > 0)
            {
                grades += ddlGrades.SelectedItem.Text + "$";
                idnos += lblIDNO.Text + "$";
                courseno += lblIDNO.ToolTip + "$";
            }
        }

        CustomStatus cs = (CustomStatus)objMarkEntryController.AddAuditCourse(objMarkEntry, grades, idnos, courseno);
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            //objCommon.DisplayMessage(this.UpdatePanel1, "Course Added Successfully!!", this.Page);
            objCommon.DisplayMessage(updpnlExam, "Grade Allotment Done Successfully!!", this.Page);
            BindListView();
            // ClearControls();
        }
        else
            //objCommon.DisplayMessage(this.UpdatePanel1, "Error!!", this.Page);
            objCommon.DisplayMessage(updpnlExam, "Error!!", this.Page);
    }

    private void BindListView()
    {
        try
        {
            string titcredits = string.Empty;
            DataSet ds = null;
            //ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (SR.IDNO = S.IDNO)", "DISTINCT SR.IDNO", "SR.SEATNO,S.STUDNAME,DBO.FN_DESC('BRANCHSNAME',BRANCHNO)BRANCHNAME,BRANCHNO,SR.SEMESTERNO,SR.REGNO,SR.CCODE,SR.COURSENO,SR.COURSENAME,SR.GRADE,DBO.FN_DESC('SECTIONNAME',SR.SECTIONNO)SECTION,SR.SECTIONNO,SR.ROLL_NO AS ROLLNO,SR.LOCKE", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0 AND S.CAN=0 AND DEGREENO=" + ddlDegree.SelectedValue + " AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue, "BRANCHNO,SR.SEMESTERNO,SR.SECTIONNO,ROLLNO");


            //ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (SR.IDNO = S.IDNO)", "DISTINCT SR.IDNO", "SR.SEATNO,S.STUDNAME,DBO.FN_DESC('BRANCHSNAME',BRANCHNO)BRANCHNAME,BRANCHNO,SR.SEMESTERNO,SR.REGNO,SR.CCODE,SR.COURSENO,SR.COURSENAME,case when SR.GRADE='SA' then 1 when SR.GRADE='US' then 2 when SR.GRADE='W'then 3 when SR.GRADE='I'then 4 else 0 end GRADE ,DBO.FN_DESC('SECTIONNAME',SR.SECTIONNO)SECTION,SR.SECTIONNO,SR.ROLL_NO AS ROLLNO,SR.LOCKE", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0 AND S.CAN=0 AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue, "BRANCHNO,SR.SEMESTERNO,SR.SECTIONNO,ROLLNO");

            ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (SR.IDNO = S.IDNO)", "DISTINCT SR.IDNO", "SR.SEATNO,S.STUDNAME,DBO.FN_DESC('BRANCHSNAME',BRANCHNO)BRANCHNAME,BRANCHNO,SR.SEMESTERNO,SR.REGNO,SR.CCODE,SR.COURSENO,SR.COURSENAME,SR.GRADE,DBO.FN_DESC('SECTIONNAME',SR.SECTIONNO)SECTION,SR.SECTIONNO,SR.ROLL_NO AS ROLLNO,SR.LOCKE", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0 AND S.CAN=0 AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue, "BRANCHNO,SR.SEMESTERNO,SR.SECTIONNO,ROLLNO");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {


                    lvStudents.DataSource = ds;
                    lvStudents.DataBind();
                    lvStudents.Visible = true;
                    btnLock.Visible = true;
                    // btnUnlock.Visible = true;
                    btnReport.Visible = true;
                    btnSubmit.Visible = true;

                    int i = 0;
                    foreach (ListViewDataItem item in lvStudents.Items)
                    {
                        DropDownList ddlgrade = item.FindControl("ddlGrades") as DropDownList;
                        Label lblLock = item.FindControl("lblLock") as Label;
                        if (ds.Tables[0].Rows[i]["GRADE"].ToString() == string.Empty)
                        {
                            objCommon.FillDropDownList(ddlgrade, "ACD_AUDIT_GRADE", "ID", "GRADE_NAME", "ID>0 AND ACTIVESTATUS='1'", "ID");
                            ddlgrade.SelectedIndex = 0;
                        }
                        else
                        {
                            string grade = ds.Tables[0].Rows[i]["GRADE"].ToString();
                            int id = Convert.ToInt32(objCommon.LookUp("ACD_AUDIT_GRADE", "ID", "GRADE_NAME='" + grade + "' AND ACTIVESTATUS='1'"));
                            objCommon.FillDropDownList(ddlgrade, "ACD_AUDIT_GRADE", "ID", "GRADE_NAME", "ID>0 AND ACTIVESTATUS='1'", "ID");

                            ddlgrade.SelectedValue = id.ToString();//ddlgrade.Items.FindByText(ds.Tables[0].Rows[i]["GRADE"].ToString()).Value;
                        }
                        i++;

                        //int ddlGrade = Convert.ToInt32(ddlgrade.ToString());
                        //ddlgrade.SelectedValue = ddlgrade.Items.FindByText(ds.Tables[0].Rows[i]["GRADE"].ToString()).Value;
                        // ViewState["locke"] = Convert.ToInt32(ds.Tables[0].Rows[0]["LOCKE"]).ToString();

                        //if (Convert.ToInt32(ViewState["locke"]) > 0)
                        //{
                        //    ddlgrade.Enabled = false;


                        //}
                        //int temp = Convert.ToInt32(ddlgrade.ToolTip);
                        //if (temp > 0)
                        //{
                        //    ddlgrade.Enabled = false;

                        //}
                        if (lblLock.ToolTip.ToUpper().Equals("TRUE"))
                        {
                            ddlgrade.Enabled = false;

                        }
                    }
                }
                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    objCommon.DisplayMessage(updpnlExam, "No student Found for selecting criteria!", this.Page);
                    lvStudents.Visible = false;
                    btnLock.Visible = false;
                    //btnUnlock.Visible = false;
                    btnReport.Visible = false;
                    btnSubmit.Visible = false;

                }
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();

                //objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                objCommon.DisplayMessage(updpnlExam, "No Students found for selected criteria!", this.Page);
                lvStudents.Visible = false;
                btnLock.Visible = false;
                //btnUnlock.Visible = false;
                btnReport.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_SectionAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            CheckBox chkCourseNo = e.Item.FindControl("cbRow") as CheckBox;
            DropDownList ddlGrades = e.Item.FindControl("ddlGrades") as DropDownList;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuditCourseGradeAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //lvStudents.DataSource = null;
        // lvStudents.DataBind();
        ShowReport("AUDIT_COURSES_REPORT", "rptAuditCourseGradeAllotment.rpt");
        //btnReport.Visible = false;
        //btnLock.Visible = false;
        //btnSubmit.Visible = false;
        //lvStudents.Visible = false;

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {


        //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
        //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
        //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
        //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();


        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue;

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        string Script = string.Empty;
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, updpnlExam.GetType(), "Report", Script, true);




        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,Academic," + rptFileName;
        //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue;
        //    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    divMsg.InnerHtml += " </script>";
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}
        //}
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnReport.Visible = false;
        btnLock.Visible = false;
        btnSubmit.Visible = false;
        lvStudents.Visible = false;

        if (ddlSem.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SEMESTERNO = " + ddlSem.SelectedValue + "  AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])+"AND ISNULL(SR.CREDITS,0)=0", "COURSE_NAME"); //Commented By Sagar Mankar On Date 17102023 For CPUH
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SEMESTERNO = " + ddlSem.SelectedValue + "  AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND (ISNULL(C.IS_AUDIT,0)=1 OR ISNULL(SR.IS_AUDIT,0)=1)", "COURSE_NAME"); //Added By Sagar Mankar On Date 17102023 For CPUH

            ddlCourse.Focus();
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add("Please Select");
            ddlCourse.SelectedIndex = 0;

            ddlSem.Focus();
            //lvStudents.Visible = false;
        }
        //  lvStudents.DataSource = null;
        // lvStudents.DataBind();
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        MarksEntryController objMarkEntryController = new MarksEntryController();
        MarkEntry objMarkEntry = new MarkEntry();

        objMarkEntry.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objMarkEntry.SchemeNo = 0;
        objMarkEntry.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
        //int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        string grades = string.Empty;
        string idnos = string.Empty;
        string courseno = string.Empty;
        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            DropDownList ddlGrades = lvItem.FindControl("ddlGrades") as DropDownList;
            Label lblIDNO = lvItem.FindControl("lblIDNO") as Label;

            if (ddlGrades.SelectedIndex > 0)
            {
                grades += ddlGrades.SelectedItem.Text + "$";
                idnos += lblIDNO.Text + "$";
                courseno += lblIDNO.ToolTip + "$";


            }
            //else
            //{
            //    objCommon.DisplayMessage("Marks Entry Not Completed!!!!", this.Page);
            //    return;
            //}
        }

        CustomStatus cs = (CustomStatus)objMarkEntryController.LockAuditCourse(objMarkEntry, idnos, courseno);
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            //objCommon.DisplayMessage(this.UpdatePanel1, "Course Added Successfully!!", this.Page);
            objCommon.DisplayMessage(updpnlExam, "Grade Entry Lock Successfully!!", this.Page);
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {

                DropDownList ddlgrade = lvItem.FindControl("ddlGrades") as DropDownList;
                Label lblLock = lvItem.FindControl("lblLock") as Label;

                DataSet ds = null;
                ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (SR.IDNO = S.IDNO)", "DISTINCT SR.IDNO", "SR.SEATNO,S.STUDNAME,DBO.FN_DESC('BRANCHSNAME',BRANCHNO)BRANCHNAME,BRANCHNO,SR.SEMESTERNO,SR.REGNO,SR.CCODE,SR.COURSENO,SR.COURSENAME,SR.GRADE,DBO.FN_DESC('SECTIONNAME',SR.SECTIONNO)SECTION,SR.SECTIONNO,SR.ROLL_NO AS ROLLNO,SR.LOCKE", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.EXAM_REGISTERED=1 AND ISNULL(SR.CANCEL,0)=0 AND S.CAN=0 AND DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND SR.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SR.SEMESTERNO=" + ddlSem.SelectedValue, "BRANCHNO,SR.SEMESTERNO,SR.SECTIONNO,ROLLNO");

                if (lblLock.ToolTip.ToUpper().Equals("TRUE"))
                {
                    ddlgrade.Enabled = false;
                }
                else { ddlgrade.Enabled = true; }


                //DropDownList ddlGrades = lvItem.FindControl("ddlGrades") as DropDownList;
                //ddlGrades.Enabled = false;

                //ddlGrades.Enabled = false;
            }

            BindListView();
            // ClearControls();
            //lvStudents.Visible = true;

        }
        else
            //objCommon.DisplayMessage(this.UpdatePanel1, "Error!!", this.Page);
            objCommon.DisplayMessage(updpnlExam, "Error!!", this.Page);
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        MarksEntryController objMarkEntryController = new MarksEntryController();
        MarkEntry objMarkEntry = new MarkEntry();

        objMarkEntry.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objMarkEntry.SchemeNo = 0;
        objMarkEntry.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
        //int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        string grades = string.Empty;
        string idnos = string.Empty;
        string courseno = string.Empty;
        foreach (ListViewDataItem lvItem in lvStudents.Items)
        {
            DropDownList ddlGrades = lvItem.FindControl("ddlGrades") as DropDownList;
            Label lblIDNO = lvItem.FindControl("lblIDNO") as Label;
            if (ddlGrades.SelectedIndex > 0)
            {
                grades += ddlGrades.SelectedItem.Text + "$";
                idnos += lblIDNO.Text + "$";
                courseno += lblIDNO.ToolTip + "$";
            }
            //else
            //{
            //    objCommon.DisplayMessage("Marks Entry Not Completed!!!!", this.Page);
            //    return;
            //}
        }
        CustomStatus cs = (CustomStatus)objMarkEntryController.UnlockAuditCourse(objMarkEntry, idnos, courseno);
        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            //objCommon.DisplayMessage(this.UpdatePanel1, "Course Added Successfully!!", this.Page);
            objCommon.DisplayMessage(updpnlExam, "Grade Entry Unlock Successfully!!", this.Page);
            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                DropDownList ddlGrades = lvItem.FindControl("ddlGrades") as DropDownList;
                ddlGrades.Enabled = true;
            }
            //ClearControls();
            BindListView();
        }
        else
            //objCommon.DisplayMessage(this.UpdatePanel1, "Error!!", this.Page);
            objCommon.DisplayMessage(updpnlExam, "Error!!", this.Page);
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        btnLock.Visible = false;
        //btnUnlock.Visible = false;
        btnReport.Visible = false;
        btnSubmit.Visible = false;
        lvStudents.Visible = false;
        Common objCommon = new Common();

        if (ddlClgname.SelectedIndex > 0)
        {

            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));


            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
                ddlSession.Focus();
            }
        }
        else
        {
            ddlClgname.Focus();

            ddlSession.Items.Clear();
            ddlSession.Items.Add("Please Select");
            ddlSession.SelectedIndex = 0;

            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSem.SelectedIndex = 0;

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add("Please Select");
            ddlCourse.SelectedIndex = 0;
            //lvStudents.Visible = false;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnReport.Visible = false;
        btnLock.Visible = false;
        btnSubmit.Visible = false;
        lvStudents.Visible = false;

        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
            ddlSem.Focus();
        }
        else
        {
            ddlSession.Focus();

            ddlSem.Items.Clear();
            ddlSem.Items.Add("Please Select");
            ddlSem.SelectedIndex = 0;

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add("Please Select");
            ddlCourse.SelectedIndex = 0;
            //lvStudents.Visible = false;
        }

        //ddlCourse.SelectedIndex = 0;
        // lvStudents.Visible = false;

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnReport.Visible = false;
        btnLock.Visible = false;
        btnSubmit.Visible = false;
        lvStudents.Visible = false;

        btnShowResult.Focus();
    }
}
