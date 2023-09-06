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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class SignoutHold : System.Web.UI.Page
{
    Common objCommon = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        string duration = objCommon.LookUp("Reff", "POPUPDURATION", "");
        if (!SM1.IsInAsyncPostBack)
            Session["timeout"] = DateTime.Now.AddSeconds(Convert.ToInt32(duration)).ToString();

        if (lblTimer.Text == "0")
        {
            Response.Redirect("~/home.aspx", false);
        }
        //string loginlast = objCommon.LookUp("LOGFILE", "LOGINTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
        //lbllogintime.Text = loginlast.ToString();
      
    }
    protected void timer1_tick(object sender, EventArgs e)
    {
        if (0 > DateTime.Compare(DateTime.Now,
       DateTime.Parse(Session["timeout"].ToString())))
        {
            lblTimer.Text =
            ((Int32)DateTime.Parse(Session["timeout"].
            ToString()).Subtract(DateTime.Now).Seconds).ToString();
        }
        
        
    }
}
