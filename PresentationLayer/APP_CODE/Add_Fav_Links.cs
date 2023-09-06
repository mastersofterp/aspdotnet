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

/// <summary>
/// Summary description for Add_Fav_Links
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Add_Fav_Links : System.Web.Services.WebService {

    Common objCommon = new Common();
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    public Add_Fav_Links () {

    }

    public class Packet
    {
        public string res  { get; set; }
        public string msg  { get; set; }
        public string data { get; set; }
    }


    //Added Mahesh Malve on dated 09-09-2020
    [WebMethod]
    public string Check_InterNetConnction()
    {
        try
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead("http://www.google.com"))
            {
                var pkg = new Packet
                {
                    msg = "Success",
                };
                return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
            }
        }
        catch
        {
            var pkg = new Packet
            {
                msg = "Fail",
            };
            return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
        }

    }
    //Added Mahesh Malve on dated 09-09-2020

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Add_FavLink(int UA_NO, int LINK_NO, int OTYPE)
    {
        string retStatus = "";
        
        string SP_Name = "PKG_INS_USER_QLINKS_NEW";
        string SP_Parameters = "@P_UA_NO , @P_LINK_NO , @P_OTYPE , @P_OUT";
        string Call_Values = "" + UA_NO + "," + LINK_NO + "," + OTYPE + ",0";

        retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values,true);

        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = retStatus
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented); 
    }
    
    /// <summary>
    /// Created By SP
    /// </summary>
    /// <param name="UA_NO"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string CheckFBAuthorization(int ua_no,string accToken, string id)
    {
        string retStatus = "";

        string SP_Name = "PKG_USER_ACC_SP_UPD_FB_DETAILS";
        string SP_Parameters = "@P_UANO , @P_ACCT , @P_FBID , @P_IP,@P_OUT";
        string Call_Values = "" + ua_no + "," + accToken + "," + id + "," + HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString() + ",0";
        
        retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = retStatus
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Get_FavLink(int UA_NO)
    {
        string HtmlData = ShowQuickLinks(UA_NO);
        var pkg = new Packet
        {
            res  = "1",
            msg  = "Success",
            data = HtmlData
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented); 
    }

    public string ShowQuickLinks(int Ua_No)
    {
        string SP_Name = "PKG_GET_USER_QLINKS_NEW";
        string SP_Parameters = "@P_UA_NO";
        string Call_Values = "" + Ua_No + "";

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

//        string yourHTMLstring = @"<input type='text' class='live-search-box-1' placeholder='Search Here' />
//                                   <ul class='list-group live-search'>";
        string yourHTMLstring = "<ul class='list-group live-search'>";

        try
        {
            int HeadCount = 0;
            string HeadName = "";
            string LinkName = "";
            string Href = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["LINK_HEAD"].ToString() == HeadName || i == 0)
                {
                    HeadName = ds.Tables[0].Rows[i]["LINK_HEAD"].ToString();
                    LinkName = ds.Tables[0].Rows[i]["LINK_NAME"].ToString();
                    Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + ds.Tables[0].Rows[i]["LINK_URL"].ToString();

                    if (HeadCount == 0)
                    {
                        yourHTMLstring += @"<li class='list-group-item pnl1'><span><b><a data-toggle='collapse' data-parent='#accordion1' href='#ulli"+i+"'>" + HeadName + "</a></b></span><ul id='ulli"+i+"' class='collapse'>";
                        HeadCount++;
                    }
                    yourHTMLstring += @" <li style='margin-top:5px'>
                                             <i class='fa fa-link' aria-hidden='true' style='color:#3c8dbc'></i>&nbsp;&nbsp;<a href='"+Href+"'>" + LinkName + "";
                    yourHTMLstring += @" </a><i class='fa fa-trash DelHover pull-right' aria-hidden='true' style='color:red' onclick='DeleteFavLinks(this)'></i></li>";
                }
                else
                {
                    yourHTMLstring += @"
                                            </ul>
                                            </li>
                                        ";

                    HeadName = ds.Tables[0].Rows[i]["LINK_HEAD"].ToString();
                    LinkName = ds.Tables[0].Rows[i]["LINK_NAME"].ToString();
                    Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + ds.Tables[0].Rows[i]["LINK_URL"].ToString();

                    yourHTMLstring += @"<li class='list-group-item pnl1'><span><b><a data-toggle='collapse' data-parent='#accordion1' href='#ulli" + i + "'>" + HeadName + "</a></b></span><ul id='ulli" + i + "' class='collapse'>";

                    yourHTMLstring += @" <li style='margin-top:5px'>
                                             <i class='fa fa-link' aria-hidden='true' style='color:#3c8dbc'></i>&nbsp;&nbsp;<a href='"+Href+"'>" + LinkName + "";
                    yourHTMLstring += @" </a><i class='fa fa-trash DelHover pull-right' aria-hidden='true' style='color:red' onclick='DeleteFavLinks(this)'></i></li>";
                }

                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    yourHTMLstring += @"</ul>";
                }
            }
            return yourHTMLstring.Replace(System.Environment.NewLine, string.Empty);
        }
        catch (Exception ex)
        {
            return yourHTMLstring;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Get_Active_FavLink(int UA_NO)
    {
        DataSet ds = objCommon.FillDropDown("TBL_QUICKLINKS", "UA_QLINKS", "", "UA_NO=" + UA_NO + "","");
        string dt = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            dt = Convert.ToString(ds.Tables[0].Rows[0]["UA_QLINKS"]);
        }
        else
        {
            dt=null;
        }
        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = dt
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }

    // ResolveUrl for Getting Image Path
    public static string ResolveUrl(string originalUrl)
    {
        if (originalUrl == null)
            return null;

        // *** Absolute path - just return
        if (originalUrl.IndexOf("://") != -1)
            return originalUrl;

        // *** Fix up image path for ~ root app dir directory
        if (originalUrl.StartsWith("~"))
        {
            string newUrl = "";
            if (HttpContext.Current != null)
                newUrl = HttpContext.Current.Request.ApplicationPath +
                      originalUrl.Substring(1).Replace("//", "/");
            else
                // *** Not context: assume current directory is the base directory
                throw new ArgumentException("Invalid URL: Relative URL not allowed.");

            // *** Just to be sure fix up any double slashes
            return newUrl;
        }

        return originalUrl;
    }

    // Get Server URL
    public static string ResolveServerUrl(string serverUrl, bool forceHttps)
    {
        // *** Is it already an absolute Url?
        if (serverUrl.IndexOf("://") > -1)
            return serverUrl;

        // *** Start by fixing up the Url an Application relative Url
        string newUrl = ResolveUrl(serverUrl);

        Uri originalUri = HttpContext.Current.Request.Url;
        newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                 "://" + originalUri.Authority + newUrl;

        return newUrl;
    }

    //Todolist Added By Deepali on 24082020
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Add_TodoList(string TD_NAME, string UA_NO)
    {
        string retStatus = "";

        string SP_Name = "PKG_USER_SP_INS_TODOLIST";
        string SP_Parameters = "@P_TD_NAME,@P_UA_NO,@P_OUT";
        string Call_Values = "" + TD_NAME + "," + UA_NO + ",0";

        retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = retStatus
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }
    //Todolist Added By Deepali on 24082020
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Delete_TodoList(int UA_NO, string TD_NAME)
    {
        string retStatus = "";

        string SP_Name = "PKG_USER_SP_DEL_TODOLIST";
        string SP_Parameters = "@P_TD_NAME,@P_UA_NO,@P_OUT";
        string Call_Values = "" + TD_NAME + "," + UA_NO + ",0";

        retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = retStatus
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }

    //Todolist Added By Deepali on 24082020
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Check_TodoList(int UA_NO, string TD_NAME)
    {
        string retStatus = "";

        string SP_Name = "PKG_USER_SP_CHK_TODOLIST";
        string SP_Parameters = "@P_TD_NAME,@P_UA_NO,@P_OUT";
        string Call_Values = "" + TD_NAME + "," + UA_NO + ",0";

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
