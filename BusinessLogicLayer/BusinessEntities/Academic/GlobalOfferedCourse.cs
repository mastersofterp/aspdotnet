using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
           public class GlobalOfferedCourse
            {
                private int _orgID = 0;
                private int _college_ID = 0;
                private int _college_code = 0;
                private int _sessionNo = 0;
                private int _degreeNo = 0;
                private int _branchNo = 0;
                private int _schemeno = 0;
                private string _ccode = string.Empty;
                private int _courseno = 0;
                private int _semesterno = 0;
                private string _to_semesterno = string.Empty;
                private int _global_offered = 0;
                private double _credits = 0;
                private int _capacity = 0;
                private int _ua_no = 0;
                private int _mainFacultyno = 0;
                private int _alternateFacultyno = 0;
                private int _ctno = 0;
                private int _slottype = 0;
                private string _ipaddress = string.Empty;
                private int _groupId = 0;
                GlobalTimeTable[] _globaltimetable = null;

                public int Orgid { get { return _orgID; } set { _orgID = value; } }
                public int College_id { get { return _college_ID; } set { _college_ID = value; } }
                public int College_code { get { return _college_code; } set { _college_code = value; } }
                public int SessionNo { get { return _sessionNo; } set { _sessionNo = value; } }
                public int DegreeNo { get { return _degreeNo; } set { _degreeNo = value; } }

                public int BranchNo { get { return _branchNo; } set { _branchNo = value; } }
                public int Schemeno { get { return _schemeno; } set { _schemeno = value; } }
                public string CCODE { get { return _ccode; } set { _ccode = value; } }

                public int Courseno { get { return _courseno; } set { _courseno = value; } }
                public int Semesterno { get { return _semesterno; } set { _semesterno = value; } }
                public string To_semesterno { get { return _to_semesterno; } set { _to_semesterno = value; } }
                public int Global_offered { get { return _global_offered; } set { _global_offered = value; } }
                public double Credits { get { return _credits; } set { _credits = value; } }
                public int Capacity { get { return _capacity; } set { _capacity = value; } }
                public int Ua_no { get { return _ua_no; } set { _ua_no = value; } }
                public int MainFacultyno { get { return _mainFacultyno; } set { _mainFacultyno = value; } }
                public int AlternateFacultyno { get { return _alternateFacultyno; } set { _alternateFacultyno = value; } }
                public int CTNO { get { return _ctno; } set { _ctno = value; } }
                public int SlotType { get { return _slottype; } set { _slottype = value; } }
                public string IpAddress { get { return _ipaddress; } set { _ipaddress = value; } }
                public int GroupId { get { return _groupId; } set { _groupId = value; } }
                public GlobalTimeTable[] Globaltimetable
                {
                    get { return _globaltimetable; }
                    set { _globaltimetable = value; }
                }
            }
        }
    }
}
