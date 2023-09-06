using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic
{
    public class EventCreation
    {
        #region privatemember
        private string _eventTitle = string.Empty;
        private int _eventType = 0;
        private DateTime _eventStartDate = new DateTime();
        private DateTime _eventEndDate = new DateTime();
        private DateTime _eventStartRegDate = new DateTime();
        private DateTime _eventEndRegDate = new DateTime();
        private string _eventVenue = string.Empty;
        private string _eventDesc = string.Empty;
        private string _participant = string.Empty;
        private string _regFee = string.Empty;        
        #endregion

        #region publicmember
        public string EventTitle
        {
            get { return _eventTitle; }
            set { _eventTitle = value; }
        }
        public int EventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }
        public DateTime EventStartDate
        {
            get { return _eventStartDate; }
            set { _eventStartDate = value; }
        }
        public DateTime EventEndDate
        {
            get { return _eventEndDate; }
            set { _eventEndDate = value; }
        }
        public DateTime EventStartRegDate
        {
            get { return _eventStartRegDate; }
            set { _eventStartRegDate = value; }
        }
        public DateTime EventEndRegDate
        {
            get { return _eventEndRegDate; }
            set { _eventEndRegDate = value; }
        }
        public string EventVenue
        {
            get { return _eventVenue; }
            set { _eventVenue = value; }
        }
        public string EventDesc
        {
            get { return _eventDesc; }
            set { _eventDesc = value; }
        }
        public string EventParticipant
        {
            get { return _participant; }
            set { _participant = value; }
        }
          public string EventRegFee
        {
            get { return _regFee; }
            set { _regFee = value; }
        }
        
        #endregion
    }
}
