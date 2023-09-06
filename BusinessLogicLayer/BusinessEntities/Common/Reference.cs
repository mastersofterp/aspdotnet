using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {

            public class Reference
            {
                #region Private Members
                //private char _errors = '0';
                private bool _errors = false;  //Modified By Rishabh on 08/11/2021
                private string _collegename = string.Empty;
                private string _collegeaddress = string.Empty;
                private string _collegecode = string.Empty;
                private byte[] _collegelogo = null;
                private string _fac_usertype = string.Empty;
                private bool _enrollmentno = true;  //true - automation  false - manual
                private string _phone = string.Empty;
                private string _emailid = string.Empty;
                private int _isCollegeLogo = 1;
                double _admlatefee = 0.00;
                private bool _feedback = false;
                private DateTime _StartYear = DateTime.MinValue;
                private DateTime _EndYear = DateTime.MinValue;
                private bool _resetCounter = true; // true - reset couter 
                private int _timetable = 0;
                private string _emailsvcid = string.Empty;
                private int _POPUP_FLAG = 0;
                private string _POPUP_MSG = string.Empty;

                private string _emailsvcpwd = string.Empty;

                private string _SMSsvcid = string.Empty;

                private string _SMSsvcpwd = string.Empty;
                private int _attempt = 0;
                private int _allowlogoutpopup = 0;
                private int _popupduration = 0;
                private int _Fascility = 0;
                string _userProfileSender = string.Empty;
                string _userProfileSubject = string.Empty;
                private int _MarkEntry_OTP = 0;
                private int _MarkSaveLock_Email = 0;
                private int _MarkSaveLock_SMS = 0;
                private bool _Course_Reg_B_Time_Table = false;
                private int _AttendanceBackDays = 0;
                private bool _ENDSEMBY_DECODE_OR_ENROLL=false; //added by Mahesh on Dated 22-04-2020
                private int _Admin_Level_Marks_Entry = 0; //added by Mahesh on Dated 23-06-2020

                private byte[] _collegeBanner = null; //added by tanu on dated 08-12-2022

                #endregion

                #region Public Properties
                public int AttendanceBackDays
                {
                    get { return _AttendanceBackDays; }
                    set { _AttendanceBackDays = value; }
                }
                public int MarkEntry_OTP
                {
                    get { return _MarkEntry_OTP; }
                    set { _MarkEntry_OTP = value; }
                }
                public int MarkSaveLock_Email
                {
                    get { return _MarkSaveLock_Email; }
                    set { _MarkSaveLock_Email = value; }
                }
                public int MarkSaveLock_SMS
                {
                    get { return _MarkSaveLock_SMS; }
                    set { _MarkSaveLock_SMS = value; }
                }
                public string userProfileSender
                {
                    get { return _userProfileSender; }
                    set { _userProfileSender = value; }
                }

                public string userProfileSubject
                {
                    get { return _userProfileSubject; }
                    set { _userProfileSubject = value; }
                }
                public int POPUP_FLAG
                {
                    get { return _POPUP_FLAG; }
                    set { _POPUP_FLAG = value; }
                }

                public string POPUP_MSG
                {
                    get { return _POPUP_MSG; }
                    set { _POPUP_MSG = value; }
                }
                //public char Errors
                //{
                //    get { return _errors; }
                //    set { _errors = value; }
                //}
                public bool Errors //Modified By Rishabh on 08/11/2021
                {
                    get
                    {
                        return _errors;
                    }
                    set
                    {
                        _errors = value;
                    }
                }

                public string CollegeName
                {
                    get { return _collegename; }
                    set { _collegename = value; }
                }
                public string CollegeAddress
                {
                    get { return _collegeaddress; }
                    set { _collegeaddress = value; }
                }
                public byte[] CollegeLogo
                {
                    get { return _collegelogo; }
                    set { _collegelogo = value; }
                }
                public string CollegeCode
                {
                    get { return _collegecode; }
                    set { _collegecode = value; }
                }
                public string Fac_UserType
                {
                    get { return _fac_usertype; }
                    set { _fac_usertype = value; }
                }
                public bool EnrollmentNo
                {
                    get { return _enrollmentno; }
                    set { _enrollmentno = value; }
                }
                public string Phone
                {
                    get { return _phone; }
                    set { _phone = value; }
                }
                public string EmailID
                {
                    get { return _emailid; }
                    set { _emailid = value; }
                }
                public int IsCollegeLogo
                {
                    get { return _isCollegeLogo; }
                    set { _isCollegeLogo = value; }
                }
                public double Admlatefee
                {
                    get { return _admlatefee; }
                    set { _admlatefee = value; }
                }

                public bool Feedback
                {
                    get { return _feedback; }
                    set { _feedback = value; }
                }

                public DateTime StartYear
                {
                    get { return _StartYear; }
                    set { _StartYear = value; }
                }

                public DateTime EndYear
                {
                    get { return _EndYear; }
                    set { _EndYear = value; }
                }
                public bool ResetCounter
                {
                    get { return _resetCounter; }
                    set { _resetCounter = value; }
                }
                public int Timetable
                {
                    get { return _timetable; }
                    set { _timetable = value; }
                }

                public int Attempt
                {
                    get { return _attempt; }
                    set { _attempt = value; }
                }
                public int AllowLogoutpopup
                {
                    get { return _allowlogoutpopup; }
                    set { _allowlogoutpopup = value; }
                }
                public int Popupduration
                {
                    get { return _popupduration; }
                    set { _popupduration = value; }
                }
                public string Emailsvcid
                {
                    get { return _emailsvcid; }
                    set { _emailsvcid = value; }
                }
                public string Emailsvcpwd
                {
                    get { return _emailsvcpwd; }
                    set { _emailsvcpwd = value; }
                }
                public string SMSsvcid
                {
                    get { return _SMSsvcid; }
                    set { _SMSsvcid = value; }
                }
                public string SMSsvcpwd
                {
                    get { return _SMSsvcpwd; }
                    set { _SMSsvcpwd = value; }
                }
                public int Fascility
                {
                    get { return _Fascility; }
                    set { _Fascility = value; }
                }
                public bool Course_Reg_B_Time_Table
                {
                    get { return _Course_Reg_B_Time_Table; }
                    set { _Course_Reg_B_Time_Table = value; }
                }
                public bool ENDSEMBY_DECODE_OR_ENROLL
                {
                    get { return _ENDSEMBY_DECODE_OR_ENROLL; }
                    set { _ENDSEMBY_DECODE_OR_ENROLL = value; }
                }

                public int Admin_Level_Marks_Entry
                {
                    get { return _Admin_Level_Marks_Entry; }
                    set { _Admin_Level_Marks_Entry = value; }
                }

                #endregion


                private int _Receipt_Cancel = 0;//Added By Dileep Kare 27.07.2021
                private int _Late_Fine_Cancel_Authority_Fac_ID = 0;//Added By Shailendra K on 03.01.2023

                public int Late_Fine_Cancel_Authority_Fac_ID //Added By Shailendra K on 03.01.2023
                {
                    get { return _Late_Fine_Cancel_Authority_Fac_ID; }
                    set { _Late_Fine_Cancel_Authority_Fac_ID = value; }
                }

                //Added By Dileep Kare on 26.07.2021
                public int Receipt_Cancel
                {
                    get { return _Receipt_Cancel; }
                    set { _Receipt_Cancel = value; }
                }
                private int _Update_OldExam_Data_Migration = 0;
                public int Update_OldExam_Data_Migration //Added by Deepali on 12/07/2021
                {
                    get { return _Update_OldExam_Data_Migration; }
                    set { _Update_OldExam_Data_Migration = value; }
                }


                //addrd by tanu on 08/12/2022
                public byte[] CollegeBanner
                {
                    get { return _collegeBanner; }
                    set { _collegeBanner = value; }
                }
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS