//=================================================================================
// PROJECT NAME  : RF-CAMPUS [NITGOA]                                                         
// MODULE NAME   : ACADEMIC - ALL MARK ENTRY                                           
// CREATION DATE : 24 - JAN - 2018                                                    
// CREATED BY    :                                               
// MODIFIED BY   : AKASH RASAL                                                      
// MODIFIED DESC : MODIFY & APPLY RELATIVE & ABSOLUTE GRADING SYSTEM 
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
    int Grade_SectionNo = 0;

    #region pageload
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

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT TOP 2 SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND C.COLLEGE_ID IN(" + colgno + ")", "SESSIONNO DESC");
                    // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");     //Commented dt on 28112022

                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");     //SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND

                    if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")//&& Session["dec"].ToString() != "1")
                    {
                        pnlSelection.Visible = true;
                        pnlMarkEntry.Visible = false;
                        divUFM.Visible = true;
                        divSession.Visible = true;
                        divCllgName.Visible = false;
                        divButton.Visible = false;
                        divNotes.Visible = false;
                        btnReject.Visible = false;

                        if (Session["usertype"].ToString() == "3")
                        {
                            btnFacultyLock.Visible = true;
                            btnFacultyLock.Enabled = true;
                            //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
                            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1", "SESSIONNO DESC");
                        }
                        else
                        {
                            btnFinalLock.Visible = true;
                            btnFinalLock.Enabled = true;
                            btnLastSave.Visible = false;
                            btnReject.Visible = false;
                            btnFacultyLock.Visible = false;
                            btnFacultyLock.Enabled = true;
                            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1", "SESSIONNO DESC");
                        }

                        string sp_proc = "PKG_ACD_GET_REVAL_COLLEGE_SESSION_NEW";
                        string sp_para = "@P_UA_NO,@P_PAGE_NO,@P_COLLEGE_ID";
                        string sp_cValues = "" + Convert.ToInt32(Session["userno"]) + "," + Request.QueryString["pageno"].ToString() + "," + Session["college_nos"].ToString() + "";

                        DataSet ds = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

                        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null && ds.Tables[0] != null)
                        {
                            ddlSession.DataSource = ds;
                            ddlSession.DataTextField = "SESSION_NAME";
                            ddlSession.DataValueField = "SESSIONNO";
                            ddlSession.DataSource = ds;
                            ddlSession.DataBind();
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.UpdatePanel1, "This activity may not be Started!!!, Please contact Admin", this.Page);
                            pnlSelection.Visible = false;
                            pnlMarkEntry.Visible = false;
                            return;
                        }
                    }
                    // this.ShowCourses();
                    else
                    {
                        trSemester.Visible = true;
                        trExam.Visible = true;

                        pnlSelection.Visible = true;
                        pnlMarkEntry.Visible = false;
                        divUFM.Visible = false;
                        divCllgName.Visible = true;
                        divNotes.Visible = true;
                    }

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    objCommon.DisplayMessage(this.UpdatePanel1, "You are not authorized to view this page!!", this.Page);
                    ddlSession.Items.Clear();
                    pnlSelection.Visible = true;
                    pnlMarkEntry.Visible = false;
                }

                // this.button4();
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion

    #region CheckActivity
    private void CheckActivity()
    {
        string colgno = Session["college_nos"].ToString();
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 AND COLLEGE_ID IN(" + colgno + ")");

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO) INNER JOIN ACD_SESSION_MASTER S ON (S.SESSIONNO=SA.SESSION_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE IN('END SEM') AND ISNULL(FLOCK,0)=1 AND COLLEGE_ID IN(" + colgno + ")");

        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG'");

        ActivityController objActController = new ActivityController();
        //DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlSelection.Visible = true;
                pnlMarkEntry.Visible = false;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                pnlSelection.Visible = true;
                pnlMarkEntry.Visible = false;
            }

        }
        else
        {
            objCommon.DisplayMessage(this.UpdatePanel1, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlSelection.Visible = true;
            pnlMarkEntry.Visible = false;
        }
        dtr.Close();
    }
    #endregion

    #region ShowCourses
    private void ShowCourses()
    {
        //DataSet ds = objMarksEntry.GetCourseForTeacher(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]));
        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT St ON (R.IDNO = St.IDNO) INNER JOIN ACD_SCHEME S ON (R.SCHEMENO = S.SCHEMENO)	INNER JOIN ACD_SEMESTER SM ON (R.SEMESTERNO = SM.SEMESTERNO)LEFT OUTER JOIN ACD_SECTION SN ON (SN.SECTIONNO = R.SECTIONNO)INNER JOIN ACD_COURSE C ON (R.COURSENO = C.COURSENO AND R.SCHEMENO = C.SCHEMENO)", "  DISTINCT R.COURSENO,	(CASE 	WHEN C.SUBID =7 THEN (REPLACE(R.CCODE,'-','') COLLATE DATABASE_DEFAULT + ' - '+ R.COURSENAME COLLATE DATABASE_DEFAULT + '  <SPAN STYLE= " + "\"" + " COLOR:GREEN;FONT-WEIGHT:BOLD" + "\"" + ">(SEM : ' + SM.SEMESTERNAME COLLATE DATABASE_DEFAULT + ' - '+ S.SCHEMENAME COLLATE DATABASE_DEFAULT + ' - <SPAN STYLE=" + "\"" + "COLOR:BLACK;" + "\"" + ">SECTION : '+ SN.SECTIONNAME COLLATE DATABASE_DEFAULT +'['+ DBO.FN_DESC('BATCH',R.BATCHNO) COLLATE DATABASE_DEFAULT +'] </SPAN> ) </SPAN>') 	ELSE (REPLACE(R.CCODE,'-','') COLLATE DATABASE_DEFAULT + ' - '+ R.COURSENAME COLLATE DATABASE_DEFAULT + '  <SPAN STYLE=" + "\"" + "COLOR:GREEN;FONT-WEIGHT:BOLD" + "\"" + ">(SEM : ' + SM.SEMESTERNAME COLLATE DATABASE_DEFAULT + ' - '+ S.SCHEMENAME COLLATE DATABASE_DEFAULT + ' - <SPAN STYLE=" + "\"" + "COLOR:BLACK;" + "\"" + ">SECTION : '+ SN.SECTIONNAME COLLATE DATABASE_DEFAULT +  ' </SPAN> ) </SPAN>') END) COURSENAME ", "R.SCHEMENO,R.SEMESTERNO,R.CCODE,ISNULL(R.SECTIONNO,0) AS SECTIONNO, (CASE WHEN C.SUBID = 7  THEN ISNULL(R.BATCHNO,0) ELSE  0 END)BATCHNO", "SESSIONNO =  " + ddlSession.SelectedValue + " AND ((UA_NO = " + Session["userno"] + " AND (VALUER_UA_NO IS NULL OR VALUER_UA_NO = 0)) OR (UA_NO_PRAC = " + Session["userno"] + " AND (VALUER_UA_NO_PRAC IS NULL OR VALUER_UA_NO_PRAC = 0)) or VALUER_UA_NO = " + Session["userno"] + " or  VALUER_UA_NO_PRAC = " + Session["userno"] + ") AND R.PREV_STATUS = 0 AND ((MAXMARKS_E >0 AND C.SUBID = 1 ) OR (S4MAX>0 AND C.SUBID <> 1 ))", "R.SEMESTERNO,R.CCODE,ISNULL(R.SECTIONNO,0)");

        //  DataSet ds = objMarksEntry.GetCourseForTeacherEndSem(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]));

        divCourselist.Visible = true;

        string sp_proc = "PKG_ACD_REVAL_MARK_ENTRY_FACULTY_CRESCENT";
        string sp_para = "@P_SESSIONNO,@P_UA_NO";
        string sp_cValues = "" + (Convert.ToInt16(ddlSession.SelectedItem.Value)) + "," + Convert.ToInt32(Session["userno"]) + "";
        DataSet ds = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

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
    #endregion

    #region facultylogin used
    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            //Show the Student List with Exams that are ON
            //=============================================

            LinkButton lnks = sender as LinkButton;
            if (!lnks.ToolTip.Equals(string.Empty))
            {
                string[] sec_batchs = lnks.CommandArgument.ToString().Split('+');
                hdfValueruano.Value = sec_batchs[3].ToString();
                if (hdfValueruano.Value == "" || hdfValueruano.Value == "0")
                {
                    objCommon.DisplayMessage(this.Page, "Valuer Not Alloted For this Course", this.Page);
                    return;
                }
                else
                {
                    ViewState["Valueruano"] = hdfValueruano.Value;
                }
            }
            DataSet dsExams = null;
            if (Session["usertype"].ToString() == "3")
            {
                dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));
            }
            else
            {
                dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdfValueruano.Value), int.Parse(Request.QueryString["pageno"].ToString()));
            }
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

            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(BRANCHNO,0)", "SCHEMENO=" + SchemeNo + ""));
            int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(DEGREENO,0)", "SCHEMENO=" + SchemeNo + ""));
            ViewState["Degree"] = Degreeno;
            ViewState["Branch"] = branchno;

            // Check Mark Enrty Activitity 
            //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND BRANCH LIKE '%" + branchno + "%' AND DEGREENO LIKE '%" + Degreeno + "%' AND COLLEGE_ID IN (" + College_id + ") AND SEMESTER LIKE '%" + semesterno + "%')", "SESSIONNO DESC");
            //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND BRANCH LIKE '%" + branchno + "%' AND DEGREENO LIKE '%" + Degreeno + "%' AND SEMESTER LIKE '%" + semesterno + "%')", "SESSIONNO DESC");
              
            #region Activity
            if (semesterno == "") { semesterno = "0"; }
            string collegename = ddlSession.SelectedItem.Text.Trim() == string.Empty ? "0" : ddlSession.SelectedItem.Text.Trim();  //College fetch from database through
            string sp_proc1 = "PKG_ACD_TO_CHECK_MARK_ENTRY_ACTIVITY";
            string sp_para1 = "@P_SESSIONNO,@P_UA_TYPE,@P_PAGE_LINK,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_COLLEGE_ID";
            string sp_cValues1 = "" + ddlSession.SelectedValue + "," + Session["usertype"].ToString() + "," + Request.QueryString["pageno"].ToString() + "," + Degreeno + "," + branchno + "," + semesterno + "," + collegename + "";
            DataSet ds_CheckActivity = objCommon.DynamicSPCall_Select(sp_proc1, sp_para1, sp_cValues1);
            if (ds_CheckActivity.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(this, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                return;
            }
            #endregion
             

            #region internal mark entry check

            //string isInternal = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ISNULL(COURSENO,0)COURSENO", "COURSENO =" + (lnk.ToolTip) + " and ISNULL(MAXMARKS_I,0)!=0.00")); 
            //int isinternal = Convert.ToInt32(isInternal == string.Empty ? "0" : isInternal);
            ////Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(COURSENO,0)COURSENO", "COURSENO =" + (lnk.ToolTip) + " and ISNULL(MAXMARKS_I,0)!=0.00"));

            //if (Convert.ToInt32(isinternal) > 0)//|| isInternal != "" || isInternal != string.Empty
            //{

            //     string sp_callValues = string.Empty;
            //    // CHECKS WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 
            //    //string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY";
            //    //string sp_parameters = "@P_COURSENO,@P_SECTIONNO,@P_SCHEMENO,@P_UA_NO";    
            //    // CHECKS WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 
            //    string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CRESCENT";
            //    string sp_parameters = "@P_COURSENO,@P_SECTIONNO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
            //    //string sp_callValues = "" + (lnk.ToolTip) + "," + Section + "," + SchemeNo + "," + (Session["userno"].ToString()) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";              
            //    //DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);


            //    if (Session["usertype"].ToString() == "3")
            //    {
            //        sp_callValues = "" + (lnk.ToolTip) + "," + Section + "," + SchemeNo + "," + (Session["userno"].ToString()) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";
            //    }
            //    else
            //    {
            //        sp_callValues = "" + (lnk.ToolTip) + "," + Section + "," + SchemeNo + "," + (hdfValueruano.Value) + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";
            //    }
            //    DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            //    if (dschk.Tables[0].Rows.Count == 0 && dschk.Tables != null)
            //    {
            //        //string islocked = dschk.Tables[0].Rows[0]["LOCK"]==string.Empty?"0":dschk.Tables[0].Rows[0]["LOCK"].ToString();

            //        //if (islocked == "0" || islocked == string.Empty || islocked == null)
            //        //{
            //        //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
            //        objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for !", this.Page);
            //        return;
            //        //}
            //    }

            //    if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
            //    {
            //        string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

            //        if (islocked == "0" || islocked == string.Empty || islocked == null)
            //        {
            //            //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
            //            objCommon.DisplayMessage(UpdatePanel1, "Kindly Check Either Internal Mark Entry is not Completed / Locked for this Course", this.Page);
            //            return;
            //        }
            //    } 

            //    // ENDS HERE WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 
            //}
            #endregion

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
                //lblCourses.Text = CcodeName + " - " + CourseName;

                hdfSchemeNo.Value = SchemeNo.ToString();
                hdfExamType.Value = ExamType.ToString();
                string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                hdfSection.Value = sec_batch[0].ToString();
                hdfSemester.Value = sec_batch[2].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";
                lblSession.Text = ddlSession.SelectedItem.Text;

                // lblCourse.Text = Convert.ToString(ViewState["Course"]);               //CourseName;
                lblCourse.Text = lnk.Text;

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

                #region not in used
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

                #endregion

                // CHECKS LOCK CONDIDTION FOR THE PARTICULAR COURSE
                this.ShowStudents(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
                //        return;
                int subid = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "DISTINCT SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                if (Session["usertype"].ToString() == "3")
                {
                    btnFinalLock.Visible = false;
                    btnFinalLock.Enabled = false;
                    btnReject.Visible = false;
                    btnFacultyLock.Visible = true;
                    btnFacultyLock.Enabled = true;
                    // Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                    //Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SUBID=" + subid + "AND SEMESTERNO=" + Convert.ToInt16(hdfSemester.Value)));
                }
                else
                {
                    //btnFinalLock.Visible = true;
                    //btnFinalLock.Enabled = true;
                    btnLastSave.Visible = false;
                    btnReject.Visible = true;
                    btnReject.Enabled = true;
                    btnFacultyLock.Visible = false;
                    // Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                    //Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SUBID=" + subid + "AND SEMESTERNO=" + Convert.ToInt16(hdfSemester.Value)));
                }
                //if (Grade_SectionNo > 0)
                //{
                //    // this.ShowGradesSection(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
                this.GradesBind(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
                //}
                //else
                //{
                //    //this.ShowGrades(Convert.ToInt16(lnk.ToolTip));
                //    objCommon.DisplayMessage(this.Page, "Regular Grade Allotment Record Not Found", this.Page);
                //    return;
                //}

                //if (subId == 1 || subId == 8)
                //{
                //    int lockNo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(LOCKS2)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                //    int countIdno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + hdfSchemeNo.Value + ""));
                //    if (lockNo < countIdno)
                //    {
                //        ShowMessage("Please First Lock Your Internal & Mid Sem marks !!!");
                //    }

                //}.
                if (Session["usertype"].ToString() == "3")
                {
                    btnFinalLock.Visible = false;
                    btnFinalLock.Enabled = false;
                    btnReject.Visible = false;
                    btnFacultyLock.Visible = true;
                    btnFacultyLock.Enabled = true;
                }
                else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
                {
                    //btnFinalLock.Visible = true;
                    //btnFinalLock.Enabled = true;
                    btnLastSave.Visible = false;
                    btnReject.Visible = true;
                    btnReject.Enabled = true;
                    btnFacultyLock.Visible = false;
                    btnFacultyLock.Enabled = true;
                }
                if (Convert.ToString(ViewState["MarkLock"]) == "1")
                {
                    //btnFinalLock.Enabled = false;
                    btnFacultyLock.Enabled = false;
                    btnLastSave.Visible = true;
                    btnLastSave.Enabled = false;
                }
                if (Session["usertype"].ToString() == "3")
                {
                    if (Convert.ToString(ViewState["MarkLock"]) == "1")
                    {
                        btnLastSave.Visible = true;
                        btnLastSave.Enabled = false;
                        // btnFacultyLock.Enabled = false;
                    }
                    else
                    {
                        btnLastSave.Visible = true;
                        btnFacultyLock.Visible = true;
                        btnLastSave.Enabled = true;
                        // btnFacultyLock.Enabled = true;
                    }
                }
                else
                {
                    btnLastSave.Visible = false;
                    btnLastSave.Enabled = false;

                }


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
    #endregion

    #region BindJS
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

                int subId = Convert.ToInt32(hdfSubid.Value); // Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

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
                int count = 0;
                if (Session["usertype"].ToString() == "3")
                {
                    count = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(1)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SUBID=" + Convert.ToInt32(subId) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SECTIONNO=" + hdfSection.Value + ""));
                }
                else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
                {
                    count = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(1)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SUBID=" + Convert.ToInt32(subId) + " AND UA_NO=" + Convert.ToInt32(hdfValueruano.Value) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SECTIONNO=" + hdfSection.Value + ""));
                }
                else
                {
                    count = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(1)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBID=" + Convert.ToInt32(subId) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ""));
                }
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
                    btnLock.Enabled = false;
                    btnLastSave.Enabled = false;
                    btnFinalLock.Enabled = false;
                }
                // COMMENTED BELOW LINES
                //txtESMarks.Attributes.Add("onblur", " validateMarkTH(" + txtESMarks.ClientID + "," + lblESMarks.Text + "," + lblESMinMarks.Text + "," + txtTotMarks.ClientID + "," + txtTAMarks.ClientID + "," + txtTotMarksAll.ClientID + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + hidTotMarksAll.ClientID + "," + hidTotPer.ClientID + "," + hidGrade.ClientID + "," + hidGradePoint.ClientID + "," + Scale + "," + totalmarks + ")");
                // ENDS HERE COMMENTED LINES 
                //txtESMarks.Attributes.Add("onblur", " validateMarkTH(" + txtESMarks.ClientID + "," + lblESMarks.Text + "," + lblESMinMarks.Text + "," + txtTotMarks.Text + "," + txtTAMarks.Text + "," + txtTotMarksAll.ClientID + "," + txtTotPer.ClientID + "," + txtGrade.ClientID + "," + txtGradeP.ClientID + "," + lblTotMarks.Text + "," + lblTotPer.Text + "," + lblGrade.Text + "," + lblGradePoint.Text + ")");

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
    #endregion

    #region btnSave_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //0 - means - unlock
        // SaveAndLock(0);
    }
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        //selection panel
        pnlSelection.Visible = true;
        pnlMarkEntry.Visible = false;
        txtTitle.Text = "";
    }

    #region btnLastSave_Click
    protected void btnLastSave_Click(object sender, EventArgs e)
    {
        //0 - means - unlock
        SaveAndLock1(0);
    }
    #endregion

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

    private void SaveAndLock1(int lock_status)
    {
        try
        {
            string exam = string.Empty; string que_out = string.Empty;
            CustomStatus cs = CustomStatus.Error;
            int courseno = 0;
            string ccode = string.Empty;
            if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
            {
                courseno = Convert.ToInt32(lblCourse.ToolTip);
                string[] course = lblCourse.Text.Split('-');
                //ccode = course[0].Trim();
                ccode = objCommon.LookUp("ACD_COURSE", "DISTINCT CCODE", "COURSENO=" + Convert.ToInt32(courseno) + "");
            }
            else
            {
                courseno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COURSENO", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ""));
                ccode = objCommon.LookUp("ACD_COURSE", "COURSENO", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "");
            }
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
                            FinalConversion += Convert.ToString(hidConversion.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidConversion.Value).Trim() + ",";
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

                        string idnos = studids.TrimEnd(',');
                        string stud_marks = marks.TrimEnd(',');
                        string grades = grade.TrimEnd(',');
                        string Gdpoint = Gpoint.TrimEnd(',');
                        string ConversionMarks = FinalConversion.TrimEnd(',');
                        string Scaledn_Percent = totPer.TrimEnd(',');

                        if (!string.IsNullOrEmpty(studids))
                        {
                            //  cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAll(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()));
                            // cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAllNew(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), FinalConversion);

                            if (Session["usertype"].ToString() == "3")
                            {
                                cs = (CustomStatus)objMarksEntry.InsertRevaluationMarkEntryCrescent(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(hdfSchemeNo.Value), Convert.ToInt32(hdfSemester.Value), Convert.ToInt32(ViewState["Degree"]), Convert.ToInt32(ViewState["Branch"]), idnos, stud_marks, lock_status, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["OrgId"]), grades, Gdpoint, ConversionMarks, Convert.ToInt16(hdfSection.Value), Scaledn_Percent);
                            }
                            else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
                            {
                                cs = (CustomStatus)objMarksEntry.InsertRevaluationMarkEntryCrescent(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(hdfSchemeNo.Value), Convert.ToInt32(hdfSemester.Value), Convert.ToInt32(ViewState["Degree"]), Convert.ToInt32(ViewState["Branch"]), idnos, stud_marks, lock_status, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["OrgId"]), grades, Gdpoint, ConversionMarks, Convert.ToInt16(hdfSection.Value), Scaledn_Percent);
                            }
                            else
                            {
                                cs = (CustomStatus)objMarksEntry.InsertRevaluationMarkEntryCrescent(Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(courseno), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["Degree"]), Convert.ToInt32(ViewState["branchno"]), idnos, stud_marks, lock_status, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["OrgId"]), grades, Gdpoint, ConversionMarks, Convert.ToInt16(hdfSection.Value), Scaledn_Percent);
                            }
                        }
                    }
                }
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    string subId = hdfSubid.Value;
                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Locked Successfully!!!", this.Page);
                    btnLastSave.Enabled = false;
                    btnLock.Enabled = false;
                    btnLock.Visible = false;
                    btnFinalLock.Enabled = false;
                }
                else
                {
                    objCommon.DisplayMessage(this.UpdatePanel1, "Marks Saved Successfully!!!", this.Page);
                }
            }
            else
            {
                //  ShowMessage("Error in Saving Marks!");
                objCommon.DisplayMessage(this.UpdatePanel1, "Error in Saving Marks!", this.Page);
            }
            this.ShowStudents(courseno, Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
            if (ViewState["islock"].Equals("TRUE"))
            {
                btnLock.Enabled = false;
                btnLastSave.Enabled = false;
                btnFinalLock.Enabled = false;
            }

            if (Session["usertype"].ToString() == "3")
            {
                //Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                //if (Grade_SectionNo > 0)
                //{
                this.ShowGradesSection(Convert.ToInt16(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
                //}
            }
            else
            {
                //Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                //if (Grade_SectionNo > 0)
                //{
                this.ShowGradesSection(Convert.ToInt16(ddlCourse.SelectedValue), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
                // }
            }

            if (ViewState["islock"].Equals("TRUE"))
            {
                btnLock.Enabled = false;
                btnLastSave.Enabled = false;
                btnLock.Visible = false;
                btnFinalLock.Enabled = false;
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

    #region not in used
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
                string subid1 = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + ddlCourse.SelectedValue);
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
                    else    //Added dt on 16022023 for revaluation grade range not editable 
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
            string schemetype = string.Empty;
            if (Session["usertype"].ToString() == "3")
            {
                schemetype = objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "SCHEMETYPE", "COURSENO=" + lblCourse.ToolTip);
            }
            else
            {
                schemetype = objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "SCHEMETYPE", "COURSENO=" + ddlCourse.SelectedValue);
            }

            ListViewDataItem item1 = lvGrades.Items[0];
            TextBox a1 = (TextBox)item1.FindControl("txtMax");
            a1.Enabled = false;


            if (schemetype == "1")              //06022023 changes == to != index error throwing 
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
                /*
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
               
                ListViewDataItem item2 = lvGrades.Items[6];
                TextBox a2 = (TextBox)item2.FindControl("txtMin");
                a2.Enabled = false;
                ListViewDataItem item3 = lvGrades.Items[7];
                TextBox a3 = (TextBox)item3.FindControl("txtMax");
                a3.Enabled = false;
                ListViewDataItem item4 = lvGrades.Items[7];
                TextBox a4 = (TextBox)item4.FindControl("txtMin");
                a4.Enabled = false;
                 * */
            }


            foreach (ListViewDataItem dataRow in lvGrades.Items)   //Added dt on 16022023 for revaluation grade range not editable 
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
        DataSet dsGrades = null;
        if (Session["usertype"].ToString() == "3")
        {
            dsGrades = objMarksEntry.GetAllGradesSection(Convert.ToInt16(ddlSession.SelectedItem.Value), subid, Courseno, Convert.ToInt16(Session["userno"].ToString()), degreeNo, SectionNo, semesterno);
        }
        else
        {
            dsGrades = objMarksEntry.GetAllGradesSection(Convert.ToInt16(ddlSession.SelectedItem.Value), subid, Courseno, Convert.ToInt16(hdfValueruano.Value), degreeNo, SectionNo, semesterno);
        }
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

            foreach (ListViewDataItem dataRow in lvGrades.Items) //Added dt on 16022023 for revaluation grade range not editable 
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
            string schemetype = string.Empty;
            if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
            {
                schemetype = objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "SCHEMETYPE", "COURSENO=" + lblCourse.ToolTip);
            }
            else
            {
                schemetype = objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "SCHEMETYPE", "COURSENO=" + ddlCourse.SelectedValue);
            }

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

            foreach (ListViewDataItem dataRow in lvGrades.Items) //Added dt on 16022023 for revaluation grade range not editable 
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
        else
        {
            lvGrades.DataSource = null;
            lvGrades.DataBind();
        }
    }
    #endregion

    private void ShowStudents(int courseNo, int sectionNo, int semesterNo, string orderby)
    {

        try
        {
            int lockcount = 0;
            int Approved = 0;
            DataSet dsExams = null;
            if (Session["usertype"].ToString() == "3")
            {
                dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));
                subid = Convert.ToInt16(objCommon.LookUp("ACD_SUBJECTTYPE", "ISNULL(SUBID,0)", " SUBID IN (SELECT SUBID FROM ACD_COURSE WHERE COURSENO =" + lblCourse.ToolTip + " )"));
            }
            else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
            {
                dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(hdfValueruano.Value), int.Parse(Request.QueryString["pageno"].ToString()));
                subid = Convert.ToInt16(objCommon.LookUp("ACD_SUBJECTTYPE", "ISNULL(SUBID,0)", " SUBID IN (SELECT SUBID FROM ACD_COURSE WHERE COURSENO =" + lblCourse.ToolTip + " )"));
            }
            else
            {
                subid = Convert.ToInt16(objCommon.LookUp("ACD_SUBJECTTYPE", "ISNULL(SUBID,0)", " SUBID IN (SELECT SUBID FROM ACD_COURSE WHERE COURSENO =" + ddlCourse.SelectedValue + " )"));
                dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));
            }
            int count = 0;
            DataSet dsStudent = null;
            string sp_proc = "PKG_ACD_REVAL_MARK_ENTRY_STUDENT_CRESCENT_NEW";
            string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_UA_TYPE,@P_SECTIONNO";
            string sp_cValues = string.Empty;
            if (Session["usertype"].ToString() == "3")
            {
                sp_cValues = "" + (Convert.ToInt16(ddlSession.SelectedItem.Value)) + "," + hdfSchemeNo.Value + "," + semesterNo + "," + courseNo + "," + Convert.ToInt32(Session["userno"]) + "," + Convert.ToString(Session["usertype"]) + "," + sectionNo + "";
            }
            else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
            {
                sp_cValues = "" + (Convert.ToInt16(ddlSession.SelectedItem.Value)) + "," + hdfSchemeNo.Value + "," + semesterNo + "," + courseNo + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToString(Session["usertype"]) + "," + sectionNo + "";
            }
            else
            {
                sp_cValues = "" + (Convert.ToInt16(ddlSession.SelectedItem.Value)) + "," + hdfSchemeNo.Value + "," + semesterNo + "," + courseNo + "," + Convert.ToInt32(Session["userno"]) + "," + Convert.ToString(Session["usertype"]) + "," + sectionNo + "";
            }
            dsStudent = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);


            //   trTitle.Visible = true;
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                int DEGREENO = Convert.ToInt16(dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString());
                Session["DEGREENO"] = DEGREENO;
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    //HIDE THE MARKS ENTRY COLUMNS  
                    gvStudent.Columns[2].Visible = false;
                    gvStudent.Columns[3].Visible = false;
                    gvStudent.Columns[4].Visible = false;
                    gvStudent.Columns[5].Visible = false;
                    gvStudent.Columns[7].Visible = false;
                    colTA = string.Empty;
                    colCT1 = string.Empty;
                    colCT2 = string.Empty;

                    DataTableReader dtrExams = dsExams.Tables[0].CreateDataReader();
                    while (dtrExams.Read())
                    {

                        if (((dtrExams["FLDNAME"].ToString() == "EXTERN") || (dtrExams["FLDNAME"].ToString() == "EXTERMARK")) && (ddlExam.SelectedValue == "EXTERMARK" || ddlExam.SelectedValue == "0"))
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

                                ViewState["islock"] = Convert.ToBoolean(dsStudent.Tables[0].Rows[0]["LOCKE"].ToString()); //        //Commented dt on 31032023


                                //added by prafull on dt 10082022 for issue releted to save button

                                for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCKE"]) == true)
                                    {
                                        lockcount++;
                                    }
                                }

                                for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dsStudent.Tables[0].Rows[i]["APPROVE_UA_NO_REV"]) == 1)
                                    {
                                        Approved++;
                                    }
                                }

                                ViewState["MarkLock"] = string.Empty;
                                if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(lockcount)) // Checking the Marks lock for All Students
                                {
                                    // btnLastSave.Visible = false;
                                    btnLock.Visible = false;
                                    btnLock.Enabled = false;
                                    btnLastSave.Enabled = false;
                                    //  btnFinalLock.Enabled = false;
                                    ViewState["MarkLock"] = "1";
                                }
                                else
                                {
                                    btnLastSave.Visible = true;    //Added dt on 10022023
                                    btnLock.Visible = true;       //Added dt on 10022023
                                    //Button2.Visible = true;
                                    //Button3.Visible = true;
                                    //Button4.Visible = true;
                                    btnSave.Enabled = true;
                                    btnLastSave.Enabled = true;
                                    // btnFinalLock.Enabled = true;

                                    ////To Check Mark is null or 0
                                    //foreach (GridViewRow gvRow in gvStudent.Rows)
                                    //{ 
                                    //    TextBox txtMarks = gvRow.FindControl("txtESMarks") as TextBox;
                                    //    HiddenField hidConversion = gvRow.FindControl("hdfConversion") as HiddenField;
                                    //    if (txtMarks.Text.Equals("902.00") || txtMarks.Text.Equals("902") || txtMarks.Text.Equals("903.00") || txtMarks.Text.Equals("903") || txtMarks.Text.Equals("904.00") || txtMarks.Text.Equals("904") || txtMarks.Text.Equals("905.00") || txtMarks.Text.Equals("905"))
                                    //    {  
                                    //    }
                                    //    else if (hidConversion.Value == string.Empty || hidConversion.Value == "0.00")
                                    //    {
                                    //        txtMarks.Text = string.Empty;
                                    //    }
                                    //} 
                                }

                                if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(Approved)) // Checking the Marks lock for All Students
                                {
                                    btnFinalLock.Enabled = false;
                                }
                                else
                                {
                                    btnFinalLock.Enabled = true;
                                }


                                //commented by prafull on dt 10082022

                                //if (Convert.ToBoolean(ViewState["islock"]) == true)
                                //{
                                //    btnLastSave.Visible = false; 
                                // } 
                            }

                        }
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
                    Button3.Visible = true;
                    Button4.Visible = true;
                    btnLastSave.Visible = true;
                    Button2.Visible = true;

                }
            }
            //int TotalAllStudent = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "COUNT(R.IDNO)", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + ddlCourse.SelectedValue + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " +  ViewState["Section"].ToString() + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));   //Commented dt on 06022023
            // int TotalAllStudent = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "COUNT(R.IDNO)", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + ddlCourse.SelectedValue + "AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + ViewState["Section"].ToString() + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1 AND R.SCHEMENO=" + hdfSchemeNo.Value));
            int TotalAllStudent = 0;
            if (Session["usertype"].ToString() == "3" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
            {
                TotalAllStudent = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "APP_TYPE LIKE '%REVAL%' AND ISNULL(REV_APPROVE_STAT,0)=1 AND SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + lblCourse.ToolTip + "AND SEMESTERNO=" + hdfSemester.Value + "AND (CANCEL=0 OR CANCEL IS NULL) AND SCHEMENO=" + hdfSchemeNo.Value));
            }
            else
            {
                TotalAllStudent = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + "AND SEMESTERNO=" + hdfSemester.Value + "AND (CANCEL=0 OR CANCEL IS NULL) AND SCHEMENO=" + hdfSchemeNo.Value));
                //TotalAllStudent = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "SESSIONNO = " + ddlSession.SelectedValue + " AND COURSENO = " + ddlCourse.SelectedValue + "AND SEMESTERNO=" + hdfSemester.Value + "AND (CANCEL=0 OR CANCEL IS NULL) AND SCHEMENO=" + hdfSchemeNo.Value));
            }
            txtTotalAllStudent.Text = TotalAllStudent.ToString();

            if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(lockcount)) // Checking the Marks lock for All Students //Added dt on 04052023
            {
                btnLastSave.Visible = false;
                btnLock.Visible = false;
                btnLock.Enabled = false;
                // btnFinalLock.Enabled = false;
            }
            else
            {
                btnLastSave.Visible = true;
                btnLock.Visible = true;
                btnSave.Enabled = true;
                btnLastSave.Enabled = true;
                // btnFinalLock.Enabled = true;
            }
            ViewState["Approved"] = string.Empty;
            if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(Approved)) // Checking the Approval for All Students
            {
                btnFinalLock.Enabled = false;
                ViewState["Approved"] = "1";
            }
            else
            {
                btnFinalLock.Enabled = true;
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
        int Markscount = 0;
        string sp_cValues = string.Empty;
        if (hdfSection.Value == "" || hdfSection.Value == "0")
        {
            hdfSection.Value = "0";
        }
        if (hdfSemester.Value == "" || hdfSemester.Value == "0")
        {
            hdfSemester.Value = "0";
        }
        if (hdfValueruano.Value == "" || hdfValueruano.Value == "0")
        {
            hdfValueruano.Value = "0";
        }
        if (Session["usertype"].ToString() == "3")
        {
            // Markscount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(hdfSchemeNo.Value) + " AND SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "AND ISNULL(REV_APPROVE_STAT,0)=1 AND FINAL_EXTERN_MARK IS NULL AND NEWMRKS IS NULL AND APP_TYPE LIKE '%REVAL%'"));
            sp_cValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt16(hdfSection.Value) + "," + "1" + ",0";
        }
        else
        {
            // Markscount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND ISNULL(REV_APPROVE_STAT,0)=1 AND ISNULL(LOCK,0)=0 AND APP_TYPE LIKE '%REVAL%'"));
            sp_cValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt16(hdfSection.Value) + "," + "2" + ",0";
        }

        string sp_proc = "PKG_ACD_CHECK_REVAL_MARK_STATUS";
        string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_SECTIONNO,@P_STATUS";

        DataSet ds = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);
        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null && ds.Tables[0] != null)
        {
            Markscount = Convert.ToInt32(ds.Tables[0].Rows[0]["CNT_ID"].ToString());
        }

        if (Markscount > 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Save the Mark Entry first.", this.Page);
            return;
        }
        else
        {
            //1 - means lock marks
            SaveAndLock1(1);
        }
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

    #region Reports
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
    #endregion

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillCourseList();
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCourseList();
    }


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
            //DataSet ds = objMarksEntry.GetCourses_By_ON_EXAMS_MarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), ddlExam.SelectedValue, uano);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    lvCourse.DataSource = ds.Tables[0];
            //    lvCourse.DataBind();

            //    ViewState["Section"] = ds.Tables[0].Rows[0]["SECTIONNO"].ToString();        //Added dt on 08122022

            //}
            //else
            //{
            //    lvCourse.DataSource = null;
            //    lvCourse.DataBind();
            //}
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
            objCommon.FillDropDownList(ddlExam, "ACTIVITY_MASTER A, SESSION_ACTIVITY S,ACD_EXAM_NAME E", "DISTINCT E.FLDNAME", "E.EXAMNAME", "A.ACTIVITY_NO = S.ACTIVITY_NO AND A.EXAMNO = E.EXAMNO  AND GETDATE() >= S.START_DATE AND GETDATE() <= S.END_DATE AND S.STARTED = 1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK like '%" + int.Parse(Request.QueryString["pageno"].ToString()) + "%' AND S.SESSION_NO = " + ddlSession.SelectedValue + " AND E.EXAMNAME <> '' AND E.STATUS = 'N'", "FLDNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Examination_UnlockMarkEntry.FillExams --> " + ex.Message + " " + ex.StackTrace);
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
            // DataSet ds = objMarksEntry.GetEndExamMarksDataExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(hdfSection.Value), Convert.ToInt32(hdfSemester.Value), Convert.ToInt32('0'), Convert.ToInt32(Session["userno"]));
            string sp_proc = string.Empty; string sp_para = string.Empty; string sp_cValues = string.Empty;
            DataSet ds = null;
            int valuerno = 0;
            if (Session["usertype"].ToString() == "3")
            {
                valuerno = Convert.ToInt32(Session["userno"]);
            }
            else
            {
                valuerno = Convert.ToInt32(hdfValueruano.Value);
            }

            sp_proc = "PKG_ACD_REVAL_MARK_ENTRY_EXCEL_NEW";
            sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_SECTIONNO";
            sp_cValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + valuerno + "," + Convert.ToString(hdfSection.Value) + "";
            ds = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + ddlDegree.SelectedValue);
                //this.ShowReportExcel("xls",dcrReport, reportTitle, rptFileName);
                GridView GVDayWiseAtt = new GridView();
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=RevalMarkEntryReport.xls";
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
            DataSet ds_CheckActivity = null;
            string collegename = string.Empty;
            if (ddlSession.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() == "3")
                {
                    ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  COLLEGE_IDS IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE like '%REVAL%')", "SESSIONNO DESC");
                    //ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE like '%REVAL%' AND SA.STARTED = 1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_IDS LIKE '%" + college_id + "%') ", "SESSIONNO DESC");

                    if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "This activity may not be Started!!!, Please contact Admin", this.Page);
                        pnlSelection.Visible = false;
                        pnlMarkEntry.Visible = false;
                        btnReject.Visible = false;
                        return;
                    }
                    else
                    {
                        divCourselist.Visible = true;
                        this.ShowCourses();
                        btnFinalLock.Visible = false;
                        btnFinalLock.Enabled = false;
                        btnReject.Visible = false;
                    }
                }
                else if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7")
                {
                    ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE like '%REVAL%')", "SESSIONNO DESC");
                    // ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE like '%REVAL%' AND SA.STARTED = 1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_IDS LIKE '%" + college_id + "%') ", "SESSIONNO DESC");

                    if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "This activity may not be Started!!!, Please contact Admin", this.Page);
                        pnlSelection.Visible = false;
                        pnlMarkEntry.Visible = false;
                        return;
                    }
                    else
                    {
                        divCourselist.Visible = true;
                        this.ShowCourses();
                        btnFinalLock.Visible = true;
                        btnFinalLock.Enabled = true;
                        btnLastSave.Visible = false;
                        btnReject.Visible = true;
                        btnReject.Enabled = true;
                    }
                }
                else
                {
                    ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE like '%REVAL%')", "SESSIONNO DESC");
                    // ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE like '%REVAL%' AND SA.STARTED = 1 AND SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_IDS LIKE '%" + college_id + "%') ", "SESSIONNO DESC");

                    if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "This activity may not be Started!!!, Please contact Admin", this.Page);
                        pnlSelection.Visible = false;
                        pnlMarkEntry.Visible = false;
                        divCourselist.Visible = false;
                        return;
                    }
                    else
                    {
                        divCourselist.Visible = false;
                        objCommon.FillDropDownList(ddlSemester, "ACD_REVAL_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
                        // this.ShowCourses();   
                    }
                }
            }
        }
        else
        {
            divCourselist.Visible = false;
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

    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnks = sender as LinkButton;
            // LinkButton lnks = sender as LinkButton;
            string semesterno = string.Empty;
            if (!lnks.ToolTip.Equals(string.Empty))
            {
                string[] sec_batch = lnks.CommandArgument.ToString().Split('+');
                hdfSection.Value = sec_batch[0].ToString();
                hdfSemester.Value = sec_batch[2].ToString();
                semesterno = sec_batch[2].ToString();
                lblCourse.ToolTip = lnks.ToolTip;
                hdfValueruano.Value = sec_batch[3].ToString();
                if (hdfValueruano.Value == string.Empty || hdfValueruano.Value == "")
                {
                    hdfValueruano.Value = "0";
                }
                if (hdfSection.Value == string.Empty || hdfSection.Value == "")
                {
                    hdfSection.Value = "0";
                }

                int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lnks.ToolTip) + ""));
                hdfSchemeNo.Value = SchemeNo.ToString();


                //string[] session = ddlSession.SelectedItem.Text.Trim().Split('-');
                //string college = session[1].ToString();
                //string college_id = objCommon.LookUp("ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME LIKE '%" + college.Trim() + "%'");
                //if (college_id == "0" || college_id == "")
                //{
                //    college_id = "0";
                //}

                ViewState["college_id"] = "11";

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + "Reval Mark Entry Report";
                url += "&path=~,Reports,Academic," + "RevalMarkEntryReport_Crescent.rpt";
                if (Session["usertype"].ToString() == "3")
                {
                    url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(ViewState["college_id"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(hdfSchemeNo.Value) + ",@P_UA_NO=" + Convert.ToString(Session["userno"]) + ",@P_UA_TYPE=" + Convert.ToString(Session["usertype"]) + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + "";
                }
                else
                {
                    url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(ViewState["college_id"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(hdfSchemeNo.Value) + ",@P_UA_NO=" + Convert.ToString(hdfValueruano.Value) + ",@P_UA_TYPE=" + Convert.ToString(Session["usertype"]) + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + "";
                }
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[0]) + ",@P_SECTIONNO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[3]) + ",@P_SEMESTERNO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[2]) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=0";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
            }
        }
        catch
        {
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
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

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S WITH (NOLOCK) INNER JOIN ACD_REVAL_RESULT R ON (R.SESSIONNO=S.SESSIONNO)", "DISTINCT S.SESSIONNO", "SESSION_PNAME", "S.SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND S.COLLEGE_ID=" + ViewState["college_id"].ToString(), "S.SESSIONNO desc");
            }
        }

        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
    }

    #region Notinused
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //Show the Student List with Exams that are ON
            //=============================================
            ViewState["Degree"] = string.Empty; ViewState["branch"] = string.Empty;
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


            //int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lnk.ToolTip) + ""));
            //string[] sec_batch_sem = lnk.CommandArgument.ToString().Split('+');
            //string Section = sec_batch_sem[0].ToString();
            //string semesterno = sec_batch_sem[2].ToString();
            string College_id = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "SESSIONNO=" + ddlSession.SelectedValue);

            int branchno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + ViewState["schemeno"].ToString() + ""));
            int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + ViewState["schemeno"].ToString() + ""));

            ViewState["Degree"] = Degreeno;
            ViewState["branch"] = branchno;

            if (Session["usertype"].ToString() == "3")
            {
                // Check Mark Enrty Activitity 
                DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND BRANCH LIKE '%" + branchno + "%' AND DEGREENO LIKE '%" + Degreeno + "%' AND COLLEGE_ID IN (" + College_id + ") AND SEMESTER LIKE '%" + ddlSemester.SelectedValue + "%')", "SESSIONNO DESC");

                if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                {
                    objCommon.DisplayMessage(this, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                    return;
                }
            }
            //string isInternal = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ISNULL(COURSENO,0)COURSENO", "COURSENO =" + (lnk.ToolTip) + " and ISNULL(MAXMARKS_I,0)!=0.00"));
            string isInternal = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ISNULL(COURSENO,0)COURSENO", "COURSENO =" + ddlCourse.SelectedValue + " and ISNULL(MAXMARKS_I,0)!=0.00"));

            int isinternal = Convert.ToInt32(isInternal == string.Empty ? "0" : isInternal);

            #region Notinused
            //Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(COURSENO,0)COURSENO", "COURSENO =" + (lnk.ToolTip) + " and ISNULL(MAXMARKS_I,0)!=0.00"));

            //if (Convert.ToInt32(isinternal) > 0)//|| isInternal != "" || isInternal != string.Empty
            //{

            //    // CHECKS WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 
            //    //string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY";
            //    //string sp_parameters = "@P_COURSENO,@P_SECTIONNO,@P_SCHEMENO,@P_UA_NO";
            //    //string sp_callValues = "" + ddlCourse.SelectedValue + "," + 0 + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "";
            //    //DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            //    //if (dschk.Tables[0].Rows.Count == 0 && dschk.Tables != null)
            //    //{
            //    //    //string islocked = dschk.Tables[0].Rows[0]["LOCK"]==string.Empty?"0":dschk.Tables[0].Rows[0]["LOCK"].ToString();

            //    //    //if (islocked == "0" || islocked == string.Empty || islocked == null)
            //    //    //{
            //    //    //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
            //    //    objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for !", this.Page);
            //    //   // return;
            //    //    //}
            //    //}


            //    //if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
            //    //{
            //    //    string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

            //    //    if (islocked == "0" || islocked == string.Empty || islocked == null)
            //    //    {
            //    //        //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
            //    //        objCommon.DisplayMessage(UpdatePanel1, "Kindly Check Either Internal Mark Entry is not Completed / Locked for this Course", this.Page);
            //    //        return;
            //    //    }
            //    //}

            //    // ENDS HERE WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 
            //}
            #endregion

            if (!ddlCourse.SelectedValue.Equals(string.Empty))
            {
                ViewState["Section"] = objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_REVAL_RESULT T ON (T.IDNO=R.IDNO AND T.SESSIONNO=R.SESSIONNO AND T.COURSENO=R.COURSENO AND T.SEMESTERNO=R.SEMESTERNO)", "DISTINCT ISNULL(SECTIONNO,0)", "ISNULL(R.CANCEL,0)=0 AND R.SESSIONNO=" + ddlSession.SelectedValue + "AND R.SCHEMENO=" + ViewState["schemeno"].ToString() + "AND R.SEMESTERNO=" + ddlSemester.SelectedValue + "AND R.COURSENO=" + ddlCourse.SelectedValue + "AND VALUER_UA_NO=" + Convert.ToString(ViewState["valuerno"]) + " AND APP_TYPE LIKE '%REVAL%' AND ISNULL(REV_APPROVE_STAT,0)=1");   //IN FUTURE UA_NO CONDITION REQUIRED 
                ViewState["Reg_uno"] = objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_REVAL_RESULT T ON (T.IDNO=R.IDNO AND T.SESSIONNO=R.SESSIONNO AND T.COURSENO=R.COURSENO AND T.SEMESTERNO=R.SEMESTERNO)", "DISTINCT CASE WHEN ISNULL(UA_NO_PRAC,0) =0 THEN UA_NO ELSE UA_NO_PRAC END AS FACULTY", "ISNULL(R.CANCEL,0)=0 AND R.SESSIONNO=" + ddlSession.SelectedValue + "AND R.SCHEMENO=" + ViewState["schemeno"].ToString() + "AND R.SEMESTERNO=" + ddlSemester.SelectedValue + "AND R.COURSENO=" + ddlCourse.SelectedValue + "AND APP_TYPE LIKE '%REVAL%' AND ISNULL(REV_APPROVE_STAT,0)=1");   //IN FUTURE UA_NO CONDITION REQUIRED 
                ViewState["valuerno"] = objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_REVAL_RESULT T ON (T.IDNO=R.IDNO AND T.SESSIONNO=R.SESSIONNO AND T.COURSENO=R.COURSENO AND T.SEMESTERNO=R.SEMESTERNO)", "DISTINCT ISNULL(VALUER_UA_NO,0)", "ISNULL(R.CANCEL,0)=0 AND R.SESSIONNO=" + ddlSession.SelectedValue + "AND R.SCHEMENO=" + ViewState["schemeno"].ToString() + "AND R.SEMESTERNO=" + ddlSemester.SelectedValue + "AND R.COURSENO=" + ddlCourse.SelectedValue + "AND APP_TYPE LIKE '%REVAL%' AND ISNULL(REV_APPROVE_STAT,0)=1");   //IN FUTURE UA_NO CONDITION REQUIRED 

                ViewState["lnk"] = ddlCourse.SelectedValue;
                // sectionno = Convert.ToInt16(hdfSection.Value);
                int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));
                String SchemeName = objCommon.LookUp("ACD_SCHEME", "upper(SCHEMENAME)", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "");
                String CcodeName = objCommon.LookUp("ACD_COURSE", "upper(CCODE)", "COURSENO=" + ddlCourse.SelectedValue + "");
                String CourseName = objCommon.LookUp("ACD_COURSE", "upper(COURSE_NAME)", "COURSENO=" + ddlCourse.SelectedValue + "");
                lblScheme.Text = SchemeName.ToString();
                lblCourses.Text = CcodeName + " - " + CourseName;

                hdfSchemeNo.Value = ViewState["schemeno"].ToString();
                hdfExamType.Value = ExamType.ToString();
                // string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                hdfSection.Value = ViewState["Section"].ToString();                //sec_batch[0].ToString();
                hdfSemester.Value = ddlSemester.SelectedValue;            //sec_batch[2].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                //hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";
                lblSession.Text = ddlSession.SelectedItem.Text;
                //lblCourse.Text = CourseName;
                int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ""));
                hdfSubid.Value = subId.ToString();
                string sp_proc = "PKG_ACD_GET_RULE_FOR_ENDSEM_MARK_ENTRY";
                string sp_para = "@P_COURSENO,@P_SCHEMENO,@P_SESSIONNO";
                string sp_cValues = "" + ddlCourse.SelectedValue + "," + ViewState["schemeno"].ToString() + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";
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

                // CHECKS LOCK CONDIDTION FOR THE PARTICULAR COURSE
                this.ShowStudents(Convert.ToInt16(ddlCourse.SelectedValue), Convert.ToInt16(ViewState["Section"]), Convert.ToInt16(ddlSemester.SelectedValue), "R.PREV_STATUS,R.REGNO");
                //        return;
                //if (Session["usertype"].ToString() == "3")
                //{
                //    Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + ddlCourse.SelectedValue + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                //}
                //else
                //{
                //    Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + ddlCourse.SelectedValue + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
                //}
                //if (Grade_SectionNo > 0)
                //{
                this.ShowGradesSection(Convert.ToInt16(ddlCourse.SelectedValue), Convert.ToInt16(ViewState["Section"]), Convert.ToInt16(ddlSemester.SelectedValue));
                //}
                //else
                //{
                //    //this.ShowGrades(Convert.ToInt16(ddlCourse.SelectedValue));  //For Revaluation only ACD_GRADE_POINT table used as per open ticket 40643
                //    objCommon.DisplayMessage(this.Page, "Record Not Found, Please Contact Admin", this.Page);
                //}
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "CallButton();", true);


                btnSave.Enabled = true;
                btnLock.Enabled = true;
                btnSave.Visible = true;
                btnLock.Visible = true;
                Button3.Visible = true;
                Button4.Visible = true;
                btnLastSave.Visible = true;
                Button2.Visible = true;
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
    #endregion

    protected void ddlSemester_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "3")
        {
            // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON (C.COURSENO = SR.COURSENO) INNER JOIN ACD_STUDENT_RESULT R ON (R.SESSIONNO=SR.SESSIONNO AND R.COURSENO=SR.COURSENO AND R.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND R.UA_NO=" + Session["userno"].ToString() + "OR R.UA_NO_PRAC=" + Session["userno"].ToString(), "COURSE_NAME");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON (C.COURSENO = SR.COURSENO) INNER JOIN ACD_STUDENT_RESULT R ON (R.SESSIONNO=SR.SESSIONNO AND R.COURSENO=SR.COURSENO AND R.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND R.VALUER_UA_NO=" + Session["userno"].ToString(), "COURSE_NAME");
            ddlCourse.Focus();
        }
        else
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"].ToString() + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            ddlCourse.Focus();
        }
    }
    protected void btnRevalMarkRpt_Click(object sender, EventArgs e)
    {
        this.ShowMarkEntryReport("Reval Mark Entry Report", "RevalMarkEntryReport_Crescent.rpt");
    }
    private void ShowMarkEntryReport(string reportTitle, string rptFileName)
    {
        try
        {
            ViewState["college_id"] = "11";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Session["usertype"].ToString() == "3")
            {
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(ViewState["college_id"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(hdfSchemeNo.Value) + ",@P_UA_NO=" + Convert.ToString(Session["userno"]) + ",@P_UA_TYPE=" + Convert.ToString(Session["usertype"]) + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + "";
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(ViewState["college_id"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + ",@P_COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(hdfSchemeNo.Value) + ",@P_UA_NO=" + Convert.ToString(hdfValueruano.Value) + ",@P_UA_TYPE=" + Convert.ToString(Session["usertype"]) + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + "";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
        }
    }

    private void CollegeSchemeMappFacultywise()
    {
        string sp_procedure = "PKG_ACD_GET_COLLEGE_DETAILS_FACULTYWISE";
        string sp_parameters = "@P_UANO";

        string sp_callValues = "" + Convert.ToInt32(Session["userno"]) + "";
        DataSet dsCERT = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
        ddlClgname.DataSource = dsCERT;
        ddlClgname.DataTextField = "COL_SCHEME_NAME";
        ddlClgname.DataValueField = "COSCHNO";
        ddlClgname.DataBind();
        dsCERT.Dispose();
    }
    protected void btnFinalLock_Click(object sender, EventArgs e)
    {
        int Markscount = 0;
        string sp_cValues = string.Empty;
        if (hdfSection.Value == "" || hdfSection.Value == "0")
        {
            hdfSection.Value = "0";
        }
        if (hdfSemester.Value == "" || hdfSemester.Value == "0")
        {
            hdfSemester.Value = "0";
        }
        if (hdfValueruano.Value == "" || hdfValueruano.Value == "0")
        {
            hdfValueruano.Value = "0";
        }
        if (Session["usertype"].ToString() == "3")   //|| Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "7"
        {
            // Markscount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(hdfSchemeNo.Value) + " AND SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + " AND COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "AND ISNULL(REV_APPROVE_STAT,0)=1 AND FINAL_EXTERN_MARK IS NULL AND NEWMRKS IS NULL AND APP_TYPE LIKE '%REVAL%'"));
            sp_cValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt16(hdfSection.Value) + "," + "1" + ",0";
        }
        else
        {
            // Markscount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND ISNULL(REV_APPROVE_STAT,0)=1 AND ISNULL(LOCK,0)=0 AND APP_TYPE LIKE '%REVAL%'"));
            sp_cValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt16(hdfSection.Value) + "," + "2" + ",0";
        }
        string sp_proc = "PKG_ACD_CHECK_REVAL_MARK_STATUS";
        string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_SECTIONNO,@P_STATUS";

        DataSet ds = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);
        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null && ds.Tables[0] != null)
        {
            Markscount = Convert.ToInt32(ds.Tables[0].Rows[0]["CNT_ID"].ToString());
        }

        if (Markscount > 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Save the Mark Entry first.", this.Page);
            return;
        }
        else
        {
            //1 - means lock marks
            SaveAndLock1(1);
        }

    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        string SP_Name = "PKG_REVAL_MARKENTRY_STATUS_COE";
        string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_ORGID,@P_SUBMITTEDBY,@P_IPADDRESS,@P_OUT";
        string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt32(Session["OrgId"]) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + Convert.ToString(ViewState["ipAddress"]) + ",0";
        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
        if (que_out != "0")
        {
            btnFacultyLock.Visible = true;
            btnFacultyLock.Enabled = true;
            btnLastSave.Visible = false;
            objCommon.DisplayMessage(this.UpdatePanel1, "Reval Mark Entry Rejected Sucessfully..", this.Page);
        }
        else
        {
            btnFacultyLock.Visible = false;
            btnFacultyLock.Enabled = false;
            btnLastSave.Visible = false;
            objCommon.DisplayMessage(this.UpdatePanel1, "Server Error", this.Page);
        }
        this.ShowStudents(Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
        if (!ViewState["islock"].Equals("TRUE"))
        {
            btnLock.Enabled = false;
            btnLastSave.Enabled = false;
            btnLock.Visible = false;
            btnFacultyLock.Enabled = false;
            btnLastSave.Visible = false;
            btnFacultyLock.Enabled = false;
            btnFacultyLock.Visible = false;
        }
        //Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Convert.ToInt32(hdfValueruano.Value) + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
        //if (Grade_SectionNo > 0)
        //{
        this.GradesBind(Convert.ToInt16(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
        //}
        if (!ViewState["islock"].Equals("TRUE"))
        {
            btnLock.Enabled = false;
            btnLastSave.Enabled = false;
            btnLock.Visible = false;
            btnFacultyLock.Enabled = false;
            btnLastSave.Visible = false;
            btnFacultyLock.Visible = false;
            btnFacultyLock.Enabled = true;
        }
    }
    protected void btnFacultyLock_Click(object sender, EventArgs e)
    {
        int Markscount = 0;
        string sp_cValues = string.Empty;
        if (hdfSection.Value == "" || hdfSection.Value == "0")
        {
            hdfSection.Value = "0";
        }
        if (hdfSemester.Value == "" || hdfSemester.Value == "0")
        {
            hdfSemester.Value = "0";
        }
        if (hdfValueruano.Value == "" || hdfValueruano.Value == "0")
        {
            hdfValueruano.Value = "0";
        }
        if (Session["usertype"].ToString() == "3")
        {
            //Markscount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT R INNER JOIN ACD_STUDENT_RESULT S ON (R.IDNO=S.IDNO AND R.SESSIONNO=S.SESSIONNO AND R.COURSENO=S.COURSENO AND R.SEMESTERNO=S.SEMESTERNO)", "COUNT(R.IDNO)", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND R.SCHEMENO=" + Convert.ToInt32(hdfSchemeNo.Value) + " AND R.SEMESTERNO=" + Convert.ToInt32(hdfSemester.Value) + " AND R.COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "AND ISNULL(REV_APPROVE_STAT,0)=1 AND FINAL_EXTERN_MARK IS NULL AND NEWMRKS IS NULL AND APP_TYPE LIKE '%REVAL%'"));
            sp_cValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt16(hdfSection.Value) + "," + "1" + ",0";
        }
        else
        {
            //Markscount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT R INNER JOIN ACD_STUDENT_RESULT S ON (R.IDNO=S.IDNO AND R.SESSIONNO=S.SESSIONNO AND R.COURSENO=S.COURSENO AND R.SEMESTERNO=S.SEMESTERNO)", "COUNT(R.IDNO)", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND R.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND R.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND R.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND ISNULL(REV_APPROVE_STAT,0)=1 AND ISNULL(LOCK,0)=0 AND APP_TYPE LIKE '%REVAL%'"));
            sp_cValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt16(hdfSection.Value) + "," + "2" + ",0";
        }
        string sp_proc = "PKG_ACD_CHECK_REVAL_MARK_STATUS";
        string sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_SECTIONNO,@P_STATUS";

        DataSet ds = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);
        if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables != null && ds.Tables[0] != null)
        {
            Markscount = Convert.ToInt32(ds.Tables[0].Rows[0]["CNT_ID"].ToString());
        }

        if (Markscount > 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Save the Mark Entry first.", this.Page);
            return;
        }
        else
        {
            string SP_Name = "PKG_REVAL_MARKENTRY_STATUS_FACULTY";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_UA_NO,@P_ORGID,@P_SUBMITTEDBY,@P_IPADDRESS,@P_OUT";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(hdfSchemeNo.Value) + "," + Convert.ToInt32(hdfSemester.Value) + "," + Convert.ToInt32(lblCourse.ToolTip) + "," + Convert.ToInt32(hdfValueruano.Value) + "," + Convert.ToInt32(Session["OrgId"]) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + Convert.ToString(ViewState["ipAddress"]) + ",0";
            string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            if (que_out != "0")
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Marks Locked Successfully!!!", this.Page);
                btnLastSave.Enabled = false;
                btnLock.Enabled = false;
                btnLock.Visible = false;
                btnFacultyLock.Enabled = false;
                btnLastSave.Visible = false;
                objCommon.DisplayMessage(this.UpdatePanel1, "Reval Mark Entry Lock Sucessfully..", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Server Error", this.Page);
            }
            this.ShowStudents(Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
            if (ViewState["islock"].Equals("TRUE"))
            {
                btnLock.Enabled = false;
                btnLastSave.Enabled = false;
                btnFacultyLock.Enabled = false;
                btnLastSave.Visible = false;
            }
            //Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] + "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
            //if (Grade_SectionNo > 0)
            //{
            this.GradesBind(Convert.ToInt16(lblCourse.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
            // }
            if (ViewState["islock"].Equals("TRUE"))
            {
                btnLock.Enabled = false;
                btnLastSave.Enabled = false;
                btnLock.Visible = false;
                btnFacultyLock.Enabled = false;
                btnLastSave.Visible = false;
                btnFacultyLock.Visible = true;
            }
        }
    }
    private void GradesBind(int Courseno, int SectionNo, int semesterno)
    {
        int subid = Convert.ToInt16(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Courseno + ""));
        int degreeNo = Convert.ToInt16(objCommon.LookUp("ACD_COURSE C INNER JOIN ACD_SCHEME S ON (C.SCHEMENO = S.SCHEMENO) ", "DEGREENO", "C.COURSENO=" + Courseno + ""));
        string sp_callValues = string.Empty;
        if (Session["usertype"].ToString() == "3")
        {
            sp_callValues = "" + Convert.ToInt16(ddlSession.SelectedItem.Value) + "," + subid + "," + Courseno + "," + Convert.ToInt16(Session["userno"].ToString()) + "," + degreeNo + "," + SectionNo + "," + semesterno + "";
        }
        else
        {
            sp_callValues = "" + Convert.ToInt16(ddlSession.SelectedItem.Value) + "," + subid + "," + Courseno + "," + Convert.ToInt16(hdfValueruano.Value) + "," + degreeNo + "," + SectionNo + "," + semesterno + "";
            // sp_callValues = objMarksEntry.GetAllGradesSection(Convert.ToInt16(ddlSession.SelectedItem.Value), subid, Courseno, Convert.ToInt16(hdfValueruano.Value), degreeNo, SectionNo, semesterno);
        }

        string sp_procedure = "PKG_GET_ALL_GRADES_REVAL";
        string sp_parameters = "@P_SESSIONNO,@P_SUBID,@P_COURSENO,@P_UANO,@P_DEGREENO,@P_SECTIONNO,@P_SEMESTERNO";

        DataSet dsGrades = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

        if (dsGrades != null && dsGrades.Tables.Count > 0)
        {
            lvGrades.DataSource = dsGrades;
            lvGrades.DataBind();
        }
        foreach (ListViewDataItem dataRow in lvGrades.Items)
        {
            TextBox txtmin = dataRow.FindControl("txtMin") as TextBox;
            TextBox txtmax = dataRow.FindControl("txtMax") as TextBox;
            txtmax.Enabled = false;
            txtmin.Enabled = false;
        }
    }

}
