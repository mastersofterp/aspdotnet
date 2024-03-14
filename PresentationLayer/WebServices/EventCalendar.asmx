<%@ WebService Language="C#" Class="EventCalendar" %>

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using Newtonsoft.Json;
using BusinessLogicLayer.BusinessLogic;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class EventCalendar  : System.Web.Services.WebService {
    
    EventCalendarController objevent = new EventCalendarController();
    Common objCommon = new Common();
   

    [WebMethod(EnableSession = true)]
    public string SpecialEvent()
    {
        var jsonstring = string.Empty;
        DataSet ds = new DataSet();
        int UANO = Convert.ToInt32(Session["userno"].ToString());
        ds = objevent.GetAllEventList(UANO);

        if (ds.Tables[0].Rows.Count > 0)
        {
            jsonstring = JsonConvert.SerializeObject(ds.Tables[0]);
        }
        return jsonstring;
    }
    
}

