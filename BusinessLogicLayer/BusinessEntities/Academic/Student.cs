using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Student
    {
        //general info 
        #region Private Member
        private int _idNo = 0;
        private string _annual_income = string.Empty;
        private string _collegejss = string.Empty;
        private string _cetorderno = string.Empty;
        private string _cetdate = string.Empty;
        private decimal _cetamount = 0;
        private string _regNo = string.Empty;
        private string _rollNo = string.Empty;
        private int _sectionNo = 0;
        private string _studName = string.Empty;
        private string _firstName = string.Empty;
        private string _MiddleName = string.Empty;
        private string _lastName = string.Empty;
        private string _studNameHindi = string.Empty;
        private string _fatherfirstName = string.Empty;
        private string _fatherName = string.Empty;
        private string _fatherMiddleName = string.Empty;
        private string _fatherLastName = string.Empty;
        private string _motherName = string.Empty;
        private string _fatherMobile = string.Empty;
        
        private string _motherMobile = string.Empty;
        private string _fatherOfficeNo = string.Empty;
        private string _motherOfficeNo = string.Empty;
        private DateTime _dob = DateTime.Today;
        private string _Age = string.Empty;
        private decimal _nataMarks = 0;
        private string _internationalStu = string.Empty; 

        
        private char _sex = ' ';
        private int _religionNo = 0;
        private char _married = ' ';
        private int _nationalityNo = 0;
        private int _categoryNo = 0;
        private int _admcategoryNo = 0;
        private string _addharcardno = string.Empty;
        // address info
        private string _pAddress = string.Empty;
        private string _pCity = string.Empty;

        // PG ENTRANCE EXAM SCORES

        private int _PGQUALIFYNO = 0;
        private string _PGENTROLLNO = string.Empty;
        private decimal _pgpercentage = 0.0m;
        private decimal _pgpercentile = 0.0m;
        private string _pgyearOfExam = string.Empty;
        private int _PGRANK = 0;
        private decimal _pgscore = 0.0m;


        private int _pState = 0;
        private string _pPinCode = string.Empty;
        private string _pMobile = string.Empty;
        // admission details
        // private DateTime _admDate = DateTime.Today;
        private int _admtype = 0;
        private int _ugpgot = 0;
        private int _cityno = 0;
        private string _password = string.Empty;
        private double _fees = 0.0;
        private int _cdbno = 0;
        private DateTime? _admDate = null;
        //private bool _hosteler = false;
        private int _hosteler = 0; 


        private int _countryno = 0;
        private int _degreeNo = 0;
        private int _batchNo = 0;
        private int _branchNo = 0;
        private int _classYear = 0;
        private int _semesterNo = 0;
        private int _pType = 0;
        private int _examPtype = 0;  //examPtype -> field use in GEC only 

        private int _college_id = 0; //added by reena
        private int _stateNo = 0;
        private string _collegeCode = string.Empty;
        private string _birthPlace = string.Empty;
        private string _birthvillage = string.Empty;
        private string _birthtaluka = string.Empty;
        private string _birthdistrict = string.Empty;
        private string _birthdistate = string.Empty;

        private string _birthPinCode = string.Empty;
        private string _Specialization = string.Empty;
        


        private int _mToungeNo = 0;
        private string _otherLanguage = string.Empty;
        private decimal _height = 0.0m;
        private decimal _weight = 0;

        private string _identyMark = string.Empty;
        private int _bloodGroupNo = 0;
        private int _caste = 0;
        private string _subcaste = string.Empty;
        private bool _anti_ragging = false;        //Added by sachin on 22-07-2022
        
        private string _countryDomicile = string.Empty;
        private bool _irregular = false;
        private bool _urban = false;
        private int _year = 0;
        private bool _pro = false;
        private int _schemeNo = 0;
        private string _lastRollNo = string.Empty;
        private int _roll2 = 0;
        private string _accNo = string.Empty;
        private string _visano = string.Empty;
        private string _passportNo = string.Empty;
        private string _emailID = string.Empty;

        //private bool _idType = false;
        private string _schemeType = string.Empty;
        private int _idType = 0;
        private bool _admCancel = false;
        private int _admBatch = 0;
        private DateTime _leaveDate = DateTime.Today;
        private bool _can = false;
        private DateTime _canDate = DateTime.Today;
        private string _remark = string.Empty;
        private int _facAdvisor = 0;
        private string _prjname = string.Empty;
        private string _qualifyexamname = string.Empty;
        private decimal _percentage = 0.0m;
        private decimal _percentile = 0.0m;
        private string _yearOfExam = string.Empty;
        private string _qexmRollNo = string.Empty;
        private int uano = 0;
        private int _shift = 0;
        QualifiedExam[] _lastQualExams = null;
        private string _studId = string.Empty;
        private string _studentMobile = string.Empty;
        private int _cap_Institute = 0;
        //prospectus details
        private int _sessionNo = 0;
        private string _serialNo = string.Empty;
        private string _reciptNo = string.Empty;
        private DateTime _saleDate = DateTime.Today;
        private string _amount = string.Empty;
        private string _IPADDRESS;
        private DateTime _printDate = DateTime.Today;
        //BranchChange 
        private int _newbranchNo = 0;
        GEC_Student[] _paidDemandDrafts = null; // multiple dd details        

        private int _admroundNo = 0;
        private int _prosNo = 0;
        private string _District;
        private int _ALLINDIARANK = 0;
        private int _STATERANK = 0;
        private decimal _score = 0.0m;
        private string _paper = string.Empty;
        private string _paper_code = string.Empty;
        private string _colg_address = string.Empty;
        private int _transport = 0;
        private string _studentAlternateMobile = string.Empty;
        private string _fatherAlternateMobile = string.Empty;
        private string _motherAlternateMobile = string.Empty;
       
        
        
        

        private decimal _PERCENTILE = 0.00m;
        private string _qualifyNo = string.Empty;
        private int _admquotano = 0;
        // STUDNET TEMP DATA UPLOAD
        private string _gatescore = string.Empty;
        private string _gatereg = string.Empty;
        private string _gatepaper = string.Empty;
        private string _gateyear = string.Empty;
        private string _ph = string.Empty;
        private string _documents = string.Empty;
        private int _paytypeno = 0;
        private int _gatescholarship = 0;

        //upload sign userd
        private byte[] _studsign = null;
        private byte[] _studphoto = null;

        private int _bankno = 0;
        string _enrollNo = string.Empty;

        private int _phdstatus = 0;
        private int _phdsupervisorno = 0;

        private int _phdcosupervisorno1 = 0;
        private int _phdcosupervisorno2 = 0;

        private int _typesupervisorno = 0;
        private int _typecosupervisorno1 = 0;
        private int _typecosupervisorno2 = 0;

        private int _checkersdetailno = 0;
        private int _checkerno1 = 0;
        private int _checkerno2 = 0;
        private string _checkername1 = string.Empty;
        private string _checkername2 = string.Empty;
        private int _collatorno1 = 0;
        private int _collatorno2 = 0;

        private string _corres_address = string.Empty;
        private string _corres_pin = string.Empty;
        private string _corres_mob = string.Empty;
        private int _yearsofstudey = 0;
        private string _stateotherstate = string.Empty;

        

        private int _supervisorno = 0;
        private int _supervisormemberno = 0;
        private int _supervisorstatus = 0;

        private int _joinsupervisorno = 0;
        private int _joinsupervisormemberno = 0;
        private int _joinsupervisorstatus = 0;

        private int _institutefacultyno = 0;
        private int _institutefacmemberno = 0;
        private int _institutefacultystatus = 0;

        private int _drcno = 0;
        private int _drcmemberno = 0;
        private int _drcstatus = 0;

        private int _phdstatuscat = 0;

        private int _credits = 0;
        private string _topics = string.Empty;
        private string _workdone = string.Empty;
        private string _phdremark = string.Empty;
        private int _grade = 0;

        private DateTime _attempt1datewritten = DateTime.Today;
        private DateTime? _attempt2datewritten = DateTime.Today;
        private DateTime _attempt1dateoral = DateTime.Today;
        private DateTime? _attempt2dateoral = DateTime.Today;
        private DateTime _approveddate = DateTime.Today;

        private bool _net = false;
        private int _scholorship = 0;
        private int _physical_handicap = 0;

        private DateTime? _visaExpiryDate = null;
        private DateTime? _passportExpiryDate = null;
        private DateTime? _passportIssueDate = null;
        private DateTime? _stayPermitDate = null;
        private bool _indianOrigin = false;
        private string _agency = string.Empty;
        private string _scholarshipScheme = string.Empty;
        private string _passportIssuePlace = string.Empty;
        private string _citizenship = string.Empty;
        double _csabAmout = 0.00;

        private int _collegeid = 0;
        private string _class_admited = string.Empty;
        private string _stateof_domecial = string.Empty;

        private string _AllIndiaRollNo = string.Empty;
        private string _StateRollNo = string.Empty;
        private DateTime? _DOR = null;
        private string _motheremail = string.Empty;
        private string _fatheremail = string.Empty;
        private int _claimType = 0;

        private string _workexp = string.Empty;
        private string _designation = string.Empty;
        private string _orglastwork = string.Empty;
        private string _totalworkexp = string.Empty;
        private string _epfno = string.Empty;
        //hosteller & transport//Added by Rita M. 06-05-2019
        private int _hostelSts = 0;
        //Modified
        //private bool _transport = false; // Modified By Rishabh on 03/11/2021
        private int _transportSts = 0;

        //Added by Pritish 06052019
        private string _prospectusno = string.Empty;
        private DateTime _entrydate = DateTime.Today;
        private string _receiptno = string.Empty;
        private int _totalamt = 0;

        //Added by Rita 28052019
        private int _studType = 0;
        private int _noduests = 0;
        private DateTime? _noduesdt = null;
        private int _lock = 0;
        private int _unlock = 0;
        private int _newDegreeno = 0;

        //Added by abhishek 06-06-2019
        private string remark = string.Empty;
        private int _ScholershipTypeNo = 0;


        // Added By Nikhil V.Lambe on 10/02/2021 dor submit ifsc code and bank address.
        private string _ifsccode = string.Empty;
        private string _bankaddress = string.Empty;
        //------------------------------------------------------------------------------
        private string _sportName = string.Empty;
        private int _sportLevel = 0;
        private string _sportAchieve = string.Empty;
        QualifiedExam[] _EntranceExams = null;
        private string _MeritNo = string.Empty;
        private string _ApplicationID = string.Empty;
        private int _installment = 0;
        private string _studentMobile2 = string.Empty;
        private int _NewCollege_Id = 0; //Added by Dileep Kare on 05.01.2022
        private int _NewPtypeno = 0; //Added by Dileep Kare on 05.01.2022
        private int _clsAdvisor = 0;//Added by SP - 240122
        private int _FAsectionNo = 0;//Added by SP - 270122
        private int _clsDeptno = 0;//Added by SP - 240122

        //added by Rohit on 30-04-2022 for PHD 
        private string _typeofdisable = string.Empty;
        private string _AadharCardNo = string.Empty;
        private string _PhdInstitutename = string.Empty;
        private int _PhdNoc = 0;
        private string _pending_documents = string.Empty;
        private int _special = 0;

        private int _secondjoinsupervisorno = 0;
        private int _secondjoinsupervisormemberno = 0;
        private int _secondjoinsupervisorstatus = 0;
        private int _drcchairno = 0;
        private int _drcchairmemberno = 0;
        private string _superrole = string.Empty;
        private string _research = string.Empty;

        private int _schMode = 0; // added by SP
        private double _schamtorpercentage = 0.00;// added by SP

        #endregion





        //-----Phd module Synopsis
        private int _PhdExaminer1 = 0;
        private int _PhdExaminer1Status = 0;
        private int _PhdExaminer2 = 0;
        private int _PhdExaminer2Status = 0;
        private int _PhdExaminer3 = 0;
        private int _PhdExaminer3Status = 0;
        private int _PhdExaminer4 = 0;
        private int _PhdExaminer4Status = 0;
        private int _PhdExaminer5 = 0;
        private int _PhdExaminer5Status = 0;
        private int _PhdExaminer6 = 0;
        private int _PhdExaminer6Status = 0;
        private int _PhdExaminer7 = 0;
        private int _PhdExaminer7Status = 0;
        private int _PhdExaminer8 = 0;
        private int _PhdExaminer8Status = 0;
        private int _PhdExaminer9 = 0;
        private int _PhdExaminer9Status = 0;
        private int _PhdExaminer10 = 0;
        private int _PhdExaminer10Status = 0;


        private string _PhdExaminerFile1 = string.Empty;
        private string _PhdExaminerFile2 = string.Empty;
        private string _PhdExaminerFile3 = string.Empty;
        private string _PhdExaminerFile4 = string.Empty;

        private string _PhdStatusValue = string.Empty;


        //----- Phd presynopsis and synopsis
        private DateTime _PhdPresyndate = DateTime.Today;
        private string _PhdSynName = string.Empty;
        private string _PhdPreSynName = string.Empty;
        private string _PhdSynFile = string.Empty;
        private string _PhdPreSynFile = string.Empty;

        //  ---- Phd Tracker Priority 
        private string _PhdPriExaminer1 = string.Empty;
        private string _PhdPriExaminer2 = string.Empty;
        private string _PhdPriExaminer3 = string.Empty;
        private string _PhdPriExaminer4 = string.Empty;

        private string _IFSCcode = string.Empty;
        private string _Accholdername = string.Empty;
        private string _NADID = string.Empty;
        private string _ThesisTitle = string.Empty;
        private string _Descipline = string.Empty;
        //--- Phd degree completed 

        private string _ThesisTitleHindi = string.Empty;
        private int _PhdPassoutyear = 0;
        private int _PhdConvocationyear = 0;
        private decimal _PhddegreeTotalAmount = 0;
        private string _PhdFeesRef = string.Empty;
        private string _PhdDegreeRemark = string.Empty;
        private DateTime _PhdConvocationDate = DateTime.Today;
        //Added by Bhagyashree on 07062023
        private string _abccid = string.Empty;
        private string _dteappid = string.Empty;

        // Added by Shrikant Waghmare on  24-08-2023
        private int _academicyearno = 0;

        // Added by Shrikant Waghmare on 23-09-2023
        private string _casteName = string.Empty;

        //Added by Shrikant Waghmare on 26-09-2023
        private int _motherAnnualIncome = 0;

        // Added by Shrikant Waghmare on 23-10-2023
        private int _seattype = 0;
        private int _admcentre = 0;
        private int _defencequota = 0;
        private int _minorityquota = 0;

        // Added by Shrikant Waghmare on 16-12-2023
        private string _eligibiltyno = string.Empty;

        //methods

        #region Public Property Fields
        public decimal NataMarks //Added By Rishabh on 13/04/2022
            {
            get
                {
                return _nataMarks;
                }
            set
                {
                _nataMarks = value;
                }
            }

        public string InternationalStu // Added By Rishabh on 13/04/2022
            {
            get
                {
                return _internationalStu;
                }
            set
                {
                _internationalStu = value;
                }
            }
        public int NewCollege_ID //Added by Dileep Kare on 05.01.2022
        {
            get { return _NewCollege_Id; }
            set { _NewCollege_Id = value; }
        }
        public int NewPayTypeNO //Added by Dileep Kare on 05.01.2022
        {
            get { return _NewPtypeno; }
            set { _NewPtypeno = value; }
        }
        public string StudMobileno2
        {
            get { return _studentMobile2; }
            set { _studentMobile2 = value; }
        }
        public string MeritNo
        {
            get { return _MeritNo; }
            set { _MeritNo = value; }
        }
        public string ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        public int Installment
        {
            get { return _installment; }
            set { _installment = value; }
        }

        public int ScholershipTypeNo
        {
            get { return _ScholershipTypeNo; }
            set { _ScholershipTypeNo = value; }
        }

        public string EpfNo
        {
            get { return _epfno; }
            set { _epfno = value; }
        }
        public string TotalWorkExp
        {
            get { return _totalworkexp; }
            set { _totalworkexp = value; }
        }


        public string OrgLastWork
        {
            get { return _orglastwork; }
            set { _orglastwork = value; }
        }

        public string Designation
        {
            get { return _designation; }
            set { _designation = value; }
        }

        public string WorkExp
        {
            get { return _workexp; }
            set { _workexp = value; }
        }

        public string Annual_income
        {
            get { return _annual_income; }
            set { _annual_income = value; }
        }

        public string CollegeJss
        {
            get { return _collegejss; }
            set { _collegejss = value; }
        }
        public string Cetorderno
        {
            get { return _cetorderno; }
            set { _cetorderno = value; }
        }
        public string Cetdate
        {
            get { return _cetdate; }
            set { _cetdate = value; }
        }
        public decimal Cetamount
        {
            get { return _cetamount; }
            set { _cetamount = value; }
        }
        public string AllIndiaRollNo
        {
            get { return _AllIndiaRollNo; }
            set { _AllIndiaRollNo = value; }
        }
        public string StateRollNo
        {
            get { return _StateRollNo; }
            set { _StateRollNo = value; }
        }
        public DateTime? DOR
        {
            get { return _DOR; }
            set { _DOR = value; }
        }
        public int ClaimType
        {
            get { return _claimType; }
            set { _claimType = value; }
        }


        public int Physical_Handicap
        {
            get { return _physical_handicap; }
            set { _physical_handicap = value; }
        }
        public string fatherfirstName
        {
            get { return _fatherfirstName; }
            set { _fatherfirstName = value; }
        } 
         public string AddharcardNo
        {
            get { return _addharcardno; }
            set { _addharcardno = value; }
        }
        
        public int IdType
        {
            get { return _idType; }
            set { _idType = value; }
        }
        public string QexmRollNo
        {
            get { return _qexmRollNo; }
            set { _qexmRollNo = value; }
        }

        public string YearOfExam
        {
            get { return _yearOfExam; }
            set { _yearOfExam = value; }
        }
        public decimal Percentile
        {
            get { return _percentile; }
            set { _percentile = value; }
        }
        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }
        public string Qualifyexamname
        {
            get { return _qualifyexamname; }
            set { _qualifyexamname = value; }
        }
        public string EmailID
        {
            get { return _emailID; }
            set { _emailID = value; }
        }
        public string RegNo
        {
            get { return _regNo; }
            set { _regNo = value; }
        }
        public string OtherLanguage
        {
            get { return _otherLanguage; }
            set { _otherLanguage = value; }
        }

        public int MToungeNo
        {
            get { return _mToungeNo; }
            set { _mToungeNo = value; }
        }

        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public string BirthPlace
        {
            get { return _birthPlace; }
            set { _birthPlace = value; }
        }
        public string Birthvillage
        {
            get { return _birthvillage; }
            set { _birthvillage = value; }
        }
        public string Birthtaluka
        {
            get { return _birthtaluka; }
            set { _birthtaluka = value; }
        }
        public string Birthdistrict
        {
            get { return _birthdistrict; }
            set { _birthdistrict = value; }
        }
        public string Birthdistate
        {
            get { return _birthdistate; }
            set { _birthdistate = value; }
        }
        public decimal Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public int IdNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }
        public string PCity
        {
            get { return _pCity; }
            set { _pCity = value; }
        }

        public string RollNo
        {
            get { return _rollNo; }
            set { _rollNo = value; }
        }
        public int SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }
        public string StudName
        {
            get { return _studName; }
            set { _studName = value; }
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName= value; }
        }

        public string StudNameHindi
        {
            get { return _studNameHindi; }
            set { _studNameHindi = value; }
        }
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value; }
        }
        public string FatherMiddleName
        {
            get { return _fatherMiddleName; }
            set { _fatherMiddleName = value; }
        }
        public string FatherLastName
        {
            get { return _fatherLastName; }
            set { _fatherLastName = value; }
        }
        public string MotherName
        {
            get { return _motherName; }
            set { _motherName = value; }
        }
        public string FatherMobile
        {
            get { return _fatherMobile; }
            set { _fatherMobile = value; }
        }
        public string MotherMobile
        {
            get { return _motherMobile; }
            set { _motherMobile= value; }
        }
        public string FatherOfficeNo
        {
            get { return _fatherOfficeNo; }
            set { _fatherOfficeNo = value; }
        }
        public string MotherOfficeNo
        {
            get { return _motherMobile; }
            set { _motherMobile = value; }
        }
        public DateTime Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
        public string Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        public char Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public int ReligionNo
        {
            get { return _religionNo; }
            set { _religionNo = value; }
        }

        public char Married
        {
            get { return _married; }
            set { _married = value; }
        }
        public int NationalityNo
        {
            get { return _nationalityNo; }
            set { _nationalityNo = value; }
        }
        public int CategoryNo
        {
            get { return _categoryNo; }
            set { _categoryNo = value; }
        }
        public int AdmCategoryNo
        {
            get { return _admcategoryNo; }
            set { _admcategoryNo = value; }
        }
        // address info
        public string PAddress
        {
            get { return _pAddress; }
            set { _pAddress = value; }
        }

        public int Cdbno
        {
            get { return _cdbno; }
            set { _cdbno = value; }
        }

        public int PState
        {
            get { return _pState; }
            set { _pState = value; }
        }
        public string BirthPinCode
        {
            get { return _birthPinCode; }
            set { _birthPinCode = value; }
        }
        public string Specialization
        {
            get { return _Specialization; }
            set { _Specialization = value; }
        }
        public string PPinCode
        {
            get { return _pPinCode; }
            set { _pPinCode = value; }
        }
        public string PMobile
        {
            get { return _pMobile; }
            set { _pMobile = value; }
        }
        // admission details
        public int AdmType
        {
            get { return _admtype; }
            set { _admtype = value; }
        }

        public int Ugpgot
        {
            get { return _ugpgot; }
            set { _ugpgot = value; }
        }

        public int City
        {
            get { return _cityno; }
            set { _cityno = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public double Fees
        {
            get { return _fees; }
            set { _fees = value; }
        }
        public DateTime? AdmDate
        {
            get { return _admDate; }
            set { _admDate = value; }
        }
        //public bool Hosteler
        //{
        //    get { return _hosteler; }
        //    set { _hosteler = value; }
        //}
        public int Hosteler
        {
            get { return _hosteler; }
            set { _hosteler = value; }
        }

        public int CountryNo
        {
            get { return _countryno; }
            set { _countryno = value; }
        }


        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }
        public int BatchNo
        {
            get { return _batchNo; }
            set { _batchNo = value; }
        }
         public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }
        public int ClassYear
        {
            get { return _classYear; }
            set { _classYear = value; }
        }
        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }
        public int PType
        {
            get { return _pType; }
            set { _pType = value; }
        }

        public int ExamPtype  // only for gec project
        {
            get { return _examPtype; }
            set { _examPtype = value; }
        }

        public int StateNo
        {
            get { return _stateNo; }
            set { _stateNo = value; }
        }
        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }
        public string IdentyMark
        {
            get { return _identyMark; }
            set { _identyMark = value; }
        }

        public int BloodGroupNo
        {
            get { return _bloodGroupNo; }
            set { _bloodGroupNo = value; }
        }
        public int Caste
        {
            get { return _caste; }
            set { _caste = value; }
        }
        public string Subcaste
        {
            get { return _subcaste; }
            set { _subcaste = value; }
        }

        public bool Anti_Ragging
        {
            get { return _anti_ragging; }
            set { _anti_ragging = value; }

        }

        public string CountryDomicile
        {
            get { return _countryDomicile; }
            set { _countryDomicile = value; }
        }
        public bool Irregular
        {
            get { return _irregular; }
            set { _irregular = value; }
        }
        public bool Urban
        {
            get { return _urban; }
            set { _urban = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public bool Pro
        {
            get { return _pro; }
            set { _pro = value; }
        }
        public int SchemeNo
        {
            get { return _schemeNo; }
            set { _schemeNo = value; }
        }
        public string LastRollNo
        {
            get { return _lastRollNo; }
            set { _lastRollNo = value; }
        }
        public int Roll2
        {
            get { return _roll2; }
            set { _roll2 = value; }
        }
        public string AccNo
        {
            get { return _accNo; }
            set { _accNo = value; }
        }
        public string Visano
        {
            get { return _visano; }
            set { _visano = value; }
        }
        public string PassportNo
        {
            get { return _passportNo; }
            set { _passportNo = value; }
        }
        //public string EmailID
        //{
        //    get { return _emailID; }
        //    set { _emailID = value; }
        //}
        //public bool IdType
        //{
        //    get { return _idType; }
        //    set { _idType = value; }
        //}
        public bool AdmCancel
        {
            get { return _admCancel; }
            set { _admCancel = value; }
        }
        public int AdmBatch
        {
            get { return _admBatch; }
            set { _admBatch = value; }
        }
        public DateTime LeaveDate
        {
            get { return _leaveDate; }
            set { _leaveDate = value; }
        }
        public bool Can
        {
            get { return _can; }
            set { _can = value; }
        }
        public DateTime CanDate
        {
            get { return _canDate; }
            set { _canDate = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public int FacAdvisor
        {
            get { return _facAdvisor; }
            set { _facAdvisor = value; }
        }
        public string Prjname
        {
            get { return _prjname; }
            set { _prjname = value; }
        }
        public int Uano
        {
            get { return uano; }
            set { uano = value; }
        }
        public string SchemeType
        {
            get { return _schemeType; }
            set { _schemeType = value; }
        }
        
        public QualifiedExam[] LastQualifiedExams
        {
            get { return _lastQualExams; }
            set { _lastQualExams = value; }
        }
        public string StudId
        {
            get { return _studId; }
            set { _studId = value; }
        }


        public GEC_Student[] PaidDemandDrafts
        {
            get { return _paidDemandDrafts; }
            set { _paidDemandDrafts = value; }
        }

        public int AdmroundNo
        {
            get { return _admroundNo; }
            set { _admroundNo = value; }
        }

        public int Shift
        {
            get { return _shift; }
            set { _shift = value; }
        }
        public string StudentMobile
        {
            get { return _studentMobile; }
            set { _studentMobile = value; }
        }
        public string StudentAlternateMobile //Added By Rishabh
        {
            get { return _studentAlternateMobile; }
            set { _studentAlternateMobile = value; }
        }
        public string FatherAlternateMobile //Added By Rishabh
        {
            get { return _fatherAlternateMobile; }
            set { _fatherAlternateMobile = value; }
        }
        public string MotherAlternateMobile //Added By Rishabh
        {
            get { return _motherAlternateMobile; }
            set { _motherAlternateMobile = value; }
        }
        public int Cap_Institute
        {
            get { return _cap_Institute; }
            set { _cap_Institute = value; }
        }
        public string SerialNo
        {
            get { return _serialNo; }
            set { _serialNo = value; }
        }
        public DateTime SaleDate
        {
            get { return _saleDate; }
            set { _saleDate = value; }
        }
        public string Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
         public int SessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }
         public string IPADDRESS
         {
             get
             {
                 return this._IPADDRESS;
             }
             set
             {
                 if ((this._IPADDRESS != value))
                 {
                     this._IPADDRESS = value;
                 }
             }
         }

         public DateTime PrintDate
         {
             get { return _printDate; }
             set { _printDate = value; }
         }

         public string ReciptNo
         {
             get { return _reciptNo; }
             set { _reciptNo = value; }
         }
         public int NewBranchNo
         {
             get { return _newbranchNo; }
             set { _newbranchNo = value; }
         }
         public int ProsNo
         {
             get { return _prosNo; }
             set { _prosNo = value; }
         }
         public string PDISTRICT
         {
             get { return _District; }
             set { _District = value; }
         }
         public int ALLINDIARANK
         {
             get { return _ALLINDIARANK; }
             set { _ALLINDIARANK = value; }
         }
         public int STATERANK
         {
             get { return _STATERANK; }
             set { _STATERANK = value; }
         }
         public decimal Score
         {
             get { return _score; }
             set { _score = value; }
         }
         public string Paper
         {
             get { return _paper; }
             set { _paper = value; }
         }
         public string Paper_code
         {
             get { return _paper_code; }
             set { _paper_code = value; }
         }
         public decimal PERCENTILE
         {
             get { return _PERCENTILE; }
             set { _PERCENTILE = value; }
         }
         public string QUALIFYNO
         {
             get { return _qualifyNo; }
             set { _qualifyNo = value; }
         }
         public int ADMQUOTANO
         {
             get { return _admquotano; }
             set { _admquotano = value; }
         }

         public string GATE_SCORE
         {
             get { return _gatescore; }
             set { _gatescore = value; }
         }
         public string GATE_REG
         {
             get { return _gatereg; }
             set { _gatereg = value; }
         }
         public string GATE_PAPER
         {
             get { return _gatepaper; }
             set { _gatepaper = value; }
         }
         public string GATE_YEAR
         {
             get { return _gateyear; }
             set { _gateyear = value; }
         }

         public string PH
         {
             get { return _ph; }
             set { _ph = value; }
         }

         public string DOCUMENTS
         {
             get { return _documents; }
             set { _documents = value; }
         }
         public int PayTypeNO
         {
             get { return _paytypeno; }
             set { _paytypeno = value; }
         }

         public int GetScholarship
         {
             get { return _gatescholarship; }
             set { _gatescholarship = value; }
         }

         public byte[] StudSign
         {
             get { return _studsign; }
             set { _studsign = value; }
         }

         public byte[] StudPhoto
         {
             get { return _studphoto; }
             set { _studphoto = value; }
         }

         public int BankNo
         {
             get { return _bankno; }
             set { _bankno = value; }
         }

         public string EnrollNo
         {
             get { return _enrollNo; }
             set { _enrollNo = value; }
         }
         public int PhdStatus
         {
             get { return _phdstatus; }
             set { _phdstatus = value; }
         }
         public int PhdSupervisorNo
         {
             get { return _phdsupervisorno; }
             set { _phdsupervisorno = value; }
         }

         public int PhdCoSupervisorNo1
         {
             get { return _phdcosupervisorno1; }
             set { _phdcosupervisorno1 = value; }
         }

         public int PhdCoSupervisorNo2
         {
             get { return _phdcosupervisorno2; }
             set { _phdcosupervisorno2 = value; }
         }

         public int TypeSupervisorNo
         {
             get { return _typesupervisorno; }
             set { _typesupervisorno = value; }
         }

         public int TypeCoSupervisorNo1
         {
             get { return _typecosupervisorno1; }
             set { _typecosupervisorno1 = value; }
         }

         public int TypeCoSupervisorNo2
         {
             get { return _typecosupervisorno2; }
             set { _typecosupervisorno2 = value; }
         }


         public int CheckersdetailNo
         {
             get { return _checkersdetailno; }
             set { _checkersdetailno = value; }
         }
         public int CheckerNo1
         {
             get { return _checkerno1; }
             set { _checkerno1 = value; }
         }

         public int CheckerNo2
         {
             get { return _checkerno2; }
             set { _checkerno2 = value; }
         }
         public int CollatorNo1
         {
             get { return _collatorno1; }
             set { _collatorno1 = value; }
         }

         public int CollatorNo2
         {
             get { return _collatorno2; }
             set { _collatorno2 = value; }
         }

         public string CheckerName1
         {
             get { return _checkername1; }
             set { _checkername1 = value; }
         }
         public string CheckerName2
         {
             get { return _checkername2; }
             set { _checkername2 = value; }
         }

         public bool Net
         {
             get { return _net; }
             set { _net = value; }
         }

         public string Corres_address
         {
             get { return _corres_address; }
             set { _corres_address = value; }
         }
         public string Corres_pin
         {
             get { return _corres_pin; }
             set { _corres_pin = value; }
         }

         public string Corres_mob
         {
             get { return _corres_mob; }
             set { _corres_mob = value; }
         }
         public string Stateotherstate
         {
             get { return _stateotherstate; }
             set { _stateotherstate = value; }
         }
         public int Yearsofstudey
         {
             get { return _yearsofstudey; }
             set { _yearsofstudey = value; }
         }
         public string Colg_address
         {
             get { return _colg_address; }
             set { _colg_address = value; }
         }
         public int SupervisorNo
         {
             get { return _supervisorno; }
             set { _supervisorno = value; }
         }
         public int SupervisormemberNo
         {
             get { return _supervisormemberno; }
             set { _supervisormemberno = value; }
         }
         public int SupervisorStatus
         {
             get { return _supervisorstatus; }
             set { _supervisorstatus = value; }
         }

         public int JoinsupervisorNo
         {
             get { return _joinsupervisorno; }
             set { _joinsupervisorno = value; }
         }
         public int JoinsupervisormemberNo
         {
             get { return _joinsupervisormemberno; }
             set { _joinsupervisormemberno = value; }
         }
         public int JoinsupervisorStatus
         {
             get { return _joinsupervisorstatus; }
             set { _joinsupervisorstatus = value; }
         }


         public int InstitutefacultyNo
         {
             get { return _institutefacultyno; }
             set { _institutefacultyno = value; }
         }
         public int InstitutefacmemberNo
         {
             get { return _institutefacmemberno; }
             set { _institutefacmemberno = value; }
         }
         public int InstitutefacultyStatus
         {
             get { return _institutefacultystatus; }
             set { _institutefacultystatus = value; }
         }

         public int DrcNo
         {
             get { return _drcno; }
             set { _drcno = value; }
         }
         public int DrcmemberNo
         {
             get { return _drcmemberno; }
             set { _drcmemberno = value; }
         }
         public int Drcstatus
         {
             get { return _drcstatus; }
             set { _drcstatus = value; }
         }
         public int Phdstatuscat
         {
             get { return _phdstatuscat; }
             set { _phdstatuscat = value; }
         }
         public int Credits
         {
             get { return _credits; }
             set { _credits = value; }
         }
         public string Topics
         {
             get { return _topics; }
             set { _topics = value; }
         }
         public string Workdone
         {
             get { return _workdone; }
             set { _workdone = value; }
         }
         public string Phdremark
         {
             get { return _phdremark; }
             set { _phdremark = value; }
         }
         public int Grade
         {
             get { return _grade; }
             set { _grade = value; }
         }
         public int Scholorship
         {
             get { return _scholorship; }
             set { _scholorship = value; }
         }
         public DateTime Attempt1DateWritten
         {
             get { return _attempt1datewritten; }
             set { _attempt1datewritten = value; }
         }

         public DateTime? Attempt2DateWritten
         {
             get { return _attempt2datewritten; }
             set { _attempt2datewritten = value; }
         }

         public DateTime Attempt1DateOral
         {
             get { return _attempt1dateoral; }
             set { _attempt1dateoral = value; }
         }

         public DateTime? Attempt2DateOral
         {
             get { return _attempt2dateoral; }
             set { _attempt2dateoral = value; }
         }

         public DateTime ApprovedDate
         {
             get { return _approveddate; }
             set { _approveddate = value; }
         }
         public DateTime? VisaExpiryDate
         {
             get { return _visaExpiryDate; }
             set { _visaExpiryDate = value; }
         }
         public DateTime? PassportExpiryDate
         {
             get { return _passportExpiryDate; }
             set { _passportExpiryDate = value; }
         }
         public DateTime? PassportIssueDate
         {
             get { return _passportIssueDate; }
             set { _passportIssueDate = value; }
         }
         public DateTime? StayPermitDate
         {
             get { return _stayPermitDate; }
             set { _stayPermitDate = value; }
         }
         public bool IndianOrigin
         {
             get { return _indianOrigin; }
             set { _indianOrigin = value; }
         }
         public string Agency
         {
             get { return _agency; }
             set { _agency = value; }
         }
         public string ScholarshipScheme
         {
             get { return _scholarshipScheme; }
             set { _scholarshipScheme = value; }
         }
         public string PassportIssuePlace
         {
             get { return _passportIssuePlace; }
             set { _passportIssuePlace = value; }
         }
         public string Citizenship
         {
             get { return _citizenship; }
             set { _citizenship = value; }
         }


         public double CsabAmount
         {
             get { return _csabAmout; }
             set { _csabAmout = value; }
         }
         public int Collegeid
         {
             get { return _collegeid; }
             set { _collegeid = value; }
         }

         public string Class_admited
         {
             get { return _class_admited; }
             set { _class_admited = value; }
         }
         public string Stateof_domecial
         {
             get { return _stateof_domecial; }
             set { _stateof_domecial = value; }
         }
         public int College_ID
         {
             get { return _college_id; }
             set { _college_id = value; }
         }
         public string Motheremail
         {
             get { return _motheremail; }
             set { _motheremail = value; }
         }

         public string Fatheremail
         {
             get { return _fatheremail; }
             set { _fatheremail = value; }
         }
         // pg entrance exam scores

         public int PGQUALIFYNO
         {
             get { return _PGQUALIFYNO; }
             set { _PGQUALIFYNO = value; }
         }
         public string PGENTROLLNO
         {
             get { return _PGENTROLLNO; }
             set { _PGENTROLLNO = value; }
         }
         public decimal pgpercentage
         {
             get { return _pgpercentage; }
             set { _pgpercentage = value; }
         }
         public decimal pgpercentile
         {
             get { return _pgpercentile; }
             set { _pgpercentile = value; }
         }
         public string pgyearOfExam
         {
             get { return _pgyearOfExam; }
             set { _pgyearOfExam = value; }
         }
         public int PGRANK
         {
             get { return _PGRANK; }
             set { _PGRANK = value; }
         }
         public decimal pgscore
         {
             get { return _pgscore; }
             set { _pgscore = value; }
         }

         //Added By RitaM. 0605-2019
         public int HostelSts
         {
             get { return _hostelSts; }
             set { _hostelSts = value; }
         }
         //public bool Transportation    // Modified By Rishabh on 03/11/2021
         //{
         //    get
         //    {
         //        return _transport;
         //    }
         //    set
         //    {
         //        _transport = value;
         //    }
         //}

         public int Transportation
         {
             get { return _transport; }
             set { _transport = value; }
         }
         public int TransportSts
         {
             get { return _transportSts; }
             set { _transportSts = value; }
         }

        //Added By Pritish 0605-2019
         public string ProspectusNo
         {
             get { return _prospectusno; }
             set { _prospectusno = value; }
         }

         public DateTime EntryDate
         {
             get { return _entrydate; }
             set { _entrydate = value; }
         }

         public string ReceiptNo
         {
             get { return _receiptno; }
             set { _receiptno = value; }
         }

         public int TotalAmt
         {
             get { return _totalamt; }
             set { _totalamt = value; }
         }

         //Added by Rita on date 27052019

         public int StudType
         {
             get { return _studType; }
             set { _studType = value; }
         }
         public int NoDueStatus
         {
             get { return _noduests; }
             set { _noduests = value; }
         }

         public DateTime? NoDueDate
         {
             get { return _noduesdt; }
             set { _noduesdt = value; }
         }
         public int Lock
         {
             get { return _lock; }
             set { _lock = value; }
         }
         public int UnLock
         {
             get { return _unlock; }
             set { _unlock = value; }
         }
         public int NewDegreeNo
         {
             get { return _newDegreeno; }
             set { _newDegreeno = value; }
         }     
         //Added By abhishek 06-06-2019

         public string Remarks
         {
             get { return remark; }
             set { remark = value; }
         }


         //Added By Nikhil V.Lambe on 10/02/2021
         public string IfscCode
         {
             get { return _ifsccode; }
             set { _ifsccode = value; }
         }
         public string BankAddress
         {
             get { return _bankaddress; }
             set { _bankaddress = value; }
         }
        //Added By Deepali on 08/07/2021
         public string SportName
         {
             get { return _sportName; }
             set { _sportName = value; }
         }
         public int SportLevel
         {
             get { return _sportLevel; }
             set { _sportLevel = value; }
         }
         public string SportAchieve
         {
             get { return _sportAchieve; }
             set { _sportAchieve = value; }
         }

         public QualifiedExam[] EntranceExams
         {
             get { return _EntranceExams; }
             set { _EntranceExams = value; }
         }
         /// Added by SP
         /// </summary>
         public int ClassAdvisor
         {
             get
             {
                 return _clsAdvisor;
             }
             set
             {
                 _clsAdvisor = value;
             }
         }
         /// <summary>
         /// Added by SP
         /// </summary>
         public int FASectionNo
         {
             get
             {
                 return _FAsectionNo;
             }
             set
             {
                 _FAsectionNo = value;
             }
         }
         public int ClassDept
         {
             get
             {
                 return _clsDeptno;
             }
             set
             {
                 _clsDeptno = value;
             }
         }


         //added by Rohit on 30-04-2022 for PHD 
         public string TypeofDisablity
         {
             get { return _typeofdisable; }
             set { _typeofdisable = value; }
         }

         public string AadharCardNo
         {
             get { return _AadharCardNo; }
             set { _AadharCardNo = value; }
         }
         public string PhdInstitutename
         {
             get { return _PhdInstitutename; }
             set { _PhdInstitutename = value; }
         }
         public int PhdNoc
         {
             get { return _PhdNoc; }
             set { _PhdNoc = value; }
         }

         public string PENDING_DOCUMENTS
         {
             get { return _pending_documents; }
             set { _pending_documents = value; }
         }

         public int Special
         {
             get { return _special; }
             set { _special = value; }
         }
         public int Secondjoinsupervisorno
         {
             get { return _secondjoinsupervisorno; }
             set { _secondjoinsupervisorno = value; }
         }
         public int Secondjoinsupervisormemberno
         {
             get { return _secondjoinsupervisormemberno; }
             set { _secondjoinsupervisormemberno = value; }
         }
         public int Secondjoinsupervisorstatus
         {
             get { return _secondjoinsupervisorstatus; }
             set { _secondjoinsupervisorstatus = value; }
         }
         public int DrcChairNo
         {
             get { return _drcchairno; }
             set { _drcchairno = value; }
         }
         public int DrcChairmemberNo
         {
             get { return _drcchairmemberno; }
             set { _drcchairmemberno = value; }
         }
         public string SuperRole
         {
             get { return _superrole; }
             set { _superrole = value; }
         }
         public string Research
         {
             get { return _research; }
             set { _research = value; }
         }
         /// <summary>
         /// Added by SP
         /// </summary>
         public int SchMode
         {
             get
             {
                 return _schMode;
             }
             set
             {
                 _schMode = value;
             }
         }
         public double SchAmtOrPercentage
         {
             get
             {
                 return _schamtorpercentage;
             }
             set
             {
                 _schamtorpercentage = value;
             }
         }
        //End


        //end
        #endregion

         public int PhdExaminer1
         {
             get
             {
                 return _PhdExaminer1;
             }
             set
             {
                 _PhdExaminer1 = value;
             }
         }

         public int PhdExaminer2
         {
             get
             {
                 return _PhdExaminer2;
             }
             set
             {
                 _PhdExaminer2 = value;
             }
         }
         public int PhdExaminer3
         {
             get
             {
                 return _PhdExaminer3;
             }
             set
             {
                 _PhdExaminer3 = value;
             }
         }
         public int PhdExaminer4
         {
             get
             {
                 return _PhdExaminer4;
             }
             set
             {
                 _PhdExaminer4 = value;
             }
         }
         public int PhdExaminer5
         {
             get
             {
                 return _PhdExaminer5;
             }
             set
             {
                 _PhdExaminer5 = value;
             }
         }
         public int PhdExaminer6
         {
             get
             {
                 return _PhdExaminer6;
             }
             set
             {
                 _PhdExaminer6 = value;
             }
         }
         public int PhdExaminer7
         {
             get
             {
                 return _PhdExaminer7;
             }
             set
             {
                 _PhdExaminer7 = value;
             }
         }
         public int PhdExaminer8
         {
             get
             {
                 return _PhdExaminer8;
             }
             set
             {
                 _PhdExaminer8 = value;
             }
         }
         public int PhdExaminer9
         {
             get
             {
                 return _PhdExaminer9;
             }
             set
             {
                 _PhdExaminer9 = value;
             }
         }
         public int PhdExaminer10
         {
             get
             {
                 return _PhdExaminer10;
             }
             set
             {
                 _PhdExaminer10 = value;
             }
         }

         public string PhdStatusValue
         {
             get
             {
                 return _PhdStatusValue;
             }
             set
             {
                 _PhdStatusValue = value;
             }
         }

         public string PhdExaminerFile1
         {
             get { return _PhdExaminerFile1; }
             set { _PhdExaminerFile1 = value; }
         }


         public string PhdExaminerFile2
         {
             get { return _PhdExaminerFile2; }
             set { _PhdExaminerFile2 = value; }
         }
         public string PhdExaminerFile3
         {
             get { return _PhdExaminerFile3; }
             set { _PhdExaminerFile3 = value; }
         }
         public string PhdExaminerFile4
         {
             get
             {
                 return _PhdExaminerFile4;
             }
             set
             {
                 _PhdExaminerFile4 = value;
             }
         }




         public int PhdExaminer1Status
         {
             get { return _PhdExaminer1Status; }
             set { _PhdExaminer1Status = value; }
         }

         public int PhdExaminer2Status
         {
             get { return _PhdExaminer2Status; }
             set { _PhdExaminer2Status = value; }
         }
         public int PhdExaminer3Status
         {
             get { return _PhdExaminer3Status; }
             set { _PhdExaminer3Status = value; }
         }
         public int PhdExaminer4Status
         {
             get { return _PhdExaminer4Status; }
             set { _PhdExaminer4Status = value; }
         }
         public int PhdExaminer5Status
         {
             get { return _PhdExaminer5Status; }
             set { _PhdExaminer5Status = value; }
         }
         public int PhdExaminer6Status
         {
             get { return _PhdExaminer6Status; }
             set { _PhdExaminer6Status = value; }
         }
         public int PhdExaminer7Status
         {
             get { return _PhdExaminer7Status; }
             set { _PhdExaminer7Status = value; }
         }
         public int PhdExaminer8Status
         {
             get { return _PhdExaminer8Status; }
             set { _PhdExaminer8Status = value; }
         }
         public int PhdExaminer9Status
         {
             get { return _PhdExaminer9Status; }
             set { _PhdExaminer9Status = value; }
         }
         public int PhdExaminer10Status
         {
             get { return _PhdExaminer10Status; }
             set { _PhdExaminer10Status = value; }
         }



         public string NADID
         {
             get
             {
                 return _NADID;
             }
             set
             {
                 _NADID = value;
             }
         }


         public string IFSCcode
         {
             get
             {
                 return _IFSCcode;
             }
             set
             {
                 _IFSCcode = value;
             }
         }
         public string Accholdername
         {
             get
             {
                 return _Accholdername;
             }
             set
             {
                 _Accholdername = value;
             }
         }
         public string ThesisTitle
         {
             get
             {
                 return _ThesisTitle;
             }
             set
             {
                 _ThesisTitle = value;
             }
         }
         public string Descipline
         {
             get
             {
                 return _Descipline;
             }
             set
             {
                 _Descipline = value;
             }
         }


         //-------phd degree completed -- 02082018
         public int PhdPassoutyear
         {
             get
             {
                 return _PhdPassoutyear;
             }
             set
             {
                 _PhdPassoutyear = value;
             }
         }

         public int PhdConvocationyear
         {
             get
             {
                 return _PhdConvocationyear;
             }
             set
             {
                 _PhdConvocationyear = value;
             }
         }

         public string ThesisTitleHindi
         {
             get
             {
                 return _ThesisTitleHindi;
             }
             set
             {
                 _ThesisTitleHindi = value;
             }
         }

         public Decimal PhddegreeTotalAmount
         {
             get
             {
                 return _PhddegreeTotalAmount;
             }
             set
             {
                 _PhddegreeTotalAmount = value;
             }
         }

         public string PhdFeesRef
         {
             get
             {
                 return _PhdFeesRef;
             }
             set
             {
                 _PhdFeesRef = value;
             }
         }

         public string PhdDegreeRemark
         {
             get
             {
                 return _PhdDegreeRemark;
             }
             set
             {
                 _PhdDegreeRemark = value;
             }
         }

         public DateTime PhdConvocationDate
         {
             get
             {
                 return _PhdConvocationDate;
             }
             set
             {
                 _PhdConvocationDate = value;
             }
         }
         //------------------
         //----Phd Tracker Priority   ----//

         public string PhdPriExaminer1
         {
             get
             {
                 return _PhdPriExaminer1;
             }
             set
             {
                 _PhdPriExaminer1 = value;
             }
         }

         public string PhdPriExaminer2
         {
             get
             {
                 return _PhdPriExaminer2;
             }
             set
             {
                 _PhdPriExaminer2 = value;
             }
         }

         public string PhdPriExaminer3
         {
             get
             {
                 return _PhdPriExaminer3;
             }
             set
             {
                 _PhdPriExaminer3 = value;
             }
         }

         public string PhdPriExaminer4
         {
             get
             {
                 return _PhdPriExaminer4;
             }
             set
             {
                 _PhdPriExaminer4 = value;
             }
         }

         //----  Phd Pre synopsis and Synopsis
         public DateTime PhdPresyndate
         {
             get
             {
                 return _PhdPresyndate;
             }
             set
             {
                 _PhdPresyndate = value;
             }
         }

         public string PhdSynName
         {
             get
             {
                 return _PhdSynName;
             }
             set
             {
                 _PhdSynName = value;
             }
         }

         public string PhdPreSynName
         {
             get
             {
                 return _PhdPreSynName;
             }
             set
             {
                 _PhdPreSynName = value;
             }
         }

         public string PhdSynFile
         {
             get
             {
                 return _PhdSynFile;
             }
             set
             {
                 _PhdSynFile = value;
             }
         }

         public string PhdPreSynFile
         {
             get
             {
                 return _PhdPreSynFile;
             }
             set
             {
                 _PhdPreSynFile = value;
             }
         }

         public string AbccId
         {
             get { return _abccid; }
             set { _abccid = value; }
         }

         public string DteAppId
         {
             get { return _dteappid; }
             set { _dteappid = value; }
         }

         // Added By Shrikant W. on 24-08-2023   
         public int AcademicYearNo
         {
             get { return _academicyearno; }
             set { _academicyearno = value; }
         }


         // Added By Shrikant W. on 23-09-2023
         public string CasteName
         {
             get { return _casteName; }
             set { _casteName = value; }
         }

         // Added By Shrikant W. on 26-09-2023
         public int MotherAnnualIncome
         {
             get { return _motherAnnualIncome; }
             set { _motherAnnualIncome = value; }
         }

         public int SeatType
         {
             get { return _seattype; }
             set { _seattype = value; }
         }

         public int AdmissionCentre
         {
             get { return _admcentre; }
             set { _admcentre = value; }
         }

         public int DefenceQuota
         {
             get { return _defencequota; }
             set { _defencequota = value; }
         }

         public int MinorityQuota
         {
             get { return _minorityquota; }
             set { _minorityquota = value; }
         }
        
        // Added By Shrikant W. on 16-12-2023
         public string EligibilityNo
         {
             get { return _eligibiltyno; }
             set { _eligibiltyno = value; }
         }
    }
}