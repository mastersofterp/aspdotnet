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

using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;

using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Text;


using System.Web.Services;
using System.Web.Script.Serialization;


public partial class home : System.Web.UI.Page
{
    Common objCommon = new Common();
    public string sMarquee = string.Empty;
    public string Notice = string.Empty;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        ////To Set the MasterPage
        //if (Session["masterpage"] != null)
        //    objCommon.SetMasterPage(Page, "RFC_CONFIG/ConfigSiteMasterPage.master");
        //else
        //    objCommon.SetMasterPage(Page, "RFC_CONFIG/ConfigSiteMasterPage.master");

        // Set MasterPage
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
            Response.Redirect("~/default.aspx");
        }

    }

}
