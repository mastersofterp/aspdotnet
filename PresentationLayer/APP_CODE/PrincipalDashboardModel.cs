using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PrincipalDashboardModel
/// </summary>
public class PrincipalDashboardModel
{
    public PrincipalDashboardModel()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public class StudentCounters
    {
        public string _male { get; set; }
        public string _female { get; set; }
        public string _other { get; set; }
        public string _total { get; set; }
    }
    public class StudentsCount
    {
        public string BatchNo { get; set; }
        public string Year { get; set; }
        public string Count { get; set; }
    }

    public class ActivityDetails
    {
        public string ActivityName { get; set; }
        public string SessionName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ActivityStatus { get; set; }
    }

    public class AdmFeesDeatils
    {
        public string Receipt { get; set; }
        public string Fee { get; set; }
        public string Year { get; set; }
    }

    public class PrincipalNews
    {
        public string Month { get; set; }
        public string Day { get; set; }
        public string Title { get; set; }
        public string NewsDesc { get; set; }
        public string Link { get; set; }
    }


    public class ResultBody
    {
        public string SessionName { get; set; }
        public string DegreeName { get; set; }
        public string BranchShortName { get; set; }
        public string BranchName { get; set; }
        public string Sem1 { get; set; }
        public string Sem2 { get; set; }
        public string Sem3 { get; set; }
        public string Sem4 { get; set; }
    }

    public class ResultHeader
    {
        public string Header { get; set; }
    }

    public class PrincipalResultAnalysisData
    {
        public List<ResultHeader> tHeader { get; set; }
        public List<ResultBody> tBody { get; set; }
    }   
   
}
public class LeaveCount
{
    public string ToTal_Applied { get; set; }
    public string Approve_Leave { get; set; }
    public string Pending_Leave { get; set; }
}

public class PrincipalQuickAccess
{
    public string Link { get; set; }
    public string LinkName { get; set; }
    public int PageNo { get; set; }
}

public class LeaveApprovalData
{
    public string EmpName { get; set; }
    public string SUBDEPT { get; set; }
    public string LName { get; set; }
    public string From_date { get; set; }
    public string TO_DATE { get; set; }
    public string Pending_on { get; set; }
    public int LETRNO { get; set; }
}