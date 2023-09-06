using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DynamicAL_v2;
using System.Web.Services;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PageBasedWebMethod : System.Web.UI.Page
{
    Common objCommon = new Common();
    DynamicControllerAL AL = new DynamicControllerAL();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region WebMethod
    [WebMethod]
    public static string GetShortCut(string sc)
    {
        PageBasedWebMethod sm = new PageBasedWebMethod();
        string PageNo = sm.objCommon.LookUp("ACCESS_LINK", "AL_No", "SHORTCUT_KEY='" + sc + "'");
        DataTable dt = new DataTable();
        dt.Columns.Add("PageNo");
        dt.Rows.Add(PageNo);
        DynamicControllerAL AL = new DynamicControllerAL();
        return AL.Dt2Json(dt);
    }

    [WebMethod]
    public static string GetQuestions(string input)
    {
        PageBasedWebMethod sm = new PageBasedWebMethod();
        DynamicControllerAL AL = new DynamicControllerAL();

        DataSet ds = AL.InlineQuery("SELECT * FROM CHAT_BOT WHERE question LIKE '%" + input + "%'");
        return AL.Dt2Json(ds.Tables[0]);
    }
    #endregion
}