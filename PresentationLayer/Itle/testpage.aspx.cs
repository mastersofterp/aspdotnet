//=================================================================================
// PROJECT NAME  : CCMS                                                           
// MODULE NAME   : ITLE                                              
// PAGE NAME     : Test.aspx                                  
// CREATION DATE : 05-10-2011                                                   
// CREATED BY    : MAZHAR S.PATHAN                  
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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;




using System.Collections.Generic;

public partial class Itle_testpage : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITestMasterController objTestc = new ITestMasterController();
    ITestResultController objresult = new ITestResultController();
    ITestMaster objTest = new ITestMaster();
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

    int count = 0;

    //--------------------------------------------------below is ccms

    //Common objCommon = new Common();
    //UAIMS_Common objUCommon = new UAIMS_Common();
    
    IQuestionbankController objIQBC = new IQuestionbankController();

    //DataTable dt = new DataTable();
    //int ctr = 0;
    //long timerStartValue = 1800;
    //string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["CCMS_REG"].ConnectionString;
    //int t = 0;
    //int count = 0;
    //int no2 = 0;
    //int no3 = 0;
    //DateTime date;
    //DateTime mydate;
    #region Page Events


    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["CollegeId"],Session["UserName"],Session["Password"],
    /// Session["DataBase"].If the user has not Login, he will be redirected to "default.aspx" page. If there is request for first time for this 
    /// page then description for Page help is retrived from MENUHELPMASTER table and stored under Cache variable.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //Response.Redirect(this.Request.Url.ToString(), false);
    //    //if (IsPostBack)
    //    //{
    //    //    if ((bool)HttpContext.Current.Items["IsRefresh"])
    //    //    {
    //    //        Response.Write("refreshed");
    //    //    }
    //    //    else
    //    //    {
    //    //        Response.Write("Postback");
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    Response.Write("First Load");
    //    //}



    //    string dt = Request.Cookies["start"].Value;
    //    mydate =Convert.ToDateTime(dt);
    //    //For displaying user friendly messages
    //    Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
    //    Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
    //    Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
    //    Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

    //    //Check Session
    //    if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
    //    {
    //       // objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
    //    }
    //    else
    //    {
    //        Response.Redirect("~/Default.aspx");
    //    }

    //    //objIQBC = new IQuestionbankController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
    //    if (!Page.IsPostBack)
    //    {

            
    //        Session["post"] = Convert.ToInt32(Session["post"]) + Convert.ToInt32(Session["post"]);

            

    //        if (Session["ICourseNo"] == null)
    //            Response.Redirect("ITLE_Select_Course.aspx");

    //        DataSet ds = new DataSet();

    //        //if (HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()] == null)
    //        //{
    //        //    ds = objCommon.FillDropDown("CCMS_HELP_client", "HELPDESC", "PAGENAME", "PAGENAME='" + objCommon.GetCurrentPageName() + "'", "");
    //        //}
    //        //else
    //        //{
    //        //    ds = (DataSet)HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()];
    //        //    DataView dv = ds.Tables[0].DefaultView;
    //        //    dv.RowFilter = "PAGENAME='" + objCommon.GetCurrentPageName() + "'";
    //        //    ds.Tables.Remove("Table");
    //        //    ds.Tables.Add(dv.ToTable());
    //        //}



    //        //if (ds.Tables[0].Rows.Count > 0)
    //        //{
    //        //    lblHelp.Text = ds.Tables[0].Rows[0]["HELPDESC"].ToString();
    //        //}
    //        //else
    //        //{
    //        //    lblHelp.Text = "No Help Present!";
    //        //}
    //        //Page.Title = Session["coll_name"].ToString();
    //        lblUrname.Text = Convert.ToString(Session["USERFULLNAME"].ToString());
    //        this.timerStartValue = long.Parse(Session["SEC"].ToString());
    //        this.TimerInterval = 500;
    //        lblSession.Text = Session["sessionname"].ToString();
    //        lblSession.ToolTip = Session["currentsession"].ToString();
    //        lblCourse.Text = Session["ICourseName"].ToString();
    //        //
    //        //HttpCookie Time = Request.Cookies["start"];
    //        //string name = Session["useridname"].ToString();
    //        //name = name.Substring(0, name.IndexOf("@"));
    //        //lblSeatno.Text = objCommon.LookUp("student_result", "ROLLNO", "IDNO=" + name);
    //        //objResult.TESTNO = Convert.ToInt32(Session["Test_No"]);
    //        //objResult.IDNO = Convert.ToInt32(Session["STUDID"]);
    //        //objResult.SDSRNO = Convert.ToInt32(Session["SDSRNO"]);
    //        //BindShowQues();
    //        //COUNT();
    //        lblQueNo.Text = "1" + "/" + Convert.ToInt32(Session["NOQ"].ToString());
    //    }
        
    //    Session["post"] =Convert.ToInt32(Session["post"]) + 1;

    //    string[] ctl = Request.Form.GetValues("__EVENTTARGET");

    //    if (Convert.ToInt32(Session["post"]) > 1 & ctl == null)
    //        Response.Redirect("Result.aspx");
    //    //else
    //    //    Response.Redirect(this.Request.Url.ToString(), false);
        
    //    //StringBuilder bldr = new StringBuilder();
    //    ////bldr.AppendFormat("var Timer = new myTimer({0},{1},'{2}','timerData');", this.timerStartValue, this.TimerInterval, this.lblTimerCount.ClientID);
    //    //bldr.Append("closewin()");
    //    //ClientScript.RegisterStartupScript(this.GetType(), "TimerScript", bldr.ToString(), true);
    //    //ClientScript.RegisterHiddenField("timerData", timerStartValue.ToString());


    //}
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

                lblUrname.Text = Convert.ToString(Session["USERFULLNAME"].ToString());

                //Page Authorization
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.timerStartValue = long.Parse(Session["SEC"].ToString());
                this.TimerInterval = 500;
                lblSession.Text = Session["sessionname"].ToString();
                lblSession.ToolTip = Session["currentsession"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();
                objTest.TESTNO = Convert.ToInt32(Session["TESTNO"]);
                // objTest.IDNO = Convert.ToInt32(Session["STUDID"]);
                objTest.UA_NO = Convert.ToInt32(Session["idno"].ToString());
                objTest.SDSRNO = Convert.ToInt32(Session["SUBID"]);
                HttpCookie Time = Request.Cookies["start"];
                string name = Session["username"].ToString();
                lblSeatno.Text = objCommon.LookUp("UAIMSACAD.ACAD_STUDENT_RESULT", "ROLL_NO", "IDNO=" + name);

                BindShowQues();
                // COUNT();
                lblQueNo.Text = "1" + "/" + Convert.ToInt32(Session["NOQ"].ToString());
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('Test.aspx','','fullscreen=yes');", true);
            }
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
            objTest.TEST_NO = Convert.ToInt32(Session["Test_No"]);
            objTest.UA_NO = Convert.ToInt32(Session["userno"]);
            objTest.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            const int selectques = 3;
            DataRow dr = null;
            int[] a = new int[selectques - 1];
            ArrayList list = new ArrayList();
            int noofquestions = Convert.ToInt32(objCommon.LookUp("ACD_ITESTQUESTION", "COUNT(TESTNO)", "TESTNO=" + Convert.ToInt32(Session["Test_No"])));
            Session["NOQ"] = Convert.ToInt32(noofquestions);
            Session["noofquestions"] = noofquestions - 2;
            lblQueNo.Visible = false;




            int[] _myArr = new int[noofquestions];

            for (int i = 0; i < noofquestions; i++)
                _myArr[i] = i + 1;

            _myArr = (int[])Shuffle(_myArr);


            SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
            String sql = "SELECT T.QU_SRNO,Q.QUESTIONIMAGE,TYPE,Q.QUESTIONTEXT,Q.ANS1TEXT,ANS1IMG,ANS2IMG,ANS3IMG,ANS4IMG,ANS2TEXT,ANS3TEXT,ANS4TEXT,CORRECTANS,T.CORRECT_MARKS FROM ACD_IQUESTIONBANK Q INNER JOIN  ACD_ITESTQUESTION  T ON (T.QUESTIONNO=Q.QUESTIONNO)WHERE Q.COURSENO=" + objTest.COURSENO + "AND T.TESTNO=" + Convert.ToInt32(objTest.TEST_NO);
            DataSet ds = objSqlHelper.ExecuteDataSet(sql);

            lvTest.DataSource = ds;
            lvTest.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtTbl = (DataTable)Session["Answered"];

                int i = 0;
                foreach (ListViewDataItem li in lvTest.Items)
                {



                    if (ds.Tables[0].Rows[i]["TYPE"].Equals("T"))
                    {

                        RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;
                        r.Items.Add(ds.Tables[0].Rows[i]["ANS1TEXT"].ToString());
                        r.Items.Add(ds.Tables[0].Rows[i]["ANS2TEXT"].ToString());
                        r.Items.Add(ds.Tables[0].Rows[i]["ANS3TEXT"].ToString());
                        r.Items.Add(ds.Tables[0].Rows[i]["ANS4TEXT"].ToString());

                    }
                    else
                    {
                        string imageBankFolder = "IMAGE_QUESTION/";

                        RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;

                        r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS1IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS1TEXT"].ToString());
                        r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS2IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS2TEXT"].ToString());
                        r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS3IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS3TEXT"].ToString());
                        r.Items.Add("<img src=" + imageBankFolder + ds.Tables[0].Rows[i]["ANS4IMG"].ToString() + " width=70px>" + ds.Tables[0].Rows[i]["ANS4TEXT"].ToString());

                    }
                    i++;
                    lblQueNo.Visible = false;
                }
            }






            for (int i = 0; i < _myArr.Length; i++)
            {

                //SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
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
                    dtRe.Columns.Add(new DataColumn("QUESTIONIMAGE", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS1IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS2IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS3IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("ANS4IMG", typeof(string)));
                    dtRe.Columns.Add(new DataColumn("CORRECTANS", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("SELECTED", typeof(int)));
                    dtRe.Columns.Add(new DataColumn("CORRECT_MARKS", typeof(decimal)));
                    dtRe.Columns.Add(new DataColumn("USERNO", typeof(int)));

                    //foreach (DataRow r in ds.Tables[0].Rows)
                    //{
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        int g = Convert.ToInt32(_myArr.GetValue(j));

                        if (g.Equals(Convert.ToInt32(ds.Tables[0].Rows[g - 1]["QU_SRNO"].ToString())))
                        {

                            dr = dtRe.NewRow();
                            dr["QU_SRNO"] = dtRe.Rows.Count + 1;
                            dr["QUENO"] = ds.Tables[0].Rows[g - 1]["QU_SRNO"].ToString();
                            dr["QUESTIONTEXT"] = ds.Tables[0].Rows[g - 1]["QUESTIONTEXT"].ToString();
                            dr["ANS1TEXT"] = ds.Tables[0].Rows[g - 1]["ANS1TEXT"].ToString();
                            dr["ANS2TEXT"] = ds.Tables[0].Rows[g - 1]["ANS2TEXT"].ToString();
                            dr["ANS3TEXT"] = ds.Tables[0].Rows[g - 1]["ANS3TEXT"].ToString();
                            dr["ANS4TEXT"] = ds.Tables[0].Rows[g - 1]["ANS4TEXT"].ToString();
                            dr["QUESTIONIMAGE"] = ds.Tables[0].Rows[g - 1]["QUESTIONIMAGE"].ToString();
                            dr["ANS1IMG"] = ds.Tables[0].Rows[g - 1]["ANS1IMG"].ToString();
                            dr["ANS2IMG"] = ds.Tables[0].Rows[g - 1]["ANS2IMG"].ToString();
                            dr["ANS3IMG"] = ds.Tables[0].Rows[g - 1]["ANS3IMG"].ToString();
                            dr["ANS4IMG"] = ds.Tables[0].Rows[g - 1]["ANS4IMG"].ToString();
                            dr["CORRECTANS"] = ds.Tables[0].Rows[g - 1]["CORRECTANS"].ToString();
                            dr["CORRECT_MARKS"] = ds.Tables[0].Rows[g - 1]["CORRECT_MARKS"].ToString();

                            dr["SELECTED"] = -1;
                            dr["USERNO"] = Convert.ToInt32(Session["userno"].ToString());
                            dtRe.Rows.Add(dr);
                            Session["Answered"] = dtRe;
                        }
                    }


                    //}
                }
                //show();

            }




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ITLE_Test.BindShowQues -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
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
        r.SelectedIndex = Convert.ToInt32(dt.Rows[ctr]["selected"]);
        Session["ctr"] = ctr;
    }
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
                    drow["SELECTED"] = Convert.ToString(rl.SelectedIndex);
                }
                //Convert.ToInt32(drow["QUESTIONNO"])==Convert.ToInt32(Label1.Text)
            }

        }
        Session["Answered"] = dt;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //dt = (DataTable)Session["Answered"];
        //decimal TOTALMARKS = 0.0m;

        //foreach (DataRow X in   dt.Rows)
        //{
        //    if (Convert.ToInt32(X["SELECTED"].ToString()) +1  == Convert.ToInt32(X["CORRECTANS"].ToString()))
        //    {
        //        TOTALMARKS += Convert.ToDecimal(X["CORRECT_MARKS"].ToString());

        //    }
        //}


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

        decimal TOTALMARKS = 0.0m;
        int NOOFCORRECTANS = 0;
        int TotQue = dt.Rows.Count;
        int StudAttempt;
        Session["TOTQUES"] = TotQue;
        foreach (DataRow X in dt.Rows)
        {
            objTest.CORRECTMARKS += Convert.ToInt32(X["CORRECT_MARKS"]);
            if (Convert.ToInt32(X["SELECTED"].ToString()) + 1 == Convert.ToInt32(X["CORRECTANS"].ToString()))
            {
                TOTALMARKS += Convert.ToDecimal(X["CORRECT_MARKS"].ToString());
                Session["TOTSCORE"] = Convert.ToInt32(TOTALMARKS);
                NOOFCORRECTANS += 1;
                Session["NOCORRANS"] = NOOFCORRECTANS;

            }
        }

        //foreach (DataRow X in dt.Rows)
        //{
        //    objTest.CORRECTMARKS += Convert.ToInt32(X["CORRECT_MARKS"]);
        //    if (Convert.ToInt32(X["SELECTED"].ToString()) == Convert.ToInt32(X["CORRECTANS"].ToString()))
        //    {
        //        TOTALMARKS += Convert.ToDecimal(X["CORRECT_MARKS"].ToString());
        //        Session["TOTSCORE"] = Convert.ToInt32(TOTALMARKS);
        //        NOOFCORRECTANS += 1;
        //        Session["NOCORRANS"] = NOOFCORRECTANS;
        //        //Response.Redirect("Result.aspx");

        //        //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);

        //    }
        //}
        objTest.TEST_NO = Convert.ToInt32(Session["Test_No"]);
        objTest.IDNO = Convert.ToInt32(Session["idno"].ToString());
        //objTest.CORRECTMARKS = Convert.ToInt32(X["CORRECT_MARKS"]);
        objTest.TOTAL = Convert.ToInt32(TOTALMARKS);
        objTest.SDSRNO = Convert.ToInt32(Session["ICourseNo"]);
        //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);
        objTest.COLLEGE_CODE = Session["colcode"].ToString();

        //DataSet ds = objCommon.FillDropDown("ITLE_TESTRESULT", "*", "", "TEST_NO" + Convert.ToInt32(objTest.TEST_NO) + " AND IDNO" + Session["STUDID"], "");
        DataSet ds = objCommon.FillDropDown("ACD_ITESTMASTER", "*", "TESTDURATION", "TESTNO=" + objTest.TEST_NO, "TESTNO");
        //string ret = objCommon.LookUp("ITLE_TESTRESULT", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(objTest.TEST_NO) + " AND IDNO=" + Session["STUDID"]);
        string ret = objCommon.LookUp("ITLE_TESTRESULT", "MAX(STUDATTEMPTS)", "TESTNO=" + objTest.TEST_NO + "");
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

        objTest.STUDATTEMPTS = StudAttempt;
        int cs = Convert.ToInt32(objresult.AddTestResult2(objTest));
        if (cs != -99)
        {
            objCommon.DisplayMessage("Record Saved Sucessfully", this.Page);
        }
        Response.Redirect("Thanks.aspx");
    }
    private void oldcode()
    {

        if (ds1.Tables[0].Rows.Count > 0)
        {
            DataTable dtTbl = (DataTable)Session["Answered"];


            foreach (ListViewDataItem li in lvTest.Items)
            {

                int i = 0;

                if (ds1.Tables[0].Rows[i]["TYPE"].Equals("T"))
                {
                    Label lblQ = (Label)li.FindControl("Label1");
                    lblQ.Text = ds1.Tables[0].Rows[i]["QU_SRNO"].ToString() + ".";

                    Label lblQuestion = (Label)li.FindControl("Label2");
                    lblQuestion.Text = ds1.Tables[0].Rows[i]["QUESTIONTEXT"].ToString();

                    RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;
                    r.Items.Add(ds1.Tables[0].Rows[i]["ANS1TEXT"].ToString());
                    r.Items.Add(ds1.Tables[0].Rows[i]["ANS2TEXT"].ToString());
                    r.Items.Add(ds1.Tables[0].Rows[i]["ANS3TEXT"].ToString());
                    r.Items.Add(ds1.Tables[0].Rows[i]["ANS4TEXT"].ToString());
                    ((RadioButtonList)lvTest.FindControl("RadioButtonList1")).Items.Add(ds1.Tables[0].Rows[0]["ANS1TEXT"].ToString());
                }
                else
                {
                    string imageBankFolder = "IMAGE_QUESTION/";

                    Label lblQ = (Label)li.FindControl("Label1");
                    lblQ.Text = ds1.Tables[0].Rows[i]["QU_SRNO"].ToString() + ".";

                    Label lblQuestion = (Label)li.FindControl("Label2");
                    lblQuestion.Text = ds1.Tables[0].Rows[i]["QUESTIONTEXT"].ToString();
                    RadioButtonList r = (RadioButtonList)li.FindControl("RadioButtonList1") as RadioButtonList;

                    r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS1IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS1TEXT"].ToString());
                    r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS2IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS2TEXT"].ToString());
                    r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS3IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS3TEXT"].ToString());
                    r.Items.Add("<img src=" + imageBankFolder + ds1.Tables[0].Rows[i]["ANS4IMG"].ToString() + " width=70px>" + ds1.Tables[0].Rows[i]["ANS4TEXT"].ToString());

                }


            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //System.Windows.Forms.MessageBox.Show(this.lblTimerCount.Text);
    }


    //Used to get activate when time out occure without submitting...
    //[System.Web.Services.WebMethod]
    //public static void GetCurrentTime()
    //{
    //    ITLE_Test obj = new ITLE_Test();
    //    int StudAttempt = 0;
    //    TestResult objResult = new TestResult();
    //    IQuestionbankController objIQBC = new IQuestionbankController();
    //    objResult.STUDATTEMPTS = StudAttempt + 1;
    //    objResult.TESTNO = Convert.ToInt32(obj.Session["Test_No"]);
    //    objResult.IDNO = Convert.ToInt32(obj.Session["STUDID"]);
    //    objResult.SDSRNO = Convert.ToInt32(obj.Session["SDSRNO"]);
    //    objResult.CORRECTMARKS = 0;
    //    objResult.TOTALMARKS = 0;
        
    //    //objResult.SDSRNO = Convert.ToInt32(X["SDSRNO"]);
    //    objResult.COLLEGE_CODE = "BIRLA";
    //    try
    //    {
    //        SqlConnection myconnection;
    //        //myconnection = new SqlConnection(@"Password=iitms!123;User ID=sa; SERVER=IITMSSERVER\MSSQL2008R2;DataBase=" + obj.Session["DataBase"].ToString().Trim() + ";");
            
    //        objIQBC = new IQuestionbankController(obj.Session["UserName"].ToString().Trim(), obj.Session["Password"].ToString().Trim(), obj.Session["DataBase"].ToString().Trim());
    //        myconnection = new SqlConnection(objIQBC._client_constr);
    //        //myconnection.ConnectionString = "@" + obj._client_constr;  // Set database path to connection string
    //        myconnection.Open();
    //        SqlCommand cmd = new SqlCommand("Insert into ITLE_TESTRESULT(TESTNO,IDNO,SDSRNO,TOTALMARKS,CORRECTMARKS,TESTDATE,COLLEGE_CODE,STUDATTEMPTS)" +
    //              " values(" + objResult.TESTNO + "," + objResult.IDNO + "," + objResult.SDSRNO + "," + 0 + "," + 0 + ",GETDATE(),'BIRLA',(select max(studattempts) from ITLE_TESTRESULT where testno=" + objResult.TESTNO + " and idno=" + objResult.IDNO + ")+1)", myconnection);
    //        //cmd.Connection = myconnection;
    //        cmd.ExecuteNonQuery();
    //        //SqlConnection con = new SqlConnection(obj._client_constr);
    //        //string sql = "";
    //        //SqlCommand cm = new SqlCommand();
    //        //int cs = Convert.ToInt32(obj.objIQBC.AddTestResult(objResult));
    //        //if (cs != -99)
    //        //{
    //        //objCommon.DisplayUserMessage("Record Saved Sucessfully", this.Page);
    //        //}
    //        //System.Windows.Forms.MessageBox.Show("hello");
    //    }
    //    catch(Exception ex)
    //    {

    //    }
    //}
}
