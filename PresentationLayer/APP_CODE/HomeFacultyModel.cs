using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HomeFacultyModel
/// </summary>
public class HomeFacultyModel
{
    public HomeFacultyModel()
    {


    }
    public string SlotIme { get; set; }
    public string Subject { get; set; }
    public string Branch { get; set; }
    public string Semester { get; set; }
    public string Section { get; set; }
    public string CourseCode { get; set; }
    public string BranchShortName { get; set; }


}

public class EmpInOutTIme
{
    public string Day { get; set; }
    public string InTime { get; set; }
    public string OutTime { get; set; }
}

public class EmpNews
{
    public string Month { get; set; }
    public string Day { get; set; }
    public string Title { get; set; }
    public string NewsDescription { get; set; }
    public string NewsLink { get; set; }
}

public class QuickAccessLinks
{
    public string Link { get; set; }
    public string LinkName { get; set; }
}

public class EmployeeTask
{
    public string AL_URL { get; set; }
    public string ACTIVITY_NAME { get; set; }
    public string STAT { get; set; }

    public string Link { get; set; }
    public string LinkName { get; set; }
    public int PageNo { get; set; }
}

public class UpcommingHolidays
{
    public string Holiday { get; set; }
    public string Month { get; set; }
}

public class TimeTable
{
    //public string DAY_NAME { get; set; }
    //public string Lecture1 { get; set; }
    //public string Lecture2 { get; set; }
    //public string Lecture3 { get; set; }
    //public string Lecture4 { get; set; }
    //public string Lecture5 { get; set; }
    //public string Lecture6 { get; set; }
    //public string Lecture7 { get; set; }


    public string Slot { get; set; }
    public string Monday { get; set; }
    public string Tuesday { get; set; }
    public string Wednesday { get; set; }
    public string Thursday { get; set; }
    public string Friday { get; set; }
    public string Saturday { get; set; }
    public string Sunday { get; set; }

}

public class TimeTableHeader
{
    public string Slot { get; set; }
}

public class TableList
{
    public List<TimeTableHeader> tblHeader { get; set; }
    public List<TimeTable> objTTList { get; set; }
}

public class CourseList
{
    public string CourseName { get; set; }
    public string BranchName { get; set; }
    public string CourseShortName { get; set; }

}


public class FacultyQuickAccess
{
    public string Link { get; set; }
    public string LinkName { get; set; }
    public int PageNo { get; set; }
}

public class TodoList
{
    public string SRNO { get; set; }
    public string TODO { get; set; }
    public string TODOCOUNT { get; set; }
}