//=================================================================================
// PROJECT NAME  : RF-CAMPUS [COMMON CODE]                                                         
// MODULE NAME   : EXAMINATION - ALL MARK ENTRY FOR BACKLOG                                          
// CREATION DATE : 02 - MAY - 2022                                                    
// CREATED BY    : NARESH BEERLA                                             
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

public partial class ACADEMIC_MarkEntryAll_Backlog : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

    string th_pr = string.Empty;
    int subid;
    //int sectionno;
    int Grade_SectionNo = 0;

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

                    //--------------************* COMMENTED THIS LOGIC AS THE COLLEGE'S ARE NOT SHOWING FOR BACKLOG ON DT 28052022 AS PER REQUIREMENT ***************//

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
                    //--------------************* COMMENTED THIS LOGIC AS THE COLLEGE'S ARE NOT SHOWING FOR BACKLOG ON DT 28052022 AS PER REQUIREMENT ***************//

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_STUDENT_RESULT WHERE (UA_NO=" + Session["userno"].ToString() + " OR UA_NO_PRAC=" + Session["userno"].ToString() + " ) AND ISNULL(CANCEL,0)=0 AND ISNULL(PREV_STATUS,0)=1)", "SESSIONNO DESC");

                    if (Session["usertype"].ToString() == "3")//&& Session["dec"].ToString() != "1")
                    { }
                    // this.ShowCourses();
                    else
                    {
                        //trbranch.Visible = true;
                        //trdegree.Visible = true;
                        //trscheme.Visible = true;
                        //trSemester.Visible = true;
                        //trExam.Visible = true;
                        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
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

            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckActivity()
    {
        string colgno = objCommon.LookUp("ACD_COURSE_TEACHER", "DISTINCT COLLEGE_ID", "UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0+)");
        //Session["college_nos"].ToString();
        string sessionno = string.Empty;
        // sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 AND COLLEGE_ID IN("+colgno+")");

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO) INNER JOIN ACD_SESSION_MASTER S ON (S.SESSIONNO=SA.SESSION_NO)", "max(SA.SESSION_NO)SESSION_NO", "AM.ACTIVITY_CODE IN('BACKENDSEM') AND ISNULL(FLOCK,0)=1 AND COLLEGE_ID IN(" + colgno + ")");

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


    private void ShowCourses()
    {
        DataSet ds = objMarksEntry.GetCourseForTeacherEndSem_Backlog(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]));
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

            // commeted on 06052022

            //DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

            //if (dsExams != null && dsExams.Tables.Count > 0)
            //{
            //    if (dsExams.Tables[0].Rows.Count <= 0)
            //    {
            //        objCommon.DisplayMessage(UpdatePanel1, "Marks Entry Activity is OFF. Contact MIS!", this.Page);
            //        return;
            //    }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(UpdatePanel1, "Marks Entry Activity is OFF. Contact MIS!", this.Page);
            //    return;
            //}

            // commmented on 06052022

            LinkButton lnk = sender as LinkButton;


            int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lnk.ToolTip) + ""));
            string[] sec_batch_sem = lnk.CommandArgument.ToString().Split('+');
            string semesterno = sec_batch_sem[2].ToString();
            string sectionno = sec_batch_sem[0].ToString();
            string College_id = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "SESSIONNO=" + ddlSession.SelectedValue);

            // Check Mark Enrty Activitity 

            // commented on 06052022
            //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND COLLEGE_ID IN (" + College_id + ") AND SEMESTER LIKE '%" + semesterno + "%')", "SESSIONNO DESC");

            //if (ds_CheckActivity.Tables[0].Rows.Count == 0)
            //{
            //    objCommon.DisplayMessage(this, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
            //    return;
            //}

            // commented on 06052022
  //************************************* COMMENTED THIS  LOGIC  BECOZ OF BACKLOG END SEM MARK ENTRY AS PER REQUIREMENT ON DT 02052022 ********************************

            // CHECKS WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY

            //string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY";
            //string sp_parameters = "@P_COURSENO,@P_SECTIONNO,@P_SCHEMENO,@P_UA_NO";
            //string sp_callValues = "" + (lnk.ToolTip) + "," + sectionno + "," + SchemeNo + "," + (Session["userno"].ToString()) + "";
            //DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
            //if (dschk.Tables[0].Rows.Count == 0 && dschk.Tables != null)
            //{
            //    //string islocked = dschk.Tables[0].Rows[0]["LOCK"]==string.Empty?"0":dschk.Tables[0].Rows[0]["LOCK"].ToString();

            //    //if (islocked == "0" || islocked == string.Empty || islocked == null)
            //    //{
            //    //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
            //    objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for !", this.Page);
            //    return;
            //    //}
            //}

            //if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
            //{
            //    string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

            //    if (islocked == "0" || islocked == string.Empty || islocked == null)
            //    {
            //        //objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for"+lnk.Text.ToString()+" !", this.Page);
            //        objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for !", this.Page);
            //        return;
            //    }
            //}

            // ENDS HERE WHETHER ALL THE SUBEXAMS ARE LOCKED OR NOT FOR END SEM MARK ENTRY 

            //      ************************************* COMMENTED THIS  LOGIC  BECOZ OF BACKLOG END SEM MARK ENTRY AS PER REQUIREMENT ON DT 02052022 ********************************

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
                hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";
                lblSession.Text = ddlSession.SelectedItem.Text;

                int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                hdfSubid.Value = subId.ToString();

                // commented on 06052022
                string sp_proc = "PKG_ACD_GET_RULE_FOR_ENDSEM_MARK_ENTRY";
                string sp_para = "@P_COURSENO,@P_SCHEMENO,@P_SESSIONNO";
                string sp_cValues = "" + (lnk.ToolTip) + "," + SchemeNo + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";
                DataSet dsRule = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);

                if (dsRule.Tables[0].Rows.Count > 0 && dsRule.Tables != null && dsRule.Tables[0] != null)
                {
                    string Rule = dsRule.Tables[0].Rows[0]["RULE1"].ToString();
                    hdfCourseTotal.Value = dsRule.Tables[0].Rows[0]["COURSE_TOTAL"].ToString();
                    hdfMinPassMark.Value = dsRule.Tables[0].Rows[0]["MIN_PASSING"].ToString();
                    hdfMinPassMark_I.Value = dsRule.Tables[0].Rows[0]["INT_MIN_PASSING"].ToString();
                    hdfRule.Value = Rule;
                }
                else
                {
                    objCommon.DisplayMessage(UpdatePanel1, "Kindly Check the End Sem Mark Entry Rule is not Defined!", this.Page);
                    return;
                }
                // commented on 06052022


                // CHECKS LOCK CONDIDTION FOR THE PARTICULAR COURSE
                this.ShowStudents(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value), "R.PREV_STATUS,R.REGNO");
                //        return;
                Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"]));
                if (Grade_SectionNo > 0)
                {
                    this.ShowGradesSection(Convert.ToInt16(lnk.ToolTip), Convert.ToInt16(hdfSection.Value), Convert.ToInt16(hdfSemester.Value));
                }
                else
                {
                    this.ShowGrades(Convert.ToInt16(lnk.ToolTip));
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
    private void BindJS()
    {
        try
        {
            foreach (GridViewRow gvRow in gvStudent.Rows)
            {
                TextBox txtTAMarks = gvRow.FindControl("txtTAMarks") as TextBox;
                TextBox txtTotMarks = gvRow.FindControl("txtTotMarks") as TextBox;
                Label lblTAMarks = gvRow.FindControl("lblTAMarks") as Label;
                Label lblTAMinMarks = gvRow.FindControl("lblTAMinMarks") as Label;

                Label lblTotMinMarks = gvRow.FindControl("lblTotMinMarks") as Label;
                Label lblTotMarks = gvRow.FindControl("lblTotMarks") as Label;


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

                HiddenField hdfAbolish = gvRow.FindControl("hdfAbolish") as HiddenField;

                HiddenField hdfRedo = gvRow.FindControl("hdfredo") as HiddenField;

                //int ExamType = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue + ""));

                //int SchemeNo = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));

                int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                int Scale = 0;//Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCALEDN_MARK", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
                Decimal obtper = Convert.ToDecimal(0);
                Decimal InterMarkPer = Convert.ToDecimal(0);
                Decimal Internalmarks = Convert.ToDecimal(0);
                int totalmarks = Convert.ToInt32(100);

                if (txtTAMarks.Text == string.Empty || txtTAMarks.Text == null || txtTAMarks.Text == "")
                {
                }
                else
                {
                    Internalmarks = Convert.ToDecimal(txtTAMarks.Text);
                }

                if (txtESMarks.Text == string.Empty || txtESMarks.Text == null || txtESMarks.Text == "")
                {
                }
                else
                {
                    obtper = Convert.ToDecimal((Convert.ToDecimal(txtESMarks.Text) * 100 / 100));
                }
                Decimal Extpassing = Convert.ToDecimal(Convert.ToDecimal(hdfMaxCourseMarks.Value) * Convert.ToDecimal(hdfMinPassMark.Value)) / 100;

                if (hdfAbolish.Value == "0")
                {
                    if (Internalmarks <= 0)
                    {
                        InterMarkPer = 0;
                    }
                    else
                    {
                        InterMarkPer = Convert.ToDecimal((Convert.ToDecimal(Internalmarks) * 100) / Convert.ToDecimal(ViewState["MAXMARKS_I"].ToString()));
                    }


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
                }
                else
                {

                    //if (subId == 1)
                    //{
                    //    obtper = Convert.ToDecimal((Convert.ToDecimal(txtESMarks.Text) * 100 / 100));  //var Conversion = obtper;
                    //    var sum = obtper;
                    //    sum.toFixed(2);
                    //}
                    //else if (subid == 4)
                    //{
                    //    var Conversion = (parseFloat(obtper) * 75 / 100);
                    //    var sum = Number(INT) + Number(((obtper * 75) / 100));
                    //    // alert(Conversion);                             
                    //    sum.toFixed(2);
                    //}

                }
                if (txtESMarks.Text.Equals("902.00") || txtESMarks.Text.Equals("902") || txtESMarks.Text.Equals("903.00") || txtESMarks.Text.Equals("903") || txtESMarks.Text.Equals("904.00") || txtESMarks.Text.Equals("904") || txtESMarks.Text.Equals("905.00") || txtESMarks.Text.Equals("905"))
                {
                    if (txtTAMarks.Text == string.Empty || txtTAMarks.Text == "")
                    {
                        txtTAMarks.Text = "0";
                    }
                    txtESMarks.Enabled = false;
                    txtTotMarksAll.Text = Convert.ToString(txtTAMarks.Text.ToString());
                    double Totalmarks = Convert.ToDouble(txtTotMarksAll.Text.ToString());
                    double TotPer = Convert.ToDouble((Totalmarks * 100) / Convert.ToDouble(hdfCourseTotal.Value));
                    txtTotPer.Text = Convert.ToString(TotPer) + ".00";
                }

                int count = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(1)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SUBID=" + Convert.ToInt32(subId) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ""));

                if (count < 1)
                {
                    if (txtESMarks.Text.Equals("902.00") || txtESMarks.Text.Equals("902") || txtESMarks.Text.Equals("903.00") || txtESMarks.Text.Equals("903") || txtESMarks.Text.Equals("904.00") || txtESMarks.Text.Equals("904") || txtESMarks.Text.Equals("905.00") || txtESMarks.Text.Equals("905"))
                    {
                        if (txtTAMarks.Text == string.Empty || txtTAMarks.Text == "")
                        {
                            txtTAMarks.Text = "0";
                        }

                        //txtESMarks.ReadOnly = true;
                        txtESMarks.Enabled = false;
                        txtTotMarksAll.Text = Convert.ToString(txtTAMarks.Text.ToString());
                        double Totalmarks = Convert.ToDouble(txtTotMarksAll.Text.ToString());
                        double TotPer = Convert.ToDouble((Totalmarks * 100) / Convert.ToDouble(hdfCourseTotal.Value));
                        txtTotPer.Text = Convert.ToString(TotPer) + ".00";
                    }
                }
                if (lblESMarks.ToolTip.ToUpper().Equals("TRUE"))
                {
                    txtESMarks.Enabled = false;
                }

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
                            HiddenField hidAbolish = gvStudent.Rows[i].FindControl("hdfAbolish") as HiddenField;


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
                            exam = "EXTERMARK";

                            if (txtMarks.Text.Equals("902.00") || txtMarks.Text.Equals("902") || txtMarks.Text.Equals("903.00") || txtMarks.Text.Equals("903") || txtMarks.Text.Equals("904.00") || txtMarks.Text.Equals("904") || txtMarks.Text.Equals("905.00") || txtMarks.Text.Equals("905"))
                            {
                                hidGradePoint.Value = "0.00";
                                hidConversion.Value = "0.00";
                            }
                            if (hidAbolish.Value == "0")
                            {
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

                            else
                            {
                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                                studids += lbl.ToolTip + ",";
                                totmarks += Convert.ToString(txtTotMarksAll.Text).Trim() == string.Empty ? "-100," : Convert.ToString(txtTotMarksAll.Text).Trim() + ",";
                                grade += Convert.ToString(txtGrade.Text).Trim() == string.Empty ? "-100," : Convert.ToString(txtGrade.Text).Trim() + ",";
                                Gpoint += Convert.ToString(hidGradePoint.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidGradePoint.Value).Trim() + ",";
                                totPer += Convert.ToString(txtTotPer.Text).Trim() == string.Empty ? "-100," : Convert.ToString(txtTotPer.Text).Trim() + ",";
                                FinalConversion += Convert.ToString(hidConversion.Value).Trim() == string.Empty ? "-100," : Convert.ToString(hidConversion.Value).Trim() + ",";
                            }
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
                        //  cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAll(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()));
                        cs = (CustomStatus)objMarksEntry.UpdateMarkEntryAllNew_Backlog(Convert.ToInt32(ddlSession2.SelectedValue), courseno, ccode, studids, marks, totmarks, grade, Gpoint, totPer, lgrade, max, min, point, totStud, lock_status, exam, 0, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), "0", txtTitle.Text, Convert.ToInt16(Session["DEGREENO"].ToString()), Convert.ToInt16(hdfSemester.Value), FinalConversion);
                    }
                }
            }


            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    int subId = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + ""));
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
            Grade_SectionNo = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "COUNT(*)", "COURSENO=" + lblCourse.ToolTip + "AND SESSIONNO=" + ddlSession.SelectedValue + "AND UA_NO=" + Session["userno"] ));//+ "AND SECTIONNO=" + Convert.ToInt16(hdfSection.Value)));
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


        string sp_proc = "PKG_GET_ALL_GRADES_BACKLOG_ENDSEM";
        string sp_para = "@P_SESSIONO,@P_SUBID,@P_COURSENO,@P_UANO,@P_DEGREENO";//,@P_SECTIONNO";
        string sp_cValues = "" + (Convert.ToInt16(ddlSession.SelectedItem.Value)) + "," + subid + "," + Courseno + "," + Convert.ToInt16(Session["userno"].ToString()) + "," + degreeNo + "";// "," + Convert.ToInt32(hdfSection.Value) + "";

        DataSet dsGrades = objCommon.DynamicSPCall_Select(sp_proc, sp_para, sp_cValues);
        //objMarksEntry.GetAllGrades(Convert.ToInt16(ddlSession.SelectedItem.Value), subid, Courseno, Convert.ToInt16(Session["userno"].ToString()), degreeNo);
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

        DataSet dsGrades = objMarksEntry.GetAllGradesSection_Backlog(Convert.ToInt16(ddlSession.SelectedItem.Value), subid, Courseno, Convert.ToInt16(Session["userno"].ToString()), degreeNo, semesterno);
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
                    int MaxValue = Convert.ToInt32(txtmin.Text) - 1;
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

            //ListViewDataItem item1 = lvGrades.Items[0];
            //TextBox a1 = (TextBox)item1.FindControl("txtMax");
            //a1.Enabled = false;
            //if (schemetype == "1")
            //{
            //    ListViewDataItem item2 = lvGrades.Items[7];
            //    TextBox a2 = (TextBox)item2.FindControl("txtMin");
            //    a2.Enabled = false;
            //    ListViewDataItem item3 = lvGrades.Items[8];
            //    TextBox a3 = (TextBox)item3.FindControl("txtMax");
            //    a3.Enabled = false;
            //    ListViewDataItem item4 = lvGrades.Items[8];
            //    TextBox a4 = (TextBox)item4.FindControl("txtMin");
            //    a4.Enabled = false;
            //    ListViewDataItem item5 = lvGrades.Items[9];
            //    TextBox a5 = (TextBox)item5.FindControl("txtMax");
            //    a5.Enabled = false;
            //    ListViewDataItem item6 = lvGrades.Items[9];
            //    TextBox a6 = (TextBox)item6.FindControl("txtMin");
            //    a6.Enabled = false;
            //}
            //else
            //{
            //    ListViewDataItem item2 = lvGrades.Items[6];
            //    TextBox a2 = (TextBox)item2.FindControl("txtMin");
            //    a2.Enabled = false;
            //    ListViewDataItem item3 = lvGrades.Items[7];
            //    TextBox a3 = (TextBox)item3.FindControl("txtMax");
            //    a3.Enabled = false;
            //    ListViewDataItem item4 = lvGrades.Items[7];
            //    TextBox a4 = (TextBox)item4.FindControl("txtMin");
            //    a4.Enabled = false;
            //    ListViewDataItem item5 = lvGrades.Items[8];
            //    TextBox a5 = (TextBox)item5.FindControl("txtMax");
            //    a5.Enabled = false;
            //    ListViewDataItem item6 = lvGrades.Items[8];
            //    TextBox a6 = (TextBox)item6.FindControl("txtMin");
            //    a6.Enabled = false;
            //}

        }
        else
        {
            lvGrades.DataSource = null;
            lvGrades.DataBind();
        }
    }

    private void ShowStudents(int courseNo, int sectionNo, int semesterNo, string orderby)
    {

        try
        {
            //Check Exam Activity is ON
            //==========================
            //DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));          COMMENTED ON 12052022 NEED TO CHECK IS REQUIRED OR NOT


            subid = Convert.ToInt16(objCommon.LookUp("ACD_SUBJECTTYPE", "ISNULL(SUBID,0)", " SUBID IN (SELECT SUBID FROM ACD_COURSE WHERE COURSENO =" + lblCourse.ToolTip + " )"));
            //int ua_no = 0;
            int count = 0;


            DataSet dsStudent = null;
            //FILL STUDENTS WITH EXAMS THAT ARE ON
            //=====================================

            dsStudent = objCommon.FillDropDown("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON (R.IDNO = S.IDNO)	INNER JOIN ACD_COURSE C ON (R.COURSENO = C.COURSENO)	LEFT OUTER JOIN ACD_STUDENT_TEST_MARK T ON (R.SESSIONNO = T.SESSIONNO AND R.COURSENO = T.COURSENO AND R.IDNO = T.IDNO)", "DISTINCT R.IDNO,R.REGNO,R.ROLL_NO ,S.DEGREENO,S.STUDNAME AS STUDNAME,CAST(R.S4MARK AS FLOAT) AS S4MARK,ISNULL(C.S4MAX,0) AS S4MAX,ISNULL(C.S4MIN,0) AS S4MIN,R.LOCKS4,CAST(R.ACTUAL_EXTERMARK AS FLOAT) AS EXTERMARK,CAST(R.INTERMARK AS FLOAT) AS INTERMARK,CAST(R.MARKTOT AS FLOAT) AS MARKTOT,R.GRADE AS GRADE,CAST(R.GDPOINT AS INT) AS GDPOINT,R.SCALEDN_PERCENT,ISNULL(C.MAXMARKS_I,0) AS MAXMARKS_I,ISNULL(C.MAXMARKS_E,0) AS MAXMARKS_E,ISNULL(C.MINMARKS,0) AS MINMARKS,ISNULL(TOTAL_MARK,0) AS TOTAL_MARK,ISNULL(R.LOCKE,0)LOCKE,  S1MARK  AS S1MARK,ISNULL(C.S1MAX,0) AS S1MAX,ISNULL(C.S1MIN,0) AS S1MIN,R.LOCKS1,R.S2MARK AS S2MARK,ISNULL(C.S2MAX,0) AS S2MAX,ISNULL(C.S2MIN,0) AS S2MIN,R.LOCKS2", "	CAST(R.S3MARK AS INT) AS S3MARK,ISNULL(C.S3MAX,0) AS S3MAX,ISNULL(C.S3MIN,0) AS S3MIN,R.LOCKS3,R.PREV_STATUS,R.MARKTOT,R.SECTIONNO,ISNULL(ABOLISH,0)ABOLISH,ISNULL(RE_REGISTER,0)RE_REGISTER", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1 AND ISNULL(R.PREV_STATUS,0)=1 ", orderby);
            //AND R.SECTIONNO = " + hdfSection.Value + " removed section becoz not required for backlog as per gowdham on dt 12052022


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
                    //     gvStudent.Columns[6].Visible = false;
                    gvStudent.Columns[7].Visible = false;


                    //  DataTableReader dtrExams = dsExams.Tables[0].CreateDataReader();      COMMENTED ON 12052022 NEED TO CHECK IS REQUIRED OR NOT
                    //   while (dtrExams.Read() || dtrExams.Read()==null)      COMMENTED ON 12052022 NEED TO CHECK IS REQUIRED OR NOT
                 //   {

                    //if ((dtrExams["FLDNAME"].ToString() == "EXTERMARK") || (dtrExams["FLDNAME"].ToString() == ""))      COMMENTED ON 12052022 NEED TO CHECK IS REQUIRED OR NOT
                    if (("BACKLOG" == "BACKLOG"))
                        {
                            if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["MAXMARKS_E"]) > 0)
                            {
                                gvStudent.Columns[3].HeaderText = "INTERNAL";// <br>" + "[Max : " + dsStudent.Tables[0].Rows[0]["MAXMARKS_I"].ToString() + "]"; COMMENTED ON 12052022
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
                                hdfMaxCourseMarks_I.Value = dsStudent.Tables[0].Rows[0]["MAXMARKS_I"].ToString();

                                ViewState["islock"] = Convert.ToBoolean(dsStudent.Tables[0].Rows[0]["LOCKE"].ToString());

                                if (Convert.ToBoolean(ViewState["islock"]) == true)
                                {
                                    btnLastSave.Visible = false;
                                }
                                else
                                {
                                    btnLastSave.Visible = true;
                                    btnLastSave.Enabled = true;
                                }

                            }

                        }

                    //   }    COMMENTED ON 12052022 NEED TO CHECK IS REQUIRED OR NOT

                    //   dtrExams.Close();    COMMENTED ON 12052022 NEED TO CHECK IS REQUIRED OR NOT
                    //   dtrExams.Dispose();    COMMENTED ON 12052022 NEED TO CHECK IS REQUIRED OR NOT

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
                    btnLastSave.Enabled = true;
                    btnLock.Enabled = true;
                    // btnSave.Visible = true;
                    btnLock.Visible = true;
                }
            }


            int TotalAllStudent = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "COUNT(R.IDNO)", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
            txtTotalAllStudent.Text = TotalAllStudent.ToString();

            int MarksTotal = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "SUM(ISNULL(R.MARKTOT,0))", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
            txtMarksTotal.Text = MarksTotal.ToString();

            double Average = Convert.ToDouble(objCommon.LookUp("ACD_STUDENT_RESULT R", "SUM(ISNULL(R.MARKTOT,0))/COUNT(IDNO)", "R.SESSIONNO = " + ddlSession.SelectedValue + " AND R.COURSENO = " + lblCourse.ToolTip + " AND (((R.UA_NO = " + Session["userno"].ToString() + ") OR VALUER_UA_NO = " + Session["userno"].ToString() + " ) OR ((UA_NO_PRAC = " + Session["userno"].ToString() + ") OR VALUER_UA_NO_PRAC = " + Session["userno"].ToString() + " )) AND R.SEMESTERNO=" + hdfSemester.Value + " AND R.SECTIONNO = " + hdfSection.Value + " AND (R.DETAIND=0 OR R.DETAIND IS NULL)AND (R.CANCEL=0 OR R.CANCEL IS NULL) AND EXAM_REGISTERED = 1"));
            txtAverage.Text = Average.ToString();



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

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
        //btnSave.Enabled = false;
        btnLock.Enabled = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        this.ShowReport1("MarksListReport", "rptMarksList_Backlog.rpt");
    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            //***************   COMMENTED THIS LOGIC AS THE COLLEGE'S ARE NOT SHOWING FOR BACKLOG ON DT 28052022 AS PER REQUIREMENT ***************//
            //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_IDS IN (SELECT DISTINCT COLLEGE_ID FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0) AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%')", "SESSIONNO DESC");
            //***************   COMMENTED THIS LOGIC AS THE COLLEGE'S ARE NOT SHOWING FOR BACKLOG ON DT 28052022 AS PER REQUIREMENT ***************//


            //COMMENTED BY PRAFULL ON DT:29122023
           // DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_IDS IN (SELECT DISTINCT COLLEGE_ID FROM ACD_STUDENT_RESULT R  INNER JOIN ACD_STUDENT S ON (S.IDNO=R.IDNO) WHERE (UA_NO=" + Session["userno"].ToString() + " OR UA_NO_PRAC=" + Session["userno"].ToString() + " ) AND ISNULL(CANCEL,0)=0 AND ISNULL(PREV_STATUS,0)=1) AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%')", "SESSIONNO DESC");

            DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_IDS IN (SELECT DISTINCT COLLEGE_ID FROM ACD_STUDENT_RESULT R  INNER JOIN ACD_STUDENT S ON (S.IDNO=R.IDNO) WHERE (UA_NO=" + Session["userno"].ToString() + " OR UA_NO_PRAC=" + Session["userno"].ToString() + " ) AND ISNULL(CANCEL,0)=0 AND ISNULL(PREV_STATUS,0)=1) AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");


            // AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND ACTIVITY_CODE='END SEM'
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

    private void ShowReport1(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_SEMESTERNO=" + hdfSemester.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=1";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnExcelReport_Click(object sender, EventArgs e)
    {

        try
        {

            //DataSet ds = objMarksEntry.GetEndExamMarksDataExcel(Convert.ToInt32(ddlSession2.SelectedValue) , Convert.ToInt32(lblCourse.ToolTip) , Convert.ToInt32(hdfSection.Value) ,Convert.ToInt32('0'),Convert.ToInt32(Session["userno"]));
            DataSet ds = objMarksEntry.GetEndExamMarksDataExcel_Backlog(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblCourse.ToolTip), Convert.ToInt32(hdfSemester.Value), Convert.ToInt32('1'), Convert.ToInt32(Session["userno"]));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //string degree = objCommon.LookUp("ACD_DEGREE", "DEGREENAME", "DEGREENO=" + ddlDegree.SelectedValue);
                //this.ShowReportExcel("xls",dcrReport, reportTitle, rptFileName);
                GridView GVDayWiseAtt = new GridView();
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=BacklogEndSemesterExamReport.xls";
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

    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)(sender);

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + "MarksEntryReport";
        url += "&path=~,Reports,Academic," + "rptMarksList_Backlog.rpt";
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[0]) + ",@P_SECTIONNO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[1]) + ",@P_SEMESTERNO=" + Convert.ToInt32(lnk.CommandArgument.Split(',')[2]) + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_PREV_STATUS=1";

        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);






    }
}