using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using IITMS.SQLServer.SQLDAL;
using System.Web.Services;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;


public partial class Notification : System.Web.UI.Page
{
    public static SQLHelper sqlhelper = null;

    Common objCommon = new Common();
    SessionActivityController sac = new SessionActivityController();

    protected void Page_Load(object sender, EventArgs e)
    {
        details();
    }

    private int details()
    {
        int userno = Convert.ToInt32(Session["userno"]);
        return userno;
    }
   

    [WebMethod]
    public static int GetNotificationCount()
    {
        Notification notification = new Notification();
        int userno = notification.details();
       
        SessionActivityController sac = new SessionActivityController();
        //var data= notification.details();
        Common objCommon1 = new Common();
        //int ua_type = Convert.ToInt32(objCommon1.LookUp("USER_ACC", "UA_TYPE", "ua_no=" + "'" + userno + "'"));
        //int data1 = 0;
        int data = 0;
        //data1 = objCommon1.LookUp("NOTIFICATION", "count(*)", "UA_TYPE="+ua);

        string conStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        //string selectTopics = "select count(*) from NOTIFICATION";
        string selectTopics = "select count(*) FROM [dbo].[tblNOTIFICATION] where ISREAD=0 and RECEIVER_UA_NO='"+userno+"'";
        //   string selectTopics = 
        // Define the ADO.NET Objects
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand topiccmd = new SqlCommand(selectTopics, con);
            con.Open();
            data = (int)topiccmd.ExecuteScalar();
            //if (numrows == 0)
            //{
            //    noTopics.Visible = true;
            //    topics.Visible = false;
            //}
        }
        return data;

    }

    [WebMethod]
    public static void GetNotification1()
    {

        int data = 0;
        SqlDataReader rdr = null;
        string conStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        string selectTopics = "SELECT NOTIFICATION_NAME,NOTIFICATION_MSG,STARTDATE FROM NOTIFICATION";
        // Define the ADO.NET Objects
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand topiccmd = new SqlCommand(selectTopics, con);
            con.Open();
            rdr = topiccmd.ExecuteReader();
            while (rdr.Read())
            {
                // get the results of each column
                string NOTIFICATION_NAME = (string)rdr["NOTIFICATION_NAME"];
                string NOTIFICATION_MSG = (string)rdr["NOTIFICATION_MSG"];
                // string STARTDATE = (string)rdr["STARTDATE"];
            }
            //if (numrows == 0)
            //{
            //    noTopics.Visible = true;
            //    topics.Visible = false;
            //}
        }
        //  return NOTIFICATION_NAME;

    }
    [WebMethod]
    public static DataSet GetNotification2()
    {
        DataSet ds = null;
        //try
        //{

        int data = 0;
        SqlDataReader rdr = null;
        string conStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        string selectTopics = "SELECT NOTIFICATION_NAME,NOTIFICATION_MSG,STARTDATE FROM NOTIFICATION";
        // Define the ADO.NET Objects
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand topiccmd = new SqlCommand(selectTopics, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(topiccmd);
            da.Fill(ds);
            topiccmd.ExecuteNonQuery();
            //while (rdr.Read())
            //{
            //    // get the results of each column
            //    string NOTIFICATION_NAME = (string)rdr["NOTIFICATION_NAME"];
            //    string NOTIFICATION_MSG = (string)rdr["NOTIFICATION_MSG"];
            //    // string STARTDATE = (string)rdr["STARTDATE"];
            //}
            //if (numrows == 0)
            //{
            //    noTopics.Visible = true;
            //    topics.Visible = false;
            //}
        }
        //}
        //catch (Exception ex)
        //{
        //    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStud_CourseReg_ChangeSemester_Bulk-> " + ex.ToString());
        //}
        return ds;
        //  return NOTIFICATION_NAME;

    }


    [WebMethod]
    public static List<NotificationModel> GetNotification()
    {
        Notification notification = new Notification();
        int userno = notification.details();

        List<NotificationModel> data = new List<NotificationModel>();
        string constr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "SELECT NOTIFICATION_NAME,NOTIFICATION_MSG,STARTDATE,RECEIVER_UA_NO,ACTIVITY_NO,ISREAD FROM tblNOTIFICATION where RECEIVER_UA_NO='" + userno + "' order by  STARTDATE desc ";
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    data = (from DataRow dr in ds.Tables[0].Rows
                            select new NotificationModel()
                            {
                                MessageHeading = dr["NOTIFICATION_NAME"].ToString(),
                                Message = dr["NOTIFICATION_MSG"].ToString(),
                               // Date = Convert.ToDateTime(dr["STARTDATE"]).ToShortDateString(),
                                Date = dr["STARTDATE"].ToString(),
                                //Date = Convert.ToDateTime(dr["STARTDATE"]).ToString(),
                                // Date = Convert.ToString(dr["STARTDATE"]),
                                uano = Convert.ToInt32(dr["RECEIVER_UA_NO"]),
                                ActivityNo = Convert.ToInt32(dr["ACTIVITY_NO"]),

                                IsRead = Convert.ToBoolean(dr["ISREAD"])
                            }).ToList();
                    return data;
                }
            }
        }
    }

     [WebMethod]
    public static int UpdateNotificationCounts(int ActivityNo)
    {
        object ret = 0;
        try
        {
            string conStr = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            Notification notification = new Notification();
            int userno = notification.details();
            string selectTopics = "update [dbo].[tblNOTIFICATION] set  ISREAD=1  where ISREAD=0 and ACTIVITY_NO='" + ActivityNo + "' and receiver_ua_no='" + userno + "'";
            //   string selectTopics = 
            // Define the ADO.NET Objects
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand topiccmd = new SqlCommand(selectTopics, con);
                con.Open();
                ret = (int)topiccmd.ExecuteNonQuery();
                //SqlParameter[] sqlParam = new SqlParameter[1];
                //sqlParam[0] = new SqlParameter("@P_UA_NO_Receive", UserId);
                //ret = sqlHelper.ExecuteNonQuerySP("sptblNotification_Count_Update", sqlParam, true);

            }
        }
        catch (Exception ex)
        {
            ret = -99;

            throw;
        }
        return 1;
    }

     //[WebMethod]
     //public static string BindUrls(int ActivityNo)
     //{
     //    string query = string.Empty;
     //    try
     //    {
     //        string host = HttpContext.Current.Request.Url.Host;
     //        string scheme = HttpContext.Current.Request.Url.Scheme;
     //        int portno = HttpContext.Current.Request.Url.Port;
     //        Notification notification = new Notification();
     //        // string userid = notification.username();


     //        DataSet dsGetUser = LogTableController.SearchUser_FOR_URL(ActivityNo);

     //        if (dsGetUser.Tables[0].Rows.Count > 0)
     //        {
     //            if (host == "localhost")
     //            {
     //                if (dsGetUser.Tables[0].Rows.Count > 0)
     //                {
     //                    // li1.Visible = true;
     //                    string lblLinkAddressql1 = dsGetUser.Tables[0].Rows[0]["AL_URL"].ToString();
     //                    int PageNoql1 = Convert.ToInt32(dsGetUser.Tables[0].Rows[0]["PNUMBER"].ToString());
     //                    //// iql1.InnerText = dsGetUser.Tables[0].Rows[0]["PNAME"].ToString();
     //                    string a = dsGetUser.Tables[0].Rows[0]["PNAME"].ToString();

     //                    if (lblLinkAddressql1.Contains("Masters/masters"))
     //                    {
     //                        query = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql1 + "&pageno=" + PageNoql1;
     //                    }
     //                    else
     //                    {
     //                        query = scheme + "://" + host + ":" + portno + "/PresentationLayer/" + lblLinkAddressql1 + "?pageno=" + PageNoql1;
     //                    }
     //                }
     //                else
     //                {
     //                    //  li1.Visible = false;
     //                }

     //            }
     //            else
     //            {
     //                if (dsGetUser.Tables[0].Rows.Count > 0)
     //                {
     //                    // li1.Visible = true;
     //                    string lblLinkAddressql1 = dsGetUser.Tables[0].Rows[0]["AL_URL"].ToString();
     //                    int PageNoql1 = Convert.ToInt32(dsGetUser.Tables[0].Rows[0]["PNUMBER"].ToString());
     //                    // iql1.InnerText = dsGetUser.Tables[0].Rows[0]["PNAME"].ToString();

     //                    if (lblLinkAddressql1.Contains("Masters/masters"))
     //                    {
     //                        query = scheme + "://" + host + "/" + lblLinkAddressql1 + "&pageno=" + PageNoql1;
     //                    }
     //                    else
     //                    {
     //                        query = scheme + "://" + host + "/" + lblLinkAddressql1 + "?pageno=" + PageNoql1;
     //                    }
     //                }
     //                else
     //                {
     //                    // li1.Visible = false;
     //                }
     //            }

     //        }
     //        else
     //        {
     //            //objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
     //        }
     //    }
     //    catch
     //    {
     //        throw;
     //    }
     //    return query;
     //}

}