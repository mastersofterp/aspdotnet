using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic
{
    public class ODTracker
    {
        private int _organizationID;

        private int _sessionno;

        private string _student_comment;        

        private int _faculty_event_uano;

        private string _student_OD_date;

        private int _time_slot_no;

        private int _eventID;

        private int _facultyEvent_SRNO;

        private int _placement_SRNO;
        
        private int _sub_EventID;

        private string _eventStartDate;

        private string _eventEndDate;

        private Boolean isPublish;

        private Boolean isSpecialEvent;

        private string eventComment;

        private string _subEventName = string.Empty;
        
        private string _eventName = string.Empty;

        private string _placementName = string.Empty;

        private int _courseNo;
        
        private int _idno;

        private int _uano;

        private int _placementHeadNo;
        
        private int _oD_TYPE;

        private int _approved_By;
        
        private int _rejected_By;

        private int _final_request_approved_by;

        private int _final_request_rejected_by;

        private string _start_time;
        
        private string _end_time;

        private int _requestStatus;

        private int _placement_no;

        private int _coordinator_Id;

        private string _start_End_Time_IDs;

        private string _stud_placement_comment;

        private string _placementDate;

        private int _stud_od_no;

        public int OrganizationID
        {
            get { return _organizationID; }
            set { _organizationID = value; }
        }
        
        public string SubEventName
        {
            get { return _subEventName; }
            set { _subEventName = value; }
        }

        public string EventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }

        public int EventID
        {
            get { return _eventID; }
            set { _eventID = value; }
        }

        public int UANO
        {
            get { return _uano; }
            set { _uano = value; }
        }

        public int FacultyEvent_SRNO
        {
            get { return _facultyEvent_SRNO; }
            set { _facultyEvent_SRNO  = value; }
        }       
        
        public int Sub_EventID
        {
            get { return _sub_EventID; }
            set { _sub_EventID = value; }
        }

        public string Event_Start_Date
        {
            get { return _eventStartDate; }
            set { _eventStartDate = value; }
        }

        public string Event_End_Date
        {
            get { return _eventEndDate; }
            set { _eventEndDate = value; }
        }

        public Boolean IsPublish
        {
            get { return isPublish; }
            set { isPublish = value; }
        }

        public Boolean IsSpecialEvent
        {
            get { return isSpecialEvent; }
            set { isSpecialEvent = value; }
        }
        
        public string EventComment
        {
            get { return eventComment; }
            set { eventComment = value; }
        }

        public int CourseNo
        {
            get { return _courseNo; }
            set { _courseNo = value; }
        }

        public int Idno
        {
            get { return _idno; }
            set { _idno = value; }
        }

        public int OD_TYPE
        {
            get { return _oD_TYPE; }
            set { _oD_TYPE = value; }
        }

        public string Start_Time
        {
            get { return _start_time; }
            set { _start_time = value; }
        }

        public string End_Time
        {
            get { return _end_time; }
            set { _end_time = value; }
        }

        public string Start_End_Time_IDs
        {
            get { return _start_End_Time_IDs; }
            set { _start_End_Time_IDs = value; }
        }

        public int Approved_By
        {
            get { return _approved_By; }
            set { _approved_By = value; }
        }

        public int Rejected_By
        {
            get { return _rejected_By; }
            set { _rejected_By = value; }
        }

        public int Final_request_approved_by
        {
            get { return _final_request_approved_by; }
            set { _final_request_approved_by = value; }
        }

        public int Final_request_rejected_by
        {
            get { return _final_request_rejected_by; }
            set { _final_request_rejected_by = value; }
        }

        public string PlacementName
        {
            get { return _placementName; }
            set { _placementName = value; }
        }

        public int PlacementHeadNo
        {
            get { return _placementHeadNo; }
            set { _placementHeadNo = value; }
        }

        public int Placement_SRNO
        {
            get { return _placement_SRNO; }
            set { _placement_SRNO = value; }
        }

        public int RequestStatus
        {
            get { return _requestStatus; }
            set { _requestStatus = value; }
        }

        public int PlacementNo
        {
            get { return _placement_no; }
            set { _placement_no = value; }
        }

        public string PlacementDate
        {
            get { return _placementDate; }
            set { _placementDate = value; }
        }

        public string StudPlacementComment
        {
            get { return _stud_placement_comment; }
            set { _stud_placement_comment = value; }
        }

        public int Coordinator_Id
        {
            get { return _coordinator_Id; }
            set { _coordinator_Id = value; }
        }

        public int Faculty_Event_Uano
        {
            get { return _faculty_event_uano; }
            set { _faculty_event_uano = value; }
        }

        public string Student_OD_date
        {
            get { return _student_OD_date; }
            set { _student_OD_date = value; }
        }

        public int Time_Slot_No
        {
            get { return _time_slot_no; }
            set { _time_slot_no = value; }
        }

        public string Student_Comment
        {
            get { return _student_comment; }
            set { _student_comment = value; }
        }

        public int SessionNo
        {
            get { return _sessionno; }
            set { _sessionno = value; }
        }

        public int Stud_OD_NO
        {
            get { return _stud_od_no; }
            set { _stud_od_no = value; }
        }

         
    }
}