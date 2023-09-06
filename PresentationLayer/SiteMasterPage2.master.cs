using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class SiteMasterPage2 : System.Web.UI.MasterPage
{
    //Common objCommon = new Common();
    UAIMS_Common objCommon = new UAIMS_Common();
    private string uaims_constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Very Important
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Left Link height
            int ht = Convert.ToInt32(Session["screenheight"]);
            master_left1.Height = Unit.Pixel(ht - (ht * 22) / 100);

            lblLink.Text = "Welcome " + Session["username"].ToString().ToUpper();

            Page.Title = Session["coll_name"].ToString();
            lblColName.Text = Session["coll_name"].ToString();

            try
            {                
                //Fill the Menu with Links
                Fill_MenuLinks(myMenu, uaims_constr);
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "SiteMasterPage2.Page_Load-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    public void Fill_MenuLinks(Menu myMenu, string constr)
    {
        SQLHelper objSH = new SQLHelper(uaims_constr);
        SqlDataReader dr = null;
        SqlDataReader drLinks = null;

        int linkno = 0;
        int mastno = 0;
        int url_idno = 0;
        MenuItem xx = null;
        MenuItem yy = null;
        MenuItem zz = null;

        try
        {
            int userno = int.Parse(Session["userno"].ToString());

            //Get all user links
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_UA_NO", userno);

            //Get all user links
            drLinks = objSH.ExecuteReaderSP("PKG_TREEVIEW_SP_RET_USERLINKS", objParams);

            //loop thru links
            while (drLinks.Read())
            {
                if (drLinks["al_link"].ToString().Trim() != string.Empty)
                {
                    if (drLinks["url_idno"] != null & drLinks["url_idno"].ToString() != string.Empty)
                        url_idno = int.Parse(drLinks["url_idno"].ToString());

                    if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty & int.Parse(drLinks["al_asno"].ToString()) != linkno)
                    {
                        xx = new MenuItem();  // this is defination of the node.
                        xx.Text = drLinks["as_title"].ToString();
                        xx.NavigateUrl = "#";

                        // adding node to root
                        myMenu.Items.Add(xx);

                        if (drLinks["al_url"].ToString().Trim() == string.Empty)
                        {
                            zz = new MenuItem();    // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            zz.NavigateUrl = "#";
                            mastno = int.Parse(drLinks["mastno"].ToString());
                            // adding node as child of node xx.
                            xx.ChildItems.Add(zz);
                        }
                        else
                        {
                            yy = new MenuItem();
                            yy.Text = drLinks["al_link"].ToString();                                

                            if (drLinks["summer"] != null & drLinks["summer"].ToString() != string.Empty)
                                if (int.Parse(drLinks["summer"].ToString()) == 1) yy.NavigateUrl = drLinks["al_url"].ToString() + "?idno=2";
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
                                    
                                    yy.ToolTip = qry;
                                }

                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    // adding node as child of node xx.
                                    zz.ChildItems.Add(yy);
                                }
                                else
                                {
                                    // adding node as child of node xx.
                                    xx.ChildItems.Add(yy);
                                }
                            }
                            else
                            {
                                // adding node as child of node xx.
                                xx.ChildItems.Add(yy);
                            }
                        }
                    }
                    else
                    {
                        if (drLinks["al_url"].ToString() == string.Empty)
                        {
                            zz = new MenuItem();   // this is defination of the node.
                            zz.Text = drLinks["al_link"].ToString();
                            if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
                                mastno = int.Parse(drLinks["mastno"].ToString());

                            // adding node as child of node xx.
                            xx.ChildItems.Add(zz);
                        }
                        else
                        {
                            yy = new MenuItem();
                            yy.Text = drLinks["al_link"].ToString();
                            //yy.ID =  Val(rs1.Fields("al_no").Value & string.Empty)

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
                                    yy.ToolTip = qry;
                                }
                            }
                            else
                            {
                                string qry = string.Empty;
                                if (drLinks["al_url"].ToString().Contains("?"))
                                    qry = drLinks["al_url"].ToString() + "&pageno=" + drLinks["al_no"].ToString();
                                else
                                    qry = drLinks["al_url"].ToString() + "?pageno=" + drLinks["al_no"].ToString();

                                yy.ToolTip = qry;
                            }

                            if ((drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty))
                            {
                                if (int.Parse(drLinks["mastno"].ToString()) == mastno & int.Parse(drLinks["mastno"].ToString()) != 0)
                                {
                                    // adding node as child of node xx.
                                    zz.ChildItems.Add(yy);
                                }
                                else
                                {
                                    // adding node as child of node xx.
                                    xx.ChildItems.Add(yy);
                                }
                            }
                            else
                            {
                                // adding node as child of node xx.
                                xx.ChildItems.Add(yy);
                            }
                        }
                    }
                }

                if (drLinks["al_asno"] != null & drLinks["al_asno"].ToString() != string.Empty)
                    linkno = int.Parse(drLinks["al_asno"].ToString());
                if (drLinks["mastno"] != null & drLinks["mastno"].ToString() != string.Empty)
                    mastno = int.Parse(drLinks["mastno"].ToString());
            }

            //Change Password for All
            xx = new MenuItem();
            xx.Text = "Change Password";
            //xx.Target = "main";
            xx.NavigateUrl = "changepassword.aspx?pageno=500";
            myMenu.Items.Add(xx);

            //Log Out for All
            xx = new MenuItem();
            xx.Text = "Logout";
            //xx.Target = "_parent";
            xx.NavigateUrl = "logout.aspx";
            myMenu.Items.Add(xx);            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "SiteMasterPage.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            //close all objects
            if (drLinks != null) drLinks.Close();
            if (dr != null) dr.Close();
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

    protected void myMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        //get the page link from tooltip of the selected node, so that we can redirect to it.
        string link = string.Empty;
        link = myMenu.SelectedItem.ToolTip;
        Response.Redirect("~/" + link.ToString());
    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/home.aspx");
    }
}
