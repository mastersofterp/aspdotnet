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
//using System.Transactions;
using System.Text;
using System.Data.SqlClient;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Text.RegularExpressions;
using System.Globalization;
//using System.Windows.Forms;

public partial class ITLE_TestMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITestMasterController objITestMaster = new ITestMasterController();
    ITestMaster objTest = new ITestMaster();

    IQuestionbankController objIQBC = new IQuestionbankController();
    IQuestionbank objQuest = new IQuestionbank();
    static string strTopItmes = string.Empty, strQuestionRation = string.Empty, strMarks = string.Empty;
    TextBox txtRatios;
    TextBox txtMarks;
    TextBox txtQuestionMarks;
    TextBox txtQuestionCount;
    static int totalmarks = 0;
    static int Totmarks = 0;
    char ShowRandom;
    string questNo = string.Empty;

    #region Page Load

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }

                //Page Authorization
                if (Session["Page"] == null)
                {
                    CheckPageAuthorization();
                    Session["Page"] = 1;
                }
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help

                lblCourseName.Text = Session["ICourseName"].ToString();
                pnllvView.Visible = true;
                pnlAdd.Visible = false;
                BindListView();
                BindStudList();
                //BindQuestionsListView();
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                divMalfunction.Visible = false;
                divpassword.Visible = false;
                txtMalFunction.Text = "0";
                Validate();
                TopicValidation();
                ViewState["action"] = null;
                if (hdnTo.Value.ToString().Trim() != "")
                {
                    lblTotal.Text = hdnTo.Value;
                }
            }
        }

    }

    #endregion

    #region Private Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TestMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TestMaster.aspx");
        }
    }

    private void TopicValidation()
    {
        foreach (ListViewItem lvi in lsvbindquestion.Items)
        {
            txtRatios = lvi.FindControl("txtSelectQ") as TextBox;
            txtQuestionCount = lvi.FindControl("TextBox2") as TextBox;
            txtRatios.Attributes.Add("OnBlur", "validateNumericBlanckCheck(" + txtRatios.ClientID + "," + txtQuestionCount.ClientID + ")");

        }
    }

    private void Validate()
    {
        if (Convert.ToInt32(Session["usertype"]) == 1)
        {
            cbRandom.Enabled = true;
            cbShowResult.Enabled = true;
            txtAttempts.Enabled = true;
        }
        else if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            cbRandom.Enabled = false;
            cbShowResult.Enabled = true;
            txtAttempts.Enabled = true;
        }
        else
        {
            cbRandom.Enabled = false;
            cbShowResult.Enabled = false;
            txtAttempts.Enabled = false;
        }
    }

    private void FillDropdown()
    {
        try
        {
            SQLHelper slp = new SQLHelper(objCommon._client_constr);
            //by Zubair
            //DataSet ds = slp.ExecuteDataSet("select TOPIC,COUNT(COURSENO) as Questions,0 AS QuestionRatio,ISNULL(MARKS_FOR_QUESTION,0) AS MARKS_FOR_QUESTION from ACD_IQUESTIONBANK where COURSENO=" + Session["ICourseNo"] + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "' group by topic,MARKS_FOR_QUESTION ORDER BY TOPIC");
            DataSet ds = slp.ExecuteDataSet("select TOPIC,COUNT(COURSENO) as Questions,0 AS QuestionRatio,(CASE WHEN QUESTION_TYPE = 'D' THEN ISNULL(MARKS_FOR_QUESTION,0) ELSE 1   END) AS MARKS_FOR_QUESTION from ACD_IQUESTIONBANK where COURSENO=" + Session["ICourseNo"] + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "' group by topic,MARKS_FOR_QUESTION,QUESTION_TYPE ORDER BY TOPIC");
            lsvbindquestion.DataSource = ds;
            lsvbindquestion.DataBind();

            objCommon.FillDropDownList(ddlSection, "ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=S.IDNO) INNER JOIN ACD_SECTION SEC ON (S.SECTIONNO = SEC.SECTIONNO)", "DISTINCT SEC.SECTIONNO AS SECTIONNO", "SEC.SECTIONNAME AS SECTIONNAME", "R.COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND R.SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND (R.UA_NO =" + Convert.ToInt32(Session["userno"]) + "OR R.UA_NO_PRAC=" + Convert.ToInt32(Session["userno"]) + "OR R.UA_NO_TUTR=" + Convert.ToInt32(Session["userno"]) + "OR AD_TEACHER_PR=" + Convert.ToInt32(Session["userno"]) + "OR AD_TEACHER_TH=" + Convert.ToInt32(Session["userno"]) + ")", "");

            //objCommon.FillDropDownList(ddlTopic, "ACD_IQUESTIONBANK", "TOPIC", "TOPIC", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "", "");

            #region comnetcode
            //objCommon.FillListBox(ddlTopic, "itle_questionbank", "distinct topicno", "topicno", "SDSRNO=" + Session["ICourseNo"] + " AND topicno <> ''", "");
            //DataSet ds = objCommon.FillDropDown("ITLE_QUESTIONBANK", "QUESTIONNO,QUESTIONTEXT ,1 AS TOTAL, TOPICNO", "1 AS CORRECT_MARKS", "SDSRNO = " + Course, "QUESTIONNO");
            //DataSet ds = objCommon.FillDropDown("ITLE_QUESTIONBANK", "DISTINCT TOPICNO", "0 AS QuestionRatio", "SDSRNO=" + Session["ICourseNo"] + " AND topicno <> ''", "TOPICNO");
            //DataSet ds = objCommon.FillDropDown("ITLE_QUESTIONBANK", "TOPICNO,COUNT(sdsrno) as Questions", "0 AS QuestionRatio", "SDSRNO=" + Session["ICourseNo"] + " AND topicno <> ''", "TOPICNO group by topicno");
            #endregion

        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(updCreateQuestion, "ITLE_TestMasterNew.aspx.FillDropdown->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void BindListView()
    {
        try
        {
            int COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            int UA_NO = Convert.ToInt32(Session["userno"]);
            DataSet ds = objITestMaster.GetAllTest(COURSENO, UA_NO);
            lvTest.DataSource = ds;
            lvTest.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TestMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindStudList()
    {
        if (Convert.ToInt32(ddlSection.SelectedValue) != 0)  // GAYATRI RODE CHANGE REGNO INSTID OF ROLLNO 05-07-2022
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=S.IDNO) INNER JOIN ACD_SECTION SEC ON (S.SECTIONNO = SEC.SECTIONNO)", "DISTINCT S.STUDNAME,SEC.SECTIONNAME,(CASE WHEN S.STUDENTMOBILE IS NULL THEN 'N/A' ELSE S.STUDENTMOBILE END ) AS STUDENTMOBILE,(CASE WHEN S.EMAILID IS NULL THEN 'N/A' ELSE S.EMAILID END ) AS  EMAILID,S.REGNO", "S.IDNO", "R.COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND R.SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND (R.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR R.UA_NO_PRAC = " + Convert.ToInt32(Session["userno"]) + " OR R.UA_NO_TUTR=" + Convert.ToInt32(Session["userno"]) + " OR R.AD_TEACHER_PR LIKE '%" + Session["userno"].ToString() + "%' OR R.AD_TEACHER_TH LIKE '%" + Session["userno"].ToString() + "%') AND S.SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + "AND isnull(CANCEL,0)=0 AND REGISTERED=1", "SECTIONNAME,S.REGNO");

            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                CheckBox chk = lvitem.FindControl("chkStud") as CheckBox;
                chk.Checked = true;
            }
        }
        else
        {  // GAYATRI RODE CHANGE REGNO INSTID OF ROLLNO  05-07-2022
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=S.IDNO) INNER JOIN ACD_SECTION SEC ON (S.SECTIONNO = SEC.SECTIONNO)", "DISTINCT S.STUDNAME,SEC.SECTIONNAME,(CASE WHEN S.STUDENTMOBILE IS NULL THEN 'N/A' ELSE S.STUDENTMOBILE END ) AS STUDENTMOBILE,(CASE WHEN S.EMAILID IS NULL THEN 'N/A' ELSE S.EMAILID END ) AS  EMAILID,S.REGNO", "S.IDNO", "R.COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND R.SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + " AND (R.UA_NO =" + Convert.ToInt32(Session["userno"]) + " OR R.UA_NO_PRAC = " + Convert.ToInt32(Session["userno"]) + " OR R.UA_NO_TUTR=" + Convert.ToInt32(Session["userno"]) + " OR R.AD_TEACHER_PR LIKE '%" + Session["userno"].ToString() + "%' OR R.AD_TEACHER_TH LIKE '%" + Session["userno"].ToString() + "%')AND isnull(CANCEL,0)=0 AND REGISTERED=1", "SECTIONNAME,S.REGNO");

            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                CheckBox chk = lvitem.FindControl("chkStud") as CheckBox;
                chk.Checked = true;
            }
        }
    }

    private void ClearControls()
    {
        txtTestName.Text = string.Empty;
        // ddlCourseName.SelectedIndex = -1;
        txtStartDt.Text = string.Empty;
        txtEndDt.Text = string.Empty;
        txtTestDuration.Text = string.Empty;
        //txtMarksRightAns.Text = string.Empty;
        //txtMarksWrongAns.Text = string.Empty;
        //cbRandom.Checked = false;
        //ddlTopic.ClearSelection();
        txtTotQue.Text = string.Empty;
        txtTotMarks.Text = string.Empty;
        txtStartDt.ReadOnly = false;
        TxtExamStartTime.Text = string.Empty;
        TxtExamEndTime.Text = string.Empty;
        ViewState["action"] = null;
        Session["QUESTION_SET"] = null;
        cbShowResult.Checked = false;
        rbtnRandomQuestion.SelectedValue = "R";
        chkFullRandomeze.Checked = false;
        divMalfunction.Visible = false;
        txtMalFunction.Text = string.Empty;
        rdoAllowMalPactice.SelectedValue = "0";
        DataSet ds = null;
        lvSelectedQuestions.DataSource = ds;
        lvSelectedQuestions.DataBind();
        lvQuestions.DataSource = ds;
        lvQuestions.DataBind();

    }

    private DataRow GetDeletableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QUESTIONNO"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TestMaster.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "AddForum.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void ShowDetail(int Test_no)
    {
        try
        {

            ViewState["Test_no"] = Test_no;
            int Course = Convert.ToInt32(Session["ICourseNo"]);
            objTest.TESTNO = Convert.ToInt32(ViewState["Test_no"]);
            DataSet ds = objITestMaster.GetTestByNo(Test_no, Convert.ToInt32(Session["ICourseNo"]));
            if (ds.Tables[0].Rows.Count != null)
            {

                if (ds.Tables[0].Rows[0]["TEST_TYPE"].ToString() == "D")
                {
                    div_ShowResult.Visible = false;
                   // divapplypass.Visible = false;
                  //  divpassword.Visible = false;
                }
                else
                {
                    div_ShowResult.Visible = true;
                  //  divapplypass.Visible = true;
                }


                Session["TestType"] = ds.Tables[0].Rows[0]["TEST_TYPE"].ToString();
                FillDropdown();
                txtTestName.Text = ds.Tables[0].Rows[0]["TESTNAME"].ToString();
                // lblCourseName.Text = Session["ICourseName"].ToString();
                txtStartDt.Text = ds.Tables[0].Rows[0]["STARTDATE"].ToString();
                txtEndDt.Text = ds.Tables[0].Rows[0]["ENDDATE"].ToString();
                txtTestDuration.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss");
                txtAttempts.Text = ds.Tables[0].Rows[0]["ATTEMPTS"].ToString();
                //   txtMarksRightAns.Text = ds.Tables[0].Rows[0]["MARKSWRIGHTANS"].ToString();
                //   txtMarksWrongAns.Text = ds.Tables[0].Rows[0]["MARKSWRONGANS"].ToString();
                cbRandom.Checked = ds.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim().Equals("Y") ? true : false;
                rbtnRandomQuestion.SelectedValue = ds.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim().Equals("Y") ? "R" : "NR";
               
                string pass = ds.Tables[0].Rows[0]["AllowPassword"].ToString().Trim();


                if (pass == "1")
                {
                    rdoAllowPassword.SelectedValue = "1";
                    divpassword.Visible = true;
                    Txtpassword.Text = ds.Tables[0].Rows[0]["T_PASSWORD"].ToString().Trim();

                }
                else
                {
                    rdoAllowPassword.SelectedValue = "0";
                    divpassword.Visible = false;
                   
                }
                    
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["MalFunctionCount"].ToString()) == 0)
                {
                    divMalfunction.Visible = false;
                    txtMalFunction.Text = "0";
                    rdoAllowMalPactice.SelectedValue = "0";
                }
                else
                {
                    divMalfunction.Visible = true;
                    txtMalFunction.Text = ds.Tables[0].Rows[0]["MalFunctionCount"].ToString();
                    rdoAllowMalPactice.SelectedValue = "1";
                }
                if (rbtnRandomQuestion.SelectedValue == "R")
                {
                    chkFullRandomeze.Checked = ds.Tables[0].Rows[0]["FULL_RANDOME_TEST"].ToString().Trim().Equals("Y") ? true : false;
                    chkFullRandomeze.Enabled = true;

                }
                else
                {
                    chkFullRandomeze.Checked = ds.Tables[0].Rows[0]["FULL_RANDOME_TEST"].ToString().Trim().Equals("Y") ? true : false;
                    chkFullRandomeze.Enabled = false;
                }
                cbShowResult.Checked = ds.Tables[0].Rows[0]["SHOWRESULT"].ToString().Trim().Equals("Y") ? true : false;

                txtTotQue.Text = ds.Tables[0].Rows[0]["TOTQUESTION"].ToString().Trim();
                txtTotMarks.Text = ds.Tables[0].Rows[0]["TOTAL"].ToString().Trim();


                //tarun
                TxtExamStartTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTDATE"].ToString()).ToString("HH:mm:ss");
                TxtExamEndTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString().Trim()).ToString("HH:mm:ss");

                //By Zubair 29-10-2014
                string showRandomStatus = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRANDOM", "TESTNO=" + Test_no);
                if (showRandomStatus.Contains("N"))
                {
                    pnlQuestions.Visible = true;
                    pnlSelectedQuestion.Visible = true;
                    pnlTopic.Visible = false;
                    Uncheck();
                    string selectedQuestions = objCommon.LookUp("ACD_IQUESTION_SET_FOR_TEST", "QUESTION_SET", "TESTNO=" + Test_no);

                    DataSet questionBank = objCommon.FillDropDown("ACD_IQUESTIONBANK", "QUESTIONNO,QUESTIONTEXT ,TOPIC", "(CASE WHEN QUESTION_TYPE='O' THEN 1 ELSE MARKS_FOR_QUESTION END ) AS MARKS_FOR_QUESTION", "COURSENO = " + Session["Icourseno"] + "AND QUESTION_TYPE='" + Session["TestType"] + "'", "TOPIC");
                    if (questionBank.Tables[0].Rows.Count > 0)
                    {
                        lvQuestions.DataSource = questionBank;
                        lvQuestions.DataBind();
                    }
                    else
                    {
                        lvQuestions.DataSource = null;
                        lvQuestions.DataBind();

                    }

                    DataSet questions = objCommon.FillDropDown("ACD_IQUESTIONBANK", "QUESTIONNO,QUESTIONTEXT,(CASE WHEN QUESTION_TYPE='O' THEN 1 ELSE MARKS_FOR_QUESTION END) AS MARKS_FOR_QUESTION", "TOPIC", "QUESTIONNO IN(" + selectedQuestions + ") ", "TOPIC");
                    if (questions.Tables[0].Rows.Count > 0)
                    {

                        lvSelectedQuestions.DataSource = questions;
                        lvSelectedQuestions.DataBind();
                        Session["QUESTION_SET"] = selectedQuestions;

                        for (int i = 0; i < questions.Tables[0].Rows.Count; i++)
                        {
                            foreach (RepeaterItem lsvdata in lvQuestions.Items)
                            {

                                CheckBox chkitem = lsvdata.FindControl("cbQuestionRow") as CheckBox;

                                //LinkButton lnkResendSms = lsvdata.FindControl("lnkResendSms") as LinkButton;

                                if (chkitem.ToolTip.Equals(questions.Tables[0].Rows[i]["QUESTIONNO"].ToString()))
                                {

                                    chkitem.Checked = true;
                                    //lnkResendSms.Text = Convert.ToInt32(ds.Tables[0].Rows[i]["SMS_STATUS"]) == 1 ? "" : "Resend SMS".ToString();

                                }


                            }
                        }
                    }
                    else
                    {
                        lvSelectedQuestions.DataSource = null;
                        lvSelectedQuestions.DataBind();

                    }

                }
                else
                {
                    pnlTopic.Visible = true;

                    strTopItmes = ds.Tables[0].Rows[0]["TOPICS"].ToString();
                    strQuestionRation = ds.Tables[0].Rows[0]["QUESTIONRATIO"].ToString();
                    strMarks = ds.Tables[0].Rows[0]["MARKS_FOR_QUESTION"].ToString();

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TOPICS"].ToString()))
                    {
                        string QRem = ds.Tables[0].Rows[0]["QUESTIONRATIO"].ToString();
                        string Rem = ds.Tables[0].Rows[0]["TOPICS"].ToString();
                        string QuestionMarks = ds.Tables[0].Rows[0]["MARKS_FOR_QUESTION"].ToString();
                        Rem = Rem.Replace("'", "");
                        QRem = QRem.Replace("'", "");
                        QuestionMarks = QuestionMarks.Replace("'", "");

                        string[] values = Rem.Split(',');
                        string[] Qvalues = QRem.Split(',');
                        string[] MValues = QuestionMarks.Split(',');

                        foreach (ListViewItem lstVItem in lsvbindquestion.Items)
                        {
                            txtRatios = lstVItem.FindControl("txtSelectQ") as TextBox;
                            txtQuestionMarks = lstVItem.FindControl("txtMarks") as TextBox;


                            for (int i = 0; i < values.Length; i++)
                            {
                                if (txtRatios.ToolTip == values[i].ToString() && txtQuestionMarks.Text == MValues[i].ToString() && Qvalues[i].ToString() != null)
                                {
                                    txtRatios.Text = Qvalues[i].ToString();
                                    txtQuestionMarks.Text = MValues[i].ToString();
                                    break;
                                }
                            }

                        }
                        //ddlTopic.DataBind();
                    }
                }

                pnlAdd.Visible = true;
                pnllvView.Visible = false;



            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TestMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Uncheck Student List

    protected void UncheckStudList()
    {
        foreach (ListViewDataItem lsvdata in lvStudent.Items)
        {
            CheckBox chkitem = lsvdata.FindControl("chkStud") as CheckBox;
            chkitem.Checked = false;

        }
    }

    protected void Uncheck()
    {
        foreach (RepeaterItem lsvdata in lvQuestions.Items)
        {
            CheckBox chkitem = lsvdata.FindControl("cbQuestionRow") as CheckBox;
            chkitem.Checked = false;

        }
    }

    #endregion

    #region Selected Index Changed

    protected void txtTotQue_TextChanged(object sender, EventArgs e)
    {
        txtTotMarks.Text = txtTotQue.Text;
    }

    protected void rbtnRandomQuestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnRandomQuestion.SelectedValue == "NR")
            {
                pnlTopic.Visible = false;
                pnlQuestions.Visible = true;
                pnlSelectedQuestion.Visible = true;
                txtTotQue.Text = string.Empty;
                txtTotMarks.Text = string.Empty;
                DataSet ds = objCommon.FillDropDown("ACD_IQUESTIONBANK", "QUESTIONNO,QUESTIONTEXT ,TOPIC", "(CASE WHEN QUESTION_TYPE='O' THEN 1 ELSE MARKS_FOR_QUESTION END ) AS MARKS_FOR_QUESTION", "COURSENO = " + Session["Icourseno"] + "AND QUESTION_TYPE='" + Session["TestType"] + "'", "TOPIC");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvQuestions.DataSource = ds;
                    lvQuestions.DataBind();
                }
                else
                {
                    lvQuestions.DataSource = null;
                    lvQuestions.DataBind();

                }
                chkFullRandomeze.Checked = false;
                chkFullRandomeze.Enabled = false;

            }
            else
            {
                pnlTopic.Visible = true;
                pnlQuestions.Visible = false;
                pnlSelectedQuestion.Visible = false;
                chkFullRandomeze.Enabled = true;

                txtTotQue.Text = string.Empty;
                txtTotMarks.Text = string.Empty;
                DataSet ds = null;
                lvSelectedQuestions.DataSource = ds;
                lvSelectedQuestions.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region Page Events

    protected void btnObjectiveTest_Click(object sender, EventArgs e)
    {
        try
        {
            div_ShowResult.Visible = true;
           // divapplypass.Visible = true;
           
            LinkButton btnSelect = sender as LinkButton;
            Session["TestType"] = "O";
            //Session["TestType"] = btnSelect.ToolTip.ToString();
            FillDropdown();

            //Session["ObjectiveType"] = btnObjectiveTest.ToolTip;
            ViewState["action"] = "add";
            //ClearControls();
            int COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            //BindQuestionsListView();         
            DataSet ds = objCommon.FillDropDown("ACD_IQUESTIONBANK", "QUESTIONNO,QUESTIONTEXT ,0 AS TOTAL, TOPIC", "1 AS CORRECT_MARKS", "COURSENO = " + COURSENO, "QUESTIONNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lvQuestions.DataSource = ds;
                //lvQuestions.DataBind();
            }
             else
            {
                lvQuestions.DataSource = null;
                lvQuestions.DataBind();
            }

            pnlAdd.Visible = true;
            pnllvView.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TestMaster.btnAdd_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDescriptiveTest_Click(object sender, EventArgs e)
    {
        try
        {
            //divapplypass.Visible = false;
          //  divpassword.Visible = false;
            div_ShowResult.Visible = false;
          // divswitch.Visible = false;
            //FillListForDescriptive(); 
            LinkButton btnSelect = sender as LinkButton;
            Session["TestType"] = "D";
            //Session["TestType"] = btnSelect.ToolTip.ToString();
            FillDropdown();
            //Session["DescriptiveType"] = btnDescriptiveTest.ToolTip;
            //ddlQuestionMarks.Visible = true;
            //lblText.Visible = true;
            ViewState["action"] = "add";
            //ClearControls();
            int COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            //BindQuestionsListView();          
            DataSet ds = objCommon.FillDropDown("ACD_IQUESTIONBANK", "QUESTIONNO,QUESTIONTEXT ,0 AS TOTAL, TOPIC", "1 AS CORRECT_MARKS", "COURSENO = " + COURSENO, "QUESTIONNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lvQuestions.DataSource = ds;
                //lvQuestions.DataBind();
            }
            else
            {
                lvQuestions.DataSource = null;
                lvQuestions.DataBind();
            }

            pnlAdd.Visible = true;
            pnllvView.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TestMaster.btnAdd_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session["TestType"] = null;
        Response.Redirect("TestMaster.aspx");
    }

    protected void imgAddQuestions_Click(object sender, EventArgs e)
    {
        try
        {
            strTopItmes = string.Empty;
            strQuestionRation = string.Empty;
            strMarks = string.Empty;
            // totalmarks = 0;


            int totalmarks = 0;
            string ErmsgTopic = string.Empty;
            StringBuilder Sbldr = new StringBuilder();

            decimal TotalRatio = 0;
            int Srno = 0;


            if (rbtnRandomQuestion.SelectedValue != "NR")
            {

                foreach (ListViewItem LstItemTopic in lsvbindquestion.Items)
                {
                    Srno++;
                    txtRatios = LstItemTopic.FindControl("txtSelectQ") as TextBox;
                    txtMarks = LstItemTopic.FindControl("txtMarks") as TextBox;
                    if (!string.IsNullOrEmpty(txtRatios.Text.Trim()) && !txtRatios.Text.Trim().Equals("0"))
                    {

                        strTopItmes += "'" + txtRatios.ToolTip.Trim() + "',";
                        strQuestionRation += "'" + txtRatios.Text.Trim() + "',";
                        strMarks += "'" + txtMarks.Text.Trim() + "',";
                    }

                    //DataSet dsCountQ = objCommon.FillDropDown("ACD_IQUESTIONBANK", "count(COURSENO) as COURSENO", "count(COURSENO) as COURSENO1", " TOPIC in ('" + txtRatios.ToolTip.Trim() + "')" + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "'", "");
                    //if (Convert.ToDecimal(txtRatios.Text.Trim()) > Convert.ToDecimal(dsCountQ.Tables[0].Rows[0]["COURSENO"].ToString()))
                    //{
                    //    ErmsgTopic += "[" + Srno + "] =" + txtRatios.Text + ", \\n";

                    //    objCommon.DisplayUserMessage(updCreateQuestion, "Questions are Not Available " + ErmsgTopic, this.Page);
                    //    return;

                    //}

                    if (txtRatios.Text == "")
                    {
                        txtRatios.Text = "0";
                    }


                    totalmarks = totalmarks + Convert.ToInt32(txtRatios.Text) * Convert.ToInt32(txtMarks.Text);
                    TotalRatio += Convert.ToInt32(txtRatios.Text.ToString().Trim());
                }

                if (Session["TestType"].ToString() == "O")
                {

                    txtTotQue.Text = Convert.ToDecimal(TotalRatio).ToString();
                    txtTotMarks.Text = txtTotQue.Text;
                }
                else
                {
                    txtTotQue.Text = Convert.ToDecimal(TotalRatio).ToString();
                    txtTotMarks.Text = Convert.ToInt32(totalmarks).ToString();

                }

                if (ErmsgTopic.Length > 0)
                {
                    //objCommon.DisplayUserMessage(updCreateQuestion,  Sbldr.ToString(), this.Page);
                    ErmsgTopic = ErmsgTopic.Remove(ErmsgTopic.LastIndexOf(','));
                    objCommon.DisplayUserMessage(updCreateQuestion, "Questions are Not Available " + ErmsgTopic, this.Page);
                    return;
                }

            }

            else
            {

                decimal totalMarks = 0;
                if (lvQuestions != null)
                {

                    foreach (RepeaterItem rptitem in lvQuestions.Items)
                    {
                        CheckBox chkSel = rptitem.FindControl("cbQuestionRow") as CheckBox;
                        if (chkSel.Checked)
                        {
                            if (questNo.Equals(string.Empty))
                            {
                                questNo = chkSel.ToolTip;
                            }
                            else
                            {
                                questNo = questNo + "," + chkSel.ToolTip;
                            }
                        }

                    }

                    if (questNo.Equals(string.Empty))
                    {
                        objCommon.DisplayMessage("Please select atleast one Question", this.Page);
                        return;
                    }

                    DataSet ds = new DataSet();
                    ds = objCommon.FillDropDown("ACD_IQUESTIONBANK", "QUESTIONNO,QUESTIONTEXT,(CASE WHEN QUESTION_TYPE='O' THEN 1 ELSE MARKS_FOR_QUESTION END) AS MARKS_FOR_QUESTION", "TOPIC", "QUESTIONNO IN(" + questNo + ") ", "TOPIC");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvSelectedQuestions.DataSource = ds;
                        lvSelectedQuestions.DataBind();
                        Session["QuestionSet"] = ds;

                        if (Session["TestType"].ToString() == "O")
                        {

                            txtTotQue.Text = ds.Tables[0].Rows.Count.ToString();
                            txtTotMarks.Text = txtTotQue.Text;
                        }
                        else
                        {

                            foreach (ListViewDataItem lvMarks in lvSelectedQuestions.Items)
                            {
                                Label lblMarks = lvMarks.FindControl("lblQMarks") as Label;

                                totalMarks += Convert.ToDecimal(lblMarks.Text.ToString());

                            }

                            txtTotQue.Text = ds.Tables[0].Rows.Count.ToString();
                            txtTotMarks.Text = totalMarks.ToString();


                        }

                        Session["QUESTION_SET"] = questNo;
                    }

                }


            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    protected void btnlnkSelect_Click(object sender, EventArgs e)
    {
        try
        {

            Session["post"] = 0;
            LinkButton btnSelect = sender as LinkButton;
            Session["Test_Type"] = (btnSelect.ToolTip.ToString());

            if (Session["Test_Type"].ToString() == "Objective")
            {
                Session["Question_Type"] = "O";
            }
            else
            {
                Session["Question_Type"] = "D";
            }


            int Test_No = Convert.ToInt32(int.Parse(btnSelect.CommandArgument));
            string Test_Name = btnSelect.Text;
            DataSet ds = objCommon.FillDropDown("ACD_ITESTMASTER", "*", "TESTDURATION", "TESTNO=" + Convert.ToInt32(Test_No), "TESTNO");
            string Time = ds.Tables[0].Rows[0]["TESTDURATION"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss");
            Session["Total_Time_For_Test"] = Time;

            int Attempts = Convert.ToInt32(ds.Tables[0].Rows[0]["ATTEMPTS"]);

            DateTime Time1 = Convert.ToDateTime(Time);
            Session["Test_No"] = Test_No;
            Session["TestName"] = Test_Name;
            Session["SDSRNO"] = Session["ICourseNo"];

            //Tarun Comment objAC.Update_ITLE_User(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["ICourseNo"]), Test_No);

            HttpCookie h = new HttpCookie("start");
            Response.Cookies.Clear();
            h.Value = Time;
            Response.Cookies.Add(h);


            int sec = (Time1.Hour * 60 * 60) + (Time1.Minute * 60) + Time1.Second;
            Session["SEC"] = sec * 1000;

            //Session["SEC"] = 70000;



            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("TestMaster")));
            url += "Test_ViewFaculty.aspx?";
            //Script += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes,fullscreen=yes');";
            //Script += " window.open('Test.aspx','PoP_Up','directories=no,toolbar=no,addressbar=no,location=0,titlebar=0,menubar=no,scrollbars=1,statusbar=no,resizable=yes,fullscreen=yes');";
            Script += " window.open('Test_ViewFaculty.aspx','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
            //window.open('Test.aspx','PoP_Up','width=500,height=500,menubar=yes,toolbar=yes,resizable=yes,fullscreen=1');
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
            //Script += " window.open('Test.aspx','_blank','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes,fullscreen=yes');";
            //Response.Redirect("Test.aspx");

        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(updCreateQuestion, "ITLE_TestMasterNew.aspx.btnlnkSelect_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = string.Empty;
            DateTime d1 = DateTime.Parse(TxtExamStartTime.Text);
            DateTime d2 = DateTime.Parse(TxtExamEndTime.Text);
            TimeSpan timeFrom = TimeSpan.Parse(d1.ToString("HH:mm"));
            TimeSpan timeTo = TimeSpan.Parse(d2.ToString("HH:mm"));

            if (Convert.ToInt32(rdoAllowMalPactice.SelectedValue.ToString()) == 1 && Convert.ToInt32(txtMalFunction.Text) == 0)
            {
                objCommon.DisplayUserMessage(updCreateQuestion, "Enter Malfunction count greator than Zero(0)", this);
                return;
            }
            if (!string.IsNullOrEmpty(txtTotQue.Text) && !string.IsNullOrEmpty(txtTotMarks.Text))
            {
                if (Convert.ToDateTime(txtEndDt.Text.Trim()) < Convert.ToDateTime(txtStartDt.Text.Trim()))
                {
                    objCommon.DisplayUserMessage(updCreateQuestion, "End Date Must Be Greater Than Start Date", this);
                    return;
                }

                if (txtTotQue.Text == "0" || txtTotMarks.Text == "0.00")
                {
                    objCommon.DisplayUserMessage(updCreateQuestion, "Please Transfer Questions by clicking on Arrow button", this);
                    return;
                }
                if (timeFrom > timeTo)
                {
                    objCommon.DisplayUserMessage(updCreateQuestion, "Test Start Time Should be Less than Test End Time", this);
                    return;
                }


                //Get the id's of the student to whom test is given
                foreach (ListViewDataItem dti in lvStudent.Items)
                {
                    CheckBox chkSel = dti.FindControl("chkStud") as CheckBox;

                    if (chkSel.Checked)
                    {

                        if (idno.Equals(string.Empty))
                            idno = chkSel.ToolTip;
                        else
                            idno = idno + "," + chkSel.ToolTip;
                    }
                }
                if (idno.Equals(string.Empty))
                {
                    objCommon.DisplayMessage("Please Select Atleast one Student", this.Page);
                    return;
                }

                objTest.Studidno = idno;

                objTest.TEST_TYPE = Session["TestType"].ToString();
                objTest.TESTNAME = txtTestName.Text;
                objTest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                objTest.SESSIONNO = Convert.ToInt32(Session["SessionNo"]);

                objTest.STARTDATE = Convert.ToDateTime(Convert.ToDateTime(txtStartDt.Text).ToString("dd/MM/yyyy") + " " + TxtExamStartTime.Text.Trim());
                objTest.ENDDATE = Convert.ToDateTime(Convert.ToDateTime(txtEndDt.Text).ToString("dd/MM/yyyy") + " " + TxtExamEndTime.Text.Trim());
                objTest.ATTEMPTS = Convert.ToInt32(txtAttempts.Text.ToString());

                if (Convert.ToInt32(rdoAllowMalPactice.SelectedValue.ToString()) == 0)
                {
                    objTest.MalFunctionCount = '0';
                }
                else
                {
                    objTest.MalFunctionCount = Convert.ToInt32(txtMalFunction.Text);
                }
                //if (cbRandom.Checked)
                if (rbtnRandomQuestion.SelectedValue != "NR")
                {
                    objTest.SHOWRANDOM = 'Y';

                }
                else
                {
                    objTest.SHOWRANDOM = 'N';
                }


                if (chkshowanswerkey.Checked)
                {
                    objTest.SHOW_ANSWER_KEY = 'Y';
                }
                else
                {
                    objTest.SHOW_ANSWER_KEY = 'N';
                }

                if (cbShowResult.Checked)
                {
                    objTest.SHOWRESULT = 'Y';

                }
                else
                {
                    objTest.SHOWRESULT = 'N';
                }

                // FOR FULL RANDOMIZATION
                // BY ZUBAIR ON 07 APRIL 2015
                if (chkFullRandomeze.Checked)
                {
                    objTest.FULL_RANDOME_TEST = 'Y';

                }
                else
                {
                    objTest.FULL_RANDOME_TEST = 'N';
                }
                //END 

                if (txtTestDuration.Text == "00:00:00")
                {
                    objCommon.DisplayUserMessage(this.Page, "Test duration cannot be equal to 00:00:00", this.Page);
                    return;
                }
                else
                {
                    objTest.TESTDURATION = Convert.ToDateTime(txtTestDuration.Text);
                }
                objTest.COLLEGE_CODE = Session["colcode"].ToString();
                objTest.UA_NO = Convert.ToInt32(Session["userno"].ToString());

                string QuestionNo = string.Empty;
                string Correct_Mark = string.Empty;
                string Total_Mark = string.Empty;



                objTest.SELECTED_TOPICS = strTopItmes;//strTopItmes;
                objTest.QUESTIONRATIO = strQuestionRation;
                objTest.QUESTION_MARKS = strMarks;
                objTest.CORR_ANS = "0";
                objTest.QUESTIONNO = "0";

                objTest.TOTAL = Convert.ToDecimal(txtTotMarks.Text.Trim());
                objTest.TOTQUESTION = Convert.ToInt16(txtTotQue.Text.Trim());


                if (Convert.ToInt32(rdoAllowPassword.SelectedValue.ToString()) == 0)
                {
                    objTest.IsAllowPassword = "0";
                    objTest.Password = string.Empty;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Txtpassword.Text) && !string.IsNullOrEmpty(Txtpassword.Text))
                    {
                        objTest.IsAllowPassword = "1";
                        objTest.Password = Txtpassword.Text;
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.Page, "Please Enter The Password", this.Page);
                        return;
                    }
                }


                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {

                    if (ViewState["Test_no"] != null)
                    {
                        objTest.TESTNO = Convert.ToInt32(ViewState["Test_no"]);
                    }
                    if (objTest.SHOWRANDOM == 'Y')//just for continue transaction when questions are random
                    {
                        Session["QUESTION_SET"] = "0";
                    }

                    CustomStatus cs = (CustomStatus)objITestMaster.UpdateITestMaster(objTest, Session["QUESTION_SET"].ToString());

                    strTopItmes = string.Empty;
                    strQuestionRation = string.Empty;
                    strMarks = string.Empty;

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayUserMessage(updCreateQuestion, "Test Modified Successfully... ", this.Page);
                        //   objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Updated, Common.MessageType.Success);
                    }
                }
                else if (ViewState["action"] != null && ViewState["action"].ToString().Equals("add"))
                {

                    int checkTestName = Convert.ToInt32(objCommon.LookUp("ACD_ITESTMASTER", "COUNT(*)", "TESTNAME='" + txtTestName.Text.Trim() + "' AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"])));
                    if (checkTestName > 0)
                    {
                        objCommon.DisplayUserMessage(this.Page, "Test with this name already exist.", this.Page);
                        return;
                    }

                    if (Convert.ToDateTime(txtStartDt.Text.Trim()) < DateTime.Today)
                    {
                        objCommon.DisplayUserMessage(updCreateQuestion, "Start Date Must Be Greater Than Current Date", this);
                        return;
                    }
                    else
                    {
                        if (objTest.SHOWRANDOM == 'Y')//just for continue transaction when questions are random
                        {
                            Session["QUESTION_SET"] = "0";
                        }

                        CustomStatus cs = (CustomStatus)objITestMaster.AddITestMaster(objTest, Session["QUESTION_SET"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayUserMessage(updCreateQuestion, "Test Created Successfully... ", this.Page);
                            //   objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Saved, Common.MessageType.Success);
                        }
                    }


                }

                pnllvView.Visible = true;
                pnlAdd.Visible = false;
                pnlQuestions.Visible = false;
                pnlSelectedQuestion.Visible = false;
                pnlTopic.Visible = true;
                ClearControls();
                BindListView();
                ViewState["action"] = null;
                txtStartDt.Enabled = true;
                rbtnRandomQuestion.Enabled = true;
            }
            else
            {
                objCommon.DisplayUserMessage(updCreateQuestion, "please click on transerfer image", this.Page);

            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(updCreateQuestion, "ITLE_TestMaster.btnSubmit_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            UncheckStudList();
            ImageButton btnEdit = sender as ImageButton;
            int Test_no = int.Parse(btnEdit.CommandArgument);
            Session["TESTNO"] = Convert.ToInt32(Test_no);




            DataSet ds2 = objCommon.FillDropDown("ACD_ITESTMASTER", "TEST_TYPE", "", "TESTNO=" + Test_no, "");

            if (ds2.Tables[0].Rows[0]["TEST_TYPE"] == "O")
            {
                string final_submit = objCommon.LookUp("ACD_ITEST_RESULT", "FinalSubmit", "TESTNO=" + Convert.ToInt32(Session["TESTNO"]));

                if (final_submit == "1")
                {
                    MessageBox("This Test Is Already Attempted by student.");
                    return;
                }
                else
                {
                    ds = objCommon.FillDropDown("ACD_ITESTMASTER_STUDENTS", "IDNO", "null", "TEST_NO=" + Convert.ToString(Test_no), "IDNO");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            foreach (ListViewDataItem lsvdata in lvStudent.Items)
                            {

                                CheckBox chkitem = lsvdata.FindControl("chkStud") as CheckBox;

                                //LinkButton lnkResendSms = lsvdata.FindControl("lnkResendSms") as LinkButton;

                                if (chkitem.ToolTip.Equals(ds.Tables[0].Rows[i]["IDNO"].ToString()))
                                {

                                    chkitem.Checked = true;
                                    //lnkResendSms.Text = Convert.ToInt32(ds.Tables[0].Rows[i]["SMS_STATUS"]) == 1 ? "" : "Resend SMS".ToString();

                                }
                            }

                        }
                    }
                    ShowDetail(Test_no);
                    rbtnRandomQuestion.Enabled = false;
                    ViewState["action"] = "edit";
                }
            }
            else
            {
                  DataSet ds3 =  objCommon.FillDropDown("ACD_ISTUDENTINFO", "TESTNO", "", "TESTNO=" + Test_no, "");

                  if (ds3.Tables[0].Rows.Count > 0)
            {
                MessageBox("This Test Is Already Attempted by student.");
                return;
            }
            
                 else
                {
                    ds = objCommon.FillDropDown("ACD_ITESTMASTER_STUDENTS", "IDNO", "null", "TEST_NO=" + Convert.ToString(Test_no), "IDNO");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            foreach (ListViewDataItem lsvdata in lvStudent.Items)
                            {

                                CheckBox chkitem = lsvdata.FindControl("chkStud") as CheckBox;

                                //LinkButton lnkResendSms = lsvdata.FindControl("lnkResendSms") as LinkButton;

                                if (chkitem.ToolTip.Equals(ds.Tables[0].Rows[i]["IDNO"].ToString()))
                                {

                                    chkitem.Checked = true;
                                    //lnkResendSms.Text = Convert.ToInt32(ds.Tables[0].Rows[i]["SMS_STATUS"]) == 1 ? "" : "Resend SMS".ToString();

                                }
                            }

                        }
                    }
                    ShowDetail(Test_no);
                    rbtnRandomQuestion.Enabled = false;
                    ViewState["action"] = "edit";
                }
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TestMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnDel = sender as ImageButton;
            int questioNo = int.Parse(btnDel.AlternateText);
            DataTable dt;
            DataSet ds = (DataSet)Session["QuestionSet"];

            if (Session["QuestionSet"] != null)
            {
                //dt = ((DataTable)Session["QuestionSet"]);
                dt = ds.Tables[0];
                dt.Rows.Remove(this.GetDeletableDataRow(dt, btnDel.CommandArgument));
                Session["QuestionSet"] = dt;
                lvSelectedQuestions.DataSource = dt;
                lvSelectedQuestions.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnViewTestReport_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_Created_Test_Report", "Itle_Test_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AddForum.btnViewTestReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    #endregion

    #region startcomment

    //private void BindQuestionsListView()
    //{
    //    try
    //    {
    //        int Course = Convert.ToInt32(Session["ICourseNo"]);
    //        objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
    //        objQuest.UA_NO = Convert.ToInt32(Session["userno"]);
    //        objQuest.TEST_NO = Convert.ToInt32(Session["TESTNO"].ToString());
    //        //int TEST_NO = Convert.ToInt32(Session["TESTNO"].ToString());

    //        //  DataSet ds = null;
    //        //   ds = objIQBC.GetAllQuestion(objQuest);
    //        DataSet ds = objCommon.FillDropDown("ITLE_QUESTIONBANK", "QUESTIONNO,QUESTIONTEXT ,1 AS TOTAL, TOPICNO", "1 AS CORRECT_MARKS", "SDSRNO = " + Course, "QUESTIONNO");

    //        // ds  = objCommon.FillDropDown("ITLE_QUESTIONBANK QB INNER JOIN ITLE_TESTQUESTION TQ ON(QB.QUESTIONNO = TQ.QUESTIONNO)", "QB.QUESTIONNO", "QB.QUESTIONTEXT,TQ.CORRECT_MARKS", "TQ.TESTNO =" + TEST_NO, "TQ.QUESTIONNO");

    //        //    DataSet ds = objCommon.FillDropDown("ITLE_QUESTIONBANK  QB Left Outer Join ITLE_TESTQUESTION TQ", "DISTINCT QB.QUESTIONNO", "QUESTIONTEXT,TQ.CORRECT_MARKS", "TQ.SDSRNO = " + Course, "QUESTIONNO");


    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvQuestions.DataSource = ds;
    //            lvQuestions.DataBind();
    //        }
    //        else
    //        {
    //            lvQuestions.DataSource = null;
    //            lvQuestions.DataBind();
    //        }

    //        DataSet ds1 = objCommon.FillDropDown("ITLE_TestQuestion TQ, ITLE_TESTMASTER TM", "TQ.QUESTIONNO", "TQ.CORRECT_MARKS, TM.TOTAL", "TQ.TESTNO=TM.TESTNO AND TQ.TESTNO=" + Session["TESTNO"], "TQ.QUESTIONNO");
    //        foreach (ListViewDataItem dti in lvQuestions.Items)
    //        {
    //            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
    //            {
    //                CheckBox chkSel = dti.FindControl("cbQuestionRow") as CheckBox;
    //                TextBox txtSelectQ = dti.FindControl("TextBox1") as TextBox;
    //                if (Convert.ToInt32(chkSel.ToolTip).Equals(ds1.Tables[0].Rows[i]["QUESTIONNO"]))
    //                {
    //                    chkSel.Checked = true;
    //                    TextBox1.Text = ds1.Tables[0].Rows[i]["CORRECT_MARKS"].ToString();

    //                }

    //            }
    //        }
    //        //txttot.Text = ds1.Tables[0].Rows[0]["TOTAL"].ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayUserMessage(updCreateQuestion, "ITLE_TestMasterNew.aspx.BindQuestionsListView->  " + ex.Message + ex.StackTrace, this.Page);
    //    }
    //}

    #endregion

    #region commnetCode
    //private void FillListForDescriptive()
    //{
    //    try
    //    {
    //        SQLHelper slp = new SQLHelper(objCommon._client_constr);

    //        DataSet ds = slp.ExecuteDataSet("select TOPIC,MARKS_FOR_QUESTION,COUNT(COURSENO) as Questions,0 AS QuestionRatio from ACD_IQUESTIONBANK where COURSENO=" + Session["ICourseNo"] + "AND QUESTION_TYPE=" + Session["TestType"].ToString() + " group by topic,MARKS_FOR_QUESTION ORDER BY TOPIC");
    //        lsvbindquestion.DataSource = ds;
    //        lsvbindquestion.DataBind();


    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayUserMessage(updCreateQuestion, "ITLE_TestMasterNew.aspx.FillDropdown->  " + ex.Message + ex.StackTrace, this.Page);
    //    }
    //}

    //protected void ddlQuestionMarks_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    #endregion
    //protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if(ddlTopic.SelectedValue!='')
    //    if (ddlTopic.SelectedValue.ToString() != "0")
    //    {
    //        SQLHelper slp = new SQLHelper(objCommon._client_constr);
    //        string topic = ddlTopic.SelectedValue.ToString();
    //        //by Zubair
    //        //DataSet ds = slp.ExecuteDataSet("select TOPIC,COUNT(COURSENO) as Questions,0 AS QuestionRatio,ISNULL(MARKS_FOR_QUESTION,0) AS MARKS_FOR_QUESTION from ACD_IQUESTIONBANK where COURSENO=" + Session["ICourseNo"] + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "' group by topic,MARKS_FOR_QUESTION ORDER BY TOPIC");
    //        DataSet ds = slp.ExecuteDataSet("select TOPIC,COUNT(COURSENO) as Questions,0 AS QuestionRatio,(CASE WHEN QUESTION_TYPE = 'D' THEN ISNULL(MARKS_FOR_QUESTION,0) ELSE 1   END) AS MARKS_FOR_QUESTION from ACD_IQUESTIONBANK where COURSENO=" + Session["ICourseNo"] + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "' AND TOPIC='" + topic.ToString() + "' group by topic,MARKS_FOR_QUESTION,QUESTION_TYPE ORDER BY TOPIC");
    //        lsvbindquestion.DataSource = ds;
    //        lsvbindquestion.DataBind();
    //    }
    //    else
    //    {
    //        SQLHelper slp = new SQLHelper(objCommon._client_constr);
    //        //by Zubair
    //        //DataSet ds = slp.ExecuteDataSet("select TOPIC,COUNT(COURSENO) as Questions,0 AS QuestionRatio,ISNULL(MARKS_FOR_QUESTION,0) AS MARKS_FOR_QUESTION from ACD_IQUESTIONBANK where COURSENO=" + Session["ICourseNo"] + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "' group by topic,MARKS_FOR_QUESTION ORDER BY TOPIC");
    //        DataSet ds = slp.ExecuteDataSet("select TOPIC,COUNT(COURSENO) as Questions,0 AS QuestionRatio,(CASE WHEN QUESTION_TYPE = 'D' THEN ISNULL(MARKS_FOR_QUESTION,0) ELSE 1   END) AS MARKS_FOR_QUESTION from ACD_IQUESTIONBANK where COURSENO=" + Session["ICourseNo"] + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "' group by topic,MARKS_FOR_QUESTION,QUESTION_TYPE ORDER BY TOPIC");
    //        lsvbindquestion.DataSource = ds;
    //        lsvbindquestion.DataBind();
    //    }

    //}
    //protected void txtStartDt_TextChanged(object sender, EventArgs e)
    //{
    //    //*****VALIDATE BY: M. REHBAR SHEIKH ON 26-08-2019*****
    //    try
    //    {
    //        DateTime date = Convert.ToDateTime(txtStartDt.Text.ToString());
    //        DateTime today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
    //        if (date < today)
    //        {
    //            MessageBox("You cannot select a day earlier than today. Please select Valid Date");
    //            txtStartDt.Text = "";
    //            return;

    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}


    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void txtEndDt_TextChanged(object sender, EventArgs e)
    {
        //*****VALIDATE BY: M. REHBAR SHEIKH ON 26-08-2019*****
        try
        {
            if (txtStartDt.Text != string.Empty)
            {
                DateTime date = Convert.ToDateTime(txtEndDt.Text.ToString());
                DateTime startDate = Convert.ToDateTime(txtStartDt.Text.ToString());
                DateTime today = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                if (startDate > date)
                {
                    MessageBox("End Date Should be greater than Start Date.");
                    txtEndDt.Text = "";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ValidateDate(object sender, ServerValidateEventArgs e)
    {
        //if (Regex.IsMatch(txtStartDt.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$"))
        //{
        //    DateTime dt;
        //    e.IsValid = DateTime.TryParseExact(e.Value, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
        //    if (e.IsValid)
        //    {
        //        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Date.');", true);
        //        objCommon.DisplayMessage(this.updCreateQuestion, "You cannot select a day earlier than today", this.Page);
        //        return;
        //    }
        //}
        //else
        //{
        //    e.IsValid = false;
        //}
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStudList();
    }
    protected void rdoAllowMalPactice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(rdoAllowMalPactice.SelectedValue.ToString()) == 0)
            {
                divMalfunction.Visible = false;
                txtMalFunction.Text = "0";
            }
            else
            {
                divMalfunction.Visible = true;
            }

        }
        catch (Exception ex)
        {

            //throw;
        }
    }




    
    protected void cbHead_CheckedChanged1(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvitem in lvStudent.Items)
        {
            CheckBox chk = lvitem.FindControl("cbHead") as CheckBox;
            CheckBox chkstud = lvitem.FindControl("chkStud") as CheckBox;

            if (chk.Checked == true)
            {
                chkstud.Checked = true;
            }
        }
    }
    protected void cbShowResult_CheckedChanged(object sender, EventArgs e)
    {
        if (cbShowResult.Checked == true)
        {
            lblshowanssheet.Visible = true;
        }
        else
        {
            lblshowanssheet.Visible = false;
        }
    }

    protected void rdoAllowPassword_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(rdoAllowPassword.SelectedValue.ToString()) == 0)
            {
                divpassword.Visible = false;

            }
            else
            {
                divpassword.Visible = true;
            }

        }
        catch (Exception ex)
        {

            //throw;
        }
    }

    protected void btndelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btndelete = sender as ImageButton;
        int SessionNo = Convert.ToInt32(Session["SessionNo"]);
        int CourseNo = Convert.ToInt32(Session["ICourseNo"]);
        int Test_no = int.Parse(btndelete.CommandArgument);
        int Ua_No = Convert.ToInt32(Session["userno"]);


        DataSet ds  =  objCommon.FillDropDown("ACD_ITESTMASTER", "TEST_TYPE", "", "TESTNO=" + Test_no,"");

        if (ds.Tables[0].Rows[0]["TEST_TYPE"] == "O")
        {
            string final_submit = objCommon.LookUp("ACD_ITEST_RESULT", "FinalSubmit", "TESTNO=" + Test_no);

            if (final_submit == "1")
            {
                MessageBox("This Test Is Already Attempted by student.");
                return;
            }
            else
           
           {
             CustomStatus cs = (CustomStatus)objITestMaster.DeleteITestMaster(SessionNo,CourseNo,Test_no,Ua_No);
           
             BindListView();

             if (cs.Equals(CustomStatus.RecordUpdated))
             {

                 objCommon.DisplayUserMessage(updCreateQuestion, "Test Delete Succusessfully.", this);

                 
             }
         }

            

            }
        else
        {
            DataSet ds1 =  objCommon.FillDropDown("ACD_ISTUDENTINFO", "TESTNO", "", "TESTNO=" + Test_no, "");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                MessageBox("This Test Is Already Attempted by student.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objITestMaster.DeleteITestMaster(SessionNo, CourseNo, Test_no, Ua_No);

                BindListView();

                if (cs.Equals(CustomStatus.RecordUpdated))
                {

                    objCommon.DisplayUserMessage(updCreateQuestion, "Test Delete Succusessfully.", this);

                         }
            }
        }

            }
        }

        
   


