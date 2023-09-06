using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.UI.HtmlControls;


public partial class SiteMasterPage : System.Web.UI.MasterPage
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    private string uaims_constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    private string rfcconstr = ConfigurationManager.ConnectionStrings["UAIMS_RFCCONFIG"].ConnectionString; 
    bool connection;
    private static string isServer = System.Configuration.ConfigurationManager.AppSettings["isServer"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //if (Session["Old_Url"] != null)
            //{

            //    string Old_Url = Session["Old_Url"].ToString();
            //    string New_Url = Request.Url.ToString();

            //    if (Old_Url != New_Url)
            //    {
            //        Session.Remove("stuinfoidno");
            //    }
            //}
            //**START CHECK FOR MAINTENANCE ADDED BY SHAHBAZ AHMAD **//

            DataSet ds = objCommon.FillDropDown("REFF", "isnull(MAINTENANCE,1)MAINTENANCE", "MAINTENANCE_STIME,isnull(ALERT_FREQ,900000)ALERT_FREQ", "", "");
            string chk = string.Empty;
            if (ds.Tables[0].Rows.Count > 0)
            {
                chk = ds.Tables[0].Rows[0]["MAINTENANCE"].ToString();

                if (ds.Tables[0].Rows[0]["MAINTENANCE_STIME"] != DBNull.Value)
                {
                    DateTime Stime = Convert.ToDateTime(ds.Tables[0].Rows[0]["MAINTENANCE_STIME"].ToString());
                    hdfMaintenanceTime.Value = Convert.ToString(Stime);
                    Session["miantenanceSTime"] = Convert.ToString(Stime);
                }
                else
                {
                    // handle the case where the "DATE" column is null
                    hdfMaintenanceTime.Value = "null";
                    Session["miantenanceSTime"] = "null";
                }
                Session["maintenanceFlag"] = chk;
                hdfMaintenanceFlag.Value = chk;
                hdfAlerFreq.Value = ds.Tables[0].Rows[0]["ALERT_FREQ"].ToString();

            }
            //**END CHECK FOR MAINTENANCE **//

            #region Check Internet Connection Added Mahesh on Dated 09-09-2020
            Page.ClientScript.RegisterStartupScript(this.GetType(), "NetConnectionAlert", "CheckNetConnection();", true);
            #endregion Check Internet Connection Added Mahesh on Dated 09-09-2020

            string userid = objCommon.LookUp("LogFile", "ua_name", "ua_name='" + Session["username"].ToString() + "'");
            BindQuickLinks(userid);
            //Very Important
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Page.Header.DataBind();
            ////Add Google Translation Code only in Internet is Connected..
            //==============================================================
            //if (InternetCS.IsConnectedToInternet() == true)
            //{
            //    System.Text.StringBuilder divHtml = new System.Text.StringBuilder();
            //    divHtml.Append("<div id='google_translate_element'></div>");
            //    divHtml.Append("<script language='javascript' type='text/javascript'>");
            //    divHtml.Append("function googleTranslateElementInit() {");
            //    divHtml.Append("new google.translate.TranslateElement({pageLanguage: 'en',layout: google.translate.TranslateElement.InlineLayout.SIMPLE}, 'google_translate_element'); }");
            //    divHtml.Append("</script>");
            //    divHtml.Append("<script language='javascript' type='text/javascript' src='//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit'></script>");
            //    divGoogleTrans.InnerHtml = divHtml.ToString();
            //}

            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;
            Context.Request.Browser.Adapters.Clear();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Start", "<script> javascript:function AppValueSet() {var c= 'Tab'; return c;} </script>");
            btnLogout.Attributes.Add("OnClick", "return confirm('Are You Sure To Logout?')");
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                lblLink.Text = textInfo.ToTitleCase(Session["userfullname"].ToString());

                if (Convert.ToInt32(Session["usertype"]) == 2 || Convert.ToInt32(Session["usertype"]) == 1)
                {
                    pdes.Visible = false;
                    //lblDesignation.Text = "Student";
                }
                else
                {
                    pdes.Visible = true;
                    lblDesignation.Text = (Session["UA_DESIG"].ToString()).ToUpper();
                }

                //Bind user photo
                BindImage();

                //for displaying path
                //SRIKANTH P 24-09-2019

                if (Request.QueryString["pageno"] != null)
                {

                    lblpath.Text = objCommon.GetPath(int.Parse(Request.QueryString["pageno"].ToString()));
                    lblBrade.Text = objCommon.GetPathSubMenu(int.Parse(Request.QueryString["pageno"].ToString()));
                    //getPath(int.Parse(Request.QueryString["pageno"].ToString()));
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); // This method is user for getting page title on every page.
                }
                if (Session["payment"].ToString().Equals("payment"))
                {
                    string pageno = objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_LINK='Exam Registration'");
                    lblpath.Text = objCommon.GetPath(int.Parse(pageno));
                    lblBrade.Text = objCommon.GetPathSubMenu(int.Parse(pageno));
                }


                //if (Convert.ToInt32(Session["usertype"]) == 2)
                //{
                //    if (Session["idno"] != null)
                //    {
                //        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=STUDENT";
                //        imgDash.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=STUDENT";
                //    }
                //}
                //else if (Convert.ToInt32(Session["usertype"]) == 3 || Convert.ToInt32(Session["usertype"])== 5)
                //{
                //    if (Session["idno"] != null)
                //    {
                //        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=EMP";
                //        imgDash.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=EMP";
                //    }

                //}
                //else
                //{
                //    imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
                //    imgDash.ImageUrl = "~/IMAGES/nophoto.jpg";
                //}
                //Page title
                //Page.Title = Session["coll_name"].ToString();
                // lblColName.Text = Session["coll_name"].ToString();

                //********************
                Page.Title = Session["coll_name"].ToString();
                lnklogo.Href = "~/showimage.aspx?id=0&type=college";
                //Img1.Src = "~/showimage.aspx?id=0&type=college";
                Img1.ImageUrl = "~/showimage.aspx?id=0&type=college";
                Session["Reset"] = true;
                //Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                //********************

                //ADDED TODO LIST BY Deepali ON 24082020
                string userno_td = Convert.ToString(Session["userno"]);
                BindToDolist(userno_td);

                GetFbDetails();
                try
                {
                    if (Convert.ToInt32(Session["usertype"]) == 2 && Convert.ToString(Session["firstlog"]) == "False")
                    {
                        Fill_menu_stud(mainMenu);
                    }
                    else
                    {
                        Fill_menu(mainMenu);
                        
                        Fill_menu1(mainMenu1);// Added By Swapnil Prachand - 13112021
                    }
                    /// Added by Pritish S. for menu tab on 09/04/2021
                    /// tab patch start

                    string path = Request.Url.AbsoluteUri;
                    if (path.Contains("Home") == false)
                    {
                        if (path.Contains("MastNo") == true)
                        {
                            int alno = 0;
                            int uano = 0;
                            int mastno = 0;

                            if (Request.QueryString["PgNo"] != null)
                            {
                                alno = Convert.ToInt32(Request.QueryString["PgNo"].ToString());
                                Session["alno"] = alno;
                            }
                            else if (Session["alno"] != null)
                            {
                                alno = Convert.ToInt32(Session["alno"].ToString());
                            }

                            if (Request.QueryString["UaNo"] != null)
                            {
                                uano = Convert.ToInt32(Request.QueryString["UaNo"].ToString());
                                Session["uano"] = uano;
                            }
                            else if (Session["uano"] != null)
                            {
                                uano = Convert.ToInt32(Session["uano"].ToString());
                            }

                            if (Request.QueryString["MastNo"] != null)
                            {
                                mastno = Convert.ToInt32(Request.QueryString["MastNo"].ToString());
                                Session["masterno"] = mastno;
                            }
                            else if (Session["masterno"] != null)
                            {
                                mastno = Convert.ToInt32(Session["masterno"].ToString());
                            }

                            BindLinks(uano, alno, mastno);
                        }
                        else
                        {
                            BindDataSetLinks();
                        }

                    }
                    else
                    {
                        divLinks.Visible = false;
                    }

                    if (path.Contains("/Links.aspx") == true)
                    {
                        divBread.Visible = false;
                    }

                    /// Tab patch end


                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        liLinks.Visible = true;
                        linkFaculty.Visible = false;
                        linkStudent.Visible = true;
                    }
                    else if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        liLinks.Visible = true;
                        linkFaculty.Visible = true;
                        linkStudent.Visible = false;
                    }
                    else
                    {
                        liLinks.Visible = false;
                        linkFaculty.Visible = false;
                        linkStudent.Visible = false;
                    }
                    //// Disable ExpandDepth if the TreeView's expand/collapse
                    //// state is stored in session.
                    //if (Session["TreeViewState"] != null)
                    //    //mainMenu.ExpandDepth = -1;

                    //if (Session["TreeViewState"] == null)
                    //{
                    //    // Record the TreeView's current expand/collapse state.
                    //    //List<string> list = new List<string>;
                    //    List<string> list = new List<string>(mainMenu.Items.Count);
                    //    //SaveTreeViewState(mainMenu.Items, list);
                    //    Session["TreeViewState"] = list;
                    //}
                    //else
                    //{
                    //    // Apply the recorded expand/collapse state to the TreeView.
                    //    List<string> list = (List<string>)Session["TreeViewState"];
                    //    //RestoreTreeViewState(mainMenu.Items, list);
                    //}
                    //mainMenu.RenderingCompatibility = new Version(3, 5);

                    objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 17/01/2021
                    objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 17/01/2021
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }

                ShowQuickLinks();
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
        
    }
    public class Packet
    {
        public string res { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }
    //[WebMethod]
    ////[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    //public string Check_FBDetails(string accToken, string id)
    //{
    //    string HtmlData = CheckFBAuthorization(accToken, id);
    //    var pkg = new Packet
    //    {
    //        res = "1",
    //        msg = "Success",
    //        data = HtmlData
    //    };
    //    return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    //}
    //[WebMethod]
    ////[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    //public static string CheckFBAuthorization(string accToken, string id)
    //{
    //    User_AccController objUACC = new User_AccController();
    //    Common objCommon = new Common();
    //    //string app_id = "384201936056906";
    //    //string app_secret = "5a390623ce6b6b3cc32a1bc7baabcb52";
    //    string result = "";
    //    //if (Request["code"] == null)
    //    //{
    //    //    var redirectUrl = string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}", app_id, Request.Url.AbsoluteUri);
    //    //    Response.Redirect(redirectUrl);
    //    //}
    //    //else
    //    //{


    //    //string url = string.Format("https://graph.facebook.com/v8.0/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", app_id, Request.Url.AbsoluteUri, app_secret, Request["code"].ToString());

    //    //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

    //    //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
    //    //{
    //    //    StreamReader reader = new StreamReader(response.GetResponseStream());
    //    //    string vals = reader.ReadToEnd();
    //    //    au = JsonConvert.DeserializeObject<AccessUser>(vals);
    //    Page page = (Page)HttpContext.Current.Handler;
    //    //TextBox TextBox1 = (TextBox)page.FindControl("TextBox1");
    //    Label lbl = (Label)page.FindControl("lblMsg");

    //    if (accToken != string.Empty)
    //    {
    //        objUACC.UpdateFB_Token(Convert.ToInt32(HttpContext.Current.Session["userno"]), accToken, id, HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString());
    //        result = "accToken";
    //        lbl.Text = result;
    //    }
    //    else
    //    {
    //        result = "accToken is empty";
    //        lbl.Text = result;
    //    }
    //    //if (Convert.ToInt32(objUACC.UpdateFB_Token(Convert.ToInt32(Session["userno"]), accToken, id,Request.ServerVariables["REMOTE_HOST"].ToString())) == Convert.ToInt32(CustomStatus.RecordUpdated))
    //    //{
    //    //    result = "";
    //    //}
    //    //else
    //    //{
    //    //    objCommon.DisplayMessage("Not registered for FB Login. Try again..", this.Page);
    //    //}
    //    //}
    //    //}

    //    return result;
    //}
    protected void GetFbDetails()
    {
        string authID = "";
        string accT = "";
        if (authName.Value != string.Empty || accessT.Value != string.Empty)
        {
            authID = authName.Value;
            accT = accessT.Value;
        }
        if (string.IsNullOrEmpty(Request.QueryString["name"]))
        {
            return; //ERROR! No token returned from Facebook!!
        }
        //else
        //{
        //    objCommon.DisplayMessage("Welcome " + Request.QueryString["name"].ToString(), this.Page);
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
    public void CheckPhotoExistOrNot(string Type)
    {
        try
        {
            DataTableReader dtr = null;
            SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@P_Idno", Convert.ToInt32(Session["IDNO"].ToString()));
            objParams[1] = new SqlParameter("@P_TYPE", Type);

            dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDEMP_SP_RET_PHOTO", objParams).Tables[0].CreateDataReader();
            //byte[] imgData = null;
            if (dtr.Read())
            {
                if (dtr["PHOTO"].ToString() == "")
                {
                    imgPhoto.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                    imgDash.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                    noimg.Visible = true;
                    noimghead.Visible = true;
                    imgPhoto.Visible = false;
                    imgDash.Visible = false;
                }
                else
                {
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=" + Type;
                    imgDash.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=" + Type;
                    imgPhoto.Visible = true;
                    imgDash.Visible = true;
                    noimg.Visible = false;
                    noimghead.Visible = false;
                }
            }
            else
            {
                imgPhoto.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                imgDash.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                noimg.Visible = true;
                noimghead.Visible = true;
                imgPhoto.Visible = false;
                imgDash.Visible = false;
            }
        }
        catch { }

    }

    public void ShowQuickLinks()
    {
        Access_LinkController objAL = new Access_LinkController();
        string yourHTMLstring = @"
                                    <input type='text' class='live-search-box-1' placeholder='Search Here' />
                                        <ul class='list-group live-search'>
                                ";

        try
        {
            int HeadCount = 0;
            string HeadName = "";
            string LinkName = "";
            string Href = "";
            DataSet ds = objAL.GetUserQLinks(Convert.ToInt32(Session["userno"]));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["LINK_HEADER"].ToString() == HeadName || i == 0)
                {
                    HeadName = ds.Tables[0].Rows[i]["LINK_HEADER"].ToString();
                    LinkName = ds.Tables[0].Rows[i]["AL_LINK"].ToString();
                    Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + ds.Tables[0].Rows[i]["AL_URL1"].ToString();

                    if (HeadCount == 0)
                    {
                        yourHTMLstring += @"<li class='list-group-item'><span><a href='#'>" + HeadName + "</a></span><ul>";
                        HeadCount++;
                    }
                    yourHTMLstring += @" <li>
                                             <img src='" + ResolveUrl(String.Format(@"~/IMAGES/{0}", "")) + "' alt='image' /><a href='" + Href + "'>" + LinkName + "";
                    yourHTMLstring += @" </a></li>";
                }
                else
                {
                    yourHTMLstring += @"
                                            </ul>
                                            </li>
                                        ";

                    HeadName = ds.Tables[0].Rows[i]["LINK_HEADER"].ToString();
                    LinkName = ds.Tables[0].Rows[i]["AL_LINK"].ToString();
                    Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + ds.Tables[0].Rows[i]["AL_URL1"].ToString();

                    yourHTMLstring += @"<li class='list-group-item'><span><a href='#'>" + HeadName + "</a></span><ul>";

                    yourHTMLstring += @" <li>
                                             <img src='" + ResolveUrl(String.Format(@"~/IMAGES/{0}", "")) + "' alt='image' /><a href='" + Href + "'>" + LinkName + "";
                    yourHTMLstring += @" </a></li>";
                }

                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    yourHTMLstring += @"
                                            </ul>
                                        ";
                }
            }

            divFavorite.Controls.Add(new LiteralControl(yourHTMLstring));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Qlinks-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void Fill_menu_stud(Menu tvLinks)
    {
        SQLHelper objSH = new SQLHelper(uaims_constr);
        SqlDataReader drLinks = null;
        MenuItem xx = null;
        try
        {
            //Log Out for All
            xx = new MenuItem();
            xx.Text = "LOGOUT";
            xx.Target = "_parent";
            xx.NavigateUrl = "logout.aspx";
            tvLinks.Items.Add(xx);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            //close all objects
            if (drLinks != null) drLinks.Close();
        }
    }

    ///Added By PRiTiSH on 03/11/2020 TAB PATCH
    public void Fill_menu1(Menu tvLinks)
    {

        SQLHelper objSH = new SQLHelper(uaims_constr);
        SQLHelper objconfig = new SQLHelper(rfcconstr);

        SqlDataReader drLinks = null;
        int linkno = 0;
        int mastno = 0;
        int url_idno = 0;
        MenuItem xx = null;
        MenuItem yy = null;
        MenuItem zz = null;
        MenuItem aa = null;

        try
        {
            int userno = int.Parse(Session["userno"].ToString());
            //Get all user links
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_UA_NO", userno);
            SqlParameter[] objParamsnew = new SqlParameter[0];
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string pageno = string.Empty;
            Boolean ChkMenu_Url = true;
            Boolean findPage = false;

            if (url.ToUpper().Contains("?PAGENO"))
            {
                pageno = Request.QueryString["pageno"].ToString().Trim();
            }

            //Get all user links
            if (Session["username"].ToString() == "superadmin")
            {

                drLinks = objconfig.ExecuteReaderSP("SP_GET_MENU", objParamsnew);
            }
            else
            {
                drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_RET_USERLINKS", objParams);
            }

            while (drLinks.Read())
            {


                if (ChkMenu_Url == true)
                {
                    if (drLinks["al_no"].ToString().Trim() == pageno)
                    {
                        findPage = true;

                        if (drLinks["al_url"].ToString().Trim() == string.Empty)
                        {
                            Response.Redirect("~/notauthorized.aspx?page= this");
                        }
                        else
                        {
                            ChkMenu_Url = url.ToUpper().Contains(drLinks["al_url"].ToString().Trim().ToUpper());

                            if (ChkMenu_Url == false)
                            {
                                Response.Redirect("~/notauthorized.aspx?page= this");
                            }
                        }
                    }
                }



                if (drLinks["al_link"].ToString().Trim() != string.Empty)
                {
                    if (drLinks["url_idno"] != null & drLinks["url_idno"].ToString() != string.Empty)
                        url_idno = int.Parse(drLinks["url_idno"].ToString());
                    if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty & int.Parse(drLinks["al_asno"].ToString()) != linkno)
                    {
                        int l = int.Parse(drLinks["levelno"].ToString());
                        xx = new MenuItem();  // this is defination of the node.
                        xx.Text = drLinks["as_title"].ToString();
                        xx.Value = drLinks["al_asno"].ToString();
                        xx.NavigateUrl = "";
                        tvLinks.Items.Add(xx);


                        if (drLinks["al_url"].ToString().Trim() == string.Empty)
                        {
                            zz = new MenuItem();    // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.NavigateUrl = "";
                            mastno = int.Parse(drLinks["mastno"].ToString());
                            xx.ChildItems.Add(zz);
                        }
                        else
                        {
                            yy = new MenuItem();

                            if (drLinks["SHORTCUT_KEY"].ToString() != "-")
                            {
                                yy.Text = "" + drLinks["al_link"].ToString() + "<span class='sht-key'>" + drLinks["SHORTCUT_KEY"].ToString() + "</span>";
                            }
                            else
                            {
                                yy.Text = "" + drLinks["al_link"].ToString();
                            }

                            //yy.Text = drLinks["al_link"].ToString();
                            if (drLinks["summer"] != null & drLinks["summer"].ToString() != string.Empty)
                            {
                                if (int.Parse(drLinks["summer"].ToString()) == 1)
                                    yy.NavigateUrl = drLinks["al_url"].ToString() + "?idno=2";
                                else
                                {
                                    string qry = string.Empty;
                                    if (drLinks["al_url"].ToString().Contains("?"))
                                        qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                    else
                                        qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                    if (url_idno > 0)
                                        qry += "&idno=" + url_idno.ToString();
                                    else
                                        qry += "&idno=-1";
                                    yy.NavigateUrl = qry;
                                }
                            }
                            else
                            {
                                string qry = string.Empty;
                                if (drLinks["al_url"].ToString().Contains("?"))
                                    qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                else
                                    qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                if (url_idno > 0)
                                    qry += "&idno=" + url_idno.ToString();
                                else
                                    qry += "&idno=-1";
                                yy.NavigateUrl = qry;
                            }
                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    zz.ChildItems.Add(yy);
                                }
                                else
                                {
                                    xx.ChildItems.Add(yy);
                                }
                            }
                            else
                            {
                                xx.ChildItems.Add(yy);
                            }
                        }
                    }
                    else
                    {
                        if (drLinks["al_url"].ToString() == string.Empty)
                        {
                            int l = int.Parse(drLinks["levelno"].ToString());
                            if (int.Parse(drLinks["levelno"].ToString()) == 2)
                            {
                                aa = new MenuItem();
                                aa.Text = drLinks["al_link"].ToString();
                                aa.NavigateUrl = "";
                                zz.ChildItems.Add(aa);
                            }
                            else
                            {
                                zz = new MenuItem();   // this is defination of the node.
                                zz.Text = drLinks["al_link"].ToString();
                                zz.NavigateUrl = "";
                                xx.ChildItems.Add(zz);
                            }
                            if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
                                mastno = int.Parse(drLinks["mastno"].ToString());
                        }
                        else
                        {
                            int l = int.Parse(drLinks["levelno"].ToString());
                            yy = new MenuItem();

                            // pRITISH
                            if (drLinks["SHORTCUT_KEY"].ToString() != "-")
                            {
                                yy.Text = "" + drLinks["al_link"].ToString() + "<span class='sht-key'>" + drLinks["SHORTCUT_KEY"].ToString() + "</span>";
                            }
                            else
                            {
                                yy.Text = "" + drLinks["al_link"].ToString();
                            }

                            //yy.Text = drLinks["al_link"].ToString();
                            if (drLinks["summer"] != null & drLinks["summer"].ToString() != string.Empty)
                            {
                                if (int.Parse(drLinks["summer"].ToString()) == 1)
                                {
                                    yy.NavigateUrl = drLinks["al_url"].ToString() + "?idno=2";
                                }
                                else
                                {
                                    string qry = string.Empty;
                                    if (drLinks["al_url"].ToString().Contains("?"))
                                        qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                    else
                                        qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                    if (url_idno > 0)
                                        qry += "&idno=" + url_idno.ToString();
                                    else
                                        qry += "&idno=-1";
                                    yy.NavigateUrl = qry;
                                }
                            }
                            else
                            {
                                string qry = string.Empty;
                                if (drLinks["al_url"].ToString().Contains("?"))
                                    qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                else
                                    qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                yy.NavigateUrl = qry;
                            }

                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    if (int.Parse(drLinks["levelno"].ToString()) == 3)
                                    {
                                        aa.ChildItems.Add(yy);
                                    }
                                    else
                                    {
                                        zz.ChildItems.Add(yy);
                                    }
                                }
                                else
                                {
                                    if (int.Parse(drLinks["levelno"].ToString()) == 3)
                                    {
                                        aa.ChildItems.Add(yy);
                                    }
                                    else if (int.Parse(drLinks["levelno"].ToString()) == 2)
                                    {
                                        zz.ChildItems.Add(yy);
                                    }
                                    else
                                    {
                                        xx.ChildItems.Add(yy);
                                    }
                                }
                            }
                            else
                            {
                                xx.ChildItems.Add(yy);
                            }
                        }
                    }
                }
                if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty)
                    linkno = int.Parse(drLinks["al_asno"].ToString());
                if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
                    mastno = int.Parse(drLinks["mastno"].ToString());

                if (int.Parse(drLinks["levelno"].ToString()) == 3)
                {

                }
                else if (int.Parse(drLinks["levelno"].ToString()) == 2)
                {

                }
            }
            if (pageno != string.Empty)
            {
                if (findPage == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page= this");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            if (drLinks != null) drLinks.Close();
        }
    }

    public void Fill_menu(Menu tvLinks)
    {
        SQLHelper objSH = new SQLHelper(uaims_constr);
        SQLHelper objconfig = new SQLHelper(rfcconstr);
        SqlDataReader drLinks = null;
        int linkno = 0;
        int mastno = 0;
        int url_idno = 0;
        MenuItem xx = null;
        MenuItem yy = null;
        MenuItem zz = null;
        MenuItem aa = null;

        try
        {
            int userno = int.Parse(Session["userno"].ToString());
            //Get all user links
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_UA_NO", userno);

            SqlParameter[] objParamsnew = new SqlParameter[0];

            ///Added By Shrikant 11122017
            ///**Start
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string pageno = string.Empty;
            Boolean ChkMenu_Url = true;
            Boolean findPage = false;

            if (url.ToUpper().Contains("?PAGENO"))
            {
                pageno = Request.QueryString["pageno"].ToString().Trim();
            }
            ///**End

            //Get all user links
            if (Session["username"].ToString() == "superadmin")
            {
                drLinks = objconfig.ExecuteReaderSP("SP_GET_MENU", objParamsnew);
            }
            else
            {
                drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_RET_USERLINKS", objParams);

                DataSet dsLink = (DataSet)Session["dsLink"] as DataSet;

                if (dsLink == null || dsLink.Tables[0].Rows.Count == 0)
                {
                    DataSet ds = null;
                    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                    SqlParameter[] objParams1 = null;
                    objParams1 = new SqlParameter[1];
                    objParams1[0] = new SqlParameter("@P_UA_NO", userno);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_TREEVIEW_SP_RET_USERLINKS", objParams1);

                    Session["dsLinkSearch"] = ds;
                    Session["dsLink"] = ds;
                }
            }

            //comment below patch and add that patch in above else part
            //DataSet dsLink = (DataSet)Session["dsLink"] as DataSet;

            //if (dsLink == null || dsLink.Tables[0].Rows.Count == 0)
            //{
            //    DataSet ds = null;
            //    SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
            //    SqlParameter[] objParams1 = null;
            //    objParams1 = new SqlParameter[1];
            //    objParams1[0] = new SqlParameter("@P_UA_NO", userno);

            //    ds = objSQLHelper.ExecuteDataSetSP("PKG_TREEVIEW_SP_RET_USERLINKS", objParams1);

            //    Session["dsLinkSearch"] = ds;
            //    Session["dsLink"] = ds;
            //}


            while (drLinks.Read())
            {
                if (ChkMenu_Url == true)
                {
                    if (drLinks["al_no"].ToString().Trim() == pageno)
                    {
                        findPage = true;

                        if (drLinks["al_url"].ToString().Trim() == string.Empty)
                        {
                            Response.Redirect("~/notauthorized.aspx?page= this");
                        }
                        else
                        {
                            ChkMenu_Url = url.ToUpper().Contains(drLinks["al_url"].ToString().Trim().ToUpper());

                            if (ChkMenu_Url == false)
                            {
                                Response.Redirect("~/notauthorized.aspx?page= this");
                            }
                        }
                    }
                }


                if (drLinks["al_link"].ToString().Trim() != string.Empty)
                {
                    if (drLinks["url_idno"] != null & drLinks["url_idno"].ToString() != string.Empty)
                        url_idno = int.Parse(drLinks["url_idno"].ToString());
                    if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty & int.Parse(drLinks["al_asno"].ToString()) != linkno)
                    {

                        xx = new MenuItem();  // this is defination of the node.
                        xx.Text = drLinks["as_title"].ToString();
                        xx.Value = drLinks["al_asno"].ToString();
                        xx.NavigateUrl = "";
                        tvLinks.Items.Add(xx);


                        if (drLinks["al_url"].ToString().Trim() == string.Empty)
                        {
                            int l = int.Parse(drLinks["levelno"].ToString());
                            zz = new MenuItem();    // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.NavigateUrl = "";

                            mastno = int.Parse(drLinks["mastno"].ToString());

                            if (drLinks["al_url"].ToString() == string.Empty || drLinks["al_url"].ToString() == "")
                            {
                                string qry = string.Empty;
                                qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                zz.NavigateUrl = "Links.aspx" + "?PgNo=" + drLinks["al_no"].ToString() + "&UaNo=" + Session["userno"].ToString() + "&MastNo=" + drLinks["mastno"].ToString() + "";
                            }
                            xx.ChildItems.Add(zz);
                        }
                        else
                        {
                            yy = new MenuItem();

                            if (drLinks["SHORTCUT_KEY"].ToString() != "-")
                            {
                                yy.Text = "" + drLinks["al_link"].ToString() + "<span class='sht-key'>" + drLinks["SHORTCUT_KEY"].ToString() + "</span>";
                            }
                            else
                            {
                                yy.Text = "" + drLinks["al_link"].ToString();
                            }

                            //yy.Text = drLinks["al_link"].ToString();

                            if (drLinks["summer"] != null & drLinks["summer"].ToString() != string.Empty)
                            {
                                if (int.Parse(drLinks["summer"].ToString()) == 1)
                                    yy.NavigateUrl = drLinks["al_url"].ToString() + "?idno=2";
                                else
                                {
                                    string qry = string.Empty;
                                    if (drLinks["al_url"].ToString().Contains("?"))
                                        qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                    else
                                        qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                    if (url_idno > 0)
                                        qry += "&idno=" + url_idno.ToString();
                                    else
                                        qry += "&idno=-1";
                                    yy.NavigateUrl = qry;
                                }
                            }
                            else
                            {
                                string qry = string.Empty;
                                if (drLinks["al_url"].ToString().Contains("?"))
                                    qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                else
                                    qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                if (url_idno > 0)
                                    qry += "&idno=" + url_idno.ToString();
                                else
                                    qry += "&idno=-1";
                                yy.NavigateUrl = qry;
                            }
                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    zz.ChildItems.Add(yy);
                                }
                                else
                                {
                                    xx.ChildItems.Add(yy);
                                }
                            }
                            else
                            {
                                xx.ChildItems.Add(yy);
                            }
                        }
                    }
                    else
                    {
                        if (drLinks["al_url"].ToString() == string.Empty)
                        {
                            if (int.Parse(drLinks["levelno"].ToString()) == 2)
                            {
                                int l = int.Parse(drLinks["levelno"].ToString());
                                aa = new MenuItem();
                                aa.Text = drLinks["al_link"].ToString();
                                // aa.NavigateUrl = "";

                                if (drLinks["al_url"].ToString() == string.Empty || drLinks["al_url"].ToString() == "")
                                {
                                    string qry = string.Empty;
                                    qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                    aa.NavigateUrl = "Links.aspx" + "?PgNo=" + drLinks["al_no"].ToString() + "&UaNo=" + Session["userno"].ToString() + "&MastNo=" + drLinks["mastno"].ToString() + "";
                                    //aa.NavigateUrl = "Links.aspx" + "?pgno=" + drLinks["al_no"].ToString();
                                    Session["MastNo"] = drLinks["MASTNO"].ToString();
                                }

                                zz.ChildItems.Add(aa);
                            }
                            else
                            {
                                zz = new MenuItem();   // this is defination of the node.
                                zz.Text = drLinks["al_link"].ToString();
                                //zz.NavigateUrl = "";
                                zz.NavigateUrl = "Links.aspx" + "?PgNo=" + drLinks["al_no"].ToString() + "&UaNo=" + Session["userno"].ToString() + "&MastNo=" + drLinks["mastno"].ToString() + "";
                                // zz.NavigateUrl = "Links.aspx" + "?pgno=" + drLinks["al_no"].ToString();
                                xx.ChildItems.Add(zz);
                            }
                            if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
                                mastno = int.Parse(drLinks["mastno"].ToString());
                        }
                        else
                        {
                            int l = int.Parse(drLinks["levelno"].ToString());           //ADDED BY PRITISH S. ON 31/10/2020     START
                            if (l == 1)                                                 //ADDED BY PRITISH S. ON 31/10/2020     END
                            {
                                yy = new MenuItem();

                                // pRITISH
                                if (drLinks["SHORTCUT_KEY"].ToString() != "-")
                                {
                                    yy.Text = "" + drLinks["al_link"].ToString() + "<span class='sht-key'>" + drLinks["SHORTCUT_KEY"].ToString() + "</span>";
                                }
                                else
                                {
                                    yy.Text = "" + drLinks["al_link"].ToString();
                                }


                                //yy.Text = drLinks["al_link"].ToString();
                                if (drLinks["summer"] != null & drLinks["summer"].ToString() != string.Empty)
                                {
                                    if (int.Parse(drLinks["summer"].ToString()) == 1)
                                    {
                                        yy.NavigateUrl = drLinks["al_url"].ToString() + "?idno=2";
                                    }
                                    else
                                    {
                                        string qry = string.Empty;
                                        if (drLinks["al_url"].ToString().Contains("?"))
                                            qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                        else
                                            qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                        if (url_idno > 0)
                                            qry += "&idno=" + url_idno.ToString();
                                        else
                                            qry += "&idno=-1";
                                        yy.NavigateUrl = qry;
                                    }
                                }
                                else
                                {
                                    string qry = string.Empty;
                                    if (drLinks["al_url"].ToString().Contains("?"))
                                        qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                    else
                                        qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();
                                    yy.NavigateUrl = qry;
                                }

                                if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
                                {
                                    if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                    {
                                        if (int.Parse(drLinks["levelno"].ToString()) == 3)
                                        {
                                            //mainMenu.Attributes.Add("","");
                                            aa.ChildItems.Add(yy);
                                        }
                                        else
                                        {
                                            zz.ChildItems.Add(yy);
                                        }
                                    }
                                    else
                                    {
                                        if (int.Parse(drLinks["levelno"].ToString()) == 3)
                                        {
                                            aa.ChildItems.Add(yy);
                                        }
                                        else if (int.Parse(drLinks["levelno"].ToString()) == 2)
                                        {
                                            zz.ChildItems.Add(yy);
                                        }
                                        else
                                        {
                                            xx.ChildItems.Add(yy);
                                        }
                                    }
                                }
                                else
                                {
                                    xx.ChildItems.Add(yy);
                                }

                            }
                        }
                    }
                }
                if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty)
                    linkno = int.Parse(drLinks["al_asno"].ToString());
                if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
                    mastno = int.Parse(drLinks["mastno"].ToString());

                if (int.Parse(drLinks["levelno"].ToString()) == 3)
                {
                    //mainMenu.Attributes[".level3"];
                    //mainMenu.Attributes.Add[".level3"];
                    //mainMenu.ApplyStyle(".level3");
                    //mainMenu.CssClass[".level3"];

                }
                else if (int.Parse(drLinks["levelno"].ToString()) == 2)
                {

                }
            }

            //After just compliation of while lopp
            ///Code added by Shrikant 11122017 for checking URL page no. and page link are valid
            ///**Start
            if (pageno != string.Empty)
            {
                if (findPage == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page= this");
                }
            }
            ///**End



            //Change Password for All
            ////xx = new MenuItem();
            ////xx.Text = "CHANGE PASSWORD";
            ////xx.NavigateUrl = "changepassword.aspx?pageno=500";
            ////tvLinks.Items.Add(xx);

            //////User Profile
            ////xx = new MenuItem();
            ////xx.Text = "USER PROFILE";
            ////xx.Target = "_parent";
            ////xx.NavigateUrl = "EmpPrpofile.aspx";
            ////tvLinks.Items.Add(xx);

            //////Log Out for All
            ////xx = new MenuItem();
            ////xx.Text = "LOGOUT";
            ////xx.Target = "_parent";
            ////xx.NavigateUrl = "logout.aspx";
            ////tvLinks.Items.Add(xx);
            //mainMenu.DynamicMenuItemStyle = ;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            //close all objects
            if (drLinks != null) drLinks.Close();
        }
    }

    protected void lnkTheme1_Click(object sender, EventArgs e)
    {
        Session["masterpage"] = "SiteMasterPage.master";
        Response.Redirect(Request.Url.ToString());
    }

    protected void lnkTheme2_Click(object sender, EventArgs e)
    {
        Session["masterpage"] = "SiteMasterPage2.master";
        Response.Redirect(Request.Url.ToString());
    }

    //protected void tvLinks_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    //If the selected node is changed and we are working with the top node (If you want the ExpandCollapse func further dow, duplicate this part..)
    //    if (tvLinks.SelectedNode.Depth == 0)
    //    {
    //        tvLinks.CollapseAll();         //Collapse all nodes
    //        tvLinks.SelectedNode.Expand(); //Expand the selected node
    //    }

    //    //Save the state of the treeview
    //    if (IsPostBack)
    //    {
    //        List<string> list = new List<string>(tvLinks.Nodes.Count);
    //        SaveTreeViewState(tvLinks.Nodes, list);
    //        Session["TreeViewState"] = list;
    //    }

    //    //All done, lets redirect to the new page
    //    if (IsPostBack)
    //    {
    //        //get the page link from tooltip of the selected node, so that we can redirect to it.
    //        string link = string.Empty;
    //        link = tvLinks.SelectedNode.ToolTip;
    //        Response.Redirect("~/" + link.ToString());
    //    }
    //}

    //The save state func...

    //private void SaveTreeViewState(TreeNodeCollection nodes, List<string> list)
    //{
    //  // Recursivley record all expanded nodes in the List.
    //  foreach (TreeNode node in nodes)
    //  {
    //    if (node.ChildNodes != null && node.ChildNodes.Count != 0)
    //    {
    //        if (node.Expanded.HasValue && node.Expanded == true && !String.IsNullOrEmpty(node.Text))
    //            list.Add(node.Text);
    //        SaveTreeViewState(node.ChildNodes, list);
    //    }
    //  }
    //}

    ////The restore state func...
    //private void RestoreTreeViewState(TreeNodeCollection nodes, List<string> list)
    //{
    //    foreach (TreeNode node in nodes)
    //    {
    //        // Restore the state of one node.
    //        if (list.Contains(node.Text))
    //        {
    //            if (node.ChildNodes != null && node.ChildNodes.Count != 0 && node.Expanded.HasValue && node.Expanded == false)
    //                node.Expand();
    //        }
    //        else
    //        {
    //            if (node.ChildNodes != null && node.ChildNodes.Count != 0 && node.Expanded.HasValue && node.Expanded == true)
    //                node.Collapse();
    //        }
    //        // If the node has child nodes, restore their state, too.
    //        if (node.ChildNodes != null && node.ChildNodes.Count != 0)
    //            RestoreTreeViewState(node.ChildNodes, list);
    //    }
    //}

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/home.aspx");
        Response.Redirect("~/default.aspx");
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/home.aspx");
        if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "5")
        {
            Response.Redirect("~/homeNonFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")  //Added by sachin 17082023
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else
        {
            Response.Redirect("~/principalHome.aspx");
        }
    }

    protected void mainMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        try
        {
            LogFile objLog = new LogFile();
            if ((Session["username"] != null && Session["logid"] !=null) || Convert.ToString(Session["username"]) != string.Empty && Convert.ToString(Session["logid"])!=string.Empty)
            {
                objLog.Ua_Name = Convert.ToString(Session["username"]);
                objLog.LogoutTime = DateTime.Now;
                objLog.ID = Convert.ToInt32(Session["logid"]);
                LogTableController.UpdateLog(objLog);
                if (isServer == "true")
                {
                  //  Response.Redirect("~/default.aspx");
                    Response.Redirect("~/default.aspx",false);
                }
                else
                {
                   // Response.Redirect("~/default_crescent.aspx");
                    Response.Redirect("~/default.aspx", false);
                }
            }
            else
            {
                if (isServer == "true")
                {
                   // Response.Redirect("~/default.aspx");
                    Response.Redirect("~/default.aspx", false);
                }
                else
                {
                  //  Response.Redirect("~/default_crescent.aspx");
                    Response.Redirect("~/default.aspx", false);
                }
            }
        }
        catch(Exception Ex)
        {
            //Response.Redirect("~/default.aspx");
        }
    }
    protected void btndashboard_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else
        {
            Response.Redirect("~/principalHome.aspx");
        }
    }
    protected void btnuser_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/user.aspx");
    }

    protected void BindImage()
    {

        try
        {
            DataTableReader dtr = null;
            SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[2];
            objParams[0] = new SqlParameter("@P_Idno", Convert.ToInt32(Session["idno"]));
            string type = "";
            if (Convert.ToInt32(Session["usertype"]) == 2)
                type = "STUDENT";
            else if (Convert.ToInt32(Session["usertype"]) == 3)
                type = "EMP";

            else
            {
                //imgPhoto.ImageUrl = "~/IMAGES/no-user.png";
                //imgDash.ImageUrl = "~/IMAGES/no-user.png";
                imgPhoto.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                imgDash.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                noimg.Visible = true;
                noimghead.Visible = true;
                imgPhoto.Visible = false;
                imgDash.Visible = false;
                return;
            }
            objParams[1] = new SqlParameter("@P_TYPE", type);
            dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDEMP_SP_RET_PHOTO", objParams).Tables[0].CreateDataReader();
            if (dtr.Read())
            {
                if (dtr["PHOTO"].ToString() == "")
                {

                    imgPhoto.ImageUrl = "~/IMAGES/no-user.png";
                    imgDash.ImageUrl = "~/IMAGES/no-user.png";
                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        if (Session["idno"] != null)
                        {
                            //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=STUDENT";
                            //imgDash.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=STUDENT";
                            CheckPhotoExistOrNot("STUDENT");
                        }
                    }
                    else if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        if (Session["idno"] != null)
                        {
                            //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=EMP";
                            //imgDash.ImageUrl = "~/showimage.aspx?id=" + Session["IDNO"].ToString() + "&type=EMP";
                            CheckPhotoExistOrNot("EMP");
                        }
                    }
                    else
                    {
                        //imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
                        //imgDash.ImageUrl = "~/IMAGES/nophoto.jpg";
                        imgPhoto.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                        imgDash.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                        noimg.Visible = true;
                        noimghead.Visible = true;
                        imgPhoto.Visible = false;
                        imgDash.Visible = false;
                    }
                }
            }
            else
            {
                //imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
                //imgDash.ImageUrl = "~/IMAGES/nophoto.jpg";
                imgPhoto.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                imgDash.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
                noimg.Visible = true;
                noimghead.Visible = true;
                imgPhoto.Visible = false;
                imgDash.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
            //imgDash.ImageUrl = "~/IMAGES/nophoto.jpg";
            imgPhoto.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
            imgDash.ImageUrl = Session["userfullname"].ToString().Substring(0, 1);
            noimg.Visible = true;
            noimghead.Visible = true;
            imgPhoto.Visible = false;
            imgDash.Visible = false;
        }
    }

    private void BindQuickLinks(string LogId)
    {
        try
        {
            string host = Request.Url.Host;
            string scheme = Request.Url.Scheme;
            int portno = Request.Url.Port;
            string userid = Session["username"].ToString();

            DataSet dsGetUser = LogTableController.SearchUser(userid);

            if (dsGetUser.Tables[0].Rows.Count > 0)
            {
                if (host == "localhost")
                {
                    if (dsGetUser.Tables[0].Rows.Count >= 1)
                    {
                        li1.Visible = true;
                        string lblLinkAddressql1 = dsGetUser.Tables[0].Rows[0]["AL_URL"].ToString();
                        int PageNoql1 = Convert.ToInt32(dsGetUser.Tables[0].Rows[0]["PNUMBER"].ToString());
                        iql1.InnerText = dsGetUser.Tables[0].Rows[0]["PNAME"].ToString();

                        if (lblLinkAddressql1.Contains("Masters/masters"))
                        {
                            ql1.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql1 + "&pageno=" + PageNoql1;
                        }
                        else
                        {
                            ql1.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql1 + "?pageno=" + PageNoql1;
                        }
                    }
                    else
                    {
                        li1.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 2)
                    {
                        li2.Visible = true;
                        string lblLinkAddressql2 = dsGetUser.Tables[0].Rows[1]["AL_URL"].ToString();
                        int PageNoql2 = Convert.ToInt32(dsGetUser.Tables[0].Rows[1]["PNUMBER"].ToString());
                        iql2.InnerText = dsGetUser.Tables[0].Rows[1]["PNAME"].ToString();
                        if (lblLinkAddressql2.Contains("Masters/masters"))
                        {
                            ql2.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql2 + "&pageno=" + PageNoql2;
                        }
                        else
                        {
                            ql2.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql2 + "?pageno=" + PageNoql2;
                        }
                    }
                    else
                    {
                        li2.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 3)
                    {
                        li3.Visible = true;
                        string lblLinkAddressql3 = dsGetUser.Tables[0].Rows[2]["AL_URL"].ToString();
                        int PageNoql3 = Convert.ToInt32(dsGetUser.Tables[0].Rows[2]["PNUMBER"].ToString());
                        iql3.InnerText = dsGetUser.Tables[0].Rows[2]["PNAME"].ToString();
                        if (lblLinkAddressql3.Contains("Masters/masters"))
                        {
                            ql3.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql3 + "&pageno=" + PageNoql3;
                        }
                        else
                        {
                            ql3.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql3 + "?pageno=" + PageNoql3;
                        }
                    }
                    else
                    {
                        li3.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 4)
                    {
                        li4.Visible = true;
                        string lblLinkAddressql4 = dsGetUser.Tables[0].Rows[3]["AL_URL"].ToString();
                        int PageNoql4 = Convert.ToInt32(dsGetUser.Tables[0].Rows[3]["PNUMBER"].ToString());
                        iql4.InnerText = dsGetUser.Tables[0].Rows[3]["PNAME"].ToString();
                        if (lblLinkAddressql4.Contains("Masters/masters"))
                        {
                            ql4.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql4 + "&pageno=" + PageNoql4;
                        }
                        else
                        {
                            ql4.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql4 + "?pageno=" + PageNoql4;
                        }
                    }
                    else
                    {
                        li4.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 5)
                    {
                        li5.Visible = true;
                        string lblLinkAddressql5 = dsGetUser.Tables[0].Rows[4]["AL_URL"].ToString();
                        int PageNoql5 = Convert.ToInt32(dsGetUser.Tables[0].Rows[4]["PNUMBER"].ToString());
                        iql5.InnerText = dsGetUser.Tables[0].Rows[4]["PNAME"].ToString();
                        if (lblLinkAddressql5.Contains("Masters/masters"))
                        {
                            ql5.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql5 + "&pageno=" + PageNoql5;
                        }
                        else
                        {
                            ql5.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql5 + "?pageno=" + PageNoql5;
                        }
                    }
                    else
                    {
                        li5.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 6)
                    {
                        li6.Visible = true;
                        string lblLinkAddressql6 = dsGetUser.Tables[0].Rows[5]["AL_URL"].ToString();
                        int PageNoql6 = Convert.ToInt32(dsGetUser.Tables[0].Rows[5]["PNUMBER"].ToString());
                        iql6.InnerText = dsGetUser.Tables[0].Rows[5]["PNAME"].ToString();
                        if (lblLinkAddressql6.Contains("Masters/masters"))
                        {
                            ql6.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql6 + "&pageno=" + PageNoql6;
                        }
                        else
                        {
                            ql6.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql6 + "?pageno=" + PageNoql6;
                        }
                    }
                    else
                    {
                        li6.Visible = false;
                    }


                    if (dsGetUser.Tables[0].Rows.Count >= 7)
                    {
                        li7.Visible = true;
                        string lblLinkAddressql7 = dsGetUser.Tables[0].Rows[6]["AL_URL"].ToString();
                        int PageNoql7 = Convert.ToInt32(dsGetUser.Tables[0].Rows[6]["PNUMBER"].ToString());
                        iql7.InnerText = dsGetUser.Tables[0].Rows[6]["PNAME"].ToString();
                        if (lblLinkAddressql7.Contains("Masters/masters"))
                        {
                            ql7.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql7 + "&pageno=" + PageNoql7;
                        }
                        else
                        {
                            ql7.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql7 + "?pageno=" + PageNoql7;
                        }
                    }
                    else
                    {
                        li7.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 8)
                    {
                        li8.Visible = true;
                        string lblLinkAddressql8 = dsGetUser.Tables[0].Rows[7]["AL_URL"].ToString();
                        int PageNoql8 = Convert.ToInt32(dsGetUser.Tables[0].Rows[7]["PNUMBER"].ToString());
                        iql8.InnerText = dsGetUser.Tables[0].Rows[7]["PNAME"].ToString();
                        if (lblLinkAddressql8.Contains("Masters/masters"))
                        {
                            ql8.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql8 + "&pageno=" + PageNoql8;
                        }
                        else
                        {
                            ql8.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql8 + "?pageno=" + PageNoql8;
                        }
                    }
                    else
                    {
                        li8.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 9)
                    {
                        li9.Visible = true;
                        string lblLinkAddressql9 = dsGetUser.Tables[0].Rows[8]["AL_URL"].ToString();
                        int PageNoql9 = Convert.ToInt32(dsGetUser.Tables[0].Rows[8]["PNUMBER"].ToString());
                        iql9.InnerText = dsGetUser.Tables[0].Rows[8]["PNAME"].ToString();
                        if (lblLinkAddressql9.Contains("Masters/masters"))
                        {
                            ql9.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql9 + "&pageno=" + PageNoql9;
                        }
                        else
                        {
                            ql9.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql9 + "?pageno=" + PageNoql9;
                        }
                    }
                    else
                    {
                        li9.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 10)
                    {
                        li10.Visible = true;
                        string lblLinkAddressql10 = dsGetUser.Tables[0].Rows[9]["AL_URL"].ToString();
                        int PageNoql10 = Convert.ToInt32(dsGetUser.Tables[0].Rows[9]["PNUMBER"].ToString());
                        iql10.InnerText = dsGetUser.Tables[0].Rows[9]["PNAME"].ToString();
                        if (lblLinkAddressql10.Contains("Masters/masters"))
                        {
                            ql10.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql10 + "&pageno=" + PageNoql10;
                        }
                        else
                        {
                            ql10.HRef = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql10 + "?pageno=" + PageNoql10;
                        }
                    }
                    else
                    {
                        li10.Visible = false;
                    }
                }

                // For Virtual Directory
                //else if (host == "192.168.0.113")
                //{
                //    if (dsGetUser.Tables[0].Rows.Count >= 1)
                //    {
                //        li1.Visible = true;
                //        string lblLinkAddressql1 = dsGetUser.Tables[0].Rows[0]["AL_URL"].ToString();
                //        int PageNoql1 = Convert.ToInt32(dsGetUser.Tables[0].Rows[0]["PNUMBER"].ToString());
                //        iql1.InnerText = dsGetUser.Tables[0].Rows[0]["PNAME"].ToString();

                //        if (lblLinkAddressql1.Contains("Masters/masters"))
                //        {
                //            ql1.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql1 + "&pageno=" + PageNoql1;
                //        }
                //        else
                //        {
                //            ql1.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql1 + "?pageno=" + PageNoql1;
                //        }
                //    }
                //    else
                //    {
                //        li1.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 2)
                //    {
                //        li2.Visible = true;
                //        string lblLinkAddressql2 = dsGetUser.Tables[0].Rows[1]["AL_URL"].ToString();
                //        int PageNoql2 = Convert.ToInt32(dsGetUser.Tables[0].Rows[1]["PNUMBER"].ToString());
                //        iql2.InnerText = dsGetUser.Tables[0].Rows[1]["PNAME"].ToString();
                //        if (lblLinkAddressql2.Contains("Masters/masters"))
                //        {
                //            ql2.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql2 + "&pageno=" + PageNoql2;
                //        }
                //        else
                //        {
                //            ql2.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql2 + "?pageno=" + PageNoql2;
                //        }
                //    }
                //    else
                //    {
                //        li2.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 3)
                //    {
                //        li3.Visible = true;
                //        string lblLinkAddressql3 = dsGetUser.Tables[0].Rows[2]["AL_URL"].ToString();
                //        int PageNoql3 = Convert.ToInt32(dsGetUser.Tables[0].Rows[2]["PNUMBER"].ToString());
                //        iql3.InnerText = dsGetUser.Tables[0].Rows[2]["PNAME"].ToString();
                //        if (lblLinkAddressql3.Contains("Masters/masters"))
                //        {
                //            ql3.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql3 + "&pageno=" + PageNoql3;
                //        }
                //        else
                //        {
                //            ql3.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql3 + "?pageno=" + PageNoql3;
                //        }
                //    }
                //    else
                //    {
                //        li3.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 4)
                //    {
                //        li4.Visible = true;
                //        string lblLinkAddressql4 = dsGetUser.Tables[0].Rows[3]["AL_URL"].ToString();
                //        int PageNoql4 = Convert.ToInt32(dsGetUser.Tables[0].Rows[3]["PNUMBER"].ToString());
                //        iql4.InnerText = dsGetUser.Tables[0].Rows[3]["PNAME"].ToString();
                //        if (lblLinkAddressql4.Contains("Masters/masters"))
                //        {
                //            ql4.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql4 + "&pageno=" + PageNoql4;
                //        }
                //        else
                //        {
                //            ql4.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql4 + "?pageno=" + PageNoql4;
                //        }
                //    }
                //    else
                //    {
                //        li4.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 5)
                //    {
                //        li5.Visible = true;
                //        string lblLinkAddressql5 = dsGetUser.Tables[0].Rows[4]["AL_URL"].ToString();
                //        int PageNoql5 = Convert.ToInt32(dsGetUser.Tables[0].Rows[4]["PNUMBER"].ToString());
                //        iql5.InnerText = dsGetUser.Tables[0].Rows[4]["PNAME"].ToString();
                //        if (lblLinkAddressql5.Contains("Masters/masters"))
                //        {
                //            ql5.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql5 + "&pageno=" + PageNoql5;
                //        }
                //        else
                //        {
                //            ql5.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql5 + "?pageno=" + PageNoql5;
                //        }
                //    }
                //    else
                //    {
                //        li5.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 6)
                //    {
                //        li6.Visible = true;
                //        string lblLinkAddressql6 = dsGetUser.Tables[0].Rows[5]["AL_URL"].ToString();
                //        int PageNoql6 = Convert.ToInt32(dsGetUser.Tables[0].Rows[5]["PNUMBER"].ToString());
                //        iql6.InnerText = dsGetUser.Tables[0].Rows[5]["PNAME"].ToString();
                //        if (lblLinkAddressql6.Contains("Masters/masters"))
                //        {
                //            ql6.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql6 + "&pageno=" + PageNoql6;
                //        }
                //        else
                //        {
                //            ql6.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql6 + "?pageno=" + PageNoql6;
                //        }
                //    }
                //    else
                //    {
                //        li6.Visible = false;
                //    }


                //    if (dsGetUser.Tables[0].Rows.Count >= 7)
                //    {
                //        li7.Visible = true;
                //        string lblLinkAddressql7 = dsGetUser.Tables[0].Rows[6]["AL_URL"].ToString();
                //        int PageNoql7 = Convert.ToInt32(dsGetUser.Tables[0].Rows[6]["PNUMBER"].ToString());
                //        iql7.InnerText = dsGetUser.Tables[0].Rows[6]["PNAME"].ToString();
                //        if (lblLinkAddressql7.Contains("Masters/masters"))
                //        {
                //            ql7.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql7 + "&pageno=" + PageNoql7;
                //        }
                //        else
                //        {
                //            ql7.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql7 + "?pageno=" + PageNoql7;
                //        }
                //    }
                //    else
                //    {
                //        li7.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 8)
                //    {
                //        li8.Visible = true;
                //        string lblLinkAddressql8 = dsGetUser.Tables[0].Rows[7]["AL_URL"].ToString();
                //        int PageNoql8 = Convert.ToInt32(dsGetUser.Tables[0].Rows[7]["PNUMBER"].ToString());
                //        iql8.InnerText = dsGetUser.Tables[0].Rows[7]["PNAME"].ToString();
                //        if (lblLinkAddressql8.Contains("Masters/masters"))
                //        {
                //            ql8.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql8 + "&pageno=" + PageNoql8;
                //        }
                //        else
                //        {
                //            ql8.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql8 + "?pageno=" + PageNoql8;
                //        }
                //    }
                //    else
                //    {
                //        li8.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 9)
                //    {
                //        li9.Visible = true;
                //        string lblLinkAddressql9 = dsGetUser.Tables[0].Rows[8]["AL_URL"].ToString();
                //        int PageNoql9 = Convert.ToInt32(dsGetUser.Tables[0].Rows[8]["PNUMBER"].ToString());
                //        iql9.InnerText = dsGetUser.Tables[0].Rows[8]["PNAME"].ToString();
                //        if (lblLinkAddressql9.Contains("Masters/masters"))
                //        {
                //            ql9.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql9 + "&pageno=" + PageNoql9;
                //        }
                //        else
                //        {
                //            ql9.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql9 + "?pageno=" + PageNoql9;
                //        }
                //    }
                //    else
                //    {
                //        li9.Visible = false;
                //    }

                //    if (dsGetUser.Tables[0].Rows.Count >= 10)
                //    {
                //        li10.Visible = true;
                //        string lblLinkAddressql10 = dsGetUser.Tables[0].Rows[9]["AL_URL"].ToString();
                //        int PageNoql10 = Convert.ToInt32(dsGetUser.Tables[0].Rows[9]["PNUMBER"].ToString());
                //        iql10.InnerText = dsGetUser.Tables[0].Rows[9]["PNAME"].ToString();
                //        if (lblLinkAddressql10.Contains("Masters/masters"))
                //        {
                //            ql10.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql10 + "&pageno=" + PageNoql10;
                //        }
                //        else
                //        {
                //            ql10.HRef = scheme + "://" + host + "/ArkaJain_QL/" + lblLinkAddressql10 + "?pageno=" + PageNoql10;
                //        }
                //    }
                //    else
                //    {
                //        li10.Visible = false;
                //    }
                //}

                    // For Live Code And Test Links
                else
                {
                    if (dsGetUser.Tables[0].Rows.Count >= 1)
                    {
                        li1.Visible = true;
                        string lblLinkAddressql1 = dsGetUser.Tables[0].Rows[0]["AL_URL"].ToString();
                        int PageNoql1 = Convert.ToInt32(dsGetUser.Tables[0].Rows[0]["PNUMBER"].ToString());
                        iql1.InnerText = dsGetUser.Tables[0].Rows[0]["PNAME"].ToString();

                        if (lblLinkAddressql1.Contains("Masters/masters"))
                        {
                            ql1.HRef = scheme + "://" + host + "/" + lblLinkAddressql1 + "&pageno=" + PageNoql1;
                        }
                        else
                        {
                            ql1.HRef = scheme + "://" + host + "/" + lblLinkAddressql1 + "?pageno=" + PageNoql1;
                        }
                    }
                    else
                    {
                        li1.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 2)
                    {
                        li2.Visible = true;
                        string lblLinkAddressql2 = dsGetUser.Tables[0].Rows[1]["AL_URL"].ToString();
                        int PageNoql2 = Convert.ToInt32(dsGetUser.Tables[0].Rows[1]["PNUMBER"].ToString());
                        iql2.InnerText = dsGetUser.Tables[0].Rows[1]["PNAME"].ToString();
                        if (lblLinkAddressql2.Contains("Masters/masters"))
                        {
                            ql2.HRef = scheme + "://" + host + "/" + lblLinkAddressql2 + "&pageno=" + PageNoql2;
                        }
                        else
                        {
                            ql2.HRef = scheme + "://" + host + "/" + lblLinkAddressql2 + "?pageno=" + PageNoql2;
                        }
                    }
                    else
                    {
                        li2.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 3)
                    {
                        li3.Visible = true;
                        string lblLinkAddressql3 = dsGetUser.Tables[0].Rows[2]["AL_URL"].ToString();
                        int PageNoql3 = Convert.ToInt32(dsGetUser.Tables[0].Rows[2]["PNUMBER"].ToString());
                        iql3.InnerText = dsGetUser.Tables[0].Rows[2]["PNAME"].ToString();
                        if (lblLinkAddressql3.Contains("Masters/masters"))
                        {
                            ql3.HRef = scheme + "://" + host + "/" + lblLinkAddressql3 + "&pageno=" + PageNoql3;
                        }
                        else
                        {
                            ql3.HRef = scheme + "://" + host + "/" + lblLinkAddressql3 + "?pageno=" + PageNoql3;
                        }
                    }
                    else
                    {
                        li3.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 4)
                    {
                        li4.Visible = true;
                        string lblLinkAddressql4 = dsGetUser.Tables[0].Rows[3]["AL_URL"].ToString();
                        int PageNoql4 = Convert.ToInt32(dsGetUser.Tables[0].Rows[3]["PNUMBER"].ToString());
                        iql4.InnerText = dsGetUser.Tables[0].Rows[3]["PNAME"].ToString();
                        if (lblLinkAddressql4.Contains("Masters/masters"))
                        {
                            ql4.HRef = scheme + "://" + host + "/" + lblLinkAddressql4 + "&pageno=" + PageNoql4;
                        }
                        else
                        {
                            ql4.HRef = scheme + "://" + host + "/" + lblLinkAddressql4 + "?pageno=" + PageNoql4;
                        }
                    }
                    else
                    {
                        li4.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 5)
                    {
                        li5.Visible = true;
                        string lblLinkAddressql5 = dsGetUser.Tables[0].Rows[4]["AL_URL"].ToString();
                        int PageNoql5 = Convert.ToInt32(dsGetUser.Tables[0].Rows[4]["PNUMBER"].ToString());
                        iql5.InnerText = dsGetUser.Tables[0].Rows[4]["PNAME"].ToString();
                        if (lblLinkAddressql5.Contains("Masters/masters"))
                        {
                            ql5.HRef = scheme + "://" + host + "/" + lblLinkAddressql5 + "&pageno=" + PageNoql5;
                        }
                        else
                        {
                            ql5.HRef = scheme + "://" + host + "/" + lblLinkAddressql5 + "?pageno=" + PageNoql5;
                        }
                    }
                    else
                    {
                        li5.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 6)
                    {
                        li6.Visible = true;
                        string lblLinkAddressql6 = dsGetUser.Tables[0].Rows[5]["AL_URL"].ToString();
                        int PageNoql6 = Convert.ToInt32(dsGetUser.Tables[0].Rows[5]["PNUMBER"].ToString());
                        iql6.InnerText = dsGetUser.Tables[0].Rows[5]["PNAME"].ToString();
                        if (lblLinkAddressql6.Contains("Masters/masters"))
                        {
                            ql6.HRef = scheme + "://" + host + "/" + lblLinkAddressql6 + "&pageno=" + PageNoql6;
                        }
                        else
                        {
                            ql6.HRef = scheme + "://" + host + "/" + lblLinkAddressql6 + "?pageno=" + PageNoql6;
                        }
                    }
                    else
                    {
                        li6.Visible = false;
                    }


                    if (dsGetUser.Tables[0].Rows.Count >= 7)
                    {
                        li7.Visible = true;
                        string lblLinkAddressql7 = dsGetUser.Tables[0].Rows[6]["AL_URL"].ToString();
                        int PageNoql7 = Convert.ToInt32(dsGetUser.Tables[0].Rows[6]["PNUMBER"].ToString());
                        iql7.InnerText = dsGetUser.Tables[0].Rows[6]["PNAME"].ToString();
                        if (lblLinkAddressql7.Contains("Masters/masters"))
                        {
                            ql7.HRef = scheme + "://" + host + "/" + lblLinkAddressql7 + "&pageno=" + PageNoql7;
                        }
                        else
                        {
                            ql7.HRef = scheme + "://" + host + "/" + lblLinkAddressql7 + "?pageno=" + PageNoql7;
                        }
                    }
                    else
                    {
                        li7.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 8)
                    {
                        li8.Visible = true;
                        string lblLinkAddressql8 = dsGetUser.Tables[0].Rows[7]["AL_URL"].ToString();
                        int PageNoql8 = Convert.ToInt32(dsGetUser.Tables[0].Rows[7]["PNUMBER"].ToString());
                        iql8.InnerText = dsGetUser.Tables[0].Rows[7]["PNAME"].ToString();
                        if (lblLinkAddressql8.Contains("Masters/masters"))
                        {
                            ql8.HRef = scheme + "://" + host + "/" + lblLinkAddressql8 + "&pageno=" + PageNoql8;
                        }
                        else
                        {
                            ql8.HRef = scheme + "://" + host + "/" + lblLinkAddressql8 + "?pageno=" + PageNoql8;
                        }
                    }
                    else
                    {
                        li8.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 9)
                    {
                        li9.Visible = true;
                        string lblLinkAddressql9 = dsGetUser.Tables[0].Rows[8]["AL_URL"].ToString();
                        int PageNoql9 = Convert.ToInt32(dsGetUser.Tables[0].Rows[8]["PNUMBER"].ToString());
                        iql9.InnerText = dsGetUser.Tables[0].Rows[8]["PNAME"].ToString();
                        if (lblLinkAddressql9.Contains("Masters/masters"))
                        {
                            ql9.HRef = scheme + "://" + host + "/" + lblLinkAddressql9 + "&pageno=" + PageNoql9;
                        }
                        else
                        {
                            ql9.HRef = scheme + "://" + host + "/" + lblLinkAddressql9 + "?pageno=" + PageNoql9;
                        }
                    }
                    else
                    {
                        li9.Visible = false;
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 10)
                    {
                        li10.Visible = true;
                        string lblLinkAddressql10 = dsGetUser.Tables[0].Rows[9]["AL_URL"].ToString();
                        int PageNoql10 = Convert.ToInt32(dsGetUser.Tables[0].Rows[9]["PNUMBER"].ToString());
                        iql10.InnerText = dsGetUser.Tables[0].Rows[9]["PNAME"].ToString();
                        if (lblLinkAddressql10.Contains("Masters/masters"))
                        {
                            ql10.HRef = scheme + "://" + host + "/" + lblLinkAddressql10 + "&pageno=" + PageNoql10;
                        }
                        else
                        {
                            ql10.HRef = scheme + "://" + host + "/" + lblLinkAddressql10 + "?pageno=" + PageNoql10;
                        }
                    }
                    else
                    {
                        li10.Visible = false;
                    }
                }

            }
            else
            {
                //objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
        }
        catch
        {
            throw;
        }
    }

    //ADDED TODO LIST BY Deepali ON 24082020
    private void BindToDolist(string LogId)
    {
        try
        {
            string host = Request.Url.Host;
            string scheme = Request.Url.Scheme;
            int portno = Request.Url.Port;
            string userid = Session["userno"].ToString();

            DataSet dsGetUser = LogTableController.SearchUserToDoList(userid);

            if (dsGetUser.Tables[0].Rows.Count > 0)
            {
                if (host == "localhost")
                {
                    if (dsGetUser.Tables[0].Rows.Count >= 1)
                    {
                        //li21.Visible = true;
                        li21.Style.Add("display", "block");
                        iql21.InnerText = dsGetUser.Tables[0].Rows[0]["TD_NAME"].ToString();
                        string item0 = dsGetUser.Tables[0].Rows[0]["TD_CHECK"].ToString();
                        if (item0 == "1")
                        {
                            chk1.Checked = true;
                        }
                        else
                        {
                            chk1.Checked = false;
                        }
                        // A3.InnerText = dsGetUser.Tables[0].Rows[0]["TD_ID"].ToString();
                        // A3.Visible = false;
                    }
                    else
                    {
                        //li21.Visible = false;
                        li21.Style.Add("display", "none");
                    }

                    if (dsGetUser.Tables[0].Rows.Count >= 2)
                    {
                        li22.Style.Add("display", "block");
                        iql22.InnerText = dsGetUser.Tables[0].Rows[1]["TD_NAME"].ToString();
                        string item1 = dsGetUser.Tables[0].Rows[1]["TD_CHECK"].ToString();
                        if (item1 == "1")
                        {
                            chk2.Checked = true;
                        }
                        else
                        {
                            chk2.Checked = false;
                        }

                    }
                    else
                    {
                        li22.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 3)
                    {
                        li23.Style.Add("display", "block");
                        iql23.InnerText = dsGetUser.Tables[0].Rows[2]["TD_NAME"].ToString();
                        string item2 = dsGetUser.Tables[0].Rows[2]["TD_CHECK"].ToString();
                        if (item2 == "1")
                        {
                            chk3.Checked = true;
                        }
                        else
                        {
                            chk3.Checked = false;
                        }
                    }
                    else
                    {
                        li23.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 4)
                    {
                        li24.Style.Add("display", "block");
                        iql24.InnerText = dsGetUser.Tables[0].Rows[3]["TD_NAME"].ToString();
                        string item3 = dsGetUser.Tables[0].Rows[3]["TD_CHECK"].ToString();
                        if (item3 == "1")
                        {
                            chk4.Checked = true;
                        }
                        else
                        {
                            chk4.Checked = false;
                        }
                    }
                    else
                    {
                        li24.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 5)
                    {
                        li25.Style.Add("display", "block");
                        iql25.InnerText = dsGetUser.Tables[0].Rows[4]["TD_NAME"].ToString();
                        string item4 = dsGetUser.Tables[0].Rows[4]["TD_CHECK"].ToString();
                        if (item4 == "1")
                        {
                            chk5.Checked = true;
                        }
                        else
                        {
                            chk5.Checked = false;
                        }
                    }
                    else
                    {
                        li25.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 6)
                    {
                        li26.Style.Add("display", "block");
                        iql26.InnerText = dsGetUser.Tables[0].Rows[5]["TD_NAME"].ToString();
                        string item5 = dsGetUser.Tables[0].Rows[5]["TD_CHECK"].ToString();
                        if (item5 == "1")
                        {
                            chk6.Checked = true;
                        }
                        else
                        {
                            chk6.Checked = false;
                        }
                    }
                    else
                    {
                        li26.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 7)
                    {
                        li27.Style.Add("display", "block");
                        iql27.InnerText = dsGetUser.Tables[0].Rows[6]["TD_NAME"].ToString();
                        string item6 = dsGetUser.Tables[0].Rows[6]["TD_CHECK"].ToString();
                        if (item6 == "1")
                        {
                            chk7.Checked = true;
                        }
                        else
                        {
                            chk7.Checked = false;
                        }
                    }
                    else
                    {
                        li27.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 8)
                    {
                        li28.Style.Add("display", "block");
                        iql28.InnerText = dsGetUser.Tables[0].Rows[7]["TD_NAME"].ToString();
                        string item7 = dsGetUser.Tables[0].Rows[7]["TD_CHECK"].ToString();
                        if (item7 == "1")
                        {
                            chk8.Checked = true;
                        }
                        else
                        {
                            chk8.Checked = false;
                        }
                    }
                    else
                    {
                        li28.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 9)
                    {
                        li29.Style.Add("display", "block");
                        iql29.InnerText = dsGetUser.Tables[0].Rows[8]["TD_NAME"].ToString();
                        string item8 = dsGetUser.Tables[0].Rows[8]["TD_CHECK"].ToString();
                        if (item8 == "1")
                        {
                            chk9.Checked = true;
                        }
                        else
                        {
                            chk9.Checked = false;
                        }
                    }
                    else
                    {
                        li29.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 10)
                    {
                        li30.Style.Add("display", "block");
                        iql30.InnerText = dsGetUser.Tables[0].Rows[9]["TD_NAME"].ToString();
                        string item9 = dsGetUser.Tables[0].Rows[9]["TD_CHECK"].ToString();
                        if (item9 == "1")
                        {
                            chk10.Checked = true;
                        }
                        else
                        {
                            chk10.Checked = false;
                        }
                    }
                    else
                    {
                        li30.Style.Add("display", "none");
                    }
                }
                else
                {
                    if (dsGetUser.Tables[0].Rows.Count >= 1)
                    {
                        //li21.Visible = true;
                        li21.Style.Add("display", "block");
                        iql21.InnerText = dsGetUser.Tables[0].Rows[0]["TD_NAME"].ToString();
                        string item0 = dsGetUser.Tables[0].Rows[0]["TD_CHECK"].ToString();
                        if (item0 == "1")
                        {
                            chk1.Checked = true;
                        }
                        else
                        {
                            chk1.Checked = false;
                        }
                    }
                    else
                    {
                        //li21.Visible = false;
                        li21.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 2)
                    {
                        li22.Style.Add("display", "block");
                        iql22.InnerText = dsGetUser.Tables[0].Rows[1]["TD_NAME"].ToString();
                        string item1 = dsGetUser.Tables[0].Rows[1]["TD_CHECK"].ToString();
                        if (item1 == "1")
                        {
                            chk2.Checked = true;
                        }
                        else
                        {
                            chk2.Checked = false;
                        }

                    }
                    else
                    {
                        li22.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 3)
                    {
                        li23.Style.Add("display", "block");
                        iql23.InnerText = dsGetUser.Tables[0].Rows[2]["TD_NAME"].ToString();
                        string item2 = dsGetUser.Tables[0].Rows[2]["TD_CHECK"].ToString();
                        if (item2 == "1")
                        {
                            chk3.Checked = true;
                        }
                        else
                        {
                            chk3.Checked = false;
                        }
                    }
                    else
                    {
                        li23.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 4)
                    {
                        li24.Style.Add("display", "block");
                        iql24.InnerText = dsGetUser.Tables[0].Rows[3]["TD_NAME"].ToString();
                        string item3 = dsGetUser.Tables[0].Rows[3]["TD_CHECK"].ToString();
                        if (item3 == "1")
                        {
                            chk4.Checked = true;
                        }
                        else
                        {
                            chk4.Checked = false;
                        }
                    }
                    else
                    {
                        li24.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 5)
                    {
                        li25.Style.Add("display", "block");
                        iql25.InnerText = dsGetUser.Tables[0].Rows[4]["TD_NAME"].ToString();
                        string item4 = dsGetUser.Tables[0].Rows[4]["TD_CHECK"].ToString();
                        if (item4 == "1")
                        {
                            chk5.Checked = true;
                        }
                        else
                        {
                            chk5.Checked = false;
                        }
                    }
                    else
                    {
                        li25.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 6)
                    {
                        li26.Style.Add("display", "block");
                        iql26.InnerText = dsGetUser.Tables[0].Rows[5]["TD_NAME"].ToString();
                        string item5 = dsGetUser.Tables[0].Rows[5]["TD_CHECK"].ToString();
                        if (item5 == "1")
                        {
                            chk6.Checked = true;
                        }
                        else
                        {
                            chk6.Checked = false;
                        }
                    }
                    else
                    {
                        li26.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 7)
                    {
                        li27.Style.Add("display", "block");
                        iql27.InnerText = dsGetUser.Tables[0].Rows[6]["TD_NAME"].ToString();
                        string item6 = dsGetUser.Tables[0].Rows[6]["TD_CHECK"].ToString();
                        if (item6 == "1")
                        {
                            chk7.Checked = true;
                        }
                        else
                        {
                            chk7.Checked = false;
                        }
                    }
                    else
                    {
                        li27.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 8)
                    {
                        li28.Style.Add("display", "block");
                        iql28.InnerText = dsGetUser.Tables[0].Rows[7]["TD_NAME"].ToString();
                        string item7 = dsGetUser.Tables[0].Rows[7]["TD_CHECK"].ToString();
                        if (item7 == "1")
                        {
                            chk8.Checked = true;
                        }
                        else
                        {
                            chk8.Checked = false;
                        }
                    }
                    else
                    {
                        li28.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 9)
                    {
                        li29.Style.Add("display", "block");
                        iql29.InnerText = dsGetUser.Tables[0].Rows[8]["TD_NAME"].ToString();
                        string item8 = dsGetUser.Tables[0].Rows[8]["TD_CHECK"].ToString();
                        if (item8 == "1")
                        {
                            chk9.Checked = true;
                        }
                        else
                        {
                            chk9.Checked = false;
                        }
                    }
                    else
                    {
                        li29.Style.Add("display", "none");
                    }
                    if (dsGetUser.Tables[0].Rows.Count >= 10)
                    {
                        li30.Style.Add("display", "block");
                        iql30.InnerText = dsGetUser.Tables[0].Rows[9]["TD_NAME"].ToString();
                        string item9 = dsGetUser.Tables[0].Rows[9]["TD_CHECK"].ToString();
                        if (item9 == "1")
                        {
                            chk10.Checked = true;
                        }
                        else
                        {
                            chk10.Checked = false;
                        }
                    }
                    else
                    {
                        li30.Style.Add("display", "none");
                    }
                }

            }
            else
            {
                li21.Style.Add("display", "none");
                li22.Style.Add("display", "none");
                li23.Style.Add("display", "none");
                li24.Style.Add("display", "none");
                li25.Style.Add("display", "none");
                li26.Style.Add("display", "none");
                li27.Style.Add("display", "none");
                li28.Style.Add("display", "none");
                li29.Style.Add("display", "none");
                li30.Style.Add("display", "none");
                //objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
        }
        catch
        {
            //throw;
        }
    }

    //Added By Pranita Hiradkar on 05/10/2021 TAB PATCH
    //private void BindLinks(int uano, int alno, int mastno)
    //{
    //    try
    //    {
    //        tabcontent.Attributes["class"] = "container";
    //        DataSet dsLink = objCommon.GetUserSubLinks(uano, alno, mastno);
    //        Session["dsLink"] = dsLink;

    //        if (dsLink.Tables[0].Rows.Count > 0)
    //        {
    //            divLinks.Visible = true;
    //            repLinks.DataSource = dsLink;
    //            repLinks.DataBind();

    //            string link = dsLink.Tables[0].Rows[0]["AL_URL"].ToString();
    //            int pageno = Convert.ToInt32(dsLink.Tables[0].Rows[0]["AL_NO"].ToString());
    //            string url = string.Empty;
    //            string host = Request.Url.Host;
    //            string scheme = Request.Url.Scheme;
    //            int portno = Request.Url.Port;

    //            if (host == "localhost")
    //            {
    //                if (link.ToString().Contains("?"))
    //                {
    //                    url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno;
    //                }
    //                else
    //                {
    //                    url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno;
    //                }
    //            }
    //            else
    //            {
    //                if (link.ToString().Contains("?"))
    //                {
    //                    url = scheme + "://" + host + "/" + link + "&pageno=" + pageno;
    //                }
    //                else
    //                {
    //                    url = scheme + "://" + host + "/" + link + "?pageno=" + pageno;
    //                }
    //            }

    //            Response.Redirect(url);

    //        }
    //        else
    //        {
    //            divLinks.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //private void BindDataSetLinks()
    //{
    //    try
    //    {
    //        DataSet dsLink = (DataSet)Session["dsLink"] as DataSet;

    //        int uano = Convert.ToInt32(Session["userno"].ToString());
    //        int mastno = 0;
    //        int pageno = 0;

    //        if (Request.QueryString["pageno"] != null)
    //        {
    //            pageno = Convert.ToInt32(Request.QueryString["pageno"].ToString());
    //        }

    //        DataSet dsLinkMerge = new DataSet();
    //        dsLinkMerge.Merge(dsLink.Tables[0].Select("AL_NO=" + pageno, ""));

    //        if (dsLinkMerge.Tables.Count <= 0)
    //        {
    //            dsLink.Clear();
    //        }
    //        else
    //        {
    //            dsLink.Clear();
    //            dsLink = objCommon.GetUserSubLinks(uano, pageno, Convert.ToInt32(dsLinkMerge.Tables[0].Rows[0]["MASTNO"]));
    //        }

    //        if (dsLink != null && dsLink.Tables[0].Rows.Count > 0)
    //        {
    //            if (dsLink.Tables[0].Rows.Count > 0)
    //            {
    //                divLinks.Visible = true;
    //                repLinks.DataSource = dsLink;
    //                repLinks.DataBind();

    //                foreach (RepeaterItem itemEquipment in repLinks.Items)
    //                {
    //                    LinkButton lbLink = (LinkButton)itemEquipment.FindControl("lbLink");
    //                    HtmlGenericControl rptLi = (HtmlGenericControl)itemEquipment.FindControl("rptLi");
    //                    HtmlGenericControl iStar = (HtmlGenericControl)itemEquipment.FindControl("iStar");

    //                    string link = lbLink.CommandArgument;
    //                    int pageno1 = Convert.ToInt32(lbLink.CommandName);
    //                    string url = string.Empty;
    //                    string host = Request.Url.Host;
    //                    string scheme = Request.Url.Scheme;
    //                    int portno = Request.Url.Port;

    //                    if (host == "localhost")
    //                    {
    //                        if (link.ToString().Contains("?"))
    //                        {
    //                            url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno1;
    //                        }
    //                        else
    //                        {
    //                            url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno1;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        if (link.ToString().Contains("?"))
    //                        {
    //                            url = scheme + "://" + host + "/" + link + "&pageno=" + pageno1;
    //                        }
    //                        else
    //                        {
    //                            url = scheme + "://" + host + "/" + link + "?pageno=" + pageno1;
    //                        }
    //                    }

    //                    lbLink.Attributes.Add("href", url.ToString());

    //                    string path = Request.Url.AbsoluteUri;
    //                    int pos = path.LastIndexOf("=") + 1;
    //                    string pagenumber = path.Substring(pos, path.Length - pos);

    //                    if (lbLink.CommandName == pagenumber)
    //                    {
    //                        //iStar.Style.Add("color", "#fff !important");
    //                        iStar.Style.Add("color", "#fff");
    //                        rptLi.Style.Add("background-color", "#337ab7");
    //                        lbLink.Style.Add("color", "#fff !important");
    //                        lbLink.Style.Add("font-weight", "bold");
    //                    }
    //                    else
    //                    {
    //                        lbLink.Attributes.Remove("active");
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                divLinks.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            divLinks.Visible = false;
    //            uano = Convert.ToInt32(Session["userno"].ToString());
    //            int alno = 0;
    //            mastno = 0;

    //            if (Request.QueryString["pageno"] != null)
    //            {
    //                alno = Convert.ToInt32(Request.QueryString["pageno"].ToString());
    //            }

    //            DataSet dsLinkSearch = (DataSet)Session["dsLinkSearch"] as DataSet;
    //            if (dsLinkSearch != null)
    //            {
    //                if (dsLinkSearch.Tables[0].Rows.Count > 0)
    //                {
    //                    string where = "AL_No=" + Convert.ToString(alno);
    //                    string order = "AL_No";
    //                    DataSet ds1 = new DataSet();
    //                    ds1.Merge(dsLinkSearch.Tables[0].Select(where, order));
    //                    mastno = Convert.ToInt32(ds1.Tables[0].Rows[0]["MASTNO"]);
    //                }
    //            }

    //            DataSet dsLink1 = objCommon.GetUserSubLinks(uano, alno, mastno);

    //            if (dsLink1.Tables[0].Rows.Count > 0)
    //            {
    //                divLinks.Visible = true;
    //                repLinks.DataSource = dsLink1;
    //                repLinks.DataBind();

    //                foreach (RepeaterItem itemEquipment in repLinks.Items)
    //                {
    //                    LinkButton lbLink = (LinkButton)itemEquipment.FindControl("lbLink");
    //                    HtmlGenericControl rptLi = (HtmlGenericControl)itemEquipment.FindControl("rptLi");
    //                    HtmlGenericControl iStar = (HtmlGenericControl)itemEquipment.FindControl("iStar");

    //                    string link = lbLink.CommandArgument;
    //                    int pageno1 = Convert.ToInt32(lbLink.CommandName);
    //                    string url = string.Empty;
    //                    string host = Request.Url.Host;
    //                    string scheme = Request.Url.Scheme;
    //                    int portno = Request.Url.Port;

    //                    if (host == "localhost")
    //                    {
    //                        if (link.ToString().Contains("?"))
    //                        {
    //                            url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno1;
    //                        }
    //                        else
    //                        {
    //                            url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno1;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        if (link.ToString().Contains("?"))
    //                        {
    //                            url = scheme + "://" + host + "/" + link + "&pageno=" + pageno1;
    //                        }
    //                        else
    //                        {
    //                            url = scheme + "://" + host + "/" + link + "?pageno=" + pageno1;
    //                        }
    //                    }

    //                    lbLink.Attributes.Add("href", url.ToString());


    //                    string path = Request.Url.AbsoluteUri;
    //                    int pos = path.LastIndexOf("=") + 1;
    //                    string pagenumber = path.Substring(pos, path.Length - pos);

    //                    if (lbLink.CommandName == pagenumber)
    //                    {
    //                        lbLink.Attributes.Add("class", "active");
    //                        iStar.Style.Add("color", "#fff !important");

    //                        rptLi.Style.Add("background-color", "#337ab7");
    //                        lbLink.Style.Add("color", "#fff");
    //                        lbLink.Style.Add("font-weight", "bold");
    //                    }
    //                    else
    //                    {
    //                        lbLink.Attributes.Remove("active");
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                divLinks.Visible = false;
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    //protected void lbLink_Click(object sender, EventArgs e)
    //{
    //    LinkButton lbLink = (LinkButton)(sender);
    //    string link = lbLink.CommandArgument;
    //    int pageno = Convert.ToInt32(lbLink.CommandName);
    //    string url = string.Empty;
    //    string host = Request.Url.Host;
    //    string scheme = Request.Url.Scheme;
    //    int portno = Request.Url.Port;

    //    if (host == "localhost")
    //    {
    //        if (link.ToString().Contains("?"))
    //        {
    //            url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno;
    //        }
    //        else
    //        {
    //            url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno;
    //        }
    //    }
    //    else
    //    {
    //        if (link.ToString().Contains("?"))
    //        {
    //            url = scheme + "://" + host + "/" + link + "&pageno=" + pageno;
    //        }
    //        else
    //        {
    //            url = scheme + "://" + host + "/" + link + "?pageno=" + pageno;
    //        }
    //    }

    //    Response.Redirect(url);
    //}
    //Ended By Pranita Hiradkar on 05/10/2021 TAB PATCH//
    //Added By Pranita Hiradkar on 05/10/2021 TAB PATCH
    private void BindLinks(int uano, int alno, int mastno)
    {
        try
        {
            tabcontent.Attributes["class"] = "container";
            DataSet dsLink = objCommon.GetUserSubLinks(uano, alno, mastno);
            Session["dsLink"] = dsLink;
            Session["alno_level2"] = alno;
            if (dsLink.Tables[0].Rows.Count > 0)
            {
                divLinks.Visible = true;
                repLinks.DataSource = dsLink;
                repLinks.DataBind();

                string link = dsLink.Tables[0].Rows[0]["AL_URL"].ToString();
                int pageno = Convert.ToInt32(dsLink.Tables[0].Rows[0]["AL_NO"].ToString());
                string url = string.Empty;
                string host = Request.Url.Host;
                string scheme = Request.Url.Scheme;
                int portno = Request.Url.Port;

                if (host == "localhost")
                {
                    if (link.ToString().Contains("?"))
                    {
                        url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno;
                    }
                    else
                    {
                        url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno;
                    }
                }
                else
                {
                    if (link.ToString().Contains("?"))
                    {
                        url = scheme + "://" + host + "/" + link + "&pageno=" + pageno;
                    }
                    else
                    {
                        url = scheme + "://" + host + "/" + link + "?pageno=" + pageno;
                    }
                }

                Response.Redirect(url, false);
                //Request.Redirect(url, false);

            }
            else
            {
                divLinks.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindDataSetLinks()
    {
        try
        {
            DataSet dsLink = (DataSet)Session["dsLink"] as DataSet;

            int uano = Convert.ToInt32(Session["userno"].ToString());
            int mastno = 0;
            int pageno = 0;

            if (Request.QueryString["pageno"] != null)
            {
                pageno = Convert.ToInt32(Request.QueryString["pageno"].ToString());
            }
            //Session["alno_level2"] = pageno;
            DataSet dsLinkMerge = new DataSet();
            dsLinkMerge.Merge(dsLink.Tables[0].Select("AL_NO=" + pageno, ""));

            if (dsLinkMerge.Tables.Count <= 0)
            {
                dsLink.Clear();
            }
            else
            {
                dsLink.Clear();
                if (Convert.ToInt32(dsLinkMerge.Tables[0].Rows[0]["LEVELNO"]) == 1)
                {
                    dsLink = objCommon.GetUserSubLinks(uano, Convert.ToInt32(pageno), Convert.ToInt32(dsLinkMerge.Tables[0].Rows[0]["MASTNO"]));
                }
                else
                {
                    //dsLink = objCommon.GetUserSubLinks(uano, Convert.ToInt32(Session["alno_level2"]), Convert.ToInt32(dsLinkMerge.Tables[0].Rows[0]["MASTNO"]));
                    dsLink = objCommon.GetUserSubLinks(uano, Convert.ToInt32(dsLinkMerge.Tables[0].Rows[0]["MASTNO"]), Convert.ToInt32(dsLinkMerge.Tables[0].Rows[0]["MASTNO"]));

                }
            }

            if (dsLink != null && dsLink.Tables[0].Rows.Count > 0)
            {
                if (dsLink.Tables[0].Rows.Count > 0)
                {
                    divLinks.Visible = true;
                    repLinks.DataSource = dsLink;
                    repLinks.DataBind();

                    foreach (RepeaterItem itemEquipment in repLinks.Items)
                    {
                        LinkButton lbLink = (LinkButton)itemEquipment.FindControl("lbLink");
                        HtmlGenericControl rptLi = (HtmlGenericControl)itemEquipment.FindControl("rptLi");
                        HtmlGenericControl iStar = (HtmlGenericControl)itemEquipment.FindControl("iStar");

                        string link = lbLink.CommandArgument;
                        int pageno1 = Convert.ToInt32(lbLink.CommandName);
                        string url = string.Empty;
                        string host = Request.Url.Host;
                        string scheme = Request.Url.Scheme;
                        int portno = Request.Url.Port;

                        if (host == "localhost")
                        {
                            if (link.ToString().Contains("?"))
                            {
                                url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno1;
                            }
                            else
                            {
                                url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno1;
                            }
                        }
                        else
                        {
                            if (link.ToString().Contains("?"))
                            {
                                url = scheme + "://" + host + "/" + link + "&pageno=" + pageno1;
                            }
                            else
                            {
                                url = scheme + "://" + host + "/" + link + "?pageno=" + pageno1;
                            }
                        }

                        lbLink.Attributes.Add("href", url.ToString());

                        string path = Request.Url.AbsoluteUri;
                        int pos = path.LastIndexOf("=") + 1;
                        string pagenumber = path.Substring(pos, path.Length - pos);

                        if (lbLink.CommandName == pagenumber)
                        {
                            //iStar.Style.Add("color", "#fff !important");
                            lbLink.Attributes.Add("class", "link active");
                            iStar.Style.Add("color", "#fff");
                            rptLi.Style.Add("background-color", "var(--primary-color)");
                            //lbLink.Style.Add("color", "#fff !important");
                            lbLink.Style.Add("font-weight", "bold");
                        }
                        else
                        {
                            lbLink.Attributes.Remove("active");
                        }
                    }
                }
                else
                {
                    divLinks.Visible = false;
                }
            }
            else
            {
                divLinks.Visible = false;
                uano = Convert.ToInt32(Session["userno"].ToString());
                int alno = 0;
                mastno = 0;

                if (Request.QueryString["pageno"] != null)
                {
                    alno = Convert.ToInt32(Request.QueryString["pageno"].ToString());
                }

                DataSet dsLinkSearch = (DataSet)Session["dsLinkSearch"] as DataSet;
                if (dsLinkSearch != null)
                {
                    if (dsLinkSearch.Tables[0].Rows.Count > 0)
                    {
                        string where = "AL_No=" + Convert.ToString(alno);
                        string order = "AL_No";
                        DataSet ds1 = new DataSet();
                        ds1.Merge(dsLinkSearch.Tables[0].Select(where, order));
                        mastno = Convert.ToInt32(ds1.Tables[0].Rows[0]["MASTNO"]);
                    }
                }

                DataSet dsLink1 = objCommon.GetUserSubLinks(uano, alno, mastno);

                if (dsLink1.Tables[0].Rows.Count > 0)
                {
                    divLinks.Visible = true;
                    repLinks.DataSource = dsLink1;
                    repLinks.DataBind();

                    foreach (RepeaterItem itemEquipment in repLinks.Items)
                    {
                        LinkButton lbLink = (LinkButton)itemEquipment.FindControl("lbLink");
                        HtmlGenericControl rptLi = (HtmlGenericControl)itemEquipment.FindControl("rptLi");
                        HtmlGenericControl iStar = (HtmlGenericControl)itemEquipment.FindControl("iStar");

                        string link = lbLink.CommandArgument;
                        int pageno1 = Convert.ToInt32(lbLink.CommandName);
                        string url = string.Empty;
                        string host = Request.Url.Host;
                        string scheme = Request.Url.Scheme;
                        int portno = Request.Url.Port;

                        if (host == "localhost")
                        {
                            if (link.ToString().Contains("?"))
                            {
                                url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno1;
                            }
                            else
                            {
                                url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno1;
                            }
                        }
                        else
                        {
                            if (link.ToString().Contains("?"))
                            {
                                url = scheme + "://" + host + "/" + link + "&pageno=" + pageno1;
                            }
                            else
                            {
                                url = scheme + "://" + host + "/" + link + "?pageno=" + pageno1;
                            }
                        }

                        lbLink.Attributes.Add("href", url.ToString());


                        string path = Request.Url.AbsoluteUri;
                        int pos = path.LastIndexOf("=") + 1;
                        string pagenumber = path.Substring(pos, path.Length - pos);

                        if (lbLink.CommandName == pagenumber)
                        {
                            lbLink.Attributes.Add("class", "link active");
                            iStar.Style.Add("color", "#fff !important");

                            //rptLi.Style.Add("background-color", "#255282");
                            //lbLink.Style.Add("color", "#fff");
                            lbLink.Style.Add("color", "var(--primary-color)");
                            lbLink.Style.Add("font-weight", "bold");
                        }
                        else
                        {
                            lbLink.Attributes.Remove("active");
                        }
                    }
                }
                else
                {
                    divLinks.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "news.BindListViewNews-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lbLink_Click(object sender, EventArgs e)
    {
        LinkButton lbLink = (LinkButton)(sender);
        string link = lbLink.CommandArgument;
        int pageno = Convert.ToInt32(lbLink.CommandName);
        string url = string.Empty;
        string host = Request.Url.Host;
        string scheme = Request.Url.Scheme;
        int portno = Request.Url.Port;

        if (host == "localhost")
        {
            if (link.ToString().Contains("?"))
            {
                url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "&pageno=" + pageno;
            }
            else
            {
                url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno;
            }
        }
        else
        {
            if (link.ToString().Contains("?"))
            {
                url = scheme + "://" + host + "/" + link + "&pageno=" + pageno;
            }
            else
            {
                url = scheme + "://" + host + "/" + link + "?pageno=" + pageno;
            }
        }

        Response.Redirect(url);
    }
    //Ended By Pranita Hiradkar on 05/10/2021 TAB PATCH//
    protected void Img1_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/home.aspx");
        if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else
        {
            Response.Redirect("~/principalHome.aspx");
        }
    }
}
