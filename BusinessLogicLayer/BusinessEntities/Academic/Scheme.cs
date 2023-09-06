using System;
using System.Collections.Generic;

using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Scheme
            {
                #region Private Member
                private int _schemeno = 0;
                private string _schemename = string.Empty;
                private int _schemetype = 0;
                private int _branchNo = 0;
                private int _degreeNo = 0;
                private int _dept_no = 0;
                private int _semesterno = 0;
                private int _batchNo = 0;
                private int _newscheme = 0;
                private string studid = string.Empty;                
                private string _college_code = string.Empty;
                private string _path_no = string.Empty;
                private int _schemeTypeNo = 0;
                private int _gradeType = 0; 
                private int _minimumCredits = 0;
                private int _patternno = 0;
                private string _gradeMarks = string.Empty;
                private string _abolishAttempts = string.Empty; // Added By Rishabh - 05/05/2022
                private int _specialization = 0;

                // Added By Rishabh - 27/07/2022
                private int _studyPattern = 0;
                private string _studyPatternName = string.Empty;
               
                #endregion

                #region Public Property Fields

                public int GradeType
                {
                    get { return _gradeType; }
                    set { _gradeType = value; }
                }

                public int MinimumCredits
                {
                    get { return _minimumCredits; }
                    set { _minimumCredits = value; }
                }

                public int SchemeTypeNo
                {
                    get { return _schemeTypeNo; }
                    set { _schemeTypeNo = value; }
                }
                
                public int SchemeNo
                {
                    get { return _schemeno; }
                    set { _schemeno = value; }
                }
                public string SchemeName
                {
                    get { return _schemename; }
                    set { _schemename = value; }
                }
                public int SchemeType
                {
                    get { return _schemetype; }
                    set { _schemetype = value; }
                }
                public int BranchNo
                {
                    get { return _branchNo; }
                    set { _branchNo = value; }
                }
                
                public int DegreeNo
                {
                    get { return _degreeNo; }
                    set { _degreeNo = value; }
                }
                public int Dept_No
                {
                    get { return _dept_no; }
                    set { _dept_no = value; }
                }
                public int SemesterNo
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }
                public int BatchNo
                {
                    get { return _batchNo; }
                    set { _batchNo = value; }
                }

                public int NewScheme
                {
                    get { return _newscheme; }
                    set { _newscheme = value; }
                }

                public string Studid
                {
                    get { return studid; }
                    set { studid = value; }
                }

                public string CollegeCode
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }
                public string Path_no
                {
                    get { return _path_no; }
                    set { _path_no = value; }
                }
                public int PatternNo
                {
                    get { return _patternno; }
                    set { _patternno = value; }
                }
                public string gradeMarks
                {
                    get { return _gradeMarks; }
                    set { _gradeMarks = value; }
                }

                public string AbolishAttempts  // Added By Rishabh - 05/05/2022
                {
                    get { return _abolishAttempts; }
                    set { _abolishAttempts = value; }
                }

                public int Specialization
                {
                    get { return _specialization; }
                    set { _specialization = value; }
                }

                // Added By Rishabh - 27/07/2022
                public int StudyPatternNo
                {
                    get { return _studyPattern; }
                    set { _studyPattern = value; }
                }
                public string StudyPatternName
                {
                    get { return _studyPatternName; }
                    set { _studyPatternName = value; }
                }
                #endregion
            }
        }
    }
}
