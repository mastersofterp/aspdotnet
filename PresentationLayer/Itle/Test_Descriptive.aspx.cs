using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Text;
using System.Xml.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Transactions;
using System.IO;
using System.Windows;
using System.Linq;
using System.Data.OleDb;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using IITMS.NITPRM;

public partial class Itle_Test_Descriptive : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITestMasterController objTestc = new ITestMasterController();
    TestResult objResult = new TestResult();
    CourseControlleritle objAC = new CourseControlleritle();
    ITestMaster objTest = new ITestMaster();
    IQuestionbank objQuest = new IQuestionbank();
    IQuestionbankController objIQBC = new IQuestionbankController();
    DataTable dt = new DataTable();
    DataSet ds1 = new DataSet();
    int ctr = 0;
    int t = 0;
    int no2 = 0;
    int no3 = 0;
    DateTime date;
    DateTime mydate;
    long timerStartValue = 1800;
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    static string CheckOut = string.Empty;
    int count = 0;
    string PageId;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            Session["post"] = Convert.ToInt32(Session["post"]) + Convert.ToInt32(Session["post"]);

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                lblUrname.Text = Convert.ToString(Session["USERFULLNAME"].ToString());

                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title

              
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];

                this.timerStartValue = long.Parse(Session["SEC"].ToString());
                this.TimerInterval = 500;

                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SESSION_NAME"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();
                objTest.TESTNO = Convert.ToInt32(Session["Test_No"]);
                lblTotTime.Text = Convert.ToDateTime(Session["Total_Time_For_Test"]).ToString("HH:mm:ss");
                // objTest.IDNO = Convert.ToInt32(Session["STUDID"]);
                objTest.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objTest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                HttpCookie Time = Request.Cookies["start"];
                string name = Session["username"].ToString();
                lblSeatno.Text = objCommon.LookUp("ACD_STUDENT_RESULT", "ROLL_NO", "IDNO=" + (Session["idno"]));

                FetchStudInfo();
                //sachin 22 March 2017
                CheckTestValid();
                UpdateAttempts();

                BindShowQues();
                // COUNT();
                lblQueNo.Text = "1" + "/" + Convert.ToInt32(Session["NOQ"].ToString());

                // Used to insert id,date and courseno in Log_History table
                int log_history = objAC.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));

              //  ViewState["quesno"] = null;
                hdnMalfunction.Value = Session["Malfunction"].ToString();
                if (Session["MalfunctionDesStud"] == null)
                {
                    hdnMalfunctionDesStud.Value ="0";
                }
                else
                {

                    hdnMalfunctionDesStud.Value = Session["MalfunctionDesStud"].ToString();
                }
                txtAnswer.Attributes.Add("onkeydown", "return (event.keyCode!=18);");
                txtAnswer.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                txtAnswer.Attributes.Add("onkeydown", "return (event.keyCode!=17);");

                foreach (ListViewItem itemRow in lvTest.Items)
                {
                    ImageButton img =(ImageButton)itemRow.FindControl("btnAnswer") as ImageButton;


                    int quesno = int.Parse(img.CommandArgument);
                    //if (quesno == Convert.ToInt32(Session["Question_no"]))


                    string answer = (objCommon.LookUp("ACD_ITLE_RESULTCOPY", "DESCRIPTIVE_ANSWER", "QUESTIONNO=" + quesno + "AND IDNO=" + Convert.ToInt32(Session["idno"]) + "and TESTNO=" + Convert.ToInt32(Session["Test_No"])));
                    if (answer == string.Empty || answer == "")
                    {
                        img.ImageUrl = "~/images/redArrow1.jpg";
                    }
                    else
                    {
                        img.ImageUrl = "~/images/greenArrow1.jpg";
                    }
                }

            }
        }
       // ViewState["action"] = null;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Session["post"] = Convert.ToInt32(Session["post"]) + 1;
        string[] ctl = Request.Form.GetValues("__EVENTTARGET");
    }

    void Page_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            StringBuilder bldr = new StringBuilder();
            bldr.AppendFormat("var Timer = new myTimer({0},{1},'{2}','timerData');", this.timerStartValue, this.TimerInterval, this.lblTimerCount.ClientID);
            bldr.Append("Timer.go()");
            ClientScript.RegisterStartupScript(this.GetType(), "TimerScript", bldr.ToString(), true);
            ClientScript.RegisterHiddenField("timerData", timerStartValue.ToString());
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //if (Session["masterpage"] != null)
        //    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        //else
        //    objCommon.SetMasterPage(Page, "");

        string timerVal = Request.Form["timerData"];
        if (timerVal != null || timerVal == "")
        {
            timerVal = timerVal.Replace(",", String.Empty);
            timerStartValue = long.Parse(timerVal);
        }

    }



    #endregion

    #region Private Method

    private Int32 TimerInterval
    {

        get
        {
            object o = ViewState["timerInterval"];
            if (o != null) { return Int32.Parse(o.ToString()); }
            return 50;
        }
        set { ViewState["timerInterval"] = value; }

    }

    private void COUNT()
    {
        Session["COUNT"] = Convert.ToInt32(Session["COUNT"].ToString()) + 1;

    }

    private void BindShowQues()
    {
        try
        {
            const int selectques = 3;
            DataRow dr = null;
            int[] a = new int[selectques - 1];
            ArrayList list = new ArrayList();
            //int noofquestions = Convert.ToInt32(objCommon.LookUp("ITLE_TESTQUESTION", "COUNT(TESTNO)", "TESTNO=" + Convert.ToInt32(Session["Test_No"])));
            int noofquestions = Convert.ToInt32(objCommon.LookUp("ACD_ITESTMASTER", "TOTQUESTION", "TESTNO=" + objTest.TEST_NO));
            Session["NOQ"] = Convert.ToInt32(noofquestions);
            Session["noofquestions"] = noofquestions - 2;

            int[] _myArr = new int[noofquestions];

            for (int i = 0; i < noofquestions; i++)
                _myArr[i] = i + 1;

            SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);

            int[] _myArrOp = new int[6];
            for (int i = 0; i < 6; i++)
                _myArrOp[i] = i + 1;

            _myArrOp = (int[])Shuffle(_myArrOp);

            // ---------By Saket Singh Date:23-08-2017----------
            string QuesRandom = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRANDOM", "TESTNO=" + Session["Test_No"]);
            if (QuesRandom.Contains("N"))
            {
                objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "'");//Session["userno"]);
                string questioSet = objCommon.LookUp("ACD_IQUESTION_SET_FOR_TEST", "QUESTION_SET", "TESTNO=" + Session["Test_No"]);
                string MyQuery = "select  QUESTIONNO,COURSENO,[TYPE],QUESTIONTEXT,QUESTIONIMAGE,ANS1TEXT,ANS1IMG,ANS2TEXT,ANS2IMG,ANS3TEXT,ANS3IMG,ANS4TEXT,ANS4IMG,ANS5TEXT,ANS5IMG,ANS6TEXT,ANS6IMG,CORRECTANS,COLLEGE_CODE,CREATEDDATE,UA_NO,TOPIC,REMARK,AUTHOR,QUESTION_TYPE,MARKS_FOR_QUESTION, 1 AS CORRECT_MARKS," + Session["idno"] + " AS StudentID,NULL," + Session["OrgId"] + " AS OrganizationId from ACD_IQUESTIONBANK  where " + "COURSENO=" + objTest.COURSENO + "and QUESTIONNO in(" + questioSet + ")" + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "'" + " order by newid()";
                CustomStatus cs = (CustomStatus)objTestc.AddITestMaster_Temp(MyQuery);
            }
            else
            {
                //--------Tarun Date:16-1-2014---------
                objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "'");//Session["userno"]);
                DataSet dsT = objCommon.FillDropDown("ACD_ITESTMASTER", "TOPICS,QUESTIONRATIO,MARKS_FOR_QUESTION", "QUESTIONRATIO", "testno=" + objTest.TEST_NO, "");//objSqlHelper.ExecuteDataSet(Query);


                if (!string.IsNullOrEmpty(dsT.Tables[0].Rows[0]["TOPICS"].ToString()))
                {
                    string QRem = dsT.Tables[0].Rows[0]["QUESTIONRATIO"].ToString();
                    string Rem = dsT.Tables[0].Rows[0]["TOPICS"].ToString();
                    string QMarks = dsT.Tables[0].Rows[0]["MARKS_FOR_QUESTION"].ToString();
                    //Rem = Rem.Replace("'", "");
                    QRem = QRem.Replace("'", "");
                    QMarks = QMarks.Replace("'", "");
                    string[] Mvalues = QMarks.Split(',');

                    foreach (DataRow dtr in dsT.Tables[0].Rows)
                    {
                        string[] values = Rem.Split(',');
                        string[] Qvalues = QRem.Split(',');

                        for (int i = 0; i < Qvalues.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(Qvalues[i].ToString()) && Convert.ToInt32(Qvalues[i].ToString().Trim()) != 0)
                            {
                                string QRatio = Qvalues[i].ToString();
                                string QTopic = values[i].ToString();
                                string MarkRatio = Mvalues[i].ToString();
                                string MyQuery = "select TOP " + QRatio + "  QUESTIONNO,COURSENO,[TYPE],QUESTIONTEXT,QUESTIONIMAGE,ANS1TEXT,ANS1IMG,ANS2TEXT,ANS2IMG,ANS3TEXT,ANS3IMG,ANS4TEXT,ANS4IMG,ANS5TEXT,ANS5IMG,ANS6TEXT,ANS6IMG,CORRECTANS,COLLEGE_CODE,CREATEDDATE,UA_NO,TOPIC,REMARK,AUTHOR,QUESTION_TYPE,MARKS_FOR_QUESTION, 1 AS CORRECT_MARKS," + Session["idno"] + " AS StudentID,NULL," + Session["OrgId"] + " AS OrganizationId from ACD_IQUESTIONBANK where " + "COURSENO=" + objTest.COURSENO + "and Topic in(" + QTopic + ")" + "and MARKS_FOR_QUESTION in(" + MarkRatio + ")" + " order by newid()";
                                CustomStatus cs = (CustomStatus)objTestc.AddITestMaster_Temp(MyQuery);
                            }
                        }
                    }
                }
            }

            char fullRandomeTest = Convert.ToChar(objCommon.LookUp("ACD_ITESTMASTER", "ISNULL(FULL_RANDOME_TEST,'N')", "TESTNO=" + Session["Test_No"]).Trim());
            string sql;
            if (fullRandomeTest == 'Y')
            {
                sql = "select * from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "'";
            }
            else
            {
                sql = "select * from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "' order by Topic"; //,Questionno
            }
            DataSet ds = objSqlHelper.ExecuteDataSet(sql);
            int TotQue = ds.Tables[0].Rows.Count;
            Session["TOTQUES"] = TotQue;


            for (int i = 0; i < _myArr.Length; i++)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtRe = new DataTable();
                    dtRe.Columns.Add(new DataColumn("QU_SRNO", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("QUENO", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("QUESTIONTEXT", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS1TEXT", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS2TEXT", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS3TEXT", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS4TEXT", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS5TEXT", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS6TEXT", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("QUESTIONIMAGE", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS1IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS2IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS3IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS4IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS5IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS6IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("CORRECTANS", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("SELECTED", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("CORRECT_MARKS", typeof(decimal)));
                    dtRe.Columns.Add(new DataColumn("USERNO", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("SELECTEDOPTION", typeof(object)));
                    dtRe.Columns.Add(new DataColumn("TYPE", typeof(char)));
                    dtRe.Columns.Add(new DataColumn("MARKS_FOR_QUESTION", typeof(int)));

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        dr = dtRe.NewRow();
                        dr["QU_SRNO"] = dtRe.Rows.Count + 1;
                        dr["QUENO"] = ds.Tables[0].Rows[j]["QUESTIONNO"].ToString();
                        dr["QUESTIONTEXT"] = ds.Tables[0].Rows[j]["QUESTIONTEXT"].ToString();
                        dr["ANS1TEXT"] = ds.Tables[0].Rows[j]["ANS1TEXT"].ToString();
                        dr["ANS2TEXT"] = ds.Tables[0].Rows[j]["ANS2TEXT"].ToString();
                        dr["ANS3TEXT"] = ds.Tables[0].Rows[j]["ANS3TEXT"].ToString();
                        dr["ANS4TEXT"] = ds.Tables[0].Rows[j]["ANS4TEXT"].ToString();
                        dr["ANS5TEXT"] = ds.Tables[0].Rows[j]["ANS5TEXT"].ToString();
                        dr["ANS6TEXT"] = ds.Tables[0].Rows[j]["ANS6TEXT"].ToString();

                        if (ds.Tables[0].Rows[j]["QUESTIONIMAGE"].ToString() == "")
                        {
                            dr["QUESTIONIMAGE"] = null;
                        }
                        else
                        {
                            dr["QUESTIONIMAGE"] = "~/ITLE/IMAGE_QUESTION/" + ds.Tables[0].Rows[j]["QUESTIONIMAGE"].ToString();
                            dr["ANS1IMG"] = ds.Tables[0].Rows[j]["ANS1IMG"].ToString();
                            dr["ANS2IMG"] = ds.Tables[0].Rows[j]["ANS2IMG"].ToString();
                            dr["ANS3IMG"] = ds.Tables[0].Rows[j]["ANS3IMG"].ToString();
                            dr["ANS4IMG"] = ds.Tables[0].Rows[j]["ANS4IMG"].ToString();
                            dr["ANS5IMG"] = ds.Tables[0].Rows[j]["ANS5IMG"].ToString();
                            dr["ANS6IMG"] = ds.Tables[0].Rows[j]["ANS6IMG"].ToString();
                        }
                        
                        dr["CORRECTANS"] = ds.Tables[0].Rows[j]["CORRECTANS"].ToString();
                        dr["CORRECT_MARKS"] = ds.Tables[0].Rows[j]["CORRECT_MARKS"].ToString();

                        dr["SELECTED"] = -1;
                        dr["USERNO"] = Convert.ToInt32(Session["userno"].ToString());

                        dr["TYPE"] = ds.Tables[0].Rows[j]["TYPE"].ToString();
                        dr["MARKS_FOR_QUESTION"] = ds.Tables[0].Rows[j]["MARKS_FOR_QUESTION"].ToString();
                        dtRe.Rows.Add(dr);

                        Session["Answered"] = dtRe;
                    }
                }
            }

            lvTest.DataSource = Session["Answered"];
            lvTest.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtTbl = (DataTable)Session["Answered"];
                int i = 0;
                foreach (ListViewDataItem li in lvTest.Items)
                {
                    if (dtTbl.Rows[i]["TYPE"].ToString().Equals("T"))
                    {
                        RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;
                        Image img = li.FindControl("imgQuesImage") as Image;
                        if (img.ImageUrl == "")
                        {
                            img.Visible = false;
                        }
                        else
                        {
                            img.Visible = true;
                        }

                        for (int j = 0; j < 6; j++)
                        {
                            int g = Convert.ToInt32(_myArrOp.GetValue(j));

                            //r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS1TEXT"].ToString(), "1"));
                            if (dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString() != "")
                                if (g == Convert.ToInt32(dtTbl.Rows[i]["CORRECTANS"]))
                                    r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), dtTbl.Rows[i]["CORRECTANS"].ToString()));
                                else
                                    r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), "0"));
                        }
                    }
                    else
                    {
                        Image img = li.FindControl("imgQuesImage") as Image;
                        if (img.ImageUrl == "")
                        {
                            img.Visible = false;
                        }
                        else
                            img.Visible = true;
                        RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;
                        string imageBankFolder = "IMAGE_QUESTION/";

                        for (int j = 0; j < 6; j++)
                        {
                            int g = Convert.ToInt32(_myArrOp.GetValue(j));

                            //r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS1TEXT"].ToString(), "1"));
                            if (dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() != "")
                                if (g == Convert.ToInt32(dtTbl.Rows[i]["CORRECTANS"]))
                                    r.Items.Add(new ListItem("<img src=" + imageBankFolder + dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() + " width=70px>" + dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), dtTbl.Rows[i]["CORRECTANS"].ToString()));
                                else
                                    r.Items.Add(new ListItem("<img src=" + imageBankFolder + dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() + " width=70px>" + dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), "0"));
                        }
                    }
                    i++;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Test.BindShowQues -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FetchStudInfo()
    {
        //DataSet dt = objAC.GetTestInfo(Convert.ToInt32(Session["userno"]));
        //if (dt != null)
        lblUrname.Text = Convert.ToString(Session["USERFULLNAME"].ToString());
        lblSession.Text = Session["SESSION_NAME"].ToString();
        lblSession.ToolTip = Session["SESSION_NAME"].ToString();
        lblCourse.Text = Session["ICourseName"].ToString();
        lblTestName.Text = Session["TestName"].ToString();
        Session["COURSENO"] = Convert.ToInt32(Session["ICourseNo"]);

        string STUDID = Session["idno"].ToString();
        //STUDID = STUDID.Remove(STUDID.IndexOf('@'));

        lblSeatno.Text = objCommon.LookUp("ACD_STUDENT_RESULT", "ROLL_NO", "IDNO=" + STUDID);
        objTest.TEST_NO = Convert.ToInt32(Session["Test_No"]);
        objTest.UA_NO = Convert.ToInt32(Session["userno"]);
        objTest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

        objResult.TESTNO = objTest.TEST_NO;
        objResult.IDNO = Convert.ToInt32(STUDID);
        objResult.COURSENO = objTest.COURSENO;
    }

    private void UpdateAttempts()
    {
        objAC.Update_User_Attempts(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO"]), objTest.TEST_NO);
    }

    private void SaveAnswers()
    {
        try
        {
            //Session["Question_Text"] = lblQuestion.Text.Trim();
            //Session["Descriptive_Answer"] = txtAnswer.Text.Trim();

            objQuest.QUESTIONNO = Convert.ToInt32(ViewState["quesno"]);
            objQuest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            //objQuest.QUESTIONNO = Convert.ToInt32(ViewState["ques_no"]);
            objQuest.QUESTIONTEXT = lblQuestion.Text.Trim();
            objQuest.DESCRIPTIVE_ANSWER = txtAnswer.Text.Trim();
            objQuest.QUESTION_MARKS = Convert.ToInt32(Session["Question_Marks"]);
            objQuest.TEST_TYPE = Session["Test_Type"].ToString();

            objQuest.COLLEGE_CODE = Session["colcode"].ToString().Trim();
            objQuest.IDNO = Convert.ToInt32(Session["idno"]);
            objQuest.TEST_NO = Convert.ToInt32(Session["Test_No"]);

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //objQuest.QUESTIONNO = Convert.ToInt32(ViewState["ques_no"]);

                CustomStatus upd = (CustomStatus)Convert.ToInt32(objIQBC.UpdateTestResultDetails(objQuest));

                //if (upd.Equals(CustomStatus.RecordSaved))
                //    objCommon.DisplayUserMessage(UpdatePanel_Login, "Answer Submitted Succesfully.", this.Page);


              

                
                
               // string answer = (objCommon.LookUp("ACD_ITLE_RESULTCOPY", "DESCRIPTIVE_ANSWER", "QUESTIONNO=" + quesno + "AND IDNO=" + Convert.ToInt32(Session["idno"]) + "and TESTNO=" + Convert.ToInt32(Session["Test_No"])));
               

                    ViewState["action"] = null;
                    ViewState["quesno"] = null;
                
            }

            else
            {
                CustomStatus insert = (CustomStatus)Convert.ToInt32(objIQBC.AddTestResultDetails(objQuest));
               
                //int csi = Convert.ToInt32(objIQBC.UpdateTestResultDetails(objQuest));
                //if (csi.Equals(CustomStatus.RecordSaved))
                //    objCommon.DisplayUserMessage(UpdatePanel_Login, "Marks Submitted Succesfully.", this.Page);
                ViewState["quesno"] = null;
            }



            Session["Question_no"] = null;
            Session["Question_Marks"] = null;
            ViewState["action"] = null;
           // ViewState["quesno"] = null;
            //ClearControls();
        }
        catch (Exception ex)
        {


        }

    }

    private void ClearControls()
    {
        txtAnswer.Text = string.Empty;
        Session["Question_no"] = null;
        Session["Question_Marks"] = null;
    }

    private void ShowDetails(int quesno)
    {
        try
        {
            ViewState["ques_no"] = quesno;
            divDescriptiveAnswer.Visible = true;
            DataTableReader dtr = objIQBC.GetSingleAnswerByQuesNo(quesno, Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["Test_No"]));

            if (dtr != null)
            {
                if (dtr.Read())
                {

                    lblQuestion.Text = dtr["QUESTIONTEXT"].ToString() == null ? "" : dtr["QUESTIONTEXT"].ToString();
                    txtAnswer.Text = dtr["DESCRIPTIVE_ANSWER"].ToString() == null ? "" : dtr["DESCRIPTIVE_ANSWER"].ToString();
                    //txtAnswer.Text = lbltxtAnswer. InnerText;
                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Test_Descriptive.aspx.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #endregion

    #region Sachin 21Mar2017

    private void CheckTestValid()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_ITESTMASTER", "*", "TESTDURATION", "TESTNO=" + Convert.ToInt32(Session["Test_No"]), "TESTNO");
            Session["Total_Marks"] = ds.Tables[0].Rows[0]["TOTAL"].ToString();

            string StartTime = ds.Tables[0].Rows[0]["STARTDATE"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTDATE"].ToString()).ToString("HH:mm:ss");
            string EndTime = ds.Tables[0].Rows[0]["ENDDATE"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("HH:mm:ss");

            if (Convert.ToDateTime(StartTime).TimeOfDay <= DateTime.Now.TimeOfDay && Convert.ToDateTime(EndTime).TimeOfDay >= DateTime.Now.TimeOfDay)
            {
                string Time = ds.Tables[0].Rows[0]["TESTDURATION"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss");
                Session["Total_Time_For_Test"] = Time;
                int Attempts = Convert.ToInt32(ds.Tables[0].Rows[0]["ATTEMPTS"]);
                if (Attempts > 0)
                {
                    //DataSet ds1 = objCommon.FillDropDown("ITLE_TESTRESULT", "", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(Test_No) + " AND IDNO=" + Session["STUDID"], "");
                    string ret = objCommon.LookUp("ACD_ITEST_RESULT", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(Session["Test_No"]) + " AND IDNO=" + Session["idno"]);
                    string ret1 = objCommon.LookUp("ACD_ISTUDENTINFO", "isnull(ATTEMPTS_ALLOWED,0)", "TESTNO=" + Convert.ToInt32(Session["Test_No"]) + " AND STUDENTID=" + Session["idno"]);
                    //if (ds1.Tables[0].Rows.Count == 0 || Convert.ToInt32(ds1.Tables[0].Rows[0]["STUDATTEMPTS"]) < Attempts)
                    if (ret == "" || Convert.ToInt32(ret) < Attempts || ret1 == "1")
                        Session["NOOFATTEMPT"] = Attempts;
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your attempts are over for this test')", true);
                        // Response.Redirect("~/ITLE/StudTest.aspx");
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "self.close();", true);
                        Response.Redirect("ITLE/StudTest.aspx");
                        Clear_Sessions();
                        return;
                    }
                }
                Session["TOTALMARKS"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL"]);
                DateTime Time1 = Convert.ToDateTime(Time);
                //Session["Test_No"] = Test_No;
                //Session["TestName"] = Test_Name;
                Session["COURSENO"] = ds.Tables[0].Rows[0]["COURSENO"];

                //objAC.Update_ITLE_User(Convert.ToInt32(Session["userno"]), 0, Test_No);

                HttpCookie h = new HttpCookie("start");
                Response.Cookies.Clear();
                h.Value = Time;
                Response.Cookies.Add(h);


                int sec = (Time1.Hour * 60 * 60) + (Time1.Minute * 60) + Time1.Second;
                Session["SEC"] = sec * 1000;

                //Session["SEC"] = 70000;

                string Script = string.Empty;

                if (Session["Test_Type"].ToString() == "DESCRIPTIVE")
                {
                    //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("StudTest")));
                    //url += "Test_Descriptive.aspx?";

                    //Script += " window.open('Test_Descriptive.aspx','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=yes');";

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

                }
                else
                {
                    //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("StudTest")));
                    //url += "Test.aspx?";
                    //Script += " window.open('Test.aspx','PoP_Up','status=0,width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=yes');";


                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Time')", true);
                // objCommon.DisplayUserMessage(UpdatePanel1, "Invalid Time", this.Page);
                Clear_Sessions();
                return;
            }
        }
        catch (Exception ex)
        {
            //objCommon.DisplayUserMessage(UpdatePanel1, "ITLE_StudTest.aspx.btnlnkSelect_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }

    }

    private void Clear_Sessions()
    {
        try
        {
            Session["Test_No"] = null;
            Session["Total_Marks"] = null;
            Session["Total_Time_For_Test"] = null;
            Session["Test_Type"] = null;
        }
        catch (Exception ex)
        {

        }


    }

    #endregion

    #region Public Method

    public static object Shuffle(object org)
    {
        if (org is IList)
        {
            Random rnd = new Random();
            IList arr = (IList)org;
            int newPos;
            object tempObj;
            int index = arr.Count;
            while (--index >= 0)
            {
                // new position for element at index   
                newPos = rnd.Next(1, arr.Count);
                // swap the elements at newPos and index  
                tempObj = arr[index];
                arr[index] = arr[newPos];
                arr[newPos] = tempObj;
            }
            return arr;
        }
        return org;
    }

    public void show()
    {

        dt = (DataTable)Session["Answered"];
        ListView v = lvTest;
        Label l = null;
        l = v.FindControl("Label1") as Label;
        l.Text = dt.Rows[ctr]["QU_SRNO"] + ".";


        l = v.FindControl("Label2") as Label;
        l.Text = dt.Rows[ctr]["QUESTIONTEXT"].ToString();

        RadioButtonList r = new RadioButtonList();
        r = v.FindControl("RadioButtonList1") as RadioButtonList;
        r.Items.Clear();

        r.Items.Add(dt.Rows[ctr]["ANS1TEXT"].ToString());
        r.Items.Add(dt.Rows[ctr]["ANS2TEXT"].ToString());
        r.Items.Add(dt.Rows[ctr]["ANS3TEXT"].ToString());
        r.Items.Add(dt.Rows[ctr]["ANS4TEXT"].ToString());
        r.Items.Add(dt.Rows[ctr]["ANS5TEXT"].ToString());
        r.Items.Add(dt.Rows[ctr]["ANS6TEXT"].ToString());
        r.SelectedIndex = Convert.ToInt32(dt.Rows[ctr]["selected"]);
        Session["ctr"] = ctr;
    }

    #endregion

    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Session["TOTUNANSQUESTION"] = no3;

            dt = (DataTable)Session["Answered"];
            foreach (DataRow drow in dt.Rows)
            {
                if (Convert.ToInt32(drow["SELECTED"]) != -1)
                {
                    no2 = no2 + 1;
                    Session["TOTANSQUE"] = no2;
                }
                else
                {
                    //no3 = 0;
                    no3 = no3 + 1;
                    Session["TOTUNANSQUESTION"] = no3;

                }
            }

            int TotQue = dt.Rows.Count;
            int StudAttempt;
            int Retest = 0;
            Session["TOTQUES"] = TotQue;

            objResult.TESTNO = Convert.ToInt32(Session["Test_No"].ToString());
            objResult.IDNO = Convert.ToInt32(Session["idno"]);
            //objResult.CORRECTMARKS = Convert.ToInt32(X["CORRECT_MARKS"]);
            objResult.TOTALMARKS = Convert.ToDecimal(Session["Total_Marks"]);
            objResult.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            objResult.COLLEGE_CODE = Session["colcode"].ToString().Trim();
            DataSet ds = objCommon.FillDropDown("ACD_ITEST_RESULT", "*", "", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND IDNO=" + Session["idno"], "");

            string ret = objCommon.LookUp("ACD_ITEST_RESULT", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND IDNO=" + Session["idno"]);
            string ret1 = objCommon.LookUp("ACD_ISTUDENTINFO", "MAX(ATTEMPTS_ALLOWED)", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND STUDENTID=" + Session["idno"]);
            if (ret1 == "1")
                Retest = 0;
            if (ds.Tables[0].Rows.Count == 0 || ret == "" || ret == null)
                StudAttempt = 0;
            else
                StudAttempt = Convert.ToInt32(ret);
            if (StudAttempt < Convert.ToInt32(Session["NOOFATTEMPT"]))
            {
                StudAttempt = StudAttempt + 1;
            }
            else if (StudAttempt == null || StudAttempt == 0)
                StudAttempt = 1;

            objResult.STUDATTEMPTS = StudAttempt;

            int cs = Convert.ToInt32(objIQBC.AddTestResult(objResult, Retest));
            {
                Server.Transfer("Thanks.aspx");
             //   Response.Redirect("Thanks.aspx");
            }
            //        if (cs != -99)
            //        {
            //            //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
            //        }

            //        string Flag = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRESULT", "TESTNO=" + Session["Test_No"]);
            //        if (Flag.Trim() == "Y")
            //        {

            //            Session["SHOWRESULT"] = "Y";
            //            Response.Redirect("Result.aspx");
            //        }
            //        else if (Flag.Trim() == "N")
            //        {
            //            //objCommon.DisplayUserMessage(UpdatePanel_Login, "Record Saved Sucessfully", this.Page);
            //            //Button3.Attributes.Add("OnClick", "window.close();");
            //            Session["SHOWRESULT"] = "N";
           
            //            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
            //            //this.Page.Response.Close();
            //        }
            //        //Response.Redirect("Result.aspx");
        }
        catch (Exception ex)
        {
 
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveAnswers();
            lblStatus.Visible = true;
            
            foreach (ListViewItem itemRow in lvTest.Items)
            {
                ImageButton img = (ImageButton)itemRow.FindControl("btnAnswer") as ImageButton;
                int quesno = int.Parse(img.CommandArgument);
                string answer = (objCommon.LookUp("ACD_ITLE_RESULTCOPY", "DESCRIPTIVE_ANSWER", "QUESTIONNO=" + quesno + "AND IDNO=" + Convert.ToInt32(Session["idno"]) + "and TESTNO=" + Convert.ToInt32(Session["Test_No"])));
                if (answer == string.Empty || answer == "")
                {
                    img.ImageUrl = "~/images/redArrow1.jpg";
                }
                else
                {
                    img.ImageUrl = "~/images/greenArrow1.jpg";
                }
            }
        }
        catch (Exception ex)
        {


        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.MessageBox.Show(this.lblTimerCount.Text);
    }

    protected void btnAnswer_Click(object sender, ImageClickEventArgs e)
    {

        try
        {

            lblStatus.Visible = false;
            //SaveAnswers();

            ImageButton imgReplyAnswer = sender as ImageButton;
            int quesno = int.Parse(imgReplyAnswer.CommandArgument);
            //if (quesno == Convert.ToInt32(Session["Question_no"]))
          
            int count = Convert.ToInt32(objCommon.LookUp("ACD_ITLE_RESULTCOPY", "isnull(COUNT(*),0)", "QUESTIONNO=" + quesno + "AND IDNO=" + Convert.ToInt32(Session["idno"]) + "and TESTNO=" + Convert.ToInt32(Session["Test_No"])));
            if (count > 0)
            {
                ViewState["action"] = "edit";
                ViewState["quesno"] = quesno;
                ShowDetails(quesno);
            }

            else
            {
                txtAnswer.Text = string.Empty;
                divDescriptiveAnswer.Visible = true;
                lblQuestion.Text = imgReplyAnswer.ToolTip;
                Session["Question_no"] = quesno;
                Session["Question_Marks"] = imgReplyAnswer.AlternateText;

                ViewState["quesno"] = quesno;
                ViewState["action"] = null;
            }


        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ClearControls();


    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        txtAnswer.Text = string.Empty;
        divDescriptiveAnswer.Visible = false;
    }

    protected void lvTest_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
       ImageButton img = e.Item.FindControl("btnAnswer") as ImageButton;

       // ImageButton imgReplyAnswer = e.Item.FindControl("btnAnswer") as ImageButton;

       int quesno = int.Parse(img.CommandArgument);
        //if (quesno == Convert.ToInt32(Session["Question_no"]))


        string answer =(objCommon.LookUp("ACD_ITLE_RESULTCOPY", "DESCRIPTIVE_ANSWER", "QUESTIONNO=" + quesno + "AND IDNO=" + Convert.ToInt32(Session["idno"]) + "and TESTNO=" + Convert.ToInt32(Session["Test_No"])));
        if (answer == string.Empty || answer == "")
        {
            img.ImageUrl = "~/images/redArrow1.jpg";
        }
        else
        {
            img.ImageUrl = "~/images/greenArrow1.jpg";
        }

        //DataSet ds = (objCommon.LookUp("ACD_ITLE_RESULTCOPY", "DESCRIPTIVE_ANSWER", "QUESTIONNO=" + quesno + "AND IDNO=" + Convert.ToInt32(Session["idno"]) + "and TESTNO=" + Convert.ToInt32(Session["Test_No"])));
        //if (ds != null && ds.Tables[0].Rows.Count > 0)
        //{
        //    foreach (DataTable table in ds.Tables)
        //    {
        //        foreach (DataRow dr in table.Rows)
              
        //        {
        //            string questions = ds.Tables[0].Rows[0]["DESCRIPTIVE_ANSWER"].ToString();
        //            if (questions != null && questions != "" && questions != string.Empty)
        //            {
        //                img.ImageUrl = "~/images/greenArrow1.jpg";
        //            }
        //            else
        //            {
        //                img.ImageUrl = "~/images/redArrow1.jpg";

        //            }
        //        }
        //    }
        //}


        //foreach (ListViewDataItem li in lvTest.Items)
    




        //if (ViewState["submit"] != null)
        //{
        //    ImageButton img = e.Item.FindControl("btnAnswer") as ImageButton;

        //    img.ImageUrl = "~/images/greenArrow1.jpg";
        //}
        //else
        //{
        //    ImageButton img = e.Item.FindControl("btnAnswer") as ImageButton;

        //    img.ImageUrl = "~/images/redArrow1.jpg";
        //}

       
    }

    #endregion

    #region Web Method

    [System.Web.Services.WebMethod]
    public static void GetCurrentTime(string i)
    {
        CheckOut = i;
    }

    #endregion

    #region Commented Code

    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    dt = (DataTable)Session["Answered"];

    //    foreach (ListViewDataItem li in lvTest.Items)
    //    {
    //        //int no2 = 0;
    //        RadioButtonList rl = ((RadioButtonList)li.FindControl("RadioButtonList1"));
    //        //if (rl.SelectedIndex > 0)
    //        //{
    //        //    no2 = no2 + 1;
    //        //    Session["TOTANSQUE"] = no2;
    //        //}
    //        //else
    //        //{
    //        //    no3 = no3 + 1;
    //        //    Session["TOTUNANSQUE"] = no3;
    //        //}
    //        Label lv = ((Label)li.FindControl("Label1"));
    //        int no = Convert.ToInt32(lv.Text);
    //        //no2 = no2 + Convert.ToInt32(lv.Text);

    //        foreach (DataRow drow in dt.Rows)
    //        {
    //            int no1 = Convert.ToInt32(drow["QU_SRNO"]);

    //            if (no == no1)
    //            {

    //                if (rl.SelectedValue != "")
    //                {
    //                    drow["SELECTED"] = Convert.ToString(rl.SelectedValue);
    //                    drow["SELECTEDOPTION"] = Convert.ToString(rl.SelectedItem);
    //                }
    //                //int m = Convert.ToInt32(rl.SelectedValue);

    //            }
    //            //Convert.ToInt32(drow["QUESTIONNO"])==Convert.ToInt32(Label1.Text)
    //        }

    //    }
    //    Session["Answered"] = dt;


    //}





    

    


    //protected void lblQuestions_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //LinkButton btnQuestion = sender as LinkButton;
    //        //Session["QuestionText"] = btnQuestion.Text.Trim();
    //        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Answer", "window.showModalDialog('Test_Descriptive_Answer.aspx','Reply Answer','addressbar=no,menubar=no,titlebar=0,scrollbars=1,statusbar=no,resizable=yes,dialogWidth:600px; dialogHeight:400px; dialogLeft:0px;dialogwidth:600px; dialogright:0px;');", true);//"','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');"

    //        //ModalPopupExtender1.Show();


    //        //Response.Redirect("Test_Descriptive_Answer.aspx");

    //    }

    //    catch (Exception ex)
    //    { 

    //    }


    //}
    //TextBox txtanswer;






    // Save descriptive single Answer 

    #endregion

    protected void btnUpdateMalFunction_Click(object sender, EventArgs e)
    {
        try
        {
            //lblAlert.Text = "Please do not switch window while test is in progress. Doing so " + hdnMalfunction.Value + " times will terminate the test, and you will not be able to get back to it.";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
            //// $('#myCommonAlertModal').modal('show');
            //int testNo = Convert.ToInt32(Session["Test_No"]);
            //int idno = Convert.ToInt32(Session["idno"].ToString());
            //int malfunction = hdnMalfunctionDesStud.Value == "" ? 0 : Convert.ToInt32(hdnMalfunctionDesStud.Value);
            //int count = malfunction + 1;
            //int ret = objAC.UpdateMalfunctionSwitchCount(testNo, idno, count, Convert.ToInt32(Session["ICourseNo"]));
            //hdnMalfunctionDesStud.Value = objCommon.LookUp("ACD_ISTUDENTINFO", "MalFunctionCount", "TESTNO=" + testNo + "  AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "  AND STUDENTID=" + idno).ToString();
            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);

           

        }
        catch (Exception ex)
        {

            // throw;
        }
    }


    protected void btnCloseModalPopup_Click(object sender, EventArgs e)
    {
        try
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnCloseModalPopup_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
