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
/// <summary>
/// Summary description for Feedback
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Feedback : System.Web.Services.WebService {
    Common objCommon = new Common();
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    public Feedback () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public class Packet
    {
        public string res { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string Feed_back(int UA_NO, string LINK_NO, int FEEDID)
    {
        string retStatus = "";

        string SP_Name = "PKG_INS_USER_ANNOUNCEMENT_FEEDBACK_NEW";
        string SP_Parameters = "@P_UA_NO , @P_FEEDBACK ,@P_ANNOUNCE_ID, @P_OUT";
        string Call_Values = "" + UA_NO + "," + LINK_NO + "," + FEEDID + ",0";

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
    public string Get_FeedBackink(int UA_NO, int USER)
    {
        string HtmlData = ShowFeedBack(UA_NO, USER);
        var pkg = new Packet
        {
            res = "1",
            msg = "Success",
            data = HtmlData
        };
        return JsonConvert.SerializeObject(pkg, Newtonsoft.Json.Formatting.Indented);
    }
    public string ShowFeedBack(int Ua_No, int User)
    {
        string SP_Name = "PKG_GET_USER_FEEDBACK_NEW";
        string SP_Parameters = "@P_UA_NO";
        string Call_Values = "" + Ua_No + "";

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        //string Feed_SP_Name = "PKG_GET_USER_FEEDBACK_NEW_IMAGE";
        //string Feed_SSP_Parameters = "" + "@P_UA_NO" + "," + "@P_ID" + "";
        //string Feed_SCall_Values = "" + Ua_No + "," + annouce_id + "";

        //DataSet ds1 = objCommon.DynamicSPCall_Select(Feed_SP_Name, Feed_SSP_Parameters, Call_Values);

        //        string yourHTMLstring = @"<input type='text' class='live-search-box-1' placeholder='Search Here' />
        //                                   <ul class='list-group live-search'>";
        string yourHTMLstring = "";

        try
        {
            int HeadCount = 0;
            string HeadName = "";
            string LinkName = "";
            string date = "";
            string feature = "";
            string Href = "";
            string annouce_id = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                annouce_id = ds.Tables[0].Rows[i]["ANNOUNCE_ID"].ToString();
                if (ds.Tables[0].Rows[i]["ANNOUNCE_TITLE"].ToString() == HeadName || i == 0)
                {
                    feature = ds.Tables[0].Rows[i]["FEATURE"].ToString();
                    date = ds.Tables[0].Rows[i]["PUBLISH_DATE"].ToString();
                    HeadName = ds.Tables[0].Rows[i]["ANNOUNCE_TITLE"].ToString();
                    LinkName = ds.Tables[0].Rows[i]["ANNOUCE_DETAILS"].ToString();
                    
                    Href =  ds.Tables[0].Rows[i]["FILENAME"].ToString();

                    if (HeadCount == 0)
                    {
                        Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds.Tables[0].Rows[i]["FILENAME"].ToString();
                        yourHTMLstring += @"<div class='feature'> <div>  <div><span class='featurenew'>" + feature + "</span><span class='featureDate'>" + date + "</span></div><div class='featureTitle'><h3 class='featureTitle-h3'>" + HeadName + "</h3></div><div class='featureContent'>" + LinkName + "</div><div class='row'>";
                        if (ds.Tables[0].Rows[i]["FILENAME"].ToString() == "NULL")
                        {
                            yourHTMLstring += @"</div></div><div class='emojis'>";
                        }
                        else 
                        {
                            yourHTMLstring += @"<div class='col-md-1 col-xs-4 col-sm-4  thumb'><img class='modal-hover-opacity' src='" + Href + "' onclick='onClick(this)'/></div></div></div><div class='emojis'>";
                        }
                            //yourHTMLstring += @"</div>";
                            HeadCount++;
                        }
                    DataSet ds1 = objCommon.FillDropDown("ACD_FEEDBACK_MASTER", "FEED_ID", "UA_NO,FEEDBACK_TITLE,	FEEDBACK_FILE,FILENAME", "", "");
                    DataSet ds3 = objCommon.FillDropDown("ACD_ANNOUNCEMENT_MASTER", "ANNOUNCE_ID", "FILENAME", "ANNOUNCE_ID=" + annouce_id, "");
                    DataSet ds2 = objCommon.FillDropDown("ACD_ANNOUNCMENT_FEEDBACK_DETAILS", "FEEDBACKID", "USER_FEEDBACK", "CANCEL_FEEDBACK = 0 AND ANNOUNCE_ID = " + annouce_id + "AND UA_NO=" + User, "");       
                    for (int J = 0; J < ds1.Tables[0].Rows.Count; J++)
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[J]["FEED_ID"].ToString() == ds2.Tables[0].Rows[0]["USER_FEEDBACK"].ToString())
                            {
                                string Emoji = "";
                                Emoji = ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                string value = ds1.Tables[0].Rows[J]["FEED_ID"].ToString() + "," + ds3.Tables[0].Rows[0]["ANNOUNCE_ID"].ToString();
                                string title = ds1.Tables[0].Rows[J]["FEEDBACK_TITLE"].ToString() + "," + HeadName.ToString();
                                Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                yourHTMLstring += @"<img class='emojiContainer active' src='" + Href + "'  Width='38px' Height='30px' value='" + value + "' title='" + title + "'/>";
                            }
                            else
                            {
                                string Emoji = "";
                                Emoji = ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                string value = ds1.Tables[0].Rows[J]["FEED_ID"].ToString() + "," + ds3.Tables[0].Rows[0]["ANNOUNCE_ID"].ToString();
                                string title = ds1.Tables[0].Rows[J]["FEEDBACK_TITLE"].ToString() + "," + HeadName.ToString();
                                Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                yourHTMLstring += @"<img class='emojiContainer' src='" + Href + "'  Width='38px' Height='30px' value='" + value + "' title='" + title + "'/>";
                            }
                        }
                        else
                        {
                            string Emoji = "";
                            Emoji = ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                            string value = ds1.Tables[0].Rows[J]["FEED_ID"].ToString() + "," + ds3.Tables[0].Rows[0]["ANNOUNCE_ID"].ToString();
                            string title = ds1.Tables[0].Rows[J]["FEEDBACK_TITLE"].ToString() + "," + HeadName.ToString();
                            Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                            yourHTMLstring += @"<img class='emojiContainer' src='" + Href + "'  Width='38px' Height='30px' value='" + value + "' title='" + title + "'/>";
                        }
                    }
                    yourHTMLstring += @"</div></div><input type='hidden' value='" + annouce_id + "'/></div>";
                            
                }
                else
                {
                    feature = ds.Tables[0].Rows[i]["FEATURE"].ToString();
                    date = ds.Tables[0].Rows[i]["PUBLISH_DATE"].ToString();
                    HeadName = ds.Tables[0].Rows[i]["ANNOUNCE_TITLE"].ToString();
                    LinkName = ds.Tables[0].Rows[i]["ANNOUCE_DETAILS"].ToString();
                    Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds.Tables[0].Rows[i]["FILENAME"].ToString();
                    yourHTMLstring += @"<div class='feature'> <div> <div><span class='featurenew'>" + feature + "</span><span class='featureDate'>" + date + "</span></div><div class='featureTitle'><h3 class='featureTitle-h3'>" + HeadName + "</h3></div><div class='featureContent'>" + LinkName + "</div><div class='row'>";

                    if (ds.Tables[0].Rows[i]["FILENAME"].ToString() == "NULL")
                    {
                        yourHTMLstring += @"</div></div><div class='emojis'>";
                    }
                    else
                    {
                        yourHTMLstring += @"<div class='col-md-1 col-xs-4 col-sm-4  thumb'><img class='modal-hover-opacity' src='" + Href + "' onclick='onClick(this)'/></div></div></div><div class='emojis'>";
                    }

                            //yourHTMLstring += @"</div>";
                            HeadCount++;
                            DataSet ds1 = objCommon.FillDropDown("ACD_FEEDBACK_MASTER", "FEED_ID", "UA_NO,FEEDBACK_TITLE,	FEEDBACK_FILE,FILENAME", "", "");
                            DataSet ds3 = objCommon.FillDropDown("ACD_ANNOUNCEMENT_MASTER", "ANNOUNCE_ID", "FILENAME", "ANNOUNCE_ID=" + annouce_id, "");
                            DataSet ds2 = objCommon.FillDropDown("ACD_ANNOUNCMENT_FEEDBACK_DETAILS", "FEEDBACKID", "USER_FEEDBACK", "CANCEL_FEEDBACK = 0 AND ANNOUNCE_ID = " + annouce_id + "AND UA_NO=" + User, "");   
                    
                    for (int J = 0; J < ds1.Tables[0].Rows.Count; J++)
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[J]["FEED_ID"].ToString() == ds2.Tables[0].Rows[0]["USER_FEEDBACK"].ToString())
                            {
                                string Emoji = "";
                                Emoji = ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                string value = ds1.Tables[0].Rows[J]["FEED_ID"].ToString() + "," + ds3.Tables[0].Rows[0]["ANNOUNCE_ID"].ToString();
                                string title = ds1.Tables[0].Rows[J]["FEEDBACK_TITLE"].ToString() + "," + HeadName.ToString();
                                Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                yourHTMLstring += @"<img class='emojiContainer active' src='" + Href + "'  Width='38px' Height='30px' value='" + value + "' title='" + title + "'/>";
                            }
                            else
                            {
                                string Emoji = "";
                                Emoji = ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                string value = ds1.Tables[0].Rows[J]["FEED_ID"].ToString() + "," + ds3.Tables[0].Rows[0]["ANNOUNCE_ID"].ToString();
                                string title = ds1.Tables[0].Rows[J]["FEEDBACK_TITLE"].ToString() + "," + HeadName.ToString();
                                Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                                yourHTMLstring += @"<img class='emojiContainer' src='" + Href + "'  Width='38px' Height='30px' value='" + value + "' title='" + title + "'/>";
                            }
                        }
                        else
                        {
                            string Emoji = "";
                            Emoji = ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                            string value = ds1.Tables[0].Rows[J]["FEED_ID"].ToString() + "," + ds3.Tables[0].Rows[0]["ANNOUNCE_ID"].ToString();
                            string title = ds1.Tables[0].Rows[J]["FEEDBACK_TITLE"].ToString() + "," + HeadName.ToString();
                            Href = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/UPLOAD_FILES/ANNOUNCEMENT/" + ds1.Tables[0].Rows[J]["FILENAME"].ToString();
                            yourHTMLstring += @"<img class='emojiContainer' src='" + Href + "'  Width='38px' Height='30px' value='" + value + "' title='" + title + "'/>";
                        }
                        
                    }
                    yourHTMLstring += @"</div></div></div>";
                    HeadName = "";
                }

                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    yourHTMLstring += @"";
                }
            }
            return yourHTMLstring.Replace(System.Environment.NewLine, string.Empty);
        }
        catch (Exception ex)
        {
            return yourHTMLstring;
        }
    }
}
