using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Web.Security;

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.SessionState;
using IITMS.UAIMS.BusinessLayer;

using System.Data.SqlClient;
using IITMS;
//using IITMS.NITPRM;
//using IITMS.NITPRM.BusinessLayer.BusinessEntities;
//using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


/// <summary>
/// Summary description for Autocomplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Autocomplete : System.Web.Services.WebService
{
    Common objCommon = new Common();
    public Autocomplete()
    {
    }

    

}