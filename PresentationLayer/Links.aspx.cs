using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;




public partial class Links : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Load

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                //int alno = 0;
                //int uano = 0;
                //int mastno = 0;

                //if (Request.QueryString["PgNo"] != null)
                //{
                //    alno = Convert.ToInt32(Request.QueryString["PgNo"].ToString());
                //}

                //if (Request.QueryString["UaNo"] != null)
                //{
                //    uano = Convert.ToInt32(Request.QueryString["UaNo"].ToString());
                //}

                //if (Request.QueryString["MastNo"] != null)
                //{
                //    mastno = Convert.ToInt32(Request.QueryString["MastNo"].ToString());
                //}

                //BindLinks(uano, alno, mastno);
            }
        }

        //Set the Page Title
        Page.Title = Session["coll_name"].ToString();
        divMsg.InnerHtml = string.Empty;
    }

    #endregion

    //private void BindLinks(int uano, int alno, int mastno)
    //{
    //    try
    //    {
    //        DataSet dsLink = objCommon.GetUserSubLinks(uano, alno, mastno);

    //        if (dsLink.Tables[0].Rows.Count > 0)
    //        {
    //            repLinks.DataSource = dsLink;          
    //            repLinks.DataBind();
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
    //            //url = link + "&pageno=" + pageno.ToString();
    //        }
    //        else
    //        {
    //            url = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + link + "?pageno=" + pageno;
    //            //url = link + "?pageno=" + pageno.ToString();
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

    
}
