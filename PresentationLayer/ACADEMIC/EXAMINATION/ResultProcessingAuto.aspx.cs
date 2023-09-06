//======================================================================================
// PROJECT NAME  : UAIMS [NITRAIPUR]                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : RESULT PROCESSING                                    
// ADDED DATE    : 20-JUNE-2012                                                          
// CREATED BY    : NIRAJ D. PHALKE                                                      
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

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Threading;

public partial class ACADEMIC_EXAMINATION_ResultProcessingAuto : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    //ExamController objExamController = new ExamController();
    //Exam objExam = new Exam();
    ResultProcessing objResult = new ResultProcessing();

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    this.FillDropdown();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                    if (Session["usertype"].ToString() != "1")  //admin
                    {
                        btnLock.Visible = false;
                        btnUnlock.Visible = false;
                        btnProcess.Visible = false;
                    }                    
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=ResultProcessingAuto.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ResultProcessingAuto.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            ddlSession.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
            ddlBranch.Focus();
            if (ddlBranch.SelectedIndex > 0)
                ddlBranch.SelectedIndex = 0;
            if (ddlScheme.SelectedIndex > 0)
                ddlScheme.SelectedIndex = 0;
            if (ddlSemester.SelectedIndex > 0)
                ddlSemester.SelectedIndex = 0;
            if (ddlCategory.SelectedIndex > 0)
                ddlCategory.SelectedIndex = 0;
            if (ddlResultType.SelectedIndex > 0)
                ddlResultType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        btnProcess.Enabled = false;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedValue == "1" && ddlBranch.SelectedValue == "99")
            {
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                ddlScheme.Items.Add(new ListItem("FIRST YEAR[AUTONOMOUS]", "1"));
                ddlScheme.Focus();
            }
            else
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "S.SCHEMETYPE = 1 AND B.BRANCHNO = " + ddlBranch.SelectedValue, "B.BRANCHNO");
                ddlScheme.Focus();
            }
            if (ddlScheme.SelectedIndex > 0)
                ddlScheme.SelectedIndex = 0;
            if (ddlSemester.SelectedIndex > 0)
                ddlSemester.SelectedIndex = 0;
            if (ddlCategory.SelectedIndex > 0)
                ddlCategory.SelectedIndex = 0;
            if (ddlResultType.SelectedIndex > 0)
                ddlResultType.SelectedIndex = 0;
            btnProcess.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.ddlBranch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
            ddlSemester.SelectedIndex = 0;
        if (ddlCategory.SelectedIndex > 0)
            ddlCategory.SelectedIndex = 0;
        if (ddlResultType.SelectedIndex > 0)
            ddlResultType.SelectedIndex = 0;
        ddlStudent.SelectedIndex = 0;
        btnProcess.Enabled = false;
        ddlSemester.Focus();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.CheckLockStatus();
        btnProcess.Enabled = false;
        if (ddlCategory.SelectedIndex > 0)
            ddlCategory.SelectedIndex = 0;
        ddlCategory.Focus();
        if (ddlResultType.SelectedIndex > 0)
            ddlResultType.SelectedIndex = 0;
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
    ////    try
    ////    {
    ////        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
    ////        int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
    ////        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
    ////        int prev_status = Convert.ToInt32(ddlCategory.SelectedValue);



    ////        //this.CheckLockStatus();
    ////        //AS THE RESULT PROCESSING IS DONE SCHEMEWISE, HENCE NO NEED TO CHECK SCHEME CONDITION DIFFERENTLY FOR Ist YEAR AUTO.
    ////        DataSet dsStaus = objResult.GetStudentsWithNullGrade(sessionno, schemeno, semesterno, prev_status);
    ////        if (dsStaus.Tables[0].Rows.Count > 0)
    ////        {
    ////            lvGrade_Not_Alloted_Courses.DataSource = dsStaus;
    ////            lvGrade_Not_Alloted_Courses.DataBind();
    ////            btnProcess.Visible = false;
    ////            btnUnlock.Visible = false;
    ////            btnLock.Visible = false;

    ////        }
    ////        else
    ////        {
    ////            btnProcess.Visible = true;
    ////            btnUnlock.Visible = true;
    ////            btnLock.Visible = true;

    ////            lvGrade_Not_Alloted_Courses.DataSource = null;
    ////            lvGrade_Not_Alloted_Courses.DataBind();
    ////        }


    ////        /*code for filling student dropdown list.
    ////        //if (ddlCategory.SelectedIndex > 0)
    ////        //{
    ////        //    //objCommon.FillDropDownList (ddlStudent , "ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (S.IDNO = R.IDNO)", "DISTINCT S.IDNO", "(S.STUDNAME + ' ' + S.FATHERNAME + ' ' + S.LASTNAME) AS NAME,S.REGNO,R.ROLL_NO", "R.SESSIONNO = 51 AND R.SCHEMENO = 1 AND R.SEMESTERNO = 1 AND R.PREV_STATUS = 1" + ((ddlStudent.SelectedIndex == 0) ? string.Empty : "S.IDNO = " + ddlStudent.SelectedValue), "R.ROLL_NO");
    ////        //    DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (S.IDNO = R.IDNO)", "DISTINCT S.IDNO", "(S.STUDNAME + ' ' + S.FATHERNAME + ' ' + S.LASTNAME) AS NAME,S.REGNO,R.ROLL_NO", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SEMESTERNO = " + ddlSemester.SelectedValue + " AND R.PREV_STATUS = " + ddlCategory.SelectedValue + ((ddlStudent.SelectedIndex == 0) ? string.Empty : "S.IDNO = " + ddlStudent.SelectedValue), "R.ROLL_NO");
    ////        //    DataTableReader dtr = ds.Tables[0].CreateDataReader();

    ////        //    ddlStudent.Items.Clear();
    ////        //    ddlStudent.Items.Add(new ListItem("Please Select", "0"));
    ////        //    while (dtr.Read())
    ////        //    {
    ////        //        ddlStudent.Items.Add(new ListItem(dtr["ROLL_NO"].ToString() + " - " + dtr["NAME"].ToString() + "   [" + dtr["REGNO"].ToString() + "]", dtr["IDNO"].ToString()));
    ////        //    }
    ////        //    dtr.Close();

    ////        //    ddlStudent.Focus();
    ////        //}
    ////        //else
    ////        //{
    ////        //    ddlStudent.Items.Clear();
    ////        //    ddlCategory.Focus();
    ////        //}
    ////         */
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        if (Convert.ToBoolean(Session["error"]) == true)
    ////            objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
    ////        else
    ////            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    ////    }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            string cnt = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND PREV_STATUS = " + ddlCategory.SelectedValue);
            if (Convert.ToInt32(cnt) > 0)
            {

                //Start the Process
                lock (Session.SyncRoot)
                {
                    Session["complete"] = false;
                    Session["status"] = "";
                }

                Thread t = new Thread(new ParameterizedThreadStart(ThreadProcess));
                t.Start(Session);

                //Switch the View            
                timUpdate.Enabled = true;
            }
            else
                objCommon.DisplayMessage(this.updResultProcess, "No Students for Current Selection!!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.btnProcess_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ThreadProcess(object data)
    {
        try
        {
            //Disable all buttons
            btnProcess.Visible = false;
            btnLock.Visible = false;
            btnUnlock.Visible = false;
            btnCancel.Visible = false;

            DateTime resultdate = DateTime.Now;
            if (!string.IsNullOrEmpty(txtResultDate.Text.Trim()))
                resultdate = Convert.ToDateTime(txtResultDate.Text);

            System.Web.SessionState.HttpSessionState s = (System.Web.SessionState.HttpSessionState)data;

            string idnos = string.Empty;
            if (this.IsSelected() == true)
                idnos = this.GetStudentIDNOs(false);
            else
                idnos = this.GetStudentIDNOs(true);
            
            //string ret = objResult.ResultProcessAutonomous(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStudent.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), resultdate,int.Parse(ddlResultType.SelectedValue),Convert.ToInt32(Session["userno"]),ViewState["ipAddress"].ToString());
           // string ret = objResult.ResultProcessAutonomous(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), idnos, Convert.ToInt32(ddlCategory.SelectedValue), resultdate, int.Parse(ddlResultType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString());
            string ret = objResult.ResultProcessAutonomous(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), idnos,resultdate,ViewState["ipAddress"].ToString());
            if (ret == "1")
            {
                timUpdate.Enabled = false;
                objCommon.DisplayMessage(this.updResultProcess, "Result Processed Successfully", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updResultProcess, "Error...", this.Page);

            lock (s.SyncRoot)
            {
                s["complete"] = true;
                timUpdate.Enabled = false;
            }


            btnProcess.Visible = true;
            btnLock.Visible = true;
            btnUnlock.Visible = true;
            btnCancel.Visible = true;
        }
        catch (Exception ex)
        {
            timUpdate.Enabled = false;
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.ThreadProcess --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private bool IsSelected()
    {
        foreach (ListViewDataItem dataItem in lvStudent.Items)
        {
            if ((dataItem.FindControl("cbRow") as CheckBox).Checked)
                return true;
        }
        return false;
    }

    private string GetStudentIDNOs(bool all)
    {
        string idnos = "0";

        foreach (ListViewDataItem dataItem in lvStudent.Items)
        {
            if (all == false)
            {
                if ((dataItem.FindControl("cbRow") as CheckBox).Checked)
                {
                    if (idnos.Length == 0)
                        idnos = (dataItem.FindControl("hdfIDNO") as HiddenField).Value;
                    else
                        idnos = idnos + "," + (dataItem.FindControl("hdfIDNO") as HiddenField).Value;
                }
            }
            else
            {
                if (idnos.Length == 0)
                    idnos = (dataItem.FindControl("hdfIDNO") as HiddenField).Value;
                else
                    idnos = idnos + "," + (dataItem.FindControl("hdfIDNO") as HiddenField).Value;
            }
        }

        return idnos;
    }


    protected void timUpdate_Tick(object sender, EventArgs e)
    {
        if (Session["complete"] as bool? == true)
        {
            timUpdate.Enabled = false;
            objCommon.DisplayMessage(this.updResultProcess, "Result Processed Successfully!!", this.Page);
            btnProcess.Visible = true;
            btnLock.Visible = true;
            btnUnlock.Visible = true;
            btnCancel.Visible = true;
        }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            objResult.LockResultAutonomous(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), 1,Convert.ToInt32(ddlCategory.SelectedValue));
            this.CheckLockStatus();
            objCommon.DisplayMessage(this.updResultProcess, "Result Locked Successfully!!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.btnLock_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckLockStatus()
    {
        string cnt = objCommon.LookUp("ACD_TRRESULT", "DISTINCT LOCK", "LOCK = 1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue);
        if (string.IsNullOrEmpty(cnt))
        {
            btnProcess.Visible = true;
        }
        else
        {
            if (Convert.ToInt32(cnt) > 0)   //Locked        
            {
                btnProcess.Visible = false;
                objCommon.DisplayMessage(this.updResultProcess, "Result Locked for Current Selection. UnLock the Result.", this.Page);
            }
            else
                btnProcess.Visible = true;
        }
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            objResult.LockResultAutonomous(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), 0,Convert.ToInt32(ddlCategory.SelectedValue));
            objCommon.DisplayMessage(this.updResultProcess, "Result UnLocked Successfully!!", this.Page);

            this.CheckLockStatus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.btnUnlock_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    
    protected void ddlResultType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////try
        ////{
        ////    DataSet ds = null;
        ////    hdfTot.Value = "0";
        ////    txtTotStud.Text = "0";
        ////    lvStudent.DataSource = null;
        ////    lvStudent.DataBind();

        ////    if (ddlSession.SelectedIndex <= 0 || ddlDegree.SelectedIndex <= 0 || ddlBranch.SelectedIndex <= 0 ||
        ////    ddlScheme.SelectedIndex <= 0 || ddlSemester.SelectedIndex <= 0 || ddlResultType.SelectedIndex <= 0 || ddlCategory.SelectedIndex <= 0 || txtResultDate.Text == "")
        ////    {
        ////        objCommon.DisplayMessage(this.updResultProcess, "Please Select/Enter Session/Degree/Branch/Scheme/Semester/Category/Result Date for Result Processing!", this.Page);
        ////        return;
        ////    }

        ////    //this.CheckLockStatus();
        ////    btnProcess.Enabled = true;

        ////    if (ddlResultType.SelectedIndex > 0)
        ////    {
        ////        if (ddlResultType.SelectedValue == "1") //Regular
        ////        {
        ////            //objCommon.FillDropDownList (ddlStudent , "ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (S.IDNO = R.IDNO)", "DISTINCT S.IDNO", "(S.STUDNAME + ' ' + S.FATHERNAME + ' ' + S.LASTNAME) AS NAME,S.REGNO,R.ROLL_NO", "R.SESSIONNO = 51 AND R.SCHEMENO = 1 AND R.SEMESTERNO = 1 AND R.PREV_STATUS = 1" + ((ddlStudent.SelectedIndex == 0) ? string.Empty : "S.IDNO = " + ddlStudent.SelectedValue), "R.ROLL_NO");
        ////            ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (S.IDNO = R.IDNO)", "DISTINCT S.IDNO", "S.STUDNAME AS NAME,S.REGNO,R.ROLL_NO,0 AS TR_IDNO", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SEMESTERNO = " + ddlSemester.SelectedValue + "AND (R.CANCEL IS NULL OR R.CANCEL = 0) AND (R.DETAIND IS NULL OR R.DETAIND = 0) AND R.PREV_STATUS = " + ddlCategory.SelectedValue + ((ddlStudent.SelectedIndex == 0) ? string.Empty : "S.IDNO = " + ddlStudent.SelectedValue), "R.ROLL_NO");
        ////            lblmsg.Visible = false;
        ////        }
        ////        else
        ////            if (ddlResultType.SelectedValue == "4") //WithHeld
        ////            {
        ////                //ds = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO) INNER JOIN ACD_WITHHELD W ON (W.IDNO = R.IDNO AND W.SESSIONNO = R.SESSIONNO AND W.SEMESTERNO = R.SEMESTERNO)", "DISTINCT R.IDNO", "S.REGNO,(STUDNAME + ' ' + FATHERNAME + ' ' + LASTNAME) NAME,R.ROLL_NO", "S.REGNO IS NOT NULL AND R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SEMESTERNO = " + ddlSemester.SelectedValue + " AND R.PREV_STATUS = " + ddlCategory.SelectedValue, "R.ROLL_NO");
        ////                ds = objCommon.FillDropDown("(SELECT DISTINCT (SELECT result FROM ACD_TRRESULT WHERE SEMESTERNO = R.SEMESTERNO AND RESULT = 'P' AND IDNO = R.IDNO) result,(SELECT DISTINCT semesterno FROM ACD_TRRESULT WHERE SEMESTERNO = R.SEMESTERNO AND IDNO = R.IDNO) sem,R.IDNO,S.REGNO,(STUDNAME + ' ' + FATHERNAME + ' ' + LASTNAME) NAME,CAST(R.ROLL_NO AS NVARCHAR)+ ' - ' +CAST(DBO.FN_DESC('SECTION',R.SECTIONNO) AS NVARCHAR) AS ROLL_NO,(SELECT ISNULL(WITHHELD,0) FROM ACD_TRRESULT WHERE IDNO = R.IDNO AND SESSIONNO =R.SESSIONNO AND SCHEMENO = R.SCHEMENO AND SEMESTERNO = R.SEMESTERNO)TR_IDNO FROM ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)INNER JOIN ACD_WITHHELD W ON (W.IDNO = R.IDNO AND W.SESSIONNO = R.SESSIONNO AND W.SEMESTERNO = R.SEMESTERNO)WHERE S.REGNO IS NOT NULL AND R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.SCHEMENO = " + ddlScheme.SelectedValue + "AND (R.CANCEL IS NULL OR R.CANCEL = 0) AND (R.DETAIND IS NULL OR R.DETAIND = 0) AND R.SEMESTERNO = " + ddlSemester.SelectedValue + " AND R.PREV_STATUS = 0)a", "*", "idno", "IDNO NOT IN (SELECT IDNO FROM ACD_TRRESULT WHERE RESULT = 'P' AND SEMESTERNO = A.SEM AND SESSIONNO > " + ddlSession.SelectedValue + " AND IDNO = A.IDNO AND IDNO IN (SELECT DISTINCT IDNO FROM ACD_TRRESULT WHERE result = 'p' AND SEMESTERNO < A.SEM AND IDNO = A.IDNO))", "ROLL_NO");
        ////                lblmsg.Visible = true;
        ////            }
                         


        ////        //****CODE FOR FILLING THE DROP DOWN LIST PREVIOUSLY USED.
        ////        //DataTableReader dtr = ds.Tables[0].CreateDataReader();
        ////        //ddlStudent.Items.Clear();
        ////        //ddlStudent.Items.Add(new ListItem("Please Select", "0"));
        ////        //while (dtr.Read())
        ////        //{
        ////        //    ddlStudent.Items.Add(new ListItem(dtr["ROLL_NO"].ToString() + " - " + dtr["NAME"].ToString() + "   [" + dtr["REGNO"].ToString() + "]", dtr["IDNO"].ToString()));
        ////        //}
        ////        //dtr.Close();
        ////        //ddlStudent.Focus();
        ////         //hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
        ////         //txtTotStud.Text = ds.Tables[0].Rows.Count.ToString();
        ////         if (ds.Tables[0].Rows.Count > 0)
        ////         {
        ////             lvStudent.DataSource = ds;
        ////             lvStudent.DataBind();
        ////         }
        ////         else 
        ////         {
        ////             lvStudent.DataSource = null;
        ////             lvStudent.DataBind();
        ////         }
        ////    }
        ////    else
        ////    {
        ////        //ddlStudent.Items.Clear();
        ////        //ddlCategory.Focus();
        ////        lvStudent.DataSource = null;
        ////        lvStudent.DataBind();
        ////    }
        ////}
        ////catch (Exception ex)
        ////{
        ////    if (Convert.ToBoolean(Session["error"]) == true)
        ////        objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
        ////    else
        ////        objUaimsCommon.ShowError(Page, "Server Unavailable.");
        ////}
    }
    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            lvStudent.DataSource = null;
            lvStudent.DataBind();

            if (ddlSession.SelectedIndex <= 0 || ddlDegree.SelectedIndex <= 0 || ddlBranch.SelectedIndex <= 0 ||
            ddlScheme.SelectedIndex <= 0 || ddlSemester.SelectedIndex <= 0  || txtResultDate.Text == "")
            {
                objCommon.DisplayMessage(this.updResultProcess, "Please Select/Enter Session/Degree/Branch/Scheme/Semester/Result Date for Result Processing!", this.Page);
                return;
            }

            //this.CheckLockStatus();
            btnProcess.Enabled = true;

            if (ddlSession.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0 && ddlBranch.SelectedIndex > 0 && ddlScheme.SelectedIndex > 0 && ddlSemester.SelectedIndex >0)
            {
                ////if (ddlResultType.SelectedValue == "1") //Regular
                ////{
                    //objCommon.FillDropDownList (ddlStudent , "ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (S.IDNO = R.IDNO)", "DISTINCT S.IDNO", "(S.STUDNAME + ' ' + S.FATHERNAME + ' ' + S.LASTNAME) AS NAME,S.REGNO,R.ROLL_NO", "R.SESSIONNO = 51 AND R.SCHEMENO = 1 AND R.SEMESTERNO = 1 AND R.PREV_STATUS = 1" + ((ddlStudent.SelectedIndex == 0) ? string.Empty : "S.IDNO = " + ddlStudent.SelectedValue), "R.ROLL_NO");
                    //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (S.IDNO = R.IDNO)", "DISTINCT S.IDNO", "(S.STUDNAME + ' ' + S.FATHERNAME + ' ' + S.LASTNAME) AS NAME,S.REGNO,R.ROLL_NO,0 as TR_IDNO", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SEMESTERNO = " + ddlSemester.SelectedValue + "  AND (R.CANCEL IS NULL OR R.CANCEL = 0) AND (R.DETAIND IS NULL OR R.DETAIND = 0) AND R.PREV_STATUS = " + ddlCategory.SelectedValue + ((ddlStudent.SelectedIndex == 0) ? string.Empty : "S.IDNO = " + ddlStudent.SelectedValue), "R.ROLL_NO");
                    ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (S.IDNO = R.IDNO)", "DISTINCT S.IDNO", "S.STUDNAME AS NAME,S.REGNO,R.ROLL_NO,0 as TR_IDNO", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SEMESTERNO = " + ddlSemester.SelectedValue + "  AND (R.CANCEL IS NULL OR R.CANCEL = 0) AND (R.DETAIND IS NULL OR R.DETAIND = 0)", "R.ROLL_NO");
                    lblmsg.Visible = false;
                ////}
                ////else
                ////    if (ddlResultType.SelectedValue == "4") //WithHeld
                ////    {
                ////        //ds = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO) INNER JOIN ACD_WITHHELD W ON (W.IDNO = R.IDNO AND W.SESSIONNO = R.SESSIONNO AND W.SEMESTERNO = R.SEMESTERNO)", "DISTINCT R.IDNO", "S.REGNO,(STUDNAME + ' ' + FATHERNAME + ' ' + LASTNAME) NAME,CAST(R.ROLL_NO AS NVARCHAR)+ ' - ' +CAST(DBO.FN_DESC('SECTION',R.SECTIONNO) AS NVARCHAR) AS ROLL_NO,(SELECT ISNULL(WITHHELD,0) FROM ACD_TRRESULT WHERE IDNO = R.IDNO AND SESSIONNO =R.SESSIONNO AND SCHEMENO = R.SCHEMENO AND SEMESTERNO = R.SEMESTERNO)TR_IDNO", "S.REGNO IS NOT NULL AND R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SEMESTERNO = " + ddlSemester.SelectedValue + " AND R.IDNO IN (SELECT IDNO FROM ACD_TRRESULT WHERE SEMESTERNO < R.SEMESTERNO AND RESULT = 'P' AND IDNO = R.IDNO) AND R.PREV_STATUS = " + ddlCategory.SelectedValue, "ROLL_NO");
                ////        ds = objCommon.FillDropDown("(SELECT DISTINCT (SELECT result FROM ACD_TRRESULT WHERE SEMESTERNO = R.SEMESTERNO AND RESULT = 'P' AND IDNO = R.IDNO) result,(SELECT DISTINCT semesterno FROM ACD_TRRESULT WHERE SEMESTERNO = R.SEMESTERNO AND IDNO = R.IDNO) sem,R.IDNO,S.REGNO,(STUDNAME + ' ' + FATHERNAME + ' ' + LASTNAME) NAME,CAST(R.ROLL_NO AS NVARCHAR)+ ' - ' +CAST(DBO.FN_DESC('SECTION',R.SECTIONNO) AS NVARCHAR) AS ROLL_NO,(SELECT ISNULL(WITHHELD,0) FROM ACD_TRRESULT WHERE IDNO = R.IDNO AND SESSIONNO =R.SESSIONNO AND SCHEMENO = R.SCHEMENO AND SEMESTERNO = R.SEMESTERNO)TR_IDNO FROM ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)INNER JOIN ACD_WITHHELD W ON (W.IDNO = R.IDNO AND W.SESSIONNO = R.SESSIONNO AND W.SEMESTERNO = R.SEMESTERNO)WHERE S.REGNO IS NOT NULL AND R.SESSIONNO = " + ddlSession.SelectedValue + " AND (R.CANCEL IS NULL OR R.CANCEL = 0) AND (R.DETAIND IS NULL OR R.DETAIND = 0) AND R.SCHEMENO = " + ddlScheme.SelectedValue + " AND R.SEMESTERNO = " + ddlSemester.SelectedValue + " AND R.PREV_STATUS = 0)a", "*", "idno", "IDNO NOT IN (SELECT IDNO FROM ACD_TRRESULT WHERE RESULT = 'P' AND SEMESTERNO = A.SEM AND SESSIONNO > " + ddlSession.SelectedValue + " AND IDNO = A.IDNO AND IDNO IN (SELECT DISTINCT IDNO FROM ACD_TRRESULT WHERE result = 'p' AND SEMESTERNO < A.SEM AND IDNO = A.IDNO))", "ROLL_NO");
                ////        lblmsg.Visible = true;
                ////    }

                //********************QUERY FOR TRESULT AND PRESULT********************
                //SELECT DISTINCT R.IDNO,S.REGNO,(STUDNAME + ' ' + FATHERNAME + ' ' + LASTNAME) NAME,CAST(R.ROLL_NO AS NVARCHAR)+ ' - ' +CAST(DBO.FN_DESC('SECTION',R.SECTIONNO) AS NVARCHAR) AS ROLL_NO,
                //(SELECT ISNULL(WITHHELD,0) FROM ACD_TRRESULT 
                //WHERE IDNO = R.IDNO AND SESSIONNO =R.SESSIONNO AND SCHEMENO = R.SCHEMENO AND SEMESTERNO = R.SEMESTERNO)TR_IDNO
                //FROM ACD_STUDENT_RESULT R 
                //INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)
                //INNER JOIN ACD_WITHHELD W ON (W.IDNO = R.IDNO AND W.SESSIONNO = R.SESSIONNO AND W.SEMESTERNO = R.SEMESTERNO)

                //WHERE S.REGNO IS NOT NULL AND R.SESSIONNO = 54 AND R.SCHEMENO = 3 AND R.SEMESTERNO = 3 AND R.PREV_STATUS = 0
                //AND R.IDNO NOT IN (SELECT IDNO FROM ACD_TRRESULT WHERE SEMESTERNO = R.SEMESTERNO AND RESULT = 'P' AND IDNO = R.IDNO)
                //ORDER BY ROLL_NO
                
                //NEW QUERY NOW USED.
                //SELECT *,IDNO
                //FROM (SELECT DISTINCT (SELECT RESULT FROM ACD_TRRESULT WHERE SEMESTERNO = R.SEMESTERNO AND RESULT = 'P' AND IDNO = R.IDNO) RESULT,
                //(SELECT DISTINCT SEMESTERNO FROM ACD_TRRESULT WHERE SEMESTERNO = R.SEMESTERNO AND IDNO = R.IDNO) SEM,
                //R.IDNO,S.REGNO,(STUDNAME + ' ' + FATHERNAME + ' ' + LASTNAME) NAME,CAST(R.ROLL_NO AS NVARCHAR)+ ' - ' +CAST(DBO.FN_DESC('SECTION',R.SECTIONNO) AS NVARCHAR) AS ROLL_NO,
                //(SELECT ISNULL(WITHHELD,0) FROM ACD_TRRESULT WHERE IDNO = R.IDNO AND SESSIONNO =R.SESSIONNO AND SCHEMENO = R.SCHEMENO 
                //AND SEMESTERNO = R.SEMESTERNO)TR_IDNO FROM ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)
                //INNER JOIN ACD_WITHHELD W ON (W.IDNO = R.IDNO AND W.SESSIONNO = R.SESSIONNO AND W.SEMESTERNO = R.SEMESTERNO)
                //WHERE S.REGNO IS NOT NULL AND R.SESSIONNO = 54 AND R.SCHEMENO = 3 
                //AND R.SEMESTERNO = 3 AND R.PREV_STATUS = 0)A
                //WHERE --IDNO  IN (SELECT IDNO FROM ACD_TRRESULT WHERE RESULT = 'P' AND SEMESTERNO < A.SEM AND IDNO = A.IDNO) 
                //--AND
                //IDNO NOT IN (SELECT IDNO FROM ACD_TRRESULT WHERE RESULT = 'P' AND SEMESTERNO = A.SEM AND SESSIONNO > 54 AND IDNO = A.IDNO 
                //AND IDNO IN (SELECT IDNO FROM ACD_TRRESULT WHERE RESULT = 'P' AND SEMESTERNO<A.SEM AND IDNO = A.IDNO))--FORSUNIL KUMAR NOT TO COME
                //ORDER BY ROLL_NO


                //********************CODE FOR FILLING THE DROP DOWN LIST PREVIOUSLY USED.********************
                //DataTableReader dtr = ds.Tables[0].CreateDataReader();
                //ddlStudent.Items.Clear();
                //ddlStudent.Items.Add(new ListItem("Please Select", "0"));
                //while (dtr.Read())
                //{
                //    ddlStudent.Items.Add(new ListItem(dtr["ROLL_NO"].ToString() + " - " + dtr["NAME"].ToString() + "   [" + dtr["REGNO"].ToString() + "]", dtr["IDNO"].ToString()));
                //}
                //dtr.Close();
                //ddlStudent.Focus();
                //hdfTot.Value = ds.Tables[0].Rows.Count.ToString();
                //txtTotStud.Text = ds.Tables[0].Rows.Count.ToString();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = ds;
                    lvStudent.DataBind();
                }
                else
                {
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                }
            }
            else
            {
                //ddlStudent.Items.Clear();
                //ddlCategory.Focus();
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_ResultProcessingAuto.btnCancel_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
}
