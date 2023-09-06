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

using IITMS.UAIMS;

public partial class notauthorized : System.Web.UI.Page
{
    Common objCommon = new Common();
    public string username = string.Empty;
    
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
            Session["usertype"] == null || Session["userfullname"] == null)
        {

            Response.Redirect("~/default.aspx?");
        }
        else
        {

            username = Session["userfullname"].ToString().ToUpper();
        }

        username = Session["userfullname"].ToString().ToUpper();

        if (Request.QueryString["page"] != null)
            lblMsg.Text = "You Are Not Authorized To Use <b><u>" + Request.QueryString["page"].ToString() + "</u></b> Page";
    }
}
