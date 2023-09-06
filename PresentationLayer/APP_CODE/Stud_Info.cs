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

using Newtonsoft.Json.Converters;
using mastersofterp_MAKAUAT;
using System.Web;
using System.Web.UI;


/// <summary>
/// Summary description for Stud_Info
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Stud_Info : System.Web.Services.WebService {

     Common objCommon = new Common();
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
      string ipAddress = null;
    string macAddress = null;
    string emailid = null;

   

    public Stud_Info () {

    }
    public class Packet
    {
        public string res { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Add_StudentInfo(string ROLLNO, string STUDNAME, string ENROLLNO,string MOTHERNAME,string FATHERNAME)
    {
        
        string retStatus = "";

        string SP_Name = "PKG_STUDENT_SP_INS_STUDENT_WEBAPI";
        string SP_Parameters = "@P_ROLLNO,@P_STUDNAME,@P_ENROLLNO,@P_MOTHERNAME,@P_FATHERNAME,@P_OUT";
        string Call_Values = "" + ROLLNO + "," + STUDNAME + "," + ENROLLNO + "," + MOTHERNAME + "," + FATHERNAME + ",0";

        retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = retStatus
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }

    // Added Google Sign In on Date 22/06/2020 by Mr.Aman
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Add_UserGoogleInfo(string UA_NO, string IDTOKEN, string SLOGINDATA) //string PROFILE,, string SLOGINDATA
    {
        string retStatus = "";

        string SP_Name = "PKG_USER_SP_INS_GOOGLEINFO";
        string SP_Parameters = "@P_UANO,@P_IDTOKEN,@P_SLOGINDATA,@P_OUT";  //@P_SLOGINDATA,@P_PROFILE
        string Call_Values = "" + UA_NO + "," + IDTOKEN + "," + SLOGINDATA + ",0";  //" + SLOGINDATA + "  " + PROFILE + "
    
        retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);

        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = retStatus
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }
    // Added Google Sign In on Date 22/06/2020 by Mr.Aman
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Get_GoogleUserDetails(string SLOGINDATA)
    {
        string HtmlData = ShowUserDetails(SLOGINDATA);
        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = HtmlData
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }
    // Added Google Sign In on Date 22/06/2020 by Mr.Aman
    public string ShowUserDetails(string SLOGINDATA)
     {
        string SP_Name = "PKG_USER_SP_GET_GOOGLEINFO";
        string SP_Parameters = "@P_SLOGINDATA";
        string Call_Values = "" + SLOGINDATA + "";

        string   yourHTMLstring ="";
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        string txt_username = "";
        string txt_password = "";
        int UA_NO = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (Convert.ToInt32(ds.Tables[0].Rows[i]["UA_NO"].ToString()) == UA_NO || i == 0)
            {
                UA_NO = Convert.ToInt32(ds.Tables[0].Rows[i]["UA_NO"].ToString());
                txt_username = (ds.Tables[0].Rows[i]["UA_NAME"].ToString());
                txt_password = (ds.Tables[0].Rows[i]["UA_PWD"].ToString());
            }
        }

        return yourHTMLstring = txt_username;
    }


    //public string  ShowUserDetails_bak(string SLOGINDATA)
    //{
    //    string SP_Name = "PKG_USER_SP_GET_GOOGLEINFO";
    //    string SP_Parameters = "@P_SLOGINDATA";
    //    string Call_Values = "" + SLOGINDATA + "";

    //    string yourHTMLstring = "";
    //    DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

    //        string txt_username = "";
    //        string txt_password = "";
    //        int  UA_NO=0;
         
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            if (Convert.ToInt32(ds.Tables[0].Rows[i]["UA_NO"].ToString()) == UA_NO || i == 0)
    //            {
    //                UA_NO =Convert.ToInt32(ds.Tables[0].Rows[i]["UA_NO"].ToString());
    //                txt_username =(ds.Tables[0].Rows[i]["UA_NAME"].ToString());
    //                txt_password =(ds.Tables[0].Rows[i]["UA_PWD"].ToString());
    //                if (txt_username != "" && txt_password != "")
    //                {
    //                    try
    //                    {
    //                        User_AccController objUC = new User_AccController();

    //                        string username = txt_username.Replace("'", "");
    //                        //string password = Common.EncryptPassword(txt_password.Text.Trim().Replace("'", ""));
    //                        string password = txt_password.Replace("'", "");

    //                        int userno = -1;
    //                        int ATTEMPT = 0;

    //                        emailid = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL");
    //                        string ua_status = objCommon.LookUp("USER_ACC", "UA_STATUS", "UA_NAME='" + username + "' and UA_NAME IS NOT NULL");

    //                        if (ua_status != "")
    //                        {
    //                            if (ua_status != "1")
    //                            {
    //                                //if (objUC.ValidateLogin(username, password, out userno) == Convert.ToInt32((CustomStatus.ValidUser)))
    //                              //  {
    //                                    //Login Succeded
    //                                    UserAcc objUA = objUC.GetSingleRecordByUANo(UA_NO);

    //                                    if (objUA.UA_No != 0)
    //                                    {
    //                                        string user_no = objUA.UA_No.ToString();
    //                                        Session["userno"] = objUA.UA_No.ToString();
    //                                        Session["idno"] = Convert.ToInt32(objUA.UA_IDNo);
    //                                        Session["username"] = objUA.UA_Name;
    //                                        Session["usertype"] = objUA.UA_Type;
    //                                        Session["userfullname"] = objUA.UA_FullName;
    //                                        Session["dec"] = objUA.UA_Dec.ToString();
    //                                        Session["userdeptno"] = objUA.UA_DeptNo.ToString();
    //                                        Session["colcode"] = objCommon.LookUp("reff", "College_code", string.Empty);
    //                                        Session["firstlog"] = objUA.UA_FirstLogin;
    //                                        Session["UA_DESIG"] = objUA.UA_Desig;
    //                                        //Session["currentsession"] = "56";
    //                                        //Session["sessionname"] = "2013-14 II REG";

    //                                        Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSION_STATUS= 1 AND FLOCK=1");
    //                                        Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSION_STATUS= 1 AND FLOCK=1");
    //                                        Session["FeesSessionStartDate"] = "2014";
    //                                        Session["FeesSessionEndDate"] = "2015";

    //                                        //  ipAddress = Request.ServerVariables["REMOTE_HOST"];
    //                                        Session["ipAddress"] = ipAddress;
    //                                        //Session["macAddress"] = "0000000";
    //                                        //string macAddress = GetMacAddress(ipAddress);
    //                                        //  macAddress = GetMACAddress();
    //                                        Session["macAddress"] = macAddress;
    //                                        Session["userEmpDeptno"] = objUA.UA_EmpDeptNo.ToString();


    //                                        //  Help and Freshdesk Feedback CSS  added by Shubham STATRT  // 20/09/2019
    //                                        Session["FRESHDESK_STATUS"] = objUA.ua_Freshdesk_Status.ToString();
    //                                        //  Help and Freshdesk Feedback CSS  added by Shubham END  // 20/09/2019
    //                                        //Code for LogTable
    //                                        //=================

    //                                        string lastloginid = objCommon.LookUp("LOGFILE", "MAX(ID)", "UA_NAME='" + Session["username"].ToString() + "' and UA_NAME IS NOT NULL");
    //                                        if (lastloginid != "")
    //                                            Session["lastloginid"] = lastloginid.ToString();
    //                                        else
    //                                            Session["lastloginid"] = "0";

    //                                        string lastlogout = objCommon.LookUp("LOGFILE", "LOGOUTTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
    //                                        string Allowpopup = objCommon.LookUp("reff", "ALLOWLOGOUTPOPUP", "");


    //                                        //////FOR STORE MODULE
    //                                        Application["strrefmaindept"] = objCommon.LookUp("STORE_REFERENCE", "MDNO", "");

    //                                        string aa = Application["strrefmaindept"].ToString();
    //                                        if (Session["userno"] != null)
    //                                        {
    //                                            int count = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENTUSER", "count(*)", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
    //                                            if (count > 0)
    //                                            {
    //                                                Session["strdeptcode"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
    //                                                Session["strdeptuserlevel"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

    //                                                if (Session["strdeptcode"] != null)
    //                                                    Session["strdeptname"] = objCommon.LookUp("STORE_DEPARTMENT", "MDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"].ToString()));
    //                                                else
    //                                                    Session["strdeptname"] = null;
    //                                            }
    //                                        }
    //                                        //////STORE MODULE END
    //                                        LogFile objLF = new LogFile();
    //                                        objLF.Ua_Name = Session["username"].ToString();
    //                                        objLF.LoginTime = DateTime.Now;


    //                                        int a = objUC.AddtoLogTran(Session["username"].ToString(), ipAddress, macAddress, Convert.ToDateTime(DateTime.Now));
    //                                        Session["loginid"] = a.ToString();

    //                                        if (Convert.ToString(Session["firstlog"]) == "False")   //Convert.ToInt32(Session["usertype"]) == 2 && 
    //                                        {
    //                                            //   Response.Redirect("~/changePassword.aspx?IsReset=1");

    //                                        }
    //                                        else
    //                                        {
    //                                            if (lastlogout == "" && Allowpopup == "1")
    //                                            {
    //                                                //  Response.Redirect("~/SignoutHold.aspx", false);
    //                                            }
    //                                            else if (Session["userno"].ToString() == "1")
    //                                            {
    //                                                //  Response.Redirect("~/DashBoard_Home.aspx", false);
    //                                            }
    //                                            else
    //                                            {
    //                                                //  Response.Redirect("~/home.aspx", false);
    //                                            };

    //                                        }
    //                                    }
    //                                    else
    //                                    {
    //                                        //txt_username = string.Empty;
    //                                        //  objCommon.DisplayMessage("Login Failed !, Please Check Your Username Or Password !",);

    //                                    }
    //                                //}
    //                                //else
    //                                //{
    //                                //    ATTEMPT = Convert.ToInt32(objCommon.LookUp("reff", "ATTEMPT", ""));
    //                                //    if (ua_status == "1")
    //                                //    {
    //                                //        string subject = "MIS Login Credentials";
    //                                //        string message = "Due to the unsucessfully  " + ATTEMPT + " login attempt ,your MIS account is blocked. Please contact system administrator!";
    //                                //        objCommon.sendEmail(message, emailid, subject);
    //                                //        //  objCommon.DisplayMessage(this.UpdatePanel1,"Your MIS account is blocked.Please Contact system administrator!", this.Page);
    //                                //        //  lblStatus.Text = "Your MIS account is blocked.Please Contact system administrator!";
    //                                //    }
    //                                //    else
    //                                //    {

    //                                //        //ipAddress = Request.ServerVariables["REMOTE_HOST"];
    //                                //        //macAddress = GetMACAddress();
    //                                //        //b = objUC.Failedlogin(username, ipAddress, macAddress, Convert.ToDateTime(DateTime.Now));
    //                                //        //objCommon.DisplayMessage(this.UpdatePanel1,"Login Failed !, Please Check Your Username Or Password !Only " + (ATTEMPT - b) + " attempt left!!", this.Page);
    //                                //        //lblStatus.Text = "Login Failed !, Please Check Your Username Or Password !Only " + (ATTEMPT - b) + " attempt left!!";
    //                                //    }
    //                                //}
    //                            }
    //                            else
    //                            {

    //                                //  objCommon.DisplayMessage("Your MIS account is blocked.Please Contact system administrator!");
    //                                //  lblStatus.Text = "Your MIS account is blocked.Please Contact system administrator!";
    //                            }
    //                        }
    //                        else
    //                        {
    //                            //txt_username.Text = string.Empty;
    //                            //objCommon.DisplayMessage(this.UpdatePanel1,"Login Failed !, Please Check Your Username!", this.Page);
    //                            //lblStatus.Text = "Login Failed !, Please Check Your Username!";

    //                        }

    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        //objCommon.DisplayMessage(this.UpdatePanel1,"Login Failed !, Please Check Your Username Or Password !", this.Page);
    //                    }
    //                }
    //            }            
    //    }
    //        return yourHTMLstring;
    //}



    // ResolveUrl for Getting Image Path Added for Google Sign In on Date 22/06/2020 by Mr.Aman
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

}
