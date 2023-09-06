using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Student_Acd
            {
                #region Private Members

                private int _Intake = 0;
                private int _ua_no_delete = 0;
                private int _idno = 0;
                private string _studid = string.Empty;
                private string _enrollmentno;
                private string _regno = string.Empty;
                private string _rollno = string.Empty;
                private string _sem = string.Empty;
                private int _schemeno = 0;
                private int _batchno = 0;
                private int _thbatchno = 0;
                private string _passfail = string.Empty;
                private decimal _credit = 0.0M;
                private decimal _egp = 0.0M;
                private decimal _spi = 0.0M;
                private decimal _totcredit = 0.0M;
                private decimal _totegp = 0.0M;
                private decimal _totcpi = 0.0M;
                private int _sessionno = 0;
                private char _result = ' ';
                private int _fac_advisor = 0;
                private char _flag = '0';  //0=False, 1=True
                private int ua_no = 0;
                private int _courseNo = 0;
                private int _pract_theory = 0;
                private string _adteacher = string.Empty;
                private int _th_pr = 0;
                private int subid = 0;
                private string _ua_no1 = string.Empty;
                private int _sectionno = 0;
                private int _degreeno = 0;
                private int _branchno = 0;
                private int _semesterno = 0;





                private int _seatno = 0;
                private string _studname = string.Empty;
                private string _coursename = string.Empty;
                private string _ccode = string.Empty;
                private string _punishment = string.Empty;
                private int _status = 0;
                private int _punishmentno = 0;
                private int _COLLEGE_ID = 0;
                private int _INTEXT = 0;
                /*for withheld page*/
                    private string _remark = string.Empty;
                /*for withheld page*/
                    private string _Reason = string.Empty;
                    private string _IpAddress = string.Empty;
                    private string _coursenos = string.Empty;
                    private string _juniorname = string.Empty;
                    private string _Seniorname = string.Empty;
                    private string _ufmdetails = string.Empty;
                    private string _remarks = string.Empty;
                    //ROHIT TIWARI ON 13-APRIL-2019
                    private string _ipaddress = string.Empty;
                #endregion
                    //COPY CASE  
                    private int _allow_sessionno = 0;
                    private int _Punish_ExamNo = 0;
                    private string _Punishment = string.Empty;
                    private int _copyno = 0;
                    private DateTime _date = DateTime.Today;
                    private int _isTutorial = 0;//Added by Dileep Kare on 28.01.2022
                    private string _additionalTeacher = string.Empty;
                    private int _isAdditionalFlag = 0;//Added by Dileep Kare on 28.01.2022
                #region Public Property Fields


                    public int Ua_no_delete
                    {
                        get { return _ua_no_delete; }
                        set { _ua_no_delete = value; }
                    }

                    public string Reason
                    {
                        get { return _Reason; }
                        set { _Reason = value; }

                    }
                    public string Ipaddress
                    {
                        get { return _IpAddress; }
                        set { _IpAddress = value; }

                    }
                    public string Coursenos
                    {
                        get { return _coursenos; }
                        set { _coursenos = value; }

                    }
                public int PUNISHMENTNO
                 {
                    get { return _punishmentno; }
                     set { _punishmentno = value; }
                 }
                public int COLLEGE_ID
                {
                    get { return _COLLEGE_ID; }
                    set { _COLLEGE_ID = value; }
                }
                public int INTEXT
                {
                    get { return _INTEXT; }
                    set { _INTEXT = value; }
                }
                public int IdNo
                {
                    get { return _idno; }
                    set { _idno = value; }
                }
                public string StudId
                {
                    get { return _studid; }
                    set { _studid = value; }
                }
                public string EnrollmentNo
                {
                    get { return _enrollmentno; }
                    set { _enrollmentno = value; }
                }
                public string RegNo
                {
                    get { return _regno; }
                    set { _regno = value; }
                }
                public string RollNo
                {
                    get { return _rollno; }
                    set { _rollno = value; }
                }
                public string Sem
                {
                    get { return _sem; }
                    set { _sem = value; }
                }
                public int SchemeNo
                {
                    get { return _schemeno; }
                    set { _schemeno = value; }
                }
                public int BatchNo
                {
                    get { return _batchno; }
                    set { _batchno = value; }
                }
                public int ThBatchNo
                {
                    get { return _thbatchno; }
                    set { _thbatchno = value; }
                }
                public string PassFail
                {
                    get { return _passfail; }
                    set { _passfail = value; }
                }

                public decimal Credit
                {
                    get { return _credit; }
                    set { _credit = value; }
                }
                public decimal EGP
                {
                    get { return _egp; }
                    set { _egp = value; }
                }
                public decimal SPI
                {
                    get { return _spi; }
                    set { _spi = value; }

                }
                public decimal TotCredit
                {
                    get { return _totcredit; }
                    set { _totcredit = value; }
                }
                public decimal TotEGP
                {
                    get { return _totegp; }
                    set { _totegp = value; }
                }
                public decimal TotCPI
                {
                    get { return _totcpi; }
                    set { _totcpi = value; }
                }

                public int SessionNo
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }
                public char Result
                {
                    get { return _result; }
                    set { _result = value; }
                }

                public int Fac_Advisor
                {
                    get { return _fac_advisor; }
                    set { _fac_advisor = value; }
                }
                public char Flag
                {
                    get { return _flag; }
                    set { _flag = value; }
                }
                public int UA_No
                {
                    get { return ua_no; }
                    set { ua_no = value; }
                }
                public int CourseNo
                {
                    get { return _courseNo; }
                    set { _courseNo = value; }
                }
                public int Pract_Theory
                {
                    get { return _pract_theory; }
                    set { _pract_theory = value; }
                }
                public string AdTeacher
                {
                    get { return _adteacher; }
                    set { _adteacher = value; }
                }
                public int Th_Pr
                {
                    get { return _th_pr; }
                    set { _th_pr = value; }
                }
                public int sub_id
                {
                    get { return subid; }
                    set { subid = value; }
                }
                public string Ua_no1
                {
                    get { return _ua_no1; }
                    set { _ua_no1 = value; }
                }

                public int Sectionno
                {
                    get { return _sectionno; }
                    set { _sectionno = value; }
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

                public int SemesterNo
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }

                public int Seatno
                {
                    get { return _seatno; }
                    set { _seatno = value; }
                }

                public string StudName
                {
                    get { return _studname; }
                    set { _studname = value; }
                }

                public string CourseName
                {
                    get { return _coursename; }
                    set { _coursename = value; }
                }

                public string CCODE
                {
                    get { return _ccode; }
                    set { _ccode = value; }
                }

                public string Punishment
                {
                    get { return _punishment; }
                    set { _punishment = value; }
                }

                public int Status
                {
                    get { return _status; }
                    set { _status = value; }
                }

                public string Remark
                {
                    get { return _remark; }
                    set { _remark = value; }

                }

                public string Juniorname
                {
                    get { return _juniorname; }
                    set { _juniorname = value; }
                }

                public string Seniorname
                {
                    get { return _Seniorname; }
                    set { _Seniorname = value; }
                }
                public string Ufmdetails
                {
                    get { return _ufmdetails; }
                    set { _ufmdetails = value; }
                }

                public string Remarks
                {
                    get { return _remarks; }
                    set { _remarks = value; }
                }
                public int Intake
                {
                    get { return _Intake; }
                    set { _Intake = value; }
                }

                //ROHIT TIWARI ON 13-APRIL-2019
                public string IpAddress
                {
                    get { return _ipaddress; }
                    set { _ipaddress = value; }
                }
                #endregion

                //COPY CASE
                public int Allow_sessionno
                {
                    get { return _allow_sessionno; }
                    set { _allow_sessionno = value; }
                }
                public int Punish_ExamNo
                {
                    get { return _Punish_ExamNo; }
                    set { _Punish_ExamNo = value; }
                }

                public int Copyno
                {
                    get { return _copyno; }
                    set { _copyno = value; }
                }
                public DateTime Date
                {
                    get { return _date; }
                    set { _date = value; }
                }
                public int isTutorial// Added by Dileep Kare on 28.01.2022
                {
                    get { return _isTutorial; }
                    set { _isTutorial = value; }
                }
                public string AdditionalTeacher
                {
                    get { return _additionalTeacher; }
                    set { _additionalTeacher = value; }
                }
                public int isAdditionalFlag
                {
                    get { return _isAdditionalFlag; }
                    set { _isAdditionalFlag = value; }
                }
            }
        }
    }
}
