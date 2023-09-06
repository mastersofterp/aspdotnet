//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : ITLE                                              
// PAGE NAME     : Test.aspx                                  
// CREATION DATE : 05-01-2020                                                  
// CREATED BY    :PRASHANT WANKAR(Taken reference from CCMS ITLE)                 
// MODIFIED BY   : 
// MODIFIED DESC : 
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
//using System.Transactions;
using System.IO;
//using System.Windows;
using System.Linq;
using System.Data.OleDb;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.Web.Services;
using IITMS.NITPRM;
using System.Windows.Forms;

public partial class Itle_ITLETest : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITestMasterController objTestc = new ITestMasterController();
    ITLETestController objIETestc = new ITLETestController();
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
    string _nitmn_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    static string CheckOut = string.Empty;
    int count = 0;
    string PageId;




    protected void Page_Load(object sender, EventArgs e)
    {
        Head1.DataBind();
        //Very Important
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        ////btnSave.Attributes.Add("onclick", "if(typeof (Page_ClientValidate) === 'function' && !Page_ClientValidate()){return false;} this.disabled = true;this.value = 'Working...';" + ClientScript.GetPostBackEventReference(btnSave, null) + ";");

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

                lblid.Text = Session["userno"].ToString();

                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                CheckTestValid();
                this.timerStartValue = long.Parse(Session["SEC_OBJ"].ToString());
                this.TimerInterval = 1000;

                HttpCookie Time = Request.Cookies["start"];
                hdnMalfunction.Value = Session["Malfunction"].ToString();
                hdnMalfunctionStudent.Value = Session["MalFunctionStudent"].ToString();
                FetchStudInfo();
                PreExamProcess();
                BindShowQues();
                DisplayPreview();
                UpdateAttempts();

            }
        }



    }



    void Page_PreRender(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        StringBuilder bldr = new StringBuilder();
        bldr.AppendFormat("var Timer = new myTimer({0},{1},'{2}','timerData');", this.timerStartValue, this.TimerInterval, this.lblTimerCount.ClientID);
        bldr.Append("Timer.go();");
        ClientScript.RegisterStartupScript(this.GetType(), "TimerScript", bldr.ToString(), true);
        ClientScript.RegisterHiddenField("timerData", timerStartValue.ToString());

        //}
        if (Page.IsPostBack)
        {
            DateTime lastaction = Convert.ToDateTime(objCommon.LookUp("ACD_ITEST_DETAILS", "ANSSUBMITTIME", "TDNO=" + Convert.ToInt32(Session["TDNO_OBJ"])));
            TimeSpan timeDiff = DateTime.Now - lastaction;
            double millisecDiff = timeDiff.TotalMilliseconds;
            DateTime currentTestEndTime = Convert.ToDateTime(Session["CURRTESTENDTIME"]);
            currentTestEndTime = currentTestEndTime.AddMilliseconds(millisecDiff);
            Session["CURRTESTENDTIME"] = currentTestEndTime;
        }

    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        string referer = Request.ServerVariables["HTTP_REFERER"];
        if (string.IsNullOrEmpty(referer))
        {
            Response.Redirect("~/default.aspx?");
        }

        if (Session["userno"] == null)
        {
            Response.Redirect("~/default.aspx?");
            return;
        }
        //if (!Page.IsPostBack)
        //{

        string timerVal = Request.Form["timerData"];
        if (timerVal != null || timerVal == "")
        {
            timerVal = timerVal.Replace(",", String.Empty);
            timerStartValue = long.Parse(timerVal);
            UpdateTestDurLeft(timerVal);
        }
        //}
    }

    private void UpdateTestDurLeft(string timerVal)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(Convert.ToDouble(timerVal));
        DateTime dateTime = DateTime.Today.Add(time);
        int ret = objIETestc.UpdIETestDurationLeft(Convert.ToInt32(Session["TDNO_OBJ"]), dateTime);
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

    #region Sachin 21Mar2017

    private void CheckTestValid()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_ITESTMASTER", "*", "TESTDURATION", "TESTNO=" + Convert.ToInt32(Session["Test_No_OBJ"]), "TESTNO");
            //Session["Total_Marks_OBJ"] = ds.Tables[0].Rows[0]["TOTAL"].ToString();

            string StartTime = ds.Tables[0].Rows[0]["STARTDATE"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTDATE"].ToString()).ToString("HH:mm:ss");
            string EndTime = ds.Tables[0].Rows[0]["ENDDATE"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("HH:mm:ss");

            if (Convert.ToDateTime(StartTime).TimeOfDay <= DateTime.Now.TimeOfDay && Convert.ToDateTime(EndTime).TimeOfDay >= DateTime.Now.TimeOfDay)
            {
                string ExamTiming = EndTime;
                DateTime CTimess = DateTime.Now; //ToString("HH:mm:ss");

                string CTime = CTimess.ToString("HH:mm:ss");

                TimeSpan SubTime = TimeSpan.Parse(ExamTiming) - TimeSpan.Parse(CTime);

                string Time = string.Empty;
                if (SubTime > TimeSpan.Parse(Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss")))
                {
                    Time = Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss");
                }
                else
                {
                    Time = Convert.ToDateTime(SubTime.ToString()).ToString("HH:mm:ss");

                }
                Session["Total_Time_For_Test_OBJ"] = Time;
                int Attempts = Convert.ToInt32(ds.Tables[0].Rows[0]["ATTEMPTS"]);

                Session["TOTALMARKS"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL"]);
                if (Session["TDNO_OBJ"] != null)
                {
                    DateTime durationleft = Convert.ToDateTime(objCommon.LookUp("ACD_ITEST_DETAILS", "top 1 TESTDURATIONLEFT", "TDNO=" + Session["TDNO_OBJ"].ToString() + " AND IDNO=" + Convert.ToInt32(Session["idno"]) + " "));


                    if (!String.IsNullOrEmpty(durationleft.ToString()))
                    {
                        if (SubTime > TimeSpan.Parse(Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss")))
                        {
                            Time = durationleft.ToString("HH:mm:ss");
                        }
                        else
                        {
                            Time = Convert.ToDateTime(SubTime.ToString()).ToString("HH:mm:ss");

                        }
                        // Time = durationleft.ToString("HH:mm:ss");
                    }
                }
                DateTime Time1 = Convert.ToDateTime(Time);


                HttpCookie h = new HttpCookie("start");
                Response.Cookies.Clear();
                h.Value = Time;
                Response.Cookies.Add(h);


                int sec = (Time1.Hour * 60 * 60) + (Time1.Minute * 60) + Time1.Second;
                Session["SEC_OBJ"] = sec * 1000;

                DateTime testendtime = DateTime.Now.AddMilliseconds(sec * 1000);
                Session["CURRTESTENDTIME"] = testendtime;



            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid Time')", true);
                // objCommon.DisplayUserMessage(UpdatePanel1, "Invalid Time", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            //objCommon.DisplayUserMessage(UpdatePanel1, "ITLE_StudTest.aspx.btnlnkSelect_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }

    }
    #endregion

    private void PreExamProcess()
    {
        try
        {
            if (Session["TDNO_OBJ"] == null)
            {
                int testno = Convert.ToInt32(Session["Test_No_OBJ"]);
                int idno = Convert.ToInt32(Session["idno"]);
                DateTime durationleft = Convert.ToDateTime(Session["Total_Time_For_Test_OBJ"]);
                DateTime anssubmitdate = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
                string WebBrowserName = HttpContext.Current.Request.Browser.Browser;
                string IPADDR;
                IPADDR = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(IPADDR))
                {
                    IPADDR = Request.ServerVariables["REMOTE_ADDR"];
                }
                CustomStatus cs = (CustomStatus)objIETestc.AddIETestDetails(testno, idno, durationleft, anssubmitdate, WebBrowserName, IPADDR);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    Session["TDNO_OBJ"] = Convert.ToInt32(objCommon.LookUp("ACD_ITEST_DETAILS", "TDNO", "TESTNO=" + testno + " AND IDNO=" + idno));
                    AddQuesInResultCopy();
                }

                //AddQuesInResultCopy();
            }
            else
            {
                int ret = objIETestc.UpdIETestStratTime(Convert.ToInt32(Session["TDNO_OBJ"]));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.PreExamProcess -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void AddQuesInResultCopy()
    {
        try
        {

            SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);

            DataSet dsT = objCommon.FillDropDown("ACD_ITESTMASTER", "COURSENO,QuestionRatio", "QuestionRatio", "testno=" + objTest.TEST_NO, "");//objSqlHelper.ExecuteDataSet(Query);
            if (!string.IsNullOrEmpty(dsT.Tables[0].Rows[0]["COURSENO"].ToString()))
            {
                string QRem = dsT.Tables[0].Rows[0]["QUESTIONRATIO"].ToString();
                string Rem = dsT.Tables[0].Rows[0]["COURSENO"].ToString();

                string MyQuery = "Select QUESTIONNO,	COURSENO	,TYPE,	QUESTIONTEXT,	QUESTIONIMAGE,	ANS1TEXT,	ANS1IMG,	ANS2TEXT	,ANS2IMG,	ANS3TEXT,	ANS3IMG	,ANS4TEXT,	ANS4IMG,	ANS5TEXT	,ANS5IMG,	ANS6TEXT,	ANS6IMG,	CORRECTANS	,COLLEGE_CODE,	CREATEDDATE	,StudentID,	" + Convert.ToInt32(Session["Test_No_OBJ"]) + ",'' selecteds,0 QMRK,'' desans,0 as mrkbt,'R' ANSTYPE,'U' aNS," + Session["OrgId"] + " AS OrganizationId   From ACD_IQUESTIONBANK_TEMP_CREATION where  studentid='" + Session["idno"] + "' and	COURSENO=" + Session["COURSENO_OBJ"].ToString() + "";
                CustomStatus cs = (CustomStatus)objIETestc.AddIEResultCopy(MyQuery);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.AddQuesInResultCopy -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindShowQues()
    {
        try
        {
            SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);




            if (Session["qNoList_OBJ"] == null)
            {
                //string qNosql = "SELECT QUESTIONNO FROM ACD_ITLE_RESULTCOPY WHERE TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " and	COURSENO=" + Session["COURSENO_OBJ"].ToString() + "";
                DataSet qNods = objIETestc.GetQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));
                //DataSet qNods = objSqlHelper.ExecuteDataSet(qNosql);
                List<int> qNoList = new List<int>();
                foreach (DataRow dtr in qNods.Tables[0].Rows)
                {
                    qNoList.Add(Convert.ToInt32(dtr["QUESTIONNO"]));
                }
                Session["qNoList_OBJ"] = qNoList;
            }



            int[] _myArrOp = (int[])Session["RandAnsSrList_OBJ"];
            List<int> QuesNoList = (List<int>)Session["qNoList_OBJ"];
            for (int i = 0; i < QuesNoList.Count; i++)
            {
                if (Session["CurQuesNo_OBJ"] == null)
                {
                    Session["CurQuesIndex"] = i + 1;
                    Session["CurQuesNo_OBJ"] = QuesNoList[i];
                    if (QuesNoList.Count != 1)
                    {
                        Session["NextQuesNo"] = QuesNoList[i + 1];
                        btnPrev.Visible = false;
                        btnNext.Visible = true;
                        break;
                    }
                }
                else
                {
                    if (Convert.ToInt32(Session["CurQuesNo_OBJ"]) == QuesNoList[i])
                    {
                        Session["CurQuesIndex"] = i + 1;
                        Session["CurQuesNo_OBJ"] = QuesNoList[i];
                        if (Convert.ToInt32(Session["CurQuesIndex"]) == 1)
                        {
                            btnPrev.Visible = false;
                        }
                        else
                        {
                            Session["PrevQuesNo"] = QuesNoList[i - 1];
                            btnPrev.Visible = true;
                        }
                        if (Convert.ToInt32(Session["CurQuesIndex"]) == QuesNoList.Count)
                        {
                            btnNext.Visible = true;
                            btnSave.Text = "Save";
                        }
                        else
                        {
                            Session["NextQuesNo"] = QuesNoList[i + 1];
                            btnNext.Visible = true;
                            btnSave.Text = "Save & Next";

                        }
                        break;
                    }
                }
            }



            //string sql;


            //sql = "SELECT * FROM ACD_ITLE_RESULTCOPY WHERE TESTNO=" + Session["Test_No_OBJ"] + " AND IDNO=" + Session["idno"] + " AND QUESTIONNO=" + Session["CurQuesNo_OBJ"].ToString();


            //DataSet ds = objSqlHelper.ExecuteDataSet(sql);
            DataSet ds = objIETestc.GetNextQuestionNo(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString()));

            int TotQue = QuesNoList.Count;
            Session["TOTQUES"] = TotQue;
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["COURSENO"].ToString()))
            {
                Label1.Text = Session["CurQuesIndex"].ToString();
                Label2.Text = ds.Tables[0].Rows[0]["QUESTIONTEXT"].ToString().Replace("<img", @"<img class='img-responsive'");
                Label3.Text = Session["CurQuesIndex"].ToString();
                Label4.Text = Session["TOTQUES"].ToString();
                string imageBankFolder = "ITLE_QuestionImage.aspx?FileName=";
                if (ds.Tables[0].Rows[0]["TYPE"].ToString().Equals("T"))
                {

                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString()) || ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString() == "")
                    {
                        imgQuesImage.Visible = false;
                    }
                    else
                    {
                        imgQuesImage.ImageUrl = imageBankFolder + ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString();
                        imgQuesImage.Visible = true;
                    }
                    rdBtnQuesList.Items.Clear();
                    ckBtnQuesList.Items.Clear();
                    for (int j = 0; j < 6; j++)
                    {
                        int g = Convert.ToInt32(_myArrOp.GetValue(j));

                        if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {
                                rdBtnQuesList.Items.Add(new ListItem(ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Replace("<img", @"<img class='img-responsive'"), g.ToString().Trim()));

                            }

                        }
                        else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {

                                ckBtnQuesList.Items.Add(new ListItem(ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Replace("<img", @"<img class='img-responsive'"), g.ToString().Trim()));

                            }

                        }
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString()) || ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString() == "")
                    {
                        imgQuesImage.Visible = false;
                    }
                    else
                    {
                        imgQuesImage.ImageUrl = imageBankFolder + ds.Tables[0].Rows[0]["QUESTIONIMAGE"].ToString();
                        imgQuesImage.Visible = true;
                    }
                    rdBtnQuesList.Items.Clear();
                    ckBtnQuesList.Items.Clear();



                    for (int j = 0; j < 6; j++)
                    {
                        int g = Convert.ToInt32(_myArrOp.GetValue(j));
                        if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString() != "")
                                rdBtnQuesList.Items.Add(new ListItem("<img class='img-responsive' src=" + imageBankFolder + ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString().Trim() + " >" + ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Trim(), g.ToString().Trim()));



                        }
                        else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                        {
                            if (ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString() != "")
                            {

                                ckBtnQuesList.Items.Add(new ListItem("<img class='img-responsive' src=" + imageBankFolder + ds.Tables[0].Rows[0]["ANS" + (g) + "IMG"].ToString().Trim() + " >" + ds.Tables[0].Rows[0]["ANS" + (g) + "TEXT"].ToString().Trim(), g.ToString().Trim()));

                            }

                        }

                    }
                }

                if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("R"))
                {
                    Session["ANSWER_TYPE"] = "R";
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SELECTED"].ToString()))
                    {
                        rdBtnQuesList.SelectedValue = ds.Tables[0].Rows[0]["SELECTED"].ToString().Trim();
                    }
                    else
                    {
                        foreach (ListItem li in rdBtnQuesList.Items)
                        {
                            li.Selected = false;
                        }

                        rdBtnQuesList.SelectedValue = null;
                    }
                    ckBtnQuesList.Visible = false;
                    rdBtnQuesList.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["ANSWER_TYPE"].ToString().Trim().Equals("C"))
                {
                    Session["ANSWER_TYPE"] = "C";
                    foreach (ListItem li in ckBtnQuesList.Items)
                    {
                        if (ds.Tables[0].Rows[0]["SELECTED"].ToString().Contains(li.Value))
                        {
                            li.Selected = true;
                        }

                    }
                    rdBtnQuesList.Visible = false;
                    ckBtnQuesList.Visible = true;
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
        lblTestName.Text = Session["TestName_OBJ"].ToString();
        string STUDID = dt.Tables[0].Rows[0]["UA_IDNO"].ToString();
        lblSeatno.Text = dt.Tables[0].Rows[0]["REGNO"].ToString();
        objTest.TEST_NO = Convert.ToInt32(Session["Test_No_OBJ"]);
        objTest.UA_NO = Convert.ToInt32(Session["userno"]);
        objTest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
       // imgStudPhoto.ImageUrl = "~/showimage.aspx?id=" + STUDID + "&type=student";
        lblSession.Text = Session["SESSION_NAME"].ToString();
        lblCourse.Text = Session["ICourseName"].ToString();
        objResult.TESTNO = objTest.TEST_NO;
        objResult.IDNO = Convert.ToInt32(STUDID);
        objResult.COURSENO = objTest.COURSENO;
    }

    private void UpdateAttempts()
    {
        objAC.Update_User_Attempts(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"]), objTest.TEST_NO);
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

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        try
        {

            int prevQuestion = Convert.ToInt32(Session["PrevQuesNo"]);
            Session["CurQuesNo_OBJ"] = prevQuestion;
            BindShowQues();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnPrev_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {


            if (rdBtnQuesList.SelectedValue.ToString() != "")
            {
                lblAlert.Text = "You Cannot Skip the question,if any of the option is  selected !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                // objCommon.DisplayUserMessage(UpdatePanel_Login, "You Cannot Skip the question,if any of the option is  selected", this);
                return;
            }
            // btnSave_Click(sender, e);
            string ansStat = objIETestc.GetAnswerStatus(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString()), Convert.ToInt32(Session["idno"].ToString()));
            //objCommon.LookUp("ACD_ITLE_RESULTCOPY", "ANS_STAT", "QUESTIONNO=" + Session["CurQuesNo_OBJ"].ToString() + " AND TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " and	COURSENO=" + Session["COURSENO_OBJ"].ToString() + " ");
            if (ansStat.Trim() != "")
            {

                SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);
                // string query = "UPDATE ACD_ITLE_RESULTCOPY SET ANS_STAT = 'N',SELECTEDANS='',SELECTED='' WHERE QUESTIONNO=" + Session["CurQuesNo_OBJ"].ToString() + " AND TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " and	COURSENO=" + Session["COURSENO_OBJ"].ToString() + "";
                // objSqlHelper.ExecuteNonQuery(query);
                CustomStatus cs = (CustomStatus)objIETestc.UpdateSkipStatus(Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString()), Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));
            }
            int nextQuestion = Convert.ToInt32(Session["NextQuesNo"]);
            Session["CurQuesNo_OBJ"] = nextQuestion;
            BindShowQues();
            DisplayPreview();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnNext_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (Session["ANSWER_TYPE"].ToString() == "C")
        {
            foreach (ListItem li in ckBtnQuesList.Items)
            {
                li.Selected = false;
            }
        }
        if (Session["ANSWER_TYPE"].ToString() == "R")
        {
            foreach (ListItem li in rdBtnQuesList.Items)
            {
                li.Selected = false;
            }
        }

        // btnSave_Click(sender, e);
    }

    private void DisplayPreview()
    {
        DataSet ds = objIETestc.DisplayPreview(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));

        //objCommon.FillDropDown("ACD_ITLE_RESULTCOPY", "ROW_NUMBER() OVER(ORDER BY TESTNO) AS SRNO", "QUESTIONNO,SELECTED,CASE WHEN ANS_STAT = 'S' THEN 'btn btn-success' WHEN ANS_STAT = 'R' THEN 'btn btn-outline-primary' WHEN ANS_STAT = 'N' THEN 'btn btn-warning' ELSE 'btn btn-outline-danger' END AS ANS_STAT", "TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " and	COURSENO=" + Session["COURSENO_OBJ"].ToString(), "");
        lvPreview.DataSource = ds;
        lvPreview.DataBind();

        DataSet ds1 = objIETestc.BindPreviewQuestionCount(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["COURSENO_OBJ"].ToString()));
        lblAnsCount.Text = ds1.Tables[0].Rows[0]["ANSWER"].ToString();
        lblRevCount.Text = ds1.Tables[1].Rows[0]["REVIEW"].ToString();
        lblSkipCount.Text = ds1.Tables[2].Rows[0]["SKIP"].ToString();
        lblNotAnsCount.Text = ds1.Tables[3].Rows[0]["NOT_ANSWER"].ToString();
    }

    protected void btnQuesStatus_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btnQuesStatus = sender as LinkButton;
            int selectQuesNo = Convert.ToInt32(int.Parse(btnQuesStatus.CommandArgument));
            int selectedSrNo = Convert.ToInt32(btnQuesStatus.CommandName);
            int curQuesIndex = Convert.ToInt32(Session["CurQuesIndex"].ToString());
            string selPrevQAnsStat = string.Empty;
            string ansStat = objIETestc.GetAnswerStatus(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), selectQuesNo, Convert.ToInt32(Session["idno"].ToString()));

            //objCommon.LookUp("ACD_ITLE_RESULTCOPY", "ANS_STAT", "TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND QUESTIONNO=" + selectQuesNo + "AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
            List<int> QuesNoList = (List<int>)Session["qNoList_OBJ"];
            for (int i = 0; i < QuesNoList.Count; i++)
            {
                if (selectQuesNo == QuesNoList[i])
                {
                    if (i != 0)
                    {
                        selPrevQAnsStat = objIETestc.GetAnswerStatus(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(QuesNoList[i - 1].ToString()), Convert.ToInt32(Session["idno"].ToString()));
                        //objCommon.LookUp("ACD_ITLE_RESULTCOPY", "ANS_STAT", "QUESTIONNO=" + QuesNoList[i - 1].ToString() + " AND TESTNO=" + Session["Test_No_OBJ"].ToString() + "AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
                    }
                    break;
                }
            }
            if (curQuesIndex + 1 < selectedSrNo && ansStat == "U" && selPrevQAnsStat == "U")
            {
                lblAlert.Text = "Cannot Jump To The Selected Question !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                // objCommon.DisplayMessage(UpdatePanel_Login, "Cannot Jump To The Selected Question !", this.Page);
                return;
            }
            string CURansStat = objIETestc.GetAnswerStatus(Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString()), Convert.ToInt32(Session["idno"].ToString()));
            //objCommon.LookUp("ACD_ITLE_RESULTCOPY", "ANS_STAT", "QUESTIONNO=" + Session["CurQuesNo_OBJ"].ToString() + " AND TESTNO=" + Session["Test_No_OBJ"].ToString() + "AND IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
            if (curQuesIndex != selectedSrNo)
            {

                if (CURansStat.Trim() == "U")
                {
                    int questionno = 0;
                    int testno = 0;
                    int idno = 0;
                    questionno = Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString());
                    testno = Convert.ToInt32(Session["Test_No_OBJ"].ToString());
                    idno = Convert.ToInt32(Session["idno"].ToString());
                    //SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);
                    //string query = "UPDATE ACD_ITLE_RESULTCOPY SET ANS_STAT = 'C' WHERE QUESTIONNO=" + Session["CurQuesNo_OBJ"].ToString() + " AND TESTNO=" + Session["Test_No_OBJ"].ToString() + "AND IDNO=" + Convert.ToInt32(Session["idno"].ToString());
                    //objSqlHelper.ExecuteNonQuery(query);
                    CustomStatus cs = (CustomStatus)objIETestc.UpdateQuestionStatus(questionno, testno, idno);
                }
                //}
                Session["CurQuesNo_OBJ"] = selectQuesNo;
                BindShowQues();
                DisplayPreview();
                dvIntermediate.Visible = false;
                dvShowTestStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnQuesStatus_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCloseModal_Click(object sender, EventArgs e)
    {
        dvIntermediate.Visible = false;
        dvShowTestStatus.Visible = false;
    }

    protected void btnReview_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(Session["CURRTESTENDTIME"]) < DateTime.Now)
            {
                objCommon.DisplayMessage(this.UpdatePanel_Login, "Time Out", this.Page);
                btnSubmitnew_Click(sender, e);
                return;
            }
            //string selectedAns=string.Empty;
            string selected = string.Empty;
            string selectedAns = string.Empty;
            string ansStat = string.Empty;

            if (rdBtnQuesList.SelectedValue.ToString() == null || rdBtnQuesList.SelectedValue.ToString() == "")
            {
                lblAlert.Text = "You Cannot Review the question,if any of the option is not selected !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                //objCommon.DisplayUserMessage(UpdatePanel_Login, "You Cannot Review the question,if any of the option is not selected", this);
                return;
            }
            if (Session["ANSWER_TYPE"] == "R")
            {
                //selectedAns = rdBtnQuesList.SelectedItem.ToString();
                selected = rdBtnQuesList.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(selected))
                {
                    selectedAns = GetSelectedAns(rdBtnQuesList.SelectedValue.ToString());
                }
            }
            else if (Session["ANSWER_TYPE"] == "C")
            {
                List<int> correctAnsList = new List<int>();
                foreach (ListItem item in ckBtnQuesList.Items)
                {
                    if (item.Selected)
                    {
                        correctAnsList.Add(Convert.ToInt32(item.Value));
                    }
                }
                correctAnsList.Sort();
                int selectCount = 0;
                foreach (int ans in correctAnsList)
                {
                    if (selectCount == 0)
                    {
                        selected += ans.ToString();
                        selectedAns += GetSelectedAns(ans.ToString());
                        selectCount++;
                    }
                    else
                    {
                        selected += "," + ans.ToString();
                        selectedAns += "," + GetSelectedAns(ans.ToString());
                    }
                }

            }
            if (!String.IsNullOrEmpty(selected))
            {
                ansStat = "R";
            }
            else
            {
                ansStat = "C";
            }
            int tdno = Convert.ToInt32(Session["Test_No_OBJ"]);
            int questionno = Convert.ToInt32(Session["CurQuesNo_OBJ"]);
            int idno = Convert.ToInt32(Session["idno"].ToString());
            CustomStatus cs = (CustomStatus)objIETestc.SaveIETestSelectedAnswer(tdno, questionno, selected, selectedAns, ansStat, idno);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                if (Convert.ToInt32(Session["CurQuesIndex"]) < Convert.ToInt32(Session["TOTQUES"]))
                {
                    int nextQuestion = Convert.ToInt32(Session["NextQuesNo"]);
                    Session["CurQuesNo_OBJ"] = nextQuestion;
                }
                BindShowQues();
                DisplayPreview();
            }
            else
            {
                //objCommon.DisplayMessage(UpdatePanel_Login, "Something went wrong. Please try again !!", this.Page);
                lblAlert.Text = "Something went wrong. Please try again !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string GetSelectedAns(string count)
    {
        string selectedAns = string.Empty;
        //DataSet ds = objCommon.FillDropDown("ACD_ITLE_RESULTCOPY", "ANS1TEXT,ANS2TEXT,ANS3TEXT", "ANS4TEXT,ANS5TEXT,ANS6TEXT", "QUESTIONNO=" + Session["CurQuesNo_OBJ"].ToString() + " AND TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"], "QUESTIONNO");

        DataSet ds = objIETestc.GetSelectedCorrectAnswer(Convert.ToInt32(Session["CurQuesNo_OBJ"].ToString()), Convert.ToInt32(Session["Test_No_OBJ"].ToString()), Convert.ToInt32(Session["idno"]));
        selectedAns = ds.Tables[0].Rows[0]["ANS" + (count) + "TEXT"].ToString();
        return selectedAns;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            //if (Convert.ToDateTime(Session["CURRTESTENDTIME"]) < DateTime.Now)
            //{
            //    objCommon.DisplayMessage(this.UpdatePanel_Login, "Time Out", this.Page);
            //    btnSubmitnew_Click(sender, e);
            //    return;
            //}

            //string selectedAns=string.Empty;
            string selected = string.Empty;
            string selectedAns = string.Empty;
            string ansStat = string.Empty;

            if (rdBtnQuesList.SelectedValue.ToString() == null || rdBtnQuesList.SelectedValue.ToString() == "")
            {
                lblAlert.Text = "You cannot save the question,if any of the option is not selected !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                //objCommon.DisplayUserMessage(UpdatePanel_Login, "You cannot save the question,if any of the option is not selected", this);
                return;
            }

            if (Session["ANSWER_TYPE"] == "R")
            {
                //selectedAns = rdBtnQuesList.SelectedItem.ToString();
                selected = rdBtnQuesList.SelectedValue.ToString();
                if (!string.IsNullOrEmpty(selected))
                {
                    selectedAns = GetSelectedAns(rdBtnQuesList.SelectedValue.ToString());
                }
            }
            else if (Session["ANSWER_TYPE"] == "C")
            {
                List<int> correctAnsList = new List<int>();
                foreach (ListItem item in ckBtnQuesList.Items)
                {
                    if (item.Selected)
                    {
                        correctAnsList.Add(Convert.ToInt32(item.Value));
                    }
                }
                correctAnsList.Sort();
                int selectCount = 0;
                foreach (int ans in correctAnsList)
                {
                    if (selectCount == 0)
                    {
                        selected += ans.ToString();
                        selectedAns += GetSelectedAns(ans.ToString());
                        selectCount++;
                    }
                    else
                    {
                        selected += "," + ans.ToString();
                        selectedAns += "," + GetSelectedAns(ans.ToString());
                    }
                }

            }
            if (!String.IsNullOrEmpty(selected))
            {
                ansStat = "S";
            }
            else
            {
                ansStat = "C";
            }
            int tdno = Convert.ToInt32(Session["Test_No_OBJ"]);
            int questionno = Convert.ToInt32(Session["CurQuesNo_OBJ"]);
            int idno = Convert.ToInt32(Session["idno"].ToString());
            CustomStatus cs = (CustomStatus)objIETestc.SaveIETestSelectedAnswer(tdno, questionno, selected, selectedAns, ansStat, idno);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                if (Convert.ToInt32(Session["CurQuesIndex"]) < Convert.ToInt32(Session["TOTQUES"]))
                {
                    int nextQuestion = Convert.ToInt32(Session["NextQuesNo"]);
                    Session["CurQuesNo_OBJ"] = nextQuestion;
                }
                BindShowQues();
                DisplayPreview();
            }
            else
            {
                //objCommon.DisplayMessage(UpdatePanel_Login, "Something went wrong. Please try again !!", this.Page);
                lblAlert.Text = "Something went wrong. Please try again !";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitnew_Click(object sender, EventArgs e)
    {
        try
        {

            //string tdno = objCommon.LookUp("ACD_ITEST_RESULT", "testno", "testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"]);
            if (!String.IsNullOrEmpty(Session["Test_No_OBJ"].ToString()))
            {
                SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);
                string sql = "DELETE FROM ACD_ITEST_RESULT WHERE testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"];
                objSqlHelper.ExecuteNonQuery(sql);
            }

            int correctAns = Convert.ToInt32(objCommon.LookUp("ACD_ITLE_RESULTCOPY", "COUNT(QUESTIONNO) AS COR_ANS_COUNT", "CORRECTANS=SELECTED AND ANS_STAT IN ('S','R') AND testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"]));
            decimal totmarks = Convert.ToDecimal(Session["TOTQUES"]);
            decimal correctMarks = Convert.ToDecimal(correctAns);
            int studAttempt = Convert.ToInt32(Session["STUDATTEMPTS"]) + 1;

            int cs = Convert.ToInt32(objIETestc.AddIETestResult(Convert.ToInt32(Session["TDNO_OBJ"]), Convert.ToInt32(Session["TOTQUES"]), correctAns, totmarks, correctMarks, studAttempt, Convert.ToInt32(Session["COURSENO_OBJ"])));
            if (cs != -99)
            {
                //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
            }

            string Flag = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRESULT", "TESTNO=" + Session["Test_No_OBJ"].ToString());
            if (Flag.Trim() == "Y")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseFinishModal();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);
                Session["SHOWRESULT"] = "Y";
                Response.Redirect("ITLEResult.aspx", false);
            }
            else if (Flag.Trim() == "N")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseFinishModal();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);
                Session["SHOWRESULT"] = "N";
                Response.Redirect("Thanks.aspx", false);

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenFinishModal();", true);
            ////string tdno = objCommon.LookUp("ACD_ITEST_RESULT", "testno", "testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"]);
            //if (!String.IsNullOrEmpty(Session["Test_No_OBJ"].ToString()))
            //{
            //    SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);
            //    string sql = "DELETE FROM ACD_ITEST_RESULT WHERE testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"];
            //    objSqlHelper.ExecuteNonQuery(sql);
            //}

            //int correctAns = Convert.ToInt32(objCommon.LookUp("ACD_ITLE_RESULTCOPY", "COUNT(QUESTIONNO) AS COR_ANS_COUNT", "CORRECTANS=SELECTED AND ANS_STAT IN ('S','R') AND testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"]));
            //decimal totmarks = Convert.ToDecimal(Session["TOTQUES"]);
            //decimal correctMarks = Convert.ToDecimal(correctAns);
            //int studAttempt = Convert.ToInt32(Session["STUDATTEMPTS"]) + 1;

            //int cs = Convert.ToInt32(objIETestc.AddIETestResult(Convert.ToInt32(Session["TDNO_OBJ"]), Convert.ToInt32(Session["TOTQUES"]), correctAns, totmarks, correctMarks, studAttempt, Convert.ToInt32(Session["COURSENO_OBJ"])));
            //if (cs != -99)
            //{
            //    //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
            //}

            //string Flag = Session["SHOWRESULT"].ToString();//objCommon.LookUp("ACD_ITESTMASTER", "SHOWRESULT", "TESTNO=" + Session["Test_No_OBJ"].ToString());
            //if (Flag.Trim() == "Y")
            //{web.config

            //    Session["SHOWRESULT"] = "Y";
            //    Response.Redirect("ITLEResult.aspx", false);
            //}
            //else if (Flag.Trim() == "N")
            //{

            //    Session["SHOWRESULT"] = "N";
            //    Response.Redirect("Thanks.aspx", false);

            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // System.Windows.Forms.MessageBox.Show(this.lblTimerCount.Text);
    }

    [WebMethod]
    public static void GetCurrentTime(string i)
    {
        CheckOut = i;
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
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            //string tdno = objCommon.LookUp("ACD_ITEST_RESULT", "testno", "testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"]);
            if (!String.IsNullOrEmpty(Session["Test_No_OBJ"].ToString()))
            {
                SQLHelper objSqlHelper = new SQLHelper(_nitmn_constr);
                string sql = "DELETE FROM ACD_ITEST_RESULT WHERE testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"];
                objSqlHelper.ExecuteNonQuery(sql);
            }

            int correctAns = Convert.ToInt32(objCommon.LookUp("ACD_ITLE_RESULTCOPY", "COUNT(QUESTIONNO) AS COR_ANS_COUNT", "CORRECTANS=SELECTED AND ANS_STAT IN ('S','R') AND testno=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"] + " AND COURSENO=" + Session["COURSENO_OBJ"]));
            decimal totmarks = Convert.ToDecimal(Session["TOTQUES"]);
            decimal correctMarks = Convert.ToDecimal(correctAns);
            int studAttempt = Convert.ToInt32(Session["STUDATTEMPTS"]) + 1;

            int cs = Convert.ToInt32(objIETestc.AddIETestResult(Convert.ToInt32(Session["TDNO_OBJ"]), Convert.ToInt32(Session["TOTQUES"]), correctAns, totmarks, correctMarks, studAttempt, Convert.ToInt32(Session["COURSENO_OBJ"])));
            if (cs != -99)
            {
                //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
            }

            string Flag = Session["SHOWRESULT"].ToString();//objCommon.LookUp("ACD_ITESTMASTER", "SHOWRESULT", "TESTNO=" + Session["Test_No_OBJ"].ToString());
            if (Flag.Trim() == "Y")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseFinishModal();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);
                Session["SHOWRESULT"] = "Y";
                Response.Redirect("ITLEResult.aspx", false);
            }
            else if (Flag.Trim() == "N")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseFinishModal();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);
                Session["SHOWRESULT"] = "N";
                Response.Redirect("Thanks.aspx", false);

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseFinishModal();", true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Exam_Test.btnCloseModalPopup_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnUpdateMalFunction_Click(object sender, EventArgs e)
    {
        try
        {
            lblAlert.Text = "Please do not switch window while test is in progress. Doing so " + hdnMalfunction.Value + " times will terminate the test, and you will not be able to get back to it.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
            // $('#myCommonAlertModal').modal('show');
            int testNo = Convert.ToInt32(Session["Test_No_OBJ"]);
            int idno = Convert.ToInt32(Session["idno"].ToString());
            int malfunction = hdnMalfunctionStudent.Value == "" ? 0 : Convert.ToInt32(hdnMalfunctionStudent.Value);
            int count = malfunction + 1;
            int ret = objIETestc.UpdateMalfunctionCount(testNo, idno, count);
            hdnMalfunctionStudent.Value = objCommon.LookUp("ACD_ITEST_DETAILS", "MalFunctionCount", "TESTNO=" + testNo + " AND IDNO=" + idno).ToString(); ;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CloseModal();", true);

        }
        catch (Exception ex)
        {

            // throw;
        }
    }
}