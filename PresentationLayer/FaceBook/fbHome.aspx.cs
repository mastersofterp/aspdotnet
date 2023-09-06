
//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : TO CREATE HOME PAGE                                             
// CREATION DATE : 13-April-2009
// CREATED BY    : NIRAJ D. PHALKE & ASHWINI BARBATE                               
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.BusinessEntities;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using InfoSoftGlobal;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Text;
using System.IO;
using System.Net;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Web.Services;
using System.Web.Script.Serialization;
using BusinessLogicLayer.BusinessLogic.FaceBook;
using  Newtonsoft.Json;
public partial class FaceBook_fbHome : System.Web.UI.Page
{
   
    Common objCommon = new Common();
    User_AccController objUACC = new User_AccController();
    Access_LinkController objAL = new Access_LinkController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FetchDataController objFetch = new FetchDataController();
    AccessUser objFbU = new AccessUser();

    public string sMarquee = string.Empty;
    public string Notice = string.Empty;

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
        //Check Session
        
        if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        //GetFbDetails();
        // string no = Session["username"].ToString();
        //int sessionno = Convert.ToInt32(objCommon.LookUp("ACD_DASHBOARD_MASTER", "SESSIONNO", "status=1"));
        //string sessionname = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + Convert.ToInt32(sessionno));
        if (!Page.IsPostBack)
        {
            string msg = objCommon.LookUp("REFF", "POPUP_MSG", "POPUP_FLAG=1");
            if (msg != "")
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                pmarq.InnerText = msg;
                divmarquee.Visible = true;
                lblpopup.Text = msg;
                if (id != 101)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Hello", "functionConfirm()", true);
                }
            }
            if (Session["usertype"].ToString() == "3")
            {
                // trNews.Visible = false;
                //trTimeTable.Visible = true;
                // this.sessionname();
                CourseTeacherAllotController objCT = new CourseTeacherAllotController();
                ShowQuickLinks();
                //   int sessionNo = Convert.ToInt16(objCommon.LookUp("REFF", "ATT_SESSIONNO", string.Empty));
                //DataSet dsTimeTable = objCT.DisplayTimeTableFaculty(sessionNo, 0, 0, Convert.ToInt32(Session["userno"]));
                //lvTimeTable.DataSource = dsTimeTable.Tables[0];
                //lvTimeTable.DataBind();
                // (lvTimeTable.FindControl("divTitle") as HtmlGenericControl).InnerHtml = "Time Table for " + dsTimeTable.Tables[0].Rows[0]["SESSIONNAME"].ToString();
            }
            else
            {
                //Show scrolling news
                // this.sessionname();
                //  this.Branchresult();
                NewsController objNC = new NewsController();
                sMarquee = objNC.ScrollingNews(Request.ApplicationPath);
                Notice = objNC.NoticeBoard(Request.ApplicationPath);
                ShowQuickLinks();
                //trNews.Visible = true;
                //trTimeTable.Visible = false;
            }
            this.GetDates();
            this.GetCourse();
        }
        else
        {
            if (Request.Params["__EVENTTARGET"] != null &&
                    Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "loginFB")
                {
                    //CheckTokenFB();
                  //  CheckFBAuthorization();
                }
            }
        }
    }
    [WebMethod]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static string CheckFBAuthorization(string accToken, string id)
    {
        User_AccController objUACC = new User_AccController();
        Common objCommon = new Common();
        //string app_id = "384201936056906";
        //string app_secret = "5a390623ce6b6b3cc32a1bc7baabcb52";
        string result = "";
        //if (Request["code"] == null)
        //{
        //    var redirectUrl = string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}", app_id, Request.Url.AbsoluteUri);
        //    Response.Redirect(redirectUrl);
        //}
        //else
        //{


        //string url = string.Format("https://graph.facebook.com/v8.0/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", app_id, Request.Url.AbsoluteUri, app_secret, Request["code"].ToString());

        //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

        //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //{
        //    StreamReader reader = new StreamReader(response.GetResponseStream());
        //    string vals = reader.ReadToEnd();
        //    au = JsonConvert.DeserializeObject<AccessUser>(vals);
        Page page = (Page)HttpContext.Current.Handler;
        //TextBox TextBox1 = (TextBox)page.FindControl("TextBox1");
        Label lbl = (Label)page.FindControl("lblMsg");

        if (accToken != string.Empty)
        {
            objUACC.UpdateFB_Token(Convert.ToInt32(HttpContext.Current.Session["userno"]), accToken, id, HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString());
            result = "accToken";
            lbl.Text = result;
        }
        else
        {
            result = "accToken is empty";
            lbl.Text = result;
        }
        //if (Convert.ToInt32(objUACC.UpdateFB_Token(Convert.ToInt32(Session["userno"]), accToken, id,Request.ServerVariables["REMOTE_HOST"].ToString())) == Convert.ToInt32(CustomStatus.RecordUpdated))
        //{
        //    result = "";
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Not registered for FB Login. Try again..", this.Page);
        //}
        //}
        //}

        return result;
    }

    private static string GetMyFBJSON(string access_token)
    {
        string url = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name,first_name,last_name,link", access_token);

        WebClient wc = new WebClient();
        Stream data = wc.OpenRead(url);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();

        return s;
    }
    protected void GetFbDetails()
    {
        if (string.IsNullOrEmpty(Request.QueryString["name"]))
        {
            return; //ERROR! No token returned from Facebook!!
        }
        //else
        //{
        //    //objCommon.DisplayMessage("Welcome " + Request.QueryString["name"].ToString(), this.Page);
        //}

        ////let's send an http-request to facebook using the token            
        //string json = GetMyFBJSON(Request.QueryString["access_token"]);

        ////and Deserialize the JSON response
        //JavaScriptSerializer js = new JavaScriptSerializer();
        //MyFB oUser = js.Deserialize<MyFB>(json);
        //if (oUser != null)
        //{
        //    //Response.Write("Welcome, " + oUser.name);
        //    //Response.Write("<br />id, " + oUser.id);
        //    //Response.Write("<br />email, " + oUser.email);
        //    //Response.Write("<br />first_name, " + oUser.first_name);
        //    //Response.Write("<br />last_name, " + oUser.last_name);
        //    //Response.Write("<br />gender, " + oUser.gender);
        //    //Response.Write("<br />link, " + oUser.link);
        //    //Response.Write("<br />updated_time, " + oUser.updated_time);
        //    //Response.Write("<br />birthday, " + oUser.birthday);
        //    //Response.Write("<br />locale, " + oUser.locale);
        //    //Response.Write("<br />picture, " + oUser.picture);
        //    //Response.Write("<br />token, " + Request.QueryString["access_token"].ToString());
        //    //if (oUser.location != null)
        //    //{
        //    //    Response.Write("<br />locationid, " + oUser.location.id);
        //    //    Response.Write("<br />location_name, " + oUser.location.name);
        //    //}
        //}
    }
    private void GetDates()
    {
        DataSet dsDate = objCommon.FillDropDown("ACD_SESSION_MASTER", "SESSION_STDATE", "SESSION_ENDDATE", "SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + "AND ISNULL(FLOCK,0)=1", "");
        if (dsDate != null && dsDate.Tables[0].Rows.Count > 0)
        {
            string startDate = dsDate.Tables[0].Rows[0]["SESSION_STDATE"].ToString();
            string endDate = dsDate.Tables[0].Rows[0]["SESSION_ENDDATE"].ToString();

            ViewState["startDate"] = startDate;
            ViewState["endDate"] = endDate;
        }
    }
    private void GetCourse()
    {
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 2)
            {
                //=========== get all course of login faculty =====================//Convert.ToInt32(Session["currentsession"]),
                DataSet dsCalederCourse = objFetch.GetFacultyTimeTableCourses(Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]));
                ViewState["dsCalederCourse"] = dsCalederCourse;//Insert Data Set in View State
            }
            //=========== get all Holiday from master =====================//
            DataSet dsHDay = objCommon.FillDropDown("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", "ACADEMIC_HOLIDAY_NAME,ACADEMIC_HOLIDAY_STDATE,ACADEMIC_HOLIDAY_ENDDATE", "IS_HOLIDAY_EVENT=1", "HOLIDAY_NO");
            ViewState["dsHDay"] = dsHDay;//Insert Data Set in View State      

            DataSet dsEventDay = objCommon.FillDropDown("ACD_ACADEMIC_HOLIDAY_MASTER", "HOLIDAY_NO", "ACADEMIC_HOLIDAY_NAME,ACADEMIC_HOLIDAY_STDATE,ACADEMIC_HOLIDAY_ENDDATE", "IS_HOLIDAY_EVENT=2", "HOLIDAY_NO");
            ViewState["dsEventDay"] = dsEventDay;//Insert Data Set in View State 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_TIMETABLE_AttendanceEntry.GetCourse() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    /// <summary>
    /// Quick Page Url
    /// </summary>
    public void ShowQuickLinks()
    {
        try
        {
            DataSet ds = objAL.GetUserQLinks(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvQLinks.DataSource = ds;
                lvQLinks.DataBind();
            }
            else
            {
                lvQLinks.DataSource = null;
                lvQLinks.DataBind();
                divQLinks.Visible = false;
                divNews.Attributes.Add("class", "col-md-6");
                divNotices.Attributes.Add("class", "col-md-6");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Qlinks-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    /// <summary>
    /// Access Quick Link.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnLink_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //LinkButton lnk = sender as LinkButton;
    //        //string hdfalurl1 = lnk.CommandArgument;

    //        //string url2 = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + hdfalurl1;

    //        //Response.Write("<script> window.open('" + url2 + "','_blank'); </script>");
    //    }
    //    catch { }
    //  }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        string hex = "#92F303";
        try
        {
            //if (!Page.IsPostBack)
            //{
                string _tdate = DateTime.Now.ToString("dd/MM/yyyy");
                DataSet dsCalederCourse = null;
                DataTable dt = null;
                DataRowCollection drc = null;
                e.Cell.ForeColor = System.Drawing.Color.White;
                if (Convert.ToDateTime(_tdate) == e.Day.Date)
                {
                    e.Cell.CssClass = "today-date";
                }
                if (ViewState["dsCalederCourse"] != null)
                {
                    dsCalederCourse = (DataSet)ViewState["dsCalederCourse"]; //objAttController.GetAllCourses(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));
                    dt = dsCalederCourse.Tables[0];
                    drc = dt.Rows;
                }
                DataSet dsHDay = (DataSet)ViewState["dsHDay"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                DataTable dtHDay = dsHDay.Tables[0];
                DataRowCollection drcHDay = dtHDay.Rows;

                DataSet dsEventDay = (DataSet)ViewState["dsEventDay"]; //objCommon.FillDropDown("ACD_HOLIDAY_MASTER", "HOLIDAY_NO", "HOLIDAY_NAME,HOLIDAY_DATE,LOCK", "", "HOLIDAY_NO");
                DataTable dtEventDay = dsEventDay.Tables[0];
                DataRowCollection drcEventDay = dtEventDay.Rows;

                #region TimeTable
                if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"]) == 2)
                {
                    if (drc.Count > 0)
                    {
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Controls.Clear();
                            e.Cell.Text = string.Empty;
                        }
                        else
                        {
                            LinkButton litCodes = new LinkButton();
                            litCodes.ID = "LinkButton1";
                            litCodes.ForeColor = System.Drawing.Color.White;
                            litCodes.Font.Size = 11;
                            e.Cell.Height = 150;
                            litCodes.Font.Name = "Times New Roman";
                            litCodes.Attributes.Add("href", "javascript:__doPostBack('" + Calendar1.UniqueID + "$" + litCodes.ClientID + "','')");
                            e.Cell.Controls.Add(litCodes);

                            Label litRegularLec = new Label();
                            e.Cell.Controls.Add(litRegularLec);

                            foreach (DataRow dr in drc)
                            {
                                int dayno = Convert.ToInt32(dr["DAYNO"]);
                                //SET DATES WITH SEASHELL COLOR ONLY WHICH ARE SET BETWEEN START AND END DATE IN ATTENDANCE CONFIGURATION IN CURRENT SESSION
                                if (e.Day.Date >= Convert.ToDateTime(ViewState["startDate"]) && e.Day.Date <= Convert.ToDateTime(ViewState["endDate"]))
                                {
                                    if ((int)e.Day.Date.DayOfWeek == dayno)
                                    {
                                        if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                        {
                                            e.Cell.CssClass = "today-date";
                                        }

                                        litCodes.Text += "<br/>" + dr["CCODE"].ToString();
                                        //litroom.Text += "<br/>" + dr["ROOMNAME"].ToString();
                                        //string room =  dr["ROOMNAME"].ToString();
                                        //litroom.Text += "<br/>" + dr["SLOTNAME"].ToString();
                                        //string SLOT = dr["SLOTNAME"].ToString();
                                        //litSlot.ToolTip = SLOT;
                                        //litSlot.ForeColor = System.Drawing.Color.White;
                                        //litroom.ToolTip += room;
                                        //litroom.ForeColor = System.Drawing.Color.White;
                                        string course = dr["COURSE_NAME"].ToString();// + "-" + dr["COURSE_NAME"].ToString() + "\n";
                                        litCodes.ToolTip += course;
                                        litCodes.ForeColor = System.Drawing.Color.Black;
                                       
                                        //litRegularLec.ForeColor = System.Drawing.Color.Black;
                                        //litRegularLec.CssClass = "fa fa-font";

                                    }
                                    else if ((int)e.Day.Date.DayOfWeek == (dayno == 7 ? 0 : dayno))
                                    {
                                        if (Convert.ToDateTime(_tdate) == e.Day.Date)
                                        {
                                            e.Cell.CssClass = "today-date";
                                        }
                                        e.Cell.ForeColor = System.Drawing.Color.White;
                                        litCodes.Text += "<br/>" + dr["CCODE"].ToString();
                                        //litroom.Text += "<br/>" + dr["ROOMNAME"].ToString();
                                        //string room = dr["ROOMNAME"].ToString();
                                        //litroom.ToolTip += room;
                                        //litroom.ForeColor = System.Drawing.Color.White;
                                        string course = dr["COURSE_NAME"].ToString();// +"-" + dr["COURSE_NAME"].ToString() + "\n";
                                        litCodes.ToolTip += course;
                                        litCodes.ForeColor = System.Drawing.Color.White;
                                        //litRegularLec.ToolTip = "Regular Lecture";
                                        //litRegularLec.ForeColor = System.Drawing.Color.Black;
                                        //litRegularLec.CssClass = "fa fa-font";
                                    }

                                }//-------- end Session master date block ------//
                                //litCodes.Text = "<a href='#' data-toggle='modal' data-target='#myModal2'></a>";
                                //<a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">
                            }

                        }
                    }
                }
                #endregion TimeTable
                #region Holiday
                if (drcHDay.Count > 0)
                {
                    if (e.Day.IsOtherMonth)
                    {
                        e.Cell.Controls.Clear();
                        e.Cell.Text = string.Empty;
                    }
                    else
                    {
                        if (Convert.ToDateTime(_tdate) == e.Day.Date)
                        {
                            e.Cell.CssClass = "today-date";
                        }
                        Label litHDay = new Label();
                        e.Cell.Controls.Add(litHDay);

                        for (int i = 0; i <= dsHDay.Tables[0].Rows.Count - 1; i++)
                        {

                            if (((dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_ENDDATE"]) == DBNull.Value))
                            //if (((dsHDay.Tables[0].Rows[i].ItemArray.Contains((dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_ENDDATE"]) == string.Empty))))
                            {
                                if (dsHDay.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                {

                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                                        litHDay.CssClass = "fa fa-calendar-check-o";

                                    }
                                    else
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                            else
                            {
                                if ((e.Day.Date >= Convert.ToDateTime(dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_STDATE"])) && (e.Day.Date <= Convert.ToDateTime(dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_ENDDATE"])))
                                {
                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        // litHDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                    else
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        litHDay.Text += "<br/>";// +dsHDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsHDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litHDay.ToolTip += hdayNM;
                                        litHDay.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0);
                                        litHDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion  Holiday
                #region Events

                if (drcEventDay.Count > 0)
                {
                    if (e.Day.IsOtherMonth)
                    {
                        e.Cell.Controls.Clear();
                        e.Cell.Text = string.Empty;
                    }
                    else
                    {
                        Label litEventDay = new Label();
                        e.Cell.Controls.Add(litEventDay);
                        for (int i = 0; i <= dsEventDay.Tables[0].Rows.Count - 1; i++)
                        {
                            if (((dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_ENDDATE"]) == DBNull.Value))
                            //if (((dsEventDay.Tables[0].Rows[i].ItemArray.Contains((dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_ENDDATE"]) == string.Empty))))
                            {
                                if (dsEventDay.Tables[0].Rows[i].ItemArray.Contains(e.Day.Date))
                                {
                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        e.Cell.BackColor = System.Drawing.Color.White;

                                        litEventDay.Text += "<br/>";// +dsEventDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litEventDay.ToolTip += hdayNM;
                                        litEventDay.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        litEventDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                    else
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        e.Cell.BackColor = System.Drawing.Color.White;

                                        litEventDay.Text += "<br/>";// +dsEventDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litEventDay.ToolTip += hdayNM;
                                        litEventDay.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        litEventDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                            else
                            {
                                if ((e.Day.Date >= Convert.ToDateTime(dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_STDATE"])) && (e.Day.Date <= Convert.ToDateTime(dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_ENDDATE"])))
                                {
                                    if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        e.Cell.BackColor = System.Drawing.Color.White;

                                        litEventDay.Text += "<br/>";// +dsEventDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litEventDay.ToolTip += hdayNM;
                                        // litEventDay.BackColor = System.Drawing.Color.FromArgb(3, 169, 243);
                                        litEventDay.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        litEventDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                    else
                                    {
                                        //e.Cell.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        e.Cell.ForeColor = System.Drawing.Color.Yellow;
                                        e.Cell.BackColor = System.Drawing.Color.White;
                                        //e.Cell.ToolTip = dsEventDay.Tables[0].Rows[i][1].ToString();//for tooltip                              

                                        litEventDay.Text += "<br/>";// +dsEventDay.Tables[0].Rows[i]["HOLIDAY_NAME"].ToString();
                                        string hdayNM = dsEventDay.Tables[0].Rows[i]["ACADEMIC_HOLIDAY_NAME"].ToString() + "\n";
                                        litEventDay.ToolTip += hdayNM;
                                        litEventDay.ForeColor = System.Drawing.ColorTranslator.FromHtml(hex);
                                        litEventDay.CssClass = "fa fa-calendar-check-o";
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion  Restricted Holiday(Partial Allow Att.)
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_TIMETABLE_AttendanceEntry.Calendar1_DayRender --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        int day=0;
        DateTime DaySelected = Convert.ToDateTime(Calendar1.SelectedDate);
        switch (DaySelected.DayOfWeek.ToString())
        {
            case "Monday":
                day = 1;
                break;
            case "Tuesday":
                day = 2;
                break;
            case "Wednesday":
                day = 3;
                break;
            case "Thursday":
                day = 4;
                break;
            case "Friday":
                day = 5;
                break;
            case "Saturday":
                day = 6;
                break;
            case "Sunday":
                day = 7;
                break;
        }
        DataSet dsShowCourse = objFetch.GetCoursesForAttendanceFromHomePage(Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]), day);
        if (dsShowCourse.Tables[0].Rows.Count > 0)
        {
            lstCourseList.DataSource = dsShowCourse;
            lstCourseList.DataBind();
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#myModal2').modal('show')", true);
            mpe.Show();
        }
    }
    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
     
        LinkButton lnk = sender as LinkButton;
        HiddenField hdnCoursename   = lnk.NamingContainer.FindControl("hdnCoursename") as HiddenField;
        HiddenField hdnSchemename   = lnk.NamingContainer.FindControl("hdnSchemename") as HiddenField;
        HiddenField hdnSectionname  = lnk.NamingContainer.FindControl("hdnSectionname") as HiddenField;
        HiddenField hdnSubjecttype  = lnk.NamingContainer.FindControl("hdnSubjecttype") as HiddenField;
        HiddenField hdnBatch        = lnk.NamingContainer.FindControl("hdnBatch") as HiddenField;
        HiddenField hdnCourseno = lnk.NamingContainer.FindControl("hdnCourseno") as HiddenField;
        HiddenField hdnSectionno = lnk.NamingContainer.FindControl("hdnSectionno") as HiddenField;
        HiddenField hdnBatchno = lnk.NamingContainer.FindControl("hdnBatchno") as HiddenField;
        HiddenField hdnSubId = lnk.NamingContainer.FindControl("hdnSubId") as HiddenField;


        string[] arr = new string[] { hdnCoursename.Value,hdnSchemename.Value,hdnSectionname.Value,hdnSubjecttype.Value,
            hdnBatch.Value,hdnCourseno.Value,hdnSectionno.Value,hdnBatchno.Value,hdnSubId.Value};

        Session["arr"] = arr;


        //ArrayList arr = new ArrayList();
        //arr.Add(hdnCoursename.Value);
        //arr.Add(hdnSchemename.Value);
        //arr.Add(hdnSectionname.Value);
        //arr.Add(hdnSubjecttype.Value);
        //arr.Add(hdnBatch.Value);

        //string arry = String.Join(",", ((string[])arr.ToArray(typeof(String))));

        if (Convert.ToInt32(Session["usertype"]) == 3)
        {
            //string pageurl = "Academic/AttendenceByFaculty.aspx?pageno=112&coursename=" + hdnCoursename.Value + "&schemename=" + hdnSchemename.Value
            //    + "&sectionname=" + hdnSectionname.Value + "&subjecttype=" + hdnSubjecttype.Value + "&batch=" + hdnBatch.Value
            //     + "&courseno=" + hdnCourseno.Value + "&sectionno=" + hdnSectionno.Value + "&batchno=" + hdnBatchno.Value
            //      + "&subid=" + hdnSubId.Value;
            string pageurl = "Academic/AttendenceByFaculty.aspx?pageno=112";
            ScriptManager.RegisterStartupScript(this,this.GetType(), "OpenWindow", "window.open('" + pageurl + "','_newtab');", true);
        }
    }
    public string GetCourseName(object coursename, object schemename, object sectionname, object SUBJECTTYPE, object Batchname)
    {
        return coursename.ToString() + " [<span style='color:Green'>" + schemename.ToString() + "</span>]" + " (<b><span style='color:Red'>Section : " + sectionname.ToString() + "</span>) " + " (<b><span style='color:Red'>" + SUBJECTTYPE.ToString() + "</span></b>) " + " (<b><span style='color:Blue'> Batch : " + Batchname.ToString() + "</span></b>)";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string DateSelected = "";
        DateSelected = Calendar1.Caption;
        DataSet dsShowCourse = objFetch.GetCoursesForAttendanceFromHomePage(Convert.ToInt32(Session["currentsession"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["usertype"]), 1);
        if (dsShowCourse.Tables[0].Rows.Count > 0)
        {
            lstCourseList.DataSource = dsShowCourse;
            lstCourseList.DataBind();
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#myModal2').modal('show')", true);
            mpe.Show();
        }
    }

}
