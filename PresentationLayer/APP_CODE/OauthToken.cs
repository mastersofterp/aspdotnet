using DynamicAL_v2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for OauthToken
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class OauthToken : System.Web.Services.WebService {
    DynamicControllerAL AL = new DynamicControllerAL();
    public OauthToken () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public class Packet
    {
        public string res { get; set; }
        public string msg { get; set; }
        public string access_token { get; set; }
        public string created_dnt { get; set; }
        public string expiry_dnt { get; set; }
        public string time_limit { get; set; }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GenerateToken(string UserName, int TimeLimit)
    {
        Packet pkg;
        string encryPass = mastersofterp_MAKAUAT.clsTripleLvlEncyrpt.ThreeLevelEncrypt(UserName);
        DateTime created_dnt = DateTime.Now;
        DateTime expiry_dnt = created_dnt.AddMinutes(TimeLimit);
        if (encryPass != "-99")
        {
            pkg = new Packet
            {
                res = "1",
                msg = "Success",
                access_token = encryPass,
                created_dnt = created_dnt.ToString("yyyy-MM-dd HH:mm:ss"),
                expiry_dnt = expiry_dnt.ToString("yyyy-MM-dd HH:mm:ss"),
                time_limit = TimeLimit.ToString()
            };
        }
        else
        {
            pkg = new Packet
            {
                res = "0",
                msg = "Error",
                access_token = encryPass
            };
        }

        string ConStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        string retVal = "";
        string SP = "PKG_CRUD_TOKEN_MANAGEMENT";
        string PM = "@P_UA_NAME, @P_ACC_TOKEN, @P_CREATED_DNT, @P_EXPIRY_DNT, @P_TIME_LIMIT, @P_DEVICE_TYPE, @P_OPERATION";
        string VL = "" + UserName + "," + encryPass + "," + created_dnt.ToString("yyyy-MM-dd HH:mm:ss") + "," + expiry_dnt.ToString("yyyy-MM-dd HH:mm:ss") + "," + TimeLimit.ToString() + ",PC,1";
        retVal = AL.DynamicSPCall_IUD(SP, PM, VL, true, 2);

        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }
}
