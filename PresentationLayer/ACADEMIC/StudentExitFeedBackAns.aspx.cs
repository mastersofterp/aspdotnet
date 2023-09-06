//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : StudentFeedBackAns.aspx                                               
// CREATION DATE : 25-05-2012                                                   
// CREATED BY    : Pawan Mourya                               
// MODIFIED BY   : 

// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;



public partial class ACADEMIC_StudentExitFeedBackAns : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    StudentFeedBack objSEB = new StudentFeedBack();
    string Semesterno = string.Empty;
    string Degreeno = string.Empty;
    string branchno = string.Empty;
    string sessionno = string.Empty;

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
                this.CheckPageAuthorization();
                //Check for Activity On/Off
                if (Session["usertype"].ToString() == "2")
                {
                    GetStudentDeatlsForEligibilty();
                }
                if (CheckActivity() == false)
                    return;

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                if (Session["usertype"].ToString() == "2")
                {
                    pnlStudInfo.Visible = true;
                    FillLabel();
                    string count = "-1";
                    ViewState["FEEDBACK_NO"] = Convert.ToInt32(objCommon.LookUp("ACD_FEEDBACK_MASTER", "FEEDBACK_NO", "FEEDBACK_NAME like '%Exit%'"));
                    //to check feedback is already done or not
                    count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND isnull(COURSENO,0)=0 AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND isnull(UA_NO,0)=0 AND CTID=" + Convert.ToInt32(ViewState["FEEDBACK_NO"]));
                    string date = "";
                    date = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "distinct Convert(varchar(10),DATE,103)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND isnull(COURSENO,0)=0 AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND isnull(UA_NO,0)=0 AND CTID=" + Convert.ToInt32(ViewState["FEEDBACK_NO"]));
                     
                        if (Convert.ToInt32(count) > 0)//entry already done
                        {
                            lblMsg.Text = "FeedBack is already COMPLETED for " + lblName.Text + " on DATE " + date;
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            lblMsg.Visible = true;
                            pnlFeedback.Visible = false;
                        }
                        else if (Convert.ToInt32(count) == 0)//new entry
                        {
                            lblMsg.Text = "";
                            lblMsg.Visible = false;
                            FillExitFeedbackQuestion();
                            //FillTeacherQuestion();
                            pnlFeedback.Visible = true;
                        }
                }
                else
                {
                    pnlSearch.Visible = true;
                    pnlStudInfo.Visible = false;
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    protected void GetStudentDeatlsForEligibilty()
    {
        try
        {
            DataSet ds;
            ds = objCommon.FillDropDown("ACD_STUDENT", "DEGREENO,BRANCHNO,SEMESTERNO", "STUDNAME", "IDNO=" + Convert.ToInt32(Session["idno"]), "IDNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                Semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
               
            }
            else
            {
                objCommon.DisplayMessage("This Activity has not been Started for" + Semesterno + "th sem.Please Contact Admin.!!", this.Page);
                pnlStudInfo.Visible = false;
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentExitFeedbackAns.GetStudentDeatlsForEligibilty() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private bool CheckActivity()
    {
        try
        {
            int programtype =Convert.ToInt32(objCommon.LookUp("ACD_DEGREE", "DEGREETYPEID", "DEGREENO= " + ViewState["DEGREENO"].ToString() + ""));
            if (programtype == 1)//ug semester 8
            {
                sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND AM.ACTIVITY_CODE='EXITFEED' AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'  AND (" + ViewState["SEMESTERNO"].ToString() + ") IN  (8) AND (8) IN  (SELECT VALUE FROM DBO.SPLIT(SA.SEMESTER,',')) )");
            }
            else //pg semester 4
            {
                sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND AM.ACTIVITY_CODE='EXITFEED' AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'  AND (" + ViewState["SEMESTERNO"].ToString() + ") IN  (4) AND (4) IN  (SELECT VALUE FROM DBO.SPLIT(SA.SEMESTER,',')) )");
            }

            //sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND AM.ACTIVITY_CODE='EXITFEED' AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'  AND (" + ViewState["SEMESTERNO"].ToString() + ") IN  (SELECT VALUE FROM DBO.SPLIT(SA.SEMESTER,',')))");
            //sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND AM.ACTIVITY_CODE='EXITFEED' AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
            if (sessionno == string.Empty)
            {
                sessionno = "0";
            }

            Session["sessionno"] = sessionno;

            ActivityController objActController = new ActivityController();
            DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
            if (dtr.Read())
            {
                if (dtr["STARTED"].ToString().ToLower().Equals("false"))
                {
                    objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                    pnlSearch.Visible = false;
                    pnlStudInfo.Visible = false;
                }
                if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
                {
                    objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                    pnlSearch.Visible = false;
                    pnlStudInfo.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                pnlSearch.Visible = false;
                pnlStudInfo.Visible = false;
                return false;
            }

            dtr.Close();
            return true;
        }

        catch (Exception ex)
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlSearch.Visible = false;
            pnlStudInfo.Visible = false;
            return false;
            //if (Convert.ToBoolean(Session["error"]) == true)
            //{
            //    objCommon.ShowError(Page, "ACADEMIC_StudentExitFeedbackAns.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
            //    return false;
            //}
            //else
            //{
            //    objCommon.ShowError(Page, "Server Unavailable.");
            //    return false;
            //}
        }
               
    }
     
    //private void CheckActivity()
    //{

    //    try
    //    {
    //        string sessionno = string.Empty;
    //        sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT ISNULL(SESSIONNO,0)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE  STARTED = 1 AND  SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')");
    //        ActivityController objActController = new ActivityController();
    //        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(Degreeno), Convert.ToString(branchno), Convert.ToString(Semesterno));

    //        if (dtr.Read())
    //        {
    //            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
    //            {
    //                objCommon.DisplayMessage(this.updDetails, "This Activity has been Stopped. Contact Admin.!!", this.Page);
    //                pnlStart.Visible = false;

    //            }
    //            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //            {
    //                objCommon.DisplayMessage(this.updDetails, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //                pnlStart.Visible = false;
    //            }

    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updDetails, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
    //            pnlStart.Visible = false;
    //        }
    //        dtr.Close();
    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "ACADEMIC_BacklogExamregEndSem.CheckActivity() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }


    //}


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
                int sessionno = 0;
                lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
                lblSession.ToolTip = Session["sessionno"].ToString();
                lblSession.Text = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", " SESSIONNO = " + Convert.ToInt32(Session["sessionno"]));
                lblScheme.Text = dr["SCHEMENAME"] == null ? string.Empty : dr["SCHEMENAME"].ToString();
                lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"] == null ? string.Empty : dr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dr["SEMESTERNO"] == null ? string.Empty : dr["SEMESTERNO"].ToString();
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=STUDENT";
            }
        }
        if (dr != null) dr.Close();

        //if (lblScheme.ToolTip != "")
        //{
        //    int idno = 0;
        //    idno = Session["usertype"].ToString() == "2" ? Convert.ToInt32(Session["idno"]) : Convert.ToInt32(ViewState["Id"]);
        //    DataSet ds = objSFBC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), idno, Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip));
        //    //lvSelected.DataSource = ds;
        //    //lvSelected.DataBind();
        //    this.CheckSubjectAssign();
        //}
    }

    //public string GetCourseName(object coursename, object TeachName)
    //{
    //    return coursename.ToString() + " [<span style='color:Green;font-weight: bold;'>" + TeachName.ToString() + "</span>]";
    //}

    //private void CheckSubjectAssign()
    //{
    //    SqlDataReader dr = null;
    //    foreach (ListViewDataItem item in lvSelected.Items)
    //    {
    //        Label lblComplete = item.FindControl("lblComplete") as Label;
    //        lblComplete.ForeColor = System.Drawing.Color.Green;
    //        lblComplete.Text = "Incomplete";
    //    }

    //    if (Session["usertype"].ToString() == "2")
    //        dr = objSFBC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["idno"]));
    //    else
    //        dr = objSFBC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(ViewState["Id"]));
    //    if (dr != null)
    //    {
    //        while (dr.Read())
    //        {
    //            foreach (ListViewDataItem item in lvSelected.Items)
    //            {
    //                LinkButton lnkCourse = item.FindControl("lnkbtnCourse") as LinkButton;
    //                Label lblComplete = item.FindControl("lblComplete") as Label;

    //                if (Convert.ToInt32(lnkCourse.CommandArgument) == Convert.ToInt32(dr["COURSENO"].ToString()) && lnkCourse.ToolTip == dr["UA_NO"].ToString())
    //                {
    //                    lblComplete.ForeColor = System.Drawing.Color.Red;
    //                    lblComplete.Text = "Complete";
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    if (dr != null) dr.Close();
    //}


    //private void FillTeacherQuestion()
    //{
    //    objSEB.CTID = 4;
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
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillTeacherQuestion-> " + ex.Message + "" + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void FillExitFeedbackQuestion()
    {
        lblExitFeedback.Visible = true;
        lblExitFeedback.Text = "Exit Feedback Questions : ";
        objSEB.CTID =  Convert.ToInt32(ViewState["FEEDBACK_NO"]);
        try
        {
            DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB, 0);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblExitFeedback.Visible = true;
                lvExitFeedback.DataSource = ds;
                lvExitFeedback.DataBind();

                foreach (ListViewDataItem dataitem in lvExitFeedback.Items)
                {
                    RadioButtonList rblExitFeedback = dataitem.FindControl("rblExitFeedback") as RadioButtonList;
                    HiddenField hdnExitFeedback = dataitem.FindControl("hdnExitFeedback") as HiddenField;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(hdnExitFeedback.Value) == Convert.ToInt32(ds.Tables[0].Rows[i]["QUESTIONID"]))
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

                                            rblExitFeedback.Items.Add(opt[j]);
                                            rblExitFeedback.SelectedIndex = itemindex;
                                            rblExitFeedback.SelectedItem.Value = val[j];

                                            itemindex++;
                                            break;
                                        }
                                    }
                                }
                            }
                            rblExitFeedback.SelectedIndex = -1;
                            break;
                        }
                    }
                }
            }
            else
            {
                lvExitFeedback.Items.Clear();
                lblExitFeedback.Visible = false;
                lvExitFeedback.DataSource = null;
                lvExitFeedback.DataBind();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentExitFeedBackAns.FillExitFeedbackQuestion()-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentExitFeedBackAns.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentExitFeedBackAns.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() == "2")
            {
                foreach (ListViewDataItem dataitem in lvExitFeedback.Items)
                {
                    RadioButtonList rblExitFeedback = dataitem.FindControl("rblExitFeedback") as RadioButtonList;
                    Label lblExitFeedbackQuestions = dataitem.FindControl("lblExitFeedbackQuestions") as Label;
                    if (rblExitFeedback.SelectedValue == "")
                    {
                        objCommon.DisplayMessage("You must have answer all the questions", this.Page);
                        return;
                    }
                    else
                    {
                        objSEB.AnswerIds += rblExitFeedback.SelectedValue + ",";
                        objSEB.QuestionIds += lblExitFeedbackQuestions.Text + ",";
                    }
                }
                //foreach (ListViewDataItem dataitem in lvTeacher.Items)
                //{
                //    RadioButtonList rblTeacher = dataitem.FindControl("rblTeacher") as RadioButtonList;
                //    Label lblTeacher = dataitem.FindControl("lblTeacher") as Label;
                //    if (rblTeacher.SelectedValue == "")
                //    {
                //        objCommon.DisplayMessage("You must have answer all the questions", this.Page);
                //        return;
                //    }
                //    else
                //    {
                //        objSEB.AnswerIds += rblTeacher.SelectedValue + ",";
                //        objSEB.QuestionIds += lblTeacher.Text + ",";
                //    }
                //}


                objSEB.AnswerIds = objSEB.AnswerIds.TrimEnd(',');
                objSEB.QuestionIds = objSEB.QuestionIds.TrimEnd(',');

                //this.FillAnswers();

                int retFlag = this.FillAnswers();

                if (retFlag == 1)
                {
                    objCommon.DisplayMessage("Your FeedBack Saved Successfully !", this.Page);
                    txtLikeToConvey.Text = "";
                    txtAnyComments.Text = "";
                    txtFromYourDepartment.Text = "";
                    txtFromOtherDepartment.Text = "";
                }
                else
                {
                    objCommon.DisplayMessage("Something Went Wrong !", this.Page);
                }

               // this.CheckSubjectAssign();

                this.ClearControls();
            }
            else
            {
                objCommon.DisplayMessage("Only Students fills this form!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentExitFeedBackAns.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControls()
    {
        lblMsg.Text = string.Empty;
        txtRemark.Text = string.Empty;
        pnlFeedback.Visible = false;
        txtLikeToConvey.Text = "";
        txtAnyComments.Text = "";
        txtFromYourDepartment.Text = "";
        txtFromOtherDepartment.Text = "";
    }

    private int FillAnswers()
    {
        objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
        objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
        //objSEB.Date = DateTime.Now;
        objSEB.CollegeCode = Session["colcode"].ToString();
        objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
        objSEB.CourseNo = 0;
        objSEB.UA_NO = 0;
        objSEB.FB_Status = true;
        objSEB.OverallImpression = "0";
        objSEB.Suggestion_A = lblLikeToConvey.Text;
        objSEB.Suggestion_B = txtLikeToConvey.Text;
        objSEB.Suggestion_C = lblAnyComments.Text;
        objSEB.Suggestion_D = txtAnyComments.Text;

        objSEB.CTID =  Convert.ToInt32(ViewState["FEEDBACK_NO"]);
        objSEB.ExitQuestionBestTeacher = lblNameTheBestTeachers.Text;
        objSEB.FromDepartment = txtFromYourDepartment.Text;
        objSEB.OtherDepartment = txtFromOtherDepartment.Text;


        if (!txtRemark.Text.Equals(string.Empty)) objSEB.Remark = txtRemark.Text.ToString();
        int ret = objSFBC.AddStudentFeedBackAns(objSEB);
        return ret;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString()));
        if (Convert.ToInt32(count) != 0)
            ShowReport("Student_FeedBack", "StudentFeedBackAns.rpt");
        else
            objCommon.DisplayMessage("Record Not Found", this.Page);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblName.ToolTip) + ",@P_SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"].ToString());
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentExitFeedBackAns.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "DISTINCT IDNO", "REGNO='" + txtSearch.Text.Trim() + "'");
        if (idno != "")
        {
            ViewState["Id"] = Convert.ToInt32(idno);
            FillLabel();
            pnlStudInfo.Visible = true;
            btnClear.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage("Record Not Found", this.Page);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //protected void lnkbtnCourse_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnk = sender as LinkButton;
    //        lblExitFeedback.Text = lnk.Text;
    //        //lblteacher.Text = lnk.Text;
    //        ViewState["TeachNo"] = lnk.ToolTip;
    //        ViewState["COURSENO"] = lnk.CommandArgument;
    //        ViewState["SubId"] = 0;
    //        string count = "-1";

    //        if (lnk.ToolTip == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this, "No faculty is assigned to the selected Course!!!", this.Page);
    //            return;
    //        }
    //        else
    //        {
    //            count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + Convert.ToInt32(lnk.CommandArgument) + "AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "AND UA_NO=" + Convert.ToInt32(lnk.ToolTip));
    //        }

    //        if (Convert.ToInt32(count) > 0)//entry already done
    //        {
    //            lblMsg.Text = "FeedBack is already completed for " + lnk.Text;
    //            lblMsg.ForeColor = System.Drawing.Color.Red;
    //            lblMsg.Visible = true;
    //            pnlFeedback.Visible = false;
    //        }
    //        else if (Convert.ToInt32(count) == 0)//new entry
    //        {
    //            lblMsg.Text = "";
    //            lblMsg.Visible = false;
    //            FillCourseQuestion(Convert.ToInt16(ViewState["SubId"]));
    //            //FillTeacherQuestion();
    //            pnlFeedback.Visible = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
}
