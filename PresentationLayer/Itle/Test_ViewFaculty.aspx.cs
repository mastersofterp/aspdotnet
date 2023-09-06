using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.SQLServer.SQLDAL;

using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class Itle_Test_ViewFaculty : System.Web.UI.Page
{
    Common objCommon = new Common();
    //UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objAC = new CourseControlleritle();
    ITestMaster objTest = new ITestMaster();
    TestResult objResult = new TestResult();
    IQuestionbank objQuest = new IQuestionbank();
    IQuestionbankController objIQBC = new IQuestionbankController();
    ITestMasterController objTestController = new ITestMasterController();
    //objAC = new CourseCntroller(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
    DataTable dt = new DataTable();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    int ctr = 0;
    long timerStartValue = 1800;
    int t = 0;
    int count = 0;
    int no2 = 0;
    int no3 = 0;
    DateTime date;
    DateTime mydate;
    static string CheckOut = string.Empty;

    #region Page Events


    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["CollegeId"],Session["UserName"],Session["Password"],
    /// Session["DataBase"].If the user has not Login, he will be redirected to "default.aspx" page. If there is request for first time for this 
    /// page then description for Page help is retrived from MENUHELPMASTER table and stored under Cache variable.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void Page_Load(object sender, EventArgs e)
    {

        string dt = Request.Cookies["start"].Value;
        mydate = Convert.ToDateTime(dt);
        //For displaying user friendly messages
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

        //Check Session


        //objIQBC = new IQuestionbankController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
        //objAC = new CourseControlleritle(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {


                Session["post"] = Convert.ToInt32(Session["post"]) + Convert.ToInt32(Session["post"]);



                if (Session["ICourseNo"] == null)
                    Response.Redirect("ITLE_Select_Course.aspx");

                DataSet ds = new DataSet();

               
                this.timerStartValue = long.Parse(Session["SEC"].ToString());
                this.TimerInterval = 500;

                lblSession.Text = Session["sessionname"].ToString();
                lblSession.ToolTip = Session["sessionname"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();
                objTest.TESTNO = Convert.ToInt32(Session["Test_No"]);
                // objTest.IDNO = Convert.ToInt32(Session["STUDID"]);
                objTest.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objTest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                HttpCookie Time = Request.Cookies["start"];
                string name = Session["username"].ToString();
                
                FetchStudInfo();
                //HttpCookie Time = Request.Cookies["start"];
                //string name = Session["useridname"].ToString();
                //name = name.Substring(0, name.IndexOf("@"));
                //lblSeatno.Text = objCommon.LookUp("student_result", "ROLLNO", "IDNO=" + name);

                BindShowQues();
                //COUNT();
                lblQueNo.Text = "1" + "/" + Convert.ToInt32(Session["NOQ"].ToString());
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Session["post"] = Convert.ToInt32(Session["post"]) + 1;

            string[] ctl = Request.Form.GetValues("__EVENTTARGET");

            if (Convert.ToInt32(Session["post"]) > 1 & ctl == null)
                Response.Redirect("Result.aspx");
            //else
            //    Response.Redirect(this.Request.Url.ToString(), false);

            //StringBuilder bldr = new StringBuilder();
            ////bldr.AppendFormat("var Timer = new myTimer({0},{1},'{2}','timerData');", this.timerStartValue, this.TimerInterval, this.lblTimerCount.ClientID);
            //bldr.Append("closewin()");
            //ClientScript.RegisterStartupScript(this.GetType(), "TimerScript", bldr.ToString(), true);
            //ClientScript.RegisterHiddenField("timerData", timerStartValue.ToString());


        }
    }
    #endregion


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

            if (Session["Test_Type"].ToString().Equals("Objective"))
            {
                // For Objective Type Test
                const int selectques = 3;
                DataRow dr = null;
                int[] a = new int[selectques - 1];
                ArrayList list = new ArrayList();

                int noofquestions = Convert.ToInt32(objCommon.LookUp("ACD_ITESTMASTER", "TOTQUESTION", "TESTNO=" + Session["Test_No"]));
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

                // ---------By Zubair Date:28-10-2014----------
                string QuesRandom = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRANDOM", "TESTNO=" + Session["Test_No"]);
                if (QuesRandom.Contains("N"))
                {
                    objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["userno"] + "_Faculty'");//Session["userno"]);
                    string questioSet = objCommon.LookUp("ACD_IQUESTION_SET_FOR_TEST", "QUESTION_SET", "TESTNO=" + Session["Test_No"]);
                    string MyQuery = "select  *, 1 AS CORRECT_MARKS,'" + Session["userno"] + "_Faculty' as StudentID" + ",NULL from ACD_IQUESTIONBANK where " + "COURSENO=" + Session["ICourseNo"] + "and QUESTIONNO in(" + questioSet + ")" + "AND QUESTION_TYPE='" + Session["Question_Type"].ToString() + "'" + " order by newid()";
                    CustomStatus cs = (CustomStatus)objTestController.AddITestMaster_Temp(MyQuery);
                }
                else
                {

                    //--------Tarun Date:16-1-2014---------

                    objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["userno"] + "_Faculty'");//Session["userno"]);

                    DataSet dsT = objCommon.FillDropDown("ACD_ITESTMASTER", "TOPICS,QUESTIONRATIO", "QUESTIONRATIO", "testno=" + Session["Test_No"], "");//objSqlHelper.ExecuteDataSet(Query);
                    if (!string.IsNullOrEmpty(dsT.Tables[0].Rows[0]["TOPICS"].ToString()))
                    {
                        string QRem = dsT.Tables[0].Rows[0]["QUESTIONRATIO"].ToString();
                        string Rem = dsT.Tables[0].Rows[0]["TOPICS"].ToString();

                        //Rem = Rem.Replace("'", "");
                        QRem = QRem.Replace("'", "");

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


                                    string MyQuery = "select TOP " + QRatio + " *, 1 AS CORRECT_MARKS,'" + Session["userno"] + "_Faculty' as StudentID" + ",NULL from ACD_IQUESTIONBANK where " + "COURSENO=" + Session["ICourseNo"] + "and Topic in(" + QTopic + ")" + "AND QUESTION_TYPE='" + Session["Question_Type"].ToString() + "'" + " order by newid()";
                                    CustomStatus cs = (CustomStatus)objTestController.AddITestMaster_Temp(MyQuery);


                                }
                            }
                        }
                    }
                    //--------End---------
                }

                char fullRandomeTest = Convert.ToChar(objCommon.LookUp("ACD_ITESTMASTER", "ISNULL(FULL_RANDOME_TEST,'N')", "TESTNO=" + Session["Test_No"]).Trim());
                string sql;
                if (fullRandomeTest == 'Y')
                {
                    sql = "select * from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["userno"] + "_Faculty'" + "";
                }
                else
                {
                    sql = "select * from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["userno"] + "_Faculty'" + " order by Topic,Questionno";
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
                        dtRe.Columns.Add(new DataColumn("CORRECTANS", typeof(string)));
                        dtRe.Columns.Add(new DataColumn("SELECTED", typeof(string)));

                        dtRe.Columns.Add(new DataColumn("CORRECT_MARKS", typeof(decimal)));
                        dtRe.Columns.Add(new DataColumn("USERNO", typeof(int)));
                        dtRe.Columns.Add(new DataColumn("SELECTEDOPTION", typeof(object)));
                        dtRe.Columns.Add(new DataColumn("TYPE", typeof(char)));

                        //foreach (DataRow r in ds.Tables[0].Rows)
                        //{
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            //int g = Convert.ToInt32(_myArr.GetValue(j));

                            //if (g.Equals(Convert.ToInt32(ds.Tables[0].Rows[g - 1]["QU_SRNO"].ToString())))
                            //{

                            dr = dtRe.NewRow();
                            dr["QU_SRNO"] = dtRe.Rows.Count + 1;
                            dr["QUENO"] = ds.Tables[0].Rows[j]["QUESTIONNO"].ToString();
                            dr["QUESTIONTEXT"] = ds.Tables[0].Rows[j]["QUESTIONTEXT"].ToString().Replace("font-size:", "").Replace("<p>", "");

                            dr["ANS1TEXT"] = ds.Tables[0].Rows[j]["ANS1TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "");
                            dr["ANS2TEXT"] = ds.Tables[0].Rows[j]["ANS2TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "");
                            dr["ANS3TEXT"] = ds.Tables[0].Rows[j]["ANS3TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "");
                            dr["ANS4TEXT"] = ds.Tables[0].Rows[j]["ANS4TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "");
                            dr["ANS5TEXT"] = ds.Tables[0].Rows[j]["ANS5TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "");
                            dr["ANS6TEXT"] = ds.Tables[0].Rows[j]["ANS6TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "");
                            if (ds.Tables[0].Rows[j]["QUESTIONIMAGE"].ToString() == "")
                                dr["QUESTIONIMAGE"] = null;
                            else


                            dr["QUESTIONIMAGE"] = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[j]["QUESTIONIMAGE"].ToString();
                            dr["ANS1IMG"] =ds.Tables[0].Rows[j]["ANS1IMG"].ToString();
                            dr["ANS2IMG"] = ds.Tables[0].Rows[j]["ANS2IMG"].ToString();
                            dr["ANS3IMG"] =  ds.Tables[0].Rows[j]["ANS3IMG"].ToString();
                            dr["ANS4IMG"] =  ds.Tables[0].Rows[j]["ANS4IMG"].ToString();
                            dr["ANS5IMG"] =  ds.Tables[0].Rows[j]["ANS5IMG"].ToString();
                            dr["ANS6IMG"] =  ds.Tables[0].Rows[j]["ANS6IMG"].ToString();
                            dr["CORRECTANS"] = ds.Tables[0].Rows[j]["CORRECTANS"].ToString();
                            dr["CORRECT_MARKS"] = ds.Tables[0].Rows[j]["CORRECT_MARKS"].ToString();

                            dr["SELECTED"] = "-1";
                            dr["USERNO"] = Convert.ToInt32(Session["userno"].ToString());

                            dr["TYPE"] = ds.Tables[0].Rows[j]["TYPE"].ToString();

                            dtRe.Rows.Add(dr);


                            //}

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
                                img.Visible = true;

                            //if (ds1.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim() == "Y")
                            //{

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
                            string imageBankFolder  ="Itle_QuestionImage.aspx?FileName=";
                            //if (ds1.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim() == "Y")
                            //{

                            for (int j = 0; j < 6; j++)
                            {
                                int g = Convert.ToInt32(_myArrOp.GetValue(j));

                                //r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS1TEXT"].ToString(), "1"));
                                if (dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() != "")
                                    if (g == Convert.ToInt32(dtTbl.Rows[i]["CORRECTANS"]))
                                        r.Items.Add(new ListItem("<img src=" +imageBankFolder+ dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() + " width=70px>" + dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), dtTbl.Rows[i]["CORRECTANS"].ToString()));
                                    else
                                        r.Items.Add(new ListItem("<img src="+imageBankFolder+ dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() + " width=70px>" + dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), "0"));
                            }


                        }
                        i++;

                    }
                }



            }

            //////
            //// For Descriptive Type Test
                else
                {
                    form1.Visible = false;
                    form2.Visible = true;
                    const int selectques = 3;
                    DataRow dr = null;
                    int[] a = new int[selectques - 1];
                    ArrayList list = new ArrayList();
                    //int noofquestions = Convert.ToInt32(objCommon.LookUp("ITLE_TESTQUESTION", "COUNT(TESTNO)", "TESTNO=" + Convert.ToInt32(Session["Test_No"])));
                    int noofquestions = Convert.ToInt32(objCommon.LookUp("ACD_ITESTMASTER", "TOTQUESTION", "TESTNO=" + Session["Test_No"]));
                    Session["NOQ"] = Convert.ToInt32(noofquestions);
                    Session["noofquestions"] = noofquestions - 2;

                    int[] _myArr = new int[noofquestions];

                    for (int i = 0; i < noofquestions; i++)
                        _myArr[i] = i + 1;


                    SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                    //String sql1 = "select * from ITLE_QUESTIONBANK where " + "SDSRNO=" + Convert.ToInt32(Session["SDSRNO"]);

                    //DataSet ds1 = objSqlHelper.ExecuteDataSet(sql1);
                    //for(int x=0; x < ds1.Tables[0].Rows.Count; x++)
                    //{
                    //    string[] _myArrs = new string[noofquestions];
                    //    _myArrs[x] = ds1.Tables[0].Rows[x]["QUESTIONTEXT"].ToString().Trim()
                    //}


                    //if (ds1.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim() == "Y")
                    //_myArr = (int[])Shuffle(_myArr);

                    int[] _myArrOp = new int[6];
                    for (int i = 0; i < 6; i++)
                        _myArrOp[i] = i + 1;

                    _myArrOp = (int[])Shuffle(_myArrOp);

                 // ---------By Zubair Date:28-10-2014----------
                string QuesRandom = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRANDOM", "TESTNO=" + Session["Test_No"]);
                if (QuesRandom.Contains("N"))
                {
                    objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["userno"] + "_Faculty'");//Session["userno"]);
                    string questioSet = objCommon.LookUp("ACD_IQUESTION_SET_FOR_TEST", "QUESTION_SET", "TESTNO=" + Session["Test_No"]);
                    string MyQuery = "select  *, 1 AS CORRECT_MARKS,'" + Session["userno"] + "_Faculty' as StudentID" + " from ACD_IQUESTIONBANK where " + "COURSENO=" + Session["ICourseNo"] + "and QUESTIONNO in(" + questioSet + ")" + "AND QUESTION_TYPE='" + Session["Question_Type"].ToString() + "'" + " order by newid()";
                    CustomStatus cs = (CustomStatus)objTestController.AddITestMaster_Temp(MyQuery);
                }
                else
                {
                    //--------Tarun Date:16-1-2014---------
                    objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["userno"] + "_Faculty'");//Session["userno"]);

                    DataSet dsT = objCommon.FillDropDown("ACD_ITESTMASTER", "TOPICS,QUESTIONRATIO,MARKS_FOR_QUESTION", "QUESTIONRATIO", "testno=" + Session["Test_No"], "");//objSqlHelper.ExecuteDataSet(Query);


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
                                    string MyQuery = "select TOP " + QRatio + " *, 1 AS CORRECT_MARKS,'" + Session["userno"] + "_Faculty' as StudentID" + " from ACD_IQUESTIONBANK where " + "COURSENO=" + objTest.COURSENO + "and Topic in(" + QTopic + ")" + "and MARKS_FOR_QUESTION in(" + MarkRatio + ")" + " order by newid()";
                                    CustomStatus cs = (CustomStatus)objTestController.AddITestMaster_Temp(MyQuery);

                                    //dtr = objSQL.ExecuteDataSetSP("PKG_ITLE_GET_FEEDBACK_QUESTIONMASTER", objParams).Tables[0].CreateDataReader(); ;
                                    ////dtRAll=objSqlHelper.ExecuteDataSet(MyQuery).Tables[0];
                                }
                            }
                        }
                    }
                }
                    //--------End---------
                    //---Tarun Comment following code------this is old pattern ---
                    ////string GetTopic = (objCommon.LookUp("itle_testmaster", "topics", "testno=" + objTest.TEST_NO));
                    ////String sql = "";
                    //////SQLHelper objSqlHelper = new SQLHelper(objIQBC._client_constr);
                    //////String sql = "SELECT T.QU_SRNO,Q.QUESTIONIMAGE,TYPE,Q.QUESTIONTEXT,Q.ANS1TEXT,ANS1IMG,ANS2IMG,ANS3IMG,ANS4IMG,ANS5IMG,ANS6IMG,ANS2TEXT,ANS3TEXT,ANS4TEXT,ANS5TEXT,ANS6TEXT,CORRECTANS,T.CORRECT_MARKS FROM ITLE_QUESTIONBANK Q INNER JOIN  ITLE_TESTQUESTION  T ON (T.QUESTIONNO=Q.QUESTIONNO)WHERE T.TESTNO=" + Convert.ToInt32(objTest.TEST_NO);
                    ////if (string.IsNullOrEmpty(GetTopic))
                    ////{
                    ////    sql = "select TOP " + noofquestions + " *, 1 AS CORRECT_MARKS from ITLE_QUESTIONBANK where " + "SDSRNO=" + objTest.SDSRNO + " order by newid()";
                    ////}
                    ////else
                    ////{
                    ////    sql = "select TOP " + noofquestions + " *, 1 AS CORRECT_MARKS from ITLE_QUESTIONBANK where " + "SDSRNO=" + objTest.SDSRNO + "and Topicno in(" + GetTopic + ")" + " order by newid()";
                    ////}
                    string sql = "select * from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["userno"] + "_Faculty'" + " order by Topic,Questionno";

                    DataSet ds = objSqlHelper.ExecuteDataSet(sql);

                    //FOR INSERTING QUESTION LIST INTO RESULTCOPY TABLE BY IDNO IN DESCRIPTIVE TEST
                    // 13/03/2014
                    //string Insertsql = "select QUESTIONNO AS QUESTIONNO,QUESTIONTEXT AS QUESTIONTEXT,MARKS_FOR_QUESTION AS QUESTION_MARKS,COURSENO AS COURSENO," + Session["Test_No"] + " AS TESTNO," + Session["idno"] + "from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "' order by Topic,Questionno";
                    //CustomStatus acs = (CustomStatus)objTestc.AddITestResultCopy(Insertsql);

                    int TotQue = ds.Tables[0].Rows.Count;
                    Session["TOTQUES"] = TotQue;


                    for (int i = 0; i < _myArr.Length; i++)
                    {

                        //SQLHelper objSqlHelper = new SQLHelper(_client_constr);
                        ////String sql = "SELECT T.QU_SRNO,Q.QUESTIONTEXT,Q.ANS1TEXT,ANS2TEXT,ANS3TEXT,ANS4TEXT,CORRECTANS FROM ACD_IQUESTIONBANK Q INNER JOIN  ACD_ITESTQUESTION  T ON (T.QUESTIONNO=Q.QUESTIONNO)WHERE T.QU_SRNO=" + _myArr[i] + "AND Q.COURSENO=" + objTest.COURSENO + "AND T.TESTNO=" + Convert.ToInt32(objTest.TEST_NO);
                        //String sql = "SELECT T.QU_SRNO,Q.QUESTIONTEXT,Q.ANS1TEXT,ANS2TEXT,ANS3TEXT,ANS4TEXT,CORRECTANS FROM ACD_IQUESTIONBANK Q INNER JOIN  ACD_ITESTQUESTION  T ON (T.QUESTIONNO=Q.QUESTIONNO)WHERE Q.COURSENO=" + objTest.COURSENO + "AND T.TESTNO=" + Convert.ToInt32(objTest.TEST_NO);

                        //DataSet ds = objSqlHelper.ExecuteDataSet(sql);

                        //DataSet ds = objTestc.GetAllQuestionByTestNo(objTest);
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
                            dtRe.Columns.Add(new DataColumn("CORRECTANS", typeof(string)));
                            dtRe.Columns.Add(new DataColumn("SELECTED", typeof(string)));
                            dtRe.Columns.Add(new DataColumn("CORRECT_MARKS", typeof(decimal)));
                            dtRe.Columns.Add(new DataColumn("USERNO", typeof(int)));
                            dtRe.Columns.Add(new DataColumn("SELECTEDOPTION", typeof(object)));
                            dtRe.Columns.Add(new DataColumn("TYPE", typeof(char)));
                            dtRe.Columns.Add(new DataColumn("MARKS_FOR_QUESTION", typeof(int)));

                            //foreach (DataRow r in ds.Tables[0].Rows)
                            //{
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
                                    dr["QUESTIONIMAGE"] = null;
                                else



                                    dr["QUESTIONIMAGE"] = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[j]["QUESTIONIMAGE"].ToString();
                                dr["ANS1IMG"] = ds.Tables[0].Rows[j]["ANS1IMG"].ToString();
                                dr["ANS2IMG"] = ds.Tables[0].Rows[j]["ANS2IMG"].ToString();
                                dr["ANS3IMG"] = ds.Tables[0].Rows[j]["ANS3IMG"].ToString();
                                dr["ANS4IMG"] = ds.Tables[0].Rows[j]["ANS4IMG"].ToString();
                                dr["ANS5IMG"] = ds.Tables[0].Rows[j]["ANS5IMG"].ToString();
                                dr["ANS6IMG"] = ds.Tables[0].Rows[j]["ANS6IMG"].ToString();
                                dr["CORRECTANS"] = ds.Tables[0].Rows[j]["CORRECTANS"].ToString();
                                dr["CORRECT_MARKS"] = ds.Tables[0].Rows[j]["CORRECT_MARKS"].ToString();

                                dr["SELECTED"] = "-1";
                                dr["USERNO"] = Convert.ToInt32(Session["userno"].ToString());

                                dr["TYPE"] = ds.Tables[0].Rows[j]["TYPE"].ToString();
                                dr["MARKS_FOR_QUESTION"] = ds.Tables[0].Rows[j]["MARKS_FOR_QUESTION"].ToString();
                                dtRe.Rows.Add(dr);




                                Session["Answered"] = dtRe;
                            }
                        }





                    }





                    ListView1.DataSource = Session["Answered"];


                    ListView1.DataBind();

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
                                    img.Visible = true;

                                //if (ds1.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim() == "Y")
                                //{

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
                                //if (ds1.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim() == "Y")
                                //{

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
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Test.BindShowQues -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FetchStudInfo()
    {
        //DataSet dt = objAC.GetTestInfo(Convert.ToInt32(Session["userno"]));
        //if (dt != null)
        lblUrname.Text = Session["userfullname"].ToString().Trim();//dt.Tables[0].Rows[0]["userfullname"].ToString();
        lblSession.Text = Session["SESSION_NAME"].ToString();
        lblSession.ToolTip = Session["SessionNo"].ToString();
        lblCourse.Text = Session["ICourseName"].ToString();//dt.Tables[0].Rows[0]["SUBJECTNAME"].ToString();
        lblTestName.Text = Session["TestName"].ToString();//dt.Tables[0].Rows[0]["TESTNAME"].ToString();
        lblSeatno.Text = Session["userno"].ToString();

        // For Descriptive Test 30/04/2014
                
        lblUrnamefrm2.Text = Session["userfullname"].ToString().Trim();
        lblSessionfrm2.Text = Session["SESSION_NAME"].ToString();
        lblSessionfrm2.ToolTip = Session["SessionNo"].ToString();
        lblCoursefrm2.Text = Session["ICourseName"].ToString();
        lblTestNamefrm2.Text = Session["TestName"].ToString();
        lblSeatnofrm2.Text = Session["userno"].ToString();
        lblTotTime.Text = Convert.ToDateTime(Session["Total_Time_For_Test"]).ToString("HH:mm:ss");

        //Session["SDSRNO"] = dt.Tables[0].Rows[0]["sdsrno"].ToString();

        //string STUDID = dt.Tables[0].Rows[0]["USER_ID"].ToString();
        //STUDID = STUDID.Remove(STUDID.IndexOf('@'));

         //objCommon.LookUp("student_result", "ROLLNO", "IDNO=" + STUDID);
        //objTest.TEST_NO = Convert.ToInt32(dt.Tables[0].Rows[0]["testno"].ToString());
        //objTest.UA_NO = Convert.ToInt32(Session["userno"]);
        //objTest.SDSRNO = Convert.ToInt32(dt.Tables[0].Rows[0]["sdsrno"].ToString());

        //objResult.TESTNO = objTest.TEST_NO;
        //objResult.IDNO = Convert.ToInt32(STUDID);
        //objResult.SDSRNO = objTest.SDSRNO;
    }

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
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = (DataTable)Session["Answered"];

        foreach (ListViewDataItem li in lvTest.Items)
        {
            //int no2 = 0;
            RadioButtonList rl = ((RadioButtonList)li.FindControl("RadioButtonList1"));
            //if (rl.SelectedIndex > 0)
            //{
            //    no2 = no2 + 1;
            //    Session["TOTANSQUE"] = no2;
            //}
            //else
            //{
            //    no3 = no3 + 1;
            //    Session["TOTUNANSQUE"] = no3;
            //}
            Label lv = ((Label)li.FindControl("Label1"));
            int no = Convert.ToInt32(lv.Text);
            //no2 = no2 + Convert.ToInt32(lv.Text);

            foreach (DataRow drow in dt.Rows)
            {
                int no1 = Convert.ToInt32(drow["QU_SRNO"]);

                if (no == no1)
                {

                    if (rl.SelectedValue != "")
                    {
                        drow["SELECTED"] = Convert.ToString(rl.SelectedValue);
                        drow["SELECTEDOPTION"] = Convert.ToString(rl.SelectedItem);
                    }
                    //int m = Convert.ToInt32(rl.SelectedValue);

                }
                //Convert.ToInt32(drow["QUESTIONNO"])==Convert.ToInt32(Label1.Text)
            }

        }
        Session["Answered"] = dt;


    }
    //protected void Button3_Click(object sender, EventArgs e)
    //{

    //    //if (System.Windows.Forms.MessageBox.Show("Validated?", "Validate", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
    //    //    return;

    //    Session["TOTUNANSQUESTION"] = no3;


    //    dt = (DataTable)Session["Answered"];
    //    foreach (DataRow drow in dt.Rows)
    //    {
    //        if (Convert.ToInt32(drow["SELECTED"]) != -1)
    //        {
    //            no2 = no2 + 1;
    //            Session["TOTANSQUE"] = no2;
    //        }
    //        else
    //        {
    //            //no3 = 0;
    //            no3 = no3 + 1;
    //            Session["TOTUNANSQUESTION"] = no3;

    //        }
    //    }

    //    decimal TOTALMARKS = 0.0m;
    //    int NOOFCORRECTANS = 0;
    //    int TotQue = dt.Rows.Count;
    //    int StudAttempt;
    //    int Retest = 0;
    //    Session["TOTQUES"] = TotQue;

    //    objResult.TESTNO = Convert.ToInt32(Session["Test_No"]);
    //    objResult.IDNO = Convert.ToInt32(Session["STUDID"]);
    //    int count = Convert.ToInt32(objCommon.LookUp("ITLE_RESULTCOPY", "isnull(COUNT(*),0)", "ua_no = " + objResult.IDNO + " and testno=" + objResult.TESTNO));
    //    if (count > 0)
    //        objCommon.DeleteClientTableRow("ITLE_RESULTCOPY", "ua_no = " + objResult.IDNO + " and testno=" + objResult.TESTNO);

    //    foreach (DataRow X in dt.Rows)
    //    {
    //        objResult.CORRECTMARKS += Convert.ToInt32(X["CORRECT_MARKS"]);
    //        if ((X["SELECTED"].ToString()) == (X["CORRECTANS"].ToString()))
    //        {


    //            TOTALMARKS += Convert.ToDecimal(X["CORRECT_MARKS"].ToString());
    //            Session["TOTSCORE"] = Convert.ToInt32(TOTALMARKS);
    //            NOOFCORRECTANS += 1;
    //            Session["NOCORRANS"] = NOOFCORRECTANS;
    //            //Response.Redirect("Result.aspx");

    //            //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);

    //        }

    //        objQuest.QUESTIONNO = Convert.ToInt32(X["queno"]);
    //        objQuest.QUESTIONTEXT = X["questiontext"].ToString();
    //        objQuest.SELECTED = (X["SELECTED"]).ToString();
    //        objQuest.FTBANS = X["selectedoption"].ToString();
    //        objQuest.CORRECTANS = (X["correctans"]).ToString();
    //        objQuest.COLLEGE_CODE = Session["DataBase"].ToString().Trim();
    //        objQuest.UA_NO = Convert.ToInt32(Session["STUDID"]);
    //        objQuest.TEST_NO = Convert.ToInt32(Session["Test_No"]);


    //        int csi = Convert.ToInt32(objIQBC.AddTestResultDetails(objQuest));
    //    }
    //    objResult.TESTNO = Convert.ToInt32(Session["Test_No"]);
    //    objResult.IDNO = Convert.ToInt32(Session["STUDID"]);
    //    //objResult.CORRECTMARKS = Convert.ToInt32(X["CORRECT_MARKS"]);
    //    objResult.TOTALMARKS = Convert.ToInt32(TOTALMARKS);
    //    objResult.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
    //    //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);
    //    objResult.COLLEGE_CODE = Session["DataBase"].ToString().Trim();
    //    DataSet ds = objCommon.FillDropDown("ITLE_TESTRESULT", "*", "", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND IDNO=" + Session["STUDID"], "");

    //    string ret = objCommon.LookUp("ITLE_TESTRESULT", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND IDNO=" + Session["STUDID"]);
    //    string ret1 = objCommon.LookUp("ITLE_STUDENTINFO", "MAX(ATTEMPTS_ALLOWED)", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND STUDENTID='" + Session["userno"] + "_Faculty'");
    //    if (ret1 == "1")
    //        Retest = 0;
    //    if (ds.Tables[0].Rows.Count == 0 || ret == "" || ret == null)
    //        StudAttempt = 0;
    //    else
    //        StudAttempt = Convert.ToInt32(ret);
    //    if (StudAttempt < Convert.ToInt32(Session["NOOFATTEMPT"]))
    //    {
    //        StudAttempt = StudAttempt + 1;
    //    }
    //    else if (StudAttempt == null || StudAttempt == 0)
    //        StudAttempt = 1;

    //    objResult.STUDATTEMPTS = StudAttempt;
    //    int cs = Convert.ToInt32(objIQBC.AddTestResult(objResult, Retest));
    //    if (cs != -99)
    //    {
    //        //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
    //    }

    //    string Flag = objCommon.LookUp("ITLE_TESTMASTER", "SHOWRESULT", "TESTNO=" + Session["Test_No"]);
    //    if (Flag.Trim() == "Y")
    //    {

    //        Session["SHOWRESULT"] = "Y";
    //        Response.Redirect("Result.aspx");
    //    }
    //    else if (Flag.Trim() == "N")
    //    {
    //        //objCommon.DisplayUserMessage(UpdatePanel_Login, "Record Saved Sucessfully", this.Page);
    //        //Button3.Attributes.Add("OnClick", "window.close();");
    //        Session["SHOWRESULT"] = "N";
    //        Response.Redirect("Thanx.aspx");
    //        //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
    //        //this.Page.Response.Close();
    //    }
    //    //Response.Redirect("Result.aspx");

    //}

    //protected void btnhdn_Click(object sender, EventArgs e)
    //{

    //    //if (System.Windows.Forms.MessageBox.Show("Validated?", "Validate", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
    //    //    return;

    //    Session["TOTUNANSQUESTION"] = no3;


    //    dt = (DataTable)Session["Answered"];
    //    foreach (DataRow drow in dt.Rows)
    //    {
    //        if (Convert.ToInt32(drow["SELECTED"]) != -1)
    //        {
    //            no2 = no2 + 1;
    //            Session["TOTANSQUE"] = no2;
    //        }
    //        else
    //        {
    //            //no3 = 0;
    //            no3 = no3 + 1;
    //            Session["TOTUNANSQUESTION"] = no3;

    //        }
    //    }

    //    decimal TOTALMARKS = 0.0m;
    //    int NOOFCORRECTANS = 0;
    //    int TotQue = dt.Rows.Count;
    //    int StudAttempt;
    //    int Retest = 0;
    //    Session["TOTQUES"] = TotQue;
    //    foreach (DataRow X in dt.Rows)
    //    {
    //        objResult.CORRECTMARKS += Convert.ToInt32(X["CORRECT_MARKS"]);
    //        if (Convert.ToInt32(X["SELECTED"].ToString()) == Convert.ToInt32(X["CORRECTANS"].ToString()))
    //        {
    //            TOTALMARKS += Convert.ToDecimal(X["CORRECT_MARKS"].ToString());
    //            Session["TOTSCORE"] = Convert.ToInt32(TOTALMARKS);
    //            NOOFCORRECTANS += 1;
    //            Session["NOCORRANS"] = NOOFCORRECTANS;
    //            //Response.Redirect("Result.aspx");

    //            //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);

    //        }
    //    }
    //    objResult.TESTNO = Convert.ToInt32(Session["Test_No"]);
    //    objResult.IDNO = Convert.ToInt32(Session["STUDID"]);
    //    //objResult.CORRECTMARKS = Convert.ToInt32(X["CORRECT_MARKS"]);
    //    objResult.TOTALMARKS = Convert.ToInt32(TOTALMARKS);
    //    objResult.SDSRNO = Convert.ToInt32(Session["SDSRNO"]);
    //    //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);
    //    objResult.COLLEGE_CODE = Session["DataBase"].ToString().Trim();
    //    DataSet ds = objCommon.FillDropDown("ITLE_TESTRESULT", "*", "", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND IDNO=" + Session["STUDID"], "");

    //    string ret = objCommon.LookUp("ITLE_TESTRESULT", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND IDNO=" + Session["STUDID"]);
    //    string ret1 = objCommon.LookUp("ITLE_STUDENTINFO", "MAX(ATTEMPTS_ALLOWED)", "TESTNO=" + Convert.ToInt32(objResult.TESTNO) + " AND STUDENTID=" + Session["STUDID"]);
    //    if (ret1 == "1")
    //        Retest = 0;
    //    if (ds.Tables[0].Rows.Count == 0 || ret == "" || ret == null)
    //        StudAttempt = 0;
    //    else
    //        StudAttempt = Convert.ToInt32(ret);
    //    if (StudAttempt < Convert.ToInt32(Session["NOOFATTEMPT"]))
    //    {
    //        StudAttempt = StudAttempt + 1;
    //    }
    //    else if (StudAttempt == null || StudAttempt == 0)
    //        StudAttempt = 1;

    //    objResult.STUDATTEMPTS = StudAttempt;
    //    int cs = Convert.ToInt32(objIQBC.AddTestResult(objResult, Retest));
    //    if (cs != -99)
    //    {
    //        objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
    //    }

    //    string Flag = objCommon.LookUp("ITLE_TESTMASTER", "SHOWRESULT", "TESTNO=" + Session["Test_No"]);
    //    if (Flag.Trim() == "Y")
    //    {

    //        Session["SHOWRESULT"] = "Y";
    //        Response.Redirect("Result.aspx");
    //    }
    //    else if (Flag.Trim() == "N")
    //    {
    //        //objCommon.DisplayUserMessage(UpdatePanel_Login, "Record Saved Sucessfully", this.Page);
    //        //Button3.Attributes.Add("OnClick", "window.close();");
    //        Session["SHOWRESULT"] = "N";
    //        Response.Redirect("Thanx.aspx");
    //        //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
    //        //this.Page.Response.Close();
    //    }
    //    //Response.Redirect("Result.aspx");

    //}


    //private void old code()
    //{
    //    if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            DataTable dtTbl = (DataTable)Session["Answered"];


    //            foreach (ListViewDataItem li in lvTest.Items)
    //            {

    //                int i = 0;

    //                if (ds.Tables[0].Rows[i]["TYPE"].Equals("T"))
    //                {
    //                    Label lblQ = (Label)li.FindControl("Label1");
    //                    lblQ.Text = ds.Tables[0].Rows[i]["QU_SRNO"].ToString() + ".";

    //                    Label lblQuestion = (Label)li.FindControl("Label2");
    //                    lblQuestion.Text = ds.Tables[0].Rows[i]["QUESTIONTEXT"].ToString();

    //                    RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;
    //                    r.Items.Add(ds.Tables[0].Rows[i]["ANS1TEXT"].ToString());
    //                    r.Items.Add(ds.Tables[0].Rows[i]["ANS2TEXT"].ToString());
    //                    r.Items.Add(ds.Tables[0].Rows[i]["ANS3TEXT"].ToString());
    //                    r.Items.Add(ds.Tables[0].Rows[i]["ANS4TEXT"].ToString());
    //                    ((RadioButtonList)lvTest.FindControl("RadioButtonList1")).Items.Add(ds.Tables[0].Rows[0]["ANS1TEXT"].ToString());
    //                }
    //                else
    //                {
    //                    string imageBankFolder = "IMAGE_QUESTION/";

    //                    Label lblQ = (Label)li.FindControl("Label1");
    //                    lblQ.Text = ds.Tables[0].Rows[i]["QU_SRNO"].ToString() + ".";

    //                    Label lblQuestion = (Label)li.FindControl("Label2");
    //                    lblQuestion.Text = ds.Tables[0].Rows[i]["QUESTIONTEXT"].ToString();
    //                    RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;

    //                    r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS1IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS1TEXT"].ToString());
    //                    r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS2IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS2TEXT"].ToString());
    //                    r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS3IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS3TEXT"].ToString());
    //                    r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS4IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS4TEXT"].ToString());

    //                }


    //            }
    //        }
    //}


    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    Server.Transfer("ITLE_StudTest.aspx", true);

    //}

    //protected void Timer1_Tick(object sender, EventArgs e)
    //{
    //    ////DateTime mydate2 = new DateTime();
    //    //DateTime mydate2;
    //    //mydate2 = System.DateTime.Now;

    //    //string mydate3;

    //    //try
    //    //{
    //    //    mydate3 = (mydate - mydate2).ToString();
    //    //    lblTimerCount.Text = "Time Left: " + mydate3.ToString();
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    objCommon.DisplayUserMessage("Error Setting up the Timer. Contact Admin", this);
    //    //    //    Me.Label5.Text = "Error Setting up the Timer. Contact Admin"
    //    //    //End Try
    //    //}

    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        //System.Windows.Forms.MessageBox.Show(this.lblTimerCount.Text);
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


            ShowDetails(quesno);


            //int count = Convert.ToInt32(objCommon.LookUp("ACD_ITLE_RESULTCOPY", "isnull(COUNT(*),0)", "QUESTIONNO=" + quesno + "AND IDNO=" + Convert.ToInt32(Session["idno"]) + "and TESTNO=" + Convert.ToInt32(Session["Test_No"])));
            //if (count > 0)
            //{
            //    ViewState["action"] = "edit";
               
            //}

            //else
            //{
           // txtAnswer.Text = string.Empty;
            divDescriptiveAnswer.Visible = true;
            lblQuestion.Text = imgReplyAnswer.ToolTip;
            Session["Question_no"] = quesno;
            Session["Question_Marks"] = imgReplyAnswer.AlternateText;

            //}



        }
        catch (Exception ex)
        {

        }
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


                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
           
        }

    }

    //Used to get activate when time out occure without submitting...
    [System.Web.Services.WebMethod]
    public static void GetCurrentTime(string i)
    {
        CheckOut = i;
        //ITLE_Test obj = new ITLE_Test();
        //obj.Button3.Click += new System.EventHandler(obj.Button3_Click);
        //obj.Button3_Click(obj.Events.,null);
        //ITLE_Test obj = new ITLE_Test();
        //int StudAttempt = 0;
        //TestResult objResult = new TestResult();
        //IQuestionbankController objIQBC = new IQuestionbankController();
        //objResult.STUDATTEMPTS = StudAttempt + 1;
        //objResult.TESTNO = Convert.ToInt32(obj.Session["Test_No"]);
        //objResult.IDNO = Convert.ToInt32(obj.Session["STUDID"]);
        //objResult.SDSRNO = Convert.ToInt32(obj.Session["SDSRNO"]);
        //objResult.CORRECTMARKS = 0;
        //objResult.TOTALMARKS = 0;

        ////objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);
        //objResult.COLLEGE_CODE = "BIRLA";
        //try
        //{
        //    SqlConnection myconnection;
        //    //myconnection = new SqlConnection(@"Password=iitms!123;User ID=sa; SERVER=IITMSSERVER\MSSQL2008R2;DataBase=" + obj.Session["DataBase"].ToString().Trim() + ";");

        //    objIQBC = new IQuestionbankController(obj.Session["UserName"].ToString().Trim(), obj.Session["Password"].ToString().Trim(), obj.Session["DataBase"].ToString().Trim());
        //    myconnection = new SqlConnection(objIQBC._client_constr);
        //    //myconnection.ConnectionString = "@" + obj._client_constr;  // Set database path to connection string
        //    myconnection.Open();
        //    SqlCommand cmd = new SqlCommand("Insert into ITLE_TESTRESULT(TESTNO,IDNO,SDSRNO,TOTALMARKS,CORRECTMARKS,TESTDATE,COLLEGE_CODE,STUDATTEMPTS)" +
        //          " values(" + objResult.TESTNO + "," + objResult.IDNO + "," + objResult.SDSRNO + "," + 0 + "," + 0 + ",GETDATE(),'BIRLA',(select max(studattempts) from ITLE_TESTRESULT where testno=" + objResult.TESTNO + " and idno=" + objResult.IDNO + ")+1)", myconnection);
        //    //cmd.Connection = myconnection;
        //    cmd.ExecuteNonQuery();
        //    //SqlConnection con = new SqlConnection(obj._client_constr);
        //    //string sql = "";
        //    //SqlCommand cm = new SqlCommand();
        //    //int cs = Convert.ToInt32(obj.objIQBC.AddTestResult(objResult));
        //    //if (cs != -99)
        //    //{
        //    //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
        //    //}
        //    //System.Windows.Forms.MessageBox.Show("hello");
        //}
        //catch(Exception ex)
        //{

        //}
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterStartupScript(this.GetType(), "PrintOperation", "printing()", true);
    }

    protected void lvTest_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {

        ImageButton img = e.Item.FindControl("btnAnswer") as ImageButton;

        img.ImageUrl = "~/images/greenArrow1.jpg";

    }
   
}
