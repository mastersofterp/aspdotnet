using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentHomeModel
/// </summary>
public class StudentHomeModel
{
    public StudentHomeModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public class StudentAttendance
    {
        public string CourseName { get; set; }
        public string Attendance { get; set; }
        public string AttendancePerc { get; set; }
        public string CourseCode { get; set; }
        public string SectionName { get; set; }
    }

    public class StudAttendance
    {
        public List<StudentAttendance> AttendList { get; set; }
        public string AttendancePercent { get; set; }
    }


    public class StudentNotice
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string NewsDescription { get; set; }
    }

    public class GetNoticeData
    {
        public List<StudentNotice> NoticeList { get; set; }
        public string Announcement { get; set; }
        public string Assignment { get; set; }
    }

    public class StudentNews
    {
        public string Month { get; set; }
        public string Day { get; set; }
        public string Title { get; set; }
        public string NewsDesc { get; set; }
        public string Link { get; set; }
    }


    public class StudentTimeTable
    {
        public string Slot { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
//PRASHANTG-200424
    public class TodaysTimeTable
    {
        public string Slot { get; set; }
        public string SlotNo { get; set; }
        public string CCode { get; set; }
    }
    //PRASHANTG-200424
    public class ExamTimeTable
    {
        public string ExamDate { get; set; }  
        public string CCode { get; set; }
        public string Slot { get; set; }
        public string Course { get; set; }
        public string Backlog { get; set; }
        public string Semester { get; set; }
    }
    //PRASHANTG-200424
    public class Placement
    {
        public string Company { get; set; }
        public string SchDate { get; set; }
        public string Salary { get; set; }
        public string Criteria { get; set; }
        public string Course { get; set; }      
    }
    public class TimeTableHeader
    {
        public string Slot { get; set; }
    }

    public class GetStudentTimeTable
    {
        public List<StudentTimeTable> StudentTTBody { get; set; }
        public List<TimeTableHeader> StudTTHead { get; set; }
    }


    public class StudQuickAccess
    {
        public string Link { get; set; }
        public string LinkName { get; set; }
        public int PageNo { get; set; }
    }

    public class StudentTask
    {
        public string Link { get; set; }
        public string LinkName { get; set; }
        public int PageNo { get; set; }
    }
}