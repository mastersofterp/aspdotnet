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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Report_ArearsCalculationNEW : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                  Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                ViewState["IDNO"] = "0";
            }

        }
        
    }
    protected void btncalenergy_Click(object sender, EventArgs e)
    {
        try
        {

        }

        catch
        {

        }




    }
    protected void btncalwater_Click(object sender, EventArgs e)
    {
        try
        {
        }

        catch
        {

        }

    }
}
