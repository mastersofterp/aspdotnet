using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Course
            {
                #region Private Members
                private int _sessionno = 0;
                private int _schemeno = 0;
                private string _schno = string.Empty;
                private string _delschno = string.Empty;
                private int _courseno = 0;
                private string _ccode = string.Empty;
                private string _coursename = string.Empty;
                private int _subid = 0;
                private int _elect = 0;
                private decimal _credits = 0.0m;
                private decimal _lecture = 0.0m;
                private decimal _theory = 0.0m;
                private decimal _practical = 0.0m;
                private decimal _maxmarks_i = 0.0m;
                private decimal _maxmarks_e = 0.0m;
                private decimal _minmarks = 0.0m;
                //private int _exam = 0;
                //private string _abbr = string.Empty;
                private decimal _srno = 0.0m;
                private decimal _s1max = 0.0m;
                private decimal _s2max = 0.0m;
                private decimal _s3max = 0.0m;
                private decimal _s4max = 0.0m;
                private decimal _s5max = 0.0m;
                private decimal _s6max = 0.0m;
                private decimal _s7max = 0.0m;
                private decimal _s8max = 0.0m;
                private decimal _s9max = 0.0m;
                private decimal _s10max = 0.0m;
                private decimal _assignmax = 0.0m;
                private int _globalele = 0;
                private decimal _ptmaxmarke = 0.0m;
                private decimal _ptmaxmarki = 0.0m;
                private string _temp = string.Empty;
                private bool _drawing = false;
                private string _colcode = string.Empty;
                private int _levelno = 0;
                private int _cgroupno = 0;
                private int _groupno = 0;
                private int _deptno = 0;
                private string _prerequisite = string.Empty;
                private int _prerequisite_cr = 0;
                private int _group1 = 0;
                private int _group2 = 0;
                private int _group3 = 0;
                private int _group4 = 0;
                
                //Pre-Defined Mark Pattern for Respective Exams(Course Create)
                //Course Assesment Practical Internal (CAPI)
                //code modified 25/07/09
                private int _CAPI = 0;
                private int _CAPE = 0;
                private int _CAPIE = 0;
                private int _CAEPP = 0;
                private int _CAEPE = 0;

                private int _CAOI = 0;
                private int _CAOE = 0;
                private int _CAOIE = 0;
                private int _CAEOP = 0;
                private int _CAEOE = 0;

                private int _CATI = 0;
                private int _CATE = 0;
                private int _CATIE = 0;
                private int _CAETP = 0;
                private int _CAETE = 0;

                private decimal _s1min = 0.0m;

                public decimal S1Min
                {
                    get { return _s1min; }
                    set { _s1min = value; }
                }
                private decimal _s2min = 0.0m;
                private decimal _s3min = 0.0m;
                private decimal _s4min = 0.0m;
                private decimal _s5min = 0.0m;
                private decimal _s6min = 0.0m;
                private decimal _s7min = 0.0m;
                private decimal _s8min = 0.0m;
                private decimal _s9min = 0.0m;
                private decimal _s10min = 0.0m;
                private int _semesterno = 0;
                private int _grade = 0;
                private int _minGrade = 0;
                private int _specialisation = 0;
                private int _paperhrs = 0;
              
                private decimal _extermarksmax = 0.0m;
                private decimal _extermarksmin = 0.0m;



                private int _scaling = 0;
                private int _categoryno = 0;
       
                #endregion
                private decimal _Total_Marks = 0;// Added by Dileep Kare on 25.02.2022
                private decimal _InterMarkMin = 0;//Added by Dileep Kare on 25.02.2022
                private int _orgid = 0;//Added By Dileep Kare on 11.03.2022
                private int _degreeno = 0;
                private int _branchno = 0;
                private int _specialisationno = 0;
                private string _specialisation_name = string.Empty;
                private int _Knowledge_partner = 0;
                private int _specal_map_no = 0;
                private int _intake = 0;
                private bool _status = false;
                private string _courseshortname = string.Empty;
                private string _activestatus = string.Empty;
                private string _subjecttype = string.Empty;
                private int _theory_Pra = 0;
                private int _sec_batch = 0;


                private int _electivegrpno = 0;
                private decimal _mintotalmarks = 0;
                private int _valueadded = 0;
                private int _isfeedback = 0;
                private int _audit = 0;

                public int Electivegrpno
                {
                    get { return _electivegrpno; }
                    set { _electivegrpno = value; }
                }
                public decimal Total_Marks
                {
                    get { return _Total_Marks; }
                    set { _Total_Marks = value; }
                }
                public decimal InterMarkMin
                {
                    get { return _InterMarkMin; }
                    set { _InterMarkMin = value; }
                }
                public int OrgId //Added By Dileep Kare on 11.03.2022
                {
                    get { return _orgid; }
                    set { _orgid = value; }
                }

                public int degreeno
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }
                public int branchno
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }
                public string specialisation_name
                {
                    get { return _specialisation_name; }
                    set { _specialisation_name = value; }
                }
                public int specialisationno
                {
                    get { return _specialisationno; }
                    set { _specialisationno = value; }
                }
                public int Knowledge_partner
                {
                    get { return _Knowledge_partner; }
                    set { _Knowledge_partner = value; }
                }
                public int intake
                {
                    get { return _intake; }
                    set { _intake = value; }
                }

                public bool status
                {
                    get { return _status; }
                    set { _status = value; }
                }

                public int Special_Map_No
                {
                    get { return _specal_map_no; }
                    set { _specal_map_no = value; }
                }

                public string CourseShortName
                {
                    get { return _courseshortname; }
                    set { _courseshortname = value; }
                }
                public string activestatus
                {
                    get { return _activestatus; }
                    set { _activestatus = value; }
                }
                public string subjecttype
                {
                    get { return _subjecttype; }
                    set { _subjecttype = value; }
                }
                public int theory_Pra
                {
                    get { return _theory_Pra; }
                    set { _theory_Pra = value; }
                }
                public int sec_batch
                {
                    get { return _sec_batch; }
                    set { _sec_batch = value; }
                }

                public int IsFeedback
                {
                    get { return _isfeedback; }
                    set { _isfeedback = value; }
                }

                #region Public Property Fields

                public int SessionNo
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }
                public int SchemeNo
                {
                    get { return _schemeno; }
                    set { _schemeno = value; }
                }
                public string SchNo
                {
                    get { return _schno; }
                    set { _schno = value; }
                }
                public string DelSchNo
                {
                    get { return _delschno; }
                    set { _delschno = value; }
                }
                public int CourseNo
                {
                    get { return _courseno; }
                    set { _courseno = value; }
                }
                public string CCode
                {
                    get { return _ccode; }
                    set { _ccode = value; }
                }
                public string CourseName
                {
                    get { return _coursename; }
                    set { _coursename = value; }
                }
                public int SubID
                {
                    get { return _subid; }
                    set { _subid = value; }
                }
                public int Elect
                {
                    get { return _elect; }
                    set { _elect = value; }
                }
                public decimal Credits
                {
                    get { return _credits; }
                    set { _credits = value; }
                }
                public decimal Lecture
                {
                    get { return _lecture; }
                    set { _lecture = value; }
                }
                public decimal Theory
                {
                    get { return _theory; }
                    set { _theory = value; }
                }
                public decimal Practical
                {
                    get { return _practical; }
                    set { _practical = value; }
                }
                public decimal MaxMarks_I
                {
                    get { return _maxmarks_i; }
                    set { _maxmarks_i = value; }
                }
                public decimal MaxMarks_E
                {
                    get { return _maxmarks_e; }
                    set { _maxmarks_e = value; }
                }
                public decimal MinMarks
                {
                    get { return _minmarks; }
                    set { _minmarks = value; }
                }
                //public int Exam
                //{
                //    get { return _exam; }
                //    set { _exam = value; }
                //}
                //public string Abbr
                //{
                //    get { return _abbr; }
                //    set { _abbr = value; }
                //}
                public decimal SrNo
                {
                    get { return _srno; }
                    set { _srno = value; }
                }
                public decimal S1Max
                {
                    get { return _s1max; }
                    set { _s1max = value; }
                }
                public decimal S2Max
                {
                    get { return _s2max; }
                    set { _s2max = value; }
                }
                public decimal S3Max
                {
                    get { return _s3max; }
                    set { _s3max = value; }
                }
                public decimal S4Max
                {
                    get { return _s4max; }
                    set { _s4max = value; }
                }
                public decimal S5Max
                {
                    get { return _s5max; }
                    set { _s5max = value; }
                }
                public decimal S6Max
                {
                    get { return _s6max; }
                    set { _s6max = value; }
                }
                public decimal S7Max
                {
                    get { return _s7max; }
                    set { _s7max = value; }
                }
                public decimal S8Max
                {
                    get { return _s8max; }
                    set { _s8max = value; }
                }
                public decimal S9Max
                {
                    get { return _s9max; }
                    set { _s9max = value; }
                }
                public decimal S10Max
                {
                    get { return _s10max; }
                    set { _s10max = value; }
                }
                public decimal AssignMax
                {
                    get { return _assignmax; }
                    set { _assignmax = value; }
                }
                public int GlobalEle
                {
                    get { return _globalele; }
                    set { _globalele = value; }
                }
                public decimal Ptmaxmarki
                {
                    get { return _ptmaxmarki; }
                    set { _ptmaxmarki = value; }
                }
                public decimal Ptmaxmarke
                {
                    get { return _ptmaxmarke; }
                    set { _ptmaxmarke = value; }
                }
                public string Temp
                {
                    get { return _temp; }
                    set { _temp = value; }
                }
                public bool Drawing
                {
                    get { return _drawing; }
                    set { _drawing = value; }
                }
                public string CollegeCode
                {
                    get { return _colcode; }
                    set { _colcode = value; }
                }
                public int Levelno
                {
                    get { return _levelno; }
                    set { _levelno = value; }
                }

                public string Prerequisite
                {
                    get { return _prerequisite; }
                    set { _prerequisite = value; }
                }
                public int Prerequisite_cr
                {
                    get { return _prerequisite_cr; }
                    set { _prerequisite_cr = value; }
                }

                public int CGroupno
                {
                    get { return _cgroupno; }
                    set { _cgroupno = value; }
                }
                public int Groupno
                {
                    get { return _groupno; }
                    set { _groupno = value; }
                }
                public int Group1
                {
                    get { return _group1; }
                    set { _group1 = value; }
                }
                public int Group2
                {
                    get { return _group2; }
                    set { _group2 = value; }
                }
                public int Group3
                {
                    get { return _group3; }
                    set { _group3 = value; }
                }
                public int Group4
                {
                    get { return _group4; }
                    set { _group4 = value; }
                }
                private int _group5 = 0;

                public int Group5
                {
                    get { return _group5; }
                    set { _group5 = value; }
                }

                public int Deptno
                {
                    get { return _deptno; }
                    set { _deptno = value; }
                }
                public int CAPI
                {
                    get { return _CAPI; }
                    set { _CAPI = value; }
                }

                public int CAPE
                {
                    get { return _CAPE; }
                    set { _CAPE = value; }
                }
                public int CAPIE
                {
                    get { return _CAPIE; }
                    set { _CAPIE = value; }
                }
                public int CAEPP
                {
                    get { return _CAEPP; }
                    set { _CAEPP = value; }
                }
                public int CAEPE
                {
                    get { return _CAEPE; }
                    set { _CAEPE = value; }
                }
                public int CAOI
                {
                    get { return _CAOI; }
                    set { _CAOI = value; }
                }


                public int CAOE
                {
                    get { return _CAOE; }
                    set { _CAOE = value; }
                }


                public int CAOIE
                {
                    get { return _CAOIE; }
                    set { _CAOIE = value; }
                }


                public int CAEOP
                {
                    get { return _CAEOP; }
                    set { _CAEOP = value; }
                }


                public int CAEOE
                {
                    get { return _CAEOE; }
                    set { _CAEOE = value; }
                }


                public int CATI
                {
                    get { return _CATI; }
                    set { _CATI = value; }
                }


                public int CATE
                {
                    get { return _CATE; }
                    set { _CATE = value; }
                }

                public int CATIE
                {
                    get { return _CATIE; }
                    set { _CATIE = value; }
                }

                public int CAETP
                {
                    get { return _CAETP; }
                    set { _CAETP = value; }
                }
                public int CAETE
                {
                    get { return _CAETE; }
                    set { _CAETE = value; }
                }

               

                public decimal S2Min
                {
                    get { return _s2min; }
                    set { _s2min = value; }
                }

                public decimal S3Min
                {
                    get { return _s3min; }
                    set { _s3min = value; }
                }

                public decimal S4Min
                {
                    get { return _s4min; }
                    set { _s4min = value; }
                }

                public decimal S5Min
                {
                    get { return _s5min; }
                    set { _s5min = value; }
                }

                public decimal S6Min
                {
                    get { return _s6min; }
                    set { _s6min = value; }
                }

                public decimal S7Min
                {
                    get { return _s7min; }
                    set { _s7min = value; }
                }

                public decimal S8Min
                {
                    get { return _s8min; }
                    set { _s8min = value; }
                }

                public decimal S9Min
                {
                    get { return _s9min; }
                    set { _s9min = value; }
                }

                public decimal S10Min
                {
                    get { return _s10min; }
                    set { _s10min = value; }
                }

                public int Grade
                {
                    get { return _grade; }
                    set { _grade = value; }
                }

                public int MinGrade
                {
                    get { return _minGrade; }
                    set { _minGrade = value; }
                }

                public int SemesterNo
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }
                public int Specialisation
                {
                    get { return _specialisation; }
                    set { _specialisation = value; }
                }
                public int Paper_hrs
                {
                    get { return _paperhrs; }
                    set { _paperhrs = value; }
                }
                public decimal ExtermarkMax
                {
                    get { return _extermarksmax; }
                    set { _extermarksmax = value; }
                }
                public decimal ExtermarkMin
                {
                    get { return _extermarksmin; }
                    set { _extermarksmin = value; }
                }
                public int Scaling
                {
                    get { return _scaling; }
                    set { _scaling = value; }
                }
                public int Categoryno
                {
                    get { return _categoryno; }
                    set { _categoryno = value; }
                }

                public decimal MinTotalMarks
                {
                    get { return _mintotalmarks; }
                    set { _mintotalmarks = value; }
                }

                public int ValueAdded
                {
                    get { return _valueadded; }
                    set { _valueadded = value; }
                }

                public int IsAudit
                {
                    get { return _audit; }
                    set { _audit = value; }
                }

                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS