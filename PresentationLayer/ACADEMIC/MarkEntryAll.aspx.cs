//=================================================================================
// PROJECT NAME  : RF-CAMPUS [NITGOA]                                                         
// MODULE NAME   : ACADEMIC - ALL MARK ENTRY                                           
// CREATION DATE : 24 - JAN - 2018                                                    
// CREATED BY    :                                               
// MODIFIED BY   : AKASH RASAL                                                      
// MODIFIED DESC : MODIFY & APPLY RELATIVE & ABSOLUTE GRADING STSTEM 
// MODIFIED BY   : NARESH BEERLA                                            
// MODIFIED DESC : THIS PAGE IS USED FOR END SEM MARK ENTRY & CALCULATION OF TOTAL MARKS & GRADE ALLOTMENT AS PER THE MARKS 
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
using System.Xml.Linq;
using System.Data.OleDb;
using System.Data.Common;
using System.Threading;

public partial class Academic_MarkEntryAll : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

    string th_pr = string.Empty;
    int subid;
    string colTA;
    string colCT1;
    string colCT2;
    //int sectionno;
    int Grade_SectionNo=0;


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
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "CallButton();", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"Alert", "CallButton()", true);
            //Thread.Sleep(3000);

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
            //    CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                {
                }
                if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
                {

                    //lblText.Attributes.Add("style", "text-decoration:blink");
                    //CheckActivity();
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

                    
                    //objCommon.FillDropDownList(ddlSession, "SESSION_ACTIVITY SA INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=SA.SESSION_NO", "DISTINCT SA.SESSION_NO", "SM.SESSION_NAME", "SA.STARTED=1 AND SA.SHOW_STATUS=1", "");

                     string colgno = Session["college_nos"].ToString();
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT TOP 2 SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND C.COLLEGE_ID IN(" + colgno + ")", "SESSIONNO DESC");

                     objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
                  
                    if (Session["usertype"].ToString() == "3" )//&& Session["dec"].ToString() != "1")
                    {}
                       // this.ShowCourses();
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
                    objCommon.DisplayMessage(this.UpdatePanel1,"You are not authorized to view this page!!", this.Page);
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
        string colgno = Session["college_nos"].ToString();
        string sessionno = string.Empty;
         sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 AND COLLEGE_ID IN("+colgno+")");

         sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO) INNER JOIN ACD_SESSION_MASTER S ON (S.SESSIONNO=SA.SESSION_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE IN('END SEM') AND ISNULL(FLOCK,0)=1 AND COLLEGE_ID IN(" + colgno + ")");

        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG'");

        ActivityController objActController = new ActivityController();
        //DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.UpdatePanel1,"This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlSelection.Visible = true;
                pnlMarkEntry.Visible = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this.UpdatePanel1,"Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlSelection.Visible = true;
                pnlMarkEntry.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage(this.UpdatePanel1,"Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
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
                lvCourse.Visible = true;
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
        }
    }
   
    protected void lnkbtnCourse_Click(object sender, EventArgs e)
      {
        try
        {
            //Show the Student List with Exams that are ON
            //=============================================

            DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

            if (dsExams != null && dsExams.Tables.Count > 0)
            {
                if (dsExams.Tables[0].Rows.Count <= 0)
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Marks Entry Activity is OFF. Contact MIS!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Marks Entry Activity is OFF. Contact MIS!", this.Page);
                return;
            }
            LinkButton lnk = sender as LinkButton;
            

            int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lnk.ToolTip) + ""));
            string[] sec_batch_sem = lnk.CommandArgument.ToString().Split('+');
            string Section = sec_batch_sem[0].ToString();
            string semesterno = sec_batch_sem[2].ToString();            
            string College_id = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "SESSIONNO=" + ddlSession.SelectedValue);

            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + SchemeNo + ""));
            int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + SchemeNo + ""));


            // Check Mark Enrty Activitity 
            DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND BRANCH LIKE '%" + branchno + "%' AND DEGREENO LIKE '%" + Degreeno + "%' AND COLLEGE_ID IN (" + College_id + ") AND SEMESTER LIKE '%" + semesterno + "%')", "SESSIONNO DESC");

            if (ds_CheckActivity.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(this, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                return;
            }


             string isInternal = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ISNULL(COURSENO,0)COURSENO", "COURSENO =" + (lnk.ToolTip) + " and ISNULL(MAXMARKS_I,0)!=0.00"));

            int isinternal =  Convert.ToInt32(isInternal == string.Empty ? "0" : isInternal);
            //Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(COURSENO,0)COURSENO", "COURSENO =" + (lnk.ToolTip) + " and ISNULL(MAXMARKS_I,0)!=0.00"));

            if (Convert.ToInt32(isinternal) > 0)//|| isInternal != "" || isInternal != string.Empty
            {

                // CHECKS WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 
                string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_Crescent";
                string sp_parameters = "@P_COURSENO,@P_SECTIONNO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                string sp_callValues = "" + (lnk.ToolTip) + "," + Section + "," + SchemeNo + "," + (Session["userno"].ToString()) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";
                DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

                if (dschk.Tables[0].Rows.Count == 0 && dschk.Tables != null)
                {
                    //string islocked = dschk.Tables[0].Rows[0]["LOCK"]==string.Empty?"0":dschk.Tables[0].Rows[0]["LOCK"].ToString();

                    //if (islocked == "0" || islocked == string.Empty || islocked == null)
                    //{
                    //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
                    objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for !", this.Page);
                    return;
                    //}
                }


                if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                {
                    string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                    if (islocked == "0" || islocked == string.Empty || islocked == null)
                    {
                        //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
                        objCommon.DisplayMessage(UpdatePanel1, "Kindly Check Either Internal Mark Entry is not Completed / Locked for this Course", this.Page);
                        return;
                    }
                }

                // ENDS HERE WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 
            }
          
            if (!lnk.ToolTip.Equals(string.Empty))
            {

              //  CheckActivity();
                lblCourse.Text = lnk.Text;
                lblCourse.ToolTip = lnk.ToolTip;
                ViewState["lnk"] = lnk.ToolTip;
               // sectionno = Convert.ToInt16(hdfSection.Value);
                int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));
                //int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                String SchemeName = objCommon.LookUp("ACD_SCHEME", "upper(SCHEMENAME)", "SCHEMENO=" + Convert.ToInt32(SchemeNo) + "");
                String CcodeName = objCommon.LookUp("ACD_COURSE", "upper(CCODE)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "");
                String CourseName = objCommon.LookUp("ACD_COURSE", "upper(COURSE_NAME)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "");
             //   String subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "");
                lblScheme.Text = SchemeName.ToString();
                lblCourses.Text = CcodeName + " - " + CourseName;

                hdfSchemeNo.Value = SchemeNo.ToString();
                hdfExamType.Value = ExamType.ToString();
                string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                hdfSection.Value = sec_batch[0].ToString();
                hdfSemester.Value = sec_batch[2].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                hdfBatch.Value =  sec_batch.Length == 2 ? sec_batch[1].ToString() : "0" ;
                lblSession.Text= ddlSession.SelectedItem.Text;

                int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                hdfSubid.Value = subId.ToString();
                string sp_proc = "PKG_ACD_GET_RULE_FOR_ENDSEM_MARK_ENTRY";
                string sp_para = "@P_COURSENO,@P_SCHEMENO,@P_SESSIONNO";
                string sp_cValues = "" + (lnk.ToolTip) + "," + SchemeNo + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";
                DataSet dsRule = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

                if (dsRule.Tables[0].Rows.Count > 0 && dsRule.Tables != null && dsRule.Tables[0] != null)
                {
                    string Rule = dsRule.Tables[0].Rows[0]["RULE1"].ToString();
                    hdfCourseTotal.Value = dsRule.Tables[0].Rows[0]["COURSE_TOTAL"].ToString();
                    hdfMinPassMark.Value = dsRule.Tables[0].Rows[0]["MIN_PASSING"].ToString();
                    hdfMinPassMark_I.Value = dsRule.Tables[0].Rows[0]["INT_MIN_PASSING"].ToString();   // ADDED ON 11042022 FOR LAW
                    hdfRule.Value = Rule;
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the End Sem Mark Entry Rule is not Defined!", this.Page);
                    return;
                }



                // CHECKD THE LOCK CONDITION 

                //DataSet dsExams = objCommon.FillDropDown("ACD_EXAM_NAME", "DISTINCT EXAMNO AS EXAMNO,FLDNAME,EXAMNAME", "CONCAT(FLDNAME,'-',EXAMNO) AS FLDNAME2 ", "EXAMNO=" + Convert.ToInt32(sec_batch[2].ToString()) + " AND ISNULL(ACTIVESTATUS,0)=1", "EXAMNO");
                //string exams = string.Empty;
                //if (dsExams != null && dsExams.Tables.Count > 0 && dsExams.Tables[0].Rows.Count > 0)
                //{
                //    DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                //    while (dtr.Read())
                //    {
                //        exams += dtr["FLDNAME2"] == DBNull.Value ? string.Empty : dtr["FLDNAME2"].ToString() + ",";
                //    }
                //    dtr.Close();

                //}
                //else
                //    objCommon.DisplayMessage(this.UpdatePanel1, "Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);






                // CHECKS LOCK CONDIDTION FOR THE PARTICULAR COURSE
                this.ShowStudents(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value),"R.PREV_STATUS,R.REGNO");
        //        return;
                Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                if (Grade_SectionNo > 0)
                {
                    this.ShowGradesSection(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
                }
                else
                {
                    this.ShowGrades(Convert.ToInt16(lnk.ToolTip));
                }             
             
               
                //if (subId == 1 || subId == 8)
                //{
                //    int lockNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(LOCKS2)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                //    int countIdno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                //    if (lockNo < countIdno)
                //    {
                //        ShowMessage("Please First Lock Your Internal & Mid Sem marks !!!");
                //    }

                //}
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "CallButton();", true);

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
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
                //if (th_pr == "1" || th_pr == "8")
                //{
                    /////////
                    TextBox txtTAMarks = gvRow.FindControl("txtTAMarks") as TextBox;
                    TextBox txtTotMarks = gvRow.FindControl("txtTotMarks") as TextBox;
                    Label lblTAMarks = gvRow.FindControl("lblTAMarks") as Label;
                    Label lblTAMinMarks = gvRow.FindControl("lblTAMinMarks") as Label;

                    Label lblTotMinMarks = gvRow.FindControl("lblTotMinMarks") as Label;
                    Label lblTotMarks = gvRow.FindControl("lblTotMarks") as Label;

                    //if (lblTAMarks.ToolTip.ToUpper().Equals("TRUE")) txtTAMarks.Enabled = false;
                    //txtTAMarks.Attributes.Add("onblur", "validateMark1(this,this,this,this," + lblTAMarks.Text + "," + lblTAMinMarks.Text + ",'','1','0')");

                    //if (lblTotMarks.ToolTip.ToUpper().Equals("TRUE")) txtTotMarks.Enabled = false;
                    //txtTotMarks.Attributes.Add("onblur", "validateMark1(this,this,this,this," + lblTotMarks.Text + "," + lblTotMinMarks.Text + ",'','1','0')");
                  
                    ////////////////

                    TextBox txtESMarks = gvRow.FindControl("txtESMarks") as TextBox;
                    Label lblESMarks = gvRow.FindControl("lblESMarks") as Label;
                    Label lblESMinMarks = gvRow.FindControl("lblESMinMarks") as Label;
                    TextBox txtTotMarksAll = gvRow.FindControl("txtTotMarksAll") as TextBox;
                    TextBox txtTotPer = gvRow.FindControl("txtTotPer") as TextBox;
                    TextBox txtGrade = gvRow.FindControl("txtGrade") as TextBox;
                    TextBox txtGradeP = gvRow.FindControl("txtGradePoint") as TextBox;

                    HiddenField hidTotMarksAll = gvRow.FindControl("hidTotMarksAll") as HiddenField;
                    HiddenField hidTotPer = gvRow.FindControl("hidTotPer") as HiddenField;
                    HiddenField hidGrade = gvRow.FindControl("hidGrade") as HiddenField;
                    HiddenField hidGradePoint = gvRow.FindControl("hidGradePoint") as HiddenField;

                    //int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));

                    //int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                    int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                    int Scale = 0;//Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCALEDN_MARK", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                    Decimal obtper = Convert.ToDecimal(0);
                    Decimal InterMarkPer = Convert.ToDecimal(0);
                    Decimal Internalmarks = Convert.ToDecimal(0);
                    //int totalmarks = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "(ISNULL(MAXMARKS_E,0)+ISNULL(S1MAX,0)+ISNULL(S2MAX,0)+ISNULL(S4MAX,0))TOTMARKS", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                    int totalmarks = Convert.ToInt32(100);

                //---------------------------------------------- ADDED ON 11042022 FOR LAW -----------------------------------------------//

                    if (txtTAMarks.Text == string.Empty || txtTAMarks.Text == null || txtTAMarks.Text == "")
                    {
                        Internalmarks = 0;
                    }
                    else
                    {
                        Internalmarks = Convert.ToDecimal(txtTAMarks.Text);
                    }
                    //var obtper = ((Number(obt.val()) * 100) / 100).toFixed(2);                 
                    //var Extpassing = parseFloat((parseFloat(hdfMaxCourseMarks) * parseFloat(hdfMinPassMark)) / 100);
              //      Decimal obtper = (Convert.ToDecimal(txtESMarks.Text) * 100) / 100;

                    if (txtESMarks.Text == string.Empty || txtESMarks.Text == null || txtESMarks.Text == "")
                    {

                    }
                    else
                    {
                        obtper = Convert.ToDecimal((Convert.ToDecimal(txtESMarks.Text) * 100 / 100));
                    }
                    Decimal Extpassing = Convert.ToDecimal(Convert.ToDecimal(hdfMaxCourseMarks.Value) * Convert.ToDecimal(hdfMinPassMark.Value)) / 100;

                    if (Internalmarks <= 0)
                    {
                        InterMarkPer = 0;
                    }
                    else if (Internalmarks <= 0 && Convert.ToDouble(ViewState["MAXMARKS_I"].ToString()) <= 0)
                    {
                        InterMarkPer = 0;
                    }
                    else
                    {
                        InterMarkPer = Convert.ToDecimal((Convert.ToDecimal(Internalmarks) * 100) / Convert.ToDecimal(ViewState["MAXMARKS_I"].ToString()));
                    }
                   // Decimal InterMarkPer = Convert.ToDecimal((Convert.ToDecimal(Internalmarks) * 100) / Convert.ToDecimal(ViewState["MAXMARKS_I"].ToString()));

                    // if((txtGrade.Text !="AB" ||txtGrade.Text !="UFM"))
                    //if ((txtESMarks.Text != "902" || txtESMarks.Text != "902.00") || (txtESMarks.Text!="903.00" || txtESMarks.Text!="903") || (txtESMarks.Text!="904.00" || txtESMarks.Text!="904") || (txtESMarks.Text!="905.00" || txtESMarks.Text!="905"))
                    if (txtESMarks.Text == string.Empty || txtESMarks.Text == null || txtESMarks.Text == "")
                    {

                    }

                    else if (Convert.ToDecimal(txtESMarks.Text) > 901 && Convert.ToDecimal(txtESMarks.Text) < 906)
                    {
                    }
                    else if ((Convert.ToDecimal(InterMarkPer) < Convert.ToDecimal(hdfMinPassMark_I.Value)) || (Convert.ToDecimal(obtper) < Convert.ToDecimal(Extpassing)))
                    {
                        txtGrade.Text = "U";
                    }

                    //---------------------------------------------- ADDED ON 11042022 FOR LAW -----------------------------------------------//





                    if (txtESMarks.Text.Equals("902.00") || txtESMarks.Text.Equals("902") || txtESMarks.Text.Equals("903.00") || txtESMarks.Text.Equals("903") || txtESMarks.Text.Equals("904.00") || txtESMarks.Text.Equals("904") || txtESMarks.Text.Equals("905.00") || txtESMarks.Text.Equals("905"))
                    {
                        txtESMarks.Enabled = false;
                        txtTotMarksAll.Text = Convert.ToString(txtTAMarks.Text);
                        double Totalmarks = Convert.ToDouble(txtTotMarksAll.Text);
                        double TotPer = Convert.ToDouble((Totalmarks * 100) / Convert.ToDouble(hdfCourseTotal.Value));
                        txtTotPer.Text = Convert.ToString(TotPer) + ".00";
                    }

                    int count = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(1)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SUBID=" + Convert.ToInt32(subId) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ""));

                    if (count < 1)
                    {
                        //if (txtESMarks.Text.Equals("401.00") || txtESMarks.Text.Equals("401") || txtESMarks.Text.Equals("402.00") || txtESMarks.Text.Equals("402") || txtESMarks.Text.Equals("403.00") || txtESMarks.Text.Equals("403"))
                        if (txtESMarks.Text.Equals("902.00") || txtESMarks.Text.Equals("902") || txtESMarks.Text.Equals("903.00") || txtESMarks.Text.Equals("903") || txtESMarks.Text.Equals("904.00") || txtESMarks.Text.Equals("904") || txtESMarks.Text.Equals("905.00") || txtESMarks.Text.Equals("905"))
                        {
                            //txtESMarks.ReadOnly = true;
                            txtESMarks.Enabled = false;
                            txtTotMarksAll.Text = Convert.ToString(txtTAMarks.Text);
                            double Totalmarks = Convert.ToDouble(txtTotMarksAll.Text);
                            double TotPer = Convert.ToDouble((Totalmarks * 100) / Convert.ToDouble(hdfCourseTotal.Value));
                            txtTotPer.Text = Convert.ToString(TotPer) + ".00";
                        }
                    }


                    if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                    {
                        txtESMarks.Enabled = false;
                    }
                    //if (txtESMarks.Text.Trim() == "") txtESMarks.Enabled = true;

                    //if (txtESMarks.Text.Trim() == "401" || txtESMarks.Text.Trim() == "402" || txtESMarks.Text.Trim() == "403") txtESMarks.Enabled = false;

                    // COMMENTED BELOW LINES
                    //txtESMarks.Attributes.Add("onblur", " validateMarkTH(" + txtESMarks.ClientID + "," + lblESMarks.Text + "," + lblESMinMarks.Text + "," + txtTotMarks.ClientID + "," + txtTAMarks.ClientID + "," + txtTotMarksAll.ClientID + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + hidTotMarksAll.ClientID + "," + hidTotPer.ClientID + "," + hidGrade.ClientID + "," + hidGradePoint.ClientID + "," + Scale + "," + totalmarks + ")");
                    // ENDS HERE COMMENTED LINES


                    //txtESMarks.Attributes.Add("onblur", " validateMarkTH(" + txtESMarks.ClientID + "," + lblESMarks.Text + "," + lblESMinMarks.Text + "," + txtTotMarks.Text + "," + txtTAMarks.Text + "," + txtTotMarksAll.ClientID + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + lblTotMarks.Text + "," + lblTotPer.Text + "," + lblGrade.Text + "," + lblGradePoint.Text + ")");

                   
//                }
                //else
                //    if (th_pr != "1" && th_pr != "8")
                //    {
                //        TextBox txtESPRMarks = gvRow.FindControl("txtESPRMarks") as TextBox;
                //        Label lblESPRMarks = gvRow.FindControl("lblESPRMarks") as Label;
                //        Label lblESPRMinMarks = gvRow.FindControl("lblESPRMinMarks") as Label;

                //        TextBox txtTotPer = gvRow.FindControl("txtTotPer") as TextBox;
                //        TextBox txtGrade = gvRow.FindControl("txtGrade") as TextBox;
                //        TextBox txtGradeP = gvRow.FindControl("txtGradePoint") as TextBox;

                //        HiddenField hidTotPer = gvRow.FindControl("hidTotPer") as HiddenField;
                //        HiddenField hidGrade = gvRow.FindControl("hidGrade") as HiddenField;
                //        HiddenField hidGradePoint = gvRow.FindControl("hidGradePoint") as HiddenField;

                //        //TextBox txtMax = lvGrades.FindControl("txtMax") as TextBox;
                //        //TextBox txtMin = lvGrades.FindControl("txtMin") as TextBox;
                //        //int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));

                //        //int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                //        int Scale1 = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCALEDN_MARK", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                //        int totalmarks1 = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "(ISNULL(MAXMARKS_E,0)+ISNULL(S1MAX,0)+ISNULL(S2MAX,0)+ISNULL(S4MAX,0))TOTMARKS", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                //        //int totalmarks1 = Convert.ToInt32(100);
                //        if (lblESPRMarks.ToolTip.ToUpper().Equals("TRUE"))
                //        {
                //            txtESPRMarks.Enabled = false;

                //        }
                //        //if (txtESPRMarks.Text.Trim() == "") txtESPRMarks.Enabled = true;
                //        //if (txtESPRMarks.Text.Trim() == "401" || txtESPRMarks.Text.Trim() == "402" || txtESPRMarks.Text.Trim() == "403") txtESPRMarks.Enabled = false;
                //        txtESPRMarks.Attributes.Add("onblur", "validateMark(" + txtESPRMarks.ClientID + "," + lblESPRMarks.Text + "," + lblESPRMinMarks.Text + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + hidTotPer.ClientID + "," + hidGrade.ClientID + "," + hidGradePoint.ClientID + "," + Scale1 + "," + totalmarks1 + ")");
                //    }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.BindJS --> " + ex.Message + " " + ex.StackTrace);
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

    protected void btnLastSave_Click(object sender, EventArgs e)
    {
        //0 - means - unlock
        SaveAndLock1(0);
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
                    string FinalConversion = string.Empty;
                    //string studids1 = string.Empty;
                    //string marks1 = string.Empty;
                    //Label lbl1;
                    //TextBox txtMarks1;
                    //MarksEntryController objMarksEntry = new MarksEntryController();
                    Label lbl;
                    TextBox txtMarks;
                    TextBox txtTotMarksAll;
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
                        if (j == 2) //TA MARKS
                        {
                            HiddenField hidTotMarksAll = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;
                            HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
                            HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
                            HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
                            //Gather Exam Marks 
                            txtMarks = gvStudent.Rows[i].FindControl("txtTAMarks") as TextBox;
                            exam = "S2";
                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            studids += lbl.ToolTip + ",";

                            totmarks += Convert.ToString(hidTotMarksAll.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarksAll.Value).Trim() + ",";
                            grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
                            Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                            totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
                           
                        }
                        else if (j == 3) //CT1/FE1 MARKS
                        {
                            HiddenField hidTotMarksAll = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;
                            HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
                            HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
                            HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
                            //Gather Exam Marks 
                            txtMarks = gvStudent.Rows[i].FindControl("txtTotMarks") as TextBox;
                            exam = "S1";
                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            studids += lbl.ToolTip + ",";

                            totmarks += Convert.ToString(hidTotMarksAll.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarksAll.Value).Trim() + ",";
                            grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
                            Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                            totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
                        }
                        else
                        if (j == 4) //TH MARKS
                        {

                            //Gather Exam Marks       
                            txtMarks = gvStudent.Rows[i].FindControl("txtESMarks") as TextBox;
                            Label lblESMinMarks = gvStudent.Rows[i].FindControl("lblESMinMarks") as Label;


                            HiddenField hidTotMarksAll = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;
                            HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
                            HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
                            HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
                            txtTotMarksAll = gvStudent.Rows[i].FindControl("txtTotMarksAll") as TextBox;
                            txtTotPer = gvStudent.Rows[i].FindControl("txtTotPer") as TextBox;
                            txtGrade = gvStudent.Rows[i].FindControl("txtGrade") as TextBox;
                            txtGradePoint = gvStudent.Rows[i].FindControl("txtGradePoint") as TextBox;

                            //logic for round up TA mark entry
                            if (txtMarks.Text != string.Empty)
                            {
                                string r = string.Empty;
                                if (txtMarks.Text.Contains("."))
                                {
                                    //r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
                                    //if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
                                    //{
                                    //    int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
                                    //    txtMarks.Text = Convert.ToString(val + 1);
                                    //}
                                    //else
                                    txtMarks.Text = txtMarks.Text; //.Substring(0, txtMarks.Text.IndexOf('.'))

                                }
                                else
                                    r = txtMarks.Text;
                            }
                            exam = "EXTER";

                            //Check if marks exists, then add the id
                            //if (!string.IsNullOrEmpty(txtMarks.Text.Trim()))
                            //{
                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            studids += lbl.ToolTip + ",";
                            totmarks += Convert.ToString(hidTotMarksAll.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarksAll.Value).Trim() + ",";
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
                                    //r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
                                    //if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
                                    //{
                                    //    int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
                                    //    txtMarks.Text = Convert.ToString(val + 1);
                                    //}
                                    //else
                                    txtMarks.Text = txtMarks.Text;
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

                        TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;

                        TextBox txtpoint = dataRow.FindControl("txtGradePoints") as TextBox;

                        HiddenField hidTotStud = dataRow.FindControl("hidTotalStudent") as HiddenField;


                        point += txtpoint.Text.Trim() == string.Empty ? "-100," : txtpoint.Text + ",";
                        lgrade += txtgra.Text.Trim() == string.Empty ? "-100," : txtgra.Text + ",";
                        max += txtmax.Text.Trim() == string.Empty ? "-100," : txtmax.Text + ",";
                        min += txtmin.Text.Trim() == string.Empty ? "-100," : txtmin.Text + ",";
                        totStud += Convert.ToString(hidTotStud.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotStud.Value).Trim() + ",";
                    }


                    if (!string.IsNullOrEmpty(studids))
                    {
                        //cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAll(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()));
                        cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAllNew(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), FinalConversion);
                    }
                }
            }


            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                    if (subId == 1 || subid == 8)
                    {
                        int lockNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(LOCKS2)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                        int countIdno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                        if (lockNo < countIdno)
                        {

                            objMarksEntry.UpdateMinnorMidLockMarkEntry(Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(hdfSchemeNo.Value), Convert.ToInt16(Session["userno"]), Convert.ToInt16(hdfSection.Value), exam, ViewState["ipAddress"].ToString());
                            //ShowMessage("Please First Lock Your Internal & Mid Sem marks !!!");
                        }

                    }
                    //ShowMessage("Marks Locked Successfully!!!");
                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Locked Successfully!!!", this.Page);
                }
                else
                {
                    //ShowMessage("Marks Saved Successfully!!!");
                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Saved Successfully!!!", this.Page);
                }
            }
            else
                //ShowMessage("Error in Saving Marks!");
                objCommon.DisplayMessage(this.UpdatePanel1, "Error in Saving Marks!", this.Page);

               this.ShowStudents(courseno, Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value),"R.PREV_STATUS,R.REGNO");

            
                int SectionCount = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "DISTINCT ISNULL(SECTIONNO,0)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                if (SectionCount == 0)
                {
                    this.ShowGrades(Convert.ToInt32(lblCourse.ToolTip));
                }
                else
                {
                    this.ShowGradesSection(Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
                }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void SaveAndLock1(int lock_status)
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

        //    return;
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
                    string FinalConversion = string.Empty;
                    //string studids1 = string.Empty;
                    //string marks1 = string.Empty;
                    //Label lbl1;
                    //TextBox txtMarks1;
                    //MarksEntryController objMarksEntry = new MarksEntryController();
                    Label lbl;
                    TextBox txtMarks;
                    TextBox txtTotMarksAll;
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
                        
                      
                            if (j == 4) //TH MARKS
                            {

                                //Gather Exam Marks       
                                txtMarks = gvStudent.Rows[i].FindControl("txtESMarks") as TextBox;
                                Label lblESMinMarks = gvStudent.Rows[i].FindControl("lblESMinMarks") as Label;


                                HiddenField hidTotMarksAll = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;
                                HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
                                HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
                                HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
                                HiddenField hidConversion = gvStudent.Rows[i].FindControl("hdfConversion") as HiddenField;
                                                                
                                txtTotMarksAll = gvStudent.Rows[i].FindControl("txtTotMarksAll") as TextBox;
                                txtTotPer = gvStudent.Rows[i].FindControl("txtTotPer") as TextBox;
                                txtGrade = gvStudent.Rows[i].FindControl("txtGrade") as TextBox;
                                txtGradePoint = gvStudent.Rows[i].FindControl("txtGradePoint") as TextBox;

                                //logic for round up TA mark entry
                                if (txtMarks.Text != string.Empty)
                                {
                                    string r = string.Empty;
                                    if (txtMarks.Text.Contains("."))
                                    {
                                        //r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
                                        //if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
                                        //{
                                        //    int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
                                        //    txtMarks.Text = Convert.ToString(val + 1);
                                        //}
                                        //else
                                        txtMarks.Text = txtMarks.Text; //.Substring(0, txtMarks.Text.IndexOf('.'))

                                    }
                                    else
                                        r = txtMarks.Text;
                                }
                                //exam = "EXTERMARK";
                                exam = "EXTER";

                                if (txtMarks.Text.Equals("902.00") || txtMarks.Text.Equals("902") || txtMarks.Text.Equals("903.00") || txtMarks.Text.Equals("903") || txtMarks.Text.Equals("904.00") || txtMarks.Text.Equals("904") || txtMarks.Text.Equals("905.00") || txtMarks.Text.Equals("905"))
                                {
                                    hidGradePoint.Value = "0.00";
                                    hidConversion.Value = "0.00";
                                }

                                //Check if marks exists, then add the id
                                //if (!string.IsNullOrEmpty(txtMarks.Text.Trim()))
                                //{
                                #region Backup Previous dt 23032020
                                //marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                                //studids += lbl.ToolTip + ",";
                                //totmarks += Convert.ToString(hidTotMarksAll.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarksAll.Value).Trim() + ",";
                                //grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
                                //Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                                //totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
                                #endregion

                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                                studids += lbl.ToolTip + ",";
                                totmarks += Convert.ToString(txtTotMarksAll.Text).Trim() == string.Empty ? "-100," : Convert.ToString(txtTotMarksAll.Text).Trim() + ",";
                                grade += Convert.ToString(txtGrade.Text).Trim() == string.Empty ? "-100," : Convert.ToString(txtGrade.Text).Trim() + ",";
                                Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                                totPer += Convert.ToString(txtTotPer.Text).Trim() == string.Empty ? "-100," : Convert.ToString(txtTotPer.Text).Trim() + ",";
                                FinalConversion += Convert.ToString(hidConversion.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidConversion.Value).Trim()+",";
                                //}
                            }
                            //else if (j == 5) //PR MARKS
                            //{
                            //    //Gather Exam Marks 
                            //    txtMarks = gvStudent.Rows[i].FindControl("txtESPRMarks") as TextBox;

                            //    HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
                            //    HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
                            //    HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;

                            //    //logic for round up TA mark entry
                            //    if (txtMarks.Text != string.Empty)
                            //    {
                            //        string r = string.Empty;
                            //        if (txtMarks.Text.Contains("."))
                            //        {
                            //            //r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
                            //            //if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
                            //            //{
                            //            //    int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
                            //            //    txtMarks.Text = Convert.ToString(val + 1);
                            //            //}
                            //            //else
                            //            txtMarks.Text = txtMarks.Text;
                            //        }
                            //        else
                            //            r = txtMarks.Text;
                            //    }
                            //    exam = "S4";

                            //    //Check if marks exists, then add the id
                            //    //if (!string.IsNullOrEmpty(txtMarks.Text.Trim()))
                            //    //{
                            //    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            //    studids += lbl.ToolTip + ",";
                            //    totmarks += "-100,";
                            //    grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
                            //    Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                            //    totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
                            //    //}
                            //}

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

                        TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;

                        TextBox txtpoint = dataRow.FindControl("txtGradePoints") as TextBox;

                        HiddenField hidTotStud = dataRow.FindControl("hidTotalStudent") as HiddenField;


                        point += txtpoint.Text.Trim() == string.Empty ? "-100," : txtpoint.Text + ",";
                        lgrade += txtgra.Text.Trim() == string.Empty ? "-100," : txtgra.Text + ",";
                        max += txtmax.Text.Trim() == string.Empty ? "-100," : txtmax.Text + ",";
                        min += txtmin.Text.Trim() == string.Empty ? "-100," : txtmin.Text + ",";
                        totStud += Convert.ToString(hidTotStud.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotStud.Value).Trim() + ",";
                    }


                    if (!string.IsNullOrEmpty(studids))
                    {
                        //  cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAll(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()));
                        cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAllNew(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value),FinalConversion);
                    }
                }
            }


            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                    //if (subId == 1 || subid == 8)
                    //{
                    //    int lockNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(LOCKS2)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                    //    int countIdno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                    //    if (lockNo < countIdno)
                    //    {

                    //        objMarksEntry.UpdateMinnorMidLockMarkEntry(Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(hdfSchemeNo.Value), Convert.ToInt16(Session["userno"]), Convert.ToInt16(hdfSection.Value), exam, ViewState["ipAddress"].ToString());
                    //        //ShowMessage("Please First Lock Your Internal & Mid Sem marks !!!");
                    //    }

                    //}
                  //  ShowMessage("Marks Locked Successfully!!!");
                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Locked Successfully!!!", this.Page);
                    btnLastSave.Enabled = false;
                    btnLock.Enabled = false;
                }
                else
                {
                 //   ShowMessage("Marks Saved Successfully!!!");
                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Saved Successfully!!!", this.Page);
                }
            }
            else
            {
              //  ShowMessage("Error in Saving Marks!");
                objCommon.DisplayMessage(this.UpdatePanel1, "Error in Saving Marks!", this.Page);
            }
            this.ShowStudents(courseno, Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
            Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
            if (Grade_SectionNo > 0)
            {
                this.ShowGradesSection(Convert.ToInt16(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
            }
            else
            {
                this.ShowGrades(Convert.ToInt32(lblCourse.ToolTip));
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    #region SAVE&LOCK1 BACKUP
    //private void SaveAndLock1(int lock_status)
    //{
    //    try
    //    {
    //        string exam = string.Empty;
    //        CustomStatus cs = CustomStatus.Error;
    //        int courseno = Convert.ToInt32(lblCourse.ToolTip);
    //        string[] course = lblCourse.Text.Split('-');
    //        string ccode = course[0].Trim();

    //        //Check for lock and null marks
    //        if (lock_status == 1)
    //        {
    //            if (CheckMarks(lock_status) == false)
    //            {
    //                return;
    //            }
    //        }

    //        for (int j = 2; j < gvStudent.Columns.Count; j++)
    //        {
    //            if (gvStudent.Columns[j].Visible == true)
    //            {
    //                string studids = string.Empty;
    //                string marks = string.Empty;
    //                string totmarks = string.Empty;
    //                string grade = string.Empty;
    //                string Gpoint = string.Empty;
    //                string totPer = string.Empty;
    //                //string studids1 = string.Empty;
    //                //string marks1 = string.Empty;
    //                //Label lbl1;
    //                //TextBox txtMarks1;
    //                //MarksEntryController objMarksEntry = new MarksEntryController();
    //                Label lbl;
    //                TextBox txtMarks;
    //                TextBox txtTotMarksAll;
    //                TextBox txtTotPer;
    //                TextBox txtGrade;
    //                TextBox txtGradePoint;
    //                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //                //Note : -100 for Marks will be converted as NULL           
    //                //NULL means mark entry not done.                           
    //                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //                for (int i = 0; i < gvStudent.Rows.Count; i++)
    //                {
    //                    //Gather Student IDs 
    //                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
    //                    if (j == 2) //TA MARKS
    //                    {
    //                        HiddenField hidTotMarksAll = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;
    //                        HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
    //                        HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
    //                        HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
    //                        //Gather Exam Marks 
    //                        txtMarks = gvStudent.Rows[i].FindControl("txtTAMarks") as TextBox;
    //                        exam = "S2";
    //                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
    //                        studids += lbl.ToolTip + ",";

    //                        totmarks += Convert.ToString(hidTotMarksAll.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarksAll.Value).Trim() + ",";
    //                        grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
    //                        Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
    //                        totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";

    //                    }
    //                    else if (j == 3) //CT1/FE1 MARKS
    //                    {
    //                        HiddenField hidTotMarksAll = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;
    //                        HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
    //                        HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
    //                        HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
    //                        //Gather Exam Marks 
    //                        txtMarks = gvStudent.Rows[i].FindControl("txtTotMarks") as TextBox;
    //                        exam = "S1";
    //                        marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
    //                        studids += lbl.ToolTip + ",";

    //                        totmarks += Convert.ToString(hidTotMarksAll.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarksAll.Value).Trim() + ",";
    //                        grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
    //                        Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
    //                        totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
    //                    }
    //                    else
    //                        if (j == 4) //TH MARKS
    //                        {

    //                            //Gather Exam Marks       
    //                            txtMarks = gvStudent.Rows[i].FindControl("txtESMarks") as TextBox;
    //                            Label lblESMinMarks = gvStudent.Rows[i].FindControl("lblESMinMarks") as Label;


    //                            HiddenField hidTotMarksAll = gvStudent.Rows[i].FindControl("hidTotMarksAll") as HiddenField;
    //                            HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
    //                            HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
    //                            HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;
    //                            txtTotMarksAll = gvStudent.Rows[i].FindControl("txtTotMarksAll") as TextBox;
    //                            txtTotPer = gvStudent.Rows[i].FindControl("txtTotPer") as TextBox;
    //                            txtGrade = gvStudent.Rows[i].FindControl("txtGrade") as TextBox;
    //                            txtGradePoint = gvStudent.Rows[i].FindControl("txtGradePoint") as TextBox;

    //                            //logic for round up TA mark entry
    //                            if (txtMarks.Text != string.Empty)
    //                            {
    //                                string r = string.Empty;
    //                                if (txtMarks.Text.Contains("."))
    //                                {
    //                                    //r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
    //                                    //if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
    //                                    //{
    //                                    //    int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
    //                                    //    txtMarks.Text = Convert.ToString(val + 1);
    //                                    //}
    //                                    //else
    //                                    txtMarks.Text = txtMarks.Text; //.Substring(0, txtMarks.Text.IndexOf('.'))

    //                                }
    //                                else
    //                                    r = txtMarks.Text;
    //                            }
    //                            exam = "EXTERMARK";

    //                            //Check if marks exists, then add the id
    //                            //if (!string.IsNullOrEmpty(txtMarks.Text.Trim()))
    //                            //{
    //                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
    //                            studids += lbl.ToolTip + ",";
    //                            totmarks += Convert.ToString(hidTotMarksAll.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotMarksAll.Value).Trim() + ",";
    //                            grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
    //                            Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
    //                            totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
    //                            //}
    //                        }
    //                        else if (j == 5) //PR MARKS
    //                        {
    //                            //Gather Exam Marks 
    //                            txtMarks = gvStudent.Rows[i].FindControl("txtESPRMarks") as TextBox;

    //                            HiddenField hidTotPer = gvStudent.Rows[i].FindControl("hidTotPer") as HiddenField;
    //                            HiddenField hidGrade = gvStudent.Rows[i].FindControl("hidGrade") as HiddenField;
    //                            HiddenField hidGradePoint = gvStudent.Rows[i].FindControl("hidGradePoint") as HiddenField;

    //                            //logic for round up TA mark entry
    //                            if (txtMarks.Text != string.Empty)
    //                            {
    //                                string r = string.Empty;
    //                                if (txtMarks.Text.Contains("."))
    //                                {
    //                                    //r = txtMarks.Text.Substring(txtMarks.Text.IndexOf('.') + 1, 1);
    //                                    //if (Convert.ToInt32(r) >= 5)//ROUNDING IF IN DECIMAL PLACE> .5
    //                                    //{
    //                                    //    int val = Convert.ToInt32(txtMarks.Text.Substring(0, txtMarks.Text.IndexOf('.')));
    //                                    //    txtMarks.Text = Convert.ToString(val + 1);
    //                                    //}
    //                                    //else
    //                                    txtMarks.Text = txtMarks.Text;
    //                                }
    //                                else
    //                                    r = txtMarks.Text;
    //                            }
    //                            exam = "S4";

    //                            //Check if marks exists, then add the id
    //                            //if (!string.IsNullOrEmpty(txtMarks.Text.Trim()))
    //                            //{
    //                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
    //                            studids += lbl.ToolTip + ",";
    //                            totmarks += "-100,";
    //                            grade += Convert.ToString(hidGrade.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGrade.Value).Trim() + ",";
    //                            Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
    //                            totPer += Convert.ToString(hidTotPer.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotPer.Value).Trim() + ",";
    //                            //}
    //                        }

    //                }

    //                string lgrade = string.Empty;
    //                string max = string.Empty;
    //                string min = string.Empty;
    //                string point = string.Empty;
    //                string totStud = string.Empty;
    //                //THIS IS FOR ADDING LV GRADES VALUES 
    //                foreach (ListViewDataItem dataRow in lvGrades.Items)
    //                {

    //                    TextBox txtgra = dataRow.FindControl("txtGrades") as TextBox;

    //                    TextBox txtmax = dataRow.FindControl("txtMax") as TextBox;

    //                    TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;

    //                    TextBox txtpoint = dataRow.FindControl("txtGradePoints") as TextBox;

    //                    HiddenField hidTotStud = dataRow.FindControl("hidTotalStudent") as HiddenField;


    //                    point += txtpoint.Text.Trim() == string.Empty ? "-100," : txtpoint.Text + ",";
    //                    lgrade += txtgra.Text.Trim() == string.Empty ? "-100," : txtgra.Text + ",";
    //                    max += txtmax.Text.Trim() == string.Empty ? "-100," : txtmax.Text + ",";
    //                    min += txtmin.Text.Trim() == string.Empty ? "-100," : txtmin.Text + ",";
    //                    totStud += Convert.ToString(hidTotStud.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidTotStud.Value).Trim() + ",";
    //                }


    //                if (!string.IsNullOrEmpty(studids))
    //                {
    //                   //  cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAll(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()));
    //                    cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAllNew(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
    //                }
    //            }
    //        }


    //        if (cs.Equals(CustomStatus.RecordSaved))
    //        {
    //            if (lock_status == 1)
    //            {
    //                int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
    //                if (subId == 1 || subid == 8)
    //                {
    //                    int lockNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(LOCKS2)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
    //                    int countIdno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
    //                    if (lockNo < countIdno)
    //                    {

    //                        objMarksEntry.UpdateMinnorMidLockMarkEntry(Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(hdfSchemeNo.Value), Convert.ToInt16(Session["userno"]), Convert.ToInt16(hdfSection.Value), exam, ViewState["ipAddress"].ToString());
    //                        //ShowMessage("Please First Lock Your Internal & Mid Sem marks !!!");
    //                    }

    //                }
    //                //ShowMessage("Marks Locked Successfully!!!");
    //            }
    //            else
    //            {
    //               // ShowMessage("Marks Saved Successfully!!!");
    //            }
    //        }
    //        //else
    //        //    ShowMessage("Error in Saving Marks!");

    //        this.ShowStudents(courseno, Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
    //        Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
    //        if (Grade_SectionNo > 0)
    //        {
    //            this.ShowGradesSection(Convert.ToInt16(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
    //        }
    //        else
    //        {
    //            this.ShowGrades(Convert.ToInt32(lblCourse.ToolTip));
    //        }
            
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_MarkEntryAll.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    #endregion
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
                        if (j == 4)//2) //ESEM-TH MARKS
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
                                    //ShowMessage("Marks Entry Not Completed!!!");
                                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entry Not Completed!! Please Enter the Marks for all Students.!!!", this.Page);
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
                                    if (Convert.ToDouble(txt.Text) > Convert.ToDouble(lblmax.Text))
                                    {
                                        //Note : 401 for Absent
                                        //if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402  || Convert.ToInt16(txt.Text) == 403)
                                        //{
                                        //}
                                        //if (Convert.ToInt16(txt.Text) == 902 || Convert.ToInt16(txt.Text) == 903 || Convert.ToInt16(txt.Text) == 904 || Convert.ToInt16(txt.Text) == 905)
                                        if ((Convert.ToDouble(txt.Text) > 901 && Convert.ToDouble(txt.Text) < 906))
                                        {
                                        }
                                        else
                                        {
                                            //ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lblmax.Text + "]");
                                            objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lblmax.Text + "]", this.Page);
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
                                        //ShowMessage("Marks Entry Not Completed!!!");
                                        objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entry Not Completed!!!", this.Page);
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //else
                    //    if (gvStudent.Columns[5].Visible == true)
                    //    {
                    //        if (j == 3) //ESEM-PR MARKS
                    //        {
                    //            lblmax = gvStudent.Rows[i].Cells[j].FindControl("lblESPRMarks") as Label;      //Max Marks 
                    //            lblmin = gvStudent.Rows[i].Cells[j].FindControl("lblESPRMinMarks") as Label;      //Max Marks 
                    //            txt = gvStudent.Rows[i].Cells[j].FindControl("txtESPRMarks") as TextBox;    //Marks Entered 
                    //            maxMarks = lblmax.Text.Trim();
                    //            minMarks = lblmin.Text.Trim();
                    //            marks = txt.Text.Trim();

                    //            if (!txt.Text.Trim().Equals(string.Empty) && !lblmax.Text.Trim().Equals(string.Empty) && !lblmin.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                    //            {
                    //                if (txt.Text == "")
                    //                {
                    //                    //ShowMessage("Marks Entry Not Completed!!!");
                    //                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entry Not Completed!!!", this.Page);
                    //                    txt.Focus();
                    //                    flag = false;
                    //                    break;
                    //                }
                    //                else
                    //                {
                    //                    if (txt.Text.Contains("."))
                    //                    {
                    //                        string r = txt.Text.IndexOf('.') > 0 ? "0" : txt.Text.Substring(txt.Text.IndexOf('.') + 1, 1);

                    //                        if (Convert.ToInt32(r) >= 5)
                    //                        {
                    //                            int val = Convert.ToInt32(txt.Text.Substring(0, txt.Text.IndexOf('.')));
                    //                            txt.Text = Convert.ToString(val + 1);
                    //                        }
                    //                        else
                    //                            txt.Text = txt.Text.Substring(0, txt.Text.IndexOf('.'));
                    //                    }
                    //                    //Check for Marks entered greater than Max Marks
                    //                    if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lblmax.Text))
                    //                    {
                    //                        //Note : 401 for Absent
                    //                        if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402  || Convert.ToInt16(txt.Text) == 403)
                    //                        {
                    //                        }
                    //                        else
                    //                        {
                    //                            //ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lblmax.Text + "]");
                    //                            objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lblmax.Text + "]", this.Page);
                    //                            txt.Focus();
                    //                            flag = false;
                    //                            break;
                    //                        }
                    //                    }
                    //                    //else
                    //                    //    if (Convert.ToInt16(txt.Text) < Convert.ToInt16(lblmin.Text))
                    //                    //    {

                    //                    //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Lesser than Min Marks[" + lblmin.Text + "]");
                    //                    //        txt.Focus();
                    //                    //        flag = false;
                    //                    //        break;
                    //                    //    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (txt.Enabled == true)
                    //                {
                    //                    if (lock_status == 1)
                    //                    {
                    //                        //ShowMessage("Marks Entry Not Completed!!!");
                    //                        objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entry Not Completed!!!", this.Page);
                    //                        txt.Focus();
                    //                        flag = false;
                    //                        break;
                    //                    }
                                        
                    //                }
                    //            }
                    //        }
                    //    }

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
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private void ShowGrades(int Courseno)
    {
        int subid = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Courseno + ""));
        int degreeNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "DEGREENO", "C.COURSENO=" + Courseno + ""));

        string sp_proc = "PKG_GET_ALL_GRADES";
        string sp_para = "@P_SESSIONO,@P_SUBID,@P_COURSENO,@P_UANO,@P_DEGREENO,@P_SECTIONNO";
        string sp_cValues = "" + (Convert.ToInt16(ddlSession.SelectedItem.Value)) + "," + subid + "," + Courseno + "," + Convert.ToInt16(Session["userno"].ToString()) + "," + degreeNo + "," + Convert.ToInt32(hdfSection.Value) + "";

        DataSet dsGrades = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

        //DataSet dsGrades = objMarksEntry.GetAllGrades(Convert.ToInt16(ddlSession.SelectedItem.Value), subid, Courseno, Convert.ToInt16(Session["userno"].ToString()), degreeNo);
        if (dsGrades != null && dsGrades.Tables.Count > 0 && dsGrades.Tables[0].Rows.Count > 0)
        {
            lvGrades.DataSource = dsGrades;
            lvGrades.DataBind();

            int i = 1;
            int j = 0;
            int sum = 0;
            foreach (ListViewDataItem dataRow in lvGrades.Items)
            {
                if (i < lvGrades.Items.Count - 2)
                {
                    TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;
                    //int MaxValue = Convert.ToInt32(txtmin.Text) - 1;
                    double MaxValue = Convert.ToDouble(txtmin.Text) - 1;
                    ListViewDataItem item = lvGrades.Items[i];
                    TextBox a = (TextBox)item.FindControl("txtMax");
                    a.Text = MaxValue.ToString();

                }
                i++;
                TextBox txtTotalStud = dataRow.FindControl("txtTotalStudent") as TextBox;
                sum = sum + Convert.ToInt32(txtTotalStud.Text);
                //txtTotalAllStudent.Text = sum.ToString();
            }
            int k = 0;
            foreach (GridViewRow gvRow in gvStudent.Rows)
            {
                string subid1 = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
                if (subid1 == "1" || subid1 == "8")
                {

                    TextBox txtESMarks = gvRow.FindControl("txtESMarks") as TextBox;
                    Label lblESMarks = gvRow.FindControl("lblESMarks") as Label;
                    Label lblESMinMarks = gvRow.FindControl("lblESMinMarks") as Label;

                    if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                    {

                        k = k + 1;
                        int count = gvStudent.Rows.Count;
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
                if (subid1 != "1" && subid1 != "8")
                {

                    //TextBox txtESPRMarks = gvRow.FindControl("txtESPRMarks") as TextBox;
                    //Label lblESPRMarks = gvRow.FindControl("lblESPRMarks") as Label;
                    //Label lblESPRMinMarks = gvRow.FindControl("lblESPRMinMarks") as Label;
                    TextBox txtESMarks = gvRow.FindControl("txtESMarks") as TextBox;
                    Label lblESMarks = gvRow.FindControl("lblESMarks") as Label;
                    Label lblESMinMarks = gvRow.FindControl("lblESMinMarks") as Label;


                    if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                    {
                        k = k + 1;
                        int count = gvStudent.Rows.Count;
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


    private void ShowGradesSection(int Courseno, int SectionNo, int semesterno)
    {
        int subid = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Courseno + ""));
        int degreeNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "DEGREENO", "C.COURSENO=" + Courseno + ""));

        DataSet dsGrades = objMarksEntry.GetAllGradesSection(Convert.ToInt16(ddlSession.SelectedItem.Value), subid, Courseno, Convert.ToInt16(Session["userno"].ToString()), degreeNo, SectionNo, semesterno);
        if (dsGrades != null && dsGrades.Tables.Count > 0)
        {
            lvGrades.DataSource = dsGrades;
            lvGrades.DataBind();

            int i = 1;
            int j = 0;
            int sum = 0;
            foreach (ListViewDataItem dataRow in lvGrades.Items)
            {
                if (i < lvGrades.Items.Count - 2)
                {
                    TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;
                  //  int MaxValue = Convert.ToInt32(txtmin.Text) - 1;
                    double MaxValue = Convert.ToDouble(txtmin.Text) - 1;
                    ListViewDataItem item = lvGrades.Items[i];
                    TextBox a = (TextBox)item.FindControl("txtMax");
                    a.Text = MaxValue.ToString();

                }
                i++;
                TextBox txtTotalStud = dataRow.FindControl("txtTotalStudent") as TextBox;
                sum = sum + Convert.ToInt32(txtTotalStud.Text);
                //txtTotalAllStudent.Text = sum.ToString();
            }
            int k = 0;
            foreach (GridViewRow gvRow in gvStudent.Rows)
            {
                string subid1 = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
                if (subid1 == "1" || subid1 == "8")
                {

                    TextBox txtESMarks = gvRow.FindControl("txtESMarks") as TextBox;
                    Label lblESMarks = gvRow.FindControl("lblESMarks") as Label;
                    Label lblESMinMarks = gvRow.FindControl("lblESMinMarks") as Label;

                    if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                    {

                        k = k + 1;
                        int count = gvStudent.Rows.Count;
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
                if (subid1 != "1" && subid1 != "8")
                {

                    //TextBox txtESPRMarks = gvRow.FindControl("txtESPRMarks") as TextBox;
                    //Label lblESPRMarks = gvRow.FindControl("lblESPRMarks") as Label;
                    //Label lblESPRMinMarks = gvRow.FindControl("lblESPRMinMarks") as Label;
                    TextBox txtESMarks = gvRow.FindControl("txtESMarks") as TextBox;
                    Label lblESMarks = gvRow.FindControl("lblESMarks") as Label;
                    Label lblESMinMarks = gvRow.FindControl("lblESMinMarks") as Label;

                    if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                    {
                        k = k + 1;
                        int count = gvStudent.Rows.Count;
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

    private void ShowStudents(int courseNo, int sectionNo ,int semesterNo,string orderby)
    {
   
        try
        {
            //Check Exam Activity is ON

            int lockcount = 0;
            //==========================
            DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

            //if (dsExams != null && dsExams.Tables.Count > 0)
            //{
            //    if (dsExams.Tables[0].Rows.Count <= 0)
            //    {
            //        //objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entry Not Completed!!!", this.Page);
            //        objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entry Activity is OFF. Contact MIS!", this.Page);
            //        return;
            //    }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Entry Activity is OFF. Contact MIS!", this.Page);
            //    return;
            //}



            subid = Convert.ToInt16(objCommon.LookUp("ACD_SUBJECTTYPE", "ISNULL(SUBID,0)", " SUBID IN (SELECT SUBID FROM ACD_COURSE WHERE COURSENO =" + lblCourse.ToolTip + " )"));
            //int ua_no = 0;
            int count = 0;

            // CHECK IF VALUER IS ASSIGNED
            //if (subid == 1 )
            //    ua_no = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0)UA_NUM", " SESSIONNO = " + ddlSession.SelectedValue + "  AND COURSENO =  " + lblCourse.ToolTip + " ORDER BY UA_NUM DESC"));
            //else
            //    ua_no = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0)UA_NUM", " SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO =  " + lblCourse.ToolTip + " ORDER BY UA_NUM DESC"));
            //if (subid == 7 || subid == 8 || subid == 9 )
            //    //count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));
            //    count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));
            //else


            //// Modified by Prashant amle. 30/11/2015
            //if (subid != 1 && subid != 8)
            //{
            //    count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO_PRAC,UA_NO_PRAC),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));
            //}
            //else
            //{
            //    count = Convert.ToInt32(objCommon.LookUp("(SELECT DISTINCT ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0)UA_NUM FROM ACD_STUDENT_RESULT WHERE  SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + "  AND COURSENO = " + lblCourse.ToolTip.ToString() + " AND  ISNULL(ISNULL(VALUER_UA_NO,UA_NO),0) <> 0 )A", "COUNT(*)", "A.UA_NUM =" + Session["userno"].ToString()));
            //}

            //if (count == 0)
            //{
            //    gvStudent.Columns[2].Visible = false;
            //    gvStudent.Columns[3].Visible = false;

            //    btnSave.Visible = false;
            //    btnLock.Visible = false;
            //    gvStudent.DataSource = null;
            //    gvStudent.DataBind();
            //    pnlMarkEntry.Visible = false;
            //    pnlStudGrid.Visible = false;
            //    objCommon.DisplayMessage(this.UpdatePanel1,"You are not allowed to do mark entry! ", this.Page);
            //    return;
            //}
            //else
            //{
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

                dsStudent = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)	INNER JOIN ACD_COURSE C ON (R.COURSENO = C.COURSENO)	LEFT OUTER JOIN ACD_STUDENT_TEST_MARK T ON (R.SESSIONNO = T.SESSIONNO AND R.COURSENO = T.COURSENO AND R.IDNO = T.IDNO)", "DISTINCT R.IDNO,R.REGNO,R.ROLL_NO ,S.DEGREENO,S.STUDNAME AS STUDNAME,CAST(R.S4MARK AS FLOAT) AS S4MARK,ISNULL(C.S4MAX,0) AS S4MAX,ISNULL(C.S4MIN,0) AS S4MIN,R.LOCKS4,CAST(T.EXTERN_MARK AS FLOAT) AS EXTERMARK,CAST(R.INTERMARK AS FLOAT) AS INTERMARK,CAST(R.MARKTOT AS FLOAT) AS MARKTOT,R.GRADE AS GRADE,CAST(R.GDPOINT AS INT) AS GDPOINT,R.SCALEDN_PERCENT,ISNULL(C.MAXMARKS_I,0) AS MAXMARKS_I,ISNULL(C.MAXMARKS_E,0) AS MAXMARKS_E,ISNULL(C.MINMARKS,0) AS MINMARKS,ISNULL(TOTAL_MARK,0) AS TOTAL_MARK,ISNULL(R.LOCKE,0)LOCKE ,  S1MARK  AS S1MARK,ISNULL(C.S1MAX,0) AS S1MAX,ISNULL(C.S1MIN,0) AS S1MIN,R.LOCKS1,R.S2MARK AS S2MARK,ISNULL(C.S2MAX,0) AS S2MAX,ISNULL(C.S2MIN,0) AS S2MIN,R.LOCKS2", "	CAST(R.S3MARK AS INT) AS S3MARK,ISNULL(C.S3MAX,0) AS S3MAX,ISNULL(C.S3MIN,0) AS S3MIN,R.LOCKS3,R.PREV_STATUS,R.MARKTOT,R.SECTIONNO", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1 AND ISNULL(R.PREV_STATUS,0)=0 ", orderby);

                // 	T.T1MARK,(C.S2MAX/2) T1MAX, 0 AS T1MIN,T.LOCKT1,T.T2MARK,(C.S2MAX/2) T2MAX, 0 AS T2MIN,T.LOCKT2,
                            
                 
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
                        gvStudent.Columns[2].Visible = false;
                        gvStudent.Columns[3].Visible = false;
                        gvStudent.Columns[4].Visible = false;
                        gvStudent.Columns[5].Visible = false;

                   //     gvStudent.Columns[6].Visible = false;
                        gvStudent.Columns[7].Visible = false;
                  //      gvStudent.Columns[8].Visible = false;
                  //      gvStudent.Columns[9].Visible = false;

                        colTA = string.Empty;
                        colCT1 = string.Empty;
                        colCT2 = string.Empty;
                        //gvStudent.DataSource = dsStudent;
                        //gvStudent.DataBind();
                        //gvStudent.Visible = true;
                        //pnlStudGrid.Visible = true;
                        //return;


                        DataTableReader dtrExams = dsExams.Tables[0].CreateDataReader();
                        while (dtrExams.Read())
                        {
                            //if ((dtrExams["FLDNAME"].ToString() == "S1") && (ddlExam.SelectedValue == "S1" || ddlExam.SelectedValue == "0"))
                            //{
                            //    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["S1MAX"]) > 0)
                            //    {
                            //        //gvStudent.Columns[2].HeaderText = "MINOR-1 <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["T1MAX"].ToString() + "]";
                            //        gvStudent.Columns[3].HeaderText = "INTERNAL <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["S1MAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["S1MIN"].ToString() + "]";
                            //        gvStudent.Columns[3].Visible = true;

                            //      //  gvStudent.Columns[6].Visible = false;
                            //        gvStudent.Columns[7].Visible = false;
                            //    //    gvStudent.Columns[8].Visible = false;
                            //        gvStudent.Columns[9].Visible = false;
                            //        colCT1 = "true";
                            //        th_pr = "1";
                            //    }

                            //}
                            ////if ((dtrExams["FLDNAME"].ToString() == "S1T2") && (ddlExam.SelectedValue == "S1T2" || ddlExam.SelectedValue == "0"))
                            ////{
                            ////    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["S1MAX"]) > 0)
                            ////    {
                            ////        gvStudent.Columns[4].HeaderText = "MINOR-2 <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["T2MAX"].ToString() + "]";
                            ////        gvStudent.Columns[5].HeaderText = "TOTAL INTERNAL MARKS <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["S1MAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["S1MIN"].ToString() + "]";
                            ////        gvStudent.Columns[4].Visible = true;
                            ////        gvStudent.Columns[5].Visible = true;
                            ////        gvStudent.Columns[8].Visible = false;
                            ////        gvStudent.Columns[9].Visible = false;
                            ////        gvStudent.Columns[10].Visible = false;
                            ////        gvStudent.Columns[11].Visible = false;
                            ////        colCT1 = "true";
                            ////        th_pr = "1";
                            ////    }

                            ////}
                            //if ((dtrExams["FLDNAME"].ToString() == "S2") && (ddlExam.SelectedValue == "S2" || ddlExam.SelectedValue == "0"))
                            //{
                            //    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["S2MAX"]) > 0)
                            //    {
                            //        gvStudent.Columns[2].HeaderText = "MID SEMESTER EXAMINATION <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["S2MAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["S2MIN"].ToString() + "]";
                            //        gvStudent.Columns[2].Visible = true;

                            //   //     gvStudent.Columns[6].Visible = false;
                            //        gvStudent.Columns[7].Visible = false;
                            //   //     gvStudent.Columns[8].Visible = false;
                            //        gvStudent.Columns[9].Visible = false;
                            //        colCT1 = "true";
                            //        th_pr = "1";
                            //    }

                            //}
                            if (((dtrExams["FLDNAME"].ToString() == "EXTERN") || (dtrExams["FLDNAME"].ToString() == "EXTERMARK"))&& (ddlExam.SelectedValue == "EXTERMARK" || ddlExam.SelectedValue == "0"))
                            {
                                if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["MAXMARKS_E"]) > 0)
                                {
                                    gvStudent.Columns[3].HeaderText = "INTERNAL <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["MAXMARKS_I"].ToString() + "]";
                                    //+" <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["MINMARKS_I"].ToString() + "]";
                                    ViewState["MAXMARKS_I"] = dsStudent.Tables[0].Rows[0]["MAXMARKS_I"].ToString();
                                    gvStudent.Columns[4].HeaderText = "END SEM MARKS <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["MAXMARKS_E"].ToString() + "]" + " <br>" + "[Min Pass : " + dsStudent.Tables[0].Rows[0]["MINMARKS"].ToString() + "]";
                                    gvStudent.Columns[6].HeaderText = "TOTAL MARKS <br>" + "(" + dsStudent.Tables[0].Rows[0]["TOTAL_MARK"].ToString() + ")";
                                    gvStudent.Columns[4].Visible = true;

                                    gvStudent.Columns[2].Visible = true;
                                    gvStudent.Columns[3].Visible = true;

                                    gvStudent.Columns[5].Visible = false;
                                //    gvStudent.Columns[6].Visible = true;
                                    gvStudent.Columns[7].Visible = true;
                              //      gvStudent.Columns[8].Visible = true;
                              //      gvStudent.Columns[9].Visible = true;
                                    th_pr = "1";

                                    hdfMaxCourseMarks.Value = dsStudent.Tables[0].Rows[0]["MAXMARKS_E"].ToString();
                                    hdfMaxCourseMarks_I.Value = dsStudent.Tables[0].Rows[0]["MAXMARKS_I"].ToString();   // ADDED ON 11042022 FOR LAW

                                    ViewState["islock"] = Convert.ToBoolean(dsStudent.Tables[0].Rows[0]["LOCKE"].ToString());


                                    //added by prafull on dt 10082022 for issue releted to save button

                                    for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                                    {
                                        if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCKE"]) == true)
                                        {
                                            lockcount++;
                                        }
                                    }
                                    if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(lockcount)) // Checking the Marks lock for All Students
                                    {
                                        btnLastSave.Visible = false;
                                        btnLock.Visible = false;
                                    }



                                    //commented by prafull on dt 10082022

                                    //if (Convert.ToBoolean(ViewState["islock"]) == true)
                                    //{
                                    //    btnLastSave.Visible = false;

                                    // }



                                }

                            }
                            //if ((dtrExams["FLDNAME"].ToString() == "S4") && (ddlExam.SelectedValue == "S4" || ddlExam.SelectedValue == "0"))
                            //{
                            //    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["S4MAX"]) > 0)
                            //    {
                            //        gvStudent.Columns[5].HeaderText = "END SEMESTER-PR EXAMINATION MARKS <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["S4MAX"].ToString() + "]" + " <br>" + "[Min : " + dsStudent.Tables[0].Rows[0]["S4MIN"].ToString() + "]";
                            //        gvStudent.Columns[5].Visible = true;

                            //        gvStudent.Columns[2].Visible = false;
                            //        gvStudent.Columns[3].Visible = false;
                            //        gvStudent.Columns[4].Visible = false;

                            //        //gvStudent.Columns[6].Visible = true;
                            //        gvStudent.Columns[7].Visible = true;
                            //     //   gvStudent.Columns[8].Visible = true;
                            //   //     gvStudent.Columns[9].Visible = true;
                            //        th_pr = "2";
                            //    }
                            //}
                        }

                        dtrExams.Close();
                        dtrExams.Dispose();

                        lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();
                        lblStudents.Visible = true;
                        //Bind the Student List
                        gvStudent.DataSource = dsStudent;
                        gvStudent.DataBind();
                        gvStudent.DataSource = dsStudent;
                        gvStudent.DataBind();

                        this.BindJS();
                        //this.BindJS1();
                        pnlMarkEntry.Visible = true;
                        pnlStudGrid.Visible = true;
                        pnlSelection.Visible = false;
                        btnSave.Enabled = true;
                        btnLock.Enabled = true;
                        btnSave.Visible = true;
                        btnLock.Visible = true;
                    }
                }

                //if (gvStudent.Columns[5].Visible == false &&
                //    gvStudent.Columns[4].Visible == false &&
                //    gvStudent.Columns[3].Visible == false)
                //{
                //    btnSave.Visible = false;
                //    btnLock.Visible = false;
                //}
                int TotalAllStudent = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "COUNT(R.IDNO)", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
                txtTotalAllStudent.Text = TotalAllStudent.ToString();

                int MarksTotal = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "SUM(ISNULL(R.MARKTOT,0))", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
                txtMarksTotal.Text = MarksTotal.ToString();

                double Average = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R", "SUM(ISNULL(R.MARKTOT,0))/COUNT(IDNO)", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
                txtAverage.Text = Average.ToString();

                //double SD = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R", "STDEV(MARKTOT)", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
                //double SDA = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R", " (STDEV(MARKTOT)/ COUNT(IDNO))", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
                //txtSd.Text = SD.ToString();
                //txtSda.Text = SDA.ToString();

                //double Sigma = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R", " sqrt(sum(((MARKTOT - " + Average + " ) * (MARKTOT - " + Average + " ) ))/COUNT(IDNO))", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
                //txtSigma.Text = Sigma.ToString();
                
         //   }

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
        SaveAndLock1(1);
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
        //string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
        //Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
        //if (Grade_SectionNo > 0)
        //{
        //    if (Convert.ToInt32(subid) == 1 || Convert.ToInt32(subid) == 8)
        //    {
        //        this.ShowReport_WithSection("MarksListReport", "rptMarksListForEndSemWithSection.rpt");
        //    }
        //    else
        //    {
        //        this.ShowReport_WithSection("MarksListReportPrac", "rptMarksListForEndSemPracWithSection.rpt");
        //    }
        //}
        //else
        //{
        //    if (Convert.ToInt32(subid) == 1 || Convert.ToInt32(subid) == 8)
        //    {
        //        this.ShowReport("MarksListReport", "rptMarksListForEndSem.rpt");
        //    }
        //    else
        //    {
        //        this.ShowReport("MarksListReportPrac", "rptMarksListForEndSemPrac.rpt");
        //    }
        //}

         this.ShowReport1("MarksListReport", "rptMarksList.rpt");

         
    }


    private void ShowReport_WithSection(string reportTitle, string rptFileName)
    {
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_SEMESTERNO=" + hdfSemester.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=0";//,@P_SUBID=" + Convert.ToInt32(subid.ToString()) + "    ",@P_SECTIONNO=" + hdfSection.Value + 
        //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_SEMESTERNO=" + hdfSemester.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + ",@P_SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + ",@P_SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + ",@P_PREV_STATUS=0";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=0";//,@P_SUBID=" + Convert.ToInt32(subid.ToString()) + "
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_SEMESTERNO=" + hdfSemester.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=0";//,@P_SUBID=" + Convert.ToInt32(subid.ToString()) + "
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
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

        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

    private void ShowGraphReportSection(string reportTitle, string rptFileName)
    {
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);
        int degreeNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "DEGREENO", "C.COURSENO=" + lblCourse.ToolTip + ""));
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_SESSIONO=" + ddlSession2.SelectedValue + ",@P_SUBID=" + Convert.ToInt32(subid.ToString()) + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SECTIONNO=" + Convert.ToInt32(hdfSection.Value) + ",@P_DEGREENO=" + degreeNo + ",@P_SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + "";

        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
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
        //this.ShowGraphReport("GraphWiseGradesReport", "rptGraphGradesWiseStudents.rpt");
        Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value) + "AND SEMESTERNO=" + Convert.ToInt16(hdfSemester.Value)));
        if (Grade_SectionNo > 0)
        {
            this.ShowGraphReportSection("GraphWiseGradesReport", "rptGraphGradesWiseStudentsSection.rpt");
        }
        else
        {
            this.ShowGraphReport("GraphWiseGradesReport", "rptGraphGradesWiseStudents.rpt");
        }
    }

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {

            try
            {

                //DataSet ds = objMarksEntry.GetEndExamMarksDataExcel(Convert.ToInt32(ddlSession2.SelectedValue) , Convert.ToInt32(lblCourse.ToolTip) , Convert.ToInt32(hdfSection.Value) ,Convert.ToInt32('0'),Convert.ToInt32(Session["userno"]));
                DataSet ds = objMarksEntry.GetEndExamMarksDataExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(hdfSection.Value), Convert.ToInt32(hdfSemester.Value), Convert.ToInt32('0'), Convert.ToInt32(Session["userno"]));
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
                    //this.ShowMessage("No information found based on given criteria.");
                    objCommon.DisplayMessage(this.UpdatePanel1, "No information found based on given criteria.", this.Page);
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

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_IDS IN (" + Session["college_nos"].ToString() + ") AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE='END SEM')", "SESSIONNO DESC");

            DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  COLLEGE_IDS IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE='END SEM')", "SESSIONNO DESC");
            //STARTED = 1 AND SHOW_STATUS =1 

            
            if (ds_CheckActivity.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "This activity may not be Started!!!, Please contact Admin", this.Page);
                pnlSelection.Visible = false;
                pnlMarkEntry.Visible = false;
                return;
            }
            else
            {
                this.ShowCourses();
            }
        }
        else 
        {
 
        }

       
    }

    protected void btnReport1_Click(object sender, EventArgs e)
    {
        //this.ShowReport("MarksListReport", "rptMarksList.rpt");
        this.ShowReport1("MarksListReport", "rptMarksListCt.rpt");
    }

    private void ShowReport1(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value;

        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_SEMESTERNO=" + hdfSemester.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=0";

        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnTAReport_Click(object sender, EventArgs e)
    {
        //this.ShowReportForTA("TAMarksListReport", "rptMarksListForTA.rpt");

        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);

        if (Convert.ToInt32(subid) == 1 || Convert.ToInt32(subid) == 8)
        {
            this.ShowReportForMID("TAMarksListReport", "rptMarksListForMID.rpt");
        }
        //else
        //{
        //    this.ShowReportForMID("TAPracMarksListReport", "rptMarksListForMIDPrac.rpt");
        //}
    }

    private void ShowReportForMID(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]);

        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnConsolidateReport_Click(object sender, EventArgs e)
    {
        this.ShowReport1("MarksListReport", "rptMarksList.rpt");
    }

    protected void rdoRollno_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.ShowStudents(Convert.ToInt16(ViewState["lnk"].ToString()), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.rdoRollno_CheckedChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rdoTotalmarks_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.ShowStudents(Convert.ToInt16(ViewState["lnk"].ToString()), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.MARKTOT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.rdoTotalmarks_CheckedChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rdoSection_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.ShowStudents(Convert.ToInt16(ViewState["lnk"].ToString()), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.SECTIONNO,R.REGNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntryAll.rdoSection_CheckedChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtMarks_TextChanged(object sender, EventArgs e)
    {

    }

    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)(sender);

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + "MarksEntryReport";
        url += "&path=~,Reports,Academic," + "rptMarksList.rpt";
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[0]) + ",@P_SECTIONNO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[3]) + ",@P_SEMESTERNO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[2]) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=0";

        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

}
