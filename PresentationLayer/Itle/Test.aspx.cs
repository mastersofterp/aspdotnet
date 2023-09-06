//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : ITLE                                              
// PAGE NAME     : Test.aspx                                  
// CREATION DATE : 05-01-2013                                                   
// CREATED BY    : ZUBAIR AHMAD(Taken reference from CCMS ITLE)                 
// MODIFIED BY   : SAKET SINGH
// MODIFIED DATE : 12-09-2017
//=================================================================================
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

public partial class Itle_test : System.Web.UI.Page
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
        //Very Important
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        Session["timeout"] = DateTime.Now.AddMinutes(120).ToString();

        //string dt = Request.Cookies["start"].Value;
        //mydate = Convert.ToDateTime(dt);

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
                //// lblUrname.Text = Convert.ToString(Session["USERFULLNAME"].ToString());
                lblid.Text = Session["userno"].ToString();
                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                PageId = Request.QueryString["pageno"];

                this.timerStartValue = long.Parse(Session["SEC"].ToString());
                this.TimerInterval = 500;
                HttpCookie Time = Request.Cookies["start"];
                FetchStudInfo();
                UpdateAttempts();
                BindShowQues();
                // COUNT();
                lblQueNo.Text = "1" + "/" + Convert.ToInt32(Session["NOQ"].ToString());
                // Used to insert id,date and courseno in Log_History table
                int log_history = objAC.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));
            }
        }
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

            // ---------By Zubair Date:28-10-2014----------
            string QuesRandom = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRANDOM", "TESTNO=" + Session["Test_No"]);
            if (QuesRandom.Contains("N"))
            {
                objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "'");//Session["userno"]);
                string questioSet = objCommon.LookUp("ACD_IQUESTION_SET_FOR_TEST", "QUESTION_SET", "TESTNO=" + Session["Test_No"]);
                string MyQuery = "select  *, 1 AS CORRECT_MARKS," + Session["idno"] + ",NULL from ACD_IQUESTIONBANK where " + "COURSENO=" + objTest.COURSENO + "and QUESTIONNO in(" + questioSet + ")" + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "'" + " order by newid()";
                CustomStatus cs = (CustomStatus)objTestc.AddITestMaster_Temp(MyQuery);
            }
            else
            {
                //--------Tarun Date:16-1-2014---------

                objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "'");//Session["userno"]);

                DataSet dsT = objCommon.FillDropDown("ACD_ITESTMASTER", "TOPICS,QUESTIONRATIO", "QUESTIONRATIO", "testno=" + objTest.TEST_NO, "");//objSqlHelper.ExecuteDataSet(Query);
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


                                //string MyQuery = "select TOP " + QRatio + " *, 1 AS CORRECT_MARKS," + Session["idno"] + " from ACD_IQUESTIONBANK where " + "COURSENO=" + objTest.COURSENO + "and Topic in(" + QTopic + ")" + " order by newid()";

                                string MyQuery = "select TOP " + QRatio + " *, 1 AS CORRECT_MARKS," + Session["idno"] + ",NULL from ACD_IQUESTIONBANK where " + "COURSENO=" + objTest.COURSENO + "and Topic in(" + QTopic + ")" + "AND QUESTION_TYPE='" + Session["TestType"].ToString() + "'" + " order by newid()";
                                CustomStatus cs = (CustomStatus)objTestc.AddITestMaster_Temp(MyQuery);


                            }
                        }
                    }
                }
                //--------End---------
            }
            //string sql = "select * from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "' order by Questionno";
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
                    // dtRe.Columns.Add(new DataColumn("CORRECTANS", typeof(int)));
                    //dtRe.Columns.Add(new DataColumn("SELECTED", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("CORRECTANS", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("SELECTED", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("CORRECT_MARKS", typeof(decimal)));
                    dtRe.Columns.Add(new DataColumn("USERNO", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("SELECTEDOPTION", typeof(object)));
                    dtRe.Columns.Add(new DataColumn("TYPE", typeof(char)));

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {                      
                        dr = dtRe.NewRow();
                        dr["QU_SRNO"] = dtRe.Rows.Count + 1;
                        dr["QUENO"] = ds.Tables[0].Rows[j]["QUESTIONNO"].ToString();
                        dr["QUESTIONTEXT"] = ds.Tables[0].Rows[j]["QUESTIONTEXT"].ToString().Replace("font-size:", "").Replace("<p>", "").Replace("</p>", "");

                        dr["ANS1TEXT"] = ds.Tables[0].Rows[j]["ANS1TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "").Replace("</p>", "");
                        dr["ANS2TEXT"] = ds.Tables[0].Rows[j]["ANS2TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "").Replace("</p>", "");
                        dr["ANS3TEXT"] = ds.Tables[0].Rows[j]["ANS3TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "").Replace("</p>", "");
                        dr["ANS4TEXT"] = ds.Tables[0].Rows[j]["ANS4TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "").Replace("</p>", "");
                        dr["ANS5TEXT"] = ds.Tables[0].Rows[j]["ANS5TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "").Replace("</p>", "");
                        dr["ANS6TEXT"] = ds.Tables[0].Rows[j]["ANS6TEXT"].ToString().Replace("font-size:", "").Replace("<p>", "").Replace("</p>", "");

                        if (ds.Tables[0].Rows[j]["QUESTIONIMAGE"].ToString() == "")
                        {

                            dr["QUESTIONIMAGE"] = null;
                        }
                        else
                        {
                            dr["QUESTIONIMAGE"] = "Itle_QuestionImage.aspx?FileName=" + ds.Tables[0].Rows[j]["QUESTIONIMAGE"].ToString();
                            dr["ANS1IMG"] = ds.Tables[0].Rows[j]["ANS1IMG"].ToString();
                            dr["ANS2IMG"] = ds.Tables[0].Rows[j]["ANS2IMG"].ToString();
                            dr["ANS3IMG"] = ds.Tables[0].Rows[j]["ANS3IMG"].ToString();
                            dr["ANS4IMG"] = ds.Tables[0].Rows[j]["ANS4IMG"].ToString();
                            dr["ANS5IMG"] = ds.Tables[0].Rows[j]["ANS5IMG"].ToString();
                            dr["ANS6IMG"] = ds.Tables[0].Rows[j]["ANS6IMG"].ToString();
                        }

                        dr["CORRECTANS"] = ds.Tables[0].Rows[j]["CORRECTANS"].ToString();
                        dr["CORRECT_MARKS"] = ds.Tables[0].Rows[j]["CORRECT_MARKS"].ToString();

                        dr["SELECTED"] = "-1";
                        dr["USERNO"] = Convert.ToInt32(Session["idno"].ToString());

                        dr["TYPE"] = ds.Tables[0].Rows[j]["TYPE"].ToString();

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
                            img.Visible = true;

                        //if (ds1.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim() == "Y")
                        //{

                        for (int j = 0; j < 6; j++)
                        {
                            int g = Convert.ToInt32(_myArrOp.GetValue(j));

                            //r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS1TEXT"].ToString(), "1"));
                            if (dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString() != "")
                                if (g == Convert.ToInt32(dtTbl.Rows[i]["CORRECTANS"].ToString()))
                                    r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), dtTbl.Rows[i]["CORRECTANS"].ToString().Trim()));
                                else
                                    r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString(), dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString().Trim()));
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
                        string imageBankFolder = "Itle_QuestionImage.aspx?FileName=";
                        //if (ds1.Tables[0].Rows[0]["SHOWRANDOM"].ToString().Trim() == "Y")
                        //{

                        for (int j = 0; j < 6; j++)
                        {
                            int g = Convert.ToInt32(_myArrOp.GetValue(j));

                            //r.Items.Add(new ListItem(dtTbl.Rows[i]["ANS1TEXT"].ToString(), "1"));
                            if (dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() != "")
                                if (g == Convert.ToInt32(dtTbl.Rows[i]["CORRECTANS"]))
                                    r.Items.Add(new ListItem("<img src=" + imageBankFolder + dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString().Trim() + " width=70px>" + dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString().Trim(), dtTbl.Rows[i]["CORRECTANS"].ToString().Trim()));
                                else
                                    r.Items.Add(new ListItem("<img src=" + imageBankFolder + dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString().Trim() + " width=70px>" + dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString().Trim(), "<img src=" + imageBankFolder + dtTbl.Rows[i]["ANS" + (g) + "IMG"].ToString() + " width=70px>" + dtTbl.Rows[i]["ANS" + (g) + "TEXT"].ToString().Trim()));
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
        DataSet dt = objAC.GetTestInfo(Convert.ToInt32(Session["userno"]));
        if (dt != null)
            lblUrname.Text = dt.Tables[0].Rows[0]["UA_FULLNAME"].ToString();

        lblSession.Text = Session["SESSION_NAME"].ToString();
        lblSession.ToolTip = Session["SESSION_NAME"].ToString();
        lblCourse.Text = Session["ICourseName"].ToString();
        lblTestName.Text = Session["TestName"].ToString();
        Session["COURSENO"] = Convert.ToInt32(Session["ICourseNo"]);

        string STUDID = dt.Tables[0].Rows[0]["UA_IDNO"].ToString();
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

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        dt = (DataTable)Session["Answered"];

        foreach (ListViewDataItem li in lvTest.Items)
        {

            RadioButtonList rl = ((RadioButtonList)li.FindControl("RadioButtonList1"));

            Label lv = ((Label)li.FindControl("Label1"));
            int no = Convert.ToInt32(lv.Text);

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


                }

            }

        }
        Session["Answered"] = dt;


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int i3 = 90;
            int j3 = 190;
            int k3 = 190 - 90;

            Session["TOTUNANSQUESTION"] = no3;


            dt = (DataTable)Session["Answered"];
            foreach (DataRow drow in dt.Rows)
            {
                //if (Convert.ToInt32(drow["SELECTED"]) != -1)
                //{
                //    no2 = no2 + 1;
                //    Session["TOTANSQUE"] = no2;
                //}
                if (!(drow["SELECTED"]).Equals("-1"))
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

            decimal TOTALMARKS = 0.0m;
            int NOOFCORRECTANS = 0;
            int TotQue = dt.Rows.Count;
            int StudAttempt;
            int Retest = 0;
            Session["TOTQUES"] = TotQue;

            objResult.TESTNO = Convert.ToInt32(Session["Test_No"]);
            objResult.IDNO = Convert.ToInt32(Session["idno"]);
            int count = Convert.ToInt32(objCommon.LookUp("ACD_ITLE_RESULTCOPY", "isnull(COUNT(*),0)", "IDNO=" + objResult.IDNO + " and testno=" + objResult.TESTNO));
            if (count > 0)
                objCommon.DeleteClientTableRow("ACD_ITLE_RESULTCOPY", "IDNO=" + objResult.IDNO + " and testno=" + objResult.TESTNO);

            foreach (DataRow X in dt.Rows)
            {
                objResult.TOTALMARKS += Convert.ToInt32(X["CORRECT_MARKS"]);
                //if (Convert.ToInt32(X["SELECTED"].ToString()) == Convert.ToInt32(X["CORRECTANS"].ToString()))
                //{
                if ((X["SELECTED"].ToString() == X["CORRECTANS"].ToString()))
                {


                    TOTALMARKS += Convert.ToDecimal(X["CORRECT_MARKS"].ToString());
                    Session["TOTSCORE"] = Convert.ToInt32(TOTALMARKS);
                    NOOFCORRECTANS += 1;
                    Session["NOCORRANS"] = NOOFCORRECTANS;
                    //Response.Redirect("Result.aspx");

                    //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);

                }

                objQuest.QUESTIONNO = Convert.ToInt32(X["queno"]);
                objQuest.QUESTIONTEXT = X["questiontext"].ToString();
                // objQuest.SELECTED = Convert.ToInt32(X["SELECTED"]);
                objQuest.SELECTED = (X["SELECTED"]).ToString();
                objQuest.FTBANS = X["selectedoption"].ToString();
                //objQuest.CORRECTANS = Convert.ToInt32(X["correctans"]);
                objQuest.CORRECTANS = (X["correctans"]).ToString();
                objQuest.COLLEGE_CODE = Session["colcode"].ToString().Trim();
                objQuest.IDNO = Convert.ToInt32(Session["idno"]);
                objQuest.TEST_NO = Convert.ToInt32(Session["Test_No"]);


                int csi = Convert.ToInt32(objIQBC.AddTestResultDetails(objQuest));
            }
            objResult.TESTNO = Convert.ToInt32(Session["Test_No"]);
            objResult.IDNO = Convert.ToInt32(Session["idno"]);
            //objResult.CORRECTMARKS = Convert.ToInt32(X["CORRECT_MARKS"]);
            objResult.CORRECTMARKS = Convert.ToInt32(TOTALMARKS);
            objResult.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);
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
            if (cs != -99)
            {
                //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
            }

            string Flag = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRESULT", "TESTNO=" + Session["Test_No"]);
            if (Flag.Trim() == "Y")
            {

                Session["SHOWRESULT"] = "Y";
                Response.Redirect("~/ITLE/Result.aspx");
            }
            else if (Flag.Trim() == "N")
            {
                //objCommon.DisplayUserMessage(UpdatePanel_Login, "Record Saved Sucessfully", this.Page);
                //Button3.Attributes.Add("OnClick", "window.close();");
                Session["SHOWRESULT"] = "N";
                Response.Redirect("~/ITLE/Thanks.aspx");
                //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
                //this.Page.Response.Close();
            }
            //Response.Redirect("Result.aspx");
        }
        catch (Exception ex)
        { }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.MessageBox.Show(this.lblTimerCount.Text);
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

    //private void oldcode()
    //{

    //    if (ds1.Tables[0].Rows.Count > 0)
    //    {
    //        DataTable dtTbl = (DataTable)Session["Answered"];


    //        foreach (ListViewDataItem li in lvTest.Items)
    //        {

    //            int i = 0;

    //            if (ds1.Tables[0].Rows[i]["TYPE"].Equals("T"))
    //            {
    //                Label lblQ = (Label)li.FindControl("Label1");
    //                lblQ.Text = ds1.Tables[0].Rows[i]["QU_SRNO"].ToString() + ".";

    //                Label lblQuestion = (Label)li.FindControl("Label2");
    //                lblQuestion.Text = ds1.Tables[0].Rows[i]["QUESTIONTEXT"].ToString();

    //                RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;
    //                r.Items.Add(ds1.Tables[0].Rows[i]["ANS1TEXT"].ToString());
    //                r.Items.Add(ds1.Tables[0].Rows[i]["ANS2TEXT"].ToString());
    //                r.Items.Add(ds1.Tables[0].Rows[i]["ANS3TEXT"].ToString());
    //                r.Items.Add(ds1.Tables[0].Rows[i]["ANS4TEXT"].ToString());
    //                ((RadioButtonList)lvTest.FindControl("RadioButtonList1")).Items.Add(ds1.Tables[0].Rows[0]["ANS1TEXT"].ToString());
    //            }
    //            else
    //            {
    //                string imageBankFolder = "IMAGE_QUESTION/";

    //                Label lblQ = (Label)li.FindControl("Label1");
    //                lblQ.Text = ds1.Tables[0].Rows[i]["QU_SRNO"].ToString() + ".";

    //                Label lblQuestion = (Label)li.FindControl("Label2");
    //                lblQuestion.Text = ds1.Tables[0].Rows[i]["QUESTIONTEXT"].ToString();
    //                RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;

    //                r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS1IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS1TEXT"].ToString());
    //                r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS2IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS2TEXT"].ToString());
    //                r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS3IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS3TEXT"].ToString());
    //                r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS4IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS4TEXT"].ToString());

    //            }


    //        }
    //    }
    //}

   

    //protected void timer1_Tick1(object sender, EventArgs e)
    //{
    //    if (0 > DateTime.Compare(DateTime.Now, DateTime.Parse(Session["timeout"].ToString())))
    //    {
    //        lblTimer.Text = "Number of Minutes Left: " +  ((Int32)DateTime.Parse(Session["timeout"].ToString()).Subtract(DateTime.Now).TotalMinutes).ToString();
    //    }
    //}

    #endregion
}
