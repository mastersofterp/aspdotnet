using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Attendance
            {

                #region Private Member
                private int _attNo = 0;
                private int _sessionNo = 0;
                private int _uaNo = 0;
                private int _schemeNo = 0;
                private int _semesterNo = 0;
                private int _courseNo = 0;
                private string _cCode = string.Empty;
                private int _batchNo = 0;
                private int _subId = 0;
                private string _studIds = string.Empty;
                private string _attStatus = string.Empty;
                private DateTime _attDate = DateTime.MinValue;
                private DateTime _curDate = DateTime.MinValue;
                private int _period = 0;
                private int _hours = 0;
                private int _classType = 0;
                private string _collegeCode = string.Empty;
                //Added by Nikhil Vinod Lambe on 21022020

                private DateTime _fromDate = DateTime.MinValue;
                private DateTime _toDate = DateTime.MinValue;
                private int _sectionno = 0;
                private string _conditions = string.Empty;
                private string _percentage = string.Empty;
                private int _th_pr = 0;
                private int _degreeno = 0;
                private int _branchno = 0;
                private string _percentageFrom = string.Empty;
                private string _percentageTo = string.Empty;
                #endregion

                #region Public Property Fields

                public int AttNo
                {
                    get { return _attNo; }
                    set { _attNo = value; }
                }

                public int SessionNo
                {
                    get { return _sessionNo; }
                    set { _sessionNo = value; }
                }

                public int UaNo
                {
                    get { return _uaNo; }
                    set { _uaNo = value; }
                }

                public int SchemeNo
                {
                    get { return _schemeNo; }
                    set { _schemeNo = value; }
                }
                public int SemesterNo
                {
                    get { return _semesterNo; }
                    set { _semesterNo = value; }
                }

                public int CourseNo
                {
                    get { return _courseNo; }
                    set { _courseNo = value; }
                }

                public string CCode
                {
                    get { return _cCode; }
                    set { _cCode = value; }
                }
                public int BatchNo
                {
                    get { return _batchNo; }
                    set { _batchNo = value; }
                }
                public int SubId
                {
                    get { return _subId; }
                    set { _subId = value; }
                }
                public string StudIds
                {
                    get { return _studIds; }
                    set { _studIds = value; }
                }

                public string AttStatus
                {
                    get { return _attStatus; }
                    set { _attStatus = value; }
                }
                public DateTime AttDate
                {
                    get { return _attDate; }
                    set { _attDate = value; }
                }
                public DateTime CurDate
                {
                    get { return _curDate; }
                    set { _curDate = value; }
                }

                public int Period
                {
                    get { return _period; }
                    set { _period = value; }
                }
                public int Hours
                {
                    get { return _hours; }
                    set { _hours = value; }
                }
                public int ClassType
                {
                    get { return _classType; }
                    set { _classType = value; }
                }

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                //Added by Nikhil Vinod Lambe on 21022020

                public DateTime FromDate
                {
                    get { return _fromDate; }
                    set { _fromDate = value; }
                }
                public DateTime ToDate
                {
                    get { return _toDate; }
                    set { _toDate = value; }
                }
                public int SectionNo
                {
                    get { return _sectionno; }
                    set { _sectionno = value; }
                }
                public string Conditions
                {
                    get { return _conditions; }
                    set { _conditions = value; }
                }
                public string Percentage
                {
                    get { return _percentage; }
                    set { _percentage = value; }
                }
                public int Th_Pr
                {
                    get { return _th_pr; }
                    set { _th_pr = value; }
                }
                public int DegreeNo
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }
                public int BranchNo
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }
                public string PercentageFrom
                {
                    get { return _percentageFrom; }
                    set { _percentageFrom = value; }
                }
                public string PercentageTo
                {
                    get { return _percentageTo; }
                    set { _percentageTo = value; }
                }

                private string _Status;
                private int _Flag;
                private int _StatusNo;
                public string Status
                {
                    get
                    {
                        return _Status;
                    }
                    set
                    {
                        _Status = value;
                    }
                }

                public int Flag
                {
                    get
                    {
                        return _Flag;
                    }
                    set
                    {
                        _Flag = value;
                    }
                }

                public int StatusNo
                {
                    get
                    {
                        return _StatusNo;
                    }
                    set
                    {
                        _StatusNo = value;
                    }
                }

                /// <summary>
                /// Added By Rishabh B. 29122023
                /// </summary>
                public class BindCollege
                {
                    public string College_Name { get; set; }
                    public int College_Id { get; set; }
                }

                /// <summary>
                /// Added By Rishabh B. 29122023
                /// </summary>
                public class BindDegree
                {
                    public int DegreeNo { get; set; }
                    public string Degree_Name { get; set; }
                }

                /// <summary>
                /// Added By Rishabh B. 29122023
                /// </summary>
                public class AttendanceConfig
                {
                    public int SessionId { get; set; }
                    public string College_Ids { get; set; }
                    public string DegreeNos { get; set; }
                    public int SchemeType { get; set; }
                    public string SemesterNos { get; set; }
                    public DateTime StartDate { get; set; }
                    public DateTime EndDate { get; set; }
                    public string AttLockDays { get; set; }
                    public bool SMSFacility { get; set; }
                    public bool EmailFacility { get; set; }
                    public bool TeachingPlan { get; set; }
                    public bool ActiveStatus { get; set; }
                    public int OrgId { get; set; }

                    public string SessionName { get; set; }
                    public string CollegeName { get; set; }
                    public string SchemetypeName { get; set; }


                    public string StartDateN { get; set; }
                    public string EndDateN { get; set; }
                    public string DegreeName { get; set; }
                    public string SemesterName { get; set; }

                    public string SmsFacility_Str { get; set; }
                    public string EmailFacility_Str { get; set; }
                    public string TeachingPlan_Str { get; set; }
                    public string ActiveStatus_Str { get; set; }

                }
                #endregion


                /// <summary>
                /// Added By Rishabh on 16012024
                /// </summary>
                public class FacultyDiary
                {
                    public int CollegeId { get; set; }
                    public int Schemeno { get; set; }
                    public int Sessionno { get; set; }
                    public int Semesterno { get; set; }
                    public string Operator { get; set; }
                    public int Percentage { get; set; }
                    public string FromDate { get; set; }
                    public string ToDate { get; set; }
                }
            }
        }
    }
}