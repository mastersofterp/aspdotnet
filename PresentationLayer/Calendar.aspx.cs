using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;

public partial class Calendar : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [System.Web.Services.WebMethod(true)]
    public static string UpdateEvent(CalendarEvent cevent)
    {

        List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
        if (idList != null && idList.Contains(cevent.id))
        {
            if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
            {
                if (Convert.ToInt32(cevent.id) != 0)
                {
                    EventDAO.updateEvent(cevent.id, cevent.title, cevent.description);
                    return "Event updated Successfully !!";
                }
                else
                {
                    return "You Cannot Update This Event!!";
                }

                //return "updated event with id:" + cevent.id + " update title to: " + cevent.title +
                //" update description to: " + cevent.description;

          
            }

        }

        return "unable to update event with id:" + cevent.id + " title : " + cevent.title +
            " description : " + cevent.description;
    }

    [System.Web.Services.WebMethod(true)]
    public static string UpdateEventTime(ImproperCalendarEvent improperEvent)
    {
        List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
        if (idList != null && idList.Contains(improperEvent.id))
        {
            EventDAO.updateEventTime(improperEvent.id,
                DateTime.ParseExact(improperEvent.start, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
                DateTime.ParseExact(improperEvent.end, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));

            //return "updated event with id:" + improperEvent.id + "update start to: " + improperEvent.start +
            //    " update end to: " + improperEvent.end;

            return "You Can't update the following events !!";
        }

        return "unable to update event with id: " + improperEvent.id;
    }

    
    [System.Web.Services.WebMethod(true)]
    public static String deleteEvent(int id)
    {
        List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
        if (idList != null && idList.Contains(id))
        {
            if (Convert.ToInt32(id) != 0)
            {
                EventDAO.deleteEvent(id);
                //return "deleted event with id:" + id;
                return id.ToString();
            }

            else
            {

                //Common objCommon = new Common();
                //objCommon.DisplayMessage("You Cannot Delete This Event!!", Page pg);
             // string ids=  "You Cannot Delete This Event!!";
              return "0";
            }

        }

        return "unable to delete event with id: " + id;
    }

    [System.Web.Services.WebMethod]
    //[System.Web.Script.Services.ScriptMethod]//Deepak S 08th June,13
    public static int addEvent(ImproperCalendarEvent improperEvent)
    {

        CalendarEvent cevent = new CalendarEvent()
        {
            title = improperEvent.title,
            description = improperEvent.description,
            start = DateTime.ParseExact(improperEvent.start, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture),
            end = DateTime.ParseExact(improperEvent.end, "dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)

        };

        if (cevent.title.ToString() == string.Empty)
        {
            return -1;
        }
        else if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
        {
            int key = EventDAO.addEvent(cevent);

            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

            if (idList != null)
            {
                idList.Add(key);
            }

            return key;

        }

        return -1;

    }

    private static bool CheckAlphaNumeric(string str)
    {

        return Regex.IsMatch(str, @"^[a-zA-Z0-9 ]*$");


    }
}