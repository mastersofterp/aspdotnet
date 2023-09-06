using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class ConfigSiteMasterPage : System.Web.UI.MasterPage
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OrganizationController objOrg = new OrganizationController();

    private string uaims_constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    bool connection;

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

           

            string userid = objCommon.LookUp("LogFile", "ua_name", "ua_name='" + Session["username"].ToString() + "'");
           
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
                    Session["usertype"] == null || Session["userfullname"] == null)// || Session["coll_name"] == null)
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

                    //lblpath.Text = objCommon.GetPath(int.Parse(Request.QueryString["pageno"].ToString()));

                    //getPath(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                if (Session["payment"].ToString().Equals("payment"))
                {
                    string pageno = objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_LINK='Exam Registration'");
                    //lblpath.Text = objCommon.GetPath(int.Parse(pageno));
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
                Session["Reset"] = true;
                //Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                //********************

                //ADDED TODO LIST BY Deepali ON 24082020
                string userno_td = Convert.ToString(Session["userno"]);
                //BindToDolist(userno_td);

                //GetFbDetails();
                try
                {
                    if (Convert.ToInt32(Session["usertype"]) == 2 && Convert.ToString(Session["firstlog"]) == "False")
                    {
                       // Fill_menu_stud(mainMenu);
                    }
                    else
                    {
                        //Fill_menu(mainMenu);
                    }

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
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server UnAvailable");
                }
                GenerateMenuItem();  
                //ShowQuickLinks();
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }


    public void GenerateMenuItem()
    {
        // Get the data from database.  
        DataSet ds = objOrg.GetData();

        foreach (DataRow mainRow in ds.Tables[0].Rows)
        {
            // Load the records from the main table to the menu control.  
            MenuItem masterItem = new MenuItem(mainRow["AL_Link"].ToString());
            masterItem.NavigateUrl = mainRow["AL_URL"].ToString();
            Menu2.Items.Add(masterItem);

            //foreach (DataRow childRow in mainRow.GetChildRows("Child"))
            //{
            //    // According to the relation of the main table and the child table, load the data from the child table.  
            //    MenuItem childItem = new MenuItem((string)childRow["childName"]);
            //    childItem.NavigateUrl = childRow["childUrl"].ToString();
            //    masterItem.ChildItems.Add(childItem);
            //}
        }
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

    public void Fill_menu(Menu tvLinks)
    {

        SQLHelper objSH = new SQLHelper(uaims_constr);
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
            //Change Password for All
            //xx = new MenuItem();
            //xx.Text = "HOME";
            //xx.NavigateUrl = "~/home.aspx";
            //tvLinks.Items.Add(xx);
            int userno = int.Parse(Session["userno"].ToString());
            //Get all user links
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_UA_NO", userno);

            ///Added By Shrikant 11122017
            ///Code added for checking URL page no. and page link are valid
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
            ///

            //Get all user links
            drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_RET_USERLINKS", objParams);



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
                            zz = new MenuItem();    // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.NavigateUrl = "";
                            mastno = int.Parse(drLinks["mastno"].ToString());
                            xx.ChildItems.Add(zz);
                        }
                        else
                        {
                            yy = new MenuItem();
                            yy.Text = drLinks["al_link"].ToString();
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
                            yy = new MenuItem();
                            yy.Text = drLinks["al_link"].ToString();
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
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
        Response.Redirect("~/home.aspx");
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
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Response.Redirect("~/default.aspx");
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

   

    //ADDED TODO LIST BY Deepali ON 24082020

}
