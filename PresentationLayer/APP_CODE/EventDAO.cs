using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


using IITMS.UAIMS;

public class EventDAO
{
    //IITMS.NITPRM.Common objCommon = new IITMS.NITPRM.Common();
    static string sessionno = string.Empty;

    public void CallFun()
    {
        //sessionno = objCommon.LookUp("REFF", "CUR_ADM_SESSIONNO", string.Empty);
        
    }
    public static int cInt(string strInt)
    {
        int i = 0; int.TryParse(strInt, out i); return i; 
    }
    //private static string connectionString=System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
    private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

   	//this method retrieves all events within range start-end
    //public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    //{

    //    List<CalendarEvent> events = new List<CalendarEvent>();
    //    //SqlConnection con = new SqlConnection(connectionString);
    //    SQLHelper objSQLhelper=new SQLHelper(connectionString);
    //    //Common COM = new Common();
    //      SqlParameter[] objParams = null;

          
    //            objParams = new SqlParameter[2];
    //            objParams[0] = new SqlParameter("@P_USERID", System.Web.HttpContext.Current.Session["username"].ToString());
    //            objParams[1] = new SqlParameter("@P_SESSIONNO", System.Web.HttpContext.Current.Session["currentsession"].ToString());

    //            DataSet ds = objSQLhelper.ExecuteDataSetSP("PKG_ITLE_CreatCalender", objParams);   //COM.CreateCalender(cInt(sessionno), cInt(System.Web.HttpContext.Current.Session["userno"].ToString()));// '*'
    //    //DataSet ds = COM.CreateCalender(cInt(sessionno));//Change to it dyanmically Ref: StudentTempRegistration
    //    //DataSet ds = null;


    //    DataTableReader dtr = ds.Tables[0].CreateDataReader();
    //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        while (dtr.Read())
    //        {
    //            CalendarEvent cevent = new CalendarEvent();
    //            cevent.id = int.Parse(dtr["id"].ToString());
    //            cevent.title = (string)dtr["EventHeader"];
    //            cevent.description = (string)dtr["EventDescription"];
    //            cevent.start = (DateTime)dtr["MainDate"];
    //            cevent.end = (DateTime)dtr["EndDate"];
    //            cevent.UserID = (string)dtr["userid"];
    //            events.Add(cevent);
    //        }
    //    }
    //    else
    //    {
            
    //    }
    //   /* SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end FROM Tbl_Calendar where event_start>=@start AND event_end<=@end", con);

    //    cmd.Parameters.AddWithValue("@start", start);
    //    cmd.Parameters.AddWithValue("@end", end);

    //    using (con)
    //    {
    //        con.Open();
    //        SqlDataReader reader = cmd.ExecuteReader();
    //        while (reader.Read())
    //        {
    //            CalendarEvent cevent = new CalendarEvent();
    //            cevent.id = int.Parse(reader["event_id"].ToString());
    //            cevent.title = (string)reader["title"];
    //            cevent.description = (string)reader["description"];
    //            cevent.start = (DateTime)reader["event_start"];
    //            cevent.end = (DateTime)reader["event_end"];
    //            events.Add(cevent);
    //        }
    //    }*/
    //    return events;
    //}
        
    //public static int  updateEvent(int id, String title, String description)
    //{
    //    //SqlConnection con = new SqlConnection(connectionString);
    //    ////SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET title=@title, description=@description WHERE event_id=@event_id", con);
    //    //SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET title=@title, description=@description WHERE event_id=0", con);
    //    //cmd.Parameters.AddWithValue("@title", title);
    //    //cmd.Parameters.AddWithValue("@description", description);
    //    //cmd.Parameters.AddWithValue("@event_id", id);
    //    //using (con)
    //    //{
    //    //    con.Open();
    //    //    cmd.ExecuteNonQuery();
    //    //}
    //    Common objcommon = new Common();
    //    int chkhave;
    //    int retStatus = Convert.ToInt32(CustomStatus.Others);
    //    SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //    try
    //    {
    //        chkhave =Convert.ToInt32(objcommon.LookUp("ITLE_Personal_Calendar", "count(UserId) as userid", "ID=" + id + " and UserId='" + System.Web.HttpContext.Current.Session["username"].ToString() + "'"));
    //        if (chkhave == 1)
    //        {
    //            SqlParameter[] objParams = null;

    //            bool flag = true;
    //            if (flag == true)
    //            {
    //                //SaveUpdateDelete

    //                objParams = new SqlParameter[5];
    //                objParams[0] = new SqlParameter("@P_Operation", "NEW_UPDATE");
    //                objParams[1] = new SqlParameter("@P_Id", id);
    //                objParams[2] = new SqlParameter("@P_EventHeader", title);
    //                objParams[3] = new SqlParameter("@P_EventDescription", description);
    //                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
    //                objParams[4].Direction = ParameterDirection.Output;

    //                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_PERSONAL_CALENDAR_SAVE_UPDATE_DELETE", objParams, true);
    //                if (Convert.ToInt32(ret) == -99)
    //                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
    //                else if (Convert.ToInt32(ret) == 1)
    //                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
    //                else if (Convert.ToInt32(ret) == 2)
    //                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
    //                else if (Convert.ToInt32(ret) == 3)
    //                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
    //                return chkhave;
    //            }
    //        }
    //        else
    //        { 
    //        return chkhave;
    //        }
    //    }
           
    //    catch (Exception ex)
    //    {
    //        retStatus = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IPersonal_CalendarController.PersonalCalendar -> " + ex.ToString());
    //    }
    //    return chkhave;
    //}

    //public static void updateEventTime(int id, DateTime start, DateTime end)
    //{
    //    SqlConnection con = new SqlConnection(connectionString);
    //    //SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET event_start=@event_start, event_end=@event_end WHERE event_id=@event_id", con);
    //    ////SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET event_start=@event_start, event_end=@event_end WHERE event_id=0", con);
    //    ////cmd.Parameters.AddWithValue("@event_start", start);
    //    ////cmd.Parameters.AddWithValue("@event_end", end);
    //    ////cmd.Parameters.AddWithValue("@event_id", id);
    //    SqlCommand cmd = new SqlCommand("UPDATE ITLE_Personal_Calendar SET maindate=@event_start, enddate=@event_end WHERE id=@id", con);
    //    cmd.Parameters.AddWithValue("@event_start", start);
    //    cmd.Parameters.AddWithValue("@event_end", end);
    //    cmd.Parameters.AddWithValue("@id", id);
    //    using (con)
    //    {
    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //    }
    //}

    //public static int deleteEvent(int id)
    //{
    //    Common objcommon = new Common();
    //    int retStatus = Convert.ToInt32(CustomStatus.Others);
    //    SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //    SqlParameter[] objParams = null;
    //    int chkhave;
    //    try
    //    {
    //         chkhave =Convert.ToInt32(objcommon.LookUp("ITLE_Personal_Calendar", "count(UserId) as userid", "ID=" + id + " and UserId='" + System.Web.HttpContext.Current.Session["username"].ToString() + "'"));
    //        if (chkhave == 1)
    //        {
    //            bool flag = true;
    //            if (flag == true)
    //            {
    //                objParams = new SqlParameter[4];
    //                objParams[0] = new SqlParameter("@P_Operation", "NEW_DELETE");
    //                objParams[1] = new SqlParameter("@P_Id", id);
    //                objParams[2] = new SqlParameter("@P_UserId", System.Web.HttpContext.Current.Session["username"].ToString());
    //                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
    //                objParams[3].Direction = ParameterDirection.Output;

    //                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_PERSONAL_CALENDAR_SAVE_UPDATE_DELETE", objParams, true);
    //                if (Convert.ToInt32(ret) == -99)
    //                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
    //                else if (Convert.ToInt32(ret) == 1)
    //                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
    //                else if (Convert.ToInt32(ret) == 2)
    //                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
    //                else if (Convert.ToInt32(ret) == 3)
    //                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
    //                return chkhave;
    //            }
    //        }
    //        else
    //        {
    //            return chkhave;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        retStatus = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IPersonal_CalendarController.PersonalCalendar -> " + ex.ToString());
    //    }
    //    return chkhave;
    //    //try
    //    //{
    //    //    SqlConnection con = new SqlConnection(connectionString);
    //    //    SqlCommand cmd = new SqlCommand("DELETE FROM ITLE_Personal_Calendar WHERE (id = @id)", con);
    //    //    cmd.Parameters.AddWithValue("@event_id", id);
    //    //    using (con)
    //    //    {
    //    //        con.Open();
    //    //        cmd.ExecuteNonQuery();
    //    //    }
    //    //}
    //    //catch (Exception ex)
    //    //{

    //    //}

    //}


    //[System.Web.Services.WebMethod]//DKS 08th June,2013

    //[System.Web.Script.Services.ScriptMethod]//DKS 08th June,2013
    //public static int addEvent(CalendarEvent cevent)
    //{
        
       
    //   //---------Tarun--------
    //    int retStatus = Convert.ToInt32(CustomStatus.Others);
    //    SQLHelper objSQLHelper = new SQLHelper(connectionString);
    //    try
    //    {
           
    //        SqlParameter[] objParams = null;

    //        bool flag = true;
    //        if (flag == true)
    //        {
    //            //SaveUpdateDelete

    //            objParams = new SqlParameter[7];
    //            objParams[0] = new SqlParameter("@P_Operation", cevent.operation);
    //            objParams[1] = new SqlParameter("@P_EventHeader", cevent.title);
    //            objParams[2] = new SqlParameter("@P_EventDescription", cevent.description);
    //            objParams[3] = new SqlParameter("@P_UserId", cevent.UserID);
    //            objParams[4] = new SqlParameter("@P_MainDate", cevent.start);
    //            objParams[5] = new SqlParameter("@P_EndDate", cevent.end);
    //            objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
    //            objParams[6].Direction = ParameterDirection.Output;

    //            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_PERSONAL_CALENDAR_SAVE_UPDATE_DELETE", objParams, true);
    //            if (Convert.ToInt32(ret) == -99)
    //                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
    //            else if (Convert.ToInt32(ret) == 1)
    //                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
    //            else if (Convert.ToInt32(ret) == 2)
    //                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
    //            else if (Convert.ToInt32(ret) == 3)
    //                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        retStatus = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IPersonal_CalendarController.PersonalCalendar -> " + ex.ToString());
    //    }

    //    //return retStatus;
    //    //----------------------

    //    int key = 0;
    //    try
    //    {
    //        //New_One_LoadById
    //        //objSQLHelper.ExecuteScalarSP();
    //        SqlParameter[] objParams = null;
    //        objParams = new SqlParameter[6];
    //        objParams[0] = new SqlParameter("@P_Operation", "New_One_LoadById");
    //        objParams[1] = new SqlParameter("@P_EventHeader", cevent.title);
    //        objParams[2] = new SqlParameter("@P_EventDescription", cevent.description);
    //        objParams[3] = new SqlParameter("@P_IDNO ", cevent.UserID);
    //        objParams[4] = new SqlParameter("@P_MainDate", cevent.start);
    //        objParams[5] = new SqlParameter("@P_EndDate", cevent.end);
    //      key=Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_ITLE_SP_GET_PERSONAL_CALENDER", objParams));
            
    //    }
    //    catch (Exception ex)
    //    {
    //        retStatus = Convert.ToInt32(CustomStatus.Error);
    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IPersonal_CalendarController.PersonalCalendar -> " + ex.ToString());
    //    }
    //    //System.Web.HttpContext.Current.Session["idList"] = key;
    //    return key;
    //    //----------------------------------------------------------------
    //    //int i = 0;
    //    //SqlConnection con = new SqlConnection(connectionString);
    //    //SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_Calendar(UserId,title, description, event_start, event_end) VALUES('" + cInt(System.Web.HttpContext.Current.Session["userno"].ToString()) + "',@title, @description, @event_start, @event_end)", con);


    //    //cmd.Parameters.AddWithValue("@title", cevent.title);
    //    //cmd.Parameters.AddWithValue("@description", cevent.description);
    //    //cmd.Parameters.AddWithValue("@event_start", cevent.start);
    //    //cmd.Parameters.AddWithValue("@event_end", cevent.end);
    //    //using (con)
    //    //{
    //    //    con.Open();
    //    //    try
    //    //    {
    //    //        cmd.ExecuteNonQuery();
    //    //    }
    //    //    catch (Exception ex)
    //    //    { 
            
    //    //    }
            

    //        //get primary key of inserted row
    //    //    cmd = new SqlCommand("SELECT max(event_id) FROM Tbl_Calendar where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end", con);
        
           
    //    //    cmd.Parameters.AddWithValue("@title", cevent.title);
    //    //    cmd.Parameters.AddWithValue("@description", cevent.description);
    //    //    cmd.Parameters.AddWithValue("@event_start", cevent.start);
    //    //    cmd.Parameters.AddWithValue("@event_end", cevent.end);

    //    //    object strObj = cmd.ExecuteScalar();
    //    //    key = int.Parse(string.Format("{0}", strObj));//Deepak K S Trival Casting
            
    //    //}

    //    //return key;

    //}

    //this method retrieves all events within range start-end


    public static DataSet GetCalenderAllEvents(int sessionno, int USERID)
    {
        DataSet ds;
        try
        {
            SQLHelper objsqlhelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
            objParams[1] = new SqlParameter("@P_USERID", USERID);
            //objParams[2] = new SqlParameter("@P_PAGE_LINK", pagelink);
            ds = objsqlhelper.ExecuteDataSetSP("GETALLCALENDAREVENTS", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.Common.CheckActivity-> " + ex.ToString());
        }
        return ds;
    }

    public static List<CalendarEvent> getEvents(DateTime start, DateTime end)
    {

        List<CalendarEvent> events = new List<CalendarEvent>();
        SqlConnection con = new SqlConnection(connectionString);



        DataSet ds = GetCalenderAllEvents(cInt(sessionno), cInt(System.Web.HttpContext.Current.Session["userno"].ToString()));// '*'
        //DataSet ds = COM.CreateCalender(cInt(sessionno));//Change to it dyanmically Ref: StudentTempRegistration
        //DataSet ds = null;


        DataTableReader dtr = ds.Tables[0].CreateDataReader();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            while (dtr.Read())
            {
                try
                {
                    CalendarEvent cevent = new CalendarEvent();
                    //      == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                    cevent.id = int.Parse(dtr["event_id"].ToString()) == null ? 0 : int.Parse(dtr["event_id"].ToString());
                    cevent.title = (string)dtr["title"] == null ? string.Empty : (string)dtr["title"];
                    cevent.description = (string)dtr["description"] == null ? string.Empty : (string)dtr["description"];
                    cevent.start = (DateTime)dtr["event_start"];
                    cevent.end = (DateTime)dtr["event_end"];
                    events.Add(cevent);
                }
                catch (Exception ex)
                { }
            }
        }
        else
        {

        }
        /* SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end FROM Tbl_Calendar where event_start>=@start AND event_end<=@end", con);

         cmd.Parameters.AddWithValue("@start", start);
         cmd.Parameters.AddWithValue("@end", end);

         using (con)
         {
             con.Open();
             SqlDataReader reader = cmd.ExecuteReader();
             while (reader.Read())
             {
                 CalendarEvent cevent = new CalendarEvent();
                 cevent.id = int.Parse(reader["event_id"].ToString());
                 cevent.title = (string)reader["title"];
                 cevent.description = (string)reader["description"];
                 cevent.start = (DateTime)reader["event_start"];
                 cevent.end = (DateTime)reader["event_end"];
                 events.Add(cevent);
             }
         }*/
        return events;
    }

    public static void updateEvent(int id, String title, String description)
    {
        SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET title=@title, description=@description WHERE event_id=@event_id", con);
        if (Convert.ToInt32(id) != 0)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET title=@title, description=@description WHERE event_id=@event_id", con);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@event_id", id);
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        else
        {
        }


    }

    public static void updateEventTime(int id, DateTime start, DateTime end)
    {
        SqlConnection con = new SqlConnection(connectionString);
        //SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET event_start=@event_start, event_end=@event_end WHERE event_id=@event_id", con);
        SqlCommand cmd = new SqlCommand("UPDATE Tbl_Calendar SET event_start=@event_start, event_end=@event_end WHERE event_id=@event_id", con);
        cmd.Parameters.AddWithValue("@event_start", start);
        cmd.Parameters.AddWithValue("@event_end", end);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void deleteEvent(int id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("DELETE FROM Tbl_Calendar WHERE (event_id = @event_id)", con);
        cmd.Parameters.AddWithValue("@event_id", id);
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }


    //[System.Web.Services.WebMethod]//DKS 08th June,2013

    //[System.Web.Script.Services.ScriptMethod]//DKS 08th June,2013
    public static int addEvent(CalendarEvent cevent)
    {
        //int i = 0;
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO Tbl_Calendar(UserId,title, description, event_start, event_end) VALUES('" + cInt(System.Web.HttpContext.Current.Session["userno"].ToString()) + "',@title, @description, @event_start, @event_end)", con);


        cmd.Parameters.AddWithValue("@title", cevent.title);
        cmd.Parameters.AddWithValue("@description", cevent.description);
        cmd.Parameters.AddWithValue("@event_start", cevent.start);
        cmd.Parameters.AddWithValue("@event_end", cevent.end);

        int key = 0;
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();

            //get primary key of inserted row
            cmd = new SqlCommand("SELECT max(event_id) FROM Tbl_Calendar where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end", con);


            cmd.Parameters.AddWithValue("@title", cevent.title);
            cmd.Parameters.AddWithValue("@description", cevent.description);
            cmd.Parameters.AddWithValue("@event_start", cevent.start);
            cmd.Parameters.AddWithValue("@event_end", cevent.end);

            object strObj = cmd.ExecuteScalar();
            key = int.Parse(string.Format("{0}", strObj));//Deepak K S Trival Casting

        }

        return key;

    }

    public int addFeedback(string UserID, string Feedback)
    {


        //---------Tarun--------
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        try
        {

            SqlParameter[] objParams = null;

            bool flag = true;
            if (flag == true)
            {
                //SaveUpdateDelete

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_Operation", "NEW_FEEDBACK_INSERT");
                objParams[1] = new SqlParameter("@P_UserId", UserID);
                objParams[2] = new SqlParameter("@P_Feedback", Feedback);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_PERSONAL_CALENDAR_SAVE_UPDATE_DELETE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 2)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (Convert.ToInt32(ret) == 3)
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IPersonal_CalendarController.PersonalCalendar -> " + ex.ToString());
        }
        return retStatus;
    }
    

    
}
