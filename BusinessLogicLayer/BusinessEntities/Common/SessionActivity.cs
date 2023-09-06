//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : SESSION ACTIVITY CLASS 
// CREATION DATE : 15-JUN-2009                                                        
// CREATED BY    : AMIT YADAV AND SANJAY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class SessionActivity
    {
        #region Private Fields
        int _sessionActivityNo = 0;
        int _activityNo = 0;
        int _sessionNo = 0;
        DateTime _startDate = DateTime.MinValue;
        DateTime _endDate = DateTime.MinValue;
        bool _isStarted = false;

        int _showStatus = 0; //Modified by Rishabh B. on 02/11/2021
        string _links = string.Empty;
        #endregion

        //added on 06-04-2020 by Vaishali for Notification
        string _activity_name = string.Empty;
        string _sessionname = string.Empty;
        int _UA_TYPE = 0;
        string _uaa_no = string.Empty;
        string _notificationmsg = string.Empty;
        string _notificationname = string.Empty;
        int _notificationstatus = 0;
        string _ActivityNos = string.Empty;
        //added on 14-12-2022 by Jay T. 
         string _Start_Time = string.Empty;
         string _End_Time = string.Empty;

        //added on 06-04-2020 by Rahul for Feedback
        int _feedbacktypeno = 0;
        string _ipaddress = string.Empty;
        int _user_no = 0;
        int _degree = 0;
        int _branch = 0;
        int _semester = 0;
        int _section = 0;
        int _college_id = 0;
        int _schemeno = 0;
        int _coscheno = 0;
        public int Feedbacktypeno
        {
            get { return _feedbacktypeno; }
            set { _feedbacktypeno = value; }
        }
        public string Ipaddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
        
        public string Notification_Name
        {
            get { return _notificationname; }
            set { _notificationname = value; }
        }

        public int User_no
        {
            get { return _user_no; }
            set { _user_no = value; }
        }
        public int Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }
        public int Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }
        public int Semester
        {
            get { return _semester; }
            set { _semester = value; }
        }
        public int College_Id
        {
            get { return _college_id; }
            set { _college_id = value; }
        }
        public int Schemeno
        {
            get { return _schemeno; }
            set { _schemeno = value; }
        }
        public int Coscheno
        {
            get { return _coscheno; }
            set { _coscheno = value; }
        }

        //added on 06-04-2020 by Vaishali for Notification
        public string Activity_Name
        {
            get { return _activity_name; }
            set { _activity_name = value; }
        }

        public string Session_Name
        {
            get { return _sessionname; }
            set { _sessionname = value; }
        }

        public int ua_type
        {
            get { return _UA_TYPE; }
            set { _UA_TYPE = value; }
        }

        public string ua_no
        {
            get { return _uaa_no; }
            set { _uaa_no = value; }
        }

        public string Notification_Message
        {
            get { return _notificationmsg; }
            set { _notificationmsg = value; }
        }

        //public string Notification_Name
        //{
        //    get { return _notificationname; }
        //    set { _notificationname = value; }
        //}

        public int Notification_Status
        {
            get { return _notificationstatus; }
            set { _notificationstatus = value; }
        }

        public string ActivityNos
        {
            get
            {
                return _ActivityNos;
            }
            set
            {
                _ActivityNos = value;
            }
        }

        #region Public Properties

        public int SessionActivityNo
        {
            get { return _sessionActivityNo; }
            set { _sessionActivityNo = value; }
        }

        public int ActivityNo
        {
            get { return _activityNo; }
            set { _activityNo = value; }
        }

        public int SessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public bool IsStarted
        {
            get { return _isStarted; }
            set { _isStarted = value; }
        }

        public int ShowStatus //Modified By Rishabh B. on 02/11/2021
        {
            get
            {
                return _showStatus;
            }
            set
            {
                _showStatus = value;
            }
        }

        public string Links
        {
            get { return _links; }
            set { _links = value; }
        }
        //added on 14-12-2022 by Jay T. 
        public string Start_Time
        {
            get { return _Start_Time; }
            set { _Start_Time = value; }
        }
        public string End_Time
        {
            get { return _End_Time; }
            set { _End_Time = value; }
        }
        #endregion
    }
}