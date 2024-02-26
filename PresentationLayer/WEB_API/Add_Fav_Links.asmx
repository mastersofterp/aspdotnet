<%@ WebService Language="C#" CodeBehind="~/APP_CODE/Add_Fav_Links.cs" Class="Add_Fav_Links" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;


public class Packet
{
    public string res { get; set; }
    public string msg { get; set; }
    public string data { get; set; }
}

public class Add_Fav_Links : System.Web.Services.WebService
{
    Common objCommon = new Common();

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Add_FavLink(int UA_NO, int LINK_NO, int OTYPE)
    {
        string retStatus = "";

        string SP_Name = "PKG_INS_USER_QLINKS_NEW";
        string SP_Parameters = "@P_UA_NO , @P_LINK_NO , @P_OTYPE , @P_OUT";
        //string Call_Values = "{UA_NO},{LINK_NO},{OTYPE},0";
        string Call_Values = "" + UA_NO + "," + LINK_NO + "," + OTYPE + ",0";

        retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = retStatus
        };

        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }
}
