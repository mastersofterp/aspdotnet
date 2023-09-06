using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Branch
            {
                #region Private members
                private int _branchNo = 0;
                private string _shortName;
                private string _longName;
                private string _branchNameInHindi;
                private int _totalIntake;
                private int _intake1 = 0;
                private int _intake2 = 0;
                private int _intake3 = 0;
                private int _intake4 = 0;
                private int _intake5 = 0;
                private int _duration;
                private string _code;
                private int sessionno = 0;  //added by reena
                private int centerno = 0;
                private int _degreeNo;
                private int _deptNo;
                private string _collegeCode;
                private int _collegeID;
                private string degree;
                private int _ugpgpf = 0;
                private string _branchCode; //Added by Irfan Shaikh on 20190413
                private int _enggStatus = 0;
                
                //Added Mahesh Malve on Dated 16-02-2021
                private string _SCHOOL_COLLEGE_CODE=string.Empty;
                private int _AICTE_NONAICTE = 0;
                private bool _isCore = false;
                private int _kpno = 0;
                private string _branchname_orignal = string.Empty;

                #endregion

                #region Public Property Fields
                public int BranchNo
                {
                    get { return _branchNo; }
                    set { _branchNo = value; }
                }
                public bool IsActive
                {
                    get;
                    set;
                }
                public string ShortName
                {
                    get { return _shortName; }
                    set { _shortName = value; }
                }

                public string LongName
                {
                    get { return _longName; }
                    set { _longName = value; }
                }

                public string BranchNameInHindi
                {
                    get { return _branchNameInHindi; }
                    set { _branchNameInHindi = value; }
                }

                public int TotalIntake
                {
                    get { return _totalIntake; }
                    set { _totalIntake = value; }
                }

                public int Intake1
                {
                    get { return _intake1; }
                    set { _intake1 = value; }
                }

                public int Intake2
                {
                    get { return _intake2; }
                    set { _intake2 = value; }
                }

                public int Intake3
                {
                    get { return _intake3; }
                    set { _intake3 = value; }
                }

                public int Intake4
                {
                    get { return _intake4; }
                    set { _intake4 = value; }
                }

                public int Intake5
                {
                    get { return _intake5; }
                    set { _intake5 = value; }
                }

                public int Duration
                {
                    get { return _duration; }
                    set { _duration = value; }
                }

                public string Code
                {
                    get { return _code; }
                    set { _code = value; }
                }

                public int Ugpgot
                {
                    get { return _ugpgpf; }
                    set { _ugpgpf = value; }
                }

                public int DegreeNo
                {
                    get { return _degreeNo; }
                    set { _degreeNo = value; }
                }

                public int DeptNo
                {
                    get { return _deptNo; }
                    set { _deptNo = value; }
                }

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                public int CollegeID
                {
                    get { return _collegeID; }
                    set { _collegeID = value; }
                }

                public int SessionNo
                {
                    get { return sessionno; }
                    set { sessionno = value; }
                }

                public int CenterNo
                {
                    get { return centerno; }
                    set { centerno = value; }
                }

                public string Degree
                {
                    get { return degree; }
                    set { degree = value; }
                }

                public object Ugpgpf { get; set; }

                public object BranchCode { get; set; } //Added by Irfan Shaikh on 20190413

                public int EnggStatus
                {
                    get { return _enggStatus; }
                    set { _enggStatus = value; }
                }

                //Added Mahesh on Dated 16-02-2021

                public string SCHOOL_COLLEGE_CODE
                {
                    get { return _SCHOOL_COLLEGE_CODE; }
                    set { _SCHOOL_COLLEGE_CODE = value; }
                }

                public int AICTE_NONAICTE
                {
                    get { return _AICTE_NONAICTE; }
                    set { _AICTE_NONAICTE=value;}
                }

                public bool Iscore
                {
                    get { return _isCore; }
                    set { _isCore = value; }
                }

                public int KpNo
                {
                    get { return _kpno; }
                    set { _kpno = value; }
                }

                public string Branchname_Origral
                {
                    get { return _branchname_orignal; }
                    set { _branchname_orignal = value; }
                }

                #endregion                
            }
        }
    }
}
