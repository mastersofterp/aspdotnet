using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
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
//using System.Transactions;
using System.IO;
using System.Net;
using System.Text;
using IITMS.NITPRM;
using System.Text.RegularExpressions;
using IITMS.SQLServer.SQLDAL;



public partial class Itle_ITLEStudTest : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseControlleritle objCourse = new CourseControlleritle();
    ITestMasterController objTestc = new ITestMasterController();
    ITestMaster objTM = new ITestMaster();
    string PageId;
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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
        //CheckBrowser();
        if (!Page.IsPostBack)
        {
            //string IpAdd = GetRemoteIP();
            ////Check Session

            //int dscheck = Convert.ToInt32(objCommon.LookUp("Lib_MapIPADD", "count(*)", "IPADD='" + IpAdd + "'"));

            //if (dscheck > 0)
            //{
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                PageId = Request.QueryString["pageno"];

                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourse.Text = Session["ICourseName"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourse.ForeColor = System.Drawing.Color.Green;
                //objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", "TESTNO>0", "TESTNO");
                //this.btnStart.Attributes.Add("onClick", "disableBackButton()");
                //this.btnStart.Attributes.Add("onClick", "Window()");

                ddlTestType_SelectedIndexChanged(sender, e);

            }

            // Used to insert id,date and courseno in Log_History table
            int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));
            //}
            //else
            //{
            //    //objCommon.DisplayUserMessage(UpdatePanel1, "You are not authorized for the Test Please contact Administrator!", this.Page);
            //    Response.Redirect("Itle_TestAuth.aspx");
            //}

        }

    }

    #endregion

    #region Private Method

  

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudTest.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudTest.aspx");
        }
    }

    private void BindListView()
    {
        try
        {

            CourseControlleritle objAC = new CourseControlleritle();
            DataSet ds = null;

            int COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            if (Convert.ToInt32(Session["usertype"]) == 2 || Convert.ToInt32(Session["usertype"]) == 4)
            {
                ds = objAC.GetTestAll(COURSENO, ddlTestType.SelectedValue, Convert.ToInt32(Session["idno"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvTest.DataSource = ds;
                    lvTest.DataBind();
                    DivTestList.Visible = true;
                }
                else
                {
                    lvTest.DataSource = null;
                    lvTest.DataBind();
                    DivTestList.Visible = true;
                }
                //lblSession.Text = ds.Tables[0].Rows[0].
                //lblSession.Text = Session["SESSION_NAME"].ToString();
                //lblSession.ToolTip = ddlSession.SelectedValue.ToString();
                //lblSession.ToolTip = Session["SessionNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "ITLE_StudTest.aspx.BindListView->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    //IT WAS USED PREVIOUSLY
    private void CheckBrowser()
    {
        try
        {
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            string s = browser.Browser;
            if (s.Equals("IE"))
            {
            }
            else
            {

                Response.Redirect("Browser.aspx");
                return;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion

    #region Public Method

    public string GetIP()
    {
        string Str = "";
        Str = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
        IPAddress[] addr = ipEntry.AddressList;
        return addr[addr.Length - 1].ToString();

    }

    public static string GetRemoteIP()
    {
        string strValue = "";
        //Gets a comma-delimited list of IP Addresses
        string ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //If any are available - use the first one
        if (!string.IsNullOrEmpty(ipList))
        {
            strValue = ipList.Split(',')[0];
        }
        else
        {
            strValue = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return strValue;
    }

    #endregion

    #region Page Events

    protected void btnlnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
            Session["post_OBJ"] = 0;
            LinkButton lbnTest = sender as LinkButton;
            Session["Test_Type_OBJ"] = (lbnTest.ToolTip.ToString());

            if (Session["Test_Type_OBJ"].ToString() == "OBJECTIVE")
            {
                Session["TestType_OBJ"] = "O";
            }
            else
            {
                Session["TestType_OBJ"] = "D";
            }


            Session["post_OBJ"] = 0;
            LinkButton btnSelect = sender as LinkButton;
            int Test_No = Convert.ToInt32(int.Parse(btnSelect.CommandArgument));
            string Test_Name = btnSelect.Text;
            DataSet ds = objCommon.FillDropDown("ACD_ITESTMASTER", "*", "TESTDURATION", "TESTNO=" + Convert.ToInt32(Test_No), "TESTNO");
            Session["Total_Marks_OBJ"] = ds.Tables[0].Rows[0]["TOTAL"].ToString();

            string StartTime = ds.Tables[0].Rows[0]["STARTDATE"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTDATE"].ToString()).ToString("HH:mm:ss");
            string EndTime = ds.Tables[0].Rows[0]["ENDDATE"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("HH:mm:ss");

            if (Convert.ToDateTime(StartTime).TimeOfDay <= DateTime.Now.TimeOfDay && Convert.ToDateTime(EndTime).TimeOfDay >= DateTime.Now.TimeOfDay)
            {


                string Time = ds.Tables[0].Rows[0]["TESTDURATION"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss");
                Session["Total_Time_For_Test_OBJ"] = Time;


                ////////////////////////////////////////

                TimeSpan ts = TimeSpan.Parse(Time);
                double totalMinits = ts.TotalMinutes;
                int totMin = Convert.ToInt32(totalMinits);

                Session["totMin_OBJ"] = totMin;

                ///////////////////////////////////////////////////

                string Finalsub = objCommon.LookUp("ACD_ITEST_RESULT", "isnull(finalSubmit,0)", "TESTNO=" + Convert.ToInt32(Test_No) + " AND IDNO=" + Session["idno"]);

                if(Finalsub =="")
                {
                    Finalsub="0";
                }

                int Attempts = Convert.ToInt32(ds.Tables[0].Rows[0]["ATTEMPTS"]);
                if (Attempts > 0 && Finalsub != "0")
                {
                    //DataSet ds1 = objCommon.FillDropDown("ITLE_TESTRESULT", "", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(Test_No) + " AND IDNO=" + Session["STUDID"], "");
                    string ret = objCommon.LookUp("ACD_ITEST_RESULT", "MAX(STUDATTEMPTS)", "TESTNO=" + Convert.ToInt32(Test_No) + " AND IDNO=" + Session["idno"]);
                    string ret1 = objCommon.LookUp("ACD_ISTUDENTINFO", "isnull(ATTEMPTS_ALLOWED,0)", "TESTNO=" + Convert.ToInt32(Test_No) + " AND STUDENTID=" + Session["idno"]);
                    //if (ds1.Tables[0].Rows.Count == 0 || Convert.ToInt32(ds1.Tables[0].Rows[0]["STUDATTEMPTS"]) < Attempts)
                    if (Finalsub == "0")
                    {
                        
                            if (ret == "" || Convert.ToInt32(ret) < Attempts || ret1 == "1")
                            {
                                Session["NOOFATTEMPT_OBJ"] = Attempts;
                            }
                            else
                            {
                                objCommon.DisplayUserMessage(UpdatePanel1, "Your attempts are over for this test", this);
                                return;
                            }
                        
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(UpdatePanel1, "Final Submition Done for this test !!", this);
                        return;
                    }
                }
                //else
                //{
                   
                //        if ()
                //        {
                //            objCommon.DisplayUserMessage(UpdatePanel1, "Final Submition Done for this test !!", this);
                //            return;
                //        }
                    
                //}
                Session["TOTALMARKS_OBJ"] = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL"]);
                DateTime Time1 = Convert.ToDateTime(Time);
                Session["Test_No_OBJ"] = Test_No;
                Session["TestName_OBJ"] = Test_Name;
                Session["COURSENO_OBJ"] = ds.Tables[0].Rows[0]["COURSENO"];

                //objAC.Update_ITLE_User(Convert.ToInt32(Session["userno"]), 0, Test_No);



                //// refresh the value for necessary session
                Session["TDNO_OBJ"] = null;
                int[] _myArrOp = new int[6];
                for (int i = 0; i < 6; i++)
                    _myArrOp[i] = i + 1;

                _myArrOp = (int[])Shuffle(_myArrOp);
                Session["RandAnsSrList_OBJ"] = _myArrOp;
                Session["CurQuesNo_OBJ"] = null;
                Session["qNoList_OBJ"] = null;

                string tdno = objCommon.LookUp("ACD_ITEST_DETAILS", "TDNO", "TESTNO=" + Session["Test_No_OBJ"].ToString() + " AND IDNO=" + Session["idno"].ToString());
                if (!string.IsNullOrEmpty(tdno))
                {
                    Session["TDNO_OBJ"] = Convert.ToInt32(tdno);

                }


                // ---------By Zubair Date:28-10-2014----------
                string QuesRandom = objCommon.LookUp("ACD_ITESTMASTER", "SHOWRANDOM", "TESTNO=" + Session["Test_No_OBJ"]);
                if (QuesRandom.Contains("N"))
                {
                    objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "'");//Session["userno"]);
                    string questioSet = objCommon.LookUp("ACD_IQUESTION_SET_FOR_TEST", "QUESTION_SET", "TESTNO=" + Session["Test_No_OBJ"]);
                    string MyQuery = "select  *, 1 AS CORRECT_MARKS," + Session["idno"] + ",'R' ANS_STATE from ACD_IQUESTIONBANK where " + "COURSENO=" + Session["COURSENO_OBJ"].ToString() + "and QUESTIONNO in(" + questioSet + ")" + "AND QUESTION_TYPE='" + Session["TestType_OBJ"].ToString() + "'" + " order by newid()";
                    CustomStatus cs = (CustomStatus)objTestc.AddITestMaster_Temp(MyQuery);
                }
                else
                {

                    //--------Tarun Date:16-1-2014---------

                    objSqlHelper.ExecuteNonQuery("delete from ACD_IQUESTIONBANK_TEMP_CREATION where studentid='" + Session["idno"] + "'");//Session["userno"]);

                    DataSet dsT = objCommon.FillDropDown("ACD_ITESTMASTER", "TOPICS,QUESTIONRATIO", "QUESTIONRATIO", "testno=" + Session["Test_No_OBJ"].ToString(), "");//objSqlHelper.ExecuteDataSet(Query);
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

                                    string MyQuery = "select TOP " + QRatio + " *, 1 AS CORRECT_MARKS," + Session["idno"] + ",'R' ANS_STATE from ACD_IQUESTIONBANK where " + "COURSENO=" + Session["COURSENO_OBJ"].ToString() + "and Topic in(" + QTopic + ")" + "AND QUESTION_TYPE='" + Session["TestType_OBJ"].ToString() + "'" + " order by newid()";
                                    CustomStatus cs = (CustomStatus)objTestc.AddITestMaster_Temp(MyQuery);


                                }
                            }
                        }
                    }
                    //--------End---------
                }


                HttpCookie h = new HttpCookie("start");
                Response.Cookies.Clear();
                h.Value = Time;
                Response.Cookies.Add(h);


                int sec = (Time1.Hour * 60 * 60) + (Time1.Minute * 60) + Time1.Second;
                Session["SEC_OBJ"] = sec * 1000;

                //Session["SEC_OBJ"] = 70000;

                string Script = string.Empty;

                if (Session["Test_Type_OBJ"].ToString() == "DESCRIPTIVE")
                {
                    //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("StudTest")));
                    //url += "Test_Descriptive.aspx?";

                    //Script += " window.open('Test_Descriptive.aspx','PoP_Up','width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


                    string url3 = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/Itle/Test_Descriptive.aspx?";
                    Response.Redirect(String.Format(url3, HttpUtility.UrlEncode(HF1.Value), HttpUtility.UrlEncode(HF2.Value)), false);

                    Context.ApplicationInstance.CompleteRequest();

                }
                else
                {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ITLEStudTest")));
                    url += "ITLETest.aspx?";

                    Script += " window.open('ITLETest.aspx','PoP_Up','status=0,width=0,height=0,menubar=no,location=no,toolbar=no,scrollbars=1,resizable=yes,fullscreen=1');";
                    
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
                   
                }
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Invalid Time", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(UpdatePanel1, "ITLE_StudTest.aspx.btnlnkSelect_Click->  " + ex.Message + ex.StackTrace, this.Page);
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

    private String SanitizeUserInput(String text)
    {
        if (String.IsNullOrEmpty(text))
            return String.Empty;

        String rxPattern = "<(?>\"[^\"]*\"|'[^']*'|[^'\">])*>";
        Regex rx = new Regex(rxPattern);
        String output = rx.Replace(text, String.Empty);

        return output;
    }

    protected void ddlTestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        objTM.TEST_TYPE = ddlTestType.SelectedValue;
        BindListView();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlTestType.SelectedValue = "0";
        DivTestList.Visible = false;
       
    }

    #endregion

    #region Commented Codes

    //protected void btnStart_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int Test_No = Convert.ToInt32(ddlTest.SelectedValue);
    //        DataSet ds = objCommon.FillDropDown("ACD_ITESTMASTER", "*", "TESTDURATION", "TESTNO=" + Convert.ToInt32(Test_No), "TESTNO");
    //        string Time = ds.Tables[0].Rows[0]["TESTDURATION"] == null ? "" : Convert.ToDateTime(ds.Tables[0].Rows[0]["TESTDURATION"].ToString()).ToString("HH:mm:ss");
    //        DateTime Time1 = Convert.ToDateTime(Time);
    //        Session["Test_No_OBJ"] = Test_No;
    //        HttpCookie h = new HttpCookie("start");
    //        Response.Cookies.Clear();
    //        h.Value = Time;
    //        Response.Cookies.Add(h);


    //        int sec = (Time1.Hour * 60 * 60) + (Time1.Minute * 60) + Time1.Second;
    //        Session["SEC_OBJ"] = sec * 1000;

    //        //this.btnStart.Attributes.Add("onClick", "disableBackButton()");
    //        this.btnStart.Attributes.Add("onClick", "open_win()");

    //        //Response.Redirect("Test.aspx");
    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayUserMessage(UpdatePanel1, "Invalid Time", this.Page);
    //        return;
    //    }


    //}


    //BELOW METHOD CAME FROM CCMS_ITLE TO START TEST

    #endregion

    protected void lvTest_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}