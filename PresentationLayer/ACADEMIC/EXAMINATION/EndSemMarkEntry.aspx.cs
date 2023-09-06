//=================================================================================
// PROJECT NAME  : RF-CAMPUS [NITGOA]                                                         
// MODULE NAME   : ACADEMIC - END SEM MARK ENTRY                                           
// CREATION DATE : 14 -OCT-2009                                                     
// CREATED BY    :                                               
// MODIFIED BY   :PRASHANT AMLE                                                      
// MODIFIED DESC :MODIFY & APPLY RELATIVE & ABSOLUTE GRADING STSTEM 
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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


public partial class Academic_EndSemMarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    MarksEntryController objMarksEntry = new MarksEntryController();
    string th_pr = string.Empty;
     int subid;
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
        if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
                {

                    //lblText.Attributes.Add("style", "text-decoration:blink");
                    CheckActivity();
                    //Check for Panel
                    if (ViewState["action"] == null)
                    {
                        //selection panel
                        pnlSelection.Visible = true;
                        pnlMarkEntry.Visible = false;
                    }
                    else if (ViewState["action"].ToString().Equals("markentry"))
                    {
                        //mark entry panel
                        pnlMarkEntry.Visible = true;
                        pnlSelection.Visible = false;
                    }

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND FLOCK = 1", "SESSIONNO DESC");
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "TOP 2 SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                    ddlSession.SelectedIndex = 1;
                    if (Session["usertype"].ToString() == "3" )//&& Session["dec"].ToString() != "1")
                        this.ShowCourses();
                    else
                    {
                        trbranch.Visible = true;
                        trdegree.Visible = true;
                        trscheme.Visible = true;
                        trSemester.Visible = true;
                        trExam.Visible = true;
                        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                    }

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    objCommon.DisplayMessage("You are not authorized to view this page!!", this.Page);
                    ddlSession.Items.Clear();
                    pnlSelection.Visible = true;
                    pnlMarkEntry.Visible = false;
                }
                
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    private void CheckActivity()
    {
        string sessionno = string.Empty;
         sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1");
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG'");

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlSelection.Visible = true;
                pnlMarkEntry.Visible = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlSelection.Visible = true;
                pnlMarkEntry.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlSelection.Visible = true;
            pnlMarkEntry.Visible = false;
        }
        dtr.Close();
    }
    private void ShowCourses()
    {
        //DataSet ds = objMarksEntry.GetCourseForTeacher(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]));
        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT St ON (R.IDNO = St.IDNO) INNER JOIN ACD_SCHEME S ON (R.SCHEMENO = S.SCHEMENO)	INNER JOIN ACD_SEMESTER SM ON (R.SEMESTERNO = SM.SEMESTERNO)LEFT OUTER JOIN ACD_SECTION SN ON (SN.SECTIONNO = R.SECTIONNO)INNER JOIN ACD_COURSE C ON (R.COURSENO = C.COURSENO AND R.SCHEMENO = C.SCHEMENO)", "  DISTINCT R.COURSENO,	(CASE 	WHEN C.SUBID =7 THEN (REPLACE(R.CCODE,'-','') COLLATE DATABASE_DEFAULT + ' - '+ R.COURSENAME COLLATE DATABASE_DEFAULT + '  <SPAN STYLE= " + "\"" + " COLOR:GREEN;FONT-WEIGHT:BOLD" + "\"" + ">(SEM : ' + SM.SEMESTERNAME COLLATE DATABASE_DEFAULT + ' - '+ S.SCHEMENAME COLLATE DATABASE_DEFAULT + ' - <SPAN STYLE=" + "\"" + "COLOR:BLACK;" + "\"" + ">SECTION : '+ SN.SECTIONNAME COLLATE DATABASE_DEFAULT +'['+ DBO.FN_DESC('BATCH',R.BATCHNO) COLLATE DATABASE_DEFAULT +'] </SPAN> ) </SPAN>') 	ELSE (REPLACE(R.CCODE,'-','') COLLATE DATABASE_DEFAULT + ' - '+ R.COURSENAME COLLATE DATABASE_DEFAULT + '  <SPAN STYLE=" + "\"" + "COLOR:GREEN;FONT-WEIGHT:BOLD" + "\"" + ">(SEM : ' + SM.SEMESTERNAME COLLATE DATABASE_DEFAULT + ' - '+ S.SCHEMENAME COLLATE DATABASE_DEFAULT + ' - <SPAN STYLE=" + "\"" + "COLOR:BLACK;" + "\"" + ">SECTION : '+ SN.SECTIONNAME COLLATE DATABASE_DEFAULT +  ' </SPAN> ) </SPAN>') END) COURSENAME ", "R.SCHEMENO,R.SEMESTERNO,R.CCODE,ISNULL(R.SECTIONNO,0) AS SECTIONNO, (CASE WHEN C.SUBID = 7  THEN ISNULL(R.BATCHNO,0) ELSE  0 END)BATCHNO", "SESSIONNO =  " + ddlSession.SelectedValue + " AND ((UA_NO = " + Session["userno"] + " AND (VALUER_UA_NO IS NULL OR VALUER_UA_NO = 0)) OR (UA_NO_PRAC = " + Session["userno"] + " AND (VALUER_UA_NO_PRAC IS NULL OR VALUER_UA_NO_PRAC = 0)) or VALUER_UA_NO = " + Session["userno"] + " or  VALUER_UA_NO_PRAC = " + Session["userno"] + ") AND R.PREV_STATUS = 0 AND ((MAXMARKS_E >0 AND C.SUBID = 1 ) OR (S4MAX>0 AND C.SUBID <> 1 ))", "R.SEMESTERNO,R.CCODE,ISNULL(R.SECTIONNO,0)");
        DataSet ds = objMarksEntry.GetCourseForTeacherEndSem(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]));
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds.Tables[0];
                lvCourse.DataBind();
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
        }
    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
      {
        try
        {
            //Show the Student List with Exams that are ON
            //=============================================
            LinkButton lnk = sender as LinkButton;
            if (!lnk.ToolTip.Equals(string.Empty))
            {
                lblCourse.Text = lnk.Text;
                lblCourse.ToolTip = lnk.ToolTip;
                int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));
                int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                hdfSchemeNo.Value = SchemeNo.ToString();
                hdfExamType.Value = ExamType.ToString();
                string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                hdfSection.Value = sec_batch[0].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                hdfBatch.Value =  sec_batch.Length == 2 ? sec_batch[1].ToString() : "0" ;
                this.ShowStudents(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value));
                this.ShowGrades(Convert.ToInt16(lnk.ToolTip));
                int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                hdfSubid.Value = subId.ToString();
                if (subId == 1)
                {
                    int lockNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(LOCKS2)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                    int countIdno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                    if (lockNo < countIdno)
                    {
                        ShowMessage("Please First Lock Your Internal & Mid Sem marks !!!");
                    }

                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    private void BindJS()
    {
        try
        {
            foreach (GridViewRow gvRow in gvStudent.Rows)
            {
                if (th_pr == "1")
                {
                    TextBox txtESMarks = gvRow.FindControl("txtESMarks") as TextBox;
                    Label lblESMarks = gvRow.FindControl("lblESMarks") as Label;
                    Label lblESMinMarks = gvRow.FindControl("lblESMinMarks") as Label;

                    TextBox txtTotMarks = gvRow.FindControl("txtTotMarks") as TextBox;
                    TextBox txtTAMarks = gvRow.FindControl("txtTAMarks") as TextBox;
                    TextBox txtTotMarksAll = gvRow.FindControl("txtTotMarksAll") as TextBox;
                    TextBox txtTotPer = gvRow.FindControl("txtTotPer") as TextBox;
                    TextBox txtGrade = gvRow.FindControl("txtGrade") as TextBox;
                    TextBox txtGradeP = gvRow.FindControl("txtGradePoint") as TextBox;
                   
                    HiddenField hidTotMarks = gvRow.FindControl("hidTotMarksAll") as HiddenField;
                    HiddenField hidTotPer = gvRow.FindControl("hidTotPer") as HiddenField;
                    HiddenField hidGrade = gvRow.FindControl("hidGrade") as HiddenField;
                    HiddenField hidGradePoint = gvRow.FindControl("hidGradePoint") as HiddenField;

                    //int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));

                    //int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                     
                    int Scale = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCALEDN_MARK", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                    int totalmarks = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "(ISNULL(MAXMARKS_E,0)+ISNULL(S1MAX,0)+ISNULL(S2MAX,0)+ISNULL(S4MAX,0))TOTMARKS", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                    if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                    {
                        txtESMarks.ReadOnly = true;
                        
                    }
                    //if (txtESMarks.Text.Trim() == "") txtESMarks.Enabled = true;
                   
                    //if (txtESMarks.Text.Trim() == "401" || txtESMarks.Text.Trim() == "402" || txtESMarks.Text.Trim() == "403") txtESMarks.Enabled = false;
                    txtESMarks.Attributes.Add("onblur", " validateMarkTH(" + txtESMarks.ClientID + "," + lblESMarks.Text + "," + lblESMinMarks.Text + "," + txtTotMarks.Text + "," + txtTAMarks.Text + "," + txtTotMarksAll.ClientID + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + hidTotMarks.ClientID + "," + hidTotPer.ClientID + "," + hidGrade.ClientID + "," + hidGradePoint.ClientID + "," + Scale + "," + totalmarks + ")");
                    //txtESMarks.Attributes.Add("onblur", " validateMarkTH(" + txtESMarks.ClientID + "," + lblESMarks.Text + "," + lblESMinMarks.Text + "," + txtTotMarks.Text + "," + txtTAMarks.Text + "," + txtTotMarksAll.ClientID + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + lblTotMarks.Text + "," + lblTotPer.Text + "," + lblGrade.Text + "," + lblGradePoint.Text + ")");
                    
                    
                   
                   

                    
                }
                else
                    if (th_pr == "2")
                    {
                        TextBox txtESPRMarks = gvRow.FindControl("txtESPRMarks") as TextBox;
                        Label lblESPRMarks = gvRow.FindControl("lblESPRMarks") as Label;
                        Label lblESPRMinMarks = gvRow.FindControl("lblESPRMinMarks") as Label;

                        TextBox txtTotPer = gvRow.FindControl("txtTotPer") as TextBox;
                        TextBox txtGrade = gvRow.FindControl("txtGrade") as TextBox;
                        TextBox txtGradeP = gvRow.FindControl("txtGradePoint") as TextBox;

                        HiddenField hidTotPer = gvRow.FindControl("hidTotPer") as HiddenField;
                        HiddenField hidGrade = gvRow.FindControl("hidGrade") as HiddenField;
                        HiddenField hidGradePoint = gvRow.FindControl("hidGradePoint") as HiddenField;

                        //TextBox txtMax = lvGrades.FindControl("txtMax") as TextBox;
                        //TextBox txtMin = lvGrades.FindControl("txtMin") as TextBox;
                        //int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));

                        //int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                        int Scale1 = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCALEDN_MARK", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                        int totalmarks1 = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "(ISNULL(MAXMARKS_E,0)+ISNULL(S1MAX,0)+ISNULL(S2MAX,0)+ISNULL(S4MAX,0))TOTMARKS", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                        if (lblESPRMarks.ToolTip.ToUpper().Equals("TRUE"))
                        {
                            txtESPRMarks.Enabled = false;
                            
                        }
                        //if (txtESPRMarks.Text.Trim() == "") txtESPRMarks.Enabled = true;
                        //if (txtESPRMarks.Text.Trim() == "401" || txtESPRMarks.Text.Trim() == "402" || txtESPRMarks.Text.Trim() == "403") txtESPRMarks.Enabled = false;
                        txtESPRMarks.Attributes.Add("onblur", "validateMark(" + txtESPRMarks.ClientID + "," + lblESPRMarks.Text + "," + lblESPRMinMarks.Text + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + hidTotPer.ClientID + "," + hidGrade.ClientID + "," + hidGradePoint.ClientID + "," + Scale1 + "," + totalmarks1 + ")");
                    }
               
                
            }

            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.BindJS --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //0 - means - unlock
        SaveAndLock(0);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        
        ViewState["action"] = null;
        //selection panel
        pnlSelection.Visible = true;
        pnlMarkEntry.Visible = false;
        txtTitle.Text = "";
    }

    #region Private/Public Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void SaveAndLock(int lock_status)
    {
        
        try
        {
            
            string exam = string.Empty;
            CustomStatus cs = CustomStatus.Error;
            int courseno = Convert.ToInt32(lblCourse.ToolTip);
            string[] course = lblCourse.Text.Split('-');
            string ccode = course[0].Trim();

            //Check for lock and null marks
            if (lock_status == 1)
            {
                if (CheckMarks(lock_status) == false)
                {
                    return;
                }
            }

            for (int j = 2; j < gvStudent.Columns.Count; j++)
            {

                if (gvStudent.Columns[j].Visible == true)
                {
                    string studids = string.Empty;
                    string marks = string.Empty;
                    string totmarks = string.Empty;
                    string grade = string.Empty;
                    string Gpoint = string.Empty;
                    string totPer = string.Empty;

                    //MarksEntryController objMarksEntry = new MarksEntryController();
                    Label lbl;
                    TextBox txtMarks;
                    TextBox txtTotMarks;
                    TextBox txtTotPer;
                    TextBox txtGrade;
                    TextBox txtGradePoint;
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    //Note : -100 for Marks will be converted as NULL           
                    //NULL means mark entry not done.                           
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    for (int i = 0; i < gvStudent.Rows.Count; i++)
                    {
                        //Gather Student IDs 
                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;

                        if (j == 2) //TH MARKS
                        {
                            
                            //Gather Exam Marks       
                            txtMarks = gvStudent.Rows[i].FindControl("txtESMarks") as TextBox;
                            Label lblESMinMarks = gvStudent.Rows[i].FindControl("lblESMinMarks") as Label;
                            
                            txtTotMarks = gvStudent.Rows[i].FindControl("txtTotMarksAll") as TextBox;
                            HiddenField hidTotMarks = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;

                            HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
                            HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
                            HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
                            txtTotPer = gvStudent.Rows[i].FindControl("txtTotPer") as TextBox;
                            txtGrade = gvStudent.Rows[i].FindControl("txtGrade") as TextBox;
                            txtGradePoint = gvStudent.Rows[i].FindControl("txtGradePoint") as TextBox;
                           
                            //logic for round up TA mark entry
                            if (txtMarks.Text != string.Empty)
                            {
                                string r =string.Empty;
                                if (txtMarks.Text.Contains("."))
                                {
                                    r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
                                    if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
                                    {
                                        int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
                                        txtMarks.Text = Convert.ToString(val + 1);
                                    }
                                    else
                                        txtMarks.Text = txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')); 

                                }
                                else
                                    r = txtMarks.Text;
                            }
                            exam = "EXTERMARK";

                            //Check if marks exists, then add the id
                            //if (!string.IsNullOrEmpty(txtMarks.Text.Trim()))
                            //{
                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                                studids += lbl.ToolTip + ",";
                                totmarks += Convert.ToString(hidTotMarks.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarks.Value).Trim() + ",";
                                grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
                                Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                                totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
                            //}
                        }
                        else if (j == 5) //PR MARKS
                        {
                            //Gather Exam Marks 
                            txtMarks = gvStudent.Rows[i].FindControl("txtESPRMarks") as TextBox;

                            HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
                            HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
                            HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;

                            //logic for round up TA mark entry
                            if (txtMarks.Text != string.Empty)
                            {
                                string r = string.Empty;
                                if (txtMarks.Text.Contains("."))
                                {
                                    r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
                                    if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
                                    {
                                        int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
                                        txtMarks.Text = Convert.ToString(val + 1);
                                    }
                                    else
                                        txtMarks.Text = txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')); 
                                }
                                else
                                    r = txtMarks.Text;
                            }
                            exam = "S4";

                            //Check if marks exists, then add the id
                            //if (!string.IsNullOrEmpty(txtMarks.Text.Trim()))
                            //{
                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                                studids += lbl.ToolTip + ",";
                                totmarks += "-100,";
                                grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
                                Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                                totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
                            //}
                        }
                       
                    }

                    string lgrade = string.Empty;
                    string max = string.Empty;
                    string min = string.Empty;
                    string point = string.Empty;
                    string totStud = string.Empty;
                    //THIS IS FOR ADDING LV GRADES VALUES 
                    foreach (ListViewDataItem dataRow in lvGrades.Items)
                    {
                        
                        TextBox txtgra = dataRow.FindControl("txtGrades") as TextBox;
                        
                        TextBox txtmax = dataRow.FindControl("txtMax") as TextBox;
                        
                        TextBox txtmin= dataRow.FindControl("txtMin") as TextBox;
                        
                        TextBox txtpoint = dataRow.FindControl("txtGradePoints") as TextBox;

                        HiddenField hidTotStud = dataRow.FindControl("hidTotalStudent") as HiddenField;

                        
                        point += txtpoint.Text.Trim() == string.Empty ? "-100," : txtpoint.Text + ",";
                        lgrade += txtgra.Text.Trim() == string.Empty ? "-100," : txtgra.Text + ",";
                        max += txtmax.Text.Trim() == string.Empty ? "-100," : txtmax.Text + ",";
                        min += txtmin.Text.Trim() == string.Empty ? "-100," : txtmin.Text + ",";
                        totStud += Convert.ToString(hidTotStud.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotStud.Value).Trim() + ",";
                    }
                    

                    //below code commented for gettinf argument error from BLL method..it will get solve whenever this page will use[07-09-2016]

                   // if (!string.IsNullOrEmpty(studids))
                     //   cs = (CustomStatus)objMarksEntry.UpdateMarkEntry(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()));
                }
            }


            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                    if (subId == 1)
                    {
                        int lockNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(LOCKS2)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                        int countIdno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                        if (lockNo < countIdno)
                        {

                            objMarksEntry.UpdateMinnorMidLockMarkEntry(Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(hdfSchemeNo.Value), Convert.ToInt16(Session["userno"]), Convert.ToInt16(hdfSection.Value), exam, ViewState["ipAddress"].ToString());
                            //ShowMessage("Please First Lock Your Internal & Mid Sem marks !!!");
                        }

                    }
                    ShowMessage("Marks Locked Successfully!!!");
                }
                else
                {
                    ShowMessage("Marks Saved Successfully!!!");
                }
            }
            else
                ShowMessage("Error in Saving Marks!");

            this.ShowStudents(courseno, Convert.ToInt16(hdfSection.Value));
           
            this.ShowGrades(Convert.ToInt32(lblCourse.ToolTip));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckExamON()
    {
        bool flag = true;
        if (gvStudent.Columns[3].Visible == true) return flag;
        //if (gvStudent.Columns[4].Visible == true) return flag;
        return false;
    }

    private bool CheckMarks(int lock_status)
    {
        bool flag = true;
        try
        {
            Label lblmax, lblmin;
            TextBox txt;
            string marks = string.Empty;
            string maxMarks = string.Empty;
            string minMarks = string.Empty;

            for (int j = 2; j < gvStudent.Columns.Count; j++)    //columns
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                {
                    if (gvStudent.Columns[4].Visible == true)
                    {
                        if (j == 2) //ESEM-TH MARKS
                        {
                            lblmax = gvStudent.Rows[i].Cells[j].FindControl("lblESMarks") as Label;      //Max Marks 
                            lblmin = gvStudent.Rows[i].Cells[j].FindControl("lblESMinMarks") as Label;      //Max Marks 
                            txt = gvStudent.Rows[i].Cells[j].FindControl("txtESMarks") as TextBox;    //Marks Entered 
                            maxMarks = lblmax.Text.Trim();
                            minMarks = lblmin.Text.Trim();
                            marks = txt.Text.Trim();

                            if (!txt.Text.Trim().Equals(string.Empty) && !lblmax.Text.Trim().Equals(string.Empty) && !lblmin.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                            {
                                if (txt.Text == "")
                                {
                                    ShowMessage("Marks Entry Not Completed!!!");
                                    txt.Focus();
                                    flag = false;
                                    break;
                                }
                                else
                                {
                                    if (txt.Text.Contains("."))
                                    {
                                        string r = txt.Text.Substring(txt.Text.IndexOf('.') + 1, 1);

                                        if (Convert.ToInt32(r) >= 5)
                                        {
                                            int val = Convert.ToInt32(txt.Text.Substring(0, txt.Text.IndexOf('.')));
                                            txt.Text = Convert.ToString(val + 1);
                                        }
                                    }
                                    //Check for Marks entered greater than Max Marks
                                    if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lblmax.Text))
                                    {
                                        //Note : 401 for Absent
                                        if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402  || Convert.ToInt16(txt.Text) == 403)
                                        {
                                        }
                                        else
                                        {
                                            ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lblmax.Text + "]");
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                    }
                                    
                                }
                            }
                            else
                            {
                                if (txt.Enabled == true)
                                {
                                    if (lock_status == 1)
                                    {
                                        ShowMessage("Marks Entry Not Completed!!!");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                        if (gvStudent.Columns[5].Visible == true)
                        {
                            if (j == 3) //ESEM-PR MARKS
                            {
                                lblmax = gvStudent.Rows[i].Cells[j].FindControl("lblESPRMarks") as Label;      //Max Marks 
                                lblmin = gvStudent.Rows[i].Cells[j].FindControl("lblESPRMinMarks") as Label;      //Max Marks 
                                txt = gvStudent.Rows[i].Cells[j].FindControl("txtESPRMarks") as TextBox;    //Marks Entered 
                                maxMarks = lblmax.Text.Trim();
                                minMarks = lblmin.Text.Trim();
                                marks = txt.Text.Trim();

                                if (!txt.Text.Trim().Equals(string.Empty) && !lblmax.Text.Trim().Equals(string.Empty) && !lblmin.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                                {
                                    if (txt.Text == "")
                                    {
                                        ShowMessage("Marks Entry Not Completed!!!");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                    else
                                    {
                                        if (txt.Text.Contains("."))
                                        {
                                            string r = txt.Text.IndexOf('.') > 0 ? "0" : txt.Text.Substring(txt.Text.IndexOf('.') + 1, 1);

                                            if (Convert.ToInt32(r) >= 5)
                                            {
                                                int val = Convert.ToInt32(txt.Text.Substring(0, txt.Text.IndexOf('.')));
                                                txt.Text = Convert.ToString(val + 1);
                                            }
                                            else
                                                txt.Text = txt.Text.Substring(0, txt.Text.IndexOf('.'));
                                        }
                                        //Check for Marks entered greater than Max Marks
                                        if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lblmax.Text))
                                        {
                                            //Note : 401 for Absent
                                            if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402  || Convert.ToInt16(txt.Text) == 403)
                                            {
                                            }
                                            else
                                            {
                                                ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lblmax.Text + "]");
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                        //else
                                        //    if (Convert.ToInt16(txt.Text) < Convert.ToInt16(lblmin.Text))
                                        //    {

                                        //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Lesser than Min Marks[" + lblmin.Text + "]");
                                        //        txt.Focus();
                                        //        flag = false;
                                        //        break;
                                        //    }
                                    }
                                }
                                else
                                {
                                    if (txt.Enabled == true)
                                    {
                                        if (lock_status == 1)
                                        {
                                            ShowMessage("Marks Entry Not Completed!!!");
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                        
                                    }
                                }
                            }
                        }

                    if (flag == false)
                    {
                        break;
                    }
                   
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private void ShowGrades(int Courseno)
    {
        int subid = Convert.ToInt16( objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO="+Courseno+""));
        int degreeNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "DEGREENO", "C.COURSENO=" + Courseno + ""));

        DataSet dsGrades = objMarksEntry.GetAllGrades(Convert.ToInt16(Session["currentsession"].ToString()), subid, Courseno, Convert.ToInt16(Session["userno"].ToString()),degreeNo);
        if (dsGrades != null && dsGrades.Tables.Count > 0)
        {
            lvGrades.DataSource = dsGrades;
            lvGrades.DataBind();
           
            int i = 1;
            int j = 0;
            int sum = 0;
           foreach (ListViewDataItem dataRow in lvGrades.Items)
            {
                if (i < lvGrades.Items.Count-2)
               {
                TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;
                int MaxValue = Convert.ToInt32(txtmin.Text)-1;
                ListViewDataItem item = lvGrades.Items[i];
                TextBox a = (TextBox)item.FindControl("txtMax");
                a.Text = MaxValue.ToString();
            
               }
               i++;
               TextBox txtTotalStud = dataRow.FindControl("txtTotalStudent") as TextBox;
               sum = sum + Convert.ToInt32(txtTotalStud.Text);
               txtTotalAllStudent.Text = sum.ToString();
            }
            int k =0;
           foreach (GridViewRow gvRow in gvStudent.Rows)
           {
               string subid1 = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
               if (subid1 == "1")
               {

                   TextBox txtESMarks = gvRow.FindControl("txtESMarks") as TextBox;
                   Label lblESMarks = gvRow.FindControl("lblESMarks") as Label;
                   Label lblESMinMarks = gvRow.FindControl("lblESMinMarks") as Label;

                   if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                   {
                       
                       k=k+1;
                       int count= gvStudent.Rows.Count;
                       if (count == k)
                       {
                           foreach (ListViewDataItem dataRow in lvGrades.Items)
                           {
                               if (j < lvGrades.Items.Count)
                               {
                                   TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;
                                   TextBox txtmax = dataRow.FindControl("txtMax") as TextBox;
                                   txtmax.Enabled = false;
                                   txtmin.Enabled = false;
                               }
                               j++;
                           }
                       }
                   }
               }
               if (subid1 == "2")
               {

                   TextBox txtESPRMarks = gvRow.FindControl("txtESPRMarks") as TextBox;
                   Label lblESPRMarks = gvRow.FindControl("lblESPRMarks") as Label;
                   Label lblESPRMinMarks = gvRow.FindControl("lblESPRMinMarks") as Label;

                   if (lblESPRMarks.ToolTip.ToUpper().Equals("TRUE"))
                   {
                       k=k+1;
                       int count= gvStudent.Rows.Count;
                       if (count == k)
                       {
                           foreach (ListViewDataItem dataRow in lvGrades.Items)
                           {
                               if (j < lvGrades.Items.Count)
                               {
                                   TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;
                                   TextBox txtmax = dataRow.FindControl("txtMax") as TextBox;
                                   txtmax.Enabled = false;
                                   txtmin.Enabled = false;

                               }
                               j++;
                           }
                       }
                   }
               }
           }

           string schemetype = objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "SCHEMETYPE", "COURSENO=" + lblCourse.ToolTip);
           
           ListViewDataItem item1 = lvGrades.Items[0];
           TextBox a1 = (TextBox)item1.FindControl("txtMax");
           a1.Enabled = false;
           if (schemetype == "1")
           {
               ListViewDataItem item2 = lvGrades.Items[7];
               TextBox a2 = (TextBox)item2.FindControl("txtMin");
               a2.Enabled = false;
               ListViewDataItem item3 = lvGrades.Items[8];
               TextBox a3 = (TextBox)item3.FindControl("txtMax");
               a3.Enabled = false;
               ListViewDataItem item4 = lvGrades.Items[8];
               TextBox a4 = (TextBox)item4.FindControl("txtMin");
               a4.Enabled = false;
               ListViewDataItem item5 = lvGrades.Items[9];
               TextBox a5 = (TextBox)item5.FindControl("txtMax");
               a5.Enabled = false;
               ListViewDataItem item6 = lvGrades.Items[9];
               TextBox a6 = (TextBox)item6.FindControl("txtMin");
               a6.Enabled = false;
           }
           else
           {
               ListViewDataItem item2 = lvGrades.Items[6];
               TextBox a2 = (TextBox)item2.FindControl("txtMin");
               a2.Enabled = false;
               ListViewDataItem item3 = lvGrades.Items[7];
               TextBox a3 = (TextBox)item3.FindControl("txtMax");
               a3.Enabled = false;
               ListViewDataItem item4 = lvGrades.Items[7];
               TextBox a4 = (TextBox)item4.FindControl("txtMin");
               a4.Enabled = false;
               ListViewDataItem item5 = lvGrades.Items[8];
               TextBox a5 = (TextBox)item5.FindControl("txtMax");
               a5.Enabled = false;
               ListViewDataItem item6 = lvGrades.Items[8];
               TextBox a6 = (TextBox)item6.FindControl("txtMin");
               a6.Enabled = false;
           }

        }
        else
        {
            lvGrades.DataSource = null;
            lvGrades.DataBind();
        }

     

    }
    private void ShowStudents(int courseNo, int sectionNo)
    {
        try
        {
            //Check Exam Activity is ON
            //==========================
            DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

            if (dsExams != null && dsExams.Tables.Count > 0)
            {
                if (dsExams.Tables[0].Rows.Count <= 0)
                {
                    objCommon.DisplayMessage("Marks Entry Activity is OFF. Contact MIS!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage("Marks Entry Activity is OFF. Contact MIS!", this.Page);
                return;
            }



            subid = Convert.ToInt16(objCommon.LookUp("ACD_SUBJECTTYPE", "ISNULL(SUBID,0)", " SUBID IN (SELECT SUBID FROM ACD_COURSE WHERE COURSENO =" + lblCourse.ToolTip + " )"));
            //int ua_no = 0;
            int count = 0;

            // CHECK IF VALUER IS ASSIGNED
            //if (subid == 1 )
            //    ua_no = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0)UA_NUM", " SESSIONNO = " + ddlSession.SelectedValue + "  AND COURSENO =  " + lblCourse.ToolTip + " ORDER BY UA_NUM DESC"));
            //else
            //    ua_no = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0)UA_NUM", " SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO =  " + lblCourse.ToolTip + " ORDER BY UA_NUM DESC"));
            if (subid == 7 || subid == 8 || subid == 9 )
                //count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));
                count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));
            else
                if (subid == 1)
                    count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));
                else
                    count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));

            if (count == 0)
            {
                gvStudent.Columns[2].Visible = false;
                gvStudent.Columns[3].Visible = false;

                btnSave.Visible = false;
                btnLock.Visible = false;
                gvStudent.DataSource = null;
                gvStudent.DataBind();
                pnlMarkEntry.Visible = false;
                pnlStudGrid.Visible = false;
                objCommon.DisplayMessage("You are not allowed to do mark entry! ", this.Page);
                return;
            }
            else
            {
                DataSet dsStudent = null;
                //FILL STUDENTS WITH EXAMS THAT ARE ON
                //=====================================

                //if (subid == 7) //|| subid == 7 || subid == 9)
                //{
                //    dsStudent = objCommon.FillDropDown(" ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)	INNER JOIN ACD_COURSE C ON (R.COURSENO = C.COURSENO)	LEFT OUTER JOIN ACD_STUDENT_TEST_MARK T ON (R.SESSIONNO = T.SESSIONNO AND R.COURSENO = T.COURSENO AND R.IDNO = T.IDNO)", "DISTINCT R.IDNO,R.REGNO,R.ROLL_NO ,S.STUDNAME+(CASE PREV_STATUS WHEN 0 THEN '' ELSE ' (Ex)' END) AS STUDNAME,CAST(R.S4MARK AS INT) AS S4MARK,ISNULL(C.S4MAX,0) AS S4MAX,ISNULL(C.S4MIN,0) AS S4MIN,R.LOCKS4,CAST(R.EXTERMARK AS INT) AS EXTERMARK,ISNULL(C.MAXMARKS_E,0) AS MAXMARKS_E,ISNULL(C.MINMARKS,0) AS MINMARKS,R.LOCKE,CAST(R.S1MARK AS INT) AS S1MARK,ISNULL(C.S1MAX,0) AS S1MAX,ISNULL(C.S1MIN,0) AS S1MIN,R.LOCKS1,	CAST(R.S2MARK AS INT) AS S2MARK,ISNULL(C.S2MAX,0) AS S2MAX,ISNULL(C.S2MIN,0) AS S2MIN,R.LOCKS2", "	CAST(R.S3MARK AS INT) AS S3MARK,ISNULL(C.S3MAX,0) AS S3MAX,ISNULL(C.S3MIN,0) AS S3MIN,R.LOCKS3,	T.T1MARK,(C.S2MAX/2) T1MAX, 0 AS T1MIN,T.LOCKT1,T.T2MARK,(C.S2MAX/2) T2MAX, 0 AS T2MIN,T.LOCKT2,R.PREV_STATUS", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND R.BATCHNO = " + hdfBatch.Value.ToString() + "AND ((R.UA_NO = " + Session["userno"].ToString() + " OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR (UA_NO_PRAC = " + Session["userno"].ToString() + " OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL) AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND ACCEPTED = 1 AND EXAM_REGISTERED = 1  AND PREV_STATUS=0", "R.PREV_STATUS,R.REGNO");
                //    // trTitle.Visible = true;
                //}
                //else
                //{
                  
                    dsStudent = objCommon.FillDropDown(" ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)	INNER JOIN ACD_COURSE C ON (R.COURSENO = C.COURSENO)	LEFT OUTER JOIN ACD_STUDENT_TEST_MARK T ON (R.SESSIONNO = T.SESSIONNO AND R.COURSENO = T.COURSENO AND R.IDNO = T.IDNO)", "DISTINCT R.IDNO,R.REGNO,R.ROLL_NO ,S.DEGREENO,S.STUDNAME+(CASE PREV_STATUS WHEN 0 THEN '' ELSE ' (Ex)' END) AS STUDNAME,CAST(R.S4MARK AS INT) AS S4MARK,ISNULL(C.S4MAX,0) AS S4MAX,ISNULL(C.S4MIN,0) AS S4MIN,R.LOCKS4,CAST(R.EXTERMARK AS INT) AS EXTERMARK,CAST(R.MARKTOT AS INT) AS MARKTOT,R.GRADE AS GRADE,CAST(R.GDPOINT AS INT) AS GDPOINT,R.SCALEDN_PERCENT,ISNULL(C.MAXMARKS_E,0) AS MAXMARKS_E,ISNULL(C.MINMARKS,0) AS MINMARKS,R.LOCKE,CAST(ISNULL(R.S1MARK,0) AS INT) AS S1MARK,ISNULL(C.S1MAX,0) AS S1MAX,ISNULL(C.S1MIN,0) AS S1MIN,R.LOCKS1,	CAST(ISNULL(R.S2MARK,0) AS INT) AS S2MARK,ISNULL(C.S2MAX,0) AS S2MAX,ISNULL(C.S2MIN,0) AS S2MIN,R.LOCKS2", "	CAST(R.S3MARK AS INT) AS S3MARK,ISNULL(C.S3MAX,0) AS S3MAX,ISNULL(C.S3MIN,0) AS S3MIN,R.LOCKS3,	T.T1MARK,(C.S2MAX/2) T1MAX, 0 AS T1MIN,T.LOCKT1,T.T2MARK,(C.S2MAX/2) T2MAX, 0 AS T2MIN,T.LOCKT2,R.PREV_STATUS", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) 	AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1  ", "R.PREV_STATUS,R.REGNO");
                 
                //}
                // dsStudent = objCommon.FillDropDown(" ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)	INNER JOIN ACD_COURSE C ON (R.COURSENO = C.COURSENO)	LEFT OUTER JOIN ACD_STUDENT_TEST_MARK T ON (R.SESSIONNO = T.SESSIONNO AND R.COURSENO = T.COURSENO AND R.IDNO = T.IDNO)", "DISTINCT R.IDNO,R.REGNO,R.ROLL_NO ,S.STUDNAME+(CASE PREV_STATUS WHEN 0 THEN '' ELSE ' (Ex)' END) AS STUDNAME,CAST(R.S4MARK AS INT) AS S4MARK,ISNULL(C.S4MAX,0) AS S4MAX,ISNULL(C.S4MIN,0) AS S4MIN,R.LOCKS4,CAST(R.EXTERMARK AS INT) AS EXTERMARK,ISNULL(C.MAXMARKS_E,0) AS MAXMARKS_E,ISNULL(C.MINMARKS,0) AS MINMARKS,R.LOCKE,CAST(R.S1MARK AS INT) AS S1MARK,ISNULL(C.S1MAX,0) AS S1MAX,ISNULL(C.S1MIN,0) AS S1MIN,R.LOCKS1,	CAST(R.S2MARK AS INT) AS S2MARK,ISNULL(C.S2MAX,0) AS S2MAX,ISNULL(C.S2MIN,0) AS S2MIN,R.LOCKS2", "	CAST(R.S3MARK AS INT) AS S3MARK,ISNULL(C.S3MAX,0) AS S3MAX,ISNULL(C.S3MIN,0) AS S3MIN,R.LOCKS3,	T.T1MARK,(C.S2MAX/2) T1MAX, 0 AS T1MIN,T.LOCKT1,T.T2MARK,(C.S2MAX/2) T2MAX, 0 AS T2MIN,T.LOCKT2,R.PREV_STATUS", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + " and (VALUER_UA_NO = 0 or VALUER_UA_NO is null )) OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + " AND  (VALUER_UA_NO_PRAC IS NULL OR VALUER_UA_NO_PRAC =0 )) OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) 	AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1 AND ACCEPTED =1 AND PREV_STATUS= 0 ", "R.PREV_STATUS,R.REGNO");
               
                //   trTitle.Visible = true;
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    int DEGREENO = Convert.ToInt16( dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());
                    Session["DEGREENO"] = DEGREENO;
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        //HIDE THE MARKS ENTRY COLUMNS  

                        gvStudent.Columns[4].Visible = false;
                        gvStudent.Columns[5].Visible = false;
                        gvStudent.Columns[2].Visible = false;
                        gvStudent.Columns[3].Visible = false;
                        DataTableReader dtrExams = dsExams.Tables[0].CreateDataReader();
                        if(subid==1)
                        {
                        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["S1MARK"]) >= 0)
                        {
                            gvStudent.Columns[2].HeaderText = "MINOR <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["S1MAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["S1MIN"].ToString() + "]";
                            gvStudent.Columns[2].Visible = true;
                        }
                        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["S2MARK"]) >= 0)
                        {
                            gvStudent.Columns[3].HeaderText = "MID-SEM <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["S2MAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["S2MIN"].ToString() + "]";
                            gvStudent.Columns[3].Visible = true;
                        }
                        }
                        while (dtrExams.Read())
                        {

                            if ((dtrExams["FLDNAME"].ToString() == "EXTERMARK") && (ddlExam.SelectedValue == "EXTERMARK" || ddlExam.SelectedValue == "0"))
                            {
                                if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["MAXMARKS_E"]) > 0)
                                {
                                    gvStudent.Columns[4].HeaderText = "ESEM-TH <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["MAXMARKS_E"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["MINMARKS"].ToString() + "]";
                                    gvStudent.Columns[4].Visible = true;
                                    gvStudent.Columns[6].Visible = true;
                                    th_pr = "1";
                                }

                            }
                            if ((dtrExams["FLDNAME"].ToString() == "S4") && (ddlExam.SelectedValue == "S4" || ddlExam.SelectedValue == "0"))
                            {
                                if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["S4MAX"]) > 0)
                                {
                                    gvStudent.Columns[5].HeaderText = "ESEM-PR <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["S4MAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["S4MIN"].ToString() + "]";
                                    gvStudent.Columns[5].Visible = true;
                                    gvStudent.Columns[6].Visible = false;
                                    th_pr = "2";
                                }
                            }
                        }

                        dtrExams.Close();
                        dtrExams.Dispose();

                        lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                        //Bind the Student List
                        gvStudent.DataSource = dsStudent;
                        gvStudent.DataBind();
                        gvStudent.DataSource = dsStudent;
                        gvStudent.DataBind();

                        this.BindJS();

                        pnlMarkEntry.Visible = true;
                        pnlStudGrid.Visible = true;
                        pnlSelection.Visible = false;
                        btnSave.Enabled = true;
                        btnLock.Enabled = true;
                        btnSave.Visible = true;
                        btnLock.Visible = true;
                    }
                }

                if (gvStudent.Columns[2].Visible == false &&
                    gvStudent.Columns[4].Visible == false &&
                    gvStudent.Columns[5].Visible == false)
                {
                    btnSave.Visible = false;
                    btnLock.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void btnLock_Click(object sender, EventArgs e)
    {
        //1 - means lock marks
        SaveAndLock(1);
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
        btnSave.Enabled = false;
        btnLock.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
       // this.ShowReport("MarksListReport", "rptMarksListForEndSem.rpt");
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
        
        if (Convert.ToInt32(subid) == 1)
        {
            this.ShowReport("MarksListReport", "rptMarksListForEndSem.rpt");
        }
        else
        {
            this.ShowReport("MarksListReportPrac", "rptMarksListForEndSemPrac.rpt");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=0";//,@P_SUBID=" + Convert.ToInt32(subid.ToString()) + "

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }
    private void ShowGraphReport(string reportTitle, string rptFileName)
    {
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
        int degreeNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "DEGREENO", "C.COURSENO=" + lblCourse.ToolTip + ""));
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_SESSIONO=" + ddlSession2.SelectedValue + ",@P_SUBID=" + Convert.ToInt32(subid.ToString()) + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + degreeNo + "";

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }
    #region Selected Index Change

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0 && ddlSession.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")      //admin
                objCommon.FillDropDownList(ddlDept, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            else if (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1")      //HOD
            {
                string branchno = string.Empty;

                objCommon.FillDropDownList(ddlDept, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO = " + Session["userdeptno"].ToString(), "BRANCHNO");
            }
            else if (Session["usertype"].ToString() == "3")
            {
                string branchno = string.Empty;
                objCommon.FillDropDownList(ddlDept, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO = " + Session["userdeptno"].ToString(), "BRANCHNO");
            }
            ddlDept.Focus();
        }
        else
        {
            ddlDept.Items.Clear();
           // this.ClearStudGrid();
            ddlDegree.Focus();
        }
           
        this.FillExams();
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0 && ddlSession.SelectedIndex > 0 && ddlDept.SelectedIndex > 0)
            FillSchemeList();

    }
   
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT R ON (S.SEMESTERNO = R.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SEMESTERNAME", "R.SEMESTERNO>0 AND SESSIONNO="+ ddlSession.SelectedValue+" AND R.SCHEMENO=" + ddlScheme.SelectedValue, "S.SEMESTERNO");
        FillCourseList();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillCourseList();
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCourseList();
    }

    #endregion

    #region Private Methods
    
    private void FillCourseList()
    {
        try
        {
            int uano;
            if (Session["usertype"].ToString() != "3" || (Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1"))
            {
                uano = 0;
            }
            else
            {
                uano = Convert.ToInt32(Session["userno"]);
            }
            DataSet ds = objMarksEntry.GetCourses_By_ON_EXAMS_MarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), ddlExam.SelectedValue, uano);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds.Tables[0];
                lvCourse.DataBind();
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
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillCourseList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillExams()
    {
        try
        {
            objCommon.FillDropDownList(ddlExam, "ACTIVITY_MASTER A, SESSION_ACTIVITY S,ACD_EXAM_NAME E", "DISTINCT E.FLDNAME","E.EXAMNAME", "A.ACTIVITY_NO = S.ACTIVITY_NO AND A.EXAMNO = E.EXAMNO  AND GETDATE() >= S.START_DATE AND GETDATE() <= S.END_DATE AND S.STARTED = 1 AND UA_TYPE LIKE '%"+ Session["usertype"].ToString()+"%' AND PAGE_LINK like '%"+int.Parse(Request.QueryString["pageno"].ToString())+"%' AND S.SESSION_NO = "+ ddlSession.SelectedValue+" AND E.EXAMNAME <> '' AND E.STATUS = 'N'", "FLDNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillExams --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void FillSchemeList()
    {
        try
        {
            int uano;
            if ((Session["usertype"].ToString() == "3" && Session["dec"].ToString() == "1") || Session["usertype"].ToString() == "7")
            {
                uano = 0;
            }
            else
            {
                uano = Convert.ToInt32(Session["userno"]);
            }

            DataSet ds = objMarksEntry.GetSchemeForMarkEntry_Admin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue));//, uano);
           
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlScheme.Items.Clear();
                    ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                    ddlScheme.DataTextField = "SCHEMENAME";
                    ddlScheme.DataValueField = "SCHEMENO";
                    ddlScheme.DataSource = ds;
                    ddlScheme.DataBind();
                    ddlScheme.Focus();
                }
                else
                {
                    //For First Year Engineering..
                    // Schemeno = 1 for Autonomous
                    // 24 for RTM (Both Civil Schemes)
                    if (ddlDegree.SelectedValue == "1" )
                    {
                        ddlScheme.Items.Clear();
                        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                       
                        ddlScheme.Focus();
                    }
                    else
                    {
                        ddlScheme.Items.Clear();
                        ddlDept.Focus();
                    }
                }
            }
            else
            {
                //For First Year Engineering..
                // Schemeno = 1 for Autonomous
                // 24 for RTM (Both Civil Schemes)
                if (ddlDegree.SelectedValue == "1" )
                {
                    ddlScheme.Items.Clear();
                    ddlScheme.Items.Add(new ListItem("Please Select", "0"));
                    
                    ddlScheme.Focus();
                }
                else
                {
                    ddlScheme.Items.Clear();
                    ddlDept.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillSchemeList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    
   #endregion


    protected void btnGraphReport_Click(object sender, EventArgs e)
    {
        this.ShowGraphReport("GraphWiseGradesReport", "rptGraphGradesWiseStudents.rpt");
        
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {

            try
            {

                DataSet ds = objMarksEntry.GetEndExamMarksDataExcel(Convert.ToInt32(ddlSession2.SelectedValue) , Convert.ToInt32(lblCourse.ToolTip) , Convert.ToInt32(hdfSection.Value) ,Convert.ToInt32('0'),Convert.ToInt32(Session["userno"]));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + ddlDegree.SelectedValue);
                    //this.ShowReportExcel("xls",dcrReport, reportTitle, rptFileName);
                    GridView GVDayWiseAtt = new GridView();
                    GVDayWiseAtt.DataSource = ds;
                    GVDayWiseAtt.DataBind();

                    string attachment = "attachment; filename=EndSemesterExamReort.xls";
                    //string attachment = "attachment; filename=" + degree.Replace(" ", "_") + "_" + branch.Replace(" ", "_") + "_" + ccode + "_" + txtAttDate.Text.Trim() + "_" + txtRecDate.Text.Trim() + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    //Response.ContentType = "application/pdf";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVDayWiseAtt.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    this.ShowMessage("No information found based on given criteria.");
                }
               
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Academic_Examination_EndSemMarkEntry.btnExcelReport_Click() --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
            }
        
       
    }
}
