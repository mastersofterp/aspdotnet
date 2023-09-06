using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using BusinessLogicLayer.BusinessEntities;

/// <summary>
/// Summary description for NotificationComponent
/// </summary>
public class NotificationComponent
{

    bool connection;
    //public NotificationComponent()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentTime"></param>


    public void RegisterNotification(DateTime currentTime)
    {

        string conStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        string sqlCommand = @"SELECT RECEIVER_UA_NO FROM [dbo].[tblNOTIFICATION] where ISREAD=0";
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(sqlCommand, con);
            cmd.Parameters.AddWithValue("ISREAD", 0);
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            cmd.Notification = null;
            SqlDependency sqlDep = new SqlDependency(cmd);
            sqlDep.OnChange += sqlDep_OnChange;
            //we must have to execute the command here
            //using (SqlDataReader reader = cmd.ExecuteReader())
            //{
            //    // nothing need to add here now
            //}
        }

    }

    //Added by Deepali on 14/08/2020
    private bool CheckConnection()
    {
        WebClient client = new WebClient();
        byte[] datasize = null;
        try
        {
            datasize = client.DownloadData("http://www.google.com");
            connection = true;
        }
        catch (Exception ex)
        {
            connection = false;
        }
        return connection;
    }

    void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
    {
        //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record
        if (e.Type == SqlNotificationType.Change)
        {
            SqlDependency sqlDep = sender as SqlDependency;
            sqlDep.OnChange -= sqlDep_OnChange;

            //from here we will send notification message to client
            var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            notificationHub.Clients.All.notify("added");
            //re-register notification
            RegisterNotification(DateTime.Now);
        }
    }

    //////////////////// for android 31 03 2020////////////////////////

    public bool SendToFCMServer(List<NotificationEntity> regIds, NotificationAndroid notificationAndroid, int time)
    {
        try
        {
            // Time must be between

            string appId = "798833055907";
            string serverKey = "AAAAuf4pGKM:APA91bF47Ue1OXrgn2iOhjLhaVvQyfS4ekPiB_UJf-KS1nibFQNAQedUWscQgWc4givIFFGrm88jFm7rrAWayarPYNY-5ePTelzga6UyUWjSqcvwHhX7foamsMxkjWDCwY9Wtog1krJv";

            WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            webRequest.Method = "post";
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            webRequest.Headers.Add(string.Format("Sender: id={0}", appId));

            string csvIds = string.Join(",", from item in regIds select item.RegID);
            string list = csvIds;
            string newList = string.Join(",", list.Split(',').Select(x => string.Format("'{0}'", x)).ToList());
            string abc = newList.Replace("'", "\"");
            string json = JsonConvert.SerializeObject(notificationAndroid);
            string customData = "\"Message\":" + json;
            string postData = "{\"time_to_live\":" + time + ",\"delay_while_idle\":true,\"data\": {" + customData + "},\"registration_ids\":[" + abc + " ]}";
            //string postData = "{\"time_to_live\":" + time + ",\"delay_while_idle\":true,\"data\": " + customData + ",\"registration_ids\":[\"cTJXroKBMUI:APA91bEcNlkCk7gvUoOgMeZoGz-Zj5AeiVRAQaEM3FuNXsTvKBLNbdQjgysmBO21RbVDlgvcr9mZS9QsYhmVPE41UwyOisiao3DCqljwcoaS7BNyvZunXSHLi6fPpnNigUyPJF98qnmC\"]}";

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            webRequest.ContentLength = byteArray.Length;

            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            Uri a = webRequest.RequestUri;
            WebResponse tResponse = webRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
