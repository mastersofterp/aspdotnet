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
            public class AcademicDashboardEntity
            {
                #region Declared Variable & Initialize;

                private int _SessionNo=0;
                private int _CollegeId = 0;
                private int _SchemeNo=0;
                private int _DeptNo = 0;
                private int _Dashboard_Type=0;
                private int _ExamProcessView = 0;

                #endregion Declared Variable & Initialize;

                #region Assign Value

                public int SessionNo
                {
                    get { return _SessionNo; }
                    set { _SessionNo = value; }
                }

                public int CollegeId
                {
                    get { return _CollegeId; }
                    set { _CollegeId = value; }
                }

                public int SchemeNo
                {
                    get { return _SchemeNo; }
                    set { _SchemeNo = value; }
                }

                public int DeptNo
                {
                    get { return _DeptNo; }
                    set { _DeptNo = value; }
                }

                public int Dashboard_Type
                {
                    get { return _Dashboard_Type; }
                    set { _Dashboard_Type = value; }
                }

                public int ExamProcessView
                {
                    get { return _ExamProcessView; }
                    set { _ExamProcessView = value; }
                }

                #endregion Assign Value


            }
        }
    }
}
