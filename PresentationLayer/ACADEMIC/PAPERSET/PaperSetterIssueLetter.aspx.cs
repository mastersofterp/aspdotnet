using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class ACADEMIC_PaperSetterIssueLetter : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();


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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add
                    ViewState["action"] = "add";

                    // Fill Dropdown lists
                    //FillDropdown();
                    FillDropdownCollege();   //College Dropdown added 17-2-23
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CreateOperator.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IssueLetterToFac.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IssueLetterToFac.aspx");
        }
    }
    #endregion

    //#region TAB1 APPLY PAPER SETTER

    //#region Other Events
    //protected void ddlDepartment1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDepartment1.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlSemester1, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)INNER JOIN ACD_STUDENT_RESULT SR ON(SR.COURSENO = C.COURSENO AND SR.SCHEMENO = C.SCHEMENO  AND SR.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND REGISTERED = 1 AND C.MAXMARKS_E > 0 AND C.BOS_DEPTNO =" + ddlDepartment1.SelectedValue, " S.SEMESTERNO");
    //        //objCommon.FillDropDownList(ddlSemester1, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)INNER JOIN ACD_STUDENT_RESULT SR ON(SR.COURSENO = C.COURSENO AND SR.SCHEMENO = C.SCHEMENO  AND SR.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND REGISTERED = 1 AND C.MAXMARKS_E > 0 AND C.BOS_DEPTNO =" + ddlDepartment1.SelectedValue, " S.SEMESTERNO");
    //    }
    //    else
    //        ddlSemester1.SelectedIndex = 0;
    //    lvCourse1.DataSource = null;
    //    lvCourse1.DataBind();
    //    pnlList1.Visible = false;
    //    btnSave1.Visible = false;
    //    btnCancel1.Visible = false;
    //    ddlDepartment1.Focus();
    //}

    //protected void ddlSemester1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lvCourse1.DataSource = null;
    //    lvCourse1.DataBind();
    //    pnlList1.Visible = false;
    //    btnSave1.Visible = false;
    //    btnCancel1.Visible = false;
    //    ddlSemester1.Focus();
    //}

    //#endregion

    //#region Private Methods
    //private void BindListView()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON (SR.CCODE = C.CCODE AND SR.SCHEMENO = C.SCHEMENO AND SR.SEMESTERNO = C.SEMESTERNO)LEFT OUTER JOIN ACD_PAPERSET_DETAILS P ON (c.CCODE = P.CCODE AND c.SEMESTERNO = P.SEMESTERNO AND (P.CANCEL IS NULL OR P.CANCEL = 0)) ", "DISTINCT C.CCODE ,COURSE_NAME", "COUNT(DISTINCT IDNO)TOTAL_STUDENT ,QTY ", "C.SUBID = 1 AND MAXMARKS_E > 0 AND C.BOS_DEPTNO = " + ddlDepartment1.SelectedValue + " AND C.SEMESTERNO = " + ddlSemester1.SelectedValue + " AND REGISTERED = 1 GROUP BY C.CCODE,COURSE_NAME,QTY", "CCODE");
    //    if (ds != null && ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvCourse1.DataSource = ds;
    //            lvCourse1.DataBind();
    //            pnlList1.Visible = true;
    //            btnSave1.Visible = true;
    //            btnCancel1.Visible = true;
    //        }
    //        else
    //        {
    //            lvCourse1.DataSource = null;
    //            lvCourse1.DataBind();
    //            pnlList1.Visible = false;
    //            btnSave1.Visible = false;
    //            btnCancel1.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        lvCourse1.DataSource = null;
    //        lvCourse1.DataBind();
    //        pnlList1.Visible = false;
    //        btnSave1.Visible = false;
    //        btnCancel1.Visible = false;
    //    }
    //}
    //#endregion

    //#region Click Events
    //protected void btnSave1_Click(object sender, EventArgs e)
    //{
    //    CourseController objCCont = new CourseController();
    //    foreach (ListViewItem item in lvCourse1.Items)
    //    {
    //        Label lblCCode = item.FindControl("lblCCode") as Label;
    //        CheckBox cbchk = item.FindControl("cbchk") as CheckBox;

    //        int sessionno = Convert.ToInt16(objCommon.LookUp("ACD_SESSION_MASTER", "TOP 1 SESSIONNO", "SESSIONNO > 0 ORDER BY SESSIONNO DESC"));
    //        int ret;
    //        string str = objCommon.LookUp("ACD_PAPERSET_DETAILS", "COUNT(SEMESTERNO)", "(DEAN_LOCK = 1 OR APPROVED IS not NULL OR BOS_LOCK = 1) AND BOS_DEPTNO = " + ddlDepartment1.SelectedValue + " AND CCODE = '" + lblCCode.Text + "' AND SEMESTERNO = " + ddlSemester1.SelectedValue + " AND SESSIONNO = " + sessionno);

    //        if (str == "" || str == "0")
    //        {
    //            if (cbchk.Checked)
    //            {
    //                ret = objCCont.UpdateCourseBal(sessionno, Convert.ToInt16(ddlDepartment1.SelectedValue), Convert.ToInt16(ddlSemester1.SelectedValue), lblCCode.Text);

    //            }
    //            else
    //            {
    //                ret = objCCont.DeleteCourseBal(sessionno, Convert.ToInt16(ddlDepartment1.SelectedValue), Convert.ToInt16(ddlSemester1.SelectedValue), lblCCode.Text);
    //            }
    //            if (ret == 99)
    //            {
    //                objCommon.DisplayMessage("Data Not Saved", this.Page);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updPaperStock, "No further stock request allowed. Entry is locked!", this.Page);
    //            return;
    //        }
    //    }
    //    objCommon.DisplayMessage(this.updPaperStock, "Paper Set Details Saved!", this.Page);
    //    BindListView();

    //}
    //protected void btnClear1_Click(object sender, EventArgs e)
    //{
    //    tbc1.ActiveTabIndex = 0;
    //    ddlSemester1.SelectedIndex = 0;
    //    ddlDepartment1.SelectedIndex = 0;

    //    lvCourse1.DataSource = null;
    //    lvCourse1.DataBind();
    //    pnlList1.Visible = false;
    //    btnSave1.Visible = false;
    //    btnCancel1.Visible = false;
    //}
    //protected void btnCancel1_Click(object sender, EventArgs e)
    //{
    //    lvCourse1.DataSource = null;
    //    lvCourse1.DataBind();
    //    pnlList1.Visible = false;
    //    btnSave1.Visible = false;
    //    btnCancel1.Visible = false;
    //}

    //protected void btnShow1_Click(object sender, EventArgs e)
    //{
    //    if (ddlDepartment1.SelectedIndex > 0)
    //    {
    //        if (ddlSemester1.SelectedIndex > 0)
    //            BindListView();
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updPaperStock, "Please Select Semester", this.Page);
    //            lvCourse1.DataSource = null;
    //            lvCourse1.DataBind();
    //            pnlList1.Visible = false;

    //        }
    //    }
    //    else
    //    {
    //        if (ddlDepartment1.Items.Count == 1)
    //            objCommon.DisplayMessage(this.updPaperStock, "Please Select Degree and  Department", this.Page);
    //        else
    //            objCommon.DisplayMessage(this.updPaperStock, "Please Select Department", this.Page);

    //        lvCourse1.DataSource = null;
    //        lvCourse1.DataBind();
    //        pnlList1.Visible = false;

    //    }
    //}


    //#endregion

    //#endregion

    #region COMMENT TAB2 MODIFY PAPER SETTER
    //    #region Private Method

    //    private void BindListView2()
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_STAFF S ON(P.APPROVED = S.STAFFNO)", "DISTINCT P.CCODE, COURSE_NAME,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER", "P.SEMESTERNO,P.APPROVED,S.INTERNAL_EXTERNAL,BOS_LOCK,DEAN_LOCK,ISNULL(RECEIVED,0)RECEIVED", "P.BOS_DEPTNO = " + ddlDepartment2.SelectedValue + " AND (P.SEMESTERNO = " + ddlSemester2.SelectedValue + " OR " + ddlSemester2.SelectedValue + " = 0 ) AND MAXMARKS_E > 0 AND SUBID = 1 AND BOS_LOCK = 1 AND DEAN_LOCK = 1 AND APPROVED <> 0", " DEAN_LOCK DESC,SEMESTER,P.CCODE");

    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lvCourse2.DataSource = ds;
    //                lvCourse2.DataBind();
    //                pnlList2.Visible = true;
    //            }
    //            else
    //            {
    //                lvCourse2.DataSource = null;
    //                lvCourse2.DataBind();
    //                pnlList2.Visible = false;
    //                btnSave2.Visible = false;
    //                btnCancel2.Visible = false;
    //                btnLock2.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            lvCourse2.DataSource = null;
    //            lvCourse2.DataBind();
    //            pnlList2.Visible = false;
    //            btnSave2.Visible = false;
    //            btnCancel2.Visible = false;
    //            btnLock2.Visible = false;
    //        }
    //    }
    //    private void SaveAndLock2(bool exam_sec)
    //    {
    //        CourseController objCCont = new CourseController();
    //        foreach (ListViewItem item in lvCourse2.Items)
    //        {
    //            Label lblCCode = item.FindControl("lblCCode") as Label;
    //            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
    //            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
    //            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;
    //            int ret = 0;
    //            CheckBox chkAppFac1 = item.FindControl("chkAppFac1") as CheckBox;
    //            CheckBox chkAppFac2 = item.FindControl("chkAppFac2") as CheckBox;
    //            CheckBox chkAppFac3 = item.FindControl("chkAppFac3") as CheckBox;

    //            if (chkAppFac1.Enabled)
    //                ret = 0;// objCCont.AcceptPaperSetFaculty(Convert.ToInt16(ddlSession2.SelectedValue), Convert.ToInt16(ddlDepartment2.SelectedValue), Convert.ToInt16(ddlSemester2.SelectedValue), lblCCode.Text, Convert.ToInt16(ddlFaculty1.SelectedValue), Convert.ToInt16(ddlFaculty2.SelectedValue), Convert.ToInt16(ddlFaculty3.SelectedValue), chkAppFac1.Checked, chkAppFac2.Checked, chkAppFac3.Checked, exam_sec);

    //            if (ret != -99)
    //            {
    //                if (exam_sec)
    //                    objCommon.DisplayMessage("Faculty Allotment Locked.", this.Page);
    //                else
    //                    objCommon.DisplayMessage("Faculty Allotment Done.", this.Page);
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage("Data Not Saved", this.Page);
    //                return;
    //            }
    //        }
    //        BindListView2();
    //    }

    //    #endregion

    //    #region Click Events
    //    protected void btnSave2_Click(object sender, EventArgs e)
    //    {
    //        SaveAndLock2(false);
    //    }
    //    protected void btnLock2_Click(object sender, EventArgs e)
    //    {
    //        SaveAndLock2(true);
    //    }
    //    protected void btnCancel2_Click(object sender, EventArgs e)
    //    {
    //        pnlList2.Visible = false;
    //        lvCourse2.DataSource = null;
    //        lvCourse2.DataBind();
    //    }
    //    protected void btnShow2_Click(object sender, EventArgs e)
    //    {
    //        if (ddlDepartment2.SelectedIndex > 0)
    //        {
    //            BindListView2();
    //        }
    //        else
    //        {
    //            if (ddlDepartment2.Items.Count == 1)
    //                objCommon.DisplayMessage(this.updFacAllot, "Please Select Degree and  Department", this.Page);
    //            else
    //                objCommon.DisplayMessage(this.updFacAllot, "Please Select Department", this.Page);

    //            lvCourse2.DataSource = null;
    //            lvCourse2.DataBind();
    //            pnlList2.Visible = false;

    //        }
    //    }
    //    protected void btnClear2_Click(object sender, EventArgs e)
    //    {
    //        ddlDepartment2.SelectedIndex = 0;
    //        ddlSemester2.SelectedIndex = 0;
    //        ddlDegree2.SelectedIndex = 0;
    //        ddlSession2.SelectedIndex = 0;
    //        lvCourse2.DataSource = null;
    //        lvCourse2.DataBind();
    //        btnSave2.Visible = false;
    //        btnCancel2.Visible = false;
    //        btnLock2.Visible = false;
    //        pnlList2.Visible = false;


    //        ddlDepartment2.Items.Clear();
    //        ddlDepartment2.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSemester2.Items.Clear();
    //        ddlSemester2.Items.Add(new ListItem("Please Select", "0"));

    //    }
    //    #endregion

    //    #region Other Events
    //    protected void ddlDepartment2_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        if (ddlDepartment2.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlSemester2, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND BOS_LOCK = 1 )", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0  AND P.BOS_DEPTNO =" + ddlDepartment2.SelectedValue + " AND P.SESSIONNO = " + ddlSession2.SelectedValue, " S.SEMESTERNO");
    //        }
    //        else
    //            ddlSemester2.SelectedIndex = 0;

    //        lvCourse2.DataSource = null;
    //        lvCourse2.DataBind();
    //        btnSave2.Visible = false;
    //        btnCancel2.Visible = false;
    //        btnLock2.Visible = false;
    //    }
    //    protected void ddlSemester2_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        lvCourse2.DataSource = null;
    //        lvCourse2.DataBind();
    //        btnSave2.Visible = false;
    //        btnCancel2.Visible = false;
    //        btnLock2.Visible = false;
    //    }

    //    protected void ddlDegree2_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        lvCourse2.DataSource = null;
    //        lvCourse2.DataBind();
    //        btnSave2.Visible = false;
    //        btnCancel2.Visible = false;
    //        btnLock2.Visible = false;
    //        pnlList2.Visible = false;

    //        if (ddlDegree2.SelectedIndex > 0)
    //            objCommon.FillDropDownList(ddlDepartment2, "ACD_DEPARTMENT D INNER JOIN ACD_PAPERSET_DETAILS P  ON (D.DEPTNO = P.BOS_DEPTNO AND BOS_LOCK = 1)", " DISTINCT DEPTNO", "DEPTNAME", "DEPTNO>0 AND DEPTNO IN (SELECT DEPTNO FROM ACD_BRANCH WHERE DEGREENO = " + ddlDegree2.SelectedValue + ")", "DEPTNO");
    //        else
    //            ddlDepartment2.SelectedIndex = 0;
    //    }

    //    protected void lvCourse2_ItemDataBound(object sender, ListViewItemEventArgs e)
    //    {
    //        Label lblCCode = e.Item.FindControl("lblCCode") as Label;

    //        //DROPDOWNS TO FILL
    //        DropDownList ddlFaculty1 = e.Item.FindControl("ddlFaculty1") as DropDownList;
    //        DropDownList ddlFaculty2 = e.Item.FindControl("ddlFaculty2") as DropDownList;
    //        DropDownList ddlFaculty3 = e.Item.FindControl("ddlFaculty3") as DropDownList;

    //        //PREVIOUS ALLOTMENT IF DONE
    //        HiddenField hffaculty1 = e.Item.FindControl("hffaculty1") as HiddenField;
    //        HiddenField hffaculty2 = e.Item.FindControl("hffaculty2") as HiddenField;
    //        HiddenField hffaculty3 = e.Item.FindControl("hffaculty3") as HiddenField;

    //        //LOCK STATUS
    //        HiddenField hfDeanLock = e.Item.FindControl("hfDeanLock") as HiddenField;
    //        HiddenField hfRECEIVED1 = e.Item.FindControl("hfRECEIVED1") as HiddenField;
    //        HiddenField hfRECEIVED2 = e.Item.FindControl("hfRECEIVED2") as HiddenField;
    //        HiddenField hfRECEIVED3 = e.Item.FindControl("hfRECEIVED3") as HiddenField;

    //        CheckBox chkAppFac1 = e.Item.FindControl("chkAppFac1") as CheckBox;
    //        CheckBox chkAppFac2 = e.Item.FindControl("chkAppFac2") as CheckBox;
    //        CheckBox chkAppFac3 = e.Item.FindControl("chkAppFac3") as CheckBox;

    //        string ua_no1 = "0", ua_no2 = "0", ua_no3 = "0";

    //        if (hfRECEIVED1.Value.ToLower() == "true")
    //        {
    //            objCommon.FillDropDownList(ddlFaculty1, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO = " + hffaculty1.Value, string.Empty);
    //            if (hffaculty1.Value != "")
    //                ua_no1 = hffaculty1.Value;
    //            ddlFaculty1.SelectedValue = ua_no1;
    //            ddlFaculty1.Enabled = false;
    //            chkAppFac1.Enabled = false;
    //        }
    //        else
    //        {
    //            objCommon.FillDropDownList(ddlFaculty1, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0", string.Empty);
    //            if (hffaculty1.Value != "")
    //                ua_no1 = hffaculty1.Value;
    //            else
    //                ua_no1 = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT TOP 1 UA_NO", "SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "'");
    //            ddlFaculty1.SelectedValue = ua_no1;
    //            ddlFaculty1.Enabled = true;
    //            chkAppFac1.Checked = false;
    //            chkAppFac1.Enabled = false;
    //        }

    //        if (hfRECEIVED2.Value.ToLower() == "true")
    //        {
    //            objCommon.FillDropDownList(ddlFaculty2, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO = " + hffaculty2.Value, string.Empty);
    //            ddlFaculty2.SelectedValue = ua_no2;
    //            ddlFaculty2.Enabled = false;
    //            if (hffaculty2.Value != "")
    //                ua_no2 = hffaculty2.Value;
    //            ddlFaculty2.SelectedValue = ua_no2;
    //            chkAppFac2.Enabled = false;
    //        }
    //        else
    //        {
    //            objCommon.FillDropDownList(ddlFaculty2, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0", string.Empty);
    //            if (hffaculty2.Value != "")
    //                ua_no2 = hffaculty2.Value; 
    //            ddlFaculty2.Enabled = true;
    //            ddlFaculty2.SelectedValue = ua_no2; 
    //            chkAppFac2.Checked = false;
    //            chkAppFac2.Enabled = false;
    //        }
    //        if (hfRECEIVED3.Value.ToLower() == "true")
    //        {
    //            objCommon.FillDropDownList(ddlFaculty3, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO = " + hffaculty3.Value, string.Empty);
    //            if (hffaculty3.Value != "")
    //                ua_no3 = hffaculty3.Value;
    //            ddlFaculty3.SelectedValue = ua_no3;
    //            ddlFaculty3.Enabled = false;
    //            chkAppFac3.Enabled = false;
    //        }
    //        else
    //        {
    //            objCommon.FillDropDownList(ddlFaculty3, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0", string.Empty);
    //            if (hffaculty3.Value != "")
    //                ua_no3 = hffaculty3.Value;
    //            ddlFaculty3.SelectedValue = ua_no3; 
    //            ddlFaculty3.Enabled = true;
    //            chkAppFac3.Checked = false;
    //            chkAppFac3.Enabled = false;
    //        }
    //        #region Comment
    //        // objCommon.FillDropDownList(ddlFaculty1, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NOT NULL AND UA_NO > 0)AND UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0", string.Empty);
    //            //SELECTED UANO IS PREVIOUS VALUE ALLOTED OR THE TOP 1 OF THE ALLOTED FACULTY
    //            //btnSave2.Visible = true;
    //            //btnLock2.Visible = true;
    //            //btnCancel2.Visible = true;
    //            //pnlList2.Visible = true;
    //            //btnSave2.Enabled = false;
    //            //btnLock2.Enabled = false;
    //            //btnCancel2.Enabled = false;

    //        //    if (hfDeanLock.Value.ToLower() == "true")
    //        //    {
    //        //    ua_no1 = (ua_no1 == "" ? ddlFaculty1.Items[1].Value : ua_no1);
    //        //    ddlFaculty1.SelectedValue = ua_no1;
    //        //    ddlFaculty2.SelectedValue = ua_no2;//(ua_no2 == "" ? ddlFaculty2.Items[2].Value : ua_no2);
    //        //    ddlFaculty3.SelectedValue = ua_no3;//(ua_no3 == "" ? ddlFaculty3.Items[3].Value : ua_no3);

    //        //    if (ua_no1 != "0")
    //        //    {
    //        //        ddlFaculty2.Items.Remove(ddlFaculty1.SelectedItem);
    //        //        ddlFaculty3.Items.Remove(ddlFaculty1.SelectedItem);
    //        //    }
    //        //    if (ua_no2 != "0")
    //        //    {
    //        //        ddlFaculty1.Items.Remove(ddlFaculty2.SelectedItem);
    //        //        ddlFaculty3.Items.Remove(ddlFaculty2.SelectedItem);
    //        //    }
    //        //    if (ua_no3 != "0")
    //        //    {
    //        //        ddlFaculty1.Items.Remove(ddlFaculty3.SelectedItem);
    //        //        ddlFaculty2.Items.Remove(ddlFaculty3.SelectedItem);
    //        //    }
    //        //    ddlFaculty1.Enabled = true;
    //        //    ddlFaculty2.Enabled = true;
    //        //    ddlFaculty3.Enabled = true;
    //        //    pnlList2.Visible = true;
    //        //    //btnSave2.Visible = true;
    //        //    //btnLock2.Visible = true;
    //        //    //btnCancel2.Visible = true;

    //        //    //btnSave2.Enabled = true;
    //        //    //btnLock2.Enabled = true;
    //        //    //btnCancel2.Enabled = true;
    //        //}
    //#endregion
    //}

    //    protected void ddlFaculty1_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        foreach (ListViewDataItem item in lvCourse2.Items)
    //        {
    //            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
    //            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
    //            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;
    //            Label lblCCode = item.FindControl("lblCCode") as Label;
    //            CheckBox chkAppFac1 = item.FindControl("chkAppFac1") as CheckBox;

    //            if (sender.GetHashCode() == ddlFaculty1.GetHashCode())
    //            {
    //                string ua_no2 = ddlFaculty2.SelectedValue;
    //                string ua_no3 = ddlFaculty3.SelectedValue;

    //                objCommon.FillDropDownList(ddlFaculty2, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ua_no3 + " )", string.Empty);
    //                objCommon.FillDropDownList(ddlFaculty3, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO NOT IN (" + ddlFaculty1.SelectedValue + "," + ua_no2 + " )", string.Empty);

    //                ddlFaculty2.SelectedValue = ua_no2 == ddlFaculty1.SelectedValue ? "0" : ua_no2;
    //                ddlFaculty3.SelectedValue = ua_no3 == ddlFaculty1.SelectedValue ? "0" : ua_no3;

    //                if (ddlFaculty1.SelectedIndex == 0)
    //                {
    //                    chkAppFac1.Enabled = false;
    //                    chkAppFac1.Checked = false;
    //                }
    //                else
    //                {
    //                    chkAppFac1.Enabled = true;
    //                }

    //                return;
    //            }
    //        }
    //    }
    //    protected void ddlFaculty2_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        foreach (ListViewDataItem item in lvCourse2.Items)
    //        {
    //            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
    //            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
    //            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;
    //            Label lblCCode = item.FindControl("lblCCode") as Label;
    //            CheckBox chkAppFac2 = item.FindControl("chkAppFac2") as CheckBox;

    //            if (sender.GetHashCode() == ddlFaculty2.GetHashCode())
    //            {
    //                string ua_no1 = ddlFaculty1.SelectedValue;
    //                string ua_no3 = ddlFaculty3.SelectedValue;

    //                objCommon.FillDropDownList(ddlFaculty1, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO NOT IN (" + ddlFaculty2.SelectedValue + "," + ua_no3 + " )", string.Empty);
    //                objCommon.FillDropDownList(ddlFaculty3, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')  UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO NOT IN (" + ddlFaculty2.SelectedValue + "," + ua_no1 + " )", string.Empty);

    //                ddlFaculty1.SelectedValue = ua_no1 == ddlFaculty2.SelectedValue ? "0" : ua_no1;
    //                ddlFaculty3.SelectedValue = ua_no3 == ddlFaculty2.SelectedValue ? "0" : ua_no3;

    //                if (ddlFaculty2.SelectedIndex == 0)
    //                {
    //                    chkAppFac2.Enabled = false;
    //                    chkAppFac2.Checked = false;
    //                }
    //                else
    //                {
    //                    chkAppFac2.Enabled = true;
    //                }

    //                return;
    //            }
    //        }
    //    }
    //    protected void ddlFaculty3_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        foreach (ListViewDataItem item in lvCourse2.Items)
    //        {
    //            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;
    //            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;
    //            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;
    //            Label lblCCode = item.FindControl("lblCCode") as Label;
    //            CheckBox chkAppFac3 = item.FindControl("chkAppFac3") as CheckBox;

    //            if (sender.GetHashCode() == ddlFaculty3.GetHashCode())
    //            {
    //                string ua_no1 = ddlFaculty1.SelectedValue;
    //                string ua_no2 = ddlFaculty2.SelectedValue;

    //                objCommon.FillDropDownList(ddlFaculty1, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')  UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO NOT IN (" + ddlFaculty3.SelectedValue + "," + ua_no2 + " )", string.Empty);
    //                objCommon.FillDropDownList(ddlFaculty2, "(SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "')  UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE UA_NO NOT IN (SELECT DISTINCT UA_NO FROM ACD_STUDENT_RESULT WHERE SESSIONNO = " + ddlSession2.SelectedValue + " AND UA_NO > 0 AND CCODE = '" + lblCCode.Text + "') UNION SELECT STAFFNO,STAFF_NAME COLLATE DATABASE_DEFAULT AS STAFF_NAME  FROM ACD_STAFF WHERE (UA_NO IS NULL OR UA_NO = 0))A", "STAFFNO", "STAFF_NAME", "STAFFNO > 0 AND STAFFNO NOT IN (" + ddlFaculty3.SelectedValue + "," + ua_no1 + " )", string.Empty);

    //                ddlFaculty1.SelectedValue = (ua_no1 == ddlFaculty3.SelectedValue ? "0" : ua_no1);
    //                ddlFaculty2.SelectedValue = (ua_no2 == ddlFaculty3.SelectedValue ? "0" : ua_no2);

    //                if (ddlFaculty3.SelectedIndex == 0)
    //                {
    //                    chkAppFac3.Enabled = false;
    //                    chkAppFac3.Checked = false;
    //                }
    //                else
    //                {
    //                    chkAppFac3.Enabled = true;
    //                }

    //                return;
    //            }
    //        }
    //    }


    //    protected void cbhead1_OnCheckedChanged(object sender, EventArgs e)
    //    {
    //        CheckBox cbhead1 = sender as CheckBox;
    //        foreach (ListViewItem item in lvCourse2.Items)
    //        {
    //            CheckBox chkAppFac1 = item.FindControl("chkAppFac1") as CheckBox;
    //            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;

    //            if (cbhead1.Checked)
    //            {
    //                if (chkAppFac1.Enabled == true)
    //                    if (ddlFaculty1.SelectedIndex > 0)
    //                        chkAppFac1.Checked = true;
    //                    else
    //                        chkAppFac1.Checked = false;
    //            }
    //            else
    //                if (chkAppFac1.Enabled == true)
    //                    chkAppFac1.Checked = false;
    //        }
    //    }
    //    protected void chkAppFac1_OnCheckedChanged(object sender, EventArgs e)
    //    {
    //        CheckBox chkAppFac1 = sender as CheckBox;
    //        foreach (ListViewItem item in lvCourse2.Items)
    //        {
    //            CheckBox chkAppFac11 = item.FindControl("chkAppFac1") as CheckBox;
    //            DropDownList ddlFaculty1 = item.FindControl("ddlFaculty1") as DropDownList;

    //            if (chkAppFac11.GetHashCode() == chkAppFac1.GetHashCode())
    //            {
    //                if (chkAppFac1.Enabled == true)
    //                    if (ddlFaculty1.SelectedIndex > 0)
    //                        chkAppFac1.Checked = true;
    //                    else
    //                        chkAppFac1.Checked = false;
    //                else
    //                    if (chkAppFac1.Enabled == true)
    //                        chkAppFac1.Checked = false;
    //                return;
    //            }

    //        }
    //    }
    //    protected void cbhead2_OnCheckedChanged(object sender, EventArgs e)
    //    {
    //        CheckBox cbhead2 = sender as CheckBox;
    //        foreach (ListViewItem item in lvCourse2.Items)
    //        {
    //            CheckBox chkAppFac2 = item.FindControl("chkAppFac2") as CheckBox;
    //            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;

    //            if (cbhead2.Checked)
    //            {
    //                if (chkAppFac2.Enabled == true)
    //                    if (ddlFaculty2.SelectedIndex > 0)
    //                        chkAppFac2.Checked = true;
    //                    else
    //                        chkAppFac2.Checked = false;
    //            }
    //            else
    //                if (chkAppFac2.Enabled == true)
    //                    chkAppFac2.Checked = false;
    //        }
    //    }
    //    protected void chkAppFac2_OnCheckedChanged(object sender, EventArgs e)
    //    {
    //        CheckBox chkAppFac2 = sender as CheckBox;
    //        foreach (ListViewItem item in lvCourse2.Items)
    //        {
    //            CheckBox chkAppFac22 = item.FindControl("chkAppFac2") as CheckBox;
    //            DropDownList ddlFaculty2 = item.FindControl("ddlFaculty2") as DropDownList;

    //            if (chkAppFac22.GetHashCode() == chkAppFac2.GetHashCode())
    //            {
    //                if (chkAppFac2.Enabled == true)
    //                    if (ddlFaculty2.SelectedIndex > 0)
    //                        chkAppFac22.Checked = true;
    //                    else
    //                        chkAppFac22.Checked = false;

    //                else
    //                    if (chkAppFac2.Enabled == true)
    //                        chkAppFac22.Checked = false;
    //                return;
    //            }
    //        }
    //    }
    //    protected void cbhead3_OnCheckedChanged(object sender, EventArgs e)
    //    {
    //        CheckBox cbhead3 = sender as CheckBox;
    //        foreach (ListViewItem item in lvCourse2.Items)
    //        {
    //            CheckBox chkAppFac3 = item.FindControl("chkAppFac3") as CheckBox;
    //            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;

    //            if (cbhead3.Checked)
    //            {
    //                if (chkAppFac3.Enabled == true)
    //                    if (ddlFaculty3.SelectedIndex > 0)
    //                        chkAppFac3.Checked = true;
    //                    else
    //                        chkAppFac3.Checked = false;
    //            }
    //            else
    //                if (chkAppFac3.Enabled == true)
    //                    chkAppFac3.Checked = false;
    //        }
    //    }
    //    protected void chkAppFac3_OnCheckedChanged(object sender, EventArgs e)
    //    {
    //        CheckBox chkAppFac3 = sender as CheckBox;
    //        foreach (ListViewItem item in lvCourse2.Items)
    //        {
    //            CheckBox chkAppFac33 = item.FindControl("chkAppFac3") as CheckBox;
    //            DropDownList ddlFaculty3 = item.FindControl("ddlFaculty3") as DropDownList;

    //            if (chkAppFac33.GetHashCode() == chkAppFac3.GetHashCode())
    //            {
    //                if (chkAppFac33.Enabled == true)
    //                    if (ddlFaculty3.SelectedIndex > 0)
    //                        chkAppFac33.Checked = true;
    //                    else
    //                        chkAppFac33.Checked = false;
    //                else
    //                    if (chkAppFac33.Enabled == true)
    //                        chkAppFac33.Checked = false;
    //                return;
    //            }
    //        }
    //    }
    //    #endregion

    //    protected void btnReport2_Click(object sender, EventArgs e)
    //    {
    //        if (ddlDepartment2.SelectedIndex > 0)
    //        {
    //            //string semesterno = ddlSemester.SelectedValue;
    //            //semesterno = semesterno == "0" ? " "  : " AND SEMESTERNO = " + semesterno;

    //            //string lock_dept = objCommon.LookUp("ACD_PAPERSET_DETAILS", "DISTINCT (DEAN_LOCK)", "DEAN_LOCK = 1  AND BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND SESSIONNO =" + ddlSession.SelectedValue + semesterno);

    //            //if (lock_dept.ToLower() == "true")
    //            ShowReport2("Dean_Approved_Faculty", "rptPaperSetFacultyListDean.rpt");
    //            //else
    //            //{
    //            //    objCommon.DisplayMessage(this.updFacAllot, "Please lock the paper setter selection list", this.Page);
    //            //    lvCourse.DataSource = null;
    //            //    lvCourse.DataBind();
    //            //    pnlList.Visible = false;
    //            //}
    //            BindListView2();
    //        }
    //        else
    //        {
    //            if (ddlDepartment2.Items.Count == 1)
    //                objCommon.DisplayMessage(this.updFacAllot, "Please Select Degree and  Department", this.Page);
    //            else
    //                objCommon.DisplayMessage(this.updFacAllot, "Please Select Department", this.Page);

    //            lvCourse2.DataSource = null;
    //            lvCourse2.DataBind();
    //            pnlList2.Visible = false;
    //        }

    //    }
    //    private void ShowReport2(string reportTitle, string rptFileName)
    //    {
    //        try
    //        {
    //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //            url += "Reports/CommonReport.aspx?";
    //            url += "pagetitle=" + reportTitle;
    //            url += "&path=~,Reports,Academic," + rptFileName;
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_BOS_DEPTNO=" + Convert.ToInt32(ddlDepartment2.SelectedValue) + ",@P_CCODE=,@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester2.SelectedValue);
    //            string Script = string.Empty;
    //            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //            ScriptManager.RegisterClientScriptBlock(this.updFacAllot, updFacAllot.GetType(), "Report", Script, true);
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //        }
    //    }
    #endregion

    //#region TAB2
    //protected void btnShow2_Click(object sender, EventArgs e)
    //{
    //    BindListView2();
    //}

    //protected void btnSubmit2_Click(object sender, EventArgs e)
    //{
    //    foreach (ListViewItem item in lvCourse2.Items)
    //    {
    //        Label lblSemester = item.FindControl("lblSemester") as Label;
    //        Label lblCCode = item.FindControl("lblCCode") as Label;
    //        CheckBox chkRecieved = item.FindControl("chkRecieved") as CheckBox;
    //        CheckBox chkCancel = item.FindControl("chkCancel") as CheckBox;
    //        if (chkRecieved.Enabled)
    //        {
    //        }
    //    }
    //}
    //protected void btnCancel2_Click(object sender, EventArgs e)
    //{
    //    lvCourse2.Visible = false;
    //    lvCourse2.DataSource = null;
    //    lvCourse2.DataBind();
    //}

    //private void BindListView2()
    //{
    //    DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.SEMESTERNO= C.SEMESTERNO AND P.CCODE = C.CCODE)", "P.SEMESTERNO,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER", "P.CCODE,COURSE_NAME,ISNULL(RECEIVED,0)RECEIVED,ISNULL(CANCEL,0) CANCEL", " APPROVED =" + ddlFaculty.SelectedValue, "SEMESTERNO,CCODE");
    //    if (ds != null && ds.Tables.Count > 0)
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvCourse2.DataSource = ds;
    //            lvCourse2.DataBind();
    //            lvCourse2.Visible = false;
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updFacAllot, "No Record Found!", this.Page);
    //            lvCourse2.Visible = false;
    //            lvCourse2.DataSource = null;
    //            lvCourse2.DataBind();
    //        }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.updFacAllot, "No Record Found!", this.Page);
    //        lvCourse2.Visible = false;
    //        lvCourse2.DataSource = null;
    //        lvCourse2.DataBind();
    //    }

    //}
    //#endregion

    #region TAB3 ISSUE LETTER

    #region "General"
    private void FillDropdown()
    {
        try
        {
            if (ddlClgname.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 and ISNULL(IS_ACTIVE,0)=1  and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlSession3, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_PNAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "S.SESSIONID DESC"); //--AND FLOCK = 1
                }

                ddlSession3.Focus();
            }
            else
            {

                ddlSemester3.SelectedIndex = 0;
                ddlCourse3.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    #endregion "General"

    #region "SelectedIndexChanged"

    protected void ddlSession3_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession3.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester3, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID =1  AND C.MAXMARKS_E > 0  AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), " S.SEMESTERNO");
                ddlSemester3.Focus();
            }
            else
            {
                ddlSemester3.SelectedIndex = 0;
            }
            lvLetter.DataSource = null;
            lvLetter.DataBind();
            pnlList3.Visible = false;
            btnSubmit3.Visible = false;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ddlSession3_OnSelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlScheme.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlSemester3, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND (CANCEL IS NULL OR CANCEL = 0)) INNER JOIN ACD_COURSE C ON (C.CCODE = P.CCODE AND C.BOS_DEPTNO = P.BOS_DEPTNO)INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "(P.CANCEL = 0 OR P.CANCEL IS NULL) AND S.SEMESTERNO >0  AND C.SCHEMENO =" + ddlScheme.SelectedValue + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession3.SelectedValue), " S.SEMESTERNO");
    //        }
    //        {
    //            ddlSemester3.SelectedIndex = 0;
    //            ddlCourse3.SelectedIndex = 0;
    //            lvLetter.DataSource = null;
    //            lvLetter.DataBind();
    //            //lvLetter.Visible = false;
    //            pnlList3.Visible = false;
    //        }



    //        btnSubmit3.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ddlScheme_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    protected void ddlSemester3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlCourse3, "ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO) LEFT OUTER JOIN ACD_STAFF  S  ON (S.STAFFNO = APPROVED)", "DISTINCT C.CCODE", "C.CCODE +  ' - '+ COURSE_NAME", "SM.SESSIONID = " + Convert.ToInt32(ddlSession3.SelectedValue) + " AND C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND s.STAFFNO > 0 AND DEAN_LOCK = 1 AND (P.CCODE = '' OR '' ='') AND P.SEMESTERNO = " + ddlSemester3.SelectedValue, "CCODE");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        BindListView3();
    }
    protected void ddlCourse3_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView3();
    }

    protected void SaveRecord()
    {
        try
        {
            Exam objexame = new Exam();

            foreach (ListViewDataItem lvitem in lvLetter.Items)
            {
                Label lblCourse = lvitem.FindControl("lblCourse") as Label;
                Label lblPaperSetterCode1 = lvitem.FindControl("lblPaperSetterCode1") as Label;
                Label lblPaperSetterCode2 = lvitem.FindControl("lblPaperSetterCode2") as Label;
                Label lblPaperSetterCode3 = lvitem.FindControl("lblPaperSetterCode3") as Label;
                Label lblPaperSetterCode4 = lvitem.FindControl("lblPaperSetterCode4") as Label;
                Label lblPaperSetterCode5 = lvitem.FindControl("lblPaperSetterCode5") as Label;


                TextBox TxtFaculty1 = lvitem.FindControl("TxtFaculty1") as TextBox;
                TextBox TxtFaculty2 = lvitem.FindControl("TxtFaculty2") as TextBox;
                TextBox TxtFaculty3 = lvitem.FindControl("TxtFaculty3") as TextBox;
                TextBox TxtFaculty4 = lvitem.FindControl("TxtFaculty4") as TextBox;
                TextBox TxtFaculty5 = lvitem.FindControl("TxtFaculty5") as TextBox;

                TextBox txtDtReportingFac1 = lvitem.FindControl("txtDtReportingFac1") as TextBox;
                TextBox txtDtReportingFac2 = lvitem.FindControl("txtDtReportingFac2") as TextBox;
                TextBox txtDtReportingFac3 = lvitem.FindControl("txtDtReportingFac3") as TextBox;
                TextBox txtDtReportingFac4 = lvitem.FindControl("txtDtReportingFac4") as TextBox;
                TextBox txtDtReportingFac5 = lvitem.FindControl("txtDtReportingFac5") as TextBox;

                Label lblCourseNo = lvitem.FindControl("lblCourseNo") as Label;

                int letterno = Convert.ToInt32(lblCourseNo.ToolTip.ToString());

                if (TxtFaculty1.Enabled == true || TxtFaculty2.Enabled == true || TxtFaculty3.Enabled == true || TxtFaculty4.Enabled == true || TxtFaculty5.Enabled == true)
                {
                    if ((TxtFaculty1.Text != "") || (TxtFaculty2.Text != "") || (TxtFaculty3.Text != "") || (TxtFaculty4.Text != "") || (TxtFaculty5.Text != ""))
                    {
                        // code for extracting Faculty code from string
                        int faculty1 = 0;
                        int faculty2 = 0;
                        int faculty3 = 0;
                        int faculty4 = 0;
                        int faculty5 = 0;

                        if (TxtFaculty1.Text != "" && TxtFaculty1.Enabled == true)
                        {
                            faculty1 = Convert.ToInt32(TxtFaculty1.Text.Substring((TxtFaculty1.Text.IndexOf("[") + 1), TxtFaculty1.Text.IndexOf("]") - (TxtFaculty1.Text.IndexOf("[") + 1)));
                        }
                        else
                        {
                            faculty1 = Convert.ToInt32(TxtFaculty1.ToolTip.ToString());
                        }
                        if (TxtFaculty2.Text != "" && TxtFaculty2.Enabled == true)
                        {
                            faculty2 = Convert.ToInt32(TxtFaculty2.Text.Substring((TxtFaculty2.Text.IndexOf("[") + 1), TxtFaculty2.Text.IndexOf("]") - (TxtFaculty2.Text.IndexOf("[") + 1)));
                        }
                        else
                        {
                            faculty2 = Convert.ToInt32(TxtFaculty2.ToolTip.ToString());
                        }
                        if (TxtFaculty3.Text != "" && TxtFaculty3.Enabled == true)
                        {
                            faculty3 = Convert.ToInt32(TxtFaculty3.Text.Substring((TxtFaculty3.Text.IndexOf("[") + 1), TxtFaculty3.Text.IndexOf("]") - (TxtFaculty3.Text.IndexOf("[") + 1)));
                        }
                        else
                        {
                            faculty3 = Convert.ToInt32(TxtFaculty3.ToolTip.ToString());
                        }
                        if (TxtFaculty4.Text != "" && TxtFaculty4.Enabled == true)
                        {
                            faculty4 = Convert.ToInt32(TxtFaculty4.Text.Substring((TxtFaculty4.Text.IndexOf("[") + 1), TxtFaculty4.Text.IndexOf("]") - (TxtFaculty4.Text.IndexOf("[") + 1)));
                        }
                        else
                        {
                            faculty4 = Convert.ToInt32(TxtFaculty4.ToolTip.ToString());
                        }
                        if (TxtFaculty5.Text != "" && TxtFaculty5.Enabled == true)
                        {
                            faculty5 = Convert.ToInt32(TxtFaculty5.Text.Substring((TxtFaculty5.Text.IndexOf("[") + 1), TxtFaculty5.Text.IndexOf("]") - (TxtFaculty5.Text.IndexOf("[") + 1)));
                        }
                        else
                        {
                            faculty5 = Convert.ToInt32(TxtFaculty5.ToolTip.ToString());
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion "SelectedIndexChanged"

    #region "Submit"

    protected void btnSubmit3_Click(object sender, EventArgs e)
    {
        string staffno = string.Empty;
        int count = 0;
        string ccode = string.Empty;
        DataSet ds = new DataSet();
        PStaffController objPStaff = new PStaffController();
        if (ddlCourse3.SelectedIndex > 0)
        {
            ccode = ddlCourse3.SelectedValue;
        }

        foreach (ListViewItem item in lvLetter.Items)
        {
            CheckBox chkFac = item.FindControl("chkFac") as CheckBox;
            //if (chkFac.Checked && chkFac.Enabled)
            if (chkFac.Checked)
            {
                staffno = count == 0 ? chkFac.ToolTip : staffno + "$" + chkFac.ToolTip;
                count++;
            }
        }
        if (staffno == "")
        {
            objCommon.DisplayMessage(this.upIssueletter, "Please Select at least one paper setter to issue letter", this.Page);
        }
        else
        {
            ds = objPStaff.PaperSetIssueLetter(Convert.ToInt32(ddlSession3.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), ccode, Convert.ToInt32(ddlSemester3.SelectedValue), staffno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ShowReport3("Issue_Letter", "rptPaperSetIssueLetter.rpt", staffno);
            }
            else
            {
                objCommon.DisplayMessage(this.upIssueletter, "No Record found!", this.Page);
            }
        }

    }
    protected void btnNotRcvdLetter_Click(object sender, EventArgs e)
    {
        string staffno = string.Empty;
        int count = 0;
        foreach (ListViewItem item in lvLetter.Items)
        {
            CheckBox chkFac = item.FindControl("chkFac") as CheckBox;
            ////if (chkFac.Checked && chkFac.Enabled)
            if (chkFac.Checked)
            {
                staffno = count == 0 ? chkFac.ToolTip : staffno + "$" + chkFac.ToolTip;
                count++;
            }
        }
        if (staffno == "")
        {
            objCommon.DisplayMessage(this.upIssueletter, "Please Select at least one paper setter to issue letter", this.Page);
        }
        else
        {
            ShowReport3("Pending_Paperset_Letter", "rptPaperSetPendingLetter.rpt", staffno);
        }
    }
    protected void btnSubmit4_Click(object sender, EventArgs e)
    {
        string staffno = string.Empty;
        string ccode = string.Empty;
        int count = 0;
        foreach (ListViewItem item in lvLetter.Items)
        {
            CheckBox chkFac = item.FindControl("chkFac") as CheckBox;
            ////Label lblCcode = item.FindControl("lblCcode") as Label;

            ////if (chkFac.Checked && chkFac.Enabled)
            if (chkFac.Checked)
            {
                staffno = count == 0 ? chkFac.ToolTip : staffno + "$" + chkFac.ToolTip;
                count++;
            }
        }
        if (staffno == "")
        {
            objCommon.DisplayMessage(this.upIssueletter, "Please Select at least one Paper Setter!", this.Page);
        }
        else

            ////ShowReport3("Issue_Letter", "rptPaperSetIssueLetter.rpt", staffno);
            ShowReport3("Issue_Letter", "rptPaperSetOrder.rpt", staffno);
    }
    protected void btnClear3_Click(object sender, EventArgs e)
    {
        //tbc1.ActiveTabIndex = 2;
        //ddlSession2.SelectedIndex = 0;
        ddlSession3.Items.Clear();
        ddlSession3.Items.Add(new ListItem("Please Select", "0"));
       // ddlSession3.SelectedIndex = 0;
       // ddlScheme.SelectedIndex = 0;
        ddlSemester3.Items.Clear();
        ddlSemester3.Items.Add(new ListItem("Please Select", "0"));
       // ddlSemester3.SelectedIndex = 0;
        ddlCourse3.Items.Clear();
        ddlCourse3.Items.Add(new ListItem("Please Select", "0"));
        //ddlCourse3.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        lvLetter.DataSource = null;
        lvLetter.DataBind();
        pnlList3.Visible = false;
    }

    #endregion "Submit"

    private void ShowReport3(string reportTitle, string rptFileName, string staff)
    {
        try
        {
            string ccode = string.Empty;
            int clg_id = Convert.ToInt32(ViewState["college_id"]);
            if (ddlCourse3.SelectedIndex > 0)
            {
                ccode = ddlCourse3.SelectedValue;
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                url += "&param=@P_SESSIONID=" + Convert.ToInt32(ddlSession3.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_CCODE=" + ccode + ",@P_SEMESTERNO=" + ddlSemester3.SelectedValue + ",@P_STAFFNO=" + staff + ",@P_COLLEGE_CODE=" + clg_id;
            }
            else
            {
                url += "&param=@P_SESSIONID=" + Convert.ToInt32(ddlSession3.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_CCODE=" + ccode + ",@P_SEMESTERNO=" + ddlSemester3.SelectedValue + ",@P_STAFFNO=" + staff + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
            // code for showing reports
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.upIssueletter, this.upIssueletter.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "IssueLetterToFac.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView3()
    {
        string Scheme = ViewState["schemeno"].ToString();
        string sem = ddlSemester3.SelectedValue;
        string ccode = ddlCourse3.SelectedValue == "0" ? "" : ddlCourse3.SelectedValue;

        //DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )A ON (A.STAFFNO = P.FACULTY1 AND APPROVED1 = 1)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )B ON (B.STAFFNO = P.FACULTY2 AND APPROVED2 = 1)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )D ON (D.STAFFNO = P.FACULTY3 AND APPROVED3 = 1)", "DISTINCT ISNULL(A.STAFF_NAME,'-') AS NAME1", "(CASE WHEN A.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN A.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC1,ISNULL(B.STAFF_NAME,'-') AS NAME2,(CASE WHEN B.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN B.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC2,ISNULL(D.STAFF_NAME,'-') AS NAME3,(CASE WHEN D.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN D.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC3", "SESSIONNO = " + ddlSession3.SelectedValue + " AND (P.BOS_DEPTNO = " + dept + " OR " + dept + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED1 IS NOT NULL AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", string.Empty);
        ////DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)LEFT OUTER JOIN  ACD_STAFF  S ON (S.STAFFNO = P.APPROVED )", "DISTINCT  P.CCODE,COURSE_NAME,P.SEMESTERNO,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER,ISNULL(RECEIVED,0)RECEIVED,ISNULL(APPROVED,0)APPROVED,ISNULL(P.CANCEL,0)CANCEL,ISNULL(STAFF_NAME,'-') AS NAME", "(CASE WHEN S.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN S.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC", "SESSIONNO = " + ddlSession3.SelectedValue + " AND (P.BOS_DEPTNO = " + dept + " OR " + dept + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", " P.SEMESTERNO,P.CCODE");
        //DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO) LEFT OUTER JOIN  ACD_STAFF  S ON (S.STAFFNO = P.APPROVED )", "DISTINCT  P.CCODE,COURSE_NAME,P.SEMESTERNO,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER,ISNULL(RECEIVED,0)RECEIVED,ISNULL(APPROVED,0)APPROVED,ISNULL(P.CANCEL,0)CANCEL,ISNULL(STAFF_NAME,'-') AS NAME", "(CASE WHEN S.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN S.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC", "SM.SESSIONID = " + Convert.ToInt32(ddlSession3.SelectedValue) + " AND (P.BOS_DEPTNO = " + dept + " OR " + dept + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", " P.SEMESTERNO,P.CCODE");
        DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO) LEFT OUTER JOIN  ACD_STAFF  S ON (S.STAFFNO = P.APPROVED )", "DISTINCT  P.CCODE,COURSE_NAME,P.SEMESTERNO,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER,ISNULL(RECEIVED,0)RECEIVED,ISNULL(APPROVED,0)APPROVED,ISNULL(P.CANCEL,0)CANCEL,ISNULL(STAFF_NAME,'-') AS NAME", "(CASE WHEN S.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN S.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC", "SM.SESSIONID = " + Convert.ToInt32(ddlSession3.SelectedValue) + " AND (C.SCHEMENO = " + Scheme + " OR " + Scheme + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", " P.SEMESTERNO,P.CCODE");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvLetter.DataSource = ds;
            lvLetter.DataBind();

            pnlList3.Visible = true;
            btnSubmit3.Visible = true;
        }
        else
        {
            lvLetter.DataSource = null;
            lvLetter.DataBind();
            pnlList3.Visible = false;
            btnSubmit3.Visible = false;
        }

    }


    #endregion

    //#region TAB4 RECIEVING STATUS
    //#region "General"
    //private void BindListView4()
    //{
    //    string dept = ddlDepartment4.SelectedValue;
    //    string sem = ddlSemester4.SelectedValue;
    //    string ccode = ddlCourse4.SelectedValue == "0" ? "" : ddlCourse4.SelectedValue;

    //    //DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )A ON (A.STAFFNO = P.FACULTY1 AND APPROVED1 = 1)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )B ON (B.STAFFNO = P.FACULTY2 AND APPROVED2 = 1)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )D ON (D.STAFFNO = P.FACULTY3 AND APPROVED3 = 1)", "DISTINCT ISNULL(A.STAFF_NAME,'-') AS NAME1", "(CASE WHEN A.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN A.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC1,ISNULL(B.STAFF_NAME,'-') AS NAME2,(CASE WHEN B.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN B.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC2,ISNULL(D.STAFF_NAME,'-') AS NAME3,(CASE WHEN D.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN D.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC3", "SESSIONNO = " + ddlSession3.SelectedValue + " AND (P.BOS_DEPTNO = " + dept + " OR " + dept + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED1 IS NOT NULL AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", string.Empty);
    //    DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)INNER  JOIN ACD_STAFF  S ON (S.STAFFNO = P.APPROVED  AND( S.CANCEL=0 OR S.CANCEL IS NULL ))", " DISTINCT  P.CCODE,COURSE_NAME,P.SEMESTERNO,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER,APPROVED,ISNULL(RECEIVED,0)RECEIVED,ISNULL(P.CANCEL,0)CANCEL,(CASE WHEN FACULTY1 = APPROVED THEN QT1 WHEN FACULTY2 = APPROVED THEN QT2 WHEN FACULTY3 = APPROVED THEN QT3 END)QTY,(CASE WHEN FACULTY1 = APPROVED THEN MOI1 WHEN FACULTY2 = APPROVED THEN MOI2 WHEN FACULTY3 = APPROVED THEN MOI3 END)MOI,QT_RCVD,MOI_RCVD,ISNULL(S.STAFF_NAME,'-') AS NAME", "(CASE WHEN S.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN S.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC", "SESSIONNO = " + ddlSession4.SelectedValue + " AND (P.BOS_DEPTNO = " + dept + " OR " + dept + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND APPROVED > 0 AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", " P.SEMESTERNO,P.CCODE");
    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        lvCourse4.DataSource = ds;
    //        lvCourse4.DataBind();
    //        pnlList4.Visible = true;
    //        btnSubmit5.Visible = true;

    //    }
    //    else
    //    {
    //        lvCourse4.DataSource = null;
    //        lvCourse4.DataBind();
    //        pnlList4.Visible = false;
    //        btnSubmit5.Visible = false;
    //    }

    //}
    //#endregion "General"

    //#region "SelectedIndexChanged"
    //protected void ddlDepartment4_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlDepartment4.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlSemester4, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND DEAN_LOCK = 1 AND BOS_LOCK = 1 AND (RECEIVED IS NULL OR RECEIVED = 0))", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0  AND P.BOS_DEPTNO =" + ddlDepartment4.SelectedValue + " AND P.SESSIONNO = " + ddlSession4.SelectedValue, " S.SEMESTERNO");
    //        }
    //        else
    //            ddlSemester4.SelectedIndex = 0;

    //        lvCourse4.DataSource = null;
    //        lvCourse4.DataBind();
    //        pnlList4.Visible = false;
    //        // btnSubmit5.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ddlDepartment3_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    // BindListView4();
    //}

    //protected void ddlSemester4_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlSemester4.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlCourse4, "ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)INNER JOIN ACD_STAFF  S ON (S.STAFFNO =  APPROVED)", "DISTINCT C.CCODE", "C.CCODE +  ' - '+ COURSE_NAME", "SESSIONNO = " + ddlSession4.SelectedValue + " AND P.BOS_DEPTNO = " + ddlDepartment4.SelectedValue + " AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND DEAN_LOCK = 1  AND P.SEMESTERNO = " + ddlSemester4.SelectedValue, "CCODE");
    //            BindListView4();
    //        }
    //        else
    //        {
    //            ddlCourse4.Items.Clear();
    //            ddlCourse4.Items.Add(new ListItem("Please Select", "0"));
    //            lvCourse4.DataSource = null;
    //            lvCourse4.DataBind();
    //            pnlList4.Visible = false;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void ddlCourse4_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindListView4();
    //}

    //#endregion "SelectedIndexChanged"

    //#region "Submit"

    //protected void btnRcvdAll_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        CourseController objCC = new CourseController();
    //        string ccode = ddlCourse4.SelectedValue == "0" ? "" : ddlCourse4.SelectedValue;
    //        int ret = 0;
    //        foreach (ListViewItem item in lvCourse4.Items)
    //        {
    //            TextBox txtRcvdQTY = item.FindControl("txtRcvdQTY") as TextBox;
    //            TextBox txtRcvdMOI = item.FindControl("txtRcvdMOI") as TextBox;
    //            Label lblCcode = item.FindControl("lblCcode") as Label;
    //            Label lblQTY = item.FindControl("lblQTY") as Label;
    //            Label lblMOI = item.FindControl("lblMOI") as Label;
    //            Label lblFaculty1 = item.FindControl("lblFaculty1") as Label;

    //            int rcvdQTY = txtRcvdQTY.Text == "" ? 0 : Convert.ToInt16(txtRcvdQTY.Text);
    //            int rcvdMoi = txtRcvdMOI.Text == "" ? 0 : Convert.ToInt16(txtRcvdMOI.Text);
    //            int rcvd = Convert.ToInt16(lblQTY.Text) <= Convert.ToInt16(txtRcvdQTY.Text) ? 1 : 0;
    //            ret = objCC.AddPaperSetReceivedStatus(Convert.ToInt16(ddlSession4.SelectedValue), Convert.ToInt16(ddlDepartment4.SelectedValue), Convert.ToInt16(ddlSemester4.SelectedValue), rcvdQTY, rcvdMoi, rcvd, lblCcode.Text, Convert.ToInt16(lblFaculty1.ToolTip));
    //            if (ret != Convert.ToInt32(CustomStatus.Error))
    //                objCommon.DisplayMessage(this.updRcvSatus, "Satus received for paper setter!", this.Page);
    //        }
    //        BindListView4();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void btnRCVd_Click(object sender, EventArgs e)
    //{
    //    Button btn = sender as Button;
    //    try
    //    {
    //        CourseController objCC = new CourseController();
    //        string ccode = ddlCourse4.SelectedValue == "0" ? "" : ddlCourse4.SelectedValue;
    //        foreach (ListViewItem item in lvCourse4.Items)
    //        {
    //            Button btnRCVd1 = item.FindControl("btnRCVd1") as Button;
    //            if (btnRCVd1.GetHashCode() == btn.GetHashCode())
    //            {
    //                TextBox txtRcvdQTY = item.FindControl("txtRcvdQTY") as TextBox;
    //                TextBox txtRcvdMOI = item.FindControl("txtRcvdMOI") as TextBox;
    //                Label lblCcode = item.FindControl("lblCcode") as Label;
    //                Label lblQTY = item.FindControl("lblQTY") as Label;
    //                Label lblMOI = item.FindControl("lblMOI") as Label;
    //                int rcvdQTY = txtRcvdQTY.Text == "" ? 0 : Convert.ToInt16(txtRcvdQTY.Text);
    //                int rcvdMoi = txtRcvdMOI.Text == "" ? 0 : Convert.ToInt16(txtRcvdMOI.Text);
    //                int rcvd = Convert.ToInt16(lblQTY.Text) <= Convert.ToInt16(txtRcvdQTY.Text) ? 1 : 0;
    //                int ret = objCC.AddPaperSetReceivedStatus(Convert.ToInt16(ddlSession4.SelectedValue), Convert.ToInt16(ddlDepartment4.SelectedValue), Convert.ToInt16(ddlSemester4.SelectedValue), rcvdQTY, rcvdMoi, rcvd, lblCcode.Text, Convert.ToInt16(btn.CommandArgument));
    //                if (ret != Convert.ToInt32(CustomStatus.Error))
    //                    objCommon.DisplayMessage(this.updRcvSatus, "Satus received for paper setter!", this.Page);
    //                BindListView4();
    //                return;
    //            }
    //        }
    //        BindListView4();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void btnCancelStatus_Click(object sender, EventArgs e)
    //{
    //    Button btn = sender as Button;
    //    try
    //    {
    //        CourseController objCC = new CourseController();
    //        // string ccode = ddlCourse4.SelectedValue == "0" ? "" : ddlCourse4.SelectedValue;

    //        int ret = objCC.CancelPaperSetEntry(Convert.ToInt16(ddlSession4.SelectedValue), Convert.ToInt16(ddlDepartment4.SelectedValue), Convert.ToInt16(ddlSemester4.SelectedValue), btn.CommandName, Convert.ToInt16(btn.CommandArgument));
    //        if (ret != Convert.ToInt32(CustomStatus.Error))
    //            objCommon.DisplayMessage(this.updRcvSatus, "Cancelled this entry!", this.Page);
    //        BindListView4();
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void btnClear5_Click(object sender, EventArgs e)
    //{
    //    //tbc1.ActiveTabIndex = 3;
    //    //Response.Redirect(Request.Url.ToString());
    //    tbc1.ActiveTabIndex = 2;
    //    ddlSession4.SelectedIndex = 0;
    //    ddlDepartment4.SelectedIndex = 0;
    //    ddlSemester4.SelectedIndex = 0;
    //    ddlCourse4.SelectedIndex = 0;
    //    lvCourse4.DataSource = null;
    //    lvCourse4.DataBind();
    //    pnlList4.Visible = false;
    //}

    //#endregion "Submit"


    //#endregion

    private void FillDropdownCollege()
    {
        try
        {
            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
            //AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
            else

                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDropdown();  //added 17-2-23
    }
}

