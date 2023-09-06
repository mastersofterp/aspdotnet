using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LevelFeedback : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    StudentFeedBack objSEB = new StudentFeedBack();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;
    string sessionno = string.Empty;
    string collegeid = string.Empty;

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
                    //Check for Activity On/Off
                    GetStudentDeatlsForEligibilty();
                    //if (CheckActivity() == false)
                    //    return;
                    //ViewState["FEEDBACK_NO"] = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "(FEEDBACK_NAME like '%Subject%' or FEEDBACK_NAME like '%faculty%')"));
                    ViewState["FEEDBACK_NO"] = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "( CASE WHEN (SELECT COUNT(1) FROM ACD_FEEDBACK_MASTER WHERE (FEEDBACK_NAME like '%Subject%' or FEEDBACK_NAME like '%faculty%'))>0 THEN (SELECT FEEDBACK_NO FROM ACD_FEEDBACK_MASTER WHERE (FEEDBACK_NAME like '%Subject%' or FEEDBACK_NAME like '%faculty%'))  ELSE 0 END ) as FEEDBACK_NO", ""));



                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    try
                    {
                        if (Session["usertype"].ToString() == "2")
                        {
                            //fill student details
                            pnlStudInfo.Visible = true;
                            FillLabel();
                            //Fill DropDownList
                            objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "EXAMNO", "LEFT(EXAMNAME,5) EXAMNAME", "PATTERNNO=(SELECT PATTERNNO FROM ACD_SCHEME WHERE SCHEMENO= " + lblScheme.ToolTip + ") AND FLDNAME='S1'", "EXAMNO");
                        }
                        else
                        {
                            //pnlSearch.Visible = true;
                            pnlStudInfo.Visible = false;
                        }
                    }
                    catch { }
                }
                PopulateDropDown();
            }
            //else 
            //{
            //    GetStudentDeatlsForEligibilty();
            //}
            divMsg.InnerHtml = string.Empty;
            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (Session["OrgId"].ToString() == "15")
            {
                divcomment.Visible = true;
            }
            else
            {
                divcomment.Visible = false;
            }
        }
        catch { }
    }

    private void FillCourseList()
    {
        Course objCourse = new Course();
        CourseController objCC = new CourseController();
        SqlDataReader dr = null;
        if (Session["usertype"].ToString() == "2")
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
        }
        if (dr != null)
        {
            if (dr.Read())
            {
                int sessionno = 0;
                lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();

                lblSession.ToolTip = Session["sessionno"].ToString();
                lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", " SESSIONNO = " + Convert.ToInt32(Session["sessionno"]));
                lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
            }
        }
        if (dr != null) dr.Close();

        if (lblScheme.ToolTip != "")
        {
            int idno = 0;
            idno = Session["usertype"].ToString() == "2" ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(ViewState["Id"]);
            DataSet ds = objSFBC.GetCourseListStatus(Convert.ToInt32(lblSession.ToolTip), idno, Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Panel1.Visible = false;
                lblcrse.Visible = true;
                pnlSubject.Visible = true;
                lvSelected.DataSource = ds;
                lvSelected.DataBind();
                this.CheckSubjectAssign();
            }
            else
            {
                Panel1.Visible = true;
                objCommon.DisplayMessage("No Course registration found for activity started session.", this.Page);
                return;
            }
        }
    }

    private void PopulateDropDown()
    {

        //objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_ACTIVITY A INNER JOIN ACD_FEEDBACK_MASTER M ON (A.FEEDBACK_TYPENO=M.FEEDBACK_NO)", "FEEDBACK_TYPENO", "FEEDBACK_NAME", "FEEDBACK_TYPENO>0 AND STARTED=1 AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"]) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "AND SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "AND SESSION_NO=" + Convert.ToInt32(Session["sessionno"]), "FEEDBACK_TYPENO");
    }
    //to get student details for checking activity
    protected void GetStudentDeatlsForEligibilty()
    {
        try
        {
            //ddlSession.Items.Clear();
            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO,COLLEGE_ID,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                collegeid = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                //objCommon.FillDropDownList(ddlSession, "ACD_FEEDBACK_ACTIVITY FA INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=FA.SESSION_NO)", "DISTINCT SESSION_NO", " SESSION_NAME", "STARTED=1 AND SHOW_STATUS=1 AND DEGREENO=" + Degreeno + " AND BRANCHNO=" + branchno + " AND SEMESTERNO=" + Semesterno + " AND FA.COLLEGE_ID=" + collegeid, "SESSION_NO");
                //CheckActivity();
                
                ActivityController objActController = new ActivityController();
                DataSet dssession = objActController.FillFeedbackSession(Convert.ToInt32(Degreeno), Convert.ToInt32(branchno), Convert.ToInt32(Semesterno), Convert.ToInt32(collegeid));
                if (dssession != null && dssession.Tables.Count > 0 && dssession.Tables[0].Rows.Count > 0)
                {
                    
                    ddlSession.DataSource = dssession;
                    ddlSession.DataValueField = dssession.Tables[0].Columns[0].ToString();
                    ddlSession.DataTextField = dssession.Tables[0].Columns[1].ToString();
                    ddlSession.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage("This Activity has been Stopped.", this.Page);
                    pnlStudInfo.Visible = false;
                    return;
                }

            }
            else
            {
                objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "rd sem.Please Contact Admin.!!", this.Page);
                pnlStudInfo.Visible = false;
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedbackAns.GetStudentDeatlsForEligibilty() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //check activity start or not
    private bool CheckActivity()
    {
        try
        {
            sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            Session["sessionno"] = sessionno;

            ActivityController objActController = new ActivityController();
            if (sessionno != "")
            {
                DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
                if (dtr.Read())
                {
                    if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                    {
                        objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                        //pnlSearch.Visible = false;
                        pnlStudInfo.Visible = false;

                    }
                    if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                    {
                        objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                        //pnlSearch.Visible = false;
                        pnlStudInfo.Visible = false;

                    }
                }
                else
                {
                    objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                    //pnlSearch.Visible = false;
                    pnlStudInfo.Visible = false;

                }

                dtr.Close();
                return true;
            }
            else
            {
                objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                // pnlSearch.Visible = false;
                pnlStudInfo.Visible = false;
                return false;
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.ShowError(Page, "ACADEMIC_StudentFeedbackAns.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
                return false;
            }
        }


    }


    //to get current session
    public int GetSession()
    {
        int sessionno = 0;
        string act_code = string.Empty;

        int idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_TYPE = 2 AND UA_NO=" + Session["userno"]));
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno));
        string session = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "SA.STARTED = 1 AND (AM.ACTIVITY_CODE='Feedback' OR AM.ACTIVITY_CODE='FBM') AND " + branchno + " IN (SELECT VALUE FROM DBO.Split(BRANCH,','))");

        if (session != string.Empty)
        {
            sessionno = Convert.ToInt32(session);
        }
        return sessionno;
    }


    //to fill student details in label control
    private void FillLabel()
    {
        Course objCourse = new Course();
        CourseController objCC = new CourseController();
        SqlDataReader dr = null;
        if (Session["usertype"].ToString() == "2")
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
        }
        if (dr != null)
        {
            if (dr.Read())
            {
                //int sessionno = 0;
                lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
                //sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
                //Session["sessionno"] = sessionno;
                //lblSession.ToolTip = Session["sessionno"].ToString();
                //lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", " SESSIONNO = " + Convert.ToInt32(Session["sessionno"]));
                lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
            }
        }
        if (dr != null) dr.Close();
    }



    //function to show course name wise teacher
    public string GetCourseName(object coursename, object TeachName, object Teachertype)
    {
        return coursename.ToString() + " [<span style='color:Green;font-weight: bold;'>" + TeachName.ToString() + "</span>]" + " [<span style='color:darkcyan;font-weight: bold;'>" + Teachertype.ToString() + "</span>]";
    }


    //chechk which subject assign to which teacher with feedback status
    private void CheckSubjectAssign()
    {
        SqlDataReader dr = null;
        foreach (ListViewDataItem item in lvSelected.Items)
        {
            Label lblComplete = item.FindControl("lblComplete") as Label;
            lblComplete.ForeColor = System.Drawing.Color.Red;
            lblComplete.Text = "Incomplete";
        }

        if (Session["usertype"].ToString() == "2")
            dr = objSFBC.GetCourseSelectedStatus(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
        else
            dr = objSFBC.GetCourseSelectedStatus(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(ViewState["Id"]), Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
        if (dr != null)
        {
            while (dr.Read())
            {
                foreach (ListViewDataItem item in lvSelected.Items)
                {
                    LinkButton lnkCourse = item.FindControl("lnkbtnCourse") as LinkButton;
                    Label lblComplete = item.FindControl("lblComplete") as Label;

                    if (Convert.ToInt32(lnkCourse.CommandArgument) == Convert.ToInt32(dr["COURSENO"].ToString()) && lnkCourse.ToolTip == dr["UA_NO"].ToString())
                    {
                        lblComplete.ForeColor = System.Drawing.Color.Green;
                        lblComplete.Text = "Complete";
                        break;
                    }
                }
            }
        }
        if (dr != null) dr.Close();
    }



    private void FillCourseQuestion(int SubID, int semesterno)
    {
        objSEB.CTID = Convert.ToInt32(ddlFeedbackTyp.SelectedValue);
        objSEB.SubId = SubID;
        objSEB.SemesterNo = semesterno;
        try
        {
            DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB);
            // DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB, SubID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblcrse.Visible = true;
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                pnlFeedback.Visible = true;
                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
                    HiddenField hdnCourse = dataitem.FindControl("hdnCourse") as HiddenField;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(hdnCourse.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
                        {
                            string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
                            string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

                            if (ansOptions.Contains(","))
                            {
                                string[] opt;
                                string[] val;

                                opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
                                val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

                                int itemindex = 0;
                                for (int j = 0; j < opt.Length; j++)
                                {
                                    for (int k = 0; k < val.Length; k++)
                                    {
                                        if (j == k)
                                        {
                                            RadioButtonList lst;
                                            lst = new RadioButtonList();

                                            rblCourse.Items.Add(opt[j]);
                                            rblCourse.SelectedIndex = itemindex;
                                            rblCourse.SelectedItem.Value = val[j];

                                            itemindex++;
                                            break;
                                        }
                                    }
                                }
                            }
                            rblCourse.SelectedIndex = -1;
                            break;
                        }
                    }
                }
            }
            else
            {
                pnlFeedback.Visible = false;
                lvCourse.Items.Clear();
                lblcrse.Visible = false;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                objCommon.DisplayMessage("No feedback questions found for this semester course.", this.Page);
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.fillCourseQuestion()-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    //to check page is authorized or not
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackAns.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentFeedBackAns.aspx");
        }
    }


    // function to save the feedback details
    private int FillAnswers(DataTable dt_FEEDBACK)
    {
        objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
        objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
        objSEB.Date = DateTime.Now;
        objSEB.CollegeCode = Session["colcode"].ToString();
        objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
        if (ViewState["MODE"].ToString() == "2")
        {
            objSEB.CourseNo = Convert.ToInt32(ViewState["COURSENO"].ToString());
            objSEB.UA_NO = Convert.ToInt32(ViewState["TeachNo"].ToString());
        }
        objSEB.FB_Status = true;
        objSEB.OverallImpression = "0";
        //objSEB.Suggestion_A = "0";
        //objSEB.Suggestion_B = "0";
        //objSEB.Suggestion_C = "0";
        //objSEB.Suggestion_D = "0";
        objSEB.Suggestion_A = lblWhatOtherChanges.Text;
        objSEB.Suggestion_B = txtWhatOtherChanges.Text;
        objSEB.Suggestion_C = lblAnyComments.Text;
        objSEB.Suggestion_D = txtAnyComments.Text;

        objSEB.CTID = Convert.ToInt32(ddlFeedbackTyp.SelectedValue);

        objSEB.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);

        string COMMENTS = string.Empty;
        if (Session["OrgId"].ToString() == "15")
        {
            COMMENTS = txtComments.Text;
        }
        else
        {
            COMMENTS = "";
        }
        

        if (!txtRemark.Text.Equals(string.Empty)) objSEB.Remark = txtRemark.Text.ToString();
        int ret = objSFBC.InsertStudentFeedBackAnswer(objSEB, dt_FEEDBACK, COMMENTS);
        return ret;
    }



    // to save the feedback details
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (divcomment.Visible == true)
            {
                if (txtComments.Text.Length > 1000)
                {
                    objCommon.DisplayMessage("Maximum characters should be less than 1000 !", this.Page);
                    return;
                }
            }
            DataTable dt_FEEDBACK = new DataTable("FEEDBACK");
            dt_FEEDBACK.Columns.Add(new DataColumn("QID", typeof(int)));
            dt_FEEDBACK.Columns.Add(new DataColumn("QANSID", typeof(int)));
            dt_FEEDBACK.Columns.Add(new DataColumn("QANSTEXT", typeof(string)));
            dt_FEEDBACK.Columns.Add(new DataColumn("QANSWERTEXT", typeof(string)));
            if (Session["usertype"].ToString() == "2")
            {
                foreach (ListViewDataItem dataitem in lvCourse.Items)
                {
                    RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
                    Label lblCourse = dataitem.FindControl("lblCourse") as Label;
                    TextBox txtcourse = dataitem.FindControl("txtcourse") as TextBox;
                    HiddenField hfOPTION_TYPE = dataitem.FindControl("hfOPTION_TYPE") as HiddenField;
                    if ((rblCourse.SelectedValue == "" && rblCourse.Visible==true) || (txtcourse.Text=="" && txtcourse.Visible==true)  )
                    {
                        objCommon.DisplayMessage("You must have to answer all the questions", this.Page);
                        return;
                    }
                    else
                    {
                        if (hfOPTION_TYPE.Value == "R")
                        {
                            //objSEB.AnswerIds += rblCourse.SelectedValue + ",";
                            //objSEB.QuestionIds += lblCourse.Text + ",";
                            dt_FEEDBACK.Rows.Add(Convert.ToInt32(lblCourse.Text), Convert.ToInt32(rblCourse.SelectedValue), "0", rblCourse.SelectedItem.Text);
                        }
                        else if (hfOPTION_TYPE.Value == "T")
                        {
                            //objSEB.AnswerIds += txtcourse.Text + ",";
                            //objSEB.QuestionIds += lblCourse.Text + ",";
                            dt_FEEDBACK.Rows.Add(Convert.ToInt32(lblCourse.Text), 0, txtcourse.Text, "");
                        }
                    }
                }
                
                //objSEB.AnswerIds = objSEB.AnswerIds.TrimEnd(',');
                //objSEB.QuestionIds = objSEB.QuestionIds.TrimEnd(',');

                //this.FillAnswers();

                int retFlag = this.FillAnswers(dt_FEEDBACK);

                if (retFlag == 1)
                {
                    objCommon.DisplayMessage("Your FeedBack Saved Successfully !", this.Page);
                    txtWhatOtherChanges.Text = "";
                    txtAnyComments.Text = "";
                    //ddlFeedbackTyp.SelectedIndex = 0;
                    //ddlSession.SelectedIndex = 0;
                }
                else
                {
                    //objCommon.DisplayMessage("Your FeedBack Saved Successfully !", this.Page);
                    objCommon.DisplayMessage("Something Went Wrong !", this.Page);
                }

                this.CheckSubjectAssign();

                this.ClearControls();
                //fillquestion();
            }
            else
            {
                objCommon.DisplayMessage("Only Students fills this form!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //to clear all controls
    private void ClearControls()
    {
        lblMsg.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ddlExam.SelectedIndex = 0;
        pnlFeedback.Visible = false;
        txtComments.Text = string.Empty;
    }

    //to load questions for selected course
    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            lblcrse.Text = lnk.Text;
            lblteacher.Text = lnk.Text;
            ViewState["TeachNo"] = lnk.ToolTip;
            ViewState["COURSENO"] = lnk.CommandArgument;
            txtWhatOtherChanges.Text = string.Empty;
            txtAnyComments.Text = string.Empty;

            ViewState["SubId"] = 0;
            if (Convert.ToInt32(ddlFeedbackTyp.SelectedValue) == 0)
            {
                objCommon.DisplayMessage(this, "Please Select Feedback Type", this.Page);
                return;
            }

            if (ViewState["COURSENO"] != "")
            {
                string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + ViewState["COURSENO"]);
                if (subid != "")
                {
                    ViewState["SubId"] = subid;
                }
                else
                {
                    ViewState["SubId"] = 0;
                }
            }



            string count = "-1";

            if (lnk.ToolTip == string.Empty)
            {
                objCommon.DisplayMessage(this, "No faculty is assigned to the selected Course!!!", this.Page);
                return;
            }
            else
            {
                if (lnk.ToolTip != "")
                {
                    DataSet dsCheckStudAtt = objSFBC.GetCourseWiseAttPercent(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(lnk.CommandName), Convert.ToInt32(lnk.CommandArgument), Convert.ToInt32(lblName.ToolTip));
                    if (dsCheckStudAtt.Tables[0].Rows.Count > 0)
                    {
                        //to check att PErcent course wise for CBCS only
                        if (dsCheckStudAtt.Tables[0].Rows[0]["SCHEMETYPE"].ToString() == "CBCS")
                        {
                            //to check Attendance % for CBCS 
                            if (Convert.ToDouble(dsCheckStudAtt.Tables[0].Rows[0]["ATT_PER"].ToString()) >= 0.00)//75
                            {
                                //to check entry already done or not
                                count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + Convert.ToInt32(lnk.CommandArgument) + "AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND UA_NO=" + Convert.ToInt32(lnk.ToolTip) + "AND CTID=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Not Eligible For this Subject Feedback because Attendance is only " + dsCheckStudAtt.Tables[0].Rows[0]["ATT_PER"].ToString() + "%. Attendance Should be greater than or equals to 75.00%", this.Page);
                                pnlFeedback.Visible = false;
                                lblMsg.Text = "";
                                lblMsg.Visible = false;
                                return;
                            }
                        }
                        else
                        {
                            //to check entry already done or not
                            count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + Convert.ToInt32(lnk.CommandArgument) + "AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND UA_NO=" + Convert.ToInt32(lnk.ToolTip) + "AND CTID=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Attendance Not Done For This Selected Course!!!", this.Page);
                        pnlFeedback.Visible = false;
                        lblMsg.Text = "";
                        lblMsg.Visible = false;
                        return;
                    }

                }

            }

            if (Convert.ToInt32(count) > 0)//entry already done
            {
                string date = "";
                date = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "distinct Convert(varchar(10),DATE,103)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND isnull(COURSENO,0)=" + Convert.ToInt32(lnk.CommandArgument) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND isnull(UA_NO,0)=" + Convert.ToInt32(lnk.ToolTip) + " AND CTID=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue));

                if (date != "")
                {
                    lblMsg.Text = "FeedBack is already completed for " + lnk.Text + " on DATE " + date; ;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                    pnlFeedback.Visible = false;
                }
            }
            else if (Convert.ToInt32(count) == 0)//new entry
            {
                lblMsg.Text = "";
                lblMsg.Visible = false;
                FillCourseQuestion(Convert.ToInt16(ViewState["SubId"]), Convert.ToInt32(lnk.CommandName));
                //FillTeacherQuestion();

            }
            if (Session["OrgId"].ToString() == "2")
            {
                if (ddlFeedbackTyp.SelectedValue == "1" || ddlFeedbackTyp.SelectedValue == "2")
                {
                    lblWhatOtherChanges.Text = "Any other suggestions:";
                    divfeedback.Visible = false;
                    lblAnyComments.Text = string.Empty;
                }
                else
                {
                    lblWhatOtherChanges.Text = "Mention the topics to be removed from the syllabus because they are not the prerequisite / relevant / contemporary / required for employment / correlating with course outcomes, etc.";
                    divfeedback.Visible = true;
                    lblAnyComments.Text = "Mention the topics to be included in the syllabus because they are prerequisite / relevant / contemporary / required for employment / correlating with course outcomes etc.";
                }
            }
            else
            {
                lblWhatOtherChanges.Text = "any additional comments (write briefly)?";
                lblAnyComments.Text = "any additional comments (write briefly)?";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    //to clear the page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void btnReport_Click(object sender, EventArgs e)
    //{
    //    string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString()));
    //    if (Convert.ToInt32(count) != 0)
    //        ShowReport("Student_FeedBack", "StudentFeedBackAns.rpt");
    //    else
    //        objCommon.DisplayMessage("Record Not Found", this.Page);
    //}

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblName.ToolTip) + ",@P_SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString());
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    string idno = objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "REGNO='" + txtSearch.Text.Trim() + "'");
    //    if (idno != "")
    //    {
    //        ViewState["Id"] = Convert.ToInt32(idno);
    //        FillLabel();
    //        pnlStudInfo.Visible = true;
    //        btnClear.Visible = true;
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("Record Not Found", this.Page);
    //    }
    //}
    //private void FillTeacherQuestion()
    //{
    //    objSEB.CTID = 2;
    //    try
    //    {
    //        DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB);
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            lblteacher.Visible = true;
    //            lvTeacher.DataSource = ds;
    //            lvTeacher.DataBind();
    //            foreach (ListViewDataItem dataitem in lvTeacher.Items)
    //            {
    //                RadioButtonList rblTeacher = dataitem.FindControl("rblTeacher") as RadioButtonList;
    //                HiddenField hdnTeacher = dataitem.FindControl("hdnTeacher") as HiddenField;

    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    if (Convert.ToInt32(rblTeacher.ToolTip) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
    //                    {
    //                        string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
    //                        string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

    //                        if (ansOptions.Contains(","))
    //                        {
    //                            string[] opt;
    //                            string[] val;

    //                            opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
    //                            val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

    //                            int itemindex = 0;
    //                            for (int j = 0; j < opt.Length; j++)
    //                            {
    //                                for (int k = 0; k < val.Length; k++)
    //                                {
    //                                    if (j == k)
    //                                    {
    //                                        RadioButtonList lst;
    //                                        lst = new RadioButtonList();

    //                                        rblTeacher.Items.Add(opt[j]);
    //                                        rblTeacher.SelectedIndex = itemindex;
    //                                        rblTeacher.SelectedItem.Value = val[j];

    //                                        itemindex++;
    //                                        break;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        rblTeacher.SelectedIndex = -1;
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            lvTeacher.Items.Clear();
    //            lblteacher.Visible = false;
    //            lvTeacher.DataSource = null;
    //            lvTeacher.DataBind();
    //        }
    //    }


    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillTeacherQuestion()-> " + ex.Message + "" + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


    //to load all question for particular selected course
    //protected void btnClear_Click(object sender, EventArgs e)
    //{
    //    //Response.Redirect(Request.Url.ToString());
    //}
    protected void ddlFeedbackTyp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillquestion();
    }

    public void fillquestion()
    {
        pnlFeedback.Visible = false;

        if (ddlFeedbackTyp.SelectedIndex > 0)
        {
            Panel1.Visible = false;
            pnlSubject.Visible = false;
            lblcrse.Visible = false;
            lvSelected.DataSource = null;
            lvSelected.DataBind();
            int mode = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "MODE_ID", "FEEDBACK_NO=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue)));
            ViewState["MODE"] = mode;
            if (mode == 1)
            {
                Panel1.Visible = false;
                FillQuestion(1, Convert.ToInt32(lblSemester.ToolTip));
                //if (Session["OrgId"].ToString() == "2")
                //{
                //    if (ddlFeedbackTyp.SelectedValue == "1" || ddlFeedbackTyp.SelectedValue == "2")
                //    {
                //        lblWhatOtherChanges.Text = "Any other suggestions:";
                //        divfeedback.Visible = false;
                //        lblAnyComments.Text = string.Empty;
                //    }
                //    else
                //    {
                //        lblWhatOtherChanges.Text = "Mention the topics to be removed from the syllabus because they are not the prerequisite / relevant / contemporary / required for employment / correlating with course outcomes, etc.";
                //        divfeedback.Visible = true;
                //        lblAnyComments.Text = "Mention the topics to be included in the syllabus because they are prerequisite / relevant / contemporary / required for employment / correlating with course outcomes etc.";
                //    }
                //}
                //else
                //{
                //    lblWhatOtherChanges.Text = "any additional comments (write briefly)?";
                //    lblAnyComments.Text = "any additional comments (write briefly)?";
                //}
            }
            else if (mode == 2)
            {
                FillCourseList();
                ClearControls();
                lvSelected.Visible = true;
            }
            else if (mode == 3)
            {
                Panel1.Visible = false;
                pnlSubject.Visible = false;
                lblcrse.Visible = false;
                lvSelected.DataSource = null;
                lvSelected.DataBind();
                FillQuestion(1, Convert.ToInt32(lblSemester.ToolTip));
                //if (Session["OrgId"].ToString() == "2")
                //{
                //    if (ddlFeedbackTyp.SelectedValue == "1" || ddlFeedbackTyp.SelectedValue == "2")
                //    {
                //        lblWhatOtherChanges.Text = "Any other suggestions:";
                //        divfeedback.Visible = false;
                //        lblAnyComments.Text = string.Empty;
                //    }
                //    else
                //    {
                //        lblWhatOtherChanges.Text = "Mention the topics to be removed from the syllabus because they are not the prerequisite / relevant / contemporary / required for employment / correlating with course outcomes, etc.";
                //        divfeedback.Visible = true;
                //        lblAnyComments.Text = "Mention the topics to be included in the syllabus because they are prerequisite / relevant / contemporary / required for employment / correlating with course outcomes etc.";
                //    }
                //}
                //else
                //{
                //    lblWhatOtherChanges.Text = "any additional comments (write briefly)?";
                //    lblAnyComments.Text = "any additional comments (write briefly)?";
                //}
            }
            else
            {

            }

        }
        else
        {
            lvSelected.DataSource = null;
            lvSelected.DataBind();
            ClearControls();
            lvSelected.Visible = false;
        }
    }

    private void FillQuestion(int SubID, int semesterno)
    {
        CourseController objCC = new CourseController();
        SqlDataReader dr = null;
        if (Session["usertype"].ToString() == "2")
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            dr = objCC.GetSchemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
        }
        if (dr != null)
        {
            if (dr.Read())
            {
                int sessionno = 0;
                lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();

                lblSession.ToolTip = Session["sessionno"].ToString();
                lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", " SESSIONNO = " + Convert.ToInt32(Session["sessionno"]));
                lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
            }
        }



        objSEB.CTID = Convert.ToInt32(ddlFeedbackTyp.SelectedValue);
        objSEB.SubId = SubID;
        objSEB.SemesterNo = semesterno;
        try
        {
            string count = "0";
            count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + "AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND CTID=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue));
            if (Convert.ToInt32(count) > 0)//entry already done
            {
                string date = "";
                date = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "distinct Convert(varchar(10),DATE,103)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND CTID=" + Convert.ToInt32(ddlFeedbackTyp.SelectedValue));

                if (date != "")
                {
                    lblMsg.Text = "FeedBack is already completed for " + ddlFeedbackTyp.SelectedItem.Text + " on DATE " + date; ;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                    pnlFeedback.Visible = false;
                }
            }
            else if (Convert.ToInt32(count) == 0)//new entry
            {
                lblMsg.Text = "";
                lblMsg.Visible = false;
                DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB);
                // DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB, SubID);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //lblcrse.Visible = true;
                    lvCourse.DataSource = ds;
                    lvCourse.DataBind();
                    pnlFeedback.Visible = true;
                    foreach (ListViewDataItem dataitem in lvCourse.Items)
                    {
                        RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
                        HiddenField hdnCourse = dataitem.FindControl("hdnCourse") as HiddenField;
                        HiddenField hfOPTION_TYPE = dataitem.FindControl("OPTION_TYPE") as HiddenField;
                        TextBox txtCourse = dataitem.FindControl("txtcourse") as TextBox;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(hdnCourse.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
                            {
                                string ansOptions = ds.Tables[0].Rows[i]["ANS_OPTIONS"].ToString();
                                string ansValue = ds.Tables[0].Rows[i]["ANS_VALUE"].ToString();

                                if (ansOptions.Contains(","))
                                {
                                    string[] opt;
                                    string[] val;

                                    opt = ansOptions.Split(new[] { "," }, StringSplitOptions.None);
                                    val = ansValue.Split(new[] { "," }, StringSplitOptions.None);

                                    int itemindex = 0;
                                    for (int j = 0; j < opt.Length; j++)
                                    {
                                        for (int k = 0; k < val.Length; k++)
                                        {
                                            if (j == k)
                                            {

                                                RadioButtonList lst;
                                                lst = new RadioButtonList();

                                                rblCourse.Items.Add(opt[j]);
                                                rblCourse.SelectedIndex = itemindex;
                                                rblCourse.SelectedItem.Value = val[j];

                                                itemindex++;
                                                break;
                                            }
                                        }
                                    }
                                }
                                rblCourse.SelectedIndex = -1;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    pnlFeedback.Visible = false;
                    lvCourse.Items.Clear();
                    lblcrse.Visible = false;
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    objCommon.DisplayMessage("No feedback questions found for this semester course.", this.Page);
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.fillCourseQuestion()-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds;
        ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO,COLLEGE_ID,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            collegeid = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            ViewState["COLLEGE_ID"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            //objCommon.FillDropDownList(ddlSession, "ACD_FEEDBACK_ACTIVITY FA INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=FA.SESSION_NO)", "DISTINCT SESSION_NO", " SESSION_NAME", "STARTED=1 AND SHOW_STATUS=1 AND DEGREENO=" + Degreeno + " AND BRANCHNO=" + branchno + " AND SEMESTERNO=" + Semesterno + " AND FA.COLLEGE_ID=" + collegeid, "SESSION_NO");
            //CheckActivity();

            ActivityController objActController = new ActivityController();
            DataSet dssession = objActController.FillFeedbackSession(Convert.ToInt32(Degreeno), Convert.ToInt32(branchno), Convert.ToInt32(Semesterno), Convert.ToInt32(collegeid));
            if (dssession != null && dssession.Tables.Count > 0 && dssession.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                
                objCommon.DisplayMessage("This Activity has been Stopped.", this.Page);
                divfeedbacktype.Visible = true;
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
                divfeedbacktype.Visible = false;
                lvSelected.Visible = false;
                lvSelected.DataSource = null;
                lvSelected.DataBind();
                ClearControls();
                return;
            }

        }
        else
        {
            objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "rd sem.Please Contact Admin.!!", this.Page);
            pnlStudInfo.Visible = false;
            return;
        }
        if (ddlSession.SelectedIndex > 0)
        {
           
            divfeedbacktype.Visible = true;
            ActivityController objActController = new ActivityController();
            //objCommon.FillDropDownList(ddlFeedbackTyp, "ACD_FEEDBACK_ACTIVITY A INNER JOIN ACD_FEEDBACK_MASTER M ON (A.FEEDBACK_TYPENO=M.FEEDBACK_NO)", "FEEDBACK_TYPENO", "FEEDBACK_NAME", "FEEDBACK_TYPENO>0 AND STARTED=1 AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"]) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]) + "AND COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "AND SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "AND SESSION_NO=" + Convert.ToInt32(ddlSession.SelectedValue), "FEEDBACK_TYPENO");
            DataSet dsfeedback = objActController.FillFeedbackType(Convert.ToInt32(Degreeno), Convert.ToInt32(branchno), Convert.ToInt32(Semesterno), Convert.ToInt32(collegeid),Convert.ToInt32( ddlSession.SelectedValue));
            Session["sessionno"] = ddlSession.SelectedValue;
            ddlFeedbackTyp.Items.Clear();
            ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
            if (dsfeedback != null && dsfeedback.Tables.Count > 0 && dsfeedback.Tables[0].Rows.Count > 0)
            {

                ddlFeedbackTyp.DataSource = dsfeedback;
                ddlFeedbackTyp.DataValueField = dsfeedback.Tables[0].Columns[0].ToString();
                ddlFeedbackTyp.DataTextField = dsfeedback.Tables[0].Columns[1].ToString();
                ddlFeedbackTyp.DataBind();
            }
            lvSelected.Visible = false;
            lvSelected.DataSource = null;
            lvSelected.DataBind();
            ClearControls();

        }
        else
        {
            lvSelected.DataSource = null;
            lvSelected.DataBind();
            ClearControls();
            lvSelected.Visible = false;
            ddlFeedbackTyp.Items.Clear();
            ddlFeedbackTyp.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}