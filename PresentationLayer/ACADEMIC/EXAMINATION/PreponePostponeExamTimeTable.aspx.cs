//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Exam Days Management                                                         
// CREATION DATE : 10-SEPT-2012                                                         
// CREATED BY    : ASHISH MOTGHARE                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;


public partial class ACADEMIC_EXAMINATION_PreponePostponeExamTimeTable : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                // Set form mode equals to -1(New Mode).
                //ViewState["exdtno"] = "0";

                this.PopulateDropDown();
                this.BindDates();
                divMsg.InnerHtml = string.Empty;
                lvDate.Enabled = true;
            }
        }
    }

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //ddlDayNo.Enabled = false;
    //        //txtExamDate.Enabled = false;
    //        txtExamDate.Enabled = true;
    //        ImageButton btnEditRecord = sender as ImageButton;

    //        DataSet ds = objExamController.GetSingleExamDay(int.Parse(btnEditRecord.CommandArgument));
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ViewState["exdtno"] = btnEditRecord.CommandArgument;
    //                ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
    //                ddlExTTType.SelectedValue = ds.Tables[0].Rows[0]["EXAM_TT_TYPE"].ToString();
    //                ddlDayNo.SelectedValue = ds.Tables[0].Rows[0]["DAYNO"].ToString();
    //                txtExamDate.Text = ds.Tables[0].Rows[0]["EXAMDATE"].ToString();
    //                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
    //                //fills ddlbranch
    //                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
    //                //ddlBranch.Focus();

    //                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();

    //                // Scheme Name
    //                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue), "B.BRANCHNO");
    //                //ddlScheme.Focus();
    //                ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();


    //                //SemesterNo
    //                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
    //                this.BindExamDates();

    //                ddlSemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

    //                //fills course
    //                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON C.SEMESTERNO = SM.SEMESTERNO", "C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME AS COURSE_NAME", "C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "AND C.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "C.CCODE");
    //                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON C.SEMESTERNO = SM.SEMESTERNO INNER JOIN ACD_ELECTGROUP G ON C.GROUPNO = G.GROUPNO INNER JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO ", "C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME  AS COURSE_NAME", "C.SUBID = 1 AND C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "AND C.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "C.CCODE");
    //                //ddlCourse.Focus();

    //                ddlCourse.SelectedValue = ds.Tables[0].Rows[0]["COURSENO"].ToString();

    //                ddlSlot.SelectedValue = ds.Tables[0].Rows[0]["SLOTNO"].ToString();
    //                ddlSession.Enabled = false;

    //                //ddlDay.SelectedValue = ds.Tables[0].Rows[0]["DAYNAME"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    private string GetExdtNo()
    {
        string retExdtNo = string.Empty;
        foreach (ListViewDataItem item in lvExamday.Items)
        {
            CheckBox chkExdtNo = item.FindControl("chkExdtNO") as CheckBox;
           
            //if (chk.Checked && ddlExamType.SelectedValue=="0")
            //{
            //    if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
            //    else
            //        retIDNO += "$" + lblStudname.ToolTip.ToString();
            //}
            //else if (chk.Checked && ddlExamType.SelectedValue == "1")
            //{
            //    if (retIDNO.Length == 0) retIDNO = lblStudname.ToolTip.ToString();
            //    else
            //        retIDNO += "$" + lblStudname.ToolTip.ToString();
            //}
            //else if (ddlExamType.SelectedValue == "1")
            //{
            //    retIDNO += "$" + lblStudname.ToolTip.ToString();
            //}
            if (chkExdtNo.Checked)
            {
                if (retExdtNo.Length == 0) retExdtNo = chkExdtNo.ToolTip.ToString() + "$";
                else
                    retExdtNo +=  chkExdtNo.ToolTip.ToString() + "$";
            }
        }
        if (retExdtNo.Equals("")) return "0";
        else return retExdtNo;
        //return retIDNO;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
        ddlDayNo.Enabled = true;
        txtExamDate.Enabled = true;
        try
        {
            string ExdtNo = GetExdtNo();
            if (ExdtNo == "0")
            {
                objCommon.DisplayMessage(updExamdate, "Please Select At least One selection From Following List!!", this.Page);
                return;
            }
            else
            {
               Exam objExam = new Exam();
             
                   objExam.Dayno = Convert.ToInt32(ddlDayNo1.SelectedValue);
               
               
                   objExam.Examdate = Convert.ToDateTime(txtExamDate1.Text);
               
                   objExam.Slot = Convert.ToInt32(ddlSlot1.SelectedValue);
               
               CustomStatus cs = (CustomStatus)objExamController.UpdateExamDayForSelection(ExdtNo, objExam);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ddlSession.Enabled = true;
                    this.Clear();
                    this.BindExamDates();
                    PanelLvExamDays.Visible = false;
                    objCommon.DisplayMessage(updExamdate, "Exam Day Updated Successfully!", this.Page);
                    //PanelLvDate.Visible = false;
                    //Clear();
                    ddlDayNo1.SelectedIndex = 0;
                    ddlSlot1.SelectedIndex = 0;
                    txtExamDate1.Text=null;
                }
            }
            this.BindDates();
            lvDate.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #region User-Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamDate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamDate.aspx");
        }
    }

    private void BindDates()
    {
        DataSet dsBC;
        dsBC = objExamController.ViewDateForSelection(Convert.ToInt32(ddlSession.SelectedValue));
        if (dsBC != null && dsBC.Tables.Count > 0)
        {
            lvDate.DataSource = dsBC.Tables[0];
            lvDate.DataBind();
        }
        else
        {
            objCommon.DisplayMessage("Data Not Found", this);
        }

    }

    private void BindExamDates()
    {
        try
        {
            //DataSet ds = objExamController.GetAllExamDay(Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToInt32(ddlScheme.SelectedValue),Convert.ToInt32(ddlSemester.SelectedValue));
            //DataSet ds = objExamController.GetAllExamDayForChange(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlDayNo.SelectedValue), Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        lvExamday.DataSource = ds.Tables[0];
            //        lvExamday.DataBind();
            //    }
            //    else
            //    {
            //        lvExamday.DataSource = null;
            //        lvExamday.DataBind();
            //        PanelLvExamDays.Visible = false;

            //    }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.BindExamDates() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //Term
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            //ddlSession.SelectedIndex = 1;
            //objCommon.FillDropDownList(ddlSessionReport, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");

            //Exam Slot
           

            ////SemesterNo
            //objCommon.FillDropDownList(ddlSemesdter, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");

            //Degree Name
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlDegreeReport, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND DEGREENO > 0", "DEGREENO");

            ////Exam Time Table Types
            //ddlExTTType.Items.Add(new ListItem("Please Select", "0"));
            //ddlExTTType.Items.Add(new ListItem("Mid Exam Time Table", "1"));
            //ddlExTTType.Items.Add(new ListItem("End Exam Time Table", "2"));

            //ddlExTTTypeReport.Items.Add(new ListItem("Please Select", "0"));
            //ddlExTTTypeReport.Items.Add(new ListItem("Mid Exam Time Table", "1"));
            //ddlExTTTypeReport.Items.Add(new ListItem("End Exam Time Table", "2"));
            //Exam Day Nos.
            ddlDayNo.Items.Add(new ListItem("Please Select", "0"));
            for (int i = 1; i <= 80; i++)
            {
                ddlDayNo.Items.Add(new ListItem(i.ToString()));
            }
            ddlDayNo1.Items.Add(new ListItem("Please Select", "0"));
            for (int i = 1; i <= 80; i++)
            {
                ddlDayNo1.Items.Add(new ListItem(i.ToString()));
            }
            

            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));

            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {
        ddlSlot.SelectedIndex = 0;
        //ddlDay.SelectedIndex = 0;
        ddlSlot.Focus();
        ViewState["exdtno"] = "0";
        //Response.Redirect(Request.Url.ToString());
        ddlSession.SelectedIndex = 0;
       // ddlExTTType.SelectedIndex = 0;
        ddlDayNo.SelectedIndex = 0;
        txtExamDate.Text = string.Empty;
        ddlSlot.SelectedIndex = 0;
        ddlDegree.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlScheme.SelectedIndex = -1;
        ddlSemester.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        //ddlDay.SelectedIndex = -1;
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            // string exdtno = objCommon.LookUp("ACD_EXAM_DATE", "EXDTNO", "SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + " AND EXAM_TT_TYPE = " + ddlExTTType.SelectedValue + " AND DAYNO = " + ddlDayNo.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO = " + ddlDegree.SelectedValue + " AND convert(nvarchar(20),EXAMDATE,103)) = " + txtExamDate.Text + " AND SLOTNO = " + ddlSlot.SelectedValue);
            string exdtno = objCommon.LookUp("ACD_EXAM_DATE", "EXDTNO", "COURSENO=" + ddlCourse.SelectedValue + " AND  DAYNO=" + ddlDayNo.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND SLOTNO= " + ddlSlot.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue);

            if (exdtno != null && exdtno != string.Empty)
            {
                if (Convert.ToInt32(exdtno) == Convert.ToInt32(ViewState["exdtno"]))
                {
                    flag = false;
                }
                else
                    flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    // Below procedure is to restrict another paper entry for same branch and semester student in same date & slot 
    private bool CheckDuplicateStudentEntry()
    {
        bool flag = false;
        try
        {
            string Elect = objCommon.LookUp("ACD_Course", "ELECT", "COURSENO = " + ddlCourse.SelectedValue);
            string courseno = objCommon.LookUp("ACD_EXAM_DATE", "courseno", "DAYNO=" + ddlDayNo.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND SLOTNO= " + ddlSlot.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue);
            string Elect1 = objCommon.LookUp("ACD_Course", "ELECT", "COURSENO = " + Convert.ToInt16(courseno));

            if (Elect == "False")
            {
                // string exdtno = objCommon.LookUp("ACD_EXAM_DATE", "EXDTNO", "SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + " AND EXAM_TT_TYPE = " + ddlExTTType.SelectedValue + " AND DAYNO = " + ddlDayNo.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO = " + ddlDegree.SelectedValue + " AND convert(nvarchar(20),EXAMDATE,103)) = " + txtExamDate.Text + " AND SLOTNO = " + ddlSlot.SelectedValue);
                //string exdtno = objCommon.LookUp("ACD_EXAM_DATE", "EXDTNO", "COURSENO=" + ddlCourse.SelectedValue + " AND EXAM_TT_TYPE=" + ddlExTTType.SelectedValue + " AND DAYNO=" + ddlDayNo.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND SLOTNO= " +ddlSlot.SelectedValue +" AND SESSIONNO = " + ddlSession.SelectedValue );
                string exdtno = objCommon.LookUp("ACD_EXAM_DATE", "EXDTNO", " DAYNO=" + ddlDayNo.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND SLOTNO= " + ddlSlot.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue);

                if (exdtno != null && exdtno != string.Empty)
                {
                    if (Convert.ToInt32(exdtno) == Convert.ToInt32(ViewState["exdtno"]))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            else if (Elect == "True" && Elect1 == "True")
            {
                flag = false;
            }
            else
            {
                objCommon.DisplayMessage(updExamdate, "Existing Course for this slot not Elective Course.", this.Page);
                flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private bool CheckCourseDuplicateEntry()
    {
        bool flag = false;
        try
        {
            // string exdtno = objCommon.LookUp("ACD_EXAM_DATE", "EXDTNO", "SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + " AND EXAM_TT_TYPE = " + ddlExTTType.SelectedValue + " AND DAYNO = " + ddlDayNo.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO = " + ddlDegree.SelectedValue + " AND convert(nvarchar(20),EXAMDATE,103)) = " + txtExamDate.Text + " AND SLOTNO = " + ddlSlot.SelectedValue);
            string exdtno = objCommon.LookUp("ACD_EXAM_DATE", "EXDTNO", "COURSENO=" + ddlCourse.SelectedValue + " AND (DAYNO= 0 OR  0 = 0) AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue + " AND (SLOTNO=0 OR 0 = 0) AND SESSIONNO = " + ddlSession.SelectedValue);

            if (exdtno != null && exdtno != string.Empty)
            {
                if (Convert.ToInt32(exdtno) == Convert.ToInt32(ViewState["exdtno"]))
                {
                    flag = false;
                }
                else
                    flag = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.CheckDuplicateEntry() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    #endregion
    #region DropDownList
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedValue == "1" && ddlBranch.SelectedValue == "99")
            {
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.Items.Add(new ListItem("FIRST YEAR [R.T.M]", "24"));
                ddlScheme.Items.Add(new ListItem("FIRST YEAR [AUTONOMOUS]", "1"));
                ddlScheme.Focus();

            }
            else
            {

                // Scheme Name
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue), "B.BRANCHNO");
                ddlScheme.Focus();
                //this.BindExamDates();
                PanelLvExamDays.Visible = false;
            }
            this.BindExamDates();
            PanelLvExamDays.Visible = true;
            lvExamday.Enabled = true;
            lvExamday.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelLvDate.Visible = true;
        //this.BindExamDates();
        // this.BindDates();
        ddlDayNo.SelectedIndex = 0;
        txtExamDate.Text = "";
        txtExamDate.Enabled = true;
        lvExamday.Visible = false;
        //////this.BindExamDates();
        //////PanelLvExamDays.Visible = true;
        //////lvExamday.Enabled = true;
        //////lvExamday.Visible = true;

        objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT", "SLOTNO", "SLOTNAME", "SLOTNO>0", "SLOTNO");
        objCommon.FillDropDownList(ddlSlot1, "ACD_EXAM_TT_SLOT", "SLOTNO", "SLOTNAME", "SLOTNO>0", "SLOTNO");
        this.BindDates();
        ddlDayNo.SelectedIndex = 0;
        this.BindExamDates();
        PanelLvExamDays.Visible = true;
        lvExamday.Enabled = true;
        lvExamday.Visible = true;

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        // Branch Name
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.BRANCHNO");                               
        ddlBranch.Focus();
        PanelLvExamDays.Visible = false;
        this.BindExamDates();
        PanelLvExamDays.Visible = true;
        lvExamday.Enabled = true;
        lvExamday.Visible = true;

    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON C.SEMESTERNO = SM.SEMESTERNO INNER JOIN ACD_ELECTGROUP G ON C.GROUPNO = G.GROUPNO ", "C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME + '['+  G.GROUPNAME + ']' AS COURSE_NAME", "C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "AND C.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "C.CCODE");
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_SCHEME S ON C.SCHEMENO = S.SCHEMENO INNER JOIN ACD_SEMESTER SM ON C.SEMESTERNO = SM.SEMESTERNO INNER JOIN ACD_ELECTGROUP G ON C.GROUPNO = G.GROUPNO INNER JOIN ACD_DEGREE D ON S.DEGREENO = D.DEGREENO ", "C.COURSENO", "C.CCODE + ' - ' + C.COURSE_NAME  AS COURSE_NAME", "C.SUBID = 1 AND C.SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + "AND C.SEMESTERNO =" + Convert.ToInt32(ddlSemester.SelectedValue), "C.CCODE");

        ddlCourse.Focus();
        this.BindExamDates();
        PanelLvExamDays.Visible = true;
        lvExamday.Enabled = true;
        lvExamday.Visible = true;

    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedItem.Text == "FIRST YEAR [R.T.M]" || ddlScheme.SelectedItem.Text == "FIRST YEAR [AUTONOMOUS]")
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Add(new ListItem("I", "1"));
            ddlSemester.Items.Add(new ListItem("II", "2"));
            PanelLvExamDays.Visible = false;
        }
        else
        {
            //SemesterNo
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            PanelLvExamDays.Visible = false;
        }
        this.BindExamDates();
        PanelLvExamDays.Visible = true;
        lvExamday.Enabled = true;
        lvExamday.Visible = true;
    }
    //protected void ddlDegreeReport_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlBranchReport, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegreeReport.SelectedValue), "B.BRANCHNO");
    //    ddlBranch.Focus();
    //}

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StudCnt = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "COURSENO=" + ddlCourse.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue + " AND EXAM_REGISTERED =1");
        lblStudCnt.Text = "";
        if (Convert.ToInt32(StudCnt) > 0)
        {
            lblStudCnt.Text = "Total Registered Student(s)= " + StudCnt;
        }
        else
        {
            lblStudCnt.Text = "No Registered Student Found For Selected Course";
        }
        this.BindExamDates();
        PanelLvExamDays.Visible = true;
        lvExamday.Enabled = true;
        lvExamday.Visible = true;
    }

    #endregion



    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowReport("TimeTable_Report", "rptTimeTable.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_CoursewiseStudentReport2.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSessionReport.SelectedValue + ",@P_BRANCHNO=" + ddlBranchReport.SelectedValue + ",@P_EXAM_TYPE=" + ddlExTTTypeReport.SelectedValue + " ";
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";


    //        //To open new window from Updatepanel
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updExamdate, this.updExamdate.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Hostel_StudentHostelIdentityCard.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void btnCancelReport_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.Url.ToString());
    //}


    //Function For Deleting the Selected EXAM TIMETABLE ENTRY.
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ExamController objEXM = new ExamController();
    //        ImageButton btnDelete = sender as ImageButton;
    //        int EXDTNO = int.Parse(btnDelete.CommandArgument);

    //        ////Looks in Holiday_Master For Academic_session Entry.
    //        //int cnt = Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_HOLIDAY_MASTER", "COUNT(*)", "ACADEMIC_SESSIONNO = " + Convert.ToInt32(btnDelete.ToolTip)));
    //        //if (cnt <= 0)
    //        //{
    //        //Delete 
    //        CustomStatus cs = (CustomStatus)objEXM.DeleteExamDay(EXDTNO);
    //        objCommon.DisplayMessage(updExamdate, "EXAM TIMETABLE ENTRY Deleted Succesfully !!", this.Page);
    //        this.BindExamDates();
    //        return;

    //        //}
    //        //else
    //        //    objCommon.DisplayMessage(updExamdate,"Holiday Entry is Made for this Academic Session, Delete it First OR Contact Administrator!!", this.Page);
    //        //return;


    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_SessionCreate.btnDelete_Click-> " + ex.Message + "" + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable");
    //    }
    //}




    protected void ddlDayNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //we gets days no. as well as Exam dates on  LvDates bindlistview 
        DataSet dsBC = objExamController.EBindDate(Convert.ToInt32(ddlDayNo.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
        if (dsBC != null && dsBC.Tables[0].Rows.Count > 0)
        {
            txtExamDate.Text = dsBC.Tables[0].Rows[0][0].ToString();
            //txtExamDate.Text = dsBC.Tables[0].ToString();
            txtExamDate.Enabled = false;
        }
        else
        {
            txtExamDate.Text = "";
            txtExamDate.Enabled = true;

        }
        this.BindExamDates();
        PanelLvExamDays.Visible = true;
        lvExamday.Enabled = true;
        lvExamday.Visible = true;
    }

    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindExamDates();
        PanelLvExamDays.Visible = true;
        lvExamday.Enabled = true;
        lvExamday.Visible = true;
    }
}
