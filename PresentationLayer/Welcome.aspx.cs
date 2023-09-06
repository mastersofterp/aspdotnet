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

public partial class homenew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Common objCommon = new Common();
        string colname = objCommon.LookUp("reff", "CollegeName", string.Empty);
        Session["coll_name"] = colname;
        Page.Title = Session["coll_name"].ToString();
    }
   

}
